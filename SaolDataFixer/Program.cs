using System.IO;
using System.Text.RegularExpressions;

// GetAndSave5LetterWordsFromSaolFile();

// Get5LetterWordFromMatchiTranslations();


var fiveLetterWords = Get5LetterWordsList();
Console.WriteLine(fiveLetterWords.Contains("SKAPA"));

return;

HashSet<string> Get5LetterWordsList()
{
    // Path to the cleaned file
    var dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
    var saolOutputPath = Path.Combine(dataFolder, "SAOL_5Words_Clean.txt");

    if (!File.Exists(saolOutputPath))
    {
        return new HashSet<string>();
    }

    var words = File.ReadAllLines(saolOutputPath)
        .Select(w => w.Trim().ToUpper())
        .ToHashSet();

    return words;
}

void GetAndSave5LetterWordsFromSaolFile()
{
    // Path to the input file
    const string saolInputFile = "SAOL13_AND_14.txt";

    // Path to the input folder
    var dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");

    var saolInputPath = Path.Combine(dataFolder, saolInputFile);

    // Read all lines from the file
    var allWords = File.ReadAllLines(saolInputPath);

    // Regex for 5 letters, only A-Z, Å, Ä, Ö (case-insensitive)
    var regex = new Regex(@"^[A-ZÅÄÖ]{5}$", RegexOptions.IgnoreCase);

    // Filter words
    var filteredWords = allWords
        .Select(word => word.Trim())
        .Where(word => regex.IsMatch(word))
        .Distinct()
        .ToList();

    // Save filtered words to a new file
    const string outputPath = "SAOL_5Words_Clean.txt";
    var saolOutputPath = Path.Combine(dataFolder, outputPath);
    File.WriteAllLines(saolOutputPath, filteredWords);
}

void Get5LetterWordFromMatchiTranslations()
{
    var dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
    var translationOutputPath = Path.Combine(dataFolder, "MatchiTranslations.txt");
    var lines = File.ReadAllLines(translationOutputPath);
    var fiveLetterWordsNew = new HashSet<string>(StringComparer.OrdinalIgnoreCase);
    var wordPattern = new Regex(@"\b[A-Za-z]{5}\b");

    foreach (var line in lines)
    {
        var parts = line.Split('=', 2);
        if (parts.Length < 2) continue;

        var valuePart = parts[1];

        foreach (Match match in wordPattern.Matches(valuePart))
        {
            fiveLetterWordsNew.Add(match.Value);
        }
    }

    var sortedWords = fiveLetterWordsNew
        .OrderBy(word => word, StringComparer.OrdinalIgnoreCase);

    Console.WriteLine($"Filtered 5-letter words: {sortedWords.Count()}");
    foreach (var word in sortedWords)
    {
        Console.WriteLine(word);
    }
}