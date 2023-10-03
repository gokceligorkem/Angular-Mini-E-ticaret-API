using Microsoft.AspNetCore.Diagnostics;
using System.Net;
using System.Net.Mime;
using System.Text.Json;

namespace EticaretAPI.Presentation.Exceptions
{
    public static  class ConfigureExceptionHandlerExtension
    {
        public static void ConfigureExcepitonHandler<T>(this WebApplication application,ILogger<T> logger)
        {
            application.UseExceptionHandler(builder =>
            {
                builder.Run(async context =>
                {
                    context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;
                    context.Response.ContentType = MediaTypeNames.Application.Json;
                    var contextFeature = context.Features.Get<IExceptionHandlerFeature>();

                    if (contextFeature != null)
                    {
                        object customError = null;

                        if (contextFeature.Error is BadHttpRequestException badRequestException)
                        {
                            // BadRequestException özel bir işlem yapma
                            context.Response.StatusCode = (int)HttpStatusCode.BadRequest;
                            logger.LogError(badRequestException.Message);
                            customError = new
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Bad Request: " + badRequestException.Message,
                                Title = "GEÇERSİZ İSTEK. Hata alındı!",
                               
                            };
                        }
                        else if (contextFeature.Error is DirectoryNotFoundException notFoundException)
                        {
                            // NotFoundException özel bir işlem yapma
                            context.Response.StatusCode = (int)HttpStatusCode.NotFound;
                            logger.LogError(notFoundException.Message);
                            customError = new
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = "Not Found: " + notFoundException.Message,
                                Title = "Bulunamadı. Hata alındı!",
                                
                            };
                        }
                        else
                        {
                            // Diğer hatalar için varsayılan işlem
                            var errorMessage = "Internal Server Error: " + contextFeature.Error.Message;
                            logger.LogError(errorMessage);
                            customError = new
                            {
                                StatusCode = context.Response.StatusCode,
                                Message = errorMessage,
                                Title = "İç Sunucu Hatası Hata alındı!",
                                
                            };
                        }

                        // Hata mesajını JSON formatına dönüştürüp yanıtı gönderin
                        var json = JsonSerializer.Serialize(customError);
                        await context.Response.WriteAsync(json);
                    }
                });
            });
        }
    }
}
