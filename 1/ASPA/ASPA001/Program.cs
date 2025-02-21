using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Logging;

internal class Program // объявление класс program
{
    private static void Main(string[] args) // точка входа в программу
    {
        var builder = WebApplication.CreateBuilder(args); // создаём объект builder класса WebApplication при помощи CreateBuilder для конфигурации приложения

        builder.Services.AddHttpLogging(o => {
            o.LoggingFields = HttpLoggingFields.RequestMethod | // Request метод
            HttpLoggingFields.RequestPath | // Request uri
            HttpLoggingFields.ResponseStatusCode | // Response status
            HttpLoggingFields.ResponseBody; // Response тело
        }
        );

        builder.Logging.AddFilter("Microsoft.AstNetCore.HttpLogging", LogLevel.Information); // фильтр сообщений
        
        var app = builder.Build(); // создаём объект приложения

        app.UseHttpLogging();

        app.MapGet("/", () => "Моё первое ASPA!"); // роутим путь / и через колбек пишем что должно там быть

        app.Run(); // запускаем приложение
    }
}