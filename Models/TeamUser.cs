using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using AssignmentManagementSystem.Areas.Identity.Data;

namespace AssignmentManagementSystem.Models
{
    public class TeamUser
    {
        [Key]
        public string TeamUserId { get; set; }

        [ForeignKey("UserId")]
        public AssignmentManagementSystemUser Users { get; set; }
        public string UserId { get; set; }

        [ForeignKey("TeamId")]
        public Team Team { get; set; }
        public string TeamId { get; set; }
    }
}
