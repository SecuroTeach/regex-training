using System.Text.RegularExpressions;

const string inputFilePath = "data.csv";
const string outputFilePath = "data-solution-automatic.csv";

List<PatternDefinition> matches =
[
    new PatternDefinition(@"^//.*", ""),
    new PatternDefinition("^(?!Id,)[A-Za-z]+", ""),
    new PatternDefinition(",[^,]*$", ""),
    new PatternDefinition(@"\.invalid[^@]*", ""),
    new PatternDefinition(@"^0+(?=\d)", ""),
    new PatternDefinition(@"(\d{2})\.(\d{2})\.(\d{4})", "$3-$2-$1"),
    new PatternDefinition(@"(\d{3}) (\d{3}) (\d{3})", "+420 $1$2$3"),
];

try
{
    using StreamReader reader = new StreamReader(inputFilePath);
    using StreamWriter writer = new StreamWriter(outputFilePath);
    string? line;

    while ((line = reader.ReadLine()) is not null)
    {
        foreach(var match in matches)
        {
            line = Regex.Replace(line, match.Pattern, match.Replacement);
        }

        if(string.IsNullOrEmpty(line))
        {
            continue;
        }

        writer.WriteLine(line);
    }
}
catch (Exception ex)
{
    Console.WriteLine($"An error occurred: {ex.Message}");
    return;
}

Console.WriteLine("Done.");
