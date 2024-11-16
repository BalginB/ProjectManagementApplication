using System.ComponentModel.DataAnnotations;

namespace ProjectManagementApplication
{
    public class TaskItem
    {
        [Key]
        public int ID { get; set; }

        [Required]
        public string Description { get; set; }

        [Required]
        public string Status { get; set; } // Например: "ToDo", "InProgress", "CodeReview", "QA", "Done"
    }
}
