using NUnit.Framework;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium;
using System;
using DemoLoansPlatformTests.PageObjects;
using System.Collections.Generic;
using Microsoft.Extensions.Configuration;

namespace DemoLoansPlatformTests
{
    [TestFixture]
    // Create additional class to run SetUp and TearDown methods once for all tests
    public class BaseTest
    {
        public static IWebDriver driver;

        public static string loginDataFileLocation;
        public static string mainPageUrl;

        [OneTimeSetUp]
        public  void BeforeAllTests()
        {
            // Set ChromeDriver options and initialize WebDriver
            ChromeOptions options = new ChromeOptions();

            // Maximize the browser window
            options.AddArgument("--start-maximized");

            // Create ChromeDriver instance
            driver = new ChromeDriver(Helper.chromeDriverLocation, options);
        }

        [OneTimeTearDown]
        public void AfterAllTests()
        {
            driver.Quit();
        }

        [SetUp]
        public void BeforeEachTest()
        {
            // Create a configuration builder and add configuration source
            IConfiguration configuration = new ConfigurationBuilder()
               .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
               .Build();

            //Retrieve configuration values using the IConfiguration interface
            mainPageUrl = configuration["MainPageUrl"];
            loginDataFileLocation = configuration["LoginDataFileLocation"];

            // Go to the login page
            driver.Navigate().GoToUrl(mainPageUrl);

            // Login to application
            LoginPage.SignIn();

            // Check current user is logged to the app
            string actualUserName = driver.FindElement(LoginPage.loggedUserName).Text;
            Assert.AreEqual(LoginPage.expectedUserName, actualUserName);
        }

        [TearDown]
        public void AfterEachTest()
        {
            // Logout from app
            LoginPage.LogOut();
        }
    }

    public class Tests : BaseTest
    {        
        [Test]
        public void DashboardPageLoanOriginationSectionWindowsOpeningTest()
        {
            // Create DashboardPageObject object
            var dashboardObject = new DashboardPage(driver);

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
            var dashboardObject = new DashboardPage(driver);

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

            // Create SidebarMenu object
                var sideMenu = new SidebarMenu(driver);

            // Check the opening of sidebar menu elements
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
            var sideMenu = new SidebarMenu(driver);

            // Use multiple asserts to run the whole test if some assertion fails
            Assert.Multiple(() =>
            {
                // Check sidebar menu toggle
                sideMenu.SidebarMenuToggle();

                // Assertion to check sidebar menu is expanded
                Assert.IsTrue(SidebarMenu.isExpanded, "\"Sidebar menu\" should be expanded after clicking toggle button");

                // Check sidebar menu collapse
                sideMenu.SidebarMenuCollapse();

                // Assertion to check sidebar menu is collapsed
                Assert.IsTrue(SidebarMenu.isCollapsed, "\"Sidebar menu\" should be collapsed after clicking collapse button");
            });
        }

        [Test]
        public void SettingsPageOriginationSectionItemsOpeneingTest()
        {
            // Open "Settings" page
            Helper.OpenPage(SettingsPage.settingsPageUrl);

            // Check "Settings" page "Origination" section items openeing
            List<string> failedAssertions = SettingsPage.settingsPageOriginationSectionItemsOpening();

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
            Helper.OpenPage(SettingsPage.settingsPageUrl);

            // Check "Setttings" page "Servicing" section items openeing
            List<string> failedAssertions = SettingsPage.settingsPageServicingSectionItemsOpening();

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
            Helper.OpenPage(SettingsPage.settingsPageUrl);

            // Check "Setttings" page "Companies" section items openeing
            List<string> failedAssertions = SettingsPage.settingPageCompaniesSectionItemsOpening();

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
            Helper.OpenPage(SettingsPage.settingsPageUrl);

            // Check "Setttings" page "Logs/Audit" section items openeing
            List<string> failedAssertions = SettingsPage.settingPageLogsAuditSectionItemsOpening();

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
            Helper.OpenPage(SettingsPage.settingsPageUrl);

            // Check "Setttings" page "Users" section items openeing
            List<string> failedAssertions = SettingsPage.settingsPageUsersSectionItemsOpening();

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
            Helper.OpenPage(SettingsPage.settingsPageUrl);

            // Check "Setttings" page "Templates" section items openeing
            List<string> failedAssertions = SettingsPage.settingsPageTemplatesSectionItemsOpening();

            // Check if there are any failed assertions
            if (failedAssertions.Count > 0)
            {
                // Fail the test and display failed assertions
                Assert.Fail(string.Join(Environment.NewLine, failedAssertions));
            }
        }
    }
}