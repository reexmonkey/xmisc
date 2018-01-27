using reexmonkey.xmisc.core.io.infrastructure;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.net.contracts.clients
{
    public interface IPutClient
    {
        void Put<TRequest>(TRequest request);

        void Put<TRequest>(Func<TRequest> requestFunc);

        void Put(Uri uri, object request, TextSerializerBase serializer);

        TResponse Put<TRequest, TResponse>(TRequest request) where TResponse : new();

        TResponse Put<TRequest, TResponse>(Func<TRequest> requestFunc) where TResponse : new();

        TResponse Put<TResponse>(Uri uri, object content, TextSerializerBase serializer) where TResponse : new();

        Task PutAsync<TRequest>(TRequest request);

        Task PutAsync<TRequest>(TRequest request, CancellationToken token);

        Task PutAsync<TRequest>(Func<TRequest> requestFunc);

        Task PutAsync<TRequest>(Func<TRequest> requestFunc, CancellationToken token);

        Task<TResponse> PutAsync<TResponse>(Uri uri, object content, TextSerializerBase serializer) where TResponse : new();

        Task<TResponse> PutAsync<TResponse>(Uri uri, object content, TextSerializerBase serializer, CancellationToken token) where TResponse : new();

        Task PutAsync(Uri uri, object content, TextSerializerBase serializer);

        Task PutAsync(Uri uri, object content, TextSerializerBase serializer, CancellationToken token);
    }
}