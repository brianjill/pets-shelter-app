using Azure;
using Azure.AI.OpenAI;
using Microsoft.Extensions.Options;
using OpenAI.Chat;

namespace PetsShelterApi.AzureAi;

public class ChatService : IChatService
{
    private readonly AzureOpenAIClient _azureClient;
    private readonly string _deploymentName;

    public ChatService(IOptions<AzureAiChatOptions> options)
    {
        _azureClient = new AzureOpenAIClient(new Uri(options.Value.Endpoint), new AzureKeyCredential(options.Value.Key));
        _deploymentName = options.Value.DeploymentName;
    }


    public string Chat(string question)
    {
        var chatClient = _azureClient.GetChatClient(_deploymentName);
        var requestOptions = new ChatCompletionOptions()
        {
            MaxOutputTokenCount = 4096,
            Temperature = 1.0f,
            TopP = 1.0f,

        };

        List<ChatMessage> messages = new List<ChatMessage>()
        {
            new SystemChatMessage("You are a helpful assistant."),
            new UserChatMessage(question)
        };
        var response = chatClient.CompleteChat(messages, requestOptions);

        return response.Value.Content[0].Text;
    }
}

public interface IChatService
{
    string Chat(string question);
}