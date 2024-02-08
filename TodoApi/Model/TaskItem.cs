using System.ComponentModel.DataAnnotations.Schema;

namespace TodoApi.Model
{
    [Table("TaskItems")]
    public class TaskItem
    {
        [Column("id")]
        public int Id { get; set; }
        [Column("title")]
        public string Title { get; set; }
        [Column("is_completed")]
        public bool IsCompleted { get; set; }
    }
}
