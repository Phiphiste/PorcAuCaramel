using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Model.Test
{
    [TestClass]
    public class ModelTest
    {
        [TestMethod]
        public void QueryReturnsAtLeastOneResult()
        {
            string url = "https://query.wikidata.org/sparql?format=json&action=wbgetentities&query=SELECT%20%3Fimage%20%3FitemLabel%20%3Fhauteur%20%3Fmasse%20%3Fposition_de_jeu_sp_cialit_Label%20%3Fpays_de_nationalit_Label%20%3Fdate_de_naissance%20%3Fmembre_de_l__quipe_de_sportLabel%20%3Fd_but_de_la_p_riode_d_activit_%20%3Ffin_de_la_p_riode_d_activit_%20WHERE%20%7B%0A%20%20SERVICE%20wikibase%3Alabel%20%7B%20bd%3AserviceParam%20wikibase%3Alanguage%20%22%5BAUTO_LANGUAGE%5D%2Cen%22.%20%7D%0A%20%20%3Fitem%20wdt%3AP31%20wd%3AQ5%3B%0A%20%20%20%20wdt%3AP106%20wd%3AQ3665646%3B%0A%20%20%20%20wdt%3AP2685%20%3Fid.%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2048%20%3Fhauteur.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP54%20%3Fmembre_de_l__quipe_de_sport.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2067%20%3Fmasse.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP413%20%3Fposition_de_jeu_sp_cialit_.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP27%20%3Fpays_de_nationalit_.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP569%20%3Fdate_de_naissance.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2561%20%3Fnom.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2031%20%3Fd_but_de_la_p_riode_d_activit_.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP2032%20%3Ffin_de_la_p_riode_d_activit_.%20%7D%0A%20%20OPTIONAL%20%7B%20%3Fitem%20wdt%3AP18%20%3Fimage.%20%7D%0A%7D%0ALIMIT%20100";
            WpfApp1.Model model = new WpfApp1.Model(url);
            Assert.AreNotEqual(0, model.Players);
        }
    }
}
