using System;
using System.Linq;
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

        public async Task<bool> EditIndividual(Guid id, EditIndividualModel model)
        {
            Individual individual = await _db.Individuals.FirstOrDefaultAsync(p => p.Id == id);
            if (individual != null)
            {
                if (model.LastName != null)
                {
                    individual.LastName = model.LastName;
                }

                if (model.TypeDocument != null)
                {
                    individual.TypeDocument = model.TypeDocument;
                }

                if (model.NumberDocument != null)
                {
                    individual.NumberDocument = model.NumberDocument;
                }
                if (model.Salary != null)
                {
                    individual.Salary = (double)model.Salary;
                }

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