using System.Text;
using System.Text.Json;

using FraudDetection.Models;

namespace FraudDetection.Services
{
    public class ApiService
    {
        private readonly HttpClient
            client;

        public ApiService()
        {
            client =
                new HttpClient();

            client.BaseAddress =
                new Uri(
                    "http://localhost:5133/api/"
                );
        }

        public async Task<string>
            Register(
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

        public async Task<LoginResponse>
            Login(
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

            string json =
                await response
                    .Content
                    .ReadAsStringAsync();

            return JsonSerializer
                .Deserialize
                <
                    LoginResponse
                >(
                    json,
                    new JsonSerializerOptions
                    {
                        PropertyNameCaseInsensitive =
                            true
                    }
                )!;
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

        public async Task<string>
            GetTransactions()
        {
            HttpResponseMessage response =
                await client.GetAsync(
                    "transaction"
                );

            return await response
                .Content
                .ReadAsStringAsync();
        }

        public async Task<string>
            GetUserTransactions(
                string cpf
            )
        {
            HttpResponseMessage response =
                await client.GetAsync(
                    $"transaction/user/{cpf}"
                );

            return await response
                .Content
                .ReadAsStringAsync();
        }
    }
}