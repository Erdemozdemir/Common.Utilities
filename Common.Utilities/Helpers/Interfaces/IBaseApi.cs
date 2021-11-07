using Common.Utilities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Common.Utilities.Helpers.Interfaces
{
    public interface IBaseApi
    {

        public Task<Result<TResponseModel>> GetAsync<TResponseModel>(string url) where TResponseModel : class, new();
        
        public Task<Result<TResponseModel>> PostAsync<TResponseModel, TRequestModel>(string url, TRequestModel requestModel) where TResponseModel : class, new() where TRequestModel : class, new();

    }
}
