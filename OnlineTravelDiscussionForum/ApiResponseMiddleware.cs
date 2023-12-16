using Azure;
using Newtonsoft.Json;
using OnlineTravelDiscussionForum.Dtos;
using System.Text;

namespace OnlineTravelDiscussionForum
{
    public class ApiResponseMiddleware
    {
        private readonly RequestDelegate _next;

        public ApiResponseMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            var originalBodyStream = context.Response.Body;

            using (var memoryStream = new MemoryStream())
            {
                context.Response.Body = memoryStream;

                await _next(context);

                memoryStream.Seek(0, SeekOrigin.Begin);
                string responseBody = new StreamReader(memoryStream).ReadToEnd();
                memoryStream.Seek(0, SeekOrigin.Begin);
                //Wrap the original response into ApiResponse object
                string m = "";

                object responce = new object();
                try
                {
                    responce = new ApiResponce<object>
                    {
                        Success = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
                        Status = context.Response.StatusCode.ToString(),

                        Data = JsonConvert.DeserializeObject(responseBody) // Deserialize the inner JSON string
                    };
                }
                catch (Exception ex)
                {
                    var respbody = new{message = responseBody };
                    responce = new ApiResponce<object>
                    {
                        Success = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
                        Status = context.Response.StatusCode.ToString(),

                        Data = respbody // Deserialize the inner JSON string
                    };

                }

                //var apiResponse = new ApiResponce<object>
                //{
                //    Success = context.Response.StatusCode >= 200 && context.Response.StatusCode < 300,
                //    Message = context.Response.StatusCode.ToString(),

                //    Data = JsonConvert.DeserializeObject(responseBody) // Deserialize the inner JSON string
                //};

                // Serialize ApiResponse to JSON
                var jsonResponse = JsonConvert.SerializeObject(responce);
                var responseBytes = Encoding.UTF8.GetBytes(jsonResponse);

                // Write the modified response to the original stream
                await originalBodyStream.WriteAsync(responseBytes, 0, responseBytes.Length);
            }
        }
    }
}
