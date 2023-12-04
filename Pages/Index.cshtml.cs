using Azure;
using Azure.AI.OpenAI;
using Azure.Core;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Numerics;
using System.Threading;
using Pinagen.Pages.Shared;
using System.Diagnostics.Metrics;
using System.Runtime.CompilerServices;

namespace Pinagen.Pages
{
    public class IndexModel : PageModel
    {
        public const int NUMBER_OF_MEASURES = 4;
        public List<string[]> measures = new List<string[]>();

        public OpenAIClient openAIClient;
        public ChatCompletionsOptions completionsOptions;

        public string? keySignature = "C Major";
        public string? reHarmonization = "None";
        public string? prompt;
        public bool isPromptError;

        public string[]? chatResponse = { };

        // ON GET 
        public void OnGet() {}
        
        // ON POST
        public async Task OnPost()
        {
            //INIT ChatGPT
            Uri proxyUri = new(ProxySettings.PROXY_URL + "/v1/api");

            AzureKeyCredential token = new(ProxySettings.KEY + ProxySettings.GITHUB_USER);
            openAIClient = new(proxyUri, token);

            completionsOptions = new()
            {
                MaxTokens = 2048,
                Temperature = 0.7f,
                NucleusSamplingFactor = 0.95f,
                FrequencyPenalty = 0.5f,
                PresencePenalty = 0.5f,
                DeploymentName = "gpt-35-turbo"
            };

            // READ FORM DATA AND PHARSE THE PROMPT
            keySignature = Request.Form["keySignature"];
            reHarmonization = Request.Form["reHarmonization"];

            // PROMT CONSTRUCTION            
            prompt = $"Create ONLY a chord progression in {keySignature}, " + 
                               "with 4 measure and" +
                               "with 4 chord per measure and" +
                               $"with {reHarmonization} Reharmonization technic.";

            // GET RESPONSE FROM THE OPEN AI SERVICE
            completionsOptions.Messages.Add(new(ChatRole.User, prompt));
            Response<ChatCompletions> completions = await openAIClient.GetChatCompletionsAsync(completionsOptions);
            
            // PARSE RESPONSE GENERATION
            parseChatResponse(completions.Value.Choices[0].Message.Content);
            Console.WriteLine(measures.Count);
        }

        private void parseChatResponse(string chatResponse)
        {
            string[] chat = chatResponse.Split("\n\n");
            string[] measure = [];

            try
            {
                string delimiterString = " - ";
                for (int i = 0; i < NUMBER_OF_MEASURES; i++)
                { 
                    measure = chat[i + 1].Split("\n");
                    measure = measure[1].Split(delimiterString);
                    measures.Add(measure);               
                }
            } 
            catch (Exception ex) 
            {
                Console.WriteLine(ex.Message);
                isPromptError = true;
            }
        }

    }
}


