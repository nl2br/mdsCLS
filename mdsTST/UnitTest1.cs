using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using mdsCLS;
using System.Xml;
using System.Xml.Serialization;

namespace mdsTST
{
    [TestClass]
    public class UnitTest1
    {

        [TestMethod]
        public void Test()
        {
            Client[] tab = new Client[3];

            tab[0] = new ClientPremium(1, "Jacques", 2000);
            tab[1] = new ClientAsurveillee(1, "Jacques", 2000);
            tab[2] = new ClientNormal(1,"Jacques",2000);

            foreach(Client item in tab)
            {
                RESULTAT response = item.Calcul(1001, 0);
                Console.WriteLine("le client " + item.Type.ToString() + 
                    " avec un encours de " + item.Encours +
                    " a " + response + " la commande de 1001€");
            }

        }

        [TestMethod]
        public void TestCliPremEc3000Cmd5000()
        {
            ClientPremium objClient = new ClientPremium(1, "Jacques", 3000);
            Assert.AreEqual(objClient.Calcul(5000, objClient.Remise), RESULTAT.acceptee);
        }

        [TestMethod]
        public void TestCliPremEc2999Cmd4999()
        {
            ClientPremium objClient = new ClientPremium(1, "Jacques", 2999);
            Assert.AreEqual(objClient.Calcul(4999, objClient.Remise), RESULTAT.acceptee);
        }

        [TestMethod]
        public void TestCliPremEc3000Cmd5000Remise10()
        {
            ClientPremium objClient = new ClientPremium(1, "Jacques", 5000);
            Assert.AreEqual(objClient.Calcul(3000, objClient.Remise), RESULTAT.accepteeRemise);
        }

        [TestMethod]
        public void TestCliPremEc2999Cmd4999Remise10()
        {
            ClientPremium objClient = new ClientPremium(1, "Jacques", 5001);
            Assert.AreEqual(objClient.Calcul(3001, 10), RESULTAT.accepteeRemise);
        }

        [TestMethod]
        public void TestCliAsurvEc1001Cmd1001()
        {
            ClientAsurveillee objClient = new ClientAsurveillee(1, "Jacques", 1001);
            Assert.AreEqual(objClient.Calcul(1001, objClient.Remise), RESULTAT.refusee);
        }

        [TestMethod]
        public void TestCliAsurvEc0Cmd5001()
        {
            ClientAsurveillee objClient = new ClientAsurveillee(1, "Jacques", 5001);
            Assert.AreEqual(objClient.Calcul(0, objClient.Remise), RESULTAT.refusee);
        }

        [TestMethod]
        public void TestCliAsurvEc0Cmd5000()
        {
            ClientAsurveillee objClient = new ClientAsurveillee(1, "Jacques", 5000);
            Assert.AreEqual(objClient.Calcul(0, objClient.Remise), RESULTAT.acceptee);
        }

        [TestMethod]
        public void TestCliNormEc2000Cmd900()
        {
            ClientAsurveillee objClient = new ClientAsurveillee(1, "Jacques", 900);
            Assert.AreEqual(objClient.Calcul(2000, objClient.Remise), RESULTAT.acceptee);
        }

        [TestMethod]
        public void SaveClient()
        {
            ClientNormal oCn = new ClientNormal(26,"Jean", 5000);
            oCn.Insert();
        }

        [TestMethod]
        public void LoadClient()
        {

        }


        [TestMethod]
        public void TestSerialize()
        {
            ClientNormal oClient = new ClientNormal(12, "jacques", 230);
            Produit oProduit = new Produit("P0001", "Jouet", 20);

            oClient.MaListeProduit.Add(oProduit);

            XmlSerializer oSer = new XmlSerializer(typeof(ClientNormal));
            using (System.IO.StreamWriter oWs = new System.IO.StreamWriter("clientser.xml"))
            {
                oSer.Serialize(oWs, oClient);
            }

            using (System.IO.StreamReader oSr = new System.IO.StreamReader("clientser.xml"))
            {
                oClient = (ClientNormal)oSer.Deserialize(oSr);
            }

        }
    }
}
