using Firebase.Auth;
using Firebase.Storage;

namespace CologneStore.ImageService
{
    public class FileService : IFileService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        private static string ApiKey = "AIzaSyA816pW9OKpzEaFCfryvZIgiiVIy5G5z5c";
        private static string Bucket = "cologne-store.appspot.com";
        private static string AuthEmail = "company@gmail.com";
        private static string AuthPassword = "Company@123";

        public FileService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void DeleteFile(string fileName)
        {
            var wwwPath = _webHostEnvironment.WebRootPath;
            var fileNameWithPath = Path.Combine(wwwPath, "images\\", fileName);
            if (!File.Exists(fileNameWithPath))
                throw new FileNotFoundException(fileName);
            File.Delete(fileName);
        }

        public async Task<string> SaveFile(IFormFile file, string[] allowedExtensions)
        {
            var wwwPath = _webHostEnvironment.WebRootPath;
            var path = Path.Combine(wwwPath, "images");
            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            var extension = Path.GetExtension(file.FileName);
            if (!allowedExtensions.Contains(extension))
            {
                throw new InvalidOperationException($"Only {string.Join(",", allowedExtensions)} files are allowed");
            }

			string fileName = $"{Guid.NewGuid()}{extension}";
			string fileNameWithPath = Path.Combine(path, fileName);
            var stream = new FileStream(fileNameWithPath, FileMode.Create);

           


            var auth = new FirebaseAuthProvider(new FirebaseConfig(ApiKey));
            var a = await auth.SignInWithEmailAndPasswordAsync(AuthEmail, AuthPassword);

            var cancellation = new CancellationTokenSource();

            var FbStream = file.OpenReadStream();

            var task = new FirebaseStorage(
                Bucket,
                new FirebaseStorageOptions
                {
                    AuthTokenAsyncFactory = () => Task.FromResult(a.FirebaseToken),
                    ThrowOnCancel = true
                }).Child("images").Child(fileName).PutAsync(FbStream);

           
            string link = await task;
            
            
            



            await file.CopyToAsync(stream);

           

            return link; 
        }
    }
}
