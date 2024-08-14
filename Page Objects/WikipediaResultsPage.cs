using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Wikipedia_Testing.Page_Objects
{
    internal class WikipediaSearchResultsPage
    {
        private readonly ChromeDriver driver;

        // Constructor to initialize the driver
        public WikipediaSearchResultsPage(ChromeDriver driver)
        {
            this.driver = driver;

            // Add implicit wait globally for all elements
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        // Method to get the page title
        public string GetPageTitle(string expectedPageTitle)
        {
            // Wait for the page to load and display the search results
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
            wait.Until(ExpectedConditions.TitleContains(expectedPageTitle));

            return driver.Title;


    
        }

        // Method to get the result paragraph text
        public string GetResultText(string tableClassName)
        {
            // Use explicit wait to ensure the table is present
            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));

            IWebElement table = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(tableClassName)));


            IWebElement textElement = driver.FindElement(RelativeBy.WithLocator(By.TagName("p")).Below(table));
            return textElement.Text;
        }
    }
}
