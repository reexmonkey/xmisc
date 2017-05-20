using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace xmisc.backbone.identity.contracts.generators
{

    public interface IFingerprintGenerator<out TKey>
    {
        TKey GetFingerprint<TModel>(TModel model);
    }
}
