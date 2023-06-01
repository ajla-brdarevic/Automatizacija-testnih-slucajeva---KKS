using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Automatizacija_testnih_slucajeva___KKS
{
    [TestFixture]
    public class Favourite
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
        public void AddProductsToFavourite_ShouldDisplayProductsInFavoutite()
        {
            // Prijavi se na profil (ovdje unesite korisničko ime i lozinku za prijavu)
            Login("korisnicko_ime", "lozinka");

            // Navigiraj do "Trust what you see" preko pretrage
            SearchProduct("Trust what you see");

            // Klikni na proizvod "Trust what you see" na rezultatima pretrage
            ClickProduct("Trust what you see");

            // Dodaj proizvod u favourite
            AddToFav();

            // Navigiraj do "Korpa"
            GoToFav();

            // Provjeri da li su proizvodi dodani u korpu
            Assert.IsTrue(IsProductInFav("Trust what you see"));
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
            IWebElement productLink = driver.FindElement(By.XPath($"//h5[contains(text(), '{productName.Trim()}')]/ancestor::div[contains(@class, 'col-md-3')]/a"));

            // Kliknite na <a> element
            productLink.Click();

            // Pričekajte da se otvori stranica proizvoda
            System.Threading.Thread.Sleep(2000);
        }

        private void AddToFav()
        {
            // Pronađite element koji predstavlja gumb "Dodaj u omiljene" za proizvod "Heart Hoops"
            IWebElement addToFavoriteButton = driver.FindElement(By.CssSelector("a[data-product_id='11939']"));

            // Kliknite na gumb "Dodaj u omiljene"
            addToFavoriteButton.Click();

            // Pričekajte da se proizvod doda u omiljene
            System.Threading.Thread.Sleep(2000);
        }


        private void GoToFav()
        {
            // Pronađite element koji predstavlja link za omiljene proizvode
            IWebElement favoritesLink = driver.FindElement(By.CssSelector("a[href='https://www.oreabazaar.com/bs/end_user/auth/article']"));

            // Kliknite na link za omiljene proizvode
            favoritesLink.Click();

            // Pričekajte da se otvori stranica s omiljenim proizvodima
            System.Threading.Thread.Sleep(2000);
        }

        private bool IsProductInFav(string productName)
        {
            // Pronađite elemente koji predstavljaju sve proizvode u omiljenim proizvodima
            IReadOnlyCollection<IWebElement> favoriteProducts = driver.FindElements(By.CssSelector(".product-name"));

            // Provjerite da li je traženi proizvod prisutan u omiljenim proizvodima
            foreach (IWebElement product in favoriteProducts)
            {
                if (product.Text.Trim().Equals(productName.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }

            return false;
        }

    }
}
