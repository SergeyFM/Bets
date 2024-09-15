using System.Net;
using System.Text.Json;
using System.Text;
using Bets.Abstractions.Client.Exceptions;
using Bets.Abstractions.Client.Serialization;

namespace Bets.Abstractions.Client
{
    public abstract class ClientBase
    {
        private readonly HttpClient _client;

        private readonly Serialization.JsonSerializer serializer;

        protected ClientBase(HttpClient client, JsonSerializerOptions options)
        {
            _client = client ?? throw new ArgumentNullException(nameof(client));
            serializer = new Serialization.JsonSerializer(options);
        }

        protected Task<TOut> Get<TOut>(string endpoint) where TOut : class
        {
            return Get<TOut>(endpoint, CancellationToken.None);
        }

        protected Task<TOut> Get<TOut>(string endpoint, TOut _) where TOut : class
        {
            return Get<TOut>(endpoint, CancellationToken.None);
        }

        protected Task<TOut> Get<TOut>(string endpoint, TOut _, CancellationToken ct) where TOut : class
        {
            return Get<TOut>(endpoint, ct);
        }

        protected async Task<TOut> Get<TOut>(string endpoint, CancellationToken ct) where TOut : class
        {
            try
            {
                var request = BuildRequest(HttpMethod.Get, endpoint);
                var response = await _client.SendAsync(request, ct);
                var jsonContent = await HandleResponseThrowErrorIfFailed(response, endpoint);

                return serializer.Deserialize<TOut>(jsonContent);
            }
            catch (ClientBaseDeserializationException ex)
            {
                throw new ClientBaseException($"Ошибка десериализации результата запроса. Endpoint: {GetAbsoluteUri(endpoint)}{Environment.NewLine}{ex.JsonContent}", ex);
            }
            catch (Exception ex)
            {
                throw new ClientBaseException($"Ошибка отправки GET запроса. Endpoint: {GetAbsoluteUri(endpoint)}", ex);
            }
        }

        protected Task Post<TIn>(string endpoint, TIn value) where TIn : class
        {
            return Post(endpoint, value, CancellationToken.None);
        }

        protected async Task Post<TIn>(string endpoint, TIn value, CancellationToken ct) where TIn : class
        {
            try
            {
                var request = BuildRequest(HttpMethod.Post, endpoint, Serialize(value));
                var response = await _client.SendAsync(request, ct);
                _ = await HandleResponseThrowErrorIfFailed(response, endpoint);
            }
            catch (Exception ex)
            {
                throw new ClientBaseException($"Ошибка отправки POST запроса. Endpoint: {GetAbsoluteUri(endpoint)}", ex);
            }
        }

        protected async Task<TOut> Post<TOut>(string endpoint, CancellationToken ct) where TOut : class
        {
            try
            {
                var request = BuildRequest(HttpMethod.Post, endpoint);
                var response = await _client.SendAsync(request, ct);
                var jsonContent = await HandleResponseThrowErrorIfFailed(response, endpoint);

                return serializer.Deserialize<TOut>(jsonContent);
            }
            catch (ClientBaseDeserializationException ex)
            {
                throw new ClientBaseException($"Ошибка десериализации результата запроса. Endpoint: {GetAbsoluteUri(endpoint)}{Environment.NewLine}{ex.JsonContent}", ex);
            }
            catch (Exception ex)
            {
                throw new ClientBaseException($"Ошибка отправки POST запроса. Endpoint: {GetAbsoluteUri(endpoint)}", ex);
            }
        }

        protected Task<TOut> Post<TIn, TOut>(string endpoint, TIn value) where TIn : class where TOut : class
        {
            return Post<TIn, TOut>(endpoint, value, CancellationToken.None);
        }

        protected async Task<TOut> Post<TIn, TOut>(string endpoint, TIn value, CancellationToken ct) where TIn : class where TOut : class
        {
            try
            {
                var request = BuildRequest(HttpMethod.Post, endpoint, Serialize(value));
                var response = await _client.SendAsync(request, ct);
                var jsonContent = await HandleResponseThrowErrorIfFailed(response, endpoint);

                return serializer.Deserialize<TOut>(jsonContent);
            }
            catch (KeyNotFoundException) // Возможно никогда не возникнет
            {
                throw;
            }
            catch (ClientBaseDeserializationException ex)
            {
                throw new ClientBaseException($"Ошибка десериализации результата запроса. Endpoint: {GetAbsoluteUri(endpoint)}{Environment.NewLine}{ex.JsonContent}", ex);
            }
            catch (Exception ex)
            {
                throw new ClientBaseException($"Ошибка отправки POST запроса. Endpoint: {GetAbsoluteUri(endpoint)}", ex);
            }
        }

