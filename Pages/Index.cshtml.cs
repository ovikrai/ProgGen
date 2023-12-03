using Azure;
using Azure.AI.OpenAI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Numerics;
using System.Threading;



namespace Pinagen.Pages
{
    public class IndexModel : PageModel
    {
        public const int NUMBER_OF_MEASURES = 4;
        public string[,] staff = {
            { "X", "X", "X", "X"  },
            { "X", "X", "X", "X"  },
            { "X", "X", "X", "X"  },
            { "X", "X", "X", "X"  }
        };

        public OpenAIClient openAIClient;
        public ChatCompletionsOptions completionsOptions;

        // TODO: PROMPT CONSTRUCTION WITH FORM INPUT DATA
        public string prompt = "Create a chord progression in C minor, " +
                               "with 4 measure and " +
                               "with 4 chord per measure";

        public string[]? chatResponse = { };

   
        public void OnGet()
        {

            //STEP 2: ON CLICK PARSE PROMPT

            //STEP 3: PARSE RESPONSE (INCLUDING A GENERATION ID)

            //STEP 4: DISPLAY RESPONSE
        }

        public async Task OnPost()
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
            
            // GET RESPONSE TO RENDER VARIABLE
            parseChatResponse(completions.Value.Choices[0].Message.Content);
        }
        
        // TODO: PARSE RESPONSE TO ARRAY OF CHORDS
        private async void parseChatResponse(string chatResponse)
        {
            Console.WriteLine(chatResponse);

            string[] chords = chatResponse.Split("\n\n");

            string[] measure1;
            string[] measure2;
            string[] measure3;
            string[] measure4;
                        
            measure1 = chords[1].Split("\n");
            measure2 = chords[2].Split("\n");
            measure3 = chords[3].Split("\n");
            measure4 = chords[4].Split("\n");

            measure1 = measure1[1].Split(" - ");
            measure2 = measure2[1].Split(" - ");
            measure3 = measure3[1].Split(" - ");
            measure4 = measure4[1].Split(" - ");
             
            for (int i = 0; i < NUMBER_OF_MEASURES; i++)
            {
                switch (i)
                {
                    case 0:
                        staff[i, 0] = measure1[0];
                        staff[i, 1] = measure1[1];
                        staff[i, 2] = measure1[2];
                        staff[i, 3] = measure1[3];
                        break;


                    case 1:
                        staff[i, 0] = measure2[0];
                        staff[i, 1] = measure2[1];
                        staff[i, 2] = measure2[2];
                        staff[i, 3] = measure2[3];
                        break;


                    case 2:
                        staff[i, 0] = measure3[0];
                        staff[i, 1] = measure3[1];
                        staff[i, 2] = measure3[2];
                        staff[i, 3] = measure3[3];
                        break;

                    case 3:
                        staff[i, 0] = measure4[0];
                        staff[i, 1] = measure4[1];
                        staff[i, 2] = measure4[2];
                        staff[i, 3] = measure4[3];
                        break;
                }

            }


        }

    }
}


