using Bohrium.Tools.BDDManagementTool.Data;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Entities;
using Bohrium.Tools.BDDManagementTool.Data.EntityFramework.Repository.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using System.Xml.Schema;

namespace BDDManagementTool.EntityFrameworkImporterApp
{
    class Program
    {
        private static string xsiNamespace = @"http://www.w3.org/2001/XMLSchema-instance";
        private static string baseDataPath = @"C:\Development\Github\vpatrinhani\Bohrium-Tools-BDD\src\BDDManagementTool.WebApp\App_Data";

        private static int featureCount = 0;
        private static int scenarioCount = 0;

        static void Main(string[] args)
        {
            importStepDefinitions();

            importFeaturesAndScenarios();

            Console.WriteLine("Press <ENTER> to exit...");
            Console.ReadLine();
        }

        private static void importFeaturesAndScenarios()
        {
            string fileToImportFeatures = Path.Combine(baseDataPath, @"features-report.xml");
            string fileToImportScenarios = Path.Combine(baseDataPath, @"scenarios-report.xml");

            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            var xmlReaderSettings = new XmlReaderSettings()
            {
                CheckCharacters = false,
                DtdProcessing = DtdProcessing.Ignore,
                ValidationType = ValidationType.None,
                ValidationFlags = XmlSchemaValidationFlags.None,
                IgnoreProcessingInstructions = true,
                IgnoreComments = true,
                IgnoreWhitespace = true,
            };

            using (var xmlReaderFeatures = XmlReader.Create(fileToImportFeatures, xmlReaderSettings))
            {
                while (xmlReaderFeatures.Read())
                {
                    if (xmlReaderFeatures.IsStartElement())
                    {
                        using (var unitOfWork = new UnitOfWork(null, new BDDMgmtDbContext()))
                        {
                            RepositoryStorage repositoryStorage = new RepositoryStorage()
                            {
                                FeatureRepository = new FeatureRepository(unitOfWork),
                                StepRepository = new StepRepository(unitOfWork),
                                TableParameterRepository = new TableParameterRepository(unitOfWork),
                            };
                            
                            if (parseAndImportFeature(xmlReaderFeatures, unitOfWork, repositoryStorage))
                            {
                                unitOfWork.Commit();
                            }
                        }
                    }
                }
            }

            stopwatch.Stop();

            Console.WriteLine("{0} Features Finished in {1}", featureCount, stopwatch.Elapsed);

            stopwatch.Restart();

            var dbContext = new BDDMgmtDbContext();

            dbContext.Configuration.AutoDetectChangesEnabled = false;
            dbContext.Configuration.ValidateOnSaveEnabled = false;

            using (var xmlReaderScenarios = XmlReader.Create(fileToImportScenarios, xmlReaderSettings))
            {
                while (xmlReaderScenarios.Read())
                {
                    if (xmlReaderScenarios.IsStartElement())
                    {
                        using (var unitOfWork = new UnitOfWork(null, new BDDMgmtDbContext()))
                        {
                            RepositoryStorage repositoryStorage = new RepositoryStorage()
                            {
                                ScenarioRepository = new ScenarioRepository(unitOfWork),
                                StepRepository = new StepRepository(unitOfWork),
                                TableParameterRepository = new TableParameterRepository(unitOfWork),
                            };

                            if (parseAndImportScenario(xmlReaderScenarios, unitOfWork, repositoryStorage))
                            {
                                unitOfWork.Commit();
                            }
                        }
                    }
                }
            }

            stopwatch.Stop();

            Console.WriteLine("{0} Scenarios Finished in {1}", scenarioCount, stopwatch.Elapsed);

            //using (var unitOfWork = new UnitOfWork(null, new BDDMgmtDbContext()))
            //{
            //    var featureRepository = new FeatureRepository(unitOfWork);

            //    var featureElements = xDocFeatures.Descendants(XName.Get("Feature"));

            //    foreach (var featureElement in featureElements)
            //    {
            //        var feature = new Feature()
            //        {
            //            Id = Guid.Parse(featureElement.Attribute(XName.Get("ObjectId")).Value),
            //            Description = featureElement.Attribute(XName.Get("Description")).Value,
            //        };

            //        featureRepository.Add(feature);

            //        var backgroundStepElements = featureElement.Descendants(XName.Get("Statement"));

            //        parseAndImportSteps(feature.Id, null, backgroundStepElements, unityOfWork);
            //    }

            //    unityOfWork.Commit();
            //}

            //var xDocScenarios = XDocument.Load(fileToImportScenarios);

            //using (var unitOfWork = new UnitOfWork(null, new BDDMgmtDbContext()))
            //{
            //    var scenarioRepository = new ScenarioRepository(unitOfWork);

            //    var scenarioElements = xDocScenarios.Descendants(XName.Get("Scenario"));

            //    foreach (var scenarioElement in scenarioElements)
            //    {
            //        var scenario = new Scenario()
            //        {
            //            Id = Guid.Parse(scenarioElement.Attribute(XName.Get("ObjectId")).Value),
            //            Description = scenarioElement.Attribute(XName.Get("Description")).Value,
            //            FeatureId = Guid.Parse(scenarioElement.Attribute(XName.Get("ParentFeature")).Value),
            //        };

            //        scenarioRepository.Add(scenario);

            //        var stepElements = scenarioElement.Descendants(XName.Get("Statement"));

            //        parseAndImportSteps(null, scenario.Id, stepElements, unitOfWork);

            //        unitOfWork.Commit();
            //    }

            //    unitOfWork.Commit();
            //}
        }

