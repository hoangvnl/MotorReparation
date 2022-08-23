using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface IWorkItemRepository
    {
        public Task<WorkItemDTO> CreateItemAsync(WorkItemDTO workItemDTO);
        public Task<int> DeleteItemByIdAsync(int workItemId);
    }
}
