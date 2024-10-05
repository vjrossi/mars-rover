using Microsoft.AspNetCore.Mvc;
using MarsRover.Core.Models;

namespace MarsRover.Web.Controllers
{
    public class SimulationController : Controller
    {
        private readonly Nasa _nasa;

        public SimulationController()
        {
            _nasa = new Nasa();
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RunSimulation([FromBody] SimulationInput input)
        {
            try
            {
                var gridSetupResult = _nasa.AcceptCommands(new[] { input.GridSize });
                var results = new List<List<string>> { gridSetupResult };

                foreach (var command in input.RoverCommands)
                {
                    var roverCommands = new[] { command.InitialPosition, command.Commands };
                    var result = _nasa.AcceptCommands(roverCommands);
                    results.Add(result);
                }

                return Json(new { success = true, results });
            }
            catch (Exception ex)
            {
                return Json(new { success = false, error = ex.Message, stackTrace = ex.StackTrace });
            }
        }
    }

    public class SimulationInput
    {
        public required string GridSize { get; set; }
        public required List<RoverCommand> RoverCommands { get; set; }
    }

    public class RoverCommand
    {
        public required string InitialPosition { get; set; }
        public required string Commands { get; set; }
    }
}