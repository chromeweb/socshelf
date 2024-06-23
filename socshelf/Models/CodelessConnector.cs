// ***********************************************************************
// Assembly         : socshelf
// Author           : MattEgen
// Created          : 06-04-2024
//
// Last Modified By : MattEgen
// Last Modified On : 06-05-2024
// ***********************************************************************
// <copyright file="CodelessConnector.cs" company="socshelf">
//     Copyright (c) . All rights reserved.
// </copyright>
// <summary></summary>
// ***********************************************************************
using System.Diagnostics;
using System.Security.Policy;
using static socshelf.Models.connectorUiConfig.ResourceProviders;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace socshelf.Models
{
    /// <summary>Class CodelessConnector.</summary>
    public class CodelessConnector
    {
        /// <summary>Initializes a new instance of the <see cref="T:socshelf.Models.CodelessConnector" /> class.</summary>
        public CodelessConnector()
        {
            _connectorUiConfig = new connectorUiConfig();
            _connectorUiConfig.sampleQueries = new List<connectorUiConfig.SampleQueries> { new connectorUiConfig.SampleQueries() };
        }
        //public CodelessConnector(string title, string publisher, string description)
        //{
        //    _connectorUiConfig = new connectorUiConfig(title,publisher,description);
        //}
        private connectorUiConfig _connectorUiConfig;
    }

    /// <summary>Defines the UX for the connector</summary>
    public class connectorUiConfig
    {
        #region Properties
        /// <summary>Gets or sets the title or name of the connector</summary>
        /// <value>The title or name of the connector</value>
        public string? title { get; set; }


        /// <summary>Sets custom connector ID for internal usage</summary>
        /// <value>The identifier (normally a GUID)</value>
        public string? id { get; set; }

        /// <summary>Path to image file in SVG format. If no value is configured, a default logo is used.</summary>
        /// <value>The logo</value>
        public string? logo { get; set; }

        /// <summary>Gets or sets the publisher of this connector</summary>
        /// <value>The publisher name</value>
        public string? publisher { get; set; }

        /// <summary>Gets or sets the description of the connector.  Note:  support markdown.</summary>
        /// <value>The description markdown.</value>
        public string? descriptionMarkdown { get; set; }

        /// <summary>Queries for the customer to understand how to find the data in the event log</summary>
        /// <value>The sample queries</value>
        public List<SampleQueries> sampleQueries { get; set; }

        /// <summary>Sets the name of the table the connector inserts data to. This name can be used in other queries by specifying {{graphQueriesTableName}} placeholder in graphQueries and lastDataReceivedQuery values.</summary>
        /// <value>The name of the graph queries table.</value>
        public string? graphQueriesTableName { get; set; }

        /// <summary>Queries that present data ingestion over the last two weeks.  Provide either one query for all of the data connector's data types, or a different query for each data type</summary>
        /// <value>The graph queries</value>
        public List<GraphQueries> graphQueries { get; set; }

        /// <summary>An object that defines how to verify if the connector is connected.</summary>
        /// <value>The data types.</value>
        public List<DataTypes>? dataTypes { get; set; }

        public List<ConnectivityCriteria>? connectivityCriteria { get; set; }

        /// <summary>The information displayed under the Prerequisites section of the UI, which lists the permissions required to enable or disable the connector</summary>
        /// <value>The permissions.</value>
        public List<Permissions>? permissions { get; set; }

        /// <summary>An array of widget parts that explain how to install the connector, and actionable controls displayed on the Instructions tab.</summary>
        /// <value>The instruction steps.</value>
        public List<InstructionSteps>? instructionSteps { get; set; }

        #endregion

        #region Enums
        /// <summary>Tenant Role Types</summary>
        public enum TenantTypes
        {
            GlobalAdmin,
            SecurityAdmin,
            SecurityReader,
            InformationProtection
        }
        /// <summary>Enum ConnectivityCriteriaType</summary>
        /// <remarks>The only valid option for a Codeless Connector according to docs is HasDataConnectors</remarks>
        public enum ConnectivityCriteriaType
        {
            HasDataConnectors,
            isConnectedQuery
        }

        /// <summary>Required Licenses</summary>
        public enum Licenses
        {
            OfficeIRM,
            OfficeATP,
            Office365,
            AadP1P2,
            Mcas,
            Aatp,
            Mdatp,
            Mtp,
            IoT
        }

        public enum ResourceProvidersScope
        {
            Subscription,
            ResourceGroup,
            Resource
        }

        #endregion

        #region Constructors
        /// <summary>Initializes a new instance of the <see cref="T:socshelf.Models.connectorUiConfig" /> class.</summary>
        public connectorUiConfig()
        {
            this.title = "My Codeless Connector";
            this.id = Guid.NewGuid().ToString().ToLower(); // generate a new GUID and cast it to lowercase
            this.publisher = "Publisher Name";
            this.descriptionMarkdown = "This is a description of the connector and it supports ** markdown **";
            this.sampleQueries = new List<SampleQueries>();
            this.graphQueries = new List<GraphQueries>();   

        }

        #endregion

        /// <summary>Class SampleQueries.</summary>
        public class SampleQueries
        {
            public SampleQueries()
            {
                this.description = "Get all entries in table";
                this.query = "{{graphQueriesTableName}}"; // use the placeholder for the table name (this way it's at least valid)
            }
            /// <summary>Gets or sets the description of the query</summary>
            /// <value>The description</value>
            public string? description { get; set; }

            /// <summary>Gets or sets the query</summary>
            /// <value>The query</value>
            public string? query { get; set; }
        }

        /// <summary>Queries that present data ingestion over the last two weeks. Provide either one query for all of the data connector's data types, or a different query for each data type.</summary>
        public class GraphQueries
        {
            public GraphQueries()
            {
                this.metricName = "Total records received";
                this.legend = "Record Count";
                this.baseQuery = "{{graphQueriesTableName}}| count";
            }
            public string metricName { get; set; }
            public string legend { get; set; }
            public string baseQuery { get; set; }
        }

        /// <summary>A list of all data types for your connector, and a query to fetch the time of the last event for each data type</summary>
        public class DataTypes
        {
            public DataTypes()
            {
                this.name = "{{graphQueriesTableName}}";
                this.lastDataReceivedQuery = "{{graphQueriesTableName}}\n | summarize Time = max(TimeGenerated)\n | where isnotempty(Time)";
            }
            public string name { get; set; }
            public string lastDataReceivedQuery { get; set; }
        }

        /// <summary>An object that defines how to verify if the connector is connected.</summary>
        public class ConnectivityCriteria
        {
            public ConnectivityCriteriaType type { get; set; }

            public string value { get; set; }
        }

        /// <summary>The information displayed under the Prerequisites section of the UI, which lists the permissions required to enable or disable the connector.</summary>
        public class Permissions
        {
            public Permissions()
            {
                this.customs = new customPermissions();
                this.resourceProviders = new ResourceProviders();
                this.License = Licenses.Office365;
                this.tenant = TenantTypes.GlobalAdmin;
            }
            public customPermissions customs { get; set; }

            public ResourceProviders resourceProviders { get; set; }

            public Licenses License { get; set; }

            public TenantTypes tenant { get; set; }

        }
        public class customPermissions
        {
            public string? permissionName { get; set; }
            public string? permissionDescription { get; set; }
        }

        /// <summary>Class resourceProviders</summary>
        public class ResourceProviders
        {
            //TODO: Figure out how to make this an enum
            public enum Provider
            {
                Microsoft_OperationalInsights_workspaces,
                Microsoft_OperationalInsights_solutions,
                Microsoft_OperationalInsights_workspaces_datasources,
                microsoft_aadiam_diagnosticSettings,
                Microsoft_OperationalInsights_workspaces_sharedKeys,
                Microsoft_Authorization_policyAssignments,
            }

            static Provider provider = Provider.Microsoft_OperationalInsights_workspaces;
            string originalString = provider.ToOriginalString();


            public string providerDisplayName { get; set; }
            public string permissionsDisplayText { get; set; }
            public enum requiredPermissions
            {

            }

            public ResourceProvidersScope scope { get; set; }
        }

        public class InstructionSteps
        {
            public InstructionSteps()
            {
                this.title = "Instruction Step Title";
                this.description = "Instruction Step Description";
                this.instructions = new List<Instructions> { new Instructions() };
                this.innerSteps = new List<InstructionSteps>();
            }
            public string title { get; set; }
            public string description { get; set; }

            public List<Instructions> instructions { get; set; }

            public List<InstructionSteps>? innerSteps { get; set; }
        }

        public class Instructions
        {
            public Instructions()
            {
                this.type = "type";
                this.parameters = new List<string> { "parameters" };
            }
            public string type { get; set; }
            public List<string> parameters { get; set; }

        }
    }
    public static class ProviderExtensions
    {
        private static Dictionary<Provider, string> _map = new Dictionary<Provider, string>
    {
        { Provider.Microsoft_OperationalInsights_workspaces, "Microsoft.OperationalInsights/workspaces" },
        { Provider.Microsoft_OperationalInsights_solutions, "Microsoft.OperationalInsights/solutions" },
        { Provider.Microsoft_OperationalInsights_workspaces_datasources, "Microsoft.OperationalInsights/workspaces/datasources" },
        { Provider.microsoft_aadiam_diagnosticSettings, "microsoft.aadiam/diagnosticSettings" },
        { Provider.Microsoft_OperationalInsights_workspaces_sharedKeys, "Microsoft.OperationalInsights/workspaces/sharedKeys" },
        { Provider.Microsoft_Authorization_policyAssignments, "Microsoft.Authorization/policyAssignments" },
    };

        public static string ToOriginalString(this Provider provider)
        {
            return _map[provider];
        }
    }

}