        private static bool validateFeatureElement(XmlReader xmlReader)
        {
            bool result = ((xmlReader.NodeType == System.Xml.XmlNodeType.Element)
                && (xmlReader.Name == "Feature"));

            return result;
        }

        private static bool parseAndImportFeature(XmlReader xmlReader, IUnitOfWork unitOfWork, RepositoryStorage repositoryStorage)
        {
            if (!validateFeatureElement(xmlReader)) return false;

            featureCount++;

            var feature = new Feature()
            {
                Id = Guid.Parse(xmlReader.GetAttribute("ObjectId")),
                Description = xmlReader.GetAttribute("Description"),
            };

            repositoryStorage.FeatureRepository.Add(feature);

            using (var xmlReaderSubtree = xmlReader.ReadSubtree())
            {
                while (xmlReaderSubtree.Read())
                {
                    if (xmlReaderSubtree.IsStartElement())
                    {
                        if (parseAndImportStep(feature.Id, null, xmlReaderSubtree, unitOfWork, repositoryStorage))
                        {
                            unitOfWork.Commit();
                        }
                    }
                }
            }

            return true;
        }

        private static bool validateScenarioElement(XmlReader xmlReader)
        {
            bool result = ((xmlReader.NodeType == System.Xml.XmlNodeType.Element)
                && (xmlReader.Name == "Scenario"));

            return result;
        }

        private static bool parseAndImportScenario(XmlReader xmlReader, IUnitOfWork unitOfWork, RepositoryStorage repositoryStorage)
        {
            if (!validateScenarioElement(xmlReader)) return false;

            scenarioCount++;

            var scenario = new Scenario()
            {
                Id = Guid.Parse(xmlReader.GetAttribute("ObjectId")),
                Description = xmlReader.GetAttribute("Description"),
                FeatureId = Guid.Parse(xmlReader.GetAttribute("ParentFeature")),
            };

            repositoryStorage.ScenarioRepository.Add(scenario);

            using (var xmlReaderSubtree = xmlReader.ReadSubtree())
            {
                while (xmlReaderSubtree.Read())
                {
                    if (xmlReaderSubtree.IsStartElement())
                    {
                        if (parseAndImportStep(null, scenario.Id, xmlReaderSubtree, unitOfWork, repositoryStorage))
                        {
                            unitOfWork.Commit();
                        }
                    }
                }
            }

            return true;
        }

        private static bool validateStepElement(XmlReader xmlReader)
        {
            bool result = ((xmlReader.NodeType == System.Xml.XmlNodeType.Element)
                && (xmlReader.Name == "Statement"));

            return result;
        }

