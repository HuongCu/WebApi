using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Simple.Api.Model
{
	/// <summary>
	/// customer model share between Controller and Repositories
	/// However, they can be separated to DTO and Entity model then are mappped in Service layer.
	/// </summary>
	public class Customer
	{
		[DatabaseGenerated(DatabaseGeneratedOption.Identity)]
		[Key, Column(Order = 0)]
		public int CustomerId { get; set; }
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime DateOfBirth { get; set; }

	}
}
