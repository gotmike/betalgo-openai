﻿using OpenAI.SDK.Interfaces;

namespace OpenAI.Playground
{
    internal static class EngineTestHelper
    {
        public static async Task FetchEnginesTest(IOpenAISdk sdk)
        {
            ConsoleExtensions.WriteLine("Engine List Testing is starting:", ConsoleColor.Cyan);

            try
            {
                ConsoleExtensions.WriteLine("Fetching Engine List", ConsoleColor.DarkCyan);
                var engineList = await sdk.Engine.ListEngines();
                if (engineList == null)
                {
                    ConsoleExtensions.WriteLine("Fetching Engine List failed", ConsoleColor.DarkRed);
                    throw new NullReferenceException(nameof(engineList));
                }

                ConsoleExtensions.WriteLine("Engines:", ConsoleColor.DarkGreen);
                Console.WriteLine(string.Join(Environment.NewLine, engineList.Engines.Select(r => r.Id)));

                foreach (var engineItem in engineList.Engines)
                {
                    ConsoleExtensions.WriteLine($"Retrieving Engine:{engineItem.Id}", ConsoleColor.DarkCyan);

                    var retrieveEngineResponse = await sdk.Engine.RetrieveEngine(engineItem.Id);
                    if (retrieveEngineResponse.Successful)
                    {
                        Console.WriteLine(retrieveEngineResponse);
                    }
                    else
                    {
                        ConsoleExtensions.WriteLine($"Retrieving {engineItem.Id} Engine failed", ConsoleColor.DarkRed);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }
    }
}