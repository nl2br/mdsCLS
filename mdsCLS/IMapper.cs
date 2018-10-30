using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;

namespace mdsCLS
{
    public interface IMapper
    {
        bool Fill();
        bool Set();
    }
}
