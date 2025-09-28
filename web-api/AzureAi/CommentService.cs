using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Options;

namespace PetsShelterApi.AzureAi;

public class CommentService(IOptions<AzureAiServicesApiOptions> options) : ICommentService
{
    public IReadOnlyCollection<string> CommentAnalysis(string text)
    {
        var client = new TextAnalyticsClient(new Uri(options.Value.Endpoint),new AzureKeyCredential(options.Value.Key));

        DocumentSentiment sentiment = client.AnalyzeSentiment(text);
        var sentiments = string.Empty;
        foreach (var sentence in sentiment.Sentences)
        {
            sentiments += $"Sentence: {sentence.Text} => {sentence.Sentiment}\n";
        }
        
        DetectedLanguage detectedLanguage = client.DetectLanguage(text);
        
        return new List<string> { sentiments, $"Detected language: {detectedLanguage.Name} ISO: {detectedLanguage.Iso6391Name }" };
    }
}

public interface ICommentService
{
    IReadOnlyCollection<string> CommentAnalysis(string text);
}