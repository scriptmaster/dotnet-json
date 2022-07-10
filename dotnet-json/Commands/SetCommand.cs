using System;
using System.CommandLine;
using System.CommandLine.Invocation;
using System.Threading.Tasks;
using dotnet_json.Core;

namespace dotnet_json.Commands
{
    public class SetCommand : CommandBase, ICommandHandler
    {
        private Argument<string> Key = new Argument<string>("key", "The key to set (use ':' to set nested object and use index numbers to set array values eg. nested:key or nested:1:key)") { Arity = ArgumentArity.ExactlyOne };
        private Argument<string> Value = new Argument<string>(name: "value", getDefaultValue: () => "", description: "The value to set") { Arity = ArgumentArity.ZeroOrOne };

        public SetCommand() : base("set", "Set a value in a json file")
        {
            AddArgument(Key);
            AddArgument(Value);

            Handler = this;
        }

        protected override async Task<int> ExecuteAsync()
        {
            var key = GetParameterValue(Key) ?? throw new ArgumentException("Missing argument <key>");
            var value = GetParameterValue(Value) ?? string.Empty; // throw new ArgumentException("Missing argument <value>");

            JsonDocument document;

            await using (var inputStream = GetInputStream())
                document = JsonDocument.ReadFromStream(inputStream);

            document[key] = value;

            await using (var outputStream = GetOutputStream())
                document.WriteToStream(outputStream, GetFormatting());

            return 0;
        }
    }
}
