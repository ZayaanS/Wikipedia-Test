using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using Wikipedia_Testing.Page_Objects;

// Namespace for the test project
namespace Wikipedia_Testing.Tests
{
    // TestFixture attribute indicates that this class contains NUnit tests
    [TestFixture]
    public class SearchWikipediaForAutomationTesting_Test : IDisposable
    {
        // ChromeDriver instance used to control the browser
        private ChromeDriver driver;
        
        private WikipediaHomePage homePage;
        private WikipediaSearchResultsPage searchResultsPage;

        // SetUp method to run before each test
        [SetUp]
        public void Setup()
        {
            // Initialize the ChromeDriver instance before each test runs
            driver = new ChromeDriver();

            // Set an implicit wait globally for all elements
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            // Initilize page objects
            homePage = new WikipediaHomePage(driver);
            searchResultsPage = new WikipediaSearchResultsPage(driver);
        }

        // TearDown method to run after each test
        [TearDown]
        public void TearDown()
        {
            // Ensure resources are properly disposed of after each test
            Dispose();
        }

        // Implement IDisposable function to properly clean up resources
        public void Dispose()
        {
            // Quit the ChromeDriver session and close the browser
            driver?.Quit();

            // Dispose of the ChromeDriver instance to release unmanaged resources
            driver?.Dispose();
        }

        // Test method with the Test attribute indicates this is a test case
        [Test]
        public void SearchWikipediaForAutomationTesting()
        {
            // Navigate to Wikipedia's homepage using the Page Object
            homePage.NavigateToHomePage();

            // Perform search using the Page Object
            homePage.Search("Automation testing");

            // Wait for the page to load and display the search results
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            // Retrieve the actual title of the web page
            var actualTitle = searchResultsPage.GetPageTitle();

            // Define the expected title of the search results page
            var expectedTitle = "Test automation - Wikipedia";

            // Assert that the actual title matches the expected title 
            Assert.That(actualTitle, Is.EqualTo(expectedTitle), $"The page title does not match the expected title. Expected: '{expectedTitle}', but was: '{actualTitle}'.");

            // Get the text inside the praragraph element
            var resultText = searchResultsPage.GetResultText("box-More_footnotes_needed");

            // Define the expected text to be present in the search result
            var expectedText = "Test automation can automate some repetitive but necessary tasks in a formalized testing process";

            // Assert that the text displayed contains the expected text 
            Assert.That(resultText, Does.Contain(expectedText), $"The text '{expectedText}' was not found in the actual text.");

        }
    }
}
