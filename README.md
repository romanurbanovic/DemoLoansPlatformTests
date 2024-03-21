#  DemoLoansPlatformTests

## Description
This project contains unit tests written using NUnit framework for testing the functionality of "Lendstream" Loan Management Portal (Demo).

## Prerequisites
Before running the tests, ensure you have the following installed on your machine:
- [Visual Studio 2019](https://visualstudio.microsoft.com/downloads/)
- [.NET Core SDK](https://dotnet.microsoft.com/download) (usually comes with Visual Studio)
- [Google Chrome](https://www.google.com/chrome/) version 109.0.5414.120
- [ChromeDriver] (https://chromedriver.storage.googleapis.com/index.html?path=109.0.5414.74/)
- [Nuget package] Selenium.WebDriver version 4.18.1
- [Nuget package] Selenium.Support version 4.18.1
- [Nuget package] NUnit3TestAdapter version 4.5.0
- [Nuget package] NUnit.Console version 3.17.0
- [Nuget package] NUnit version 3.14.0
- [Nuget package] Microsoft.NET.Test.Sdk version 17.9.0
- [Nuget package] DotNetSeleniumExtras.WaitHelpers version 3.11.0
- [Nuget package] coverlet.collector version 6.0.1

## Installation
1. Clone this repository to your local machine:
    git clone https://github.com/romanurbanovi/DemoLoansPlatformTests.git

2. Open the solution file `DemoLoansPlatformTests` in Visual Studio 2019.

## Usage
1. Ask me for LoginDataFile "Lendstream_login_data.txt"

2. Write "Lendstream_login_data.txt" to your local mashine.

3. Correct the "loginDataFileLocation" parameter in "Helper.cs" class to match "Lendstream_login_data.txt" file location on your mashine.

4. Build the solution by selecting `Build > Build Solution` from the menu.

5. Once the solution is built, you can run the tests in Visual Studio:
   - Open the Test Explorer window by selecting `Test > Test Explorer` from the menu.
   - Click on "Run All Tests" in the Test Explorer window.
