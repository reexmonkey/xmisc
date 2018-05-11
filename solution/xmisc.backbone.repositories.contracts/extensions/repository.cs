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
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <returns>A dictionary that contains grouped data models satisfying the <paramref name="predicate"/>.</returns>
        public static IDictionary<TKey, List<TModel>> FindAll<TKey, TModel>(
            this IReadRepository<TKey, TModel> repository,
            Expression<Func<TModel, bool>> predicate,
            Func<TModel, TKey> keySelector,
            bool references = true,
            int? offset = null,
            int? count = null)
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            return repository
                .FindAll(predicate, references, offset, count)
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
        /// <param name="references">Decides whether to load the related references of the data models as well.</param>
        /// <param name="offset">The number of data models to bypass.</param>
        /// <param name="count">The number of data models to return.</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A dictionary that contains grouped data models satisfying the <paramref name="predicate"/>.</returns>
        public static async Task<IDictionary<TKey, List<TModel>>> FindAllAsync<TKey, TModel>(
            this IReadRepository<TKey, TModel> repository,
            Expression<Func<TModel, bool>> predicate,
            Func<TModel, TKey> keySelector,
            bool references = true,
            int? offset = null,
            int? count = null,
            CancellationToken token = default(CancellationToken))
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            return (await repository
                .FindAllAsync(predicate, references, offset, count, token))
                .GroupBy(keySelector).ToDictionary(g => g.Key, g => g.ToList());
        }

        private static void SaveImpl<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            bool references = true)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            repository.SaveImpl<TKey, TModel, TRepository>(local, remote, @default, EqualityComparer<TModel>.Default, references);
        }

        private static Task SaveImplAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            bool references = true,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            return repository.SaveImplAsync<TKey, TModel, TRepository>(local, remote, @default, EqualityComparer<TModel>.Default, references, token);
        }

        private static void SaveImpl<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            IEqualityComparer<TModel> comparer,
            bool references = true)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (!comparer.Equals(local, @default)) repository.Save(local, references);//Insert or update local model
            else //No local model available => Remove existing model from the data store
            {
                if (!comparer.Equals(remote, @default)) repository.Erase(remote);
            }
        }

        private async static Task SaveImplAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            IEqualityComparer<TModel> comparer,
            bool references = true,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (!comparer.Equals(local, @default)) await repository.SaveAsync(local, references, token);//Insert or update local model
            else //No local model available => Remove existing model from the data store
            {
                if (!comparer.Equals(remote, @default)) await repository.EraseAsync(remote, token);
            }
        }

        private static void SaveAllImpl<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            bool references = true)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            repository.SaveAllImpl<TKey, TModel, TRepository>(local, remote, EqualityComparer<TModel>.Default, references);
        }

        private static void SaveAllImpl<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            IEqualityComparer<TModel> comparer,
            bool references = true)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (local.Any())
            {
                //Eliminate stale models from the data store.
                var deletables = remote.Except(local, comparer);
                if (deletables.Any()) repository.EraseAll(deletables);

                //Insert new models and update existing models.
                repository.SaveAll(local, references);
            }
            else //No local models available => Remove all existing models from the data store
            {
                if (remote.Any()) repository.EraseAll(remote);
            }
        }

        private static Task SaveAllImplAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            bool references = true,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            return repository.SaveAllImplAsync<TKey, TModel, TRepository>(local, remote, EqualityComparer<TModel>.Default, references, token);
        }

        private static async Task SaveAllImplAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            IEqualityComparer<TModel> comparer,
            bool references = true,
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
                await repository.SaveAllAsync(local, references, token);
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
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> is potentially different from the <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save on the data store.</param>
        /// <param name="remote">Data model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The custom default value of the model type.</param>
        /// <param name="references">Should the references of the model also be saved?</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.Save(TModel, bool)"/>
        public static void Save<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            bool references = true)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            repository.Save<TKey, TModel, TRepository>(local, remote, @default, EqualityComparer<TModel>.Default, references);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> version of a data model asynchronously to the data store if it is available;
        /// otherwise erases the <paramref name="remote"/> version of that model asynchronously from the data store.
        /// <para/> The default equality comparer is used to check the availability of the <paramref name="local"/> version by comparing the it to a given <paramref name="default"/> value.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> is potentially different from the <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save asynchronously on the data store.</param>
        /// <param name="remote">TData model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The custom default value of the model type.</param>
        /// <param name="references">Should the references of the model also be saved?</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAsync(TModel, bool, CancellationToken)"/>
        public async static Task SaveAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            bool references = true,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            await repository.SaveAsync<TKey, TModel, TRepository>(local, remote, @default, EqualityComparer<TModel>.Default, references);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> version of a data model to the data store if it is available;
        /// otherwise erases the <paramref name="remote"/> version of that model from the data store.
        /// <para/> The provided equality comparer is used to check the availability of the <paramref name="local"/> version by comparing the it to a given <paramref name="default"/> value.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> is potentially different from the <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save on the data store.</param>
        /// <param name="remote">Data model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The custom default value of the model type.</param>
        /// <param name="comparer">The equality comparer that compares the <paramref name="local"/> version of a data model to a given <paramref name="default"/> value.</param>
        /// <param name="references">Should the references of the model also be saved?</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.Save(TModel, bool)"/>
        public static void Save<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            IEqualityComparer<TModel> comparer,
            bool references = true)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (!comparer.Equals(remote, @default)) repository.SaveImpl<TKey, TModel, TRepository>(local, remote, @default, comparer, references);
            else if (!comparer.Equals(local, @default)) repository.Save(local, references);
        }

        /// <summary>
        /// Saves the <paramref name="local"/> version of a data model asynchronously to the data store if it is available;
        /// otherwise erases the <paramref name="remote"/> version of that model from the data store.
        /// <para/> The provided equality comparer is used to check the availability of the <paramref name="local"/> version by comparing the it to a given <paramref name="default"/> value.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> is potentially different from the <paramref name="default"/> value.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify a data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The data model to save on the data store.</param>
        /// <param name="remote">Data model from the data store to compare with with <paramref name="local"/> model.</param>
        /// <param name="default">The custom default value of the model type.</param>
        /// <param name="comparer">The equality comparer that compares the <paramref name="local"/> version of a data model to a given <paramref name="default"/> value.</param>
        /// <param name="references">Should the references of the model also be saved?</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAsync(TModel, bool, CancellationToken)"/>
        public async static Task SaveAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            TModel local,
            TModel remote,
            TModel @default,
            IEqualityComparer<TModel> comparer,
            bool references = true,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (!comparer.Equals(remote, @default)) await repository.SaveImplAsync<TKey, TModel, TRepository>(local, remote, @default, comparer, references, token);
            else if (!comparer.Equals(local, @default)) await repository.SaveAsync(local, references, token);
        }

        /// <summary>
        /// Saves data models to the data store after synchronizing changes with existing models in the store.
        /// <para/>1. The <paramref name="local"/> sequence of models is non-empty:
        /// Remove the <paramref name="remote"/> sequence of models from the data store that are non-identical to those of the <paramref name="local"/> sequence
        /// and save all members of <paramref name="local"/> sequence.
        /// <para/>2. The <paramref name="local"/> sequence is empty: Remove all models of the <paramref name="remote"/> sequence from the data store.
        /// <para/> The default equality comparer is used to compare elements of the <paramref name="local"/> sequence against elements of the <paramref name="remote"/> sequence.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> sequence of data models is potentially non-empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        /// <param name="references">Should the references of the models also be saved?</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAll(IEnumerable{TModel}, bool)"/>
        public static void SaveAll<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            bool references = true)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            repository.SaveAll<TKey, TModel, TRepository>(local, remote, EqualityComparer<TModel>.Default, references);
        }

        /// <summary>
        /// Saves data models to the data store after synchronizing changes with existing models in the store.
        /// <para/>1. The <paramref name="local"/> sequence of models is non-empty:
        /// Remove the <paramref name="remote"/> sequence of models from the data store that are non-identical to those of the <paramref name="local"/> sequence
        /// and save all members of <paramref name="local"/> sequence.
        /// <para/>2. The <paramref name="local"/> sequence is empty: Remove all models of the <paramref name="remote"/> sequence from the data store.
        /// <para/> The provided equality comparer is used to compare elements of the <paramref name="local"/> sequence against elements of the <paramref name="remote"/> sequence.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> sequence of data models is potentially non-empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        /// <param name="comparer">The equality comparer that compares elements of the <paramref name="local"/> and <paramref name="remote"/> sequence of data models.</param>
        /// <param name="references">Should the references of the models also be saved?</param>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAll(IEnumerable{TModel}, bool)"/>
        public static void SaveAll<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            IEqualityComparer<TModel> comparer,
            bool references = true)
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (remote.Any()) repository.SaveAllImpl<TKey, TModel, TRepository>(local, remote, comparer);
            else if (local.Any()) repository.SaveAll(local);
        }

        /// <summary>
        /// Saves data models to the data store after synchronizing changes with existing models in the store.
        /// <para/>1. The <paramref name="local"/> sequence of models is non-empty:
        /// Remove the <paramref name="remote"/> sequence of models from the data store that are non-identical to those of the <paramref name="local"/> sequence
        /// and save all members of <paramref name="local"/> sequence.
        /// <para/>2. The <paramref name="local"/> sequence is empty: Remove all models of the <paramref name="remote"/> sequence from the data store.
        /// <para/> The default equality comparer is used to compare elements of the <paramref name="local"/> sequence against elements of the <paramref name="remote"/> sequence.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> sequence of data models is potentially non-empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        /// <param name="references">Should the references of the models also be saved?</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to save the <paramref name="local"/> sequence of a data models asynchronously to the data store.</returns>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAllAsync(IEnumerable{TModel}, bool, CancellationToken)"/>
        public static Task SaveAllAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            bool references = true,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            return repository.SaveAllAsync<TKey, TModel, TRepository>(local, remote, EqualityComparer<TModel>.Default, references, token);
        }

        /// <summary>
        /// Saves data models to the data store after synchronizing changes with existing models in the store.
        /// <para/>1. The <paramref name="local"/> sequence of models is non-empty:
        /// Remove the <paramref name="remote"/> sequence of models from the data store that are non-identical to those of the <paramref name="local"/> sequence
        /// and save all members of <paramref name="local"/> sequence.
        /// <para/>2. The <paramref name="local"/> sequence is empty: Remove all models of the <paramref name="remote"/> sequence from the data store.
        /// <para/> The provided equality comparer is used to compare elements of the <paramref name="local"/> sequence against elements of the <paramref name="remote"/> sequence.
        /// <para/> It is recommended to call this method, if <paramref name="remote"/> sequence of data models is potentially non-empty.
        /// </summary>
        /// <typeparam name="TKey">The type of the key to uniquely identify each data model.</typeparam>
        /// <typeparam name="TModel">The type of data model to save or erase.</typeparam>
        /// <typeparam name="TRepository">The type of repository that saves or erases a data model.</typeparam>
        /// <param name="repository">The repository that saves or erases a data model.</param>
        /// <param name="local">The sequence of data models to save on the data store.</param>
        /// <param name="remote">The sequence of data models from the data store to compare with the <paramref name="local"/> sequence of models.</param>
        /// <param name="comparer">The equality comparer that compares elements of the <paramref name="local"/> and <paramref name="remote"/> sequence of data models.</param>
        /// <param name="references">Should the references of the models also be saved?</param>
        /// <param name="token">Propagates the notification that the asynchronous operation should be cancelled.</param>
        /// <returns>A promise to save the <paramref name="local"/> sequence of a data models asynchronously to the data store.</returns>
        /// <seealso cref="IWriteRepository{TKey, TModel}.SaveAllAsync(IEnumerable{TModel}, bool, CancellationToken)"/>
        public async static Task SaveAllAsync<TKey, TModel, TRepository>(
            this TRepository repository,
            IEnumerable<TModel> local,
            IEnumerable<TModel> remote,
            IEqualityComparer<TModel> comparer,
            bool references = true,
            CancellationToken token = default(CancellationToken))
            where TRepository : IWriteRepository<TKey, TModel>, IEraseRepository<TKey, TModel>
            where TKey : IEquatable<TKey>, IComparable, IComparable<TKey>
        {
            if (remote.Any()) await repository.SaveAllImplAsync<TKey, TModel, TRepository>(local, remote, comparer, references, token);
            else if (local.Any()) await repository.SaveAllAsync(local, references, token);
        }
    }
}
