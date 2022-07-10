﻿using System.CommandLine;
using System.Threading.Tasks;
using dotnet_json.Commands;

namespace dotnet_json
{
    public class Program
    {
        public static async Task<int> Main(string[] args)
        {
            var command = CreateRootCommand();
            return await command.InvokeAsync(args);
        }

        internal static RootCommand CreateRootCommand()
        {
            var root = new RootCommand("dotnet-json: A cicd tool");

            root.AddCommand(new MergeCommand());
            root.AddCommand(new SetCommand());
            root.AddCommand(new RemoveCommand());
            root.AddCommand(new GetCommand());
            root.AddCommand(new IndentCommand());

            return root;
        }
    }
}
