using FieldAgent.Core.Entities;
using System.ComponentModel.DataAnnotations;

namespace FieldAgent.API.Models
{
    public class ViewMissionModel
    {
        public int MissionId { get; set; }

        [Required(ErrorMessage = "CodeName is required")]
        public string CodeName { get; set; }
        
        public string Notes { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime ProjectedEndDate { get; set; }
        public DateTime? ActualEndDate { get; set; }
        public decimal? OperationalCost { get; set; }
        public int AgencyId { get; set; }
        public List<Agent> Agents { get; set; }
    }
}
