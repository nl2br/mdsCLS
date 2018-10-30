using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Common;
using System.IO;
using System.Xml;

namespace mdsCLS{
    public enum TYPECLIENT { normal, premium, asurveille }
    public enum RESULTAT { acceptee, accepteeRemise, refusee }

    public abstract class Client : IDesign
    {
        public TYPECLIENT Type { get; set; }
        public int Id { get; set; }
        public string Nom { get; set; }
        public Single Encours { get; set; }
        public Single Commande { get; set; }
        public Single Remise { get; set; }
        public Single AvantRemise { get; set; }
        public RESULTAT res_to_send { get; set; }
        public bool remiseEnCours { get; set; }

        public object Fill() { return null; }
        public object Set() { return null; }


        public Client(int pId, TYPECLIENT pType, string pNom, Single pEncours)
        {
            res_to_send = RESULTAT.acceptee;
            Type = pType;
            Encours = pEncours;
            Id = pId;
            Nom = pNom;
        }

        public virtual RESULTAT Calcul(Single pMontantCommande, Single pRemise = 0)
        {
            res_to_send = RESULTAT.acceptee;
            return res_to_send;
        }

        protected Single AppliquerRemise(Single pRemise)
        {
            remiseEnCours = true;
            return Commande - ((Commande * pRemise) % 100);
        }

        public string ToJson()
        {
            return "toJson";
        }

        public string ToXML()
        {
            return "toJson";
        }

        public void SaveXML()
        {
            XmlDocument oDoc = new XmlDocument();
            XmlNode root = oDoc.CreateElement("CLIENTS");

            if (!File.Exists("clients.xml"))
            {
                XmlDeclaration dec = oDoc.CreateXmlDeclaration("1.0", null, null);
                oDoc.AppendChild(dec);// ajout the declaration

                oDoc.AppendChild(root);// ajout the root element
                oDoc.Save("clients.xml");
            }

            oDoc.Load("clients.xml");

            XmlNode oNoeudClient = oDoc.CreateElement("CLIENT");
            root.AppendChild(oNoeudClient);

            XmlAttribute oAttId = oDoc.CreateAttribute("ID");
            oAttId.Value = "100";
            oNoeudClient.Attributes.Append(oAttId);

            XmlNode oNoeudNom = oDoc.CreateElement("Nom");
            oNoeudNom.InnerText = "MonNom";

            XmlNode oNoeudType = oDoc.CreateElement("Type");
            oNoeudType.InnerText = "NORMAL";

            XmlNode oNoeudencours = oDoc.CreateElement("EnCours");
            oNoeudencours.InnerText = "196.66";

            oNoeudClient.AppendChild(oNoeudNom);
            oNoeudClient.AppendChild(oNoeudType);
            oNoeudClient.AppendChild(oNoeudencours);

            oDoc.Save("clients.xml");

            //chercher le noeud qui correspond à son client
            //XmlElement oNode = (XmlElement)oRoot.SelectSingleNode("//CLIENT[@id" + this.Id + "]");

        }

        public void Save()
        {
            // si fichier existe pas
            if (!File.Exists("client" + this.Id + ".csv"))
            {
                using (FileStream fl = File.Create("client" + this.Id + ".csv"))
                {
                    StreamWriter wfl = new StreamWriter(fl);
                    string ligne = this.Id + ";" + TYPECLIENT.normal + ";" + this.Nom + ";" + this.Encours;
                    wfl.WriteLine(ligne);
                    wfl.Close();
                }
            }
        }

