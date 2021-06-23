using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class tblCategory
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string CategoryName { get; set; }
       
    }
}
