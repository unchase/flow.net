﻿using Flow.Net.Sdk;
using Flow.Net.Sdk.Models;
using System;
using System.Threading.Tasks;

namespace Flow.Net.Examples.BlockExamples
{
    public class BlockExample : ExampleBase
    {
        public static async Task RunAsync()
        {
            Console.WriteLine("\nRunning BlockExample\n");
            await CreateFlowClientAsync();
            await Demo();
            Console.WriteLine("\nBlockExample Complete\n");
        }

        private static async Task Demo()
        {
            // get the latest sealed block
            var latestBlock = await FlowClient.GetLatestBlockAsync();
            PrintResult(latestBlock);

            // get the block by ID
            var blockByIdResult = await FlowClient.GetBlockByIdAsync(latestBlock.Id);
            PrintResult(blockByIdResult);

            // get block by height
            var blockByHeightResult = await FlowClient.GetBlockByHeightAsync(latestBlock.Height);
            PrintResult(blockByHeightResult);
        }

        private static void PrintResult(FlowBlock flowBlock)
        {
            Console.WriteLine($"ID: {flowBlock.Id.FromByteStringToHex()}");
            Console.WriteLine($"height: {flowBlock.Height}");
            Console.WriteLine($"timestamp: {flowBlock.Timestamp}\n");            
        }
    }
}
