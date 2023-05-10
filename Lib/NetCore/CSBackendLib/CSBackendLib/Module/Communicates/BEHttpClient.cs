using BEBackendLib.Module.Enums;
using System.Net.Http.Headers;
using System.Net.Http.Json;

namespace BEBackendLib.Module.Communicates
{
    public class BEHttpClient : IDisposable
    {
        private BERequestModel? _reqModel = null;
        private HttpClient? _httpClient = null;

        public BEHttpClient(BERequestModel reqModel)
        {
            _reqModel = reqModel;
        }

        ~BEHttpClient()
        {
            Dispose();
        }

        public void InitRequest()
        {
            try
            {
                if (_reqModel is null)
                    throw new Exception("Request Model is null, please check again!");

                if (string.IsNullOrEmpty(_reqModel.Url))
                    throw new Exception("Url is nullorEmpty, please check again!");

                _httpClient = new HttpClient();
                _httpClient.BaseAddress = new Uri(_reqModel.Url);

                _httpClient.DefaultRequestHeaders.Clear();
                _httpClient.DefaultRequestHeaders.Accept.Clear();

                if (_reqModel.Authorizations is not null)
                    _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(_reqModel.Authorizations.Name, _reqModel.Authorizations.Value);

                if (_reqModel.HeaderParams is not null && _reqModel.HeaderParams.Length > 0)
                {
                    foreach (var par in _reqModel.HeaderParams)
                        _httpClient.DefaultRequestHeaders.Add(par.Name, par.Value);
                }

                if (_reqModel.Timeout <= 0)
                    _reqModel.Timeout = 60;

                _httpClient.Timeout = TimeSpan.FromSeconds(_reqModel.Timeout);
                _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(_reqModel.ContentType));
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<string?> GetResponse()
        {
            try
            {
                string? res = null;
                res = _reqModel?.Method switch
                {
                    BEMethod.GET => await TaskGetAsync(),
                    BEMethod.POST => await TaskPostAsync(),
                    BEMethod.PUT => await TaskPutAsync(),
                    BEMethod.DELETE => await TaskDeleteAsync(),
                    _ => string.Empty
                };
                return res;  
            }
            catch (Exception)
            {
                throw;
            }
            finally { Dispose(); }
        }

        public void Dispose()
        {
            try
            {
                _httpClient?.Dispose();
                _httpClient = null;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string?> TaskGetAsync()
        {
            try
            {
                if (_httpClient is not null)
                {
                    HttpResponseMessage? response = await _httpClient.GetAsync(_reqModel?.Url);
                    if (response is not null)
                    {
                        if (!response.IsSuccessStatusCode)
                            return response.ReasonPhrase;
                        else
                            return await response.Content.ReadAsStringAsync();
                    }
                }
                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string?> TaskPostAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_reqModel?.JsonRequest))
                    throw new Exception("Json Request is nullorEmpty, please check again!");
                if (_httpClient is not null)
                {
                    HttpResponseMessage? response = await _httpClient.PostAsJsonAsync(_reqModel?.Url, _reqModel?.JsonRequest);
                    if (response is not null)
                    {
                        if (!response.IsSuccessStatusCode)
                            return response.ReasonPhrase;
                        else
                            return await response.Content.ReadAsStringAsync();
                    }
                }

                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string?> TaskPutAsync()
        {
            try
            {
                if (string.IsNullOrEmpty(_reqModel?.JsonRequest))
                    throw new Exception("Json Request is nullorEmpty, please check again!");

                if (_httpClient is not null)
                {
                    HttpResponseMessage? response = await _httpClient.PutAsJsonAsync(_reqModel?.Url, _reqModel?.JsonRequest);
                    if (response is not null)
                    {
                        if (!response.IsSuccessStatusCode)
                            return response.ReasonPhrase;
                        else
                            return await response.Content.ReadAsStringAsync();
                    }
                }

                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }

        private async Task<string?> TaskDeleteAsync()
        {
            try
            {
                if (_httpClient is not null)
                {
                    HttpResponseMessage? response = await _httpClient.DeleteAsync(_reqModel?.Url);
                    if (response is not null)
                    {
                        if (!response.IsSuccessStatusCode)
                            return response.ReasonPhrase;
                        else
                            return await response.Content.ReadAsStringAsync();
                    }
                }

                return string.Empty;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}