        private static bool parseAndImportStep(Guid? featureId, Guid? scenarioId, XmlReader xmlReader, IUnitOfWork unitOfWork, RepositoryStorage repositoryStorage)
        {
            if (!validateStepElement(xmlReader)) return false;

            var step = new Step()
            {
                Id = Guid.Parse(xmlReader.GetAttribute("ObjectId")),
                Type = xmlReader.GetAttribute("type", xsiNamespace),
                Keyword = xmlReader.GetAttribute("Keyword"),
                Description = xmlReader.GetAttribute("Statement"),
                FeatureId = featureId,
                ScenarioId = scenarioId,
            };

            using (var xmlReaderSubtree = xmlReader.ReadSubtree())
            {
                while (xmlReaderSubtree.Read())
                {
                    if (xmlReaderSubtree.IsStartElement())
                    {
                        parseAndImportTableParameter(step.Id, xmlReaderSubtree, unitOfWork, repositoryStorage);

                        parseAndImportStepDefinitionReference(xmlReaderSubtree, unitOfWork, step);
                    }
                }
            }

            repositoryStorage.StepRepository.Add(step);

            return true;
        }

        private static bool validateStepDefinitionReferenceElement(XmlReader xmlReader)
        {
            bool result = ((xmlReader.NodeType == System.Xml.XmlNodeType.Element)
                && (xmlReader.Name == "StepDefinitionReference"));

            return result;
        }

        private static bool parseAndImportStepDefinitionReference(XmlReader xmlReader, IUnitOfWork unitOfWork, Step step)
        {
            if (!validateStepDefinitionReferenceElement(xmlReader)) return false;

            step.StepDefinitionId = Guid.Parse(xmlReader.GetAttribute("StepDefinitionId"));

            using (var xmlReaderSubtree = xmlReader.ReadSubtree())
            {
                while (xmlReaderSubtree.Read())
                {
                    //parseAndImportStepDefinitionTypeReference(xmlReader, unitOfWork);
                }
            }

            return true;
        }

        //parseAndImportStepDefinitionTypeReference(xmlReader, unitOfWork);

        private static bool validateTableParameterElement(XmlReader xmlReader)
        {
            bool result = ((xmlReader.NodeType == System.Xml.XmlNodeType.Element)
                && (xmlReader.Name == "TableParameter"));

            return result;
        }

        private static bool parseAndImportTableParameter(Guid stepId, XmlReader xmlReader, IUnitOfWork unitOfWork, RepositoryStorage repositoryStorage)
        {
            if (!validateTableParameterElement(xmlReader)) return false;

            var tableParameter = new TableParameter()
            {
                Id = Guid.Parse(xmlReader.GetAttribute("ObjectId")),
                StepId = stepId,
            };

            repositoryStorage.TableParameterRepository.Add(tableParameter);

            using (var xmlReaderSubtree = xmlReader.ReadSubtree())
            {
                while (xmlReaderSubtree.Read())
                {
                    if (xmlReaderSubtree.IsStartElement())
                    {
                        parseAndImportTableParameterColumn(tableParameter.Id, xmlReaderSubtree, unitOfWork, repositoryStorage);

                        parseAndImportTableParameterRow(tableParameter.Id, xmlReaderSubtree, unitOfWork, repositoryStorage);
                    }
                }
            }

            return true;
        }

        private static bool validateTableParameterColumnElement(XmlReader xmlReader)
        {
            bool result = ((xmlReader.NodeType == System.Xml.XmlNodeType.Element)
                && (xmlReader.Name == "Column"));

            return result;
        }

        private static bool parseAndImportTableParameterColumn(Guid tableParameterId, XmlReader xmlReader, IUnitOfWork unitOfWork, RepositoryStorage repositoryStorage)
        {
            if (!validateTableParameterColumnElement(xmlReader)) return false;

            var tableParameterColumn = new TableParameterColumn()
            {
                Id = Guid.Parse(xmlReader.GetAttribute("ObjectId")),
                Label = xmlReader.GetAttribute("Value"),
                TableParameterId = tableParameterId,
            };

            repositoryStorage.TableParameterRepository.AddColumn(tableParameterColumn);

            return true;
        }

        private static bool validateTableParameterRowElement(XmlReader xmlReader)
        {
            bool result = ((xmlReader.NodeType == System.Xml.XmlNodeType.Element)
                && (xmlReader.Name == "Row"));

            return result;
        }

