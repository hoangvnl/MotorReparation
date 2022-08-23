using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;
using MotorReparation_Client.Service.IService;

namespace MotorReparation_Client.Pages.Tickets
{
    public partial class TicketDetails
    {
        private TicketDTO TicketModel { get; set; } = new TicketDTO();
        private string Title { get; set; } = "Create";
        [Inject]
        public ITicketService TicketService { get; set; }

        [Inject]
        public IWorkItemService WorkItemService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Inject]
        public NavigationManager NavManager { get; set; }

        [Parameter]
        public int? Id { get; set; }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                if (Id != null)
                {
                    Title = "Update";
                    TicketModel = await TicketService.GetTicketByIdAsync(Id.Value);
                }
                else
                {
                    TicketModel = new TicketDTO();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task HandleTicketUpsert()
        {
            try
            {
                TicketDTO newTicket;

                if (TicketModel.Id != 0 && Title == "Update")
                {
                    newTicket = await TicketService.UpdateTicketAsync(Id.Value, TicketModel);

                }
                else
                {
                    newTicket = await TicketService.CreateTicketAsync(TicketModel);
                }

                if (newTicket != null)
                {
                    NavManager.NavigateTo("/tickets");
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task OnClickCreateWorkItemButton()
        {
            await JSRuntime.InvokeVoidAsync("ShowCreateItemModal");
        }

        public async Task ConfirmCreate_Click(bool value)
        {
            if (value)
            {
                TicketModel = await TicketService.GetTicketByIdAsync(Id.Value);
            }

            await JSRuntime.InvokeVoidAsync("HideCreateItemModal");
        }

        private async Task OnClickDeleteItem(int id)
        {
            try
            {
                int response = await WorkItemService.DeleteWorkItemAsync(id);
                if (response != 0)
                {
                    TicketModel = await TicketService.GetTicketByIdAsync(Id.Value);

                }
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
