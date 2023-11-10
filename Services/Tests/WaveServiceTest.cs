using ApiRefactor.Models;
using ApiRefactor.Persistence;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using NSubstitute;
using Xunit;

namespace ApiRefactor.Services.Tests
{
	public class WaveServiceTests
	{
		[Fact]
		public void GetWaves_ShouldReturnListOfWaves()
		{
			// Arrange
			var dbContext = Substitute.For<WavesDbContext>();
			var waveService = new WaveService(dbContext);

			// mock a DbSet
			var mockDbSet = Substitute.For<DbSet<Wave>, IQueryable<Wave>>();
			var data = new List<Wave>()
						{
							new Wave
							{
								Id= Guid.NewGuid(),
								Name="Wave1"
							},
							new Wave
							{
								Id = Guid.NewGuid(),
								Name = "Wave2"
							}
			}.AsQueryable();

			((IQueryable<Wave>)mockDbSet).Provider.Returns(data.Provider);
			((IQueryable<Wave>)mockDbSet).Expression.Returns(data.Expression);
			((IQueryable<Wave>)mockDbSet).ElementType.Returns(data.ElementType);
			((IQueryable<Wave>)mockDbSet).GetEnumerator().Returns(data.GetEnumerator());
			// now add it to a mock DbContext
			var mockContext = Substitute.For<WavesDbContext>();
			mockContext.Set<Wave>().Returns(mockDbSet);


			// Set the Waves property of DbContext to the substitute DbSet
			dbContext.Waves.Returns(mockDbSet);

			// Act
			var result = waveService.GetWaves();

			// Assert
			result.Should().NotBeNull();
			result.Should().HaveCount(2);
		}

		[Fact]
		public void GetWave_ShouldReturnCorrectWave()
		{
			// Arrange
			var dbContext = Substitute.For<WavesDbContext>();
			var waveService = new WaveService(dbContext);

			var waveId = Guid.NewGuid();
			var expectedWave = new Wave { Id = waveId, Name = "TestWave" };

			// Mock the WavesDbContext behavior
			dbContext.Waves.FirstOrDefault(Arg.Any<Func<Wave, bool>>()).Returns(expectedWave);

			// Act
			var result = waveService.GetWave(waveId);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeEquivalentTo(expectedWave);
		}

		[Fact]
		public void AddWave_ShouldAddWaveAndReturnIt()
		{
			// Arrange
			var dbContext = Substitute.For<WavesDbContext>();
			var waveService = new WaveService(dbContext);

			var newWave = new Wave { Id = Guid.NewGuid(), Name = "NewWave" };

			// Act
			var result = waveService.AddWave(newWave);

			// Assert
			result.Should().NotBeNull();
			result.Should().BeEquivalentTo(newWave);

			// Ensure that Add and SaveChangesAsync were called
			dbContext.Waves.Received(1).Add(newWave);
			dbContext.Received(1).Save();
		}
	}
}
