using System;
using System.Threading.Tasks;
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

        public IndividualService(ApiDbContext context)
        {
            _db = context;
        }

        public async Task<Individual> CreateIndividual(IndividualModel model, User user)
        {
            Individual individual = new Individual
            {
                Name = model.Name, LastName = model.LastName,
                Patronymic = model.Patronymic, Salary = model.Salary, UserID = user.Id, User = user,
                NumberDocument = model.NumberDocument, TypeDocument = model.TypeDocument,
                DateOfBirth = model.DateOfBirth
            };
            _db.Individuals.Add(individual);
            await _db.SaveChangesAsync();
            Individual ind = await _db.Individuals.FirstOrDefaultAsync(p => p.UserID == user.Id);
            return ind;
        }

        public bool EditIndividual(Guid id, IndividualModel model)
        {
            throw new NotImplementedException();
        }

        public bool Get(Guid id, out IndividualModel model)
        {
            throw new NotImplementedException();
        }
    }
}