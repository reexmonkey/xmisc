using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace reexjungle.xmisc.foundation.concretes
{
    #region extensions

    /// <summary>
    /// Provides useful shortcuts to throw exceptions from an element or sequence of elements.
    /// </summary>
    public static class ExceptionExtensions
    {
        /// <summary>
        /// Throws an ArgumentNullException if a value is null.
        /// </summary>
        /// <typeparam name="TValue">The type of the exception source</typeparam>
        /// <param name="source">The source of the exception</param>
        /// <param name="paramName">The name of the parameter that caused the exception</param>
        /// <param name="message">The exception message</param>
        public static void ThrowIfNull<TValue>(this TValue source, string paramName = null, string message = null)
        {
            if (source == null) throw new ArgumentNullException(paramName, message);
        }

        /// <summary>
        /// Throws an ArgumentNullException if a value is null.
        /// </summary>
        /// <typeparam name="TValue">The type of the exception source</typeparam>
        /// <param name="source">The source of the exception</param>
        /// <param name="message">The exception message</param>
        /// <param name="inner">The inner exception that caused the current exception, or a null reference</param>
        public static void ThrowIfNull<TValue>(this TValue source, string message, Exception inner = null)
        {
            if (source == null) throw new ArgumentNullException(message, inner);
        }

        /// <summary>
        /// Throws an ArgumentNullException if a value is null.
        /// </summary>
        /// <typeparam name="TValue">The type of the exception source</typeparam>
        /// <param name="source">The source of the exception</param>
        /// <param name="message">The exception message</param>
        /// <param name="inner">The inner exception that caused the current exception, or a null reference</param>
        public static void ThrowIfNullOrEmpty<TValue>(this IEnumerable<TValue> source, string message, Exception inner = null)
        {
            if (source.NullOrEmpty()) throw new ArgumentException(message, inner);
        }
    }

    #endregion extensions

    #region helpers

    /// <summary>
    /// Provides generic exception services
    /// </summary>
    /// <typeparam name="TValue">The type parameter for the generic exception</typeparam>
    public class TException<TValue> : Exception
    {
        private Type type = null;

        /// <summary>
        /// Gets the data type of the generic operator, T
        /// </summary>
        public Type Type
        {
            get { return type; }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public TException()
            : base()
        {
            type = typeof(TValue);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The exception message</param>
        public TException(string message)
            : base(message)
        {
            type = typeof(TValue);
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">The exception message</param>
        /// <param name="inner">The inner exception of this execption</param>
        public TException(string message, Exception inner)
            : base(message, inner)
        {
            type = typeof(TValue);
        }
    }

    #endregion helpers
}