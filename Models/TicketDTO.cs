using DataAccess.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models
{
    public class TicketDTO
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Please enter ticket title")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Please enter ticket description")]
        public string Description { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.UtcNow;
        public string CreatedBy { get; set; }
        public bool CanUpdateStatus { get; set; } = false;
        public TicketStatus Status { get; set; }
        public virtual ICollection<WorkItemDTO> WorkItems { get; set; }
        
    }
}
