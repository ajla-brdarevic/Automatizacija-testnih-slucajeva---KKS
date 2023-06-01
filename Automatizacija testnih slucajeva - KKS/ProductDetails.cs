using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    public class ProductDetails
    {
        private IWebDriver driver = null!;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.oreabazaar.com");
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            driver.Quit();
        }

        [Test]
        public void TestNavigacijaDoDetaljaProizvoda()
        {
            var proizvodLink = driver.FindElement(By.XPath("//h5[contains(text(), 'Rozi snovi')]/ancestor::div[@class='col-xs-6 col-sm-4 col-md-2 product-item']"));
            proizvodLink.Click();

            // Pričekajte neko vrijeme da se stranica učita
            Thread.Sleep(2000); // Pauzirajte izvršavanje na 2 sekunde (prilagodite prema potrebi)

            // Provjerite je li korisnik preusmjeren na stranicu detalja o proizvodu
            var expectedUrl = "https://www.oreabazaar.com/bs/product/11950/rozi-snovi";
            bool isDetaljiProizvodaDisplayed = driver.Url == expectedUrl;

            // Provjera da li je korisnik uspješno preusmjeren na stranicu detalja o proizvodu
            Assert.IsTrue(isDetaljiProizvodaDisplayed, "Korisnik nije preusmjeren na stranicu detalja o proizvodu.");
        }

    }
}
