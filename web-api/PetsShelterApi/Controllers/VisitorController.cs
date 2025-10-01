using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PetsShelterApi.AzureAi;
using PetsShelterApi.Controllers.Model;

namespace PetsShelterApi.Controllers;

[Route("api/[controller]")]
public class VisitorController(ICommentService commentService, IChatService chatService) : Controller
{
    [HttpPost]
    [Route("analyse-comment")]
    public IReadOnlyCollection<string>? AnalyseComment([FromBody] AnalyseCommentRequestDto analyseRequest) => commentService.CommentAnalysis(analyseRequest.Comment, analyseRequest.LanguageServiceOption);

    [HttpPost]
    [Route("chat")]
    public string Chat([FromBody] ChatRequestDto chatRequest) => chatService.Chat(chatRequest.Question);
}