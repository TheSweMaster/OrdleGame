using OrdleApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// var fiveLetterWords = Get5LetterWordsList("SAOL_5Words_Clean.txt");
// builder.Services.AddSingleton(fiveLetterWords);
//
// var matchiFiveLetterWords = Get5LetterWordsList("Matchi5LetterWords.txt");
// builder.Services.AddSingleton(matchiFiveLetterWords);

builder.Services.AddSingleton(new SaolWords(Get5LetterWordsList("SAOL_5Words_Clean.txt")));
builder.Services.AddSingleton(new MatchiWords(Get5LetterWordsList("Matchi5LetterWords.txt")));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();
app.UseAntiforgery();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();


HashSet<string> Get5LetterWordsList(string fileName)
{
    // Path to the cleaned file
    var dataFolder = Path.Combine(Directory.GetCurrentDirectory(), "Data");
    var saolOutputPath = Path.Combine(dataFolder, fileName);

    if (!File.Exists(saolOutputPath))
    {
        return new HashSet<string>();
    }

    var words = File.ReadAllLines(saolOutputPath)
        .Select(w => w.Trim().ToUpper())
        .ToHashSet();

    return words;
}

public class SaolWords
{
    public HashSet<string> Words { get; }
    public SaolWords(HashSet<string> words) => Words = words;
}

public class MatchiWords
{
    public HashSet<string> Words { get; }
    public MatchiWords(HashSet<string> words) => Words = words;
}