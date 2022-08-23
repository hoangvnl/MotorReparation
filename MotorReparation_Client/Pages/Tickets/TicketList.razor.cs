using Blazored.LocalStorage;
using Common;
using DataAccess.Enums;
using Microsoft.AspNetCore.Components;
using Models;
using MotorReparation_Client.Service.IService;

namespace MotorReparation_Client.Pages.Tickets
{
    public partial class TicketList
    {
        [Inject]
        public ITicketService ticketService { get; set; }

        [Inject]
        public ILocalStorageService localStorageService { get; set; }

        [Inject]
        public NavigationManager navigationManager { get; set; }

        public IEnumerable<TicketDTO> Tickets { get; set; } = new List<TicketDTO>();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                await LoadTickets();
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task LoadTickets()
        {
            Tickets = await ticketService.GetAllTicketsAsync();
        }


    }
}
