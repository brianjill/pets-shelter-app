namespace PetsShelterApi.AzureAi;

public class AzureAiServicesApiOptions
{
    public const string AzureAiServicesApi = nameof(AzureAiServicesApi);
    public new required string Endpoint { get; set; }
    public new required string Key { get; set; }
};