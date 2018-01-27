using System;
using System.Collections.Generic;
using System.Text;

namespace reexmonkey.xmisc.backbone.net.contracts.clients
{
    public interface ICommonClient
    {
        Uri BaseUri { get; }
        string UserAgent { get; set; }
    }
}
