using System.Text;
using System.Text.Json;

namespace FraudDetection.Services
{
    public class ApiService
    {
        private readonly HttpClient client;

        public ApiService()
        {
            client = new HttpClient();

            client.BaseAddress =
                new Uri(
                    "http://localhost:5133/api/"
                );
        }

        public async Task<string> Register(
            object data
        )
        {
            StringContent content =
                new StringContent(
                    JsonSerializer.Serialize(
                        data
                    ),
                    Encoding.UTF8,
                    "application/json"
                );

            HttpResponseMessage response =
                await client.PostAsync(
                    "auth/register",
                    content
                );

            return await response
                .Content
                .ReadAsStringAsync();
        }

        public async Task<string> Login(
            object data
        )
        {
            StringContent content =
                new StringContent(
                    JsonSerializer.Serialize(
                        data
                    ),
                    Encoding.UTF8,
                    "application/json"
                );

            HttpResponseMessage response =
                await client.PostAsync(
                    "auth/login",
                    content
                );

            return await response
                .Content
                .ReadAsStringAsync();
        }

        public async Task<string>
            CreateTransaction(
                object data
            )
        {
            StringContent content =
                new StringContent(
                    JsonSerializer.Serialize(
                        data
                    ),
                    Encoding.UTF8,
                    "application/json"
                );

            HttpResponseMessage response =
                await client.PostAsync(
                    "transaction",
                    content
                );

            return await response
                .Content
                .ReadAsStringAsync();
        }

        public async Task<string>
            GetUsers()
        {
            HttpResponseMessage response =
                await client.GetAsync(
                    "users"
                );

            return await response
                .Content
                .ReadAsStringAsync();
        }
    }
}