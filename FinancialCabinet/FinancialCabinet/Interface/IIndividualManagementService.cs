using System;
using System.Threading.Tasks;
using FinancialCabinet.Entity;
using FinancialCabinet.Model;

namespace FinancialCabinet.Interface
{
    public interface IIndividualManagementService
    {
        public Task<IndividualModel> CreateIndividual(IndividualModel model);
        public Task<bool> EditIndividual(Guid id, IndividualModel model);
        public bool Get(Guid id, out Individual individual);

    }
}