﻿using Bohrium.Tools.BDDManagementTool.Data.Entities;
using Bohrium.Tools.BDDManagementTool.Data.XML.Entities;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Caching;
using System.Xml.Linq;

namespace Bohrium.Tools.BDDManagementTool.Data.XML
{
    /// <summary>
    /// Xml Data Context to provide data from XML files.
    /// </summary>
    public class XmlDataContext
    {
        #region [Properties]

        public virtual IQueryable<IFeatureEntity> Features { get; private set; }

        public virtual IQueryable<IScenarioEntity> Scenarios { get; private set; }

        public virtual IQueryable<IStatementEntity> Statements { get; private set; }

        public virtual IQueryable<IStepDefinitionEntity> StepDefinitions { get; private set; }

        public virtual IQueryable<IStepDefinitionTypeEntity> StepDefinitionTypes { get; private set; }

        public virtual IQueryable<ITableParameterEntity> Tables { get; private set; }

        public virtual IQueryable<ITableColumnEntity> TableColumns { get; private set; }

        public virtual IQueryable<ITableRowEntity> TableRows { get; private set; }

        public virtual IQueryable<ITableCellEntity> TableCells { get; private set; }

        #endregion [Properties]

        #region [.ctor]

        public XmlDataContext(string filePath)
        {
            if (string.IsNullOrEmpty(filePath))
                throw new ArgumentException("Invalid file path.", "filePath");

            if (!Directory.Exists(filePath))
                throw new DirectoryNotFoundException(string.Format("The '{0}' was not found.", filePath));

            FilePath = filePath;

            Init();
        }

        #endregion [.ctor]

        #region [Methods]

        private IEnumerable<CachedContentFile> InitCache()
        {
            ObjectCache cache = MemoryCache.Default;

            var xmlFiles = cache[KEY] as List<CachedContentFile>;

            if (xmlFiles == null)
            {
                lock (_sync)
                {
                    CacheItemPolicy policy = new CacheItemPolicy();

                    xmlFiles = Directory.GetFiles(FilePath, "*.xml")
                        .Select(filePath => new CachedContentFile()
                        {
                            Path = filePath,
                            Type = EFileType.Xml,
                            Content = File.ReadAllText(filePath)
                        }).ToList();

                    if (!xmlFiles.Any())
                    {
                        throw new SpecFlowDataNotFoundException();
                    }

                    policy.ChangeMonitors.Add(new HostFileChangeMonitor(xmlFiles.Select(x => x.Path).ToList()));

                    cache.Set(KEY, xmlFiles, policy);
                }
            }

            return xmlFiles;
        }

        private IEnumerable<CachedContentFile> CachedFiles
        {
            get { return InitCache(); }
        }

