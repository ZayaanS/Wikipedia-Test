using System;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;


// Namespace for the test project
namespace Wikipedia_Testing.Tests
{
    // TestFixture attribute indicates that this class contains NUnit tests
    [TestFixture]
    public class SearchWikipediaForAutomationTesting_Test : IDisposable
    {
        // ChromeDriver instance used to control the browser
        private ChromeDriver driver;

        // SetUp method to run before each test
        [SetUp]
        public void Setup()
        {
            // Initialize the ChromeDriver instance before each test runs
            driver = new ChromeDriver();
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
            // Navigate to Wikipedia's homepage
            driver.Navigate().GoToUrl("https://www.wikipedia.org/");

            // Locate the search box element by its ID
            var searchBox = driver.FindElement(By.Id("searchInput"));

            // Enter the search term "Automation testing" into the search box
            searchBox.SendKeys("Automation testing");

            // Submit the search form
            searchBox.Submit();

            // Wait for the page to load and display the search results
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(30);

            // Retrieve the actual title of the web page
            var actualTitle = driver.Title;

            // Define the expected title of the search results page
            var expectedTitle = "Test automation - Wikipedia";

            // Assert that the actual title matches the expected title 
            Assert.That(actualTitle, Is.EqualTo(expectedTitle), $"The page title does not match the expected title. Expected: '{expectedTitle}', but was: '{actualTitle}'.");

            // Locate the table element above the paragraph we need to access
            IWebElement table = driver.FindElement(By.ClassName("box-More_footnotes_needed"));

            // Locate the paragraph element using the relative position of the above table element
            IWebElement textElement = driver.FindElement(RelativeBy.WithLocator(By.TagName("p")).Below(table));

            // Assert that the paragraph element is displayed
            Assert.That(textElement.Displayed, Is.True);

            // Get the text inside the praragraph element
            var resultText = textElement.Text;

            // Define the expected text to be present in the search result
            var expectedText = "Test automation can automate some repetitive but necessary tasks in a formalized testing process";

            // Assert that the text displayed contains the expected text 
            Assert.That(resultText, Does.Contain(expectedText), $"The text '{expectedText}' was not found in the actual text.");
        }
    }
}
