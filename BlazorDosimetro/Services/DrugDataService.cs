using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using WebDosimetro.Shared;

namespace BlazorDosimetro.Services
{
    public class DrugDataService : IDrugDataService
    {
        private readonly HttpClient _httpClient;

        public DrugDataService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }
        public async Task<Drug> AddDrug(Drug drug)
        {
            var drugJson =
                new StringContent(JsonSerializer.Serialize(drug), Encoding.UTF8, "application/json");

            var response = await _httpClient.PostAsync("api/drugs/", drugJson);

            if (response.IsSuccessStatusCode)
            {
                return await JsonSerializer.DeserializeAsync<Drug>(await response.Content.ReadAsStreamAsync());
            }

            return null;
        }
        public async Task DeleteDrug(int drugId)
        {
            await _httpClient.DeleteAsync($"api/drugs/{drugId}");
        }

        public async Task<IEnumerable<Drug>> GetAllDrugs()
        {
            return await JsonSerializer.DeserializeAsync<IEnumerable<Drug>>
                (await _httpClient.GetStreamAsync($"api/drugs/"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task<Drug> GetDrugDetails(int drugId)
        {
            return await JsonSerializer.DeserializeAsync<Drug>
               (await _httpClient.GetStreamAsync($"api/drugs/{drugId}"), new JsonSerializerOptions() { PropertyNameCaseInsensitive = true });
        }

        public async Task UpdateDrug(Drug drug)
        {
            var drugJson =
                new StringContent(JsonSerializer.Serialize(drug), Encoding.UTF8, "application/json");

            await _httpClient.PutAsync("api/drugs", drugJson);
        }
    }
}
