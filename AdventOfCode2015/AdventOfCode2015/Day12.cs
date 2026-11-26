using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization.Metadata;

namespace AdventOfCode2015
{
    public class Day12 : IDay
    {
        // Just convert the string into json objects and check the type
        public void PartOne(string inputPath)
        {
            JsonNode[] nodes = File.ReadLines(inputPath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => JsonNode.Parse(line))
                .OfType<JsonNode>() // drops literal nulls
                .ToArray();

            long GetEveryNumberFromJsonNode(JsonNode? node)
            {
                long result = 0;
                switch (node)
                {
                    case JsonObject obj:
                        foreach (KeyValuePair<string, JsonNode?> property in obj)
                            result += GetEveryNumberFromJsonNode(property.Value);
                        break;
                    case JsonArray arr:
                        foreach (var item in arr)
                        {
                            result += GetEveryNumberFromJsonNode(item);
                        }
                        break;
                    case JsonValue val:
                        result += val.TryGetValue(out int intValue) ? intValue : 0;
                        break;
                    default:
                        break;
                }

                return result;
            }

            long solution = 0;
            foreach (JsonNode node in nodes)
                solution += GetEveryNumberFromJsonNode(node);

            // The test should be 18
            Console.WriteLine($"The sum of all numbers is: {solution}.");
        }

        public void PartTwo(string inputPath)
        {
            JsonNode[] nodes = File.ReadLines(inputPath)
                .Where(line => !string.IsNullOrWhiteSpace(line))
                .Select(line => JsonNode.Parse(line))
                .OfType<JsonNode>() // drops literal nulls
                .ToArray();

            long GetEveryNumberFromJsonNode(JsonNode? node)
            {
                long result = 0;
                switch (node)
                {
                    case JsonObject obj:
                        if (obj.AsEnumerable().Any(x => x.Value is JsonValue checkValue && checkValue.TryGetValue(out string? checkString) && checkString == "red"))
                            break;
                        foreach (KeyValuePair<string, JsonNode?> property in obj)
                            result += GetEveryNumberFromJsonNode(property.Value);
                        break;
                    case JsonArray arr:
                        foreach (var item in arr)
                        {
                            result += GetEveryNumberFromJsonNode(item);
                        }
                        break;
                    case JsonValue val:
                        result += val.TryGetValue(out int intValue) ? intValue : 0;
                        break;
                    default:
                        break;
                }

                return result;
            }

            long solution = 0;
            foreach (JsonNode node in nodes)
                solution += GetEveryNumberFromJsonNode(node);

            // The test should be 12
            Console.WriteLine($"The sum of all numbers (without objects that have a value of \"red\") is: {solution}.");
        }
    }
}
