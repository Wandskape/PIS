using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using Newtonsoft.Json;

namespace DAL003
{
    public interface IRepository:IDisposable
    {
        string BasePath { get; }
        Celebrity[] GetAllCelebrities();
        Celebrity? GetCelebrityById(int id);
        Celebrity[] GetCelebritiesBySurname(string surname);
        string? GetPhotoPathById(int id);
    }

    public record Celebrity(int Id, string Firstname, string Surname, string PhotoPath);
}
