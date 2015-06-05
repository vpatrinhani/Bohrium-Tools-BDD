using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Reflection.Emit;
using System.Text.RegularExpressions;
using Bohrium.Core.Extensions;
using Bohrium.Tools.SpecflowReportTool.DataObjects;
using Bohrium.Tools.SpecflowReportTool.Extensions;
using Bohrium.Tools.SpecflowReportTool.Parsers;
using Bohrium.Tools.SpecflowReportTool.ReportObjects;
using Bohrium.Tools.SpecflowReportTool.Utils;
using System.CodeDom.Compiler;
using ICSharpCode.Decompiler;
using Mono.Cecil;
using Mono.Cecil.Cil;
using NUnit.Framework;
using TechTalk.SpecFlow;
using OpCodes = Mono.Cecil.Cil.OpCodes;

namespace Bohrium.Tools.SpecflowReportTool
{
    public class BDDReportService : IDisposable
    {
        private IList<SpecflowTestAssemblyAssetStore> assemblyAssetStores = new List<SpecflowTestAssemblyAssetStore>();

        public void ReadAssemblies(string[] inputAssemblies)
        {
            foreach (var inputAssembly in inputAssemblies)
            {
                ReadAssembly(inputAssembly);
            }
        }

        public void ReadAssembly(string inputAssembly)
        {
            assemblyAssetStores.Add(SpecflowTestAssemblyAssetStore.New(inputAssembly));
        }

        public BDDReport ExtractBDDReport()
        {
            var specflowReport = new BDDReport();

            specflowReport.MainFeaturesAssemblyAssetStore = assemblyAssetStores.First();

            specflowReport.StepDefinitionsReport = ExtractStepDefinitionsReport();

            specflowReport.FeaturesReport = ExtractFeaturesReport();

            specflowReport.ScenariosReport = ExtractScenariosReport(specflowReport.FeaturesReport);

            specflowReport.MapStepDefinitionsUsage();

            return specflowReport;
        }

        public FeaturesReport ExtractFeaturesReport()
        {
            //chkLoadedAssembly();

            Console.Write("Looking for Features ... ");

            var specflowUnitTests = getSpecflowFeatureUnitTests();

            var featuresReport = new FeaturesReport();

            featuresReport.Features = specflowUnitTests.ToList();

            Console.WriteLine("{0} Features found.", featuresReport.Features.Count);

            return featuresReport;
        }

        public ScenariosReport ExtractScenariosReport(FeaturesReport featuresReport)
        {
            //chkLoadedAssembly();

            if (featuresReport == null) throw new ArgumentNullException("featuresReport");

            Console.Write("Looking for Scenarios ... ");

            var specflowTestScenarios = getScenarioUnitTestsFromFeatureTestFixture();

            var scenariosReport = new ScenariosReport();

            scenariosReport.Scenarios = specflowTestScenarios.ToList();

            Console.WriteLine("{0} Scenarios found.", scenariosReport.Scenarios.Count);

            return scenariosReport;
        }

        public StepDefinitionsReport ExtractStepDefinitionsReport()
        {
            //chkLoadedAssembly();

            Console.Write("Looking for StepDefinitions ... ");

            var specflowStepDefinitions = getStepDefinitions();

            var scenariosReport = new StepDefinitionsReport();

            scenariosReport.StepDefinitions = specflowStepDefinitions.ToList();

            Console.WriteLine("{0} StepDefinitions found.", scenariosReport.StepDefinitions.Count);

            return scenariosReport;
        }

        private IEnumerable<StepDefinitionDO> getStepDefinitions()
        {
            var stepDefinitions = assemblyAssetStores.SelectMany(x => x.GetStepDefinitions()).ToList();

            return stepDefinitions;
        }

        private IEnumerable<FeatureUnitTestDO> getSpecflowFeatureUnitTests()
        {
            var featureUnitTests = assemblyAssetStores.SelectMany(x => x.GetFeatureUnitTests()).ToList();

            return featureUnitTests;
        }