        private static bool parseAndImportTableParameterRow(Guid tableParameterId, XmlReader xmlReader, IUnitOfWork unitOfWork, RepositoryStorage repositoryStorage)
        {
            if (!validateTableParameterRowElement(xmlReader)) return false;

            var tableParameterRow = new TableParameterRow()
            {
                Id = Guid.Parse(xmlReader.GetAttribute("ObjectId")),
                TableParameterId = tableParameterId,
            };

            repositoryStorage.TableParameterRepository.AddRow(tableParameterRow);

            using (var xmlReaderSubtree = xmlReader.ReadSubtree())
            {
                while (xmlReaderSubtree.Read())
                {
                    if (xmlReaderSubtree.IsStartElement())
                    {
                        parseAndImportTableParameterCell(tableParameterRow.Id, xmlReaderSubtree, unitOfWork, repositoryStorage);
                    }
                }
            }

            return true;
        }

        private static bool validateTableParameterCellElement(XmlReader xmlReader)
        {
            bool result = ((xmlReader.NodeType == System.Xml.XmlNodeType.Element)
                && (xmlReader.Name == "Cell"));

            return result;
        }

        private static bool parseAndImportTableParameterCell(Guid tableParameterRowId, XmlReader xmlReader, IUnitOfWork unitOfWork, RepositoryStorage repositoryStorage)
        {
            if (!validateTableParameterCellElement(xmlReader)) return false;

            Guid tableParameterColumnId = Guid.Parse(xmlReader.GetAttribute("HeaderColumnId"));

            var tableParameterCell = new TableParameterCell()
            {
                Id = Guid.Parse(xmlReader.GetAttribute("ObjectId")),
                TableParameterColumnId = tableParameterColumnId,
                TableParameterRowId = tableParameterRowId,
                Value = xmlReader.GetAttribute("Value"),
            };

            repositoryStorage.TableParameterRepository.AddCell(tableParameterCell);

            return true;
        }

        private static void parseAndImportSteps(Guid? featureId, Guid? scenarioId, IEnumerable<XElement> stepElements, IUnitOfWork unitOfWork)
        {
            var stepRepository = new StepRepository(unitOfWork);
            var tableParameterRepository = new TableParameterRepository(unitOfWork);

            foreach (var stepElement in stepElements)
            {
                var step = new Step()
                {
                    Id = Guid.Parse(stepElement.Attribute(XName.Get("ObjectId")).Value),
                    Type = stepElement.Attribute(XName.Get("type", xsiNamespace)).Value,
                    Keyword = stepElement.Attribute(XName.Get("Keyword")).Value,
                    Description = stepElement.Attribute(XName.Get("Statement")).Value,
                    FeatureId = featureId,
                    ScenarioId = scenarioId,
                };

                stepRepository.Add(step);

                var stepDefinitionReferenceElement = stepElement.Descendants(XName.Get("StepDefinitionReference")).Where(child => child.Parent == stepElement).SingleOrDefault();
                if (stepDefinitionReferenceElement != null)
                {
                    step.StepDefinitionId = Guid.Parse(stepDefinitionReferenceElement.Attribute(XName.Get("StepDefinitionId")).Value);

                    var stepDefinitionTypeReferenceElements = stepDefinitionReferenceElement.Descendants(XName.Get("StepDefinitionTypeReference"));
                }

                parseAndImportTableParameters(step.Id, stepElement, tableParameterRepository);
            }
        }

