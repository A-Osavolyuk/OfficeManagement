namespace TaskManagerAPI.Models.Entities
{
    public class EmployeeTask
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public Guid TaskId { get; set; }
    }
}
