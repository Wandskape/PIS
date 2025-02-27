using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace DAL003
{
    public class Repository : IRepository
    {
        public string BasePath { get; }
        public static string JSONFileName { get; set; } = "/Celebrities/Celebrities.json";
        private Celebrity[] _celebrities;

        public Repository(string basePath)
        {
            BasePath = basePath;
            var jsonFilePath = Path.Combine(BasePath, JSONFileName);
            if (File.Exists(jsonFilePath))
            {
                var jsonData = File.ReadAllText(jsonFilePath);
                _celebrities = JsonConvert.DeserializeObject<Celebrity[]>(jsonData);
            }
            else
            {
                _celebrities = Array.Empty<Celebrity>();
            }
        }

        public Celebrity[] GetAllCelebrities() => _celebrities;

        public Celebrity? GetCelebrityById(int id) => _celebrities.FirstOrDefault(c => c.Id == id);

        public Celebrity[] GetCelebritiesBySurname(string surname) => _celebrities.Where(c => c.Surname.Equals(surname, StringComparison.OrdinalIgnoreCase)).ToArray();

        public string? GetPhotoPathById(int id) => _celebrities.FirstOrDefault(c => c.Id == id)?.PhotoPath;

        public static IRepository Create(string basePath)
        {
            return new Repository(basePath);
        }

        public void Dispose() { }
    }
}
