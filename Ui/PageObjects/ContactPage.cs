using OpenQA.Selenium;
using log4net;
using NUnit.Framework;

namespace UIAutomation.PageObjects
{
    public class ContactPage
    {
        private static readonly ILog Logger = LogManager.GetLogger(typeof(ContactPage));

        private readonly IWebDriver driver;
        private readonly By contactHeading = By.XPath("//h1[text()='GET IN TOUCH WITH US']");

        public ContactPage(IWebDriver webDriver)
        {
            driver = webDriver;
            Logger.Info("ContactPage initialized.");
        }

        /// <summary>
        /// Verifies Contact page is loaded by verifying contact form header
        /// </summary>
        public void IsContactPageLoaded()
        {
            try
            {
                Logger.Info("Verifying if the Contact page is loaded.");
                bool isDisplayed = driver.FindElement(contactHeading).Displayed;
                Assert.That(isDisplayed, Is.True, "Contact page did not load correctly.");
                Logger.Info("Contact page loaded successfully.");
            }
            catch (NoSuchElementException ex)
            {
                Logger.Error("Failed to find the Contact heading element.", ex);
                throw;
            }
            catch (AssertionException ex)
            {
                Logger.Error("Contact page did not load correctly.", ex);
                throw;
            }
        }
    }
}
