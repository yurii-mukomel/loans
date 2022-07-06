using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StatementTypeService : IStatementTypeService
    {
        private readonly IMapper _mapper;
        readonly IUnitOfWork _unitOfWork;

        public StatementTypeService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task Create(StatementTypeDTO statementTypeDTO)
        {
            var statementType = _mapper.Map<StatementTypeDTO, StatementType>(statementTypeDTO);
            await _unitOfWork.StatementTypes.CreateAsync(statementType);
        }

        public async Task Delete(StatementTypeDTO statementTypeDTO)
        {
            await _unitOfWork.StatementTypes.DeleteAsync(statementTypeDTO.Id);
        }

        public async Task<StatementTypeDTO> Get(int id)
        {
            var statementType = await _unitOfWork.StatementTypes.GetAsync(id);
            return _mapper.Map<StatementType, StatementTypeDTO>(statementType);
        }

        public async Task<IEnumerable<StatementTypeDTO>> GetAll()
        {
            var statementTypes = await _unitOfWork.StatementTypes.GetAllAsync();

            return _mapper.Map<IEnumerable<StatementType>, IEnumerable<StatementTypeDTO>>(statementTypes);
        }

        public async Task Update(StatementTypeDTO statementTypeDTO)
        {
            await _unitOfWork.StatementTypes.UpdateAsync(_mapper.Map<StatementTypeDTO, StatementType>(statementTypeDTO));
        }
    }
}
