using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using mdsCLS;
using System.Xml;

namespace mdsCNSL
{

    public class Program
    {
        private static TYPECLIENT SaisieType()
        {
            
            TYPECLIENT res_return = TYPECLIENT.normal;
            bool succes = false;

            while (!succes)
            {
                Console.WriteLine("Saisir le type de client N / P / S");
                string user_message = Console.ReadLine();

                switch (user_message)
                {
                    case "n":
                        succes = true;
                        res_return = TYPECLIENT.normal;
                        break;
                    case "p":
                        succes = true;
                        res_return = TYPECLIENT.premium;
                        break;
                    case "s":
                        succes = true;
                        res_return = TYPECLIENT.asurveille;
                        break;

                    default:
                        break;
                }
            }

            return res_return;
        }

        private static Single SaisieMontant(string label)
        {
            bool succes = false;
            Single res_saisie = 0;
            while (!succes)
            {
                Console.WriteLine("Saisir le montant " + label + " :");
                try
                {
                    res_saisie = Convert.ToSingle(Console.ReadLine());
                    succes = true;
                }
                catch (Exception exp)
                {
                    Console.WriteLine(exp.Message);
                } 
            }
            return res_saisie;
        }

        public static void Main(string[] args)
        {
            var htmlRead = new HtmlRead();
            htmlRead.Read();

            
            // saveXML
            var client = new ClientSerialize();
            client.Adresse = "12 allée des cerisiers";
            client.Code = "1";

            var produit1 = new ProduitSerialize();
            produit1.Code = "1";
            produit1.Nom = "ToyStory";
            produit1.PrixUnitaire = 12;

            var produit2 = new ProduitSerialize();
            produit2.Code = "2";
            produit2.Nom = "Mickey";
            produit2.PrixUnitaire = 15;

            var ligne1 = new LigneCommande();
            ligne1.NumOrder = 1;
            ligne1.Produit = produit1;
            ligne1.Quantite = 2;
            ligne1.PrixUnitaire = 12;

            var ligne2 = new LigneCommande();
            ligne2.NumOrder = 1;
            ligne2.Produit = produit2;
            ligne2.Quantite = 2;
            ligne2.PrixUnitaire = 12;

            //List<LigneCommande> lignes = new List<LigneCommande>() {ligne1, ligne2};

            var commande = new CommandeSerialize();
            commande.Code = "1";
            commande.Client = client;
            commande.DateCommande = DateTime.Now;
            commande.Lignes.Add(ligne1);
            commande.Lignes.Add(ligne2);

            commande.SaveXML();

            //var commande2 = (CommandeSerialize)commande.Clone();

            //commande2.Code = "5435454";
            //commande2.SaveXML();

            // LoadXML
            //foreach(var cmd in commande.LoadXML())
            //{
            //    foreach (var lgs in cmd.lignes)
            //    {

            //    }
            //}




            // saveXML
            ClientNormal oCn = new ClientNormal(26, "Jean", 5000);
            oCn.Insert(); 


            // Xpath
            List<Categorie> liste = Categorie.GetList();

            foreach(var categ in liste)
            {
                //Console.WriteLine("{0}: catégorie id{1} : {2} {3} {4} ",categ.Type,categ.Id, categ.LabelGip, categ.Name, categ.Tdpp);
            }


            //ClientNormal CliN = new ClientNormal(1,"Jean",5000);
            //CliN.Save();
            //Mapper<ClientNormal> crudClient = new Mapper<ClientNormal>;
            //crudClient.Insert(Cli);

            //Client objClient = new Client(SaisieType(), SaisieMontant("de l'encours"));
            //objClient.Calcul(objClient.Remise);

            //Console.WriteLine("Cette commande est " + objClient.res_to_send);
            //Console.ReadKey();
        }
    }
}
