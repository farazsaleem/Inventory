using BSL.Interface;
using DAL;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSL.SQLRepository
{
    public class InventoryService : IInventoryService
    {
        private readonly ApplicationDbContext Context;

        public InventoryService(ApplicationDbContext context)
        {
            this.Context = context;
        }

       

        public async Task<List<tblInvertory>> GetAllAsync()
        {
            List<tblInvertory> Processor = await Context.tblInvertories.Include(x => x.tblCategory).ToListAsync();
            return Processor;
        }
        public async Task<tblInvertory> Detail(int id)
        {
            tblInvertory Inventory = await Context.tblInvertories.Include(x => x.tblCategory).Where(x => x.Id == id).FirstOrDefaultAsync();
            return Inventory;
        }
       

        public async Task Add(tblInvertory entity)
        {
            await Context.Set<tblInvertory>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }

        public async Task<List<tblCategory>> GetAllCategoryAsync()
        {
            List<tblCategory> categories = await Context.tblCategories.ToListAsync();
            return categories;
        }

        public Task Update(tblInvertory entity)
        {
            Context.Entry(entity).State = EntityState.Modified;
            return Context.SaveChangesAsync();
        }

        public Task Remove(tblInvertory entity)
        {
            Context.Set<tblInvertory>().Remove(entity);
            return Context.SaveChangesAsync();
        }

        public async Task CreateCategory(tblCategory entity)
        {
            await Context.Set<tblCategory>().AddAsync(entity);
            await Context.SaveChangesAsync();
        }
    }
}
