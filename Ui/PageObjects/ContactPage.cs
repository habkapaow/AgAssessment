using OpenQA.Selenium;

namespace UIAutomation.PageObjects
{
    public class ContactPage
    {
        private readonly IWebDriver driver;
        private readonly By contactHeading = By.XPath("//h1[text()='GET IN TOUCH WITH US']");

        public ContactPage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        /// <summary>
        /// Verifies Contact page is loaded by verifying contact form header
        /// </summary>
        public void IsContactPageLoaded()
        {
            Assert.That(driver.FindElement(contactHeading).Displayed, Is.True, "Contact page did not load correctly.");
        }
    }
}
