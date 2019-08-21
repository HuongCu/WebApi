using System;

namespace Simple.Api.Model
{
	/// <summary>
	/// customer model share between Controller and Repositories
	/// However, they can be separated to DTO and Entity model then are mappped in Service layer.
	/// </summary>
	public class Customer
	{
		public int CustomerId { get; set; }
		public string FirstName { get; set; }

		public string LastName { get; set; }

		public DateTime DateOfBirth { get; set; }

	}
}
