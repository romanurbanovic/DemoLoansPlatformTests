using OpenQA.Selenium;
using System.Collections.Generic;

namespace DemoLoansPlatformTests.PageObjects
{
    public class SettingsPage
    {
        public static readonly By settingsButton = By.CssSelector("#Settings");
        public static string settingsPageUrl = "https://demo.loansplatform.com/lms/settingspg";

        // One common method for all "Settings page" sections to check correct opening of section items
        // with locator parameter "sectionName" and items count parameter "sectionItemsCount"  for various sections
        public static List<string> settingsPageSectionItemsOpening(string sectionName, int sectionItemsCount)
        {
            // Create list of failed assertions
            List<string> failedAssertions = new List<string>();

            // Iterate through the "Origination" section items
            for (int i = 1; i <= sectionItemsCount; i++)
            {
                
                // Locator for section item
                By sectionItemName = By.XPath("//h2[text()='" + sectionName + "']/parent::div//following-sibling::ul/a[" + i + "]/li");

                // Find section webelement
                IWebElement sectionItem = BaseTest.driver.FindElement(sectionItemName);

                // Check for failed assertion
                string assertion = TestMethods.CatchFailedAssertion(sectionItem, sectionItemName);

                // If there is any failed assertion add it to the list and reload the page to continue whith the next element
                if (assertion != null)
                {
                    // Add it to list
                    failedAssertions.Add(assertion);
                    // Logout if page opens with ERROR: 500
                    if (BaseTest.driver.PageSource.Contains("Error: 500")) LoginPage.LogOut();
                    // Login to the page
                    LoginPage.SignIn();
                    // Open "Settingss" page
                    Helper.OpenPage(settingsPageUrl);
                    // Move to the next element
                    continue;
                }

                // Return to the "Settings" page
                BaseTest.driver.Navigate().Back();
            }
            //Return failed assertion list
            return failedAssertions;
        }

        // Method to check correct opening of "Settings page" "Origination" section items
        public static List<string> settingsPageOriginationSectionItemsOpening()
        {
            // Use settingsPageSectionItemsOpening() method with "Origination" section parameters to locate elements
            return settingsPageSectionItemsOpening("Origination", 8);
        }

        // Method to check correct opening of "Settings page" "Servicing" section items
        public static List<string> settingsPageServicingSectionItemsOpening()
        {
            // Use settingsPageSectionItemsOpening() method with "Servicing" section parameters to locate elements
            return settingsPageSectionItemsOpening("Servicing", 5);
        }

        // Method to check correct opening of "Settings page" "Companies" section items
        public static List<string> settingPageCompaniesSectionItemsOpening()
        {
            // Use settingsPageSectionItemsOpening() method with "Companies" section parameters to locate elements
            return settingsPageSectionItemsOpening("Companies", 4);
        }

        // Method to check correct opening of "Settings page" "Logs/Audit" section items
        public static List<string> settingPageLogsAuditSectionItemsOpening()
        {
            // Use settingsPageSectionItemsOpening() method with "Logs/Audit" section parameters to locate elements
            return settingsPageSectionItemsOpening("Logs/Audit", 8);
        }

        // Method to check correct opening of "Settings page" "Users" section items
        public static List<string> settingsPageUsersSectionItemsOpening()
        {
            // Use settingsPageSectionItemsOpening() method with "Users" section parameters to locate elements
            return settingsPageSectionItemsOpening("Users", 2);
        }

        // Method to check correct opening of "Settings page" "Templates" section items
        public static List<string> settingsPageTemplatesSectionItemsOpening()
        {
            // Use settingsPageSectionItemsOpening() method with "Templates" section parameters to locate elements
            return settingsPageSectionItemsOpening("Templates", 2);
        }
    }
}
