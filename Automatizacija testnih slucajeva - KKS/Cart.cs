using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using NUnit.Framework;
using System;
using System.Collections.ObjectModel;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    internal class Cart
    {
        private IWebDriver driver = null!;

        [OneTimeSetUp]
        public void OneTimeSetup()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl("https://www.oreabazaar.com/bs/end_user/auth/login");
        }

        [OneTimeTearDown]
        public void OneTimeTeardown()
        {
            driver.Quit();
        }

        [Test]
        public void AddProductsToCart_ShouldDisplayProductsInCart()
        {
            // Prijavi se na profil (ovdje unesite korisničko ime i lozinku za prijavu)
            Login("korisnicko_ime", "lozinka");

            // Navigiraj do "Rozi snovi" preko pretrage
            SearchProduct("Rozi snovi");

            // Klikni na proizvod "Rozi snovi" na rezultatima pretrage
            ClickProduct("Rozi snovi");

            // Dodaj proizvod u korpu
            AddToCart();

            // Dodaj proizvod u korpu
            AddToCart();

            // Navigiraj do "Korpa"
            GoToCart();

            // Provjeri da li su proizvodi dodani u korpu
            Assert.IsTrue(IsProductInCart("Rozi snovi"));
        }

        private void Login(string username, string password)
        {
            // Unos ispravnih kredencijala za prijavu
            string email = "ajlabrdarevic@gmail.com";
            string pass = "sifra123";

            // Unos emaila i lozinke
            driver.FindElement(By.Id("username")).SendKeys(email);
            driver.FindElement(By.Id("password")).SendKeys(pass);

            // Klik na gumb za prijavu
            driver.FindElement(By.CssSelector(".green.login-register-button")).Click();

            // Provjera da li je prijava uspješna - provjera prisutnosti elementa na prijavljenoj stranici
            bool isLoggedIn = driver.FindElement(By.Id("user-dropdown-menu")).Displayed;

            // Provjera očekivanog rezultata
            Assert.IsTrue(isLoggedIn, "Prijavljivanje je uspjelo!");
        }

        private void SearchProduct(string searchTerm)
        {
            // Pronađite element pretrage na stranici
            IWebElement searchInput = driver.FindElement(By.Id("header-search-box"));

            // Unesite traženi pojam u polje za pretragu
            searchInput.SendKeys(searchTerm);

            // Kliknite na gumb za pretragu
            IWebElement searchButton = driver.FindElement(By.Id("header-search-box-submit"));
            searchButton.Click();

            // Pričekajte da se rezultati pretrage učitaju
            System.Threading.Thread.Sleep(2000);
        }

        private void ClickProduct(string productName)
        {
            // Pronađite <a> element koji sadrži naziv proizvoda
            IWebElement productLink = driver.FindElement(By.XPath($"//h5[contains(text(), '{productName.Trim()}')]/ancestor::div[@class='col-md-3 col-xs-6']/a"));

            // Kliknite na <a> element
            productLink.Click();

            // Pričekajte da se otvori stranica proizvoda
            System.Threading.Thread.Sleep(2000);
        }

        private void AddToCart()
        {
            // Pričekajte da se proizvod učita
            System.Threading.Thread.Sleep(2000);

            // Pronađite gumb "Dodaj u korpu" na stranici proizvoda
            IWebElement addToCartButton = driver.FindElement(By.CssSelector(".btn.product-detail-add-to-cart-btn"));

            // Izvršite JavaScript kod za klik na gumb "Dodaj u korpu"
            ((IJavaScriptExecutor)driver).ExecuteScript("arguments[0].click();", addToCartButton);

            // Pričekajte da se proizvod doda u korpu
            System.Threading.Thread.Sleep(2000);
        }


        private void GoToCart()
        {
            // Pronađite element koji predstavlja korpu
            IWebElement cartElement = driver.FindElement(By.CssSelector("a.icon-link[title='Korpa']"));

            // Kliknite na korpu
            cartElement.Click();

            // Pričekajte da se stranica korpe učita
            System.Threading.Thread.Sleep(2000);
        }

        private bool IsProductInCart(string productName)
        {
            // Pronađite elemente koji predstavljaju sve proizvode u korpi
            IReadOnlyCollection<IWebElement> cartProducts = driver.FindElements(By.CssSelector(".cart__item-title a"));

            // Provjerite da li je traženi proizvod prisutan u korpi
            foreach (IWebElement product in cartProducts)
            {
                if (product.Text.Trim().Equals(productName, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
