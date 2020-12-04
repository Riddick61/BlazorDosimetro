using BlazorDosimetro.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDosimetro.Shared;

namespace BlazorDosimetro.Data
{
    public class DrugAdd: ComponentBase
    {
        [Inject]
        public IDrugDataService DrugsDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }
        public int DrugId { get; set; }
        public Drug drug { get; set; } = new Drug();

        //used to store state of screen
        protected string Message = string.Empty;
        protected string StatusClass = string.Empty;
        protected bool Saved;

        protected override void OnInitialized()
        {
            Saved = false;
            //add some defaults
            drug = new Drug { Name = "", NoPills = 1, DoseToTake = 1, StartDate = DateTime.Now, DateToEnd = DateTime.Now };
        }

        protected async Task HandleValidSubmit()
        {
            var addedDrug = await DrugsDataService.AddDrug(drug);
            if (addedDrug != null)
            {
                StatusClass = "alert-success";
                Message = "New drug added successfully.";
                Saved = true;
            }
            else
            {
                StatusClass = "alert-danger";
                Message = "Something went wrong adding the new employee. Please try again.";
                Saved = false;
            }
           
        }

        protected void HandleInvalidSubmit()
        {
            StatusClass = "alert-danger";
            Message = "There are some validation errors. Please try again.";
        }
        protected void NavigateToOverview()
        {
            NavigationManager.NavigateTo("/drugoverview");
        }
    }
}
