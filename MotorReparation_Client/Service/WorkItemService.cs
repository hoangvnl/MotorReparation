using DataAccess.Data;
using Models;
using MotorReparation_Client.Service.IService;
using Newtonsoft.Json;
using System.Text;

namespace MotorReparation_Client.Service
{
    public class WorkItemService : IWorkItemService
    {
        private readonly HttpClient _httpClient;

        public WorkItemService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<WorkItemDTO> CreateWorkItemAsync(WorkItemDTO workItemDTO)
        {
            var content = JsonConvert.SerializeObject(workItemDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/workitem", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<WorkItemDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            return null;
        }

        public async Task<int> DeleteWorkItemAsync(int itemId)
        {
            var response = await _httpClient.DeleteAsync($"api/workitem/{itemId}");
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<int>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            return 0;
        }
    }
}
