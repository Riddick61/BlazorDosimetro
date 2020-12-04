using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDosimetro.Shared;

namespace BlazorDosimetro.Services
{
    public interface IDrugDataService
    {
        Task<IEnumerable<Drug>> GetAllDrugs();
        Task<Drug> GetDrugDetails(int drugId);
        Task<Drug> AddDrug(Drug drug);
        Task UpdateDrug(Drug drug);
        Task DeleteDrug(int drugId);
    }
}
