using DAL003;

internal class Program
{
    static void Main(string[] args)
    {
        Repository.JSONFileName = "Celebrities.json";
        using (IRepository repository = new Repository("D:\\6sem\\PIS\\3\\ASPA\\Test_DAL003\\Celebrities"))
        {
            foreach (Celebrity celebrity in repository.GetAllCelebrities())
            {
                Console.WriteLine($"Id = {celebrity.Id}, Firstname = {celebrity.Firstname}, " +
                                  $"Surname = {celebrity.Surname}, PhotoPath = {celebrity.PhotoPath} ");
            }

            Celebrity? celebrity1 = repository.GetCelebrityById(1);
            if (celebrity1 != null)
            {
                Console.WriteLine($"Id = {celebrity1.Id}, Firstname = {celebrity1.Firstname}, " +
                                  $"Surname = {celebrity1.Surname}, PhotoPath = {celebrity1.PhotoPath}");
            }

            Celebrity? celebrity3 = repository.GetCelebrityById(3);
            if (celebrity3 != null)
            {
                Console.WriteLine($"Id = {celebrity3.Id}, Firstname = {celebrity3.Firstname}, " +
                                  $"Surname = {celebrity3.Surname}, PhotoPath = {celebrity3.PhotoPath}");
            }

            Celebrity? celebrity222 = repository.GetCelebrityById(222);
            if (celebrity222 != null)
            {
                Console.WriteLine($"Id = {celebrity222.Id}, Firstname = {celebrity222.Firstname}, " +
                                  $"Surname = {celebrity222.Surname}, PhotoPath = {celebrity222.PhotoPath}");
            }
            else Console.WriteLine("Not Found 2222");

            foreach (Celebrity celebrity in repository.GetCelebritiesBySurname("Chomsky"))
            {
                Console.WriteLine($"Id = {celebrity.Id}, Firstname = {celebrity.Firstname}, " +
                                  $"Surname = {celebrity.Surname}, PhotoPath = {celebrity.PhotoPath} ");
            }

            foreach (Celebrity celebrity in repository.GetCelebritiesBySurname("Knuth"))
            {
                Console.WriteLine($"Id = {celebrity.Id}, Firstname = {celebrity.Firstname}, " +
                                  $"Surname = {celebrity.Surname}, PhotoPath = {celebrity.PhotoPath} ");
            }

            foreach (Celebrity celebrity in repository.GetCelebritiesBySurname("XXXX"))
            {
                Console.WriteLine($"Id = {celebrity.Id}, Firstname = {celebrity.Firstname}, " +
                                  $"Surname = {celebrity.Surname}, PhotoPath = {celebrity.PhotoPath} ");
            }

            Console.WriteLine($"PhotoPathById(4) = {repository.GetPhotoPathById(4)}");
            Console.WriteLine($"PhotoPathById(6) = {repository.GetPhotoPathById(6)}");
            Console.WriteLine($"PhotoPathById(222) = {repository.GetPhotoPathById(222)}");
        }
    }
}
