using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using NLog;
using Wikipedia_Testing.Custom_Exceptions;

namespace Wikipedia_Testing.Page_Objects
{
    internal class WikipediaHomePage
    {
        private readonly ChromeDriver driver;
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger();
        // Define the Wikipedia URL as a private constant or readonly field
        private const string WikipediaUrl = "https://www.wikipedia.org/";

        // Constructor to initialize the driver
        public WikipediaHomePage(ChromeDriver driver)
        {
            this.driver = driver;
            // Add implicit wait globally for all elements
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);
        }

        // Property to locate the search box
        private IWebElement SearchBox
        {
            get
            {
                try
                {
                    return driver.FindElement(By.Id("searchInput"));
                }
                catch (NoSuchElementException ex)
                {
                    logger.Error(ex, "Search box element was not found.");
                    throw new ElementNotFoundException("Search box element not found.", ex);
                }
                catch (Exception ex)
                {
                    logger.Error(ex, "Unexpected error while locating the search box.");
                    throw;
                }
            }
        }
        

        // Method to navigate to the Wikipedia homepage
        public void NavigateToHomePage()
        {
            try
            {
                driver.Navigate().GoToUrl(WikipediaUrl);
            }
            
            catch (WebDriverException ex)
            {
                logger.Error(ex, $"Failed to navigate to URL: {WikipediaUrl}");
                throw new NavigationException($"Could not navigate to the Wikipedia homepage.", ex);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unexpected error while navigating to the homepage.");
                throw;
            }
        }

        // Method to perform a search
        public void Search(string searchTerm)
        {
            try
            {
                // Add explicit wait to ensure the search box is visible before interacting
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15));
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("searchInput")));
                SearchBox.SendKeys(searchTerm);
                SearchBox.Submit();
            }
            catch (WebDriverTimeoutException ex)
            {
                logger.Error(ex, "Timeout while waiting for the search box to become visible.");
                throw new ElementNotVisibleException("Search box did not become visible within the expected time.", ex);
            }
            catch (NoSuchElementException ex)
            {
                logger.Error(ex, "Search box element was not found during search.");
                throw new ElementNotFoundException("Search box element not found during search.", ex);
            }
            catch (Exception ex)
            {
                logger.Error(ex, "Unexpected error during search.");
                throw;
            }
        }
    }
}
