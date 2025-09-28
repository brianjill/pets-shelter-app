namespace PetsShelterApi.Utilities;

public static class EnvironmentExtensions
{
    public static bool IsLocal(this IHostEnvironment hostEnvironment) =>
        hostEnvironment.IsEnvironment("Local");
}