using OpenQA.Selenium;
using System.Collections.Generic;

namespace DemoLoansPlatformTests.PageObjects
{
    public class DashboardPageObject
    {
        private IWebDriver driver;

        private static readonly By _loanWindowTitleName = By.CssSelector("div[title = 'Title']");
        private static readonly By _sidebarMenuDashboardTab = By.CssSelector("a[href='/lms/'] i");
        private static readonly By _loanServicingTab = By.XPath("//a[contains(text(), 'Loan Servicing')]");
        private static readonly By _elementOfExpandedSidebarMenu = By.XPath("//span[contains(text(), 'Dashboard')]");
        private static readonly By _sideBarToggleCollapseButton = By.ClassName("slider");
        public static readonly By settingsButton = By.CssSelector("#Settings");
        public static readonly By loggedUserName = By.CssSelector("#user_profile");
        public static bool isCollapsed;
        public static bool isExpanded;

        public DashboardPageObject(IWebDriver driver)
        {
            this.driver = driver;
        }

// "Dashboard page" "Loan Origination" section tabs opening method
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

                // If there is any failed assertion add it to the list
                if (assertion != null) failedAssertions.Add(assertion);

                // Return to the "Loan Origination" section
                driver.Navigate().Back();
            }

            //Return failed assertion list
            return failedAssertions;
        }

// Dashboard page Loan Servicing section windows opening method
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

                // If there is any failed assertion add it to the list
                if (assertion != null) failedAssertions.Add(assertion);

                // Return to the "Loan Servicing" section
                driver.Navigate().Back();
            }

            //Return failed assertion list
            return failedAssertions;
        }

// Sidebar menu elements opening method
        public List<string> SidebarMenuElementsOpening()
        {
            // Create list for failed assertions
            List<string> failedAssertions = new List<string>();

            // Iterating through "Sidebar menu"
            for (int i = 1; i < 13; i++)
            {
                // Locator for "Sidebar menu" element
                By _sidebarMenuElementname = By.XPath("//ul[@class='nav-links']/li[" + i + "]/ a/i");

                // Locate "Sidebar menu" element
                IWebElement element = driver.FindElement(By.XPath("//ul[@class='nav-links']/li[" + i + "]/ a/i/following-sibling::span"));

                // Open tab and check it opens correctly
                string assertion = TestMethods.CatchFailedAssertion(element, _sidebarMenuElementname);

                // If there is any failed assertion add it to the list
                if (assertion != null) failedAssertions.Add(assertion);
            }

            //Return failed assertion list
            return failedAssertions;
        }

// Sidebar menu Toggle method
        public void SidebarMenuToggle()
        {
            // Click sidebar Toggle/Collapse button to toggle menu
            driver.FindElement(_sideBarToggleCollapseButton).Click();

            // Check if the sidebar menu is expanded by verifying visibility of "Dashboard" word in menu
            isExpanded = driver.FindElement(_elementOfExpandedSidebarMenu).Displayed;
        }

// Sidebar menu Collapse method
        public void SidebarMenuCollapse()
        {
            // Click sidebar Toggle/Collapse button to collapse menu
            driver.FindElement(_sideBarToggleCollapseButton).Click();

            // Wait for the sidebar menu to become collapsed
            WaitUntil.WaitElementIsInvisible(driver, _elementOfExpandedSidebarMenu);

            // Check if the sidebar menu is collapsed
            isCollapsed = !driver.FindElement(_elementOfExpandedSidebarMenu).Displayed;
        }
    }
}
