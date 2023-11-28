namespace Middagsasen.Planner.Api.Services.Events
{
    public class UpdateResourceTypeRequest
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public int DefaultStaff { get; set; }
    }
}