        private IEnumerable<ScenarioUnitTestDO> getScenarioUnitTestsFromFeatureTestFixture()
        {
            var methodScenarios = assemblyAssetStores.SelectMany(x => x.GetScenarioUnitTestsFromFeatures()).ToList();

            return methodScenarios;
        }

        #region IDisposable Implementation

        // Flag: Has Dispose already been called? 
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                foreach (var specflowTestAssemblyAssetStore in assemblyAssetStores)
                {
                    specflowTestAssemblyAssetStore.Dispose();
                }

                assemblyAssetStores.Clear();
                assemblyAssetStores = null;
            }

            // Free any unmanaged objects here. 
            //
            disposed = true;
        }

        #endregion IDisposable Implementation
    }

    public class SpecflowTestAssemblyAssetStore : IDisposable
    {
        private AssemblyLoader assemblyLoader;
        private Assembly assembly;
        private AssemblyDefinition assemblyDefinition;

        public AssemblyLoader AssemblyLoader
        {
            get { return assemblyLoader; }
        }

        public Assembly Assembly
        {
            get { return assembly; }
        }

        public AssemblyDefinition AssemblyDefinition
        {
            get { return assemblyDefinition; }
        }

        public IList<FeatureUnitTestDO> ExtractedFeatures { get; set; }

        public IList<ScenarioUnitTestDO> ExtractedScenarios { get; set; }

        public IList<StepDefinitionDO> ExtractedStepDefinitions { get; set; }

        public static SpecflowTestAssemblyAssetStore New(string assemblyPath)
        {
            var assemblyAssetStore = new SpecflowTestAssemblyAssetStore();

            assemblyAssetStore.assemblyLoader = new AssemblyLoader();

            assemblyAssetStore.assembly = assemblyAssetStore.AssemblyLoader.LoadAssembly(assemblyPath);

            var resolver = new DefaultAssemblyResolver();
            resolver.AddSearchDirectory(Path.GetDirectoryName(assemblyAssetStore.assembly.Location));
            var parameters = new ReaderParameters
            {
                AssemblyResolver = resolver,
            };

            assemblyAssetStore.assemblyDefinition = AssemblyDefinition.ReadAssembly(assemblyAssetStore.assembly.Location, parameters);

            return assemblyAssetStore;
        }

        private void chkLoadedAssembly()
        {
            if ((assembly == null) || (assemblyDefinition == null))
                throw new InvalidOperationException("The Test assembly need to be loaded before running the method 'ReadAssembly'.");
        }

        public IEnumerable<FeatureUnitTestDO> GetFeatureUnitTests()
        {
            ExtractedFeatures = new List<FeatureUnitTestDO>();

            var featureUnitTestTypes = assembly.GetTypes()
                .Where(t =>
                    (t.GetCustomAttributes<TestFixtureAttribute>().Any())
                    && (t.GetCustomAttributes<GeneratedCodeAttribute>().Any(a => a.Tool == "TechTalk.SpecFlow")))
                .ToList();

            foreach (var featureUnitTestType in featureUnitTestTypes.AsParallel())
            {
                var featureUnitTestTypeDefinition = assemblyDefinition.MainModule.Types
                    .SingleOrDefault(t => t.FullName == featureUnitTestType.FullName);

                var featureUnitTestClass = new FeatureUnitTestDO();

                featureUnitTestClass.TargetType = featureUnitTestType;

                var descriptionAttribute = featureUnitTestType.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();

                featureUnitTestClass.Description = (descriptionAttribute != null)
                    ? descriptionAttribute.Description
                    : null;

                var categoryAttributes = featureUnitTestType.GetCustomAttributes<CategoryAttribute>();

                if (categoryAttributes != null)
                {
                    foreach (var categoryAttribute in categoryAttributes)
                    {
                        featureUnitTestClass.Tags.Add(categoryAttribute.Name);
                    }
                }

                ExtractedFeatures.Add(featureUnitTestClass);

                var featureBackgroundMethodInfo = featureUnitTestType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .FirstOrDefault(m => m.Name.Equals("FeatureBackground", StringComparison.InvariantCulture));

                if (featureBackgroundMethodInfo != null)
                {
                    var methodDefinition = featureUnitTestTypeDefinition.Methods
                        .SingleOrDefault(m => m.Name == featureBackgroundMethodInfo.Name);

                    var backgroundFeatureDO = new BackgroundFeatureDO()
                    {
                        ParentFeature = featureUnitTestClass.ObjectId
                    };

                    string methodBodySourceCode = methodDefinition.GetSourceCode();

                    IList<GherkinBaseStatementDO> statements;

                    int lastIndexOf = methodBodySourceCode.LastIndexOf("}");
                    if (lastIndexOf > 0)
                    {
                        methodBodySourceCode = methodBodySourceCode.Insert(lastIndexOf - 1, "this.ScenarioCleanup();");

                        parseGivenWhenThenStatements(methodBodySourceCode, out statements);

                        backgroundFeatureDO.Statements = statements.ToList();
                    }

                    featureUnitTestClass.Background = backgroundFeatureDO;
                }
            }

            return ExtractedFeatures;
        }

        public IEnumerable<ScenarioUnitTestDO> GetScenarioUnitTestsFromFeatures()
        {
            ExtractedScenarios = new List<ScenarioUnitTestDO>();

            foreach (var featureUnitTestClass in ExtractedFeatures.AsParallel())
            {
                var featureUnitTestTypeDefinition = assemblyDefinition.MainModule.Types
                    .SingleOrDefault(t => t.FullName == featureUnitTestClass.TargetType.FullName);

                var methodInfos = featureUnitTestClass.TargetType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);

                foreach (var methodScenario in methodInfos.Where(methodInfo => methodInfo.GetCustomAttributes<TestAttribute>().Any()).AsParallel())
                {
                    var scenarioUnitTestClass = new ScenarioUnitTestDO();

                    scenarioUnitTestClass.ParentFeature = featureUnitTestClass.ObjectId;
                    scenarioUnitTestClass.TargetType = methodScenario;

                    var descriptionAttribute = methodScenario.GetCustomAttributes<DescriptionAttribute>().FirstOrDefault();

                    scenarioUnitTestClass.Description = (descriptionAttribute != null)
                        ? descriptionAttribute.Description
                        : null;

                    var categoryAttributes = methodScenario.GetCustomAttributes<CategoryAttribute>();

                    if (categoryAttributes != null)
                    {
                        foreach (var categoryAttribute in categoryAttributes)
                        {
                            scenarioUnitTestClass.Tags.Add(categoryAttribute.Name);
                        }
                    }

                    var methodDefinition = featureUnitTestTypeDefinition.Methods
                        .SingleOrDefault(m => m.Name == methodScenario.Name);

                    string methodBodySourceCode = methodDefinition.GetSourceCode();

                    IList<GherkinBaseStatementDO> statements;

                    parseGivenWhenThenStatements(methodBodySourceCode, out statements);

                    scenarioUnitTestClass.Statements = statements.ToList();

                    ExtractedScenarios.Add(scenarioUnitTestClass);
                }
            }

            return ExtractedScenarios;
        }

        public IEnumerable<StepDefinitionDO> GetStepDefinitions()
        {
            ExtractedStepDefinitions = new List<StepDefinitionDO>();

            var stepDefinitionsBindTypes = assembly.GetTypes()
                .Where(t => t.GetCustomAttributes<BindingAttribute>().Any())
                .ToList();

            foreach (var stepDefinitionsBindType in stepDefinitionsBindTypes)
            {
                var stepDefinitionMethodInfos = stepDefinitionsBindType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                    .Where(methodInfo => methodInfo.GetCustomAttributes<StepDefinitionBaseAttribute>().Any());

                foreach (var stepDefinitionMethodInfo in stepDefinitionMethodInfos)
                {
                    var stepDefinitionDO = new StepDefinitionDO();

                    stepDefinitionDO.StepDefinitionMethodName = stepDefinitionMethodInfo.Name;
                    stepDefinitionDO.StepDefinitionMethodSignature =
                        stepDefinitionMethodInfo.GetMethodSignatureRepesentation();

                    var stepDefinitionBaseAttributes = stepDefinitionMethodInfo.GetCustomAttributes<StepDefinitionBaseAttribute>();

                    foreach (var stepDefinitionBaseAttribute in stepDefinitionBaseAttributes)
                    {
                        var baseStepDefinitionTypeDO = stepDefinitionBaseAttribute.GetStepDefinitionTypeDO();

                        baseStepDefinitionTypeDO.ParentStepDefinitionId = stepDefinitionDO.ObjectId;

                        baseStepDefinitionTypeDO.ParentStepDefinition = stepDefinitionDO;

                        stepDefinitionDO.StepDefinitionTypes.Add(baseStepDefinitionTypeDO);
                    }

                    ExtractedStepDefinitions.Add(stepDefinitionDO);
                }
            }

            return ExtractedStepDefinitions;
        }

        private void parseGivenWhenThenStatements(string methodBodySourceCode, out IList<GherkinBaseStatementDO> statements)
        {
            statements = new List<GherkinBaseStatementDO>();

            var scenarioMethodSourceSyntaxParser = new ScenarioMethodSourceSyntaxParser(methodBodySourceCode);

            var matchGroupStatements = scenarioMethodSourceSyntaxParser.ParseGroupStatements();

            foreach (Match matchGroupStatement in matchGroupStatements)
            {
                if (matchGroupStatement.Groups["given"].Success)
                {
                    var matchesGiven = scenarioMethodSourceSyntaxParser.ParseGroupGiven(matchGroupStatement.Groups["given"].Value);
                    foreach (Match givenMatch in matchesGiven)
                    {
                        if (givenMatch.Success)
                        {
                            var givenStatementClass = new GivenStatementDO();

                            givenStatementClass.FillFromMatch(givenMatch, scenarioMethodSourceSyntaxParser);

                            statements.Add(givenStatementClass);
                        }
                    }
                }
                else if (matchGroupStatement.Groups["when"].Success)
                {
                    var matchesWhen = scenarioMethodSourceSyntaxParser.ParseGroupWhen(matchGroupStatement.Groups["when"].Value);
                    foreach (Match whenMatch in matchesWhen)
                    {
                        if (whenMatch.Success)
                        {
                            var whenStatementClass = new WhenStatementDO();

                            whenStatementClass.FillFromMatch(whenMatch, scenarioMethodSourceSyntaxParser);

                            statements.Add(whenStatementClass);
                        }
                    }
                }
                else if (matchGroupStatement.Groups["then"].Success)
                {
                    var matchesThen = scenarioMethodSourceSyntaxParser.ParseGroupThen(matchGroupStatement.Groups["then"].Value);
                    foreach (Match thenMatch in matchesThen)
                    {
                        if (thenMatch.Success)
                        {
                            var thenStatementClass = new ThenStatementDO();

                            thenStatementClass.FillFromMatch(thenMatch, scenarioMethodSourceSyntaxParser);

                            statements.Add(thenStatementClass);
                        }
                    }
                }
            }
        }

        #region IDisposable Implementation

        // Flag: Has Dispose already been called? 
        bool disposed = false;

        // Public implementation of Dispose pattern callable by consumers. 
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        // Protected implementation of Dispose pattern. 
        protected virtual void Dispose(bool disposing)
        {
            if (disposed)
                return;

            if (disposing)
            {
                // Free any other managed objects here. 
                assemblyLoader.Dispose();
                assemblyLoader = null;
            }

            // Free any unmanaged objects here. 
            //
            disposed = true;
        }

        #endregion IDisposable Implementation
    }
}
