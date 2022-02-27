using PriorityApp.Service.Models.MasterModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace PriorityApp.Service.Contracts
{
    public interface IStateService
    {
        Task<List<StateModel>> GetAllStates();
        Task<bool> CreateState(StateModel model);
        Task<bool> UpdateState(StateModel model);

        bool DeleteState(int id);
        StateModel GetState(int id);

        Task<List<StateModel>> GetStatesBySubRegionId(int id);
    }
}
