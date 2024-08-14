using NLog;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;
using Wikipedia_Testing.Custom_Exceptions;

namespace Wikipedia_Testing.Page_Objects
{
    // Internal class to represent the Wikipedia homepage
    internal class WikipediaHomePage
    {
        
        private readonly ChromeDriver driver; // ChromeDriver instance used to control the browser
        private static readonly ILogger logger = LogManager.GetCurrentClassLogger(); // Logger instance for logging messages
        private const string WikipediaUrl = "https://www.wikipedia.org/"; // Define the Wikipedia URL as a private constant or readonly field

        // Constructor to initialize the driver
        public WikipediaHomePage(ChromeDriver driver)
        {
            
            this.driver = driver; // Initialize the ChromeDriver instance before each test runs
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10); // Add implicit wait globally for all elements
        }

        // Property to locate the search box
        private IWebElement SearchBox
        {
            get // Getter method to retrieve the search box element
            {
                try // Try block to handle potential exceptions during locating the search box
                {
                    return driver.FindElement(By.Id("searchInput")); // Return the search box element
                }
                catch (NoSuchElementException ex) // Catch block to handle NoSuchElementException
                {
                    logger.Error(ex, "Search box element was not found."); // Log the exception
                    throw new ElementNotFoundException("Search box element not found.", ex); // Throw custom ElementNotFoundException
                }
                catch (Exception ex) // Catch block to handle any other unexpected exceptions
                {
                    logger.Error(ex, "Unexpected error while locating the search box."); // Log unexpected exceptions
                    throw; // Rethrow for further handling
                }
            }
        }
        

        // Method to navigate to the Wikipedia homepage
        public void NavigateToHomePage()
        {
            try // Try block to handle potential exceptions during navigating to the homepage
            {
                driver.Navigate().GoToUrl(WikipediaUrl); // Navigating to the Wikipedia homepage
            }
            catch (WebDriverException ex) // Catch block to handle WebDriverException
            {
                logger.Error(ex, $"Failed to navigate to URL: {WikipediaUrl}"); // Log the exception
                throw new NavigationException($"Could not navigate to the Wikipedia homepage.", ex); // Throw a custom PageTitleNotFoundException with details
            }
            catch (Exception ex) // Catch block to handle any other unexpected exceptions
            {
                logger.Error(ex, "Unexpected error while navigating to the homepage."); // Log unexpected exceptions
                throw; // Rethrow for further handling
            }
        }

        // Method to perform a search
        public void Search(string searchTerm)
        {
            try // Try block to handle potential exceptions during searching
            {
                WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(15)); // Add explicit wait to ensure the search box is visible before interacting
                wait.Until(ExpectedConditions.ElementIsVisible(By.Id("searchInput"))); // Wait for the search box to be visible
                SearchBox.SendKeys(searchTerm); // Enter search text into the search box
                SearchBox.Submit(); // Submit the search item
            }
            catch (WebDriverTimeoutException ex) // Catch block to handle WebDriverTimeoutException
            {
                logger.Error(ex, "Timeout while waiting for the search box to become visible."); // Log the exception
                throw new ElementNotVisibleException("Search box did not become visible within the expected time.", ex); // Throw custom ElementNotVisibleException
            }
            catch (NoSuchElementException ex)  // Catch block to handle NoSuchElementException
            {
                logger.Error(ex, "Search box element was not found during search."); // Log the exception
                throw new ElementNotFoundException("Search box element not found during search.", ex); // Throw a custom ElementNotFoundExeption
            }
            catch (Exception ex)  // Catch block to handle any other unexpected exceptions
            {
                logger.Error(ex, "Unexpected error during search."); // Log unexpected exceptions
                throw; // Rethrow or return a default value
            }
        }
    }
}
