using System.ComponentModel.DataAnnotations;

namespace TodoApi.Model
{
    public record TaskItemPostPayload
    {
        [Required(ErrorMessage = "Title is required")]
        public string Title { get; init; }

        public TaskItemPostPayload(string title)
        {
            Title = title;
        }
    }
}