        protected async Task<TOut> Post<TIn, TOut>(string endpoint, TIn value, List<HeaderPair> headers, CancellationToken ct) where TIn : class where TOut : class
        {
            try
            {
                var request = BuildRequest(HttpMethod.Post, endpoint, Serialize(value));
                AddHeaders(request, headers);
                var response = await _client.SendAsync(request, ct);
                var jsonContent = await HandleResponseThrowErrorIfFailed(response, endpoint);

                return serializer.Deserialize<TOut>(jsonContent);
            }
            catch (KeyNotFoundException) // Возможно никогда не возникнет
            {
                throw;
            }
            catch (ClientBaseDeserializationException ex)
            {
                throw new ClientBaseException($"Ошибка десериализации результата запроса. Endpoint: {GetAbsoluteUri(endpoint)}{Environment.NewLine}{ex.JsonContent}", ex);
            }
            catch (Exception ex)
            {
                throw new ClientBaseException($"Ошибка отправки POST запроса. Endpoint: {GetAbsoluteUri(endpoint)}", ex);
            }
        }

        protected Task<TOut> Post<TIn, TOut>(string endpoint, TIn value, TOut _) where TIn : class where TOut : class
        {
            return Post<TIn, TOut>(endpoint, value, CancellationToken.None);
        }

        protected async Task<TOut> Post<TIn, TOut>(string endpoint, TIn value, TOut _, CancellationToken ct)
            where TIn : class where TOut : class
        {
            try
            {
                var body = Serialize(value);
                var request = BuildRequest(HttpMethod.Post, endpoint, body);
                var response = await _client.SendAsync(request, ct);
                var jsonContent = await HandleResponseThrowErrorIfFailed(response, endpoint);

                return serializer.Deserialize<TOut>(jsonContent);
            }
            catch (ClientBaseDeserializationException ex)
            {
                throw new ClientBaseException($"Ошибка десериализации результата запроса. Endpoint: {GetAbsoluteUri(endpoint)}{Environment.NewLine}{ex.JsonContent}", ex);
            }
            catch (Exception ex)
            {
                throw new ClientBaseException($"Ошибка отправки POST запроса. Endpoint: {GetAbsoluteUri(endpoint)}", ex);
            }
        }

        protected HttpRequestMessage BuildRequest(HttpMethod method, string endpoint)
            => new HttpRequestMessage(method, Endpoint(endpoint));

        protected HttpRequestMessage BuildRequest(HttpMethod method, string endpoint, string jsonContent)
            => new HttpRequestMessage(method, Endpoint(endpoint))
            {
                Content = new StringContent(jsonContent, Encoding.UTF8, "application/json")
            };

        protected void AddHeaders(HttpRequestMessage request, List<HeaderPair> headers)
        {
            if (headers == null)
                return;
            foreach (var pair in headers)
                request.Headers.Add(pair.Name, pair.Value);
        }

        private string Serialize<T>(T value) where T : class
        {
            if (value is JsonString x)
            {
                return x.Value;
            }

            return serializer.Serialize(value);
        }

        protected string Endpoint(string value, params object[] args)
        {
            if (_client.BaseAddress.AbsolutePath.EndsWith("/"))
            {
                value = new string(value.SkipWhile(c => c == '/').ToArray());
            }

            if (args.Any())
            {
                return string.Format(value, args);
            }

            return value;
        }

        protected string GetAbsoluteUri(string endpoint)
        {
            return string.Concat(_client.BaseAddress.AbsoluteUri.TrimEnd('/').Append('/').Concat(endpoint.SkipWhile(c => c == '/')));
        }

        async Task<string> HandleResponseThrowErrorIfFailed(HttpResponseMessage response, string endpoint)
        {
            var code = response.StatusCode;
            var jsonContent = await TryGetContentOrDefault(response.Content);
            var errorMessage = GetExceptionMessage(jsonContent ?? "No content", GetTraceIfAvailable() ?? "null");

            if (code == HttpStatusCode.OK && jsonContent != null)
            {
                return jsonContent;
            }

            throw new Exception(errorMessage);

            async Task<string> TryGetContentOrDefault(HttpContent content)
            {
                try
                {
                    return await content.ReadAsStringAsync();
                }
                catch
                {
                    return null;
                }
            }

            string GetExceptionMessage(string content, string trace)
            {
                return
                    new StringBuilder()
                        .Append((int)code).Append(' ').AppendLine(code.ToString())
                        .Append("Failed for ").AppendLine(GetAbsoluteUri(endpoint))
                        .Append("trace-id: ").AppendLine(trace)
                        .AppendLine()
                        .AppendLine(content)
                    .ToString();
            }

            string GetTraceIfAvailable()
            {
                try
                {
                    foreach (var header in response.Headers)
                    {
                        if (header.Key.Equals("trace-id", StringComparison.OrdinalIgnoreCase))
                        {
                            if (header.Value.Any())
                            {
                                var value = string.Join(string.Empty, header.Value);

                                return string.IsNullOrWhiteSpace(value) ? null : value;
                            }

                            return null;
                        }
                    }

                    return null;
                }
                catch
                {
                    return null;
                }
            }
        }
    }
}
