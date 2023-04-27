using Core.Management.Service;
using Microsoft.AspNetCore.Mvc;

namespace Core.Management.DynamicKey.Service.Controllers;

[ApiController]
[Route("api/v1/[controller]")]
public class DummyController : ControllerBase
{   
    private static readonly List<Dummy> DumiesList = new List<Dummy>{
        new Dummy(Guid.NewGuid(), "Dummy", "Description Dummy"),
        new Dummy(Guid.NewGuid(), "Dummy", "Description Dummy"),
        new Dummy(Guid.NewGuid(), "Dummy", "Description Dummy")
    };

    private readonly ILogger<DummyController> _logger;
    private static  int requestCounter = 0;

    public DummyController(ILogger<DummyController> logger)
    {
        _logger = logger;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Dummy>>> Get()
    {
        requestCounter++;
        Console.WriteLine($"Request {requestCounter} Starting ...");

        if(requestCounter<=2)
        {
            Console.WriteLine($"Request {requestCounter} Delaying ...");
            await Task.Delay(TimeSpan.FromSeconds(10));
        }

        if(requestCounter<=4)
        {
            Console.WriteLine($"Request {requestCounter} Error (500) ...");
            return StatusCode(500);
        }

         Console.WriteLine($"Request {requestCounter} Success (Ok) ...");

        requestCounter = 0;
        return Ok(DumiesList);
    }
}
