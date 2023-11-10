using ApiRefactor.Persistence;
using ApiRefactor.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

public class Program
{
	public static void Main(string[] args)
	{

		var builder = WebApplication.CreateBuilder(args);

		ConfigurationManager configuration = builder.Configuration; // allows both to access and to set up the config
	
		// Add services to the container.
		ConfigureServices(builder.Services, configuration);
		builder.Services.AddControllers();
		// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
		builder.Services.AddEndpointsApiExplorer();
		builder.Services.AddSwaggerGen();


		var app = builder.Build();

		// Configure the HTTP request pipeline.
		if (app.Environment.IsDevelopment())
		{
			app.UseSwagger();
			app.UseSwaggerUI();
		}


		app.UseHttpsRedirection();

		app.UseAuthorization();

		app.MapControllers();

		app.Run();


	}

	private static void ConfigureServices(IServiceCollection services, ConfigurationManager configuration)
	{
		// Register your services here
		
		services.AddDbContext<WavesDbContext>(x =>
			x.UseSqlite(configuration.GetConnectionString("WavesDbConnectionString")));

		services.AddTransient<IWaveService, WaveService>();
		services.AddScoped<IWavesDbContext, WavesDbContext>();
	}
}
