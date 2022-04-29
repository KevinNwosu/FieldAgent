using System.ComponentModel.DataAnnotations;

namespace FieldAgent.API.Models
{
    public class ViewAliasModel
    {
        public int AliasId { get; set; }
        
        public string AliasName { get; set; }
        public Guid? InterpolId { get; set; }
        public string Persona { get; set; }

        [Required(ErrorMessage = "Agent Id is required")]
        public int AgentId { get; set; }
    }
}
