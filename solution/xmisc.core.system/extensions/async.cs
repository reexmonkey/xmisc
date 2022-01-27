using System;
using System.Threading;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.core.system.extensions
{
    /// <summary>
    /// Provides extensions to the Async feature of C#
    /// </summary>
    public static class AsyncExtensions
    {
        private static readonly TaskFactory factory = new TaskFactory(
            CancellationToken.None,
            TaskCreationOptions.None,
            TaskContinuationOptions.None,
            TaskScheduler.Default);

        /// <summary>
        /// Calls a given task that returns a value sychronously in an I/O-bound operation.
        /// </summary>
        /// <typeparam name="TResult">The type of return value.</typeparam>
        /// <param name="func">The lambda function that encapsulates the task to call.</param>
        /// <returns>The return value of the encapsulated task.</returns>
        public static TResult ToSync<TResult>(this Func<Task<TResult>> func) => func().GetAwaiter().GetResult();

        /// <summary>
        /// Calls a given task that does not return a value sychronously in an I/O-bound operation.
        /// </summary>
        /// <param name="func">The lambda function that encapsulates the task to call.</param>
        public static void ToSync(this Func<Task> func) => func().GetAwaiter().GetResult();

        /// <summary>
        /// Runs a given task that returns a value sychronously in a CPU-bound operation.
        /// </summary>
        /// <typeparam name="TResult">The type of return value.</typeparam>
        /// <param name="func">The lambda function that encapsulates the task to call.</param>
        /// <returns>The return value of the encapsulated task.</returns>
        public static TResult RunSync<TResult>(this Func<Task<TResult>> func)
        {
            return factory.StartNew(func)
                .Unwrap()
                .GetAwaiter()
                .GetResult();
        }

        /// <summary>
        /// Runs a given task that does not return a value sychronously in an CPU-bound operation.
        /// </summary>
        /// <typeparam name="TResult">The type of return value.</typeparam>
        /// <param name="func">The lambda function that encapsulates the task to call.</param>
        public static void RunSync<TResult>(this Func<Task> func)
        {
            factory.StartNew(func)
               .Unwrap()
               .GetAwaiter()
               .GetResult();
        }

        /// <summary>
        /// Calls a given method that returns a value asychronously in an I/O-bound operation.
        /// <para /> Note: This wrapper method is just an "async" adapter and not an actual asynchronous method.
        /// </summary>
        /// <typeparam name="TResult">The type of return value.</typeparam>
        /// <param name="func">The lambda function that encapsulates the method to call.</param>
        /// <param name="cancellation">The token that propagates the notification on the cancellation of the operation.</param>
        /// <returns>A task that promises to run and return a value.</returns>
        public static Task<TResult> ToAsync<TResult>(this Func<TResult> func, CancellationToken cancellation = default)
        {
            if (cancellation.IsCancellationRequested) return Task.FromCanceled<TResult>(cancellation);
            try
            {
                return Task.FromResult(func());
            }
            catch (Exception e)
            {
                return Task.FromException<TResult>(e);
            }
        }

        /// <summary>
        ///  Calls a given method that does not return a value asychronously in an I/O-bound operation.
        ///  <para /> Note: This wrapper method is just an "async" adapter and not an actual asynchronous method.
        /// </summary>
        /// <param name="action">The lambda function that encapsulates the method to call.</param>
        /// <param name="cancellation">The token that propagates the notification on the cancellation of the operation.</param>
        /// <returns>A task that promises to execute and return no value.</returns>
        public static Task ToAsync(this Action action, CancellationToken cancellation = default)
        {
            if (cancellation.IsCancellationRequested) return Task.FromCanceled(cancellation);
            try
            {
                action();
                return Task.FromResult(true);
            }
            catch (Exception e)
            {
                return Task.FromException(e);
            }
        }

        /// <summary>
        /// Runs a given method that returns a value asychronously in a CPU-bound operation.
        /// </summary>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="func">The lambda function that encapsulates the method to call.</param>
        /// <param name="cancellation">The token that propagates the notification on the cancellation of the operation.</param>
        /// <returns>A task that promises to run and return a value.</returns>
        public static Task<TResult> RunAsync<TResult>(this Func<TResult> func, CancellationToken cancellation = default)
        {
            return factory.StartNew(func, cancellation);
        }

        /// <summary>
        /// Runs a given method that does not return a value asychronously in a CPU-bound operation.
        /// </summary>
        /// <param name="action"></param>
        /// <param name="cancellation">The token that propagates the notification on the cancellation of the operation.</param>
        /// <returns>A task that promises to execute and return no value.</returns>
        public static Task RunAsync(this Action action, CancellationToken cancellation = default)
        {
            return factory.StartNew(action);
        }
    }
}
