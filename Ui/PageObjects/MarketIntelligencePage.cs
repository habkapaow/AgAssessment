using OpenQA.Selenium;
using System.Collections.Generic;

namespace UIAutomation.PageObjects
{
    public class MarketIntelligencePage
    {
        private readonly IWebDriver driver;
        private readonly By waysYouBenefitHeadings = By.CssSelector(".services_target_markets h3");
        private readonly By letsGetStartedButton = By.XPath("//*[@id=\"prefooter\"]/a[text()=\"Let's Get Started\"]");

        public MarketIntelligencePage(IWebDriver webDriver)
        {
            driver = webDriver;
        }

        /// <summary>
        /// Verifies headers displayed for Ways You Benefit section
        /// </summary>
        public void VerifyWaysYouBenefitHeadings()
        {
            IList<IWebElement> headings = driver.FindElements(waysYouBenefitHeadings);
            // Assert that each heading is displayed
            foreach (IWebElement heading in headings)
            {
                Assert.That(heading.Displayed, Is.True, $"The heading '{heading.Text}' is not visible.");
            }
        }

        /// <summary>
        /// Click on Let Get Started button
        /// </summary>
        /// <returns>Contact Page</returns>
        public ContactPage ClickLetsGetStartedButton()
        {
            driver.FindElement(letsGetStartedButton).Click();
            return new ContactPage(driver);
        }
    }
}