using DAL003;
using Microsoft.Extensions.FileProviders;

internal class Program
{
    private static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        var app = builder.Build();

        app.UseStaticFiles(new StaticFileOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Celebrities")),
            RequestPath = "/Celebrities/download",
            OnPrepareResponse = ctx =>
            {
                ctx.Context.Response.Headers.Add("Content-Disposition", new[] { "attachment" });
            }
        });

        app.UseDirectoryBrowser(new DirectoryBrowserOptions
        {
            FileProvider = new PhysicalFileProvider(Path.Combine(builder.Environment.ContentRootPath, "Celebrities")),
            RequestPath = "/Celebrities/download"
        });

        Repository.JSONFileName = "Celebrities.json";
        using (IRepository repository = Repository.Create("Celebrities"))
        {
            app.MapGet("/Celebrities", () => repository.GetAllCelebrities());
            app.MapGet("/Celebrities/{id:int}", (int id) => repository.GetCelebrityById(id));
            app.MapGet("/Celebrities/BySurname/{surname}", (string surname) => repository.GetCelebritiesBySurname(surname));
            app.MapGet("/Celebrities/PhotoPathById/{id:int}", (int id) => repository.GetPhotoPathById(id));
        }

        app.Run();
    }
}