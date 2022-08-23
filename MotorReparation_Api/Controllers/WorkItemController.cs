using Business.Repository.IRepository;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MotorReparation_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class WorkItemController : Controller
    {
        private readonly IWorkItemRepository _workItemRepository;

        public WorkItemController(IWorkItemRepository workItemRepository)
        {
            _workItemRepository = workItemRepository;
        }

        [HttpPost]
        public async Task<IActionResult> CreateWorkItemAsync([FromBody] WorkItemDTO workItemDTO)
        {
            try
            {
                var result = await _workItemRepository.CreateItemAsync(workItemDTO);
                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }

        }

        [HttpDelete("{workItemId}")]
        public async Task<IActionResult> DeleteItemAsync(int workItemId)
        {
            try
            {
                var result = await _workItemRepository.DeleteItemByIdAsync(workItemId);

                return Ok(result);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }

        }
    }
}
