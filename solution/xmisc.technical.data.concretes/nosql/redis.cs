using reexjungle.xmisc.foundation.concretes;
using reexjungle.xmisc.foundation.contracts;
using ServiceStack.Redis;
using System;
using System.Collections.Generic;
using System.Linq;

namespace reexjungle.xmisc.technical.data.concretes.nosql
{
    public static class RedisExtensions
    {
        public static string CreateUrn<T>(this Guid key)
        {
            return string.Format("urn:{0}:{1}", typeof(T).Name.ToLowerInvariant(), key);
        }

        /// <summary>
        /// Merges the elements of a sequence to those of another and stores the result in a Redis data store.
        /// </summary>
        /// <param name="redis">The Redis client to manage the store operation</param>
        /// <param name="entities">The target sequence, whose elements are to be merged. </param>
        /// <param name="other">The source sequence, whose elements are merged with those of the target.</param>
        /// <param name="transaction">The Redis transaction to ensure the merging and storage are conducted in one atomic operation.</param>
        /// <typeparam name="TEntity">The type of elements to merge</typeparam>
        public static void MergeAll<TEntity>(
            this IRedisClient redis,
            IEnumerable<TEntity> entities,
            IEnumerable<TEntity> other,
            IRedisTransaction transaction)
            where TEntity : class, IContainsKey<Guid>, new()
        {
            redis.MergeAll<TEntity, Guid>(entities, other, transaction);
        }

        /// <summary>
        /// Merges the elements of a sequence to those of another and stores the result in a Redis data store.
        /// </summary>
        /// <param name="redis">The Redis client to manage the store operation</param>
        /// <param name="entities">The target sequence, whose elements are to be merged. </param>
        /// <param name="other">The source sequence, whose elements are merged with those of the target.</param>
        /// <param name="transaction">The Redis transaction to ensure the merging and storage are conducted in one atomic operation.</param>
        /// <typeparam name="TEntity">The type of elements to merge</typeparam>
        /// <typeparam name="TKey">The type of key to identify each element uniquely in the sequences.</typeparam>
        public static void MergeAll<TEntity, TKey>(
            this IRedisClient redis,
            IEnumerable<TEntity> entities,
            IEnumerable<TEntity> other,
            IRedisTransaction transaction)
            where TKey : IEquatable<TKey>, IComparable<TKey>
            where TEntity : class, IContainsKey<TKey>, new()
        {
            if (!other.NullOrEmpty())
            {
                var incoming = entities.Except(other).ToList();
                if (!incoming.NullOrEmpty()) transaction.QueueCommand(x => x.StoreAll(incoming));

                var outgoing = other.Except(entities).ToList();
                if (!outgoing.NullOrEmpty())
                    transaction.QueueCommand(x => x.As<TEntity>().DeleteByIds(outgoing.Select(y => y.Id)));
            }
            else transaction.QueueCommand(x => x.StoreAll(entities));
        }

        /// <summary>
        /// Removes all elements of a sequence from a Redis store.
        /// </summary>
        /// <param name="redis">The Redis client to manage the removal.</param>
        /// <param name="entities">The sequence of elements to be removed.</param>
        /// <param name="transaction">The transaction to ensure the removal is done in one operation.</param>
        /// <typeparam name="TEntities">The type of elements to remove.</typeparam>
        public static void RemoveAll<TEntities>(this IRedisClient redis, IEnumerable<TEntities> entities, IRedisTransaction transaction)
            where TEntities : class, IContainsKey<Guid>, new()
        {
            redis.RemoveAll<TEntities, Guid>(entities, transaction);
        }

        /// <summary>
        /// Removes all elements of a sequence from a Redis store.
        /// </summary>
        /// <param name="redis">The Redis client to manage the removal.</param>
        /// <param name="entities">The sequence of elements to be removed.</param>
        /// <param name="transaction">The transaction to ensure the removal is done in one operation.</param>
        /// <typeparam name="TEntities">The type of elements to remove.</typeparam>
        /// <typeparam name="TKey">The type of unique key used to identify each element in the sequence.</typeparam>
        public static void RemoveAll<TEntities, TKey>(this IRedisClient redis, IEnumerable<TEntities> entities, IRedisTransaction transaction)
            where TKey : IEquatable<TKey>, IComparable<TKey>
            where TEntities : class, IContainsKey<TKey>, new()
        {
            if (!entities.NullOrEmpty())
            {
                transaction.QueueCommand(x => x.As<TEntities>().DeleteByIds(entities.Select(y => y.Id)));
            }
        }
    }
}