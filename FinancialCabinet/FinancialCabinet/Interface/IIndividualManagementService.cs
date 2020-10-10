using System;
using System.Threading.Tasks;
using FinancialCabinet.Entity;
using FinancialCabinet.Model;

namespace FinancialCabinet.Interface
{
    public interface IIndividualManagementService
    {
        public Task<Individual> CreateIndividual(IndividualModel model, User user);
        public Task<bool> EditIndividual(Guid id, EditIndividualModel model);
        public bool Get(Guid id, out Individual individual);

    }
}