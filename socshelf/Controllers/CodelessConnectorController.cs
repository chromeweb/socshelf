using Microsoft.AspNetCore.Mvc;
using socshelf.Models;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace socshelf.Controllers
{
    public class CodelessConnectorController : Controller
    {
        public IActionResult Index()
        {
            var model = new connectorUiConfig
            {
                sampleQueries = new List<connectorUiConfig.SampleQueries> { new connectorUiConfig.SampleQueries() },
                graphQueries = new List<connectorUiConfig.GraphQueries> { new connectorUiConfig.GraphQueries() },
                dataTypes = new List<connectorUiConfig.DataTypes> { new connectorUiConfig.DataTypes() },
                connectivityCriteria = new List<connectorUiConfig.ConnectivityCriteria> { new connectorUiConfig.ConnectivityCriteria()},
                permissions = new List<connectorUiConfig.Permissions> { new connectorUiConfig.Permissions() },
                instructionSteps = new List<connectorUiConfig.InstructionSteps> { new connectorUiConfig.InstructionSteps() }
                
            };
            //Since this is a codeless connector, we are setting the connectivity criteria to HasDataConnectors by default
            model.connectivityCriteria[0].type = connectorUiConfig.ConnectivityCriteriaType.HasDataConnectors;
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
