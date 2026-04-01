using System;
using Anthropic;
using Anthropic.Models.Messages;

namespace LearningCoachAPI.Services;
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

    public async Task<string> AskClaudeAsync(string userMessage)
    {
       // AnthropicClient client = new();

        MessageCreateParams parameters = new()
        {
            MaxTokens = 1024,
            Messages =
            [
                new()
                {
                    Role = Role.User,
                    Content = userMessage,
                },
            ],
            Model = "claude-opus-4-6",
        };

        var message = await _client.Messages.Create(parameters);
        
      return message.Content[0].Json.GetProperty("text").GetString() ?? "No response";
    }

}

