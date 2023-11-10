using System.ComponentModel.DataAnnotations;

namespace ApiRefactor.Models
{
	public class Wave
	{
		[Key]
		public Guid Id { get; set; }

		[Required]
		public string Name { get; set; }

		public DateTime WaveDate { get; set; }
	}
}
