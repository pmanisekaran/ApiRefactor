using ApiRefactor.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiRefactor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WaveController : ControllerBase
{
    [HttpGet]
    public Waves Get()
    {
        return new Waves();
    }

    [HttpGet("{id}")]
    public Wave Get(Guid id)
    {
        return new Wave(id);
    }

    [HttpPost]
    public void Post(Wave wave)
    {
        wave.Save();
    }
}
