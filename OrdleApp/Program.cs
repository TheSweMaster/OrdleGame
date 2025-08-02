using OrdleApp.Components;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

var fiveLetterWords = Get5LetterWordsList();
builder.Services.AddSingleton(fiveLetterWords);

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