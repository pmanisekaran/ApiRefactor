using ApiRefactor.Models;
using ApiRefactor.Services;
using Microsoft.AspNetCore.Mvc;

namespace ApiRefactor.Controllers;

[ApiController]
[Route("api/[controller]")]
public class WaveController : ControllerBase
{

	private readonly IWaveService _waveService;
	public WaveController(IWaveService waveService)
		=> _waveService = waveService;


	[HttpGet]
	public List<Wave> Get()
		=> _waveService.GetWaves();


	[HttpGet("{id}")]
	public Wave Get(Guid id)
		=> _waveService.GetWave(id);


	[HttpPost]
	public Wave Post(Wave wave)
		=> _waveService.AddWave(wave);
}
