using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace mdsCLS
{
    public class Produit
    {
        public int Code { get; set; }
        public string Libelle { get; set; }
        public Single Prix { get; set; }

        public Produit(string Code, string Libelle, Single Prix) { }
    }
}
