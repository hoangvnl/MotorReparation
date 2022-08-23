using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository
{
    public class WorkItemRepository : IWorkItemRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public WorkItemRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }
        public async Task<WorkItemDTO> CreateItemAsync(WorkItemDTO workItemDTO)
        {
            var workItem = _mapper.Map<WorkItemDTO, WorkItem>(workItemDTO);
            workItem.CreatedDate = DateTime.Now;
            workItem.CreatedBy = "";

            var addedItem = await _db.WorkItems.AddAsync(workItem);
            await _db.SaveChangesAsync();

            return _mapper.Map<WorkItem, WorkItemDTO>(addedItem.Entity);

        }

        public async Task<int> DeleteItemByIdAsync(int workItemId)
        {
            var workItem = await _db.WorkItems.FindAsync(workItemId);
            _db.WorkItems.Remove(workItem);
            return await _db.SaveChangesAsync();
        }
    }
}
