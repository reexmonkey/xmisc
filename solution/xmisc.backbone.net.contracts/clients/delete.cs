using System;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.net.contracts.clients
{
    public interface IDeleteClient
    {
        void Delete<TRequest>(TRequest request);

        void Delete<TRequest>(Func<TRequest> requestFunc);

        void Delete(Uri uri);

        TResponse Delete<TRequest, TResponse>(TRequest request) where TResponse : new();

        TResponse Delete<TRequest, TResponse>(Func<TRequest> requestFunc) where TResponse : new();

        TResponse Delete<TResponse>(Uri uri) where TResponse : new();

        Task DeleteAsync<TRequest>(TRequest request);

        Task DeleteAsync<TRequest>(TRequest request, CancellationToken token);

        Task DeleteAsync<TRequest>(Func<TRequest> requestFunc);

        Task DeleteAsync<TRequest>(Func<TRequest> requestFunc, CancellationToken token);

        Task DeleteAsync(Uri uri);

        Task DeleteAsync(Uri uri, CancellationToken token);

        Task<TResponse> DeleteAsync<TRequest, TResponse>(TRequest request) where TResponse : new();

        Task<TResponse> DeleteAsync<TRequest, TResponse>(TRequest request, CancellationToken token) where TResponse : new();

        Task<TResponse> DeleteAsync<TRequest, TResponse>(Func<TRequest> requestFunc, CancellationToken token) where TResponse : new();

        Task<TResponse> DeleteAsync<TResponse>(Uri uri) where TResponse : new();

        Task<TResponse> DeleteAsync<TResponse>(Uri uri, CancellationToken token) where TResponse : new();
    }
}