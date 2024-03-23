using OpenQA.Selenium;
using System.Collections.Generic;

namespace DemoLoansPlatformTests.PageObjects
{
    class SidebarMenu
    {
        private IWebDriver driver;

        private static readonly By _sideBarToggleCollapseButton = By.ClassName("slider");
        private static readonly By _elementOfExpandedSidebarMenu = By.XPath("//span[contains(text(), 'Dashboard')]");
        public static bool isCollapsed;
        public static bool isExpanded;

        public SidebarMenu(IWebDriver driver)
        {
            this.driver = driver;
        }

        // "Sidebar" menu elements opening method
        public List<string> SidebarMenuElementsOpening()
        {
            // Create list for failed assertions
            List<string> failedAssertions = new List<string>();

            // Iterating through "Sidebar" menu
            for (int i = 1; i < 13; i++)
            {
                // Locator for "Sidebar menu" element
                By _sidebarMenuElementname = By.XPath("//ul[@class='nav-links']/li[" + i + "]/ a/i");

                // Locate "Sidebar" menu element
                IWebElement element = driver.FindElement(By.XPath("//ul[@class='nav-links']/li[" + i + "]/ a/i/following-sibling::span"));

                // Open tab and check it opens correctly
                string assertion = TestMethods.CatchFailedAssertion(element, _sidebarMenuElementname);

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
            }

            //Return failed assertion list
            return failedAssertions;
        }

        // "Sidebar" menu Toggle method
        public void SidebarMenuToggle()
        {
            // Click sidebar Toggle/Collapse button to toggle menu
            driver.FindElement(_sideBarToggleCollapseButton).Click();

            // Check if the sidebar menu is expanded by verifying visibility of "Dashboard" word in menu
            isExpanded = driver.FindElement(_elementOfExpandedSidebarMenu).Displayed;
        }

        // "Sidebar" menu Collapse method
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
