using reexmonkey.xmisc.core.io.infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.net.contracts.clients
{
    public interface IPostClient
    {
        void Post<TRequest>(TRequest request);

        void Post<TRequest>(Func<TRequest> requestFunc);

        void Post(Uri uri, object request, TextSerializerBase serializer);

        TResponse Post<TRequest, TResponse>(TRequest request) where TResponse : new();

        TResponse Post<TRequest, TResponse>(Func<TRequest> requestFunc) where TResponse : new();

        TResponse Post<TResponse>(Uri uri, object content, TextSerializerBase serializer) where TResponse : new();

        Task PostAsync<TRequest>(TRequest request);

        Task PostAsync<TRequest>(TRequest request, CancellationToken token);

        Task PostAsync<TRequest>(Func<TRequest> requestFunc);

        Task PostAsync<TRequest>(Func<TRequest> requestFunc, CancellationToken token);

        Task<TResponse> PostAsync<TResponse>(Uri uri, object content, TextSerializerBase serializer) where TResponse : new();

        Task<TResponse> PostAsync<TResponse>(Uri uri, object content, TextSerializerBase serializer, CancellationToken token) where TResponse : new();

        Task PostAsync(Uri uri, object content, TextSerializerBase serializer);

        Task PostAsync(Uri uri, object content, TextSerializerBase serializer, CancellationToken token);
    }
}