using Azure;
using Azure.AI.TextAnalytics;
using Microsoft.Extensions.Options;

namespace PetsShelterApi.AzureAi;

public class TextAnalyticsProvider : ITextAnalyticsProvider
{
    private TextAnalyticsClient _client;

    public TextAnalyticsProvider(IOptions<AzureAiHubOptions> options)
    {
        _client = new TextAnalyticsClient(new Uri(options.Value.Endpoint), new AzureKeyCredential(options.Value.Key)); ;
    }

    public string GetSentiment(string text, string languageCode)
    {
        if (_client == null) throw new Exception("client is null");
        // Enable opinion mining
        var options = new AnalyzeSentimentOptions() { IncludeOpinionMining = true };
        DocumentSentiment sentiment = _client.AnalyzeSentiment(text, languageCode, options);
        var sentiments = string.Empty;
        foreach (var sentence in sentiment.Sentences)
        {
            foreach (var minedOpinion in sentence.Opinions)
            {
                foreach (var assessment in minedOpinion.Assessments)
                {
                    sentiments += $"Assessment: {assessment.Text}, Sentiment: {assessment.Sentiment} ";
                }
            }
        }

        return sentiments;
    }

    public string GetKeyPhraseExtraction(string text)
    {
        if (_client == null) throw new Exception("client is null");

        KeyPhraseCollection keyPhrases = _client.ExtractKeyPhrases(text);

        return string.Join(",", keyPhrases);
    }

    public string GetLanguage(string text)
    {
        if (_client == null) throw new Exception("client is null");
        DetectedLanguage detectedLanguage = _client.DetectLanguage(text);

        return $"Detected language: {detectedLanguage.Name} ISO: {detectedLanguage.Iso6391Name}";
    }

    public string GetNamedEntityRecognition(string text)
    {
        var result = string.Empty;
        if (_client == null) throw new Exception("client is null");
        var recognizeEntities = _client.RecognizeEntities(text);
        foreach (var value in recognizeEntities.Value)
        {
            result += $"{value.Text} {value.Category} \n";
        }
        return result;
    }
}

public interface ITextAnalyticsProvider
{
    string GetSentiment(string text, string languageCode);
    string GetKeyPhraseExtraction(string text);
    string GetLanguage(string text);
    string GetNamedEntityRecognition(string text);
}