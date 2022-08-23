using DataAccess.Data;
using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class WorkItemDTO
    {
        public int Id { get; set; }
        public int ParentTicket { get; set; }
        public ItemType Type { get; set; }
        public double LaborPrice { get; set; }
        public double HoursPerQuantity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
    }
}
