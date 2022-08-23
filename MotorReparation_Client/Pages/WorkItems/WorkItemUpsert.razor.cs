using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Models;
using MotorReparation_Client.Service.IService;

namespace MotorReparation_Client.Pages.WorkItems
{
    public partial class WorkItemUpsert
    {
        private WorkItemDTO ItemModel = new WorkItemDTO();

        [Inject]
        public IWorkItemService WorkItemService { get; set; }

        [Inject]
        public IJSRuntime JSRuntime { get; set; }

        [Parameter]
        public EventCallback<bool> ConfirmationChange { get; set; }

        [Parameter]
        public int ParentTicket { get; set; }
        private async Task HandleWorkItemUpsert()
        {
            try
            {
                ItemModel.ParentTicket = ParentTicket;
                var createdItem = await WorkItemService.CreateWorkItemAsync(ItemModel);
                await OnConfirmationChange(true);

            }
            catch (Exception)
            {

                throw;
            }
        }

        private async Task OnConfirmationChange(bool value)
        {
            await ConfirmationChange.InvokeAsync(value);
        }
    }
}
