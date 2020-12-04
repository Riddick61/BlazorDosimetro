using BlazorDosimetro.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDosimetro.Shared;

namespace BlazorDosimetro.Data
{
    public class DrugDetailBase: ComponentBase
    {
        [Inject]
        public IDrugDataService DrugsDataService { get; set; }

        [Inject]
        public NavigationManager NavigationManager { get; set; }

        [Parameter]
        public string Id { get; set; }
        public Drug drug { get; set; } = new Drug();

        protected async override Task OnInitializedAsync()
        {
            drug = await DrugsDataService.GetDrugDetails(int.Parse(Id));
        }
    }
}
