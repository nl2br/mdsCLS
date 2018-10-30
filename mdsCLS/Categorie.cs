using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace mdsCLS
{
    public class Categorie
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public string Tdpc { get; set; }
        public int Tdpp { get; set; }
        public int Tdec { get; set; }
        public int Tdep { get; set; }
        public string LabelGip { get; set; }
        public int TypeValeur { get; set; }
        public List<Single> LstValeurs = null;

        public Categorie() { }

        public static List<Categorie> GetList()
        {
            var liste = new List<Categorie>();

            var doc = new XmlDocument();
            doc.Load("TypesCategories.xml");
            XmlNodeList nodes = doc.SelectNodes("//categorie[@id]");
           
            foreach(XmlNode node in nodes)
            {
                var categorie = new Categorie();
                
                // recupère le nom du type
                XmlNode parent = node.SelectSingleNode("../../libelle");
                categorie.Type = parent.InnerText;

                // recupère les valeures d'attributs
                var attributes = node.Attributes;
                foreach(XmlNode attribute in attributes)
                {
                    switch (attribute.Name)
                    {
                        case "id":
                            categorie.Id = Convert.ToInt32(attribute.Value);
                            break;
                        case "tdpc":
                            categorie.Tdpc = Convert.ToString(attribute.Value);
                            break;
                        case "tdpp":
                            categorie.Tdpp = Convert.ToInt32(attribute.Value);
                            break;
                        case "tdec":
                            categorie.Tdec = Convert.ToInt32(attribute.Value);
                            break;
                        case "tdep":
                            categorie.Tdep = Convert.ToInt32(attribute.Value);
                            break;
                        case "label_gip":
                            categorie.LabelGip = Convert.ToString(attribute.Value);
                            break;
                        default:
                            continue;
                    }
                    
                }
                liste.Add(categorie);
            }
            return liste;
        }

    }
}
