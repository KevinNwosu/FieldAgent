namespace FieldAgent.Core.Entities
{
    public class Agent
    {
        public int AgentId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public decimal Height { get; set; }
        public List<AgencyAgent> AgencyAgents { get; set; }
        public List<MissionAgent> MissionAgents { get; set; }
        public List<Alias> Aliases { get; set; }
    }
}
