namespace TodoApi.Model
{
    public record TaskItemUpdatePayload(string? Title, bool? IsCompleted);
}
