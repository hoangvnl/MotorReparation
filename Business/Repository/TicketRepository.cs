using AutoMapper;
using Business.Repository.IRepository;
using DataAccess.Data;
using DataAccess.Enums;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace Business.Repository
{
    public class TicketRepository : ITicketRepository
    {
        private readonly ApplicationDbContext _db;
        private readonly IMapper _mapper;

        public TicketRepository(ApplicationDbContext db, IMapper mapper)
        {
            _db = db;
            _mapper = mapper;
        }

        public async Task<TicketDTO> CreateTicketAsync(TicketDTO ticketDTO)
        {
            Ticket ticket = _mapper.Map<TicketDTO, Ticket>(ticketDTO);
            ticket.CreatedDate = DateTime.UtcNow;
            ticket.CreatedBy = "";
            var addedTicket = await _db.Tickets.AddAsync(ticket);
            await _db.SaveChangesAsync();

            return _mapper.Map<Ticket, TicketDTO>(addedTicket.Entity);
        }
        public async Task<IEnumerable<TicketDTO>> GetAllTicketsAsync(string? textSearch)
        {
            try
            {
                var tickets = _mapper.Map<IEnumerable<Ticket>, IEnumerable<TicketDTO>>(_db.Tickets);

                if (!string.IsNullOrEmpty(textSearch))
                {
                    tickets = tickets.Where(x => x.Title.Contains(textSearch));
                }

                return tickets;
            }
            catch (Exception)
            {

                return null;
            }
        }

        public async Task<TicketDTO> GetTicketByIdAsync(int ticketId)
        {
            try
            {
                Ticket ticket = await _db.Tickets.Include(t => t.WorkItems).FirstOrDefaultAsync(x => x.Id == ticketId);
                TicketDTO ticketDTO = _mapper.Map<Ticket, TicketDTO>(ticket);

                return ticketDTO;


            }
            catch (Exception)
            {
                return null;
            }
        }

        public async Task<TicketDTO> UpdateTicketAsync(int ticketId, TicketDTO ticketDTO)
        {
            try
            {
                if (ticketId != ticketDTO.Id)
                {
                    return null;
                }

                Ticket ticketDetails = await _db.Tickets.FindAsync(ticketId);

               

                var ticket = _mapper.Map<TicketDTO, Ticket>(ticketDTO, ticketDetails);

                var updatedTicket = _db.Update(ticket);
                await _db.SaveChangesAsync();

                return _mapper.Map<Ticket, TicketDTO>(updatedTicket.Entity);

            }
            catch (Exception)
            {

                return null;
            }
        }
    }
}
