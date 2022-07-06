using AutoMapper;
using BLL.DTO;
using BLL.Helpers;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class PersonService : IPersonService
    {
        readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public PersonService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Create(PersonDTO person)
        {
            person.Password = new PasswordHasher().HashPassword(person.Password);
            await _unitOfWork.People.CreateAsync(_mapper.Map<PersonDTO, Person>(person));
        }

        public async Task Delete(PersonDTO person)
        {
            await _unitOfWork.People.DeleteAsync(person.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<PersonDTO> Get(int id)
        {
            var person = await _unitOfWork.People.GetAsync(id);
            return _mapper.Map<Person, PersonDTO>(person);
        }

        //public async Task<IEnumerable<PersonDTO>> GetAll()
        //{
        //    var people = await _unitOfWork.People.GetAllAsync();
        //    return _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(people);
        //}

        public async Task<IEnumerable<PersonDTO>> GetMany(PersonFilter filter)
        {
            var people = await _unitOfWork.PersonFilter.GetPaginatedData(filter);
            return _mapper.Map<IEnumerable<Person>, IEnumerable<PersonDTO>>(people);
        }

        public async Task Update(PersonDTO person)
        {
            person.Password = new PasswordHasher().HashPassword(person.Password);
            await _unitOfWork.People.UpdateAsync(_mapper.Map<PersonDTO, Person>(person));
        }
    }
}
