using AutoMapper;
using BLL.DTO;
using BLL.Helpers;
using BLL.Interfaces;
using BLL.Models;
using DAL;
using DAL.Interfaces;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly IConfiguration _configuration;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper, IConfiguration configuration)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _configuration = configuration;
        }

        public async Task<AuthenticateRequest> GetUserDetails(int id)
        {
            var person = await _unitOfWork.People.GetAsync(id);
            return _mapper.Map<Person, AuthenticateRequest>(person);
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest authenticateRequest)
        {
            var hashedPassword = new PasswordHasher().HashPassword(authenticateRequest.Password);
            var user = _unitOfWork.People.Find(x => x.Email == authenticateRequest.Username
            && x.Password == hashedPassword).FirstOrDefault();

            // return null if user not found
            if (user == null) return null;
            UserDTO userDTO = _mapper.Map<Person, UserDTO>(user);

            // authentication successful so generate jwt token
            var token = new JwtGenerateHelper(_configuration).GenerateJwtToken(authenticateRequest, userDTO);

            return new AuthenticateResponse(user, token);
        }
    }
}