        static Client LoadById(int pId)
        {
            try
            {
                Client oReturn = null;
                using (FileStream fl = File.Open("client" + pId + ".csv", FileMode.Open))
                {
                    StreamReader rfl = new StreamReader(fl);
                    string ligneFile = rfl.ReadLine();
                    string[] values = ligneFile.Split(';');
                    rfl.Close();

                    switch (values[1])
                    {
                        case "normal":
                            oReturn = new ClientNormal(Convert.ToInt16(values[0]), Convert.ToString(values[2]), Convert.ToSingle(values[3]));
                            break;
                        case "premium":
                            oReturn = new ClientPremium(Convert.ToInt16(values[0]), Convert.ToString(values[2]), Convert.ToSingle(values[3]));
                            break;
                        case "asurveille":
                            oReturn = new ClientAsurveillee(Convert.ToInt16(values[0]), Convert.ToString(values[2]), Convert.ToSingle(values[3]));
                            break;
                        default:
                            throw new Exception("Type de client invalide");
                    }
                    oReturn.Id = Convert.ToInt32(values[0]);
                    oReturn.Nom = values[1];
                    return oReturn;

                }
            }
            catch (Exception)
            {
               throw new Exception("erreur"); ;
            }

        }

        public bool Delete()
        {
            //si fichier existe on le delete
            if(File.Exists("client" + this.Id + ".csv"))
            {
                File.Delete("client" + this.Id + ".csv");
                return true;
            }

            return false;
        }
    }

    public class ClientNormal:Client
    {
        public ClientNormal()
            : base(0, TYPECLIENT.normal, "", 0) {
            MaListeProduit = new List<Produit>();
        }

        public ClientNormal(int pId, string pNom, Single pEncours)
            :base(pId, TYPECLIENT.normal, pNom, pEncours) {
            MaListeProduit = new List<Produit>();
        }


        public override RESULTAT Calcul(Single pMontantCommande, Single pRemise = 0)
        {
            Commande = pMontantCommande;
            //appliquer remise sur nombre de commande au besoin
            if (pRemise != 0) { Commande = AppliquerRemise(pRemise); }

            if (((Encours >= 1000 && Encours <= 3000) && Commande > 3000) || (Encours > 3000 && Commande > 1000))
            { return RESULTAT.refusee; }
            else {
                if (remiseEnCours) { return RESULTAT.accepteeRemise; } else { return RESULTAT.acceptee; }
            }
        }

        public List<Produit> MaListeProduit { get; set; }

        //public object Fill(DbDataReader pR) { return null; }
        //public object Set(DbCommand pC) { return null; }

        public void Insert() {
            
            //this.Save();
            this.SaveXML();

        }
        public ClientNormal Load(string pId) {
            this.Fill();
            return null;
        }
        public void Update(ClientNormal pObj) {  }
        public void Delete(string pId) { }

    }

    public class ClientPremium : Client
    {
        public ClientPremium(int pId, string pNom, Single pEncours)
            : base(pId, TYPECLIENT.premium, pNom, pEncours) { }

        public override RESULTAT Calcul(Single pMontantCommande, Single pRemise = 0)
        {
            Commande = pMontantCommande;
            //appliquer remise sur nombre de commande au besoin
            if (pRemise != 0) {Commande = AppliquerRemise(pRemise);}

            if (Encours > 3000 && Commande > 5000)
            { return RESULTAT.refusee; }
            else {
                if (remiseEnCours){ return RESULTAT.accepteeRemise; }else{ return RESULTAT.acceptee; }
            }
        }

 
    }

    public class ClientAsurveillee : Client
    {
        public ClientAsurveillee(int pId, string pNom, Single pEncours)
            : base(pId, TYPECLIENT.asurveille, pNom, pEncours) { }

        public override RESULTAT Calcul(Single pMontantCommande, Single pRemise = 0)
        {
            Commande = pMontantCommande;
            //appliquer remise sur nombre de commande au besoin
            if (pRemise != 0) { Commande = AppliquerRemise(pRemise); }

            if (Commande > 5000 || (Encours > 1000 && Commande > 1000))
            { return RESULTAT.refusee; }
            else
            {
                if (remiseEnCours) { return RESULTAT.accepteeRemise; } else { return RESULTAT.acceptee; }
            }
        }

       

        
    }
    /*
    public class ClientBanni : Client
    {
        public ClientBanni(Single pEncours)
            : base(TYPECLIENT.asurveille, pEncours) { }

        public override RESULTAT Calcul(Single pMontantCommande, Single pRemise = 0)
        {
            return RESULTAT.refusee;
        }


    }
    */
}
