using System;
using Anthropic;
using Anthropic.Models.Messages;
using LearningCoachAPI.Models;

namespace LearningCoachAPI.Services;

/// <summary>
/// Handles the AI connection.
/// </summary>
//documentation：ps://platform.claude.com/docs/en/api/sdks/csharp
public class ClaudeService

{
    private readonly AnthropicClient _client;
    public ClaudeService (IConfiguration configuration){
    
        var apiKey = configuration["Anthropic:ApiKey"] ?? throw new InvalidOperationException("cannot find ApiKey");
    
        _client = new AnthropicClient() { ApiKey = apiKey } ;
        //AnthropicClient client = new() { ApiKey = apiKey };
       // _client=client;
    }
/// <summary>
/// Sends the user message to Claude API and returns the response.
/// </summary>
/// <param name="userMessage"></param>
/// <returns></returns>
    public async Task<string> AskClaudeAsync(List<ChatMessage> chatMessages,string newMessage,string systemPrompt)
    {
       // AnthropicClient client = new();
       var historyMessages = chatMessages.Select(m => new MessageParam
       {
          Role = m.MessageRole=="user"?Role.User:Role.Assistant,
          Content=m.MessageContent,
       });
       

        MessageCreateParams parameters = new()
        {
            MaxTokens = 1024, 
            // System = new List<SystemPrompt> { new() { Text = systemPrompt } },
            System =  systemPrompt,
            Messages =
                historyMessages.Append(new()
                {
                    Role = Role.User,
                    Content = newMessage,
                }).ToList(),
            
            Model = "claude-sonnet-4-6",
            // "claude-opus-4-6",
        };

        var message = await _client.Messages.Create(parameters);
        
        
      return message.Content[0].Json.GetProperty("text").GetString() ?? "No response";
    }

}

