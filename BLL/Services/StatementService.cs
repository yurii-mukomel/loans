using AutoMapper;
using BLL.DTO;
using BLL.Interfaces;
using DAL;
using DAL.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace BLL.Services
{
    public class StatementService : IStatementService
    {
        private readonly IMapper _mapper;
        readonly IUnitOfWork _unitOfWork;

        public StatementService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Create(StatementDTO statementDTO)
        {
            await _unitOfWork.Statements.CreateAsync(_mapper.Map<StatementDTO, Statement>(statementDTO));
            await _unitOfWork.SaveAsync();
        }

        public async Task Delete(StatementDTO statementDTO)
        {
            await _unitOfWork.Statements.DeleteAsync(statementDTO.Id);
            await _unitOfWork.SaveAsync();
        }

        public async Task<StatementDTO> Get(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<StatementDTO>> GetAll()
        {
            throw new NotImplementedException();
        }

        public async Task Update(StatementDTO statementDTO)
        {
            throw new NotImplementedException();
        }
    }
}
