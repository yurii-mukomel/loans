using DAL;

namespace BLL.Models
{
    public class AuthenticateResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }

        public AuthenticateResponse(Person person, string token)
        {
            Id = person.Id;
            FirstName = person.FirstName;
            LastName = person.LastName;
            Username = person.Email;
            Token = token;
        }
    }
}
