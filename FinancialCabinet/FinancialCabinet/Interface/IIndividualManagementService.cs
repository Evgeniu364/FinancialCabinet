using System;
using System.Threading.Tasks;
using FinancialCabinet.Entity;
using FinancialCabinet.Model;

namespace FinancialCabinet.Interface
{
    public interface IIndividualManagementService
    {
        public Task<Individual> CreateIndividual(IndividualModel model, User user);
        public bool EditIndividual(Guid id, IndividualModel model);
        public bool Get(Guid id, out IndividualModel model);

    }
}