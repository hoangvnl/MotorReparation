using DataAccess.Enums;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Repository.IRepository
{
    public interface ITicketRepository
    {
        public Task<TicketDTO> CreateTicketAsync(TicketDTO ticketDTO);
        public Task<IEnumerable<TicketDTO>> GetAllTicketsAsync(string? textSearch);
        public Task<TicketDTO> GetTicketByIdAsync(int ticketId);
        public Task<TicketDTO> UpdateTicketAsync(int ticketId, TicketDTO ticketDTO);
    }
}
