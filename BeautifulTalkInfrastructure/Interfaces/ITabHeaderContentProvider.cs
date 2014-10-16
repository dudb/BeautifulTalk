using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BeautifulTalkInfrastructure.Interfaces
{
    public interface ITabHeaderContentProvider<T>
    {
        T HeaderContent { get; }
        T SelectedHeaderContent { get; }
    }
}