        private void FetchObjects()
        {
            var tempFeaturesEntities = new List<IFeatureEntity>();
            var tempScenariosEntities = new List<IScenarioEntity>();
            var tempStatementEntities = new List<IStatementEntity>();
            var tempStepDefinitionEntities = new List<IStepDefinitionEntity>();
            var tempStepDefinitionTypesEntities = new List<IStepDefinitionTypeEntity>();
            var tempTableEntities = new List<ITableParameterEntity>();
            var tempTableColumnEntities = new List<ITableColumnEntity>();
            var tempTableRowEntities = new List<ITableRowEntity>();
            var tempTableCellEntities = new List<ITableCellEntity>();

            foreach (var item in CachedFiles.AsParallel())
            {
                var xDocument = XDocument.Parse(item.Content);

                if (xDocument.Root != null)
                {
                    const string xsi = "http://www.w3.org/2001/XMLSchema-instance";

                    #region [Features]

                    if (xDocument.Root.Elements("Features").Any())
                    {
                        var featuresQuery = from feature in xDocument.Root.Descendants("Feature")
                                            select new FeatureEntity()
                                            {
                                                ObjectId = Guid.Parse(feature.Attribute("ObjectId").Value),
                                                Description = feature.Attribute("Description").Value,
                                                Tags = (from tag in feature.Elements("Tags").Elements("Tag")
                                                        select tag.Value).ToArray()
                                            };

                        tempFeaturesEntities.AddRange(featuresQuery.ToList());

                        foreach (var backgroundNode in xDocument.Root.Descendants("Feature").Descendants("Background").AsParallel())
                        {
                            var parentId = Guid.Parse(backgroundNode.Attribute("ParentFeature").Value);

                            foreach (var statementNode in backgroundNode.Descendants("Statements").Elements("Statement"))
                            {
                                var statementEntity = new StatementEntity();
                                statementEntity.ObjectId = Guid.Parse(statementNode.Attribute("ObjectId").Value);
                                statementEntity.Description = statementNode.Attribute("Statement").Value;
                                statementEntity.Keyword = statementNode.Attribute("Keyword").Value;
                                statementEntity.Type = statementNode.Attribute(XName.Get("type", xsi)).Value;
                                statementEntity.FeatureId = parentId;

                                var multilineTextParameterNode = statementNode.Element("MultilineTextParameter");
                                if (multilineTextParameterNode != null)
                                    statementEntity.MultilineTextParameter = multilineTextParameterNode.Value;

                                var stepDefinitionReferenceNode = statementNode.Element("StepDefinitionReference");
                                if (stepDefinitionReferenceNode != null)
                                {
                                    statementEntity.StepDefinitionId = Guid.Parse(stepDefinitionReferenceNode.Attribute("StepDefinitionId").Value);

                                    foreach (var typeReferenceNode in stepDefinitionReferenceNode.Elements("TypeReferences"))
                                    {
                                        foreach (var stepDefinitionTypeReferenceNode in typeReferenceNode.Descendants("StepDefinitionTypeReference"))
                                        {
                                            var stepDefinitionTypeEntity = new StepDefinitionTypeEntity();
                                            stepDefinitionTypeEntity.ObjectId = Guid.Parse(stepDefinitionTypeReferenceNode.Attribute("StepDefinitionTypeId").Value);

                                            statementEntity.StepDefinitionTypes.Add(stepDefinitionTypeEntity);
                                        }
                                    }
                                }

                                foreach (var tableParameterNode in statementNode.Elements("TableParameter"))
                                {
                                    var tableEntity = new TableParameterEntity();
                                    tableEntity.ObjectId = Guid.Parse(tableParameterNode.Attribute("ObjectId").Value);
                                    tableEntity.StatementId = statementEntity.ObjectId;

                                    var headerNode = tableParameterNode.Element("Header");
                                    if (headerNode != null)
                                    {
                                        foreach (var columnNode in headerNode.Element("Columns").Elements("Column"))
                                        {
                                            var columnEntity = new TableColumnEntity();
                                            columnEntity.ObjectId = Guid.Parse(columnNode.Attribute("ObjectId").Value);
                                            columnEntity.Value = columnNode.Attribute("Value").Value;

                                            tableEntity.Columns.Add(columnEntity);

                                            tempTableColumnEntities.Add(columnEntity);
                                        }
                                    }

                                    var rowsNode = tableParameterNode.Element("Rows");
                                    if (rowsNode != null)
                                    {
                                        foreach (var rowNode in rowsNode.Elements("Row"))
                                        {
                                            var rowEntity = new TableRowEntity();

                                            foreach (var cellNode in rowNode.Elements("Cells").Elements("Cell"))
                                            {
                                                var cellEntity = new TableCellEntity();
                                                cellEntity.ObjectId = Guid.Parse(cellNode.Attribute("ObjectId").Value);
                                                cellEntity.ColumnId = Guid.Parse(cellNode.Attribute("HeaderColumnId").Value);
                                                cellEntity.Value = cellNode.Attribute("Value").Value;

                                                tempTableCellEntities.Add(cellEntity);

                                                rowEntity.Cells.Add(cellEntity);
                                            }

                                            tempTableRowEntities.Add(rowEntity);

                                            tableEntity.Rows.Add(rowEntity);
                                        }
                                    }

                                    tempTableEntities.Add(tableEntity);

                                    statementEntity.TableParameters.Add(tableEntity);
                                }

                                tempStatementEntities.Add(statementEntity);
                            }
                        }
                    }

                    #endregion [Features]

                    #region [Scenarios/Statements/Tables]

                    if (xDocument.Root.Elements("Scenarios").Any())
                    {
                        // loop between scenarios
                        foreach (var scenarioNode in xDocument.Root.Descendants("Scenario").AsParallel())
                        {
                            var scenarioEntity = new ScenarioEntity();
                            scenarioEntity.ObjectId = Guid.Parse(scenarioNode.Attribute("ObjectId").Value);
                            scenarioEntity.FeatureObjectId = Guid.Parse(scenarioNode.Attribute("ParentFeature").Value);
                            scenarioEntity.Description = scenarioNode.Attribute("Description").Value;
                            scenarioEntity.Tags = scenarioNode.Elements("Tags").Elements("Tag").Select(tag => tag.Value).ToArray();

                            // loop between statements
                            foreach (var statementNode in scenarioNode.Elements("Statements").Elements("Statement"))
                            {
                                var statementEntity = new StatementEntity();
                                statementEntity.ObjectId = Guid.Parse(statementNode.Attribute("ObjectId").Value);
                                statementEntity.Description = statementNode.Attribute("Statement").Value;
                                statementEntity.Keyword = statementNode.Attribute("Keyword").Value;
                                statementEntity.Type = statementNode.Attribute(XName.Get("type", xsi)).Value;
                                statementEntity.ScenarioId = scenarioEntity.ObjectId;

                                var multilineTextParameterNode = statementNode.Element("MultilineTextParameter");
                                if (multilineTextParameterNode != null)
                                    statementEntity.MultilineTextParameter = multilineTextParameterNode.Value;

                                var stepDefinitionReferenceNode = statementNode.Element("StepDefinitionReference");
                                if (stepDefinitionReferenceNode != null)
                                {
                                    statementEntity.StepDefinitionId = Guid.Parse(stepDefinitionReferenceNode.Attribute("StepDefinitionId").Value);

                                    foreach (var typeReferenceNode in stepDefinitionReferenceNode.Elements("TypeReferences"))
                                    {
                                        foreach (var stepDefinitionTypeReferenceNode in typeReferenceNode.Descendants("StepDefinitionTypeReference"))
                                        {
                                            var stepDefinitionTypeEntity = new StepDefinitionTypeEntity();
                                            stepDefinitionTypeEntity.ObjectId = Guid.Parse(stepDefinitionTypeReferenceNode.Attribute("StepDefinitionTypeId").Value);

                                            statementEntity.StepDefinitionTypes.Add(stepDefinitionTypeEntity);
                                        }
                                    }
                                }

                                foreach (var tableParameterNode in statementNode.Elements("TableParameter"))
                                {
                                    var tableEntity = new TableParameterEntity();
                                    tableEntity.ObjectId = Guid.Parse(tableParameterNode.Attribute("ObjectId").Value);
                                    tableEntity.StatementId = statementEntity.ObjectId;

                                    var headerNode = tableParameterNode.Element("Header");
                                    if (headerNode != null)
                                    {
                                        foreach (var columnNode in headerNode.Element("Columns").Elements("Column"))
                                        {
                                            var columnEntity = new TableColumnEntity();
                                            columnEntity.ObjectId = Guid.Parse(columnNode.Attribute("ObjectId").Value);
                                            columnEntity.Value = columnNode.Attribute("Value").Value;

                                            tableEntity.Columns.Add(columnEntity);

                                            tempTableColumnEntities.Add(columnEntity);
                                        }
                                    }

                                    var rowsNode = tableParameterNode.Element("Rows");
                                    if (rowsNode != null)
                                    {
                                        foreach (var rowNode in rowsNode.Elements("Row"))
                                        {
                                            var rowEntity = new TableRowEntity();

                                            foreach (var cellNode in rowNode.Elements("Cells").Elements("Cell"))
                                            {
                                                var cellEntity = new TableCellEntity();
                                                cellEntity.ObjectId = Guid.Parse(cellNode.Attribute("ObjectId").Value);
                                                cellEntity.ColumnId = Guid.Parse(cellNode.Attribute("HeaderColumnId").Value);
                                                cellEntity.Value = cellNode.Attribute("Value").Value;

                                                tempTableCellEntities.Add(cellEntity);

                                                rowEntity.Cells.Add(cellEntity);
                                            }

                                            tempTableRowEntities.Add(rowEntity);

                                            tableEntity.Rows.Add(rowEntity);
                                        }
                                    }

                                    tempTableEntities.Add(tableEntity);

                                    statementEntity.TableParameters.Add(tableEntity);
                                }

                                tempStatementEntities.Add(statementEntity);
                            }

                            tempScenariosEntities.Add(scenarioEntity);
                        }
                    }

                    #endregion [Scenarios/Statements/Tables]

                    #region [StepDefinition]

                    if (xDocument.Root.Elements("StepDefinitions").Any())
                    {
                        foreach (var stepDefinitionsNode in xDocument.Root.Descendants("StepDefinitions").AsParallel())
                        {
                            foreach (var stepDefinitionNode in stepDefinitionsNode.Elements("StepDefinition").AsParallel())
                            {
                                var stepDefinitionEntity = new StepDefinitionEntity();
                                stepDefinitionEntity.ObjectId = Guid.Parse(stepDefinitionNode.Attribute("ObjectId").Value);
                                stepDefinitionEntity.MethodName = stepDefinitionNode.Attribute("StepDefinitionMethodName").Value;
                                stepDefinitionEntity.MethodSignature = stepDefinitionNode.Attribute("StepDefinitionMethodSignature").Value;
                                stepDefinitionEntity.CountUsages = int.Parse(stepDefinitionNode.Attribute("CountUsages").Value);

                                foreach (var stepDefinitionTypesNode in stepDefinitionNode.Elements("StepDefinitionTypes"))
                                {
                                    foreach (var stepDefinitionTypeNode in stepDefinitionTypesNode.Elements("StepDefinitionType"))
                                    {
                                        var stepDefinitionTypeEntity = new StepDefinitionTypeEntity();

                                        stepDefinitionTypeEntity.ObjectId = Guid.Parse(stepDefinitionTypeNode.Attribute("ObjectId").Value);
                                        stepDefinitionTypeEntity.StepDefinitionId = Guid.Parse(stepDefinitionTypeNode.Attribute("ParentStepDefinitionId").Value);
                                        stepDefinitionTypeEntity.RegexStatement = stepDefinitionTypeNode.Attribute("RegexExpression").Value;
                                        stepDefinitionTypeEntity.CountUsages = int.Parse(stepDefinitionTypeNode.Attribute("CountUsages").Value);
                                        stepDefinitionTypeEntity.Type = stepDefinitionTypeNode.Attribute(XName.Get("type", xsi)).Value;

                                        stepDefinitionEntity.StepDefinitionTypes.Add(stepDefinitionTypeEntity);

                                        tempStepDefinitionTypesEntities.Add(stepDefinitionTypeEntity);
                                    }
                                }

                                tempStepDefinitionEntities.Add(stepDefinitionEntity);
                            }
                        }
                    }

                    #endregion [StepDefinition]
                }
            }

            #region [Join types for navigation]

            foreach (var statementEntity in tempStatementEntities.AsParallel())
            {
                var stepDefinitionTypeIds = statementEntity.StepDefinitionTypes.Select(x => x.ObjectId);
                statementEntity.StepDefinitionTypes = tempStepDefinitionTypesEntities.Where(x => stepDefinitionTypeIds.Contains(x.ObjectId)).ToList();

                statementEntity.Scenario = tempScenariosEntities.FirstOrDefault(x => x.ObjectId == statementEntity.ScenarioId);
                statementEntity.Feature = tempFeaturesEntities.FirstOrDefault(x => x.ObjectId == statementEntity.ScenarioId);
                statementEntity.StepDefinition = tempStepDefinitionEntities.FirstOrDefault(x => x.ObjectId == statementEntity.StepDefinitionId);

                var feature = tempFeaturesEntities.FirstOrDefault(x => x.ObjectId == statementEntity.FeatureId);
                if (feature != null)
                {
                    statementEntity.Feature = feature;
                    feature.Background = tempStatementEntities.Where(x => x.FeatureId == feature.ObjectId).ToList();
                }
            }

            foreach (var tableParameterEntity in tempTableEntities.AsParallel())
                tableParameterEntity.Statement = tempStatementEntities.FirstOrDefault(x => x.ObjectId == tableParameterEntity.StatementId);

            foreach (var scenarioEntity in tempScenariosEntities.AsParallel())
            {
                scenarioEntity.Statements = tempStatementEntities.Where(x => x.ScenarioId == scenarioEntity.ObjectId).ToList();

                var feature = tempFeaturesEntities.FirstOrDefault(x => x.ObjectId == scenarioEntity.FeatureObjectId);
                if (feature != null)
                {
                    scenarioEntity.Feature = feature;
                    feature.Scenarios = tempScenariosEntities.Where(x => x.FeatureObjectId == feature.ObjectId).ToList();
                }
            }

            foreach (var stepDefinitionTypeEntity in tempStepDefinitionTypesEntities.AsParallel())
                stepDefinitionTypeEntity.StepDefinition = tempStepDefinitionEntities.FirstOrDefault(x => x.ObjectId == stepDefinitionTypeEntity.ObjectId);

            #endregion [Join types for navigation]

            this.Features = tempFeaturesEntities.AsQueryable();
            this.Scenarios = tempScenariosEntities.AsQueryable();
            this.Statements = tempStatementEntities.AsQueryable();

            this.StepDefinitions = tempStepDefinitionEntities.AsQueryable();
            this.StepDefinitionTypes = tempStepDefinitionTypesEntities.AsQueryable();

            this.Tables = tempTableEntities.AsQueryable();
            this.TableColumns = tempTableColumnEntities.AsQueryable();
            this.TableRows = tempTableRowEntities.AsQueryable();
            this.TableCells = tempTableCellEntities.AsQueryable();
        }

        private void Init()
        {
            InitCache();

            FetchObjects();
        }

        #endregion [Methods]

        private string FilePath { get; set; }

        private const string KEY = "_XmlDataContextKey";

        internal static object _sync = new object();
    }

    internal class CachedContentFile
    {
        public string Path { get; set; }

        public EFileType Type { get; set; }

        public string Content { get; set; }
    }

    internal enum EFileType
    {
        Xml = 1,

        Json = 2
    }
}