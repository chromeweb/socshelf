using Microsoft.AspNetCore.Mvc;
using socshelf.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace socshelf.Controllers
{
    public class CodelessConnectorController : Controller
    {
        public List<string> TenantTypes { get; set; } = Enum.GetNames(typeof(connectorUiConfig.TenantTypes)).ToList();
        public List<string> Licenses { get; set; } = Enum.GetNames(typeof(connectorUiConfig.Licenses)).ToList();

        public IActionResult Index()
        {
            var model = new connectorUiConfig
            {
                sampleQueries = new List<connectorUiConfig.SampleQueries> { new connectorUiConfig.SampleQueries() },
                graphQueries = new List<connectorUiConfig.GraphQueries> { new connectorUiConfig.GraphQueries() },
                dataTypes = new List<connectorUiConfig.DataTypes> { new connectorUiConfig.DataTypes() },
                connectivityCriteria = new List<connectorUiConfig.ConnectivityCriteria> { new connectorUiConfig.ConnectivityCriteria() },
                permissions = new connectorUiConfig.Permissions(), // { new connectorUiConfig.Permissions() },
                instructionSteps = new List<connectorUiConfig.InstructionSteps> { new connectorUiConfig.InstructionSteps() }
            };
            //Since this is a codeless connector, we are setting the connectivity criteria to HasDataConnectors by default
            //TODO: Add more options for connectivity criteria
            model.connectivityCriteria[0].type = "HasDataConnectors";
            model.connectivityCriteria[0].value = string.Empty;

            
            return View(model);
        }

        [HttpPost]
        public IActionResult Submit(connectorUiConfig model)
        {
            string json = JsonSerializer.Serialize(model);
            ViewBag.Json = json;
            return View();
        }
    }
}
