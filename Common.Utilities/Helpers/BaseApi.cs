using Common.Utilities.Helpers.Interfaces;
using Common.Utilities.Models;
using System;
using System.Net.Http;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Common.Utilities.Helpers
{
    public abstract class BaseApi : IBaseApi
    {
        private readonly IHttpClientFactory _clientFactory;

        public BaseApi(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }


        public async virtual Task<Result<TResponseModel>> GetAsync<TResponseModel>(string url)
            where TResponseModel : class, new()
        {
            var res = new Result<TResponseModel>();
            try
            {
                var responseBody = await GetClient().GetAsync(url);
                res.Data = await SetHttpContentData<TResponseModel>(responseBody.Content);

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
                return res;
            }
        }

        public async virtual Task<Result<TResponseModel>> PostAsync<TResponseModel, TRequestModel>(string url, TRequestModel requestModel) where TResponseModel : class, new()
            where TRequestModel : class, new()
        {
            var res = new Result<TResponseModel>();
            try
            {
                var responseBody = await GetClient().PostAsJsonAsync(url, requestModel);
                res.Data = await SetHttpContentData<TResponseModel>(responseBody.Content);
                res.IsSuccessful = responseBody.StatusCode == System.Net.HttpStatusCode.OK;

                return res;
            }
            catch (Exception ex)
            {
                res.ErrorMessage = ex.Message;
                return res;
            }
        }

        #region  Protect Members
        protected virtual HttpClient GetClient()
        {
            var client = _clientFactory.CreateClient("defaultHttpClient");

            return client;
        }

        protected virtual async Task<TModel> SetHttpContentData<TModel>(HttpContent httpContent)
        {
            var content = await httpContent.ReadAsStringAsync();
            var model = JsonSerializer.Deserialize<TModel>(content);

            return model;
        }
        #endregion

    }
}
