using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Options;

namespace PetsShelterApi.AzureAi;

public class CommentService : ICommentService
{
    private TextAnalyticsClient _client;

    public CommentService(IOptions<AzureAiServicesApiOptions> options)
    {
        _client = new TextAnalyticsClient(new Uri(options.Value.Endpoint), new AzureKeyCredential(options.Value.Key));
    }


    public IReadOnlyCollection<string> CommentAnalysis(string comment, LanguageServiceOptions languageServiceOptions)
    {
        if (_client == null) throw new Exception("client is null");
        DetectedLanguage detectedLanguage = _client.DetectLanguage(comment);

        var textAnalysis = languageServiceOptions switch
        {
            LanguageServiceOptions.Sentiment => GetSentiment(comment),
            LanguageServiceOptions.KeyPhraseExtraction => GetKeyPhraseExtraction(comment),
            _ => "Unknown service"
        };
        return new List<string> { textAnalysis, $"Detected language: {detectedLanguage.Name} ISO: {detectedLanguage.Iso6391Name}" };
    }

    private string GetSentiment(string text)
    {
        if (_client == null) throw new Exception("client is null");
        DocumentSentiment sentiment = _client.AnalyzeSentiment(text);

        return sentiment.Sentences.Aggregate(String.Empty, (current, sentence) => current + $"Sentence: {sentence.Text} => {sentence.Sentiment}\n");
    }

    private string GetKeyPhraseExtraction(string text)
    {
        if (_client == null) throw new Exception("client is null");

        KeyPhraseCollection keyPhrases = _client.ExtractKeyPhrases(text);

        return string.Join(",", keyPhrases);
    }
}

public interface ICommentService
{
    IReadOnlyCollection<string> CommentAnalysis(string comment, LanguageServiceOptions languageServiceOptions);
}