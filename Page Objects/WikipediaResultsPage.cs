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
            try
            {
                // Wait for the page to load and display the search results
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                wait.Until(ExpectedConditions.TitleContains(expectedPageTitle));

                return driver.Title;
            }
            catch (WebDriverTimeoutException ex)
            {
                // Log the exception and rethrow or return a default value
                Console.WriteLine($"Error: Timeout while waiting for page title to contain '{expectedPageTitle}'. Exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log unexpected exceptions and rethrow or return a default value
                Console.WriteLine($"Unexpected error while getting the page title. Exception: {ex.Message}");
                throw;
            }
        }

        // Method to get the result paragraph text
        public string GetResultText(string tableClassName)
        {
            try
            {
                // Use explicit wait to ensure the table is present
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                IWebElement table = wait.Until(ExpectedConditions.ElementIsVisible(By.ClassName(tableClassName)));
                IWebElement textElement = driver.FindElement(RelativeBy.WithLocator(By.TagName("p")).Below(table));
                return textElement.Text;
            }
            catch (WebDriverTimeoutException ex)
            {
                // Log the exception and rethrow or return a default value
                Console.WriteLine($"Error: Timeout while waiting for the table with class '{tableClassName}' to be visible. Exception: {ex.Message}");
                throw; 
            }
            catch (NoSuchElementException ex)
            {
                // Log the exception and rethrow or return a default value
                Console.WriteLine($"Error: Element not found. Exception: {ex.Message}");
                throw;
            }
            catch (Exception ex)
            {
                // Log unexpected exceptions and rethrow or return a default value
                Console.WriteLine($"Unexpected error while getting the result text. Exception: {ex.Message}");
                throw;
            }
        }
    }
}
