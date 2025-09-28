using Microsoft.AspNetCore.Mvc;

namespace PetsShelterApi.Controllers;

[Route("api/[controller]")]
public class AnimalBreedController : Controller
{
    [HttpGet]
    public IReadOnlyCollection<string>? GetBreed()
    {
        return new List<string> { "Cat", "Dog" };
        
    }
}