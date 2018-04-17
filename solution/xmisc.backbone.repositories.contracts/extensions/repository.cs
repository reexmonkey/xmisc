using reexmonkey.xmisc.backbone.repositories.contracts.infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts.extensions
{
    /// <summary>
    /// Extends the features of repositories.
    /// </summary>
    public static class RepositoryExtensions
    {
        /// <summary>
        /// Reconciles changes between two sequences by using the default equality comparer and saves the difference to a remote data store.
        /// <para/> 1. In the trivial case (no <paramref name="remote"/> models), all <paramref name="local"/> models on the <paramref name="remote"/> are saved on the data store.
        /// <para/> 2. If the remote data store already contains models, these are reconciled with <paramref name="local"/> models and the difference persisted on the data store.
        /// <para/> 3. In the special case (available <paramref name="remote"/> models but no <paramref name="local"/> models), changes are synchronized by deleting all <paramref name="remote"/> models.
        /// </summary>
        /// <typeparam name="TModel">The type of the model to reconcile and save.</typeparam>
        /// <typeparam name="TRepository">The type of the repository that reconciles the sequences and saves models to a remote data store.</typeparam>
        /// <param name="repository">The repository that reconciles and saves models to a remote data store.</param>
        /// <param name="local">The sequence that contains models to save on the remote data store.</param>
        /// <param name="remote">The sequence that contains models from the remote data store.</param>
        public static void SynchronizeAll<TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote)
            where TRepository : IWriteRepository<Guid, TModel>, IEraseRepository<Guid, TModel>
        {
            repository.SynchronizeAll(local, remote, EqualityComparer<TModel>.Default);
        }

        /// <summary>
        /// Reconciles changes between two sequences by using the specified equality comparer and saves the difference to a remote data store.
        /// <para/> 1. In the trivial case (no <paramref name="remote"/> models), all <paramref name="local"/> models on the <paramref name="remote"/> are saved on the data store.
        /// <para/> 2. If the remote data store already contains models, these are reconciled with <paramref name="local"/> models and the difference persisted on the data store.
        /// <para/> 3. In the special case (available <paramref name="remote"/> models but no <paramref name="local"/> models), changes are synchronized by deleting all <paramref name="remote"/> models.
        /// </summary>
        /// <typeparam name="TModel">The type of the model to reconcile and save.</typeparam>
        /// <typeparam name="TRepository">The type of the repository that reconciles the sequences and saves models to a remote data store.</typeparam>
        /// <param name="repository">The repository that reconciles and saves models to a remote data store.</param>
        /// <param name="local">The sequence that contains models to save on the remote data store.</param>
        /// <param name="remote">The sequence that contains models from the remote data store.</param>
        /// <param name="comparer">The equality comparer to apply during reconciliation of changes between <paramref name="local"/> and <paramref name="remote"/> models.</param>
        public static void SynchronizeAll<TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            IEqualityComparer<TModel> comparer)
            where TRepository : IWriteRepository<Guid, TModel>, IEraseRepository<Guid, TModel>
        {
            if (local.Any())
            {
                //Eliminate stale models from the data store.
                var deletables = remote.Except(local, comparer);
                if (deletables.Any()) repository.EraseAll(deletables);

                //Insert new models and update existing models.
                repository.SaveAll(local);
            }
            else //No local models available => Remove all existing models from the data store
            {
                if (remote.Any()) repository.EraseAll(remote);
            }
        }

        /// <summary>
        /// Reconciles changes asynchronously between two sequences by using the default equality comparer and saves the difference to a remote data store.
        /// <para/> 1. In the trivial case (no <paramref name="remote"/> models), all <paramref name="local"/> models on the <paramref name="remote"/> are saved on the data store.
        /// <para/> 2. If the remote data store already contains models, these are reconciled with <paramref name="local"/> models and the difference persisted on the data store.
        /// <para/> 3. In the special case (available <paramref name="remote"/> models but no <paramref name="local"/> models), changes are synchronized by deleting all <paramref name="remote"/> models.
        /// </summary>
        /// <typeparam name="TModel">The type of the model to reconcile and save.</typeparam>
        /// <typeparam name="TRepository">The type of the repository that reconciles the sequences and saves models to a remote data store.</typeparam>
        /// <param name="repository">The repository that reconciles and saves models to a remote data store.</param>
        /// <param name="local">The sequence that contains models to save on the remote data store.</param>
        /// <param name="remote">The sequence that contains models from the remote data store.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>The promise to reconcile changes between the sequences.</returns>
        public static Task SynchronizeAllAsync<TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<Guid, TModel>, IEraseRepository<Guid, TModel>
        {
            return repository.SynchronizeAllAsync(local, remote, EqualityComparer<TModel>.Default, token);
        }

        /// <summary>
        /// Reconciles changes asynchronously between two sequences by using the specified equality comparer and saves the difference to a remote data store.
        /// <para/> 1. In the trivial case (no <paramref name="remote"/> models), all <paramref name="local"/> models on the <paramref name="remote"/> are saved on the data store.
        /// <para/> 2. If the remote data store already contains models, these are reconciled with <paramref name="local"/> models and the difference persisted on the data store.
        /// <para/> 3. In the special case (available <paramref name="remote"/> models but no <paramref name="local"/> models), changes are synchronized by deleting all <paramref name="remote"/> models.
        /// </summary>
        /// <typeparam name="TModel">The type of the model to reconcile and save.</typeparam>
        /// <typeparam name="TRepository">The type of the repository that reconciles the sequences and saves models to a remote data store.</typeparam>
        /// <param name="repository">The repository that reconciles and saves models to a remote data store.</param>
        /// <param name="local">The sequence that contains models to save on the remote data store.</param>
        /// <param name="remote">The sequence that contains models from the remote data store.</param>
        /// <param name="comparer">The equality comparer to apply during reconciliation of changes between <paramref name="local"/> and <paramref name="remote"/> models.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        public static async Task SynchronizeAllAsync<TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            IEqualityComparer<TModel> comparer,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<Guid, TModel>, IEraseRepository<Guid, TModel>
        {
            if (local.Any())
            {
                //Eliminate stale models from the data store.
                var deletables = remote.Except(local, comparer);
                if (deletables.Any()) await repository.EraseAllAsync(deletables, token);

                //Insert new models and update existing models.
                await repository.SaveAllAsync(local, token);
            }
            else //No local models available => Remove all existing models from the data store
            {
                if (remote.Any()) await repository.EraseAllAsync(remote, token);
            }
        }
    }
}
