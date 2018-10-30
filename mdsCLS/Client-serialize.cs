using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Xml.Serialization;
using System.Globalization;

namespace mdsCLS
{
    public class ClientSerialize
    {
        public string Code { get; set; }
        public string Adresse { get; set; }

        public ClientSerialize(){}
    }
    [XmlRoot("Commande")]
    public class CommandeSerialize : ICloneable
    {
        public string Code { get; set; }
        [XmlIgnore]
        public DateTime DateCommande;
        [XmlElement("DateCommande")]
        public string DateCommandeFormatted
        {
            get { return DateCommande.ToShortDateString(); }
            set { DateCommande = Convert.ToDateTime(value); }
        }
        public ClientSerialize Client { get; set; }
        public List<LigneCommande> Lignes { get; set; }

        public CommandeSerialize()
        {
            Lignes = new List<LigneCommande>();
        }

        public void SaveXML()
        {
            string file = "Commande.xml";
            var xs = new XmlSerializer(typeof(CommandeSerialize));
            using(var sw = new StreamWriter(file))
            {
                xs.Serialize(sw,this);
            }
        }

        public CommandeSerialize LoadXML()
        {
            string file = "Commande.xml";
            var xs = new XmlSerializer(typeof(CommandeSerialize));
            using (var sr = new StreamReader(file))
            {
                return (CommandeSerialize)xs.Deserialize(sr);
            }
        }

        public object Clone()
        {
            // creer un ser de commande
            var xs = new XmlSerializer(typeof(CommandeSerialize));

            // creer un SW
            var sw = new StringWriter();

            // on serialise l'objet this dans le stringWriter (dans la zone memoire)
            xs.Serialize(sw, this);
            sw.Close();

            // on recupere l'info dans dans le StringWriter (zone mémoire) 
            var sr = new StringReader(sw.ToString());

            // on deserialize pour recupérer notre nouvel objet
            return xs.Deserialize(sr);
        }


    }

    public class LigneCommande
    {
        public int NumOrder { get; set; }
        public int Quantite { get; set; }
        public decimal PrixUnitaire { get; set; }
        public ProduitSerialize Produit { get; set; }

    }

    public class ProduitSerialize
    {
        public string Code { get; set; }
        public string Nom { get; set; }
        public decimal PrixUnitaire { get; set; }

        public ProduitSerialize()
        {
        }
    }
}
