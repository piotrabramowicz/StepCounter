namespace StepCounterWebApi.Models
{
    public class TeamMember
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid TeamId { get; set; }
        public int NumberOfSteps { get; set; } = 0;
    }
}       