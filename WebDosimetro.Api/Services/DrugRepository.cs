using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebDosimetro.Api.Contracts;
using WebDosimetro.Api.Models;
using WebDosimetro.Shared;

namespace WebDosimetro.Api.Services
{
    public class DrugRepository : IDrugRepository
    {
        public AppDbContext _db { get; }
        public DrugRepository(AppDbContext db)
        {
            _db = db;
        }

        public async Task<bool> Create(Drug entity)
        {
            await _db.AddAsync(entity);
            return await Save();
        }

        public async Task<bool> Delete(Drug entity)
        {
            _db.Drugs.Remove(entity);
            return await Save();
        }

        public async Task<IList<Drug>> FindAll()
        {
            var drugs = await _db.Drugs.ToListAsync();
            return drugs;
        }

        public async Task<Drug> FindById(int id)
        {
            var drug = await _db.Drugs.FindAsync(id);
            return drug;
        }

        public async Task<bool> ItExists(int id)
        {
            return await _db.Drugs.AnyAsync(c => c.Id == id);
        }

        public async Task<bool> Save()
        {
            var changes = await _db.SaveChangesAsync();
            return changes > 0;
        }

        public async Task<bool> Update(Drug entity)
        {
            _db.Drugs.Update(entity);
            return await Save();
        }
    }
}
