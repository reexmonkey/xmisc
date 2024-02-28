using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace reexmonkey.xmisc.backbone.repositories.contracts
{
    /// <summary>
    /// Specifies whether a data model can marked for deletion or not.
    /// </summary>
    /// <typeparam name="TModel">The type of data model.</typeparam>
    public interface ISupportTrashing<in TModel>
    {
        /// <summary>
        /// Specifies whether the data model of type <typeparamref name="TModel"/> has been marked for deletion.
        /// <para/> True if the data model has been marked for deletion; otherwise, false.
        /// </summary>
        bool MarkedForDeletion { get; set; }
    }
}