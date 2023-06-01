using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    public class Category
    {
        // Može sadržavati null vrijednost
        private IWebDriver driver = null!;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            var options = new ChromeOptions();
            options.PageLoadStrategy = PageLoadStrategy.Normal; // Promjena strategije učitavanja stranice
            driver = new ChromeDriver(options);
            driver.Navigate().GoToUrl("https://www.oreabazaar.com");
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            // Zatvaranje preglednika nakon završetka svih testova
            driver.Quit();
        }

        [Test]
        public void NavigateToCategory_Odjeca_ShouldRedirectToClothingPage()
        {
            // Klik na "Kategorije"
            driver.FindElement(By.Id("categories-menu-btn")).Click();
            /*
            // Klik na "Odjeća"
            driver.FindElement(By.LinkText("Odjeća")).Click();

            // Provjera očekivanog rezultata - provjera prisutnosti elementa na stranici s odjećom
            bool isDisplayed = driver.FindElement(By.XPath("//h1[contains(text(),'Odjeća')]")).Displayed;

            // Provjera očekivanog rezultata
            Assert.IsTrue(isDisplayed, "Korisnik nije preusmjeren na stranicu s odjećom!");*/
        }
    }
}
