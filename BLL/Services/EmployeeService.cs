using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using DAL.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IMapper _mapper;
        readonly IUnitOfWork _unitOfWork;

        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(EmployeeRequestDTO employeeDTO)
        {
            var person = await _unitOfWork.People.GetAsync(employeeDTO.PeopleId);
            if (person != null)
            {
                var employee = _mapper.Map<EmployeeRequestDTO, Employee>(employeeDTO);
                await _unitOfWork.Employees.CreateAsync(employee);
            }
        }

        public async Task Delete(EmployeeResponseDTO employeeDTO)
        {
            await _unitOfWork.Employees.DeleteAsync(employeeDTO.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<EmployeeResponseDTO> Get(int id)
        {
            var employee = await _unitOfWork.Employees.GetAsync(id);
            return _mapper.Map<Employee, EmployeeResponseDTO>(employee);
        }

        public async Task<IEnumerable<EmployeeResponseDTO>> GetAll()
        {
            var employees = await _unitOfWork.Employees.GetAllAsync();
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResponseDTO>>(employees);
        }

        public async Task<IEnumerable<EmployeeResponseDTO>> GetMany(EmployeeFilter filter)
        {
            var employee = await _unitOfWork.EmployeeFilter.GetPaginatedData(filter);
            return _mapper.Map<IEnumerable<Employee>, IEnumerable<EmployeeResponseDTO>>(employee);
        }
    }
}
