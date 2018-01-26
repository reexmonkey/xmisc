using System;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.net.contracts.clients
{
    public interface IGetClient
    {
        TResponse Get<TRequest, TResponse>(TRequest request) where TResponse : new();

        TResponse Get<TRequest, TResponse>(Func<TRequest> requestFunc) where TResponse : new();

        TResponse Get<TRequest, TResponse>(Func<TResponse> requestFunc) where TResponse : new();

        TResponse Get<TResponse>(Uri uri) where TResponse : new();

        Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request) where TResponse : new();

        Task<TResponse> GetAsync<TRequest, TResponse>(TRequest request, CancellationToken token) where TResponse : new();

        Task<TResponse> GetAsync<TRequest, TResponse>(Func<TRequest> requestFunc) where TResponse : new();

        Task<TResponse> GetAsync<TRequest, TResponse>(Func<TRequest> requestFunc, CancellationToken token) where TResponse : new();

        Task<TResponse> GetAsync<TResponse>(Func<TResponse> requestFunc) where TResponse : new();

        Task<TResponse> GetAsync<TResponse>(Func<TResponse> requestFunc, CancellationToken token) where TResponse : new();

        Task<TResponse> GetAsync<TResponse>(Uri uri) where TResponse : new();

        Task<TResponse> GetAsync<TResponse>(Uri uri, CancellationToken token) where TResponse : new();
    }
}