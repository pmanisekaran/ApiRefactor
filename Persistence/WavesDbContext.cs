using Microsoft.EntityFrameworkCore;
using ApiRefactor.Models;
namespace ApiRefactor.Persistence
{
	public class WavesDbContext : DbContext, IWavesDbContext
	{
		private readonly IConfiguration _configuration;
		public DbSet<Wave> Waves { get; set; }


		public WavesDbContext(IConfiguration configuration, DbContextOptions<WavesDbContext> options): base(options)
		{
			_configuration = configuration;
		}
		public void Save()
		{
			this.SaveChanges();
		}

	}
}
