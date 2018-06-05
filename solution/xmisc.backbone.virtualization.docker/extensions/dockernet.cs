using Docker.DotNet;
using Docker.DotNet.Models;
using reexmonkey.xmisc.core.system.net.extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.virtualization.docker.extensions
{
    public static class DockerDotNetExtensions
    {
        public static DockerClient CreateDockerClient(string endpoint)
    => new DockerClientConfiguration(new Uri(endpoint)).CreateClient();

        public static DockerClient CreateDockerClient(string address, int port)
            => new DockerClientConfiguration(new Uri($"{address}:{port}")).CreateClient();

        public static DockerClient CreateDockerClient(IPEndPoint endPoint)
            => new DockerClientConfiguration(new Uri(endPoint.ToString())).CreateClient();

        public static Task<CreateContainerResponse> CreateContainerAsync(
            this DockerClient client,
            string image,
            string name,
            int hostPort,
            int containerPort,
            string protocol,
            IEnumerable<string> env,
            CancellationToken cancellation = default(CancellationToken))
        {
            var key = containerPort.Format(protocol);
            var exposedPorts = new Dictionary<string, EmptyStruct>()
            {
                { key, new EmptyStruct() }
            };

            var portBindings = new Dictionary<string, IList<PortBinding>>()
            {
                { key,  new List<PortBinding> { new PortBinding() { HostPort = hostPort.ToString() } }}
            };

            return client.CreateContainerAsync(image, name, env, exposedPorts, portBindings, cancellation);
        }

        public static Task<CreateContainerResponse> CreateContainerAsync(
            this DockerClient client,
            string image,
            int hostPort,
            int containerPort,
            string protocol,
            IEnumerable<string> env,
            CancellationToken cancellation = default(CancellationToken))
        {
            var key = containerPort.Format(protocol);
            var exposedPorts = new Dictionary<string, EmptyStruct>()
            {
                { key, new EmptyStruct() }
            };

            var portBindings = new Dictionary<string, IList<PortBinding>>()
            {
                { key,  new List<PortBinding> { new PortBinding() { HostPort = hostPort.ToString() } }}
            };

            return client.CreateContainerAsync(image, env, exposedPorts, portBindings, cancellation);
        }

        public static async Task<CreateContainerResponse> CreateContainerAsync(
            this DockerClient client,
            string image,
            IEnumerable<string> env,
            IDictionary<string, EmptyStruct> exposedPorts,
            IDictionary<string, IList<PortBinding>> portBindings,
            CancellationToken cancellation = default(CancellationToken))
        {
            var parameters = new CreateContainerParameters()
            {
                Image = image,
                Env = env != null ? new List<string>(env) : new List<string>(),
                ExposedPorts = exposedPorts != null ? exposedPorts : new Dictionary<string, EmptyStruct>(),
                HostConfig = new HostConfig()
                {
                    PortBindings = portBindings != null ? portBindings : new Dictionary<string, IList<PortBinding>>()
                }
            };
            return await client.Containers.CreateContainerAsync(parameters, cancellation).ConfigureAwait(false);
        }

        public static async Task<CreateContainerResponse> CreateContainerAsync(
            this DockerClient client,
            string image,
            string name,
            IEnumerable<string> env,
            IDictionary<string, EmptyStruct> exposedPorts,
            IDictionary<string, IList<PortBinding>> portBindings,
            CancellationToken cancellation = default(CancellationToken))
        {
            var parameters = new CreateContainerParameters()
            {
                Image = image,
                Name = name,
                Env = env != null ? new List<string>(env) : new List<string>(),
                ExposedPorts = exposedPorts != null ? exposedPorts : new Dictionary<string, EmptyStruct>(),
                HostConfig = new HostConfig()
                {
                    PortBindings = portBindings != null ? portBindings : new Dictionary<string, IList<PortBinding>>()
                }
            };
            return await client.Containers.CreateContainerAsync(parameters, cancellation).ConfigureAwait(false);
        }

        public static async Task<(string id, bool success)> StartContainerAsync(
            this DockerClient client,
            string containerId,
            ContainerStartParameters parameters, CancellationToken cancellation = default(CancellationToken))
        {
            var started = await client.Containers.StartContainerAsync(containerId, parameters, cancellation).ConfigureAwait(false);
            return (containerId, started);
        }

        public static async Task<(string id, bool success)> StartContainerAsync(
            this DockerClient client,
            string containerId,
            ContainerStartParameters parameters,
            int delay,
            CancellationToken cancellation = default(CancellationToken))
        {
            var result = await client.StartContainerAsync(containerId, parameters, cancellation).ConfigureAwait(false);
            await Task.Delay(delay, cancellation).ConfigureAwait(false);
            return result;
        }

        public static bool IsRunning(this ContainerListResponse container)
            => container.State.Equals("running", StringComparison.OrdinalIgnoreCase);

        public static async Task<IList<(string id, bool success)>> StartContainersAsync(
            this DockerClient client,
            IEnumerable<string> containerIds,
            ContainerStartParameters parameters,
            CancellationToken cancellation = default(CancellationToken))
        {
            var results = new List<(string id, bool success)>();
            foreach (var containerId in containerIds)
            {
                var result = await client.StartContainerAsync(containerId, parameters, cancellation).ConfigureAwait(false);
                results.Add(result);
            }
            return results;
        }

        public static async Task<(string id, bool success)> StopContainerAsync(
            this DockerClient client,
            string containerId,
            ContainerStopParameters parameters,
            CancellationToken cancellation = default(CancellationToken))
        {
            var stopped = await client.Containers.StopContainerAsync(containerId, parameters, cancellation).ConfigureAwait(false);
            return (containerId, stopped);
        }

        public static async Task<IList<(string id, bool success)>> StopContainersAsync(
            this DockerClient client,
            IEnumerable<string> containerIds,
            ContainerStopParameters parameters,
            CancellationToken cancellation = default(CancellationToken))
        {
            var results = new List<(string id, bool success)>();
            foreach (var containerId in containerIds)
            {
                var result = await client.StopContainerAsync(containerId, parameters, cancellation).ConfigureAwait(false);
                results.Add(result);
            }
            return results;
        }

        public static async Task<IList<string>> StopAndRemoveContainersAsync(
            this DockerClient client,
            IEnumerable<string> containerIds,
            ContainerStopParameters stopParameters,
            ContainerRemoveParameters removeParameters,
            CancellationToken cancellation = default(CancellationToken))
        {
            var results = new List<string>();
            var stoppedResults = await client.StopContainersAsync(containerIds, stopParameters, cancellation).ConfigureAwait(false);

            //remove stopped containers
            foreach (var result in stoppedResults)
            {
                var removed = await client.RemoveContainerAsync(result.id, removeParameters, cancellation).ConfigureAwait(false);
                results.Add(removed);
            }
            return results;
        }

        public static async Task<string> RemoveContainerAsync(
            this DockerClient client, string id,
            ContainerRemoveParameters parameters,
            CancellationToken cancellation = default(CancellationToken))
        {
            await client.Containers.RemoveContainerAsync(id, parameters, cancellation).ConfigureAwait(false);
            return id;
        }

        public static async Task<IList<string>> RemoveContainersAsync(
            this DockerClient client,
            IEnumerable<string> ids,
            ContainerRemoveParameters parameters,
            CancellationToken cancellation = default(CancellationToken))
        {
            var removed = new List<string>();
            foreach (var containerId in ids)
            {
                var result = await client.RemoveContainerAsync(containerId, parameters, cancellation).ConfigureAwait(false);
                if (!removed.Contains(containerId, StringComparer.OrdinalIgnoreCase)) removed.Add(result);
            }
            return removed;
        }

        public static Task<IList<ContainerListResponse>> GetContainersAsync(
            this DockerClient client,
            ContainersListParameters parameters,
            CancellationToken cancellation = default(CancellationToken))
            => client.Containers.ListContainersAsync(parameters, cancellation);

        public static ContainerListResponse FindContainerById(this IEnumerable<ContainerListResponse> containers, string id)
        {
            return containers.FindContainer(x => x.ID.Equals(id, StringComparison.OrdinalIgnoreCase));
        }

        public static ContainerListResponse FindContainerByNameAsync(this IEnumerable<ContainerListResponse> containers, string name)
            => containers.FindContainer(x => x.Names.Contains(name, StringComparer.OrdinalIgnoreCase) || x.Names.Contains("/" + name, StringComparer.OrdinalIgnoreCase));

        public static ContainerListResponse FindContainerByImageId(this IEnumerable<ContainerListResponse> containers, string imageId)
            => containers.FindContainer(x => x.ImageID.Equals(imageId, StringComparison.OrdinalIgnoreCase));

        public static ContainerListResponse FindContainerByImage(this IEnumerable<ContainerListResponse> containers, string image)
            => containers.FindContainer(x => x.Image.Equals(image, StringComparison.OrdinalIgnoreCase));

        public static ContainerListResponse FindContainer(this IEnumerable<ContainerListResponse> containers, Func<ContainerListResponse, bool> predicate)
            => containers.FirstOrDefault(predicate);

        public static IEnumerable<ContainerListResponse> FindContainersByImageId(this IEnumerable<ContainerListResponse> containers, string imageId)
            => containers.Where(x => x.ImageID.Equals(imageId, StringComparison.OrdinalIgnoreCase));

        public static bool Contains(this string source, string other, StringComparison comparison) => source.IndexOf(other, comparison) >= 0;

        public static IEnumerable<ContainerListResponse> FindContainersByName(this IEnumerable<ContainerListResponse> containers, string name)
            => containers.Where(x => x.Names.Contains(name, StringComparer.OrdinalIgnoreCase) || x.Names.Contains("/" + name, StringComparer.OrdinalIgnoreCase));

        public static IEnumerable<ContainerListResponse> FindContainersByImage(this IEnumerable<ContainerListResponse> containers, string image)
            => containers.Where(x => x.Image.Contains(image, StringComparison.OrdinalIgnoreCase));

        public static IEnumerable<ContainerListResponse> FindContainers(this IEnumerable<ContainerListResponse> containers, Func<ContainerListResponse, bool> predicate)
            => containers.Where(predicate);
    }
}
