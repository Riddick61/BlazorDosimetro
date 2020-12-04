using BlazorDosimetro.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDosimetro.Shared;

namespace BlazorDosimetro.Data
{
    public class DrugEditBase : ComponentBase
    {
        [Inject]
        public IDrugDataService DrugsDataService { get; set; }

        [Parameter]
        public EventCallback<bool> OnDrugDeleted { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }
        public Drug drug { get; set; } = new Drug();

        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected ConfirmBase DeleteConfirmation { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Saved = false;
            drug = await DrugsDataService.GetDrugDetails(int.Parse(Id));
        }

        protected async Task HandleValidSubmit()
        {
            drug.Id = int.Parse(Id);
            await DrugsDataService.UpdateDrug(drug);

            try
            {
                StatusClass = "alert-success";
                Message = "Drug updated successfully.";
                Saved = true;
            }
            catch (Exception)
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new drug. Please try again.";
                Saved = false;
            }
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }

        protected void DeleteDrug()
        {
            DeleteConfirmation.Show();
            StateHasChanged();
        }

        protected async Task ConfirmDeleteDrug(bool deletedConfirmed)
        {
            if (deletedConfirmed)
            {
                await DrugsDataService.DeleteDrug(drug.Id);
                await OnDrugDeleted.InvokeAsync(true);
                StatusClass = "alert-success";
                Message = "Deleted successfully";
                Saved = true;
            }
        }
        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/drugoverview");
        }

    }
}
