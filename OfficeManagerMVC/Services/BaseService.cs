using Newtonsoft.Json;
using OfficeManagerMVC.Models.DTOs;
using OfficeManagerMVC.Models.Enums;
using OfficeManagerMVC.Services.Interfaces;
using System.Net;
using System.Text;

namespace OfficeManagerMVC.Services
{
    public class BaseService : IBaseService
    {
        private readonly HttpClient httpClient;
        private readonly ITokenProvider tokenProvider;

        public BaseService(IHttpClientFactory clientFactory, ITokenProvider tokenProvider)
        {
            httpClient = clientFactory.CreateClient("OfficeManagerClient");
            this.tokenProvider = tokenProvider;
        }

        public async Task<ResponseDto?> SendAsync(RequestDto request, bool withBearer = true)
        {
            try
            {
                HttpRequestMessage message = new();

                message.Headers.Add("Accept", "application/json");

                if (withBearer)
                {
                    var token = tokenProvider.GetToken();
                    message.Headers.Add("Authorization", $"Bearer {token}");
                }

                message.RequestUri = new Uri(request.Url);

                if (request.Data is not null)
                {
                    message.Content = new StringContent(JsonConvert.SerializeObject(request.Data),
                        Encoding.UTF8, "application/json");
                }

                HttpResponseMessage httpResponse = default!;

                switch (request.Method)
                {
                    case ApiMethod.POST:
                        message.Method = HttpMethod.Post;
                        break;
                    case ApiMethod.DELETE:
                        message.Method = HttpMethod.Delete;
                        break;
                    case ApiMethod.PUT:
                        message.Method = HttpMethod.Put;
                        break;
                    default:
                        message.Method = HttpMethod.Get;
                        break;
                }

                httpResponse = await httpClient.SendAsync(message);

                switch (httpResponse.StatusCode)
                {
                    case HttpStatusCode.NotFound:
                        return new() { IsSucceeded = false, Message = "Not Found" };
                    case HttpStatusCode.Forbidden:
                        return new() { IsSucceeded = false, Message = "Forbidden" };
                    case HttpStatusCode.Unauthorized:
                        return new() { IsSucceeded = false, Message = "Unauthorized" };
                    case HttpStatusCode.InternalServerError:
                        return new() { IsSucceeded = false, Message = "Internal Server Error" };
                    default:
                        var apiContent = await httpResponse.Content.ReadAsStringAsync();
                        return JsonConvert.DeserializeObject<ResponseDto>(apiContent);
                }
            }
            catch (Exception ex)
            {
                return new ResponseDto()
                {
                    IsSucceeded = false,
                    Message = ex.Message.ToString(),
                };
            }
        }
    }
}
