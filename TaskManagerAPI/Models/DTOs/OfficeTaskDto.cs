using System.ComponentModel.DataAnnotations;

namespace TaskManagerAPI.Models.DTOs
{
    public class OfficeTaskDto
    {
        public int CreatorId { get; set; }
        public int ConsumerId { get; set; }
        public string TaskName { get; set; } = string.Empty;
        public string TaskDescription { get; set; } = string.Empty;
        public bool IsRegular { get; set; } = false;
        public bool IsOptional { get; set; } = false;
        public DateTime StartDate { get; set; } = DateTime.UtcNow;
        public DateTime EndDate { get; set; } = DateTime.UtcNow;
    }
}
