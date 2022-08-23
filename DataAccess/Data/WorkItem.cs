using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Enums;

namespace DataAccess.Data
{
    public class WorkItem
    {
        public int Id { get; set; }
        public int ParentTicket { get; set; }
        public ItemType Type{ get; set; }
        public double LaborPrice { get; set; }
        public double HoursPerQuantity { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }

        [ForeignKey("ParentTicket")]
        public virtual Ticket Ticket { get; set; }
    }
}
