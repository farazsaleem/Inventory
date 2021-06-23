using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class tblInvertory
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("tblCategory")]
        public int CategoryID { get; set; }
        public virtual tblCategory tblCategory { get; set; }
        [Required]
        public string ComputerType  { get; set; }
        [Required]
        public string Processor { get; set; }
        [Required]
        public string Brand { get; set; }
        [Required]
        public int UsbPorts { get; set; }
        [Required]
        public int RamSlots { get; set; }
        [Required]
        public string FromFactor { get; set; }
        [Required]
        public string Quantity { get; set; }
        [Required]
        public string ScreenSize { get; set; }

    }
}
