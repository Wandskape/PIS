using DAL004;

namespace Validation
{
    public class SurnameFilter : IEndpointFilter
    {
        private readonly IRepository repository;

        public SurnameFilter(IRepository repository)
        {
            this.repository = repository;
        }

        public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            Celebrity? celebrity = context.GetArgument<Celebrity>(0);
            if (celebrity == null) throw new NullCelebrityException("Value: POST /Celebrities error, Celebrity is wrong");
            if (celebrity.Surname == null || celebrity.Surname.Length <= 1) throw new SurnameValueExeption("Value: POST /Celebrities error, Surname is wrong");

            string? duplicateSurname = repository.GetAllCelebrities()
                                                  .Where(c => c.Surname.ToLower() == celebrity.Surname.ToLower())
                                                  .Select(c => c.Surname)
                                                  .FirstOrDefault();
            if (duplicateSurname != null) throw new DoublicateCelebritySurnameException("Value: POST /Celebrities error, Surname is doubled");

            return await next(context);
        }
    }

    public class PhotoExistFilter : IEndpointFilter
    {
        private readonly IRepository repository;

        public PhotoExistFilter(IRepository repository)
        {
            this.repository = repository;
        }

        public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            Celebrity? celebrity = context.GetArgument<Celebrity>(0);
            if (celebrity == null) throw new NullCelebrityException("Value: POST /Celebrities error, Celebrity is wrong");

            string filePath = Path.Combine(repository.BasePath, Path.GetFileName(celebrity.PhotoPath));
            if (!File.Exists(filePath))
            {
                context.HttpContext.Response.Headers["X-Celebrity"] = "NotFound";
                context.HttpContext.Response.Headers["X-Celebrity-Path"] = Path.GetFileName(celebrity.PhotoPath);
                return Results.NotFound();
            }

            return await next(context);
        }
    }

    public class CheckCelebrityExistsFilter : IEndpointFilter
    {
        private readonly IRepository repository;

        public CheckCelebrityExistsFilter(IRepository repository)
        {
            this.repository = repository;
        }

        public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            int id = context.GetArgument<int>(0);
            Celebrity celebrity = repository.GetCelebrityById(id);

            if (celebrity == null)
            {
                var responseMessage = new { Message = $"Celebrity with Id = {id} not found" };
                return Results.NotFound(responseMessage);
            }

            return await next(context);
        }
    }

    public class NullFieldFilter : IEndpointFilter
    {
        private readonly IRepository repository;

        public NullFieldFilter(IRepository repository)
        {
            this.repository = repository;
        }

        public async ValueTask<object> InvokeAsync(EndpointFilterInvocationContext context, EndpointFilterDelegate next)
        {
            Celebrity? celebrity = context.GetArgument<Celebrity>(0);
            if (celebrity.Surname == null || celebrity.PhotoPath == null || celebrity.Firstname == null) throw new NullFieldExeption("Value: POST /Celebrities error, fields is wrong");

            string? duplicateSurname = repository.GetAllCelebrities()
                                                  .Where(c => c.Surname.ToLower() == celebrity.Surname.ToLower())
                                                  .Select(c => c.Surname)
                                                  .FirstOrDefault();
            return await next(context);
        }
    }
}
