using ApiRefactor.Models;
using Microsoft.AspNetCore.Mvc;

namespace ApiRefactor.Services
{
	public interface IWaveService
	{
		Wave AddWave(Wave wave);
		Wave GetWave(Guid id);
		List<Wave> GetWaves();
	}
}