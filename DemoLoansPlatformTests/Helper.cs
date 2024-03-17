namespace DemoLoansPlatformTests
{
    // Class for common parameters and methods of the project
    public static class Helper
    {
        public static string mainPageUrl = "https://demo.loansplatform.com/lms/";
        public static string chromeDriverLocation = "C:\\Users\\Asus\\Downloads\\chromedriver_win32";
        public static string loginDataFileLocation = "C:\\Users\\Asus\\Desktop\\Lendstream_login_data.txt";

        // Method to open page with current url
        public static void OpenPage(string url)
        {
            BaseTest.driver.Navigate().GoToUrl(url);
        }
     }
}