        private static void parseAndImportTableParameters(Guid stepId, XElement parentElement, TableParameterRepository tableParameterRepository)
        {
            var tableParameterElements = parentElement.Descendants(XName.Get("TableParameter"));

            foreach (var tableParameterElement in tableParameterElements)
            {
                var tableParameter = new TableParameter()
                {
                    Id = Guid.Parse(tableParameterElement.Attribute(XName.Get("ObjectId")).Value),
                    StepId = stepId,
                };

                tableParameterRepository.Add(tableParameter);

                var tableParameterColumnElements = tableParameterElement.Descendants(XName.Get("Column"));

                foreach (var tableParameterColumnElement in tableParameterColumnElements)
                {
                    var tableParameterColumn = new TableParameterColumn()
                    {
                        Id = Guid.Parse(tableParameterColumnElement.Attribute(XName.Get("ObjectId")).Value),
                        Label = tableParameterColumnElement.Attribute(XName.Get("Value")).Value,
                        TableParameterId = tableParameter.Id,
                    };

                    tableParameterRepository.AddColumn(tableParameterColumn);
                }

                var tableParameterRowElements = tableParameterElement.Descendants(XName.Get("Row"));

                foreach (var tableParameterRowElement in tableParameterRowElements)
                {
                    var tableParameterRow = new TableParameterRow()
                    {
                        Id = Guid.Parse(tableParameterRowElement.Attribute(XName.Get("ObjectId")).Value),
                        TableParameterId = tableParameter.Id,
                    };

                    tableParameterRepository.AddRow(tableParameterRow);

                    var tableParameterCellElements = tableParameterRowElement.Descendants(XName.Get("Cell"));

                    foreach (var tableParameterCellElement in tableParameterCellElements)
                    {
                        Guid tableParameterColumnId = Guid.Parse(tableParameterCellElement.Attribute(XName.Get("HeaderColumnId")).Value);

                        var tableParameterCell = new TableParameterCell()
                        {
                            Id = Guid.Parse(tableParameterCellElement.Attribute(XName.Get("ObjectId")).Value),
                            TableParameterColumnId = tableParameterColumnId,
                            TableParameterRowId = tableParameterRow.Id,
                            Value = tableParameterCellElement.Attribute(XName.Get("Value")).Value,
                        };

                        tableParameterRepository.AddCell(tableParameterCell);
                    }
                }
            }
        }

        private static void importStepDefinitions()
        {
            string fileToImport = Path.Combine(baseDataPath, @"stepdefinitions-report.xml");

            var xsiNamespace = @"http://www.w3.org/2001/XMLSchema-instance";

            var xDoc = XDocument.Load(fileToImport);

            var dbContext = new BDDMgmtDbContext();

            using (var unityOfWork = new UnitOfWork(null, dbContext))
            {
                var sepDefinitionRepository = new StepDefinitionRepository(unityOfWork);
                var sepDefinitionTypeRepository = new StepDefinitionTypeRepository(unityOfWork);

                var stepDefinitionElements = xDoc.Descendants(XName.Get("StepDefinition"));

                foreach (var stepDefinitionElement in stepDefinitionElements)
                {
                    var stepDefinitionEntity = new StepDefinition()
                    {
                        Id = Guid.Parse(stepDefinitionElement.Attribute(XName.Get("ObjectId")).Value),
                        MethodName = stepDefinitionElement.Attribute(XName.Get("StepDefinitionMethodName")).Value,
                        MethodSignature = stepDefinitionElement.Attribute(XName.Get("StepDefinitionMethodSignature")).Value,
                        CountUsages = Int32.Parse(stepDefinitionElement.Attribute(XName.Get("CountUsages")).Value),
                    };

                    sepDefinitionRepository.Add(stepDefinitionEntity);

                    var stepDefinitionTypeElements = stepDefinitionElement.Descendants(XName.Get("StepDefinitionType"));

                    foreach (var stepDefinitionTypeElement in stepDefinitionTypeElements)
                    {
                        var stepDefinitionTypeEntity = new StepDefinitionType()
                        {
                            Id = Guid.Parse(stepDefinitionTypeElement.Attribute(XName.Get("ObjectId")).Value),
                            Type = stepDefinitionTypeElement.Attribute(XName.Get("type", xsiNamespace)).Value,
                            RegexExpression = stepDefinitionTypeElement.Attribute(XName.Get("RegexExpression")).Value,
                            CountUsages = Int32.Parse(stepDefinitionTypeElement.Attribute(XName.Get("CountUsages")).Value),
                            StepDefinitionId = stepDefinitionEntity.Id,
                        };

                        sepDefinitionTypeRepository.Add(stepDefinitionTypeEntity);
                    }
                }

                unityOfWork.Commit();
            }
        }
    }

    public class RepositoryStorage
    {
        public FeatureRepository FeatureRepository { get; set; }
        public ScenarioRepository ScenarioRepository { get; set; }
        public StepRepository StepRepository { get; set; }
        public TableParameterRepository TableParameterRepository { get; set; }
    }

    public class XmlReadParserUtil
    {
    }
}
