namespace SocialMedia.Api.Hellper
{
    public class AddFile
    {
        public static string UploadFile(IFormFile? file, string folderName)
        {
            if (file != null)
            {
                string FolderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", folderName);

                string FileName = $"{Guid.NewGuid()}{file.FileName}";

                string filePath = Path.Combine(FolderPath, FileName);

                using var fs = new FileStream(filePath, FileMode.Create);

                file.CopyTo(fs);

                return "wwwroot/" + folderName + "/" + FileName;
            }
            return null;
        }

        // this class to delet File
        public static string DeleteFile(string localpath, string FileName)
        {

            try
            {
                if (System.IO.File.Exists(Directory.GetCurrentDirectory() + localpath + FileName))
                {
                    System.IO.File.Delete(Directory.GetCurrentDirectory() + localpath + FileName);
                }
                var result = "Deleted";
                return result;
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }
    }
}
