using System;
using System.Collections.Generic;
using System.Text;

namespace ModelWrapper.Core
{
    interface IWrap<TModel> where TModel: class
    {
        TModel Put(TModel model);
        TModel Patch(TModel model);

        Dictionary<string, object> AsDictionary();
    }
}
