using DataAccess.Enums;
using Models;
using MotorReparation_Client.Service.IService;
using Newtonsoft.Json;
using System.Text;

namespace MotorReparation_Client.Service
{
    public class TicketService : ITicketService
    {
        private readonly HttpClient _httpClient;

        public TicketService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IEnumerable<TicketDTO>> GetAllTicketsAsync()
        {
            var response = await _httpClient.GetAsync($"api/ticket/get-all-tickets");
            var content = await response.Content.ReadAsStringAsync();
            var tickets = JsonConvert.DeserializeObject<IEnumerable<TicketDTO>>(content);

            return tickets;
        }

        public async Task<TicketDTO> UpdateTicketAsync(int ticketId, TicketDTO ticketDTO)
        {
            var content = JsonConvert.SerializeObject(ticketDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PutAsync($"api/ticket/update-ticket/{ticketId}", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TicketDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            return null;
        }

        public async Task<TicketDTO> GetTicketByIdAsync(int ticketId)
        {
            var response = await _httpClient.GetAsync($"api/ticket/get-ticket-by-id/{ticketId}");
            var content = await response.Content.ReadAsStringAsync();
            var ticket = JsonConvert.DeserializeObject<TicketDTO>(content);

            return ticket;
        }

        public async Task<TicketDTO> CreateTicketAsync(TicketDTO ticketDTO)
        {
            var content = JsonConvert.SerializeObject(ticketDTO);
            var bodyContent = new StringContent(content, Encoding.UTF8, "application/json");
            var response = await _httpClient.PostAsync($"api/ticket/create-ticket", bodyContent);
            var contentTemp = await response.Content.ReadAsStringAsync();
            var result = JsonConvert.DeserializeObject<TicketDTO>(contentTemp);

            if (response.IsSuccessStatusCode)
            {
                return result;
            }

            return null;
        }

    }
}
