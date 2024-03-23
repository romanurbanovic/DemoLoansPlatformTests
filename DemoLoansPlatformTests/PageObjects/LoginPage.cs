using OpenQA.Selenium;
using System.IO;


namespace DemoLoansPlatformTests.PageObjects
{
    class LoginPage
    {
        private IWebDriver driver;

        private static By _userNameInputTab = By.Id("eMail");
        private static By _passwordInputTab = By.Id("Password");
        public static By _submitButton = By.Id("submit_button");
        public static string expectedUserName = "Demo User300";
        public static readonly By logOutButton = By.Id("user_logout");
        public static readonly By loggedUserName = By.CssSelector("#user_profile");

        public LoginPage(IWebDriver driver)
        {
           this.driver = driver;
        }

        public static void SignIn()
        {
            // Aquire login data from file to protect private data
            string loginDataFileLocation = BaseTest.loginDataFileLocation;

            // Create StreamReader object
            StreamReader sr = new(loginDataFileLocation);

            // Read separate lines from file and create variables "userName" and "password" to login
            string userName = sr.ReadLine();
            string password = sr.ReadLine();

            // Input username
            BaseTest.driver.FindElement(_userNameInputTab).SendKeys(userName);

            // Input password
            BaseTest.driver.FindElement(_passwordInputTab).SendKeys(password);

            // Click "Login" button
            BaseTest.driver.FindElement(_submitButton).Click();
        }

        public static void LogOut()
        {
            // Click Menu button
            BaseTest.driver.FindElement(LoginPage.loggedUserName).Click();

            // Click "Log out" button
            BaseTest.driver.FindElement(logOutButton).Click();
        }
    }
}
