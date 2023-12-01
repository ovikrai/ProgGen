using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ClientApp.Pages;

public class IndexModel : PageModel
{
    public string[] measure1 = { "C#", "F", "G", "C" };
    public string[] measure2 = { "C#", "G-7b5", "G", "C" };
    public string[] measure3 = { "C#", "F", "G", "C" };
    public string[] measure4 = { "C#", "F", "G", "C" };

    private readonly ILogger<IndexModel> _logger;

    public IndexModel(ILogger<IndexModel> logger)
    {
        _logger = logger;
    }

    public void OnGet()
    {
        //STEP 1: INIT ChatGPT

        //STEP 2: ON CLICK PARSE PROMPT

        //STEP 3: PARSE RESPONSE (INCLUDING A GENERATION ID)
        for (int i; i < 4,; i++){

        }

        //STEP 4: DISPLAY RESPONSE
    }

}
