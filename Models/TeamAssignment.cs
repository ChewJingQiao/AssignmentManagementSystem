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
        public int TeamAssignmentId { get; set; }

        public string AssignmentId { get; set; }
        
        public string s3Location { get; set; }

        public string submitStatus { get; set; }

        public int mark { get; set; }

        public string Teammate1 { get; set; }

        public string Teammate2 { get; set; }

        public string Teammate3 { get; set; }

        public string Teammate4 { get; set; }
    }
}
