using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdsCLS
{
    public abstract class Mapper<T> where T:IMapper
    {
        public abstract object Insert(T pObj);
        public abstract T Load(string pId);
        public abstract void Update(T pObj);
        public abstract void Delete(string pId);

        //bool Fill() { return true; }
        //bool Set() { return true; }
    }
}
