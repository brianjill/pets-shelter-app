using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Options;

namespace PetsShelterApi.AzureAi;

public class CommentService : ICommentService
{
    private readonly ITextAnalyticsProvider _textAnalyticsProvider;

    public CommentService(ITextAnalyticsProvider textAnalyticsProvider)
    {
        _textAnalyticsProvider = textAnalyticsProvider;
    }


    public IReadOnlyCollection<string> CommentAnalysis(string comment, LanguageServiceOptions languageServiceOptions)
    {
        var language = _textAnalyticsProvider.GetLanguage(comment);
        var languageCode = language.Split(["ISO:"], StringSplitOptions.None).LastOrDefault()?.Trim() ?? "";
        var textAnalysis = languageServiceOptions switch
        {
            LanguageServiceOptions.Sentiment => _textAnalyticsProvider.GetSentiment(comment, languageCode),
            LanguageServiceOptions.KeyPhraseExtraction => _textAnalyticsProvider.GetKeyPhraseExtraction(comment),
            LanguageServiceOptions.NamedEntityRecognition => _textAnalyticsProvider.GetNamedEntityRecognition(comment),
            _ => "Unknown service"
        };
        return new List<string> { textAnalysis, language };
    }


}

public interface ICommentService
{
    IReadOnlyCollection<string> CommentAnalysis(string comment, LanguageServiceOptions languageServiceOptions);
}