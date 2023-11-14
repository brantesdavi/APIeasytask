using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIeasytask.Models
{
    public class Task
    {
        [Key]
        public int TaskId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; } = "";
        [Column(TypeName = "nvarchar(250)")]
        public string Description { get; set; }

        [Required]
        public DateTime CreatedAt { get; set; }
        public DateTime Deadline { get; set; }

        [ForeignKey("PriorityId")]
        public Priority Priority { get; set; }
        public List<Subtask> Subtasks { get; set; }

    }
}
