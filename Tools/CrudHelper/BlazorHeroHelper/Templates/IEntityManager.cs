﻿namespace BlazorHeroHelper.Templates
{
    public class IEntityManager
    {
        public const string TemplateCode1 = @"using $_ADD_EDIT_CQRS_NAMESPACE_$;
using $_GET_ALL_PAGED_CQRS_NAMESPACE_$;
using $_GET_MODEL_BY_ID_CQRS_NAMESPACE_$;
using $_REQUEST_NAMESPACE_$;
using $_SHARED_WRAPPER_NAMESPACE_$;
using System.Threading.Tasks;
using System;

namespace $_NAMESPACE_$
{
    public interface I$_ENTITY_$Manager : IManager
    {
        Task<PaginatedResult<GetAllPaged$_ENTITY_$sResponse>> Get$_ENTITY_$sAsync(GetAllPaged$_ENTITY_$sRequest request);

        Task<IResult<Get$_ENTITY_$ByIdResponse>> GetByIdAsync(long id);

        Task<IResult<Get$_ENTITY_$ByIdResponse>> GetEditByIdAsync(long id);

        Task<IResult<$_DEFAULT_ID_DATATYPE_$>> SaveAsync(AddEdit$_ENTITY_$Command request);

        Task<IResult<$_DEFAULT_ID_DATATYPE_$>> DeleteAsync($_DEFAULT_ID_DATATYPE_$ id);
    }
}";

        public const string TemplateCode2 = @"using $_ADD_EDIT_CQRS_NAMESPACE_$;
using $_GET_ALL_PAGED_CQRS_NAMESPACE_$;
using $_GET_MODEL_BY_ID_CQRS_NAMESPACE_$;
using $_REQUEST_NAMESPACE_$;
using $_CLIENT_INFRA_EXTENSION_NAMESPACE_$;
using $_SHARED_WRAPPER_NAMESPACE_$;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace $_NAMESPACE_$
{
    public class $_ENTITY_$Manager : I$_ENTITY_$Manager
    {
        private readonly HttpClient _httpClient;

        public $_ENTITY_$Manager(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<IResult<$_DEFAULT_ID_DATATYPE_$>> DeleteAsync($_DEFAULT_ID_DATATYPE_$ id)
        {
            var response = await _httpClient.DeleteAsync($""{Routes.$_ENTITY_$sEndpoints.Delete}/{id}"");
            return await response.ToResult<$_DEFAULT_ID_DATATYPE_$>();
        }

        public async Task<PaginatedResult<GetAllPaged$_ENTITY_$sResponse>> Get$_ENTITY_$sAsync(GetAllPaged$_ENTITY_$sRequest request)
        {
            var response = await _httpClient.GetAsync(Routes.$_ENTITY_$sEndpoints.GetAllPaged(request.PageNumber, request.PageSize, request.SearchString, request.Orderby));
            return await response.ToPaginatedResult<GetAllPaged$_ENTITY_$sResponse>();
        }

        public async Task<IResult<Get$_ENTITY_$ByIdResponse>> GetByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync(Routes.$_ENTITY_$sEndpoints.GetById(id));
            return await response.ToResult<Get$_ENTITY_$ByIdResponse>();
        }

        public async Task<IResult<AddEdit$_ENTITY_$Command>> GetEditByIdAsync(long id)
        {
            var response = await _httpClient.GetAsync(Routes.$_ENTITY_$sEndpoints.GetById(id));
            return await response.ToResult<AddEdit$_ENTITY_$Command>();
        }

        public async Task<IResult<$_DEFAULT_ID_DATATYPE_$>> SaveAsync(AddEdit$_ENTITY_$Command request)
        {
            var response = await _httpClient.PostAsJsonAsync(Routes.$_ENTITY_$sEndpoints.Save, request);
            return await response.ToResult<$_DEFAULT_ID_DATATYPE_$>();
        }
    }
}";
    }
}
