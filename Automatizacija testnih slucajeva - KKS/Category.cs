using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using OpenQA.Selenium;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    public class Category
    {
        private IWebDriver driver = null!;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Postavljanje WebDrivera i otvaranje stranice samo jednom prije početka svih testova
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.oreabazaar.com/bs/end_user/auth/login");

            // Unos ispravnih kredencijala za prijavu
            string email = "ajlabrdarevic@gmail.com";
            string password = "sifra123";

            // Unos emaila i lozinke
            driver.FindElement(By.Id("username")).SendKeys(email);
            driver.FindElement(By.Id("password")).SendKeys(password);

            // Klik na gumb za prijavu
            driver.FindElement(By.CssSelector(".green.login-register-button")).Click();

            // Pričekaj da se prijaviš i otvori početna stranica
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.Id("user-dropdown-menu")).Displayed);

        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            // Zatvaranje preglednika nakon završetka svih testova
            driver.Quit();
        }

        [Test]
        public void CheckCategory_ClothingExists()
        {
            // Provjeri da li je korisnik ulogovan (provjera prisutnosti elementa koji se prikazuje samo kad je korisnik ulogovan)
            bool isLoggedIn = driver.FindElement(By.Id("user-dropdown-menu")).Displayed;
            Assert.IsTrue(isLoggedIn, "Korisnik nije ulogovan.");

            // Pronađi gumb za kategorije
            IWebElement categoriesButton = driver.FindElement(By.Id("categories-menu-btn"));

            // Klikni na gumb za kategorije
            categoriesButton.Click();

            // Pričekaj da se prikažu kategorije
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(5));
            wait.Until(driver => driver.FindElement(By.Id("user-dropdown-menu")).Displayed);

            // Pronađi kategoriju odjeće
            IWebElement clothingCategory = driver.FindElement(By.CssSelector(".categories-list a[href='https://www.oreabazaar.com/bs/category/1/odjeca']"));

            // Provjeri da li je kategorija odjeće prisutna
            Assert.IsTrue(clothingCategory.Displayed, "Kategorija odjeće nije prisutna.");

            // Klikni na kategoriju odjeće
            clothingCategory.Click();

            // Provjeri da li se otvorila kategorija odjeće
            string expectedUrl = "https://www.oreabazaar.com/bs/category/1/odjeca";
            string actualUrl = driver.Url;
            Assert.AreEqual(expectedUrl, actualUrl, "Kategorija odjeće se nije otvorila.");
        }
    }
}