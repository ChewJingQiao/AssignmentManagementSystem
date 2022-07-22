using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AssignmentManagementSystem.Models
{
    public class TeamAssignment
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string TeamAssignmentId { get; set; }

        public string TeamId { get; set; }

        public string AssignmentId { get; set; }
        
        public string s3Location { get; set; }

        public string submitStatus { get; set; }

        public int mark { get; set; }
    }
}
