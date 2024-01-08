namespace Middagsasen.Planner.Api.Services.Events
{
    public class TrainingRequest
    {
        public int UserId { get; set; }
        public DateTime StartTime { get; set; }
        public bool TrainingCompleted { get; set; }
    }
}