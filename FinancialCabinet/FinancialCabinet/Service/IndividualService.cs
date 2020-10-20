using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using FinancialCabinet.Database;
using FinancialCabinet.Entity;
using FinancialCabinet.Interface;
using FinancialCabinet.Model;
using Microsoft.EntityFrameworkCore;

namespace FinancialCabinet.Service
{
    public class IndividualService : IIndividualManagementService
    {
        private ApiDbContext _db;
        private readonly IMapper _mapper;

        public IndividualService(ApiDbContext context, IMapper mapper)
        {
            _db = context;
            _mapper = mapper;
        }

        public async Task<IndividualModel> CreateIndividual(IndividualModel model)
        {

            Individual individual = _mapper.Map<Individual>(model);
            individual = _db.Individuals.Add(individual).Entity;
            await _db.SaveChangesAsync();
            return _mapper.Map<IndividualModel>(individual);
        }

        public async Task<bool> EditIndividual(Guid id, IndividualModel model)
        {
            if (model != null)
            {
                Individual individual = _mapper.Map<Individual>(model);

                individual.Id = id;
                _db.Individuals.Update(individual);
                await _db.SaveChangesAsync();
                return true;
            }

            return false;
        }

        public bool Get(Guid id, out Individual individual)
        {
            individual = _db.Individuals.FirstOrDefault(p => p.Id == id);
            if (individual != null)
            {
                return true;
            }
            else
            {
                return false;
            }

        }
    }
}