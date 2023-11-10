using ApiRefactor.Models;
using ApiRefactor.Persistence;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace ApiRefactor.Services
{
	public class WaveService : IWaveService
	{
		WavesDbContext _wavesDbContext;

		public WaveService(WavesDbContext wavesDbContext)
		{
			_wavesDbContext = wavesDbContext;
		}


		public List<Wave> GetWaves()
		{
			return _wavesDbContext.Waves.ToList();
		}

		public Wave GetWave(Guid id)
		{

			// i am not if SQLLite has Guid datatype. So comparing by string
			var wave = _wavesDbContext.Waves.FirstOrDefault(x => x.Id.ToString().ToLower() == id.ToString().ToLower());
			return wave;

		}

		public Wave AddWave(Wave wave)
		{
			_wavesDbContext.Waves.Add(wave);
			_wavesDbContext.Save();
			return GetWave(wave.Id);

		}

	}
}
