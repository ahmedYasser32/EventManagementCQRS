
using Microsoft.AspNetCore.Http;

namespace CQRS.Application.Common.Helper
{
    public static class FilesHelper
    {

   

       
        public static async Task<string> SaveFiles(IFormFile file)
        {

            if (file != null)
            {
                //create it if it doesnot exist
                Directory.CreateDirectory(@$"{Directory.GetCurrentDirectory()}+/Images/");

                string FileFolderPath = Directory.GetCurrentDirectory() + @$"/Images/";


                //2) Get File Name

                string FileName = Guid.NewGuid() + Path.GetFileName(file.FileName);


                // 3) Merge Path with File Name

                string FinalFilePath = Path.Combine(FileFolderPath, FileName);



                //4) Save File As Streams "Data Overtime"

                using (var Stream = new FileStream(FinalFilePath, FileMode.Create))
                {

                    await file.CopyToAsync(Stream);

                }

                return FinalFilePath;
            }

            return "File is empty";
        }


    }
}
