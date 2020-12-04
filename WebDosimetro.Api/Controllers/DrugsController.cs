using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebDosimetro.Api.Contracts;
using WebDosimetro.Api.DTOs;
using WebDosimetro.Api.Models;
using WebDosimetro.Shared;

namespace WebDosimetro.Api.Controllers
{
    [Route("api/[controller]")]
    [Produces("application/json")]
    [ApiController]
    public class DrugsController : ControllerBase
    {
        private readonly IDrugRepository _drugRepository;
        private readonly IMapper _mapper;

        public DrugsController(IDrugRepository drugRepository, IMapper mapper)
        {
            _drugRepository = drugRepository;
            _mapper = mapper;
        }

        // GET: api/Drugs
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Drug>>> GetAllDrugs()
        {
            var drugs = await _drugRepository.FindAll();
            var drugDTOs = _mapper.Map<IList<DrugDTO>>(drugs);
            List<DrugDTO> lstDrugs = new List<DrugDTO>();

            foreach (var drug in drugDTOs)
            {
                // what time is it now? How many days passed?
                int diffDays = DateTime.Now.Subtract(drug.StartDate).Days;
                int NoPillsLeft = drug.NoPills - (diffDays * drug.DoseToTake);
                int DaysLeft = NoPillsLeft / drug.DoseToTake;

                DateTime dateToEnd = DateTime.Now.AddDays(DaysLeft);
                drug.DateToEnd = dateToEnd;
                lstDrugs.Add(_mapper.Map<DrugDTO>(drug));
            }

            List<DrugDTO> listSorted = lstDrugs;
            listSorted.Sort(delegate (DrugDTO x, DrugDTO y)
            {
                return x.DateToEnd.CompareTo(y.DateToEnd);
            });

            return Ok(listSorted);
        }


        // GET: api/Drugs/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Drug>> GetDrugById(int id)
        {
            var drug = await _drugRepository.FindById(id);
            var drugDTO = _mapper.Map<DrugDTO>(drug);
           
            // what time is it now? How many days passed?
            int diffDays = DateTime.Now.Subtract(drug.StartDate).Days;
            int NoPillsLeft = drug.NoPills - (diffDays * drug.DoseToTake);
            int DaysLeft = NoPillsLeft / drug.DoseToTake;
            DateTime dateToEnd = DateTime.Now.AddDays(DaysLeft);
            drug.DateToEnd = dateToEnd;


            if (drug == null)
            {
                return NotFound();
            }

            return drug;
        }


        // POST: api/Drugs
        // To protect from overposting attacks, enable the specific properties you want to bind to, for
        // more details, see https://go.microsoft.com/fwlink/?linkid=2123754.
        [HttpPost]
        public async Task<ActionResult<Drug>> CreateDrug(Drug drug)
        {
            await _drugRepository.Create(drug);
            return CreatedAtAction("GetDrugById", new { id = drug.Id }, drug);
        }

        // DELETE: api/Drugs/5
        [HttpDelete("{id}")]
        public async Task<ActionResult<Drug>> DeleteDrug(int id)
        {
          
            var itExists = await _drugRepository.ItExists(id);
            if (!itExists)
            {
                return NotFound();
            }
            var drug = await _drugRepository.FindById(id);

            var isSuccess = await _drugRepository.Delete(drug);
            if (!isSuccess)
            {
                return InternalError($"Drug delete failed");
            }
            return NoContent();
        }

        [HttpPut]
        public async Task<IActionResult> UpdateDrug([FromBody] DrugDTO drugDTO)
        {
            if (drugDTO == null)
                return BadRequest();

            if (!ModelState.IsValid)
                return BadRequest(ModelState);


            var itExists = await _drugRepository.ItExists(drugDTO.Id);
            if (!itExists)
            {
                return NotFound();
            }

            var drug = _mapper.Map<Drug>(drugDTO);

            var isSuccess = await _drugRepository.Update(drug);
            if (!isSuccess)
            {
                return InternalError($"Update operation failed");
            }

            return NoContent(); //success
        }

        private ObjectResult InternalError(string message)
        {
            return StatusCode(500, "Something went wrong. Please contact the Administrator");
        }

    }
}
