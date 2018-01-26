using reexmonkey.xmisc.core.io.infrastructure;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.net.contracts.clients
{
    public interface ISendClient
    {
        void Send<TRequest>(TRequest request);

        void Send<TRequest>(Func<TRequest> requestFunc);

        void Send(Uri uri, object request, TextSerializerBase serializer);

        TResponse Send<TRequest, TResponse>(TRequest request) where TResponse : new();

        TResponse Send<TRequest, TResponse>(Func<TRequest> requestFunc) where TResponse : new();

        TResponse Send<TResponse>(Uri uri, object content, TextSerializerBase serializer) where TResponse : new();

        Task SendAsync<TRequest>(TRequest request);

        Task SendAsync<TRequest>(TRequest request, CancellationToken token);

        Task SendAsync<TRequest>(Func<TRequest> requestFunc);

        Task SendAsync<TRequest>(Func<TRequest> requestFunc, CancellationToken token);

        Task<TResponse> SendAsync<TResponse>(Uri uri, object content, TextSerializerBase serializer) where TResponse : new();

        Task<TResponse> SendAsync<TResponse>(Uri uri, object content, TextSerializerBase serializer, CancellationToken token) where TResponse : new();

        Task SendAsync(Uri uri, object content, TextSerializerBase serializer);

        Task SendAsync(Uri uri, object content, TextSerializerBase serializer, CancellationToken token);
    }
}
