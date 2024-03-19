namespace Foodies.Api.Business.Services.Interfaces
{
    public interface IFileService
    {


            public Tuple<int, string> SaveImage(IFormFile imageFile);

            public bool DeleteImage(string imageFileName);
        }
    }

