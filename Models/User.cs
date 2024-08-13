namespace BookStoreApi.Models
{
	public class User
	{
		public int Id { get; set; }
		public string Username { get; set; }
		public string Password { get; set; } // In a real-world scenario, store hashed passwords
	}
}
