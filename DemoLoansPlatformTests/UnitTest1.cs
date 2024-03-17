using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using DemoLoansPlatformTests.PageObjects;
using System.Collections.Generic;

namespace DemoLoansPlatformTests
{
    [TestFixture]
    // Create additional class to run SetUp and TearDown methods once for all tests
    public class BaseTest
    {
        public static IWebDriver driver;

        [OneTimeSetUp]
        public void BeforeAllTests()
        {
            // Launch ChromeDriver  
            driver = new ChromeDriver(Helper.chromeDriverLocation);

            // Maximize browser window
            driver.Manage().Window.Maximize();

            // Wait for window to maximize
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(2);

            // Go to the login page
            driver.Navigate().GoToUrl(Helper.mainPageUrl);
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            driver.Quit();
        }

        [SetUp]
        public void BeforeEachTest()
        {
            // Create LoginPageOject object
            var loginPage = new LoginPageOject(driver);

            // Login to application
            loginPage.SignIn();

            // Check current user is logged to the app
            string actualUserName = driver.FindElement(DashboardPageObject.loggedUserName).Text;
            Assert.AreEqual(LoginPageOject.expectedUserName, actualUserName);
        }

        [TearDown]
        public void AfterEachTest()
        {
            // Create LoginPageOject object
            var loginPage = new LoginPageOject(driver);

            loginPage.LogOut();
        }
    }

    public class Tests : BaseTest
    {
        [Test]
        public void DashboardPageLoanOriginationSectionWindowsOpeningTest()
        {
            // Create DashboardPageObject object
            var dashboardObject = new DashboardPageObject(driver);

            // Check "Loan Origination" section each window opens correctly
            List<string> failedAssertions = dashboardObject.LoanOriginationSectionTabsOpening();

            // Check if there are any failed assertions
            if (failedAssertions.Count > 0)
            {
                // Fail the test and display failed assertions
                Assert.Fail(string.Join(Environment.NewLine, failedAssertions));
            }
        }

        [Test]
        public void DashboardPageLoanServicingSectionWindowsOpeningTest()
        {
            // Create DashboardPageObject object
            var dashboardObject = new DashboardPageObject(driver);

            // Check "Loan Servicing" section each window opens correctly
            List<string> failedAssertions = dashboardObject.LoanServicingSectionTabsOpening();

            // Check if there are any failed assertions
            if (failedAssertions.Count > 0)
            {
                // Fail the test and display failed assertions
                Assert.Fail(string.Join(Environment.NewLine, failedAssertions));
            }
        }

        [Test]
        public void SideBarMenuElementsOpeningTest()
        {
            // Create DashboardPageObject object
            var sideMenu = new DashboardPageObject(driver);

            // Check the openeing of sidebar menu elements
            List<string> failedAssertions = sideMenu.SidebarMenuElementsOpening();

            // Check if there are any failed assertions
            if (failedAssertions.Count > 0)
            {
                // Fail the test and display failed assertions
                Assert.Fail(string.Join(Environment.NewLine, failedAssertions));
            }
        }

        [Test]
        public void SideBarMenuElementsToggleCollapseTest()
        {
            // Create DashboardPageObject object
            var sideMenu = new DashboardPageObject(driver);

            // Use multiple asserts to run the whole test if some assertion fails
            Assert.Multiple(() =>
            {
                // Check sidebar menu toggle
                sideMenu.SidebarMenuToggle();

                // Assertion to check sidebar menu is expanded
                Assert.IsTrue(DashboardPageObject.isExpanded, "\"Sidebar menu\" should be expanded after clicking toggle button");

                // Check sidebar menu collapse
                sideMenu.SidebarMenuCollapse();

                // Assertion to check sidebar menu is collapsed
                Assert.IsTrue(DashboardPageObject.isCollapsed, "\"Sidebar menu\" should be collapsed after clicking collapse button");
            });
        }

        [Test]
        public void SettingsPageOriginationSectionItemsOpeneingTest()
        {
            // Open "Settings" page
            Helper.OpenPage(SettingsPageObject.settingsPageUrl);

            // Check "Settings" page "Origination" section items openeing
            List<string> failedAssertions = SettingsPageObject.settingsPageOriginationSectionItemsOpening();

            // Check if there are any failed assertions
            if (failedAssertions.Count > 0)
            {
                // Fail the test and display failed assertions
                Assert.Fail(string.Join(Environment.NewLine, failedAssertions));
            }
        }

        [Test]
        public void SettingsPageServicingSectionItemsOpeneingTest()
        {
            // Open "Settings" page
            Helper.OpenPage(SettingsPageObject.settingsPageUrl);

            // Check "Setttings" page "Servicing" section items openeing
            List<string> failedAssertions = SettingsPageObject.settingsPageServicingSectionItemsOpening();

            // Check if there are any failed assertions
            if (failedAssertions.Count > 0)
            {
                // Fail the test and display failed assertions
                Assert.Fail(string.Join(Environment.NewLine, failedAssertions));
            }
        }

        [Test]
        public void SettingsPageCompaniesSectionItemsOpeneingTest()
        {
            // Open "Settings" page
            Helper.OpenPage(SettingsPageObject.settingsPageUrl);

            // Check "Setttings" page "Companies" section items openeing
            List<string> failedAssertions = SettingsPageObject.settingPageCompaniesSectionItemsOpening();

            // Check if there are any failed assertions
            if (failedAssertions.Count > 0)
            {
                // Fail the test and display failed assertions
                Assert.Fail(string.Join(Environment.NewLine, failedAssertions));
            }
        }

        [Test]
        public void SettingsPageLogsAuditSectionItemsOpeneingTest()
        {
            // Open "Settings" page
            Helper.OpenPage(SettingsPageObject.settingsPageUrl);

            // Check "Setttings" page "Logs/Audit" section items openeing
            List<string> failedAssertions = SettingsPageObject.settingPageLogsAuditSectionItemsOpening();

            // Check if there are any failed assertions
            if (failedAssertions.Count > 0)
            {
                // Fail the test and display failed assertions
                Assert.Fail(string.Join(Environment.NewLine, failedAssertions));
            }
        }

        [Test]
        public void SettingsPageUsersSectionItemsOpeneingTest()
        {
            // Open "Settings" page
            Helper.OpenPage(SettingsPageObject.settingsPageUrl);

            // Check "Setttings" page "Users" section items openeing
            List<string> failedAssertions = SettingsPageObject.settingsPageUsersSectionItemsOpening();

            // Check if there are any failed assertions
            if (failedAssertions.Count > 0)
            {
                // Fail the test and display failed assertions
                Assert.Fail(string.Join(Environment.NewLine, failedAssertions));
            }
        }

        [Test]
        public void SettingsPageTemplatesSectionItemsOpeneingTest()
        {
            // Open "Settings" page
            Helper.OpenPage(SettingsPageObject.settingsPageUrl);

            // Check "Setttings" page "Templates" section items openeing
            List<string> failedAssertions = SettingsPageObject.settingsPageTemplatesSectionItemsOpening();

            // Check if there are any failed assertions
            if (failedAssertions.Count > 0)
            {
                // Fail the test and display failed assertions
                Assert.Fail(string.Join(Environment.NewLine, failedAssertions));
            }
        }
    }
}