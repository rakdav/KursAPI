namespace KursAPI.Models
{
    public class Person
    {
        public int Id { get; set; }
        public required string Email { get; set; }
        public required string Password { get; set; }
        public Person(string email, string password)
        {
            Email = email;
            Password = password;
        }
    }
}
