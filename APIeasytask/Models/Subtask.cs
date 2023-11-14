using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace APIeasytask.Models
{
    public class Subtask
    {
        [Key] public int SubtaskId { get; set; }
        [Required]
        [Column(TypeName = "nvarchar(100)")]
        public string Title { get; set; }
        [Required]
        public bool IsDone { get; set; }
        [Required]
        public int TaskId { get; set; }
    }
}
