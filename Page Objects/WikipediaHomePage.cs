using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Wikipedia_Testing.Page_Objects
{
    internal class WikipediaHomePage
    {
        private readonly ChromeDriver driver;

        // Constructor to initialize the driver
        public WikipediaHomePage(ChromeDriver driver)
        {
            this.driver = driver;

            // Add implicit wait globally for all elements
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        // Property to locate the search box
        private IWebElement SearchBox => driver.FindElement(By.Id("searchInput"));

        // Method to navigate to the Wikipedia homepage
        public void NavigateToHomePage()
        {
            driver.Navigate().GoToUrl("https://www.wikipedia.org/");
        }

        // Method to perform a search
        public void Search(string searchTerm)
        {
            // Add explicit wait to ensure the search box is visible before interacting
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("searchInput")));

            SearchBox.SendKeys(searchTerm);
            SearchBox.Submit();
        }
    }
}
