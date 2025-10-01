namespace PetsShelterApi.AzureAi;

public class AzureAiChatOptions : BaseOptions
{
    public const string AzureAiChatApi = nameof(AzureAiChatApi);
    public new required string DeploymentName { get; set; }

}