using System;
using System.Runtime.Serialization;

namespace reexmonkey.xmisc.core.exceptions.generics
{
    /// <summary>
    /// Provides an <see cref="Exception"/> class that contains a context.
    /// </summary>
    /// <typeparam name="TContext">The type of exception context.</typeparam>
    public class Exception<TContext> : Exception
    {
        /// <summary>
        /// Gets the context of the exception.
        /// </summary>
        public TContext Context { get; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exception"/> class optionally with information on its context.
        /// </summary>
        /// <param name="context">The context of the exception.</param>
        public Exception(TContext context = default(TContext))
        {
            Context = context;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exception"/> class with the specified error message and optionally information on its context.
        /// </summary>
        /// <param name="message">The message that describes the exception.</param>
        /// <param name="context">The context of the exception.</param>
        public Exception(string message, TContext context = default(TContext)) : base(message)
        {
            Context = context;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Exception"/> class with the specified error message,
        /// the reference to the exception that caused this exception and optionally  contextual information.
        /// </summary>
        /// <param name="message"></param>
        /// <param name="innerException">The exception that caused this exception.</param>
        /// <param name="context">The context of this exception.</param>
        public Exception(string message, Exception innerException, TContext context = default(TContext)) : base(message, innerException)
        {
            Context = context;
        }

        protected Exception(SerializationInfo info, StreamingContext context) : base(info, context)
        {
            var deserialized = info.GetValue(nameof(Context), typeof(TContext));
            Context = (TContext)deserialized;
        }
    }
}