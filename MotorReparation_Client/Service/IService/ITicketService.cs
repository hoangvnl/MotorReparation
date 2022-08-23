using DataAccess.Enums;
using Models;

namespace MotorReparation_Client.Service.IService
{
    public interface ITicketService
    {
        public Task<IEnumerable<TicketDTO>> GetAllTicketsAsync();
        public Task<TicketDTO> UpdateTicketAsync(int ticketId, TicketDTO ticketDTO);
        public Task<TicketDTO> GetTicketByIdAsync(int ticketId);
        public Task<TicketDTO> CreateTicketAsync(TicketDTO ticketDTO);
    }
}
