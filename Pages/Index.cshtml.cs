using Azure;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;



namespace Pinagen.Pages
{
    public class IndexModel : PageModel
    {
        public string[,] staff = {
            { "C", "B", "C", "D"  },
            { "C", "B", "C", "D"  },
            { "C", "B", "C", "R"  },
            { "C", "B", "C", "D"  },
        };

        public OpenAIClient openAIClient;
        public ChatCompletionsOptions completionsOptions;
        public string prompt = "Compose a song in C minor, " +
                            "with 4 measure and 4 chord per measure";

        public string testResponse = "";

   
        public void OnGet()
        {




            //STEP 2: ON CLICK PARSE PROMPT

            //STEP 3: PARSE RESPONSE (INCLUDING A GENERATION ID)

            //STEP 4: DISPLAY RESPONSE
        }

        public async void OnPost()
        {
            //STEP 1: INIT ChatGPT
            Console.WriteLine(this.staff.Length / 4);
            //STEP 1: INIT ChatGPT
            string proxyUrl = "https://aoai.hacktogether.net";
            string key = "95f23aba-f82c-47cd-91a9-7bdff141082b";

            Uri proxyUri = new(proxyUrl + "/v1/api");

            AzureKeyCredential token = new(key + "/ovikrai");
            openAIClient = new(proxyUri, token);

            // PROMT CONSTRUCTION            
            completionsOptions = new()
            {
                MaxTokens = 2048,
                Temperature = 0.7f,
                NucleusSamplingFactor = 0.95f,
                FrequencyPenalty = 0.5f,
                PresencePenalty = 0.5f,
                DeploymentName = "gpt-35-turbo"
            };
            // TEST SERVICE AND EXAMPLE PROMPT
            // TODO: CHECK REFERENCE
            completionsOptions.Messages.Add(new(ChatRole.User, prompt));
            Response<ChatCompletions> completions = await openAIClient.GetChatCompletionsAsync(completionsOptions);
            testResponse = completions.Value.Choices[0].Message.Content;
            Console.WriteLine(testResponse);
        }

    }
}


