using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Net.Http.Headers;
using WebApplication.Controllers.ControllersTools;

namespace WebApplication.Controllers
{
    /// <summary>
    ///     Контроллер загрузки файлов
    /// </summary>
    [Route("api/[controller]")]
    public class FilesController : Controller
    {
        private readonly string _tempFilespath = "/Users/koloboks/Documents/Projects/details/Details/WebApplication/tmp"; // Вынести в конфиг
        private readonly int _maxBoundaryLength = 900; // Вынести в конфиг и понять зачем это нужно
        
        [HttpPost("/admin/UploadFile")]
        [DisableFormValueModelBinding]
        public async Task<IActionResult> UploadFile()
        {
            if (!MultipartRequestHelper.IsMultipartContentType(Request.ContentType)) // если не multipart кидаем BadRequest
                return BadRequest($"Expected a multipart request, but got {Request.ContentType}");

            var formAccumulator = new KeyValueAccumulator(); // Коллекция будет содержать ключ/значение всего мультипарта

            var boundary = MultipartRequestHelper.GetBoundary(
                MediaTypeHeaderValue.Parse(Request.ContentType), _maxBoundaryLength); 
            var reader = new MultipartReader(boundary, HttpContext.Request.Body);

            var section = await reader.ReadNextSectionAsync();
            while (section?.Body != null)
            {
                var hasContentDispositionHeader =
                    ContentDispositionHeaderValue.TryParse(section.ContentDisposition, out var contentDisposition);

                if (hasContentDispositionHeader) // Парсинг частей multipart сообщения, которые содержат файлы
                    if (MultipartRequestHelper.HasFileContentDisposition(contentDisposition))
                    {
                        //targetFilePath = Path.GetTempFileName();

                        var targetFilePath = _tempFilespath+"/"+contentDisposition.FileName.Buffer.Replace("\"","");
                        using (var targetStream = System.IO.File.Create(targetFilePath))
                        {
                            await section.Body.CopyToAsync(targetStream);
                            formAccumulator.Append("filePath",targetFilePath);
                        }
                    }
                    else if (MultipartRequestHelper.HasFormDataContentDisposition(contentDisposition)) // Парсинг частей multipart сообщения, которые содержат пары ключ-значение
                    {
                        var key = HeaderUtilities.RemoveQuotes(contentDisposition.Name);
                        var encoding = ControllersTools.ControllersTools.GetEncoding(section);
                        using (var streamReader = new StreamReader(section.Body, encoding, true, 1024, true))
                        {
                            // The value length limit is enforced by MultipartBodyLengthLimit
                            var value = await streamReader.ReadToEndAsync();
                            if (string.Equals(value, "undefined", StringComparison.OrdinalIgnoreCase))
                                value = string.Empty;
                            formAccumulator.Append(key.Value, value);
                            if (formAccumulator.ValueCount > _maxBoundaryLength)
                                throw new InvalidDataException($"Form key count limit {_maxBoundaryLength} exceeded.");
                        }
                    }
                
                section = await reader.ReadNextSectionAsync();
            }
            
            // Коллекцию formAccumulator передаем в методы парсинга. После парсинга - tmp файл нужно будет удалить
            return Ok("file upload successfully");
        }
    }
}