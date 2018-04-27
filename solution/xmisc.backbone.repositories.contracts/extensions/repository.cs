using reexmonkey.xmisc.backbone.repositories.contracts.infrastucture;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
        /// Finds all data models that satisfy the given predicate and groups them according to the specified key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to search.</typeparam>
        /// <param name="repository">The repository that contains the data model.</param>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data models.</param>
        /// <param name="keySelector">A function to extract the specified key for each data model group.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <returns>A dictionary that contains grouped data models satisfying the <paramref name="predicate"/>.</returns>
        public static IDictionary<TKey, List<TModel>> FindAll<TKey, TModel>(
            this IReadRepository<TKey, TModel> repository,
            Expression<Func<TModel, bool>> predicate,
            Func<TModel, TKey> keySelector,
            int? offset = null,
            int? count = null)
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            return repository
                .FindAll(predicate, offset, count)
                .GroupBy(keySelector).ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Finds all data models asynchronously that satisfy the given predicate and groups them according to the specified key selector.
        /// </summary>
        /// <typeparam name="TKey">The type of key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to search.</typeparam>
        /// <param name="repository">The repository that contains the data model.</param>
        /// <param name="predicate">The condition that when evaluated to true, returns the found data models.</param>
        /// <param name="keySelector">A function to extract the specified key for each data model group.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A dictionary that contains grouped data models satisfying the <paramref name="predicate"/>.</returns>
        public static async Task<IDictionary<TKey, List<TModel>>> FindAllAsync<TKey, TModel>(
            this IReadRepository<TKey, TModel> repository,
            Expression<Func<TModel, bool>> predicate,
            Func<TModel, TKey> keySelector,
            int? offset = null,
            int? count = null,
            CancellationToken token = default(CancellationToken))
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            return (await repository.FindAllAsync(predicate, offset, count, token))
                .GroupBy(keySelector).ToDictionary(g => g.Key, g => g.ToList());
        }

        /// <summary>
        /// Saves the <paramref name="local"/> version of a data model to the data store if it is available; 
        /// otherwise erases the <paramref name="remote"/> version of that model from the data store.
        /// <para/> The default equality comparer is used to check the availability of the <paramref name="local"/> version by comparing the it to a given <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save on the data store.</param>
        /// <param name="remote">Data model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The default value of a data model.</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.Save(TModel)"/>
        private static void SaveImpl<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            repository.SaveImpl<TKey, TModel, TRepository>(local, remote, @default, EqualityComparer<TModel>.Default);
        }


        /// <summary>
        /// Saves the <paramref name="local"/> version of a data model asynchronously to the data store if it is available; 
        /// otherwise erases the <paramref name="remote"/> version of that model asynchronously from the data store.
        /// <para/> The default equality comparer is used to check the availability of the <paramref name="local"/> version by comparing the it to a given <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save asynchronously on the data store.</param>
        /// <param name="remote">TData model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The default value of a data model.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to save the <paramref name="local"/> version of a data model asynchronously to the data store.</returns>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAsync(TModel, CancellationToken)"/>
        private static Task SaveImplAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default, 
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            return repository.SaveImplAsync<TKey, TModel, TRepository>(local, remote, @default, EqualityComparer<TModel>.Default, token);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> version of a data model to the data store if it is available; 
        /// otherwise erases the <paramref name="remote"/> version of that model from the data store.
        /// <para/> The provided equality comparer is used to check the availability of the <paramref name="local"/> version by comparing the it to a given <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save on the data store.</param>
        /// <param name="remote">Data model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The default value of a data model.</param>
        /// <param name="comparer">The equality comparer that compares the <paramref name="local"/> version of a data model to a given <paramref name="default"/> value.</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.Save(TModel)"/>
        private static void SaveImpl<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            IEqualityComparer<TModel> comparer)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (!comparer.Equals(local, @default)) repository.Save(local);//Insert or update local model
            else //No local model available => Remove existing model from the data store
            {
                if (!comparer.Equals(remote, @default)) repository.Erase(remote);
            }
        }

        /// <summary>
        /// Saves the <paramref name="local"/> version of a data model asynchronously to the data store if it is available; 
        /// otherwise erases the <paramref name="remote"/> version of that model from the data store.
        /// <para/> The provided equality comparer is used to check the availability of the <paramref name="local"/> version by comparing the it to a given <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save on the data store.</param>
        /// <param name="remote">Data model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The default value of a data model.</param>
        /// <param name="comparer">The equality comparer that compares the <paramref name="local"/> version of a data model to a given <paramref name="default"/> value.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to save the <paramref name="local"/> version of a data model asynchronously to the data store.</returns>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAsync(TModel, CancellationToken)"/>
        private async static Task SaveImplAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            IEqualityComparer<TModel> comparer, 
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (!comparer.Equals(local, @default)) await repository.SaveAsync(local, token);//Insert or update local model
            else //No local model available => Remove existing model from the data store
            {
                if (!comparer.Equals(remote, @default)) await repository.EraseAsync(remote, token);
            }
        }

        /// <summary>
        /// Saves the <paramref name="local"/> sequence of data models to the data store after synchronizing changes with the <paramref name="remote"/> sequence of data elements.
        /// <para/>1. The given <paramref name="local"/> sequence is non-empty: Erases elements of the <paramref name="remote"/> sequence of data models from the data store that are not found in the sequence of <paramref name="local"/> data models 
        /// and saves all <paramref name="local"/> data models.
        /// <para/>2. The given <paramref name="local"/> is empty: erases all elements of the <paramref name="remote"/> sequence of data models from the data store.
        /// <para/> The default equality comparer is used to compare elements of the <paramref name="local"/> sequence against elements of the <paramref name="remote"/> sequence.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        private static void SaveAllImpl<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            repository.SaveAllImpl<TKey, TModel, TRepository>(local, remote, EqualityComparer<TModel>.Default);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> sequence of data models to the data store after synchronizing changes with the <paramref name="remote"/> sequence of data elements.
        /// <para/>1. The given <paramref name="local"/> sequence is non-empty: Erases elements of the <paramref name="remote"/> sequence of data models from the data store that are not found in the sequence of <paramref name="local"/> data models 
        /// and saves all <paramref name="local"/> data models.
        /// <para/>2. The given <paramref name="local"/> is empty: erases all elements of the <paramref name="remote"/> sequence of data models from the data store.
        /// <para/> The given equality <paramref name="comparer"/> is used to compare elements of the <paramref name="local"/> sequence against elements of the <paramref name="remote"/> sequence.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        /// <param name="comparer">The equality comparer that compares elements of the <paramref name="local"/> and <paramref name="remote"/> sequence of data models.</param>
        private static void SaveAllImpl<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            IEqualityComparer<TModel> comparer)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
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
        /// Saves the <paramref name="local"/> sequence of data models asynchronously to the data store after synchronizing changes with the <paramref name="remote"/> sequence of data elements.
        /// <para/>1. The given <paramref name="local"/> sequence is non-empty: Erases elements of the <paramref name="remote"/> sequence of data models from the data store that are not found in the sequence of <paramref name="local"/> data models 
        /// and saves all <paramref name="local"/> data models.
        /// <para/>2. The given <paramref name="local"/> is empty: erases all elements of the <paramref name="remote"/> sequence of data models from the data store.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to save the <paramref name="local"/> sequence of a data models asynchronously to the data store.</returns>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAsync(TModel, CancellationToken)"/>
        private static Task SaveAllImplAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            return repository.SaveAllImplAsync<TKey, TModel, TRepository>(local, remote, EqualityComparer<TModel>.Default, token);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> sequence of data models asynchronously to the data store after synchronizing changes with the <paramref name="remote"/> sequence of data elements.
        /// <para/>1. The given <paramref name="local"/> sequence is non-empty: Erases elements of the <paramref name="remote"/> sequence of data models from the data store that are not found in the sequence of <paramref name="local"/> data models 
        /// and saves all <paramref name="local"/> data models.
        /// <para/>2. The given <paramref name="local"/> is empty: erases all elements of the <paramref name="remote"/> sequence of data models from the data store.
        /// <para/> The given equality <paramref name="comparer"/> is used to compare elements of the <paramref name="local"/> sequence against elements of the <paramref name="remote"/> sequence.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        /// <param name="comparer">The equality comparer that compares elements of the <paramref name="local"/> and <paramref name="remote"/> sequence of data models.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to save the <paramref name="local"/> sequence of a data models asynchronously to the data store.</returns>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAsync(TModel, CancellationToken)"/> 
        private static async Task SaveAllImplAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            IEqualityComparer<TModel> comparer,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
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

        /// <summary>
        /// Saves the <paramref name="local"/> version of a data model to the data store if it is available; 
        /// otherwise erases the <paramref name="remote"/> version of that model from the data store.
        /// <para/> The default equality comparer is used to check the availability of the <paramref name="local"/> version by comparing the it to a given <paramref name="default"/> value.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> is guranteed to be different from the <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save on the data store.</param>
        /// <param name="remote">Data model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The default value of a data model.</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.Save(TModel)"/>
        public static void Save<TKey, TModel, TRepository>(this TRepository repository, TModel local, TModel remote, TModel @default)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            repository.Save<TKey, TModel, TRepository>(local, remote, @default, EqualityComparer<TModel>.Default);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> version of a data model asynchronously to the data store if it is available; 
        /// otherwise erases the <paramref name="remote"/> version of that model asynchronously from the data store.
        /// <para/> The default equality comparer is used to check the availability of the <paramref name="local"/> version by comparing the it to a given <paramref name="default"/> value.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> is guranteed to be different from the <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save asynchronously on the data store.</param>
        /// <param name="remote">TData model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The default value of a data model.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAsync(TModel, CancellationToken)"/>
        public async static Task SaveAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default, 
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            await repository.SaveAsync<TKey, TModel, TRepository>(local, remote, @default, EqualityComparer<TModel>.Default);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> version of a data model to the data store if it is available; 
        /// otherwise erases the <paramref name="remote"/> version of that model from the data store.
        /// <para/> The provided equality comparer is used to check the availability of the <paramref name="local"/> version by comparing the it to a given <paramref name="default"/> value.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> is guranteed to be different from the <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save on the data store.</param>
        /// <param name="remote">Data model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The default value of a data model.</param>
        /// <param name="comparer">The equality comparer that compares the <paramref name="local"/> version of a data model to a given <paramref name="default"/> value.</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.Save(TModel)"/>
        public static void Save<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            IEqualityComparer<TModel> comparer)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (remote != null) repository.SaveImpl<TKey, TModel, TRepository>(local, remote, @default, comparer);
            else if (local != null) repository.Save(local);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> version of a data model asynchronously to the data store if it is available; 
        /// otherwise erases the <paramref name="remote"/> version of that model from the data store.
        /// <para/> The provided equality comparer is used to check the availability of the <paramref name="local"/> version by comparing the it to a given <paramref name="default"/> value.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> is guranteed to be different from the <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save on the data store.</param>
        /// <param name="remote">Data model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The default value of a data model.</param>
        /// <param name="comparer">The equality comparer that compares the <paramref name="local"/> version of a data model to a given <paramref name="default"/> value.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAsync(TModel, CancellationToken)"/>
        public async static Task SaveAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            IEqualityComparer<TModel> comparer, 
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (remote != null) await repository.SaveImplAsync<TKey, TModel, TRepository>(local, remote, @default, comparer);
            else if (local != null) await repository.SaveAsync(local);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> sequence of data models to the data store after synchronizing changes with the <paramref name="remote"/> sequence of data elements.
        /// <para/>1. The given <paramref name="local"/> sequence is non-empty: Erases elements of the <paramref name="remote"/> sequence of data models from the data store that are not found in the sequence of <paramref name="local"/> data models 
        /// and saves all <paramref name="local"/> data models.
        /// <para/>2. The given <paramref name="local"/> is empty: erases all elements of the <paramref name="remote"/> sequence of data models from the data store.
        /// <para/> The default equality comparer is used to compare elements of the <paramref name="local"/> sequence against elements of the <paramref name="remote"/> sequence.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> sequence of data models is guranteed to be non-empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAll(IEnumerable{TModel})"/>
        public static void SaveAll<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            repository.SaveAll<TKey, TModel, TRepository>(local, remote, EqualityComparer<TModel>.Default);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> sequence of data models to the data store after synchronizing changes with the <paramref name="remote"/> sequence of data elements.
        /// <para/>1. The given <paramref name="local"/> sequence is non-empty: Erases elements of the <paramref name="remote"/> sequence of data models from the data store that are not found in the sequence of <paramref name="local"/> data models 
        /// and saves all <paramref name="local"/> data models.
        /// <para/>2. The given <paramref name="local"/> is empty: erases all elements of the <paramref name="remote"/> sequence of data models from the data store.
        /// <para/> The given equality <paramref name="comparer"/> is used to compare elements of the <paramref name="local"/> sequence against elements of the <paramref name="remote"/> sequence.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> sequence of data models is guranteed to be non-empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        /// <param name="comparer">The equality comparer that compares elements of the <paramref name="local"/> and <paramref name="remote"/> sequence of data models.</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAll(IEnumerable{TModel})"/>
        public static void SaveAll<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            IEqualityComparer<TModel> comparer)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (remote.Any()) repository.SaveAllImpl<TKey, TModel, TRepository>(local, remote, comparer);
            else if (local.Any()) repository.SaveAll(local);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> sequence of data models asynchronously to the data store after synchronizing changes with the <paramref name="remote"/> sequence of data elements.
        /// <para/>1. The given <paramref name="local"/> sequence is non-empty: Erases elements of the <paramref name="remote"/> sequence of data models from the data store that are not found in the sequence of <paramref name="local"/> data models 
        /// and saves all <paramref name="local"/> data models.
        /// <para/>2. The given <paramref name="local"/> is empty: erases all elements of the <paramref name="remote"/> sequence of data models from the data store.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> sequence of data models is guranteed to be non-empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to save the <paramref name="local"/> sequence of a data models asynchronously to the data store.</returns>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAllAsync(IEnumerable{TModel}, CancellationToken)"/> 
        public static Task SaveAllAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            return repository.SaveAllAsync<TKey, TModel, TRepository>(local, remote, EqualityComparer<TModel>.Default, token);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> sequence of data models asynchronously to the data store after synchronizing changes with the <paramref name="remote"/> sequence of data elements.
        /// <para/>1. The given <paramref name="local"/> sequence is non-empty: Erases elements of the <paramref name="remote"/> sequence of data models from the data store that are not found in the sequence of <paramref name="local"/> data models 
        /// and saves all <paramref name="local"/> data models.
        /// <para/>2. The given <paramref name="local"/> is empty: erases all elements of the <paramref name="remote"/> sequence of data models from the data store.
        /// <para/> The given equality <paramref name="comparer"/> is used to compare elements of the <paramref name="local"/> sequence against elements of the <paramref name="remote"/> sequence.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> sequence of data models is guranteed to be non-empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        /// <param name="comparer">The equality comparer that compares elements of the <paramref name="local"/> and <paramref name="remote"/> sequence of data models.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to save the <paramref name="local"/> sequence of a data models asynchronously to the data store.</returns>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAllAsync(IEnumerable{TModel}, CancellationToken)"/> 
        public async static Task SaveAllAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            IEqualityComparer<TModel> comparer,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (remote.Any()) await repository.SaveAllImplAsync<TKey, TModel, TRepository>(local, remote, comparer, token);
            else if (local.Any()) await repository.SaveAllAsync(local, token);
        }
    }
}
