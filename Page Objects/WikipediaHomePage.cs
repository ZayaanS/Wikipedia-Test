using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace Wikipedia_Testing.Page_Objects
{
    internal class WikipediaHomePage
    {
        private readonly ChromeDriver driver;

        // Constructor to initialize the driver
        public WikipediaHomePage(ChromeDriver driver)
        {
            this.driver = driver;
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
            SearchBox.SendKeys(searchTerm);
            SearchBox.Submit();
        }
    }
}
