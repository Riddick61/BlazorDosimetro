using BlazorDosimetro.Services;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDosimetro.Shared;

namespace BlazorDosimetro.Data
{
    public class DrugOverviewBase: ComponentBase
    {
        public IEnumerable<Drug> Drugs { get; set; }

        [Inject]
        public IDrugDataService DrugsDataService { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Drugs = (await DrugsDataService.GetAllDrugs()).ToList();
        }
    }
}
