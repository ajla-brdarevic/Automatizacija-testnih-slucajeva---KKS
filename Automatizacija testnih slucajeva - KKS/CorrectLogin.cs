using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    public class Tests
    {
        //Može sadržavati null vrijednost
        private IWebDriver driver = null!;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            // Postavljanje WebDrivera i otvaranje stranice samo jednom prije poèetka svih testova
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.oreabazaar.com/bs/end_user/auth/login");
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            // Zatvaranje preglednika nakon završetka svih testova
            driver.Quit();
        }

        [Test]
        public void LoginWithCorrectCredentials_ShouldSucceed()
        {
            // Unos ispravnih kredencijala za prijavu
            string email = "ajlabrdarevic@gmail.com";
            string password = "sifra123";

            // Unos emaila i lozinke
            driver.FindElement(By.Id("username")).SendKeys(email);
            driver.FindElement(By.Id("password")).SendKeys(password);

            // Klik na gumb za prijavu
            driver.FindElement(By.CssSelector(".green.login-register-button")).Click();

            // Provjera da li je prijava uspješna - provjera prisutnosti elementa na prijavljenoj stranici
            bool isLoggedIn = driver.FindElement(By.Id("user-dropdown-menu")).Displayed;

            // Provjera oèekivanog rezultata
            Assert.IsTrue(isLoggedIn, "Prijavljivanje je uspjelo!");
        }
    }
}