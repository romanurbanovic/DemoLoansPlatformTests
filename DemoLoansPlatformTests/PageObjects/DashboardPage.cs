using OpenQA.Selenium;
using System.Collections.Generic;

namespace DemoLoansPlatformTests.PageObjects
{
    public class DashboardPage
    {
        private IWebDriver driver;

        private static readonly By _loanWindowTitleName = By.CssSelector("div[title = 'Title']");
        private static readonly By _sidebarMenuDashboardTab = By.CssSelector("a[href='/lms/'] i");
        private static readonly By _loanServicingTab = By.XPath("//a[contains(text(), 'Loan Servicing')]");

        public DashboardPage(IWebDriver driver)
        {
            this.driver = driver;
        }

        // "Dashboard" page "Loan Origination" section tabs opening method
        public List<string> LoanOriginationSectionTabsOpening()
        {
            // Open "Dashboard" page
            driver.FindElement(_sidebarMenuDashboardTab).Click();

            // Create list for failed assertions
            List<string> failedAssertions = new List<string>();

            // Iterate through the "Loan Origination" section tabs
            for (int i = 1; i < 10; i++)
            {
                // Locator for "Loan Origination" section tab element
                By loanOriginationSectionElementName = By.XPath("//div[@class= 'content']/div/div[" + i + "]/div[1]/span[1]");

                // Find "Loan Origination" section webelement
                IWebElement loanOriginationSectionElement = driver.FindElement(loanOriginationSectionElementName);

                // Open tab and check it opens correctly
                string assertion = TestMethods.CatchFailedAssertion(loanOriginationSectionElement, loanOriginationSectionElementName);

                // If there is any failed assertion add it to the list and reload the page to continue whith the next element
                if (assertion != null)
                {
                    // Add it to list
                    failedAssertions.Add(assertion);
                    // Logout if page opens with ERROR: 500
                    if (BaseTest.driver.PageSource.Contains("Error: 500")) LoginPage.LogOut();
                    // Login to the page
                    LoginPage.SignIn();
                    // Move to the next element
                    continue;
                }

                // Return to the "Loan Origination" section
                driver.Navigate().Back();
            }

            //Return failed assertion list
            return failedAssertions;
        }

        // "Dashboard" page "Loan Servicing" section windows opening method
        public List<string> LoanServicingSectionTabsOpening()
        {
            // Open "Dashboard" section
            driver.FindElement(_sidebarMenuDashboardTab).Click();

            // Create list of failed assertions
            List<string> failedAssertions = new List<string>();

            // Wait for "Loan Servicing" tab to be active
            WaitUntil.WaitElementIsActive(driver, _loanServicingTab);

            // Switch to Loan Servicing section
            driver.FindElement(_loanServicingTab).Click();

            // Iterate through the "Loan Servicing" section windows
            for (int i = 1; i < 9; i++)
            {
                // Locator for "Loan Servicing" section tab element
                By loanServicingSectionTabName = By.XPath("//div[@class= 'content']/div/div[" + i + "]/div[1]/span[1]");

                // Find "Loan Servising" section webelement
                IWebElement element = driver.FindElement(loanServicingSectionTabName);

                // Open tab and check it opens correctly
                string assertion = TestMethods.CatchFailedAssertion(element, _loanWindowTitleName);

                // If there is any failed assertion add it to the list and prepare to check next element
                if (assertion != null) 
                {
                    // Add to list
                    failedAssertions.Add(assertion);
                    // Logout if page opens with ERROR: 500
                    if (BaseTest.driver.PageSource.Contains("Error: 500")) LoginPage.LogOut();
                    // Login to the page
                    LoginPage.SignIn();
                    // Wait for the element to be active
                    WaitUntil.WaitElementIsActive(driver, _loanServicingTab);
                    // Click it
                    driver.FindElement(_loanServicingTab).Click();
                    // Move to the next element
                    continue;
                }


                // Return to the "Loan Servicing" section
                driver.Navigate().Back();
            }

            //Return failed assertion list
            return failedAssertions;
        }
    }
}
