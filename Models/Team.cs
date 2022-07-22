using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentManagementSystem.Models
{
    public class Team
    {
        [Key]
        public string TeamId { get; set; }

        
    }
}
