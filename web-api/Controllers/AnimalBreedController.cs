using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using PetsShelterApi.AzureAi;

namespace PetsShelterApi.Controllers;

[Route("api/[controller]")]
public class AnimalBreedController(ICommentService commentService) : Controller
{
    [HttpPost]
    public IReadOnlyCollection<string>? ReturnTextAnalysis(string text) => commentService.CommentAnalysis(text);
}