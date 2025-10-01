using PetsShelterApi.AzureAi;

namespace PetsShelterApi.Controllers.Model;

public record AnalyseCommentRequestDto(string Comment, LanguageServiceOptions LanguageServiceOption);