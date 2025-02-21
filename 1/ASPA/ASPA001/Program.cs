using Microsoft.AspNetCore.HttpLogging;
using Microsoft.Extensions.Logging;

internal class Program // ���������� ����� program
{
    private static void Main(string[] args) // ����� ����� � ���������
    {
        var builder = WebApplication.CreateBuilder(args); // ������ ������ builder ������ WebApplication ��� ������ CreateBuilder ��� ������������ ����������

        builder.Services.AddHttpLogging(o => {
            o.LoggingFields = HttpLoggingFields.RequestMethod | // Request �����
            HttpLoggingFields.RequestPath | // Request uri
            HttpLoggingFields.ResponseStatusCode | // Response status
            HttpLoggingFields.ResponseBody; // Response ����
        }
        );

        builder.Logging.AddFilter("Microsoft.AstNetCore.HttpLogging", LogLevel.Information); // ������ ���������
        
        var app = builder.Build(); // ������ ������ ����������

        app.UseHttpLogging();

        app.MapGet("/", () => "�� ������ ASPA!"); // ������ ���� / � ����� ������ ����� ��� ������ ��� ����

        app.Run(); // ��������� ����������
    }
}