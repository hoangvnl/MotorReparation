using Models;

namespace MotorReparation_Client.Service.IService
{
    public interface IWorkItemService
    {
        public Task<WorkItemDTO> CreateWorkItemAsync(WorkItemDTO workItemDTO);
        public Task<int> DeleteWorkItemAsync(int itemId);
    }
}
