namespace LearningCoachAPI.Models;

public class ChatMessage
{
    public int ChatMessageID { get;set; }
    public DateTime Time{get;set;}
    public int UserID { get; set; }
    public string MessageContent { get; set; }
    public string MessageRole { get; set; }

}