using Business.Repository.IRepository;
using DataAccess.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Models;

namespace MotorReparation_Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    public class TicketController : Controller
    {
        private readonly ITicketRepository _ticketRepository;

        public TicketController(ITicketRepository ticketRepository)
        {
            _ticketRepository = ticketRepository;
        }

        [HttpGet("get-all-tickets")]
        public async Task<IActionResult> GetAllTicketsAsync([FromQuery] string? textSearch)
        {
            try
            {
                var allTickets = await _ticketRepository.GetAllTicketsAsync(textSearch);
                return Ok(allTickets);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
        [HttpGet("get-ticket-by-id/{id}")]
        public async Task<IActionResult> GetTicketByIdAsync(int id)
        {
            try
            {
                var result = await _ticketRepository.GetTicketByIdAsync(id);

                return Ok(result);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPut("update-ticket/{id}")]
        public async Task<IActionResult> UpdateTicketStatusAsync(int id, [FromBody] TicketDTO ticketDTO)
        {
            try
            {
                var updatedTicket = await _ticketRepository.UpdateTicketAsync(id, ticketDTO);
                return Ok(updatedTicket);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }

        [HttpPost("create-ticket")]
        public async Task<IActionResult> CreateTicketAsync([FromBody] TicketDTO ticketDTO)
        {
            try
            {
                var returnTicketDTO = await _ticketRepository.CreateTicketAsync(ticketDTO);
                return Ok(returnTicketDTO);
            }
            catch (Exception ex)
            {

                return BadRequest(ex.Message);
            }
        }
    }
}
