using DAL;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BSL.Interface
{
    public interface IInventoryService
    {
        Task<List<tblInvertory>> GetAllAsync();

        Task<List<tblCategory>> GetAllCategoryAsync();
        Task<tblInvertory> Detail(int id);
        Task Add(tblInvertory entity);

        Task Update(tblInvertory entity);

        Task Remove(tblInvertory entity);

        Task CreateCategory(tblCategory entity);
    }
}
