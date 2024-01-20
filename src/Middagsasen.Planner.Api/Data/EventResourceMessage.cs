namespace Middagsasen.Planner.Api.Data
{
    public class EventResourceMessage
    {
        public int EventResourceMessageId { get; set; }
        public int EventResourceId { get; set;}
        public string Message { get; set; } = null!;
        public int Created { get; set; }
        public DateTime CreatedBy { get; set; }

        public User CreatedByUser { get; set; } = null!;
        public EventResource Resource { get; set; } = null!;
    }
}
