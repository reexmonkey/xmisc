using System;

namespace reexjungle.xmisc.technical.data.concretes.common
{
    /// <summary>
    /// Represents conjunctions to connect ORMlite queries
    /// </summary>
    public enum Conjunctor
    {
        /// <summary>
        /// Keyword for the AND conjunction
        /// </summary>
        And,

        /// <summary>
        /// Keyword for the OR conjunction
        /// </summary>
        Or
    }

    /// <summary>
    /// Represents the type of database join for an Object Relational Mapper (ORM).
    /// </summary>
    [Flags]
    public enum JoinMode
    {
        /// <summary>
        /// Inner Join
        /// </summary>
        Inner = 0x0001,

        /// <summary>
        /// Outer Join
        /// </summary>
        Outer = 0x0002,

        /// <summary>
        /// Left Join
        /// </summary>
        Left = 0x0004,

        /// <summary>
        /// Right join
        /// </summary>
        Right = 0x0008,

        /// <summary>
        /// Full Join
        /// </summary>
        Full = 0x0010
    }
}