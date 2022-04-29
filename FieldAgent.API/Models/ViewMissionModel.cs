using FieldAgent.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FieldAgent.API.Models
{
    public class ViewMissionModel
    {
        public int MissionId { get; set; }

        [Required(ErrorMessage = "CodeName is required")]
        [StringLength(50, ErrorMessage = "CodeName cannot be longer than 50 characters")]
        public string CodeName { get; set; }
        
        public string Notes { get; set; }
        
        [Required(ErrorMessage = "Start Date is required")]
        public DateTime StartDate { get; set; }

        [Required(ErrorMessage = "Projected End Date is required")]
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? OperationalCost { get; set; }

        [Required(ErrorMessage = "Agency Id is required")]
        public int AgencyId { get; set; }
        public List<Agent>? Agents { get; set; }
    }
}
