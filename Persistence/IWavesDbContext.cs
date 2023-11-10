using ApiRefactor.Models;
using Microsoft.EntityFrameworkCore;

namespace ApiRefactor.Persistence
{
	public interface IWavesDbContext
	{
		DbSet<Wave> Waves { get; set; }
		void Save();
	}
}