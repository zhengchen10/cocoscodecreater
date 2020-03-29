using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CocosCodeCreater
{
    public interface IPropertyListener
    {
        void onPropertyChanged(Object obj, String property, Object oldValue, Object newValue);
    }
}
