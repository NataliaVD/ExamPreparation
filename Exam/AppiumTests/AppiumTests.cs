using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Enums;
using OpenQA.Selenium.Appium.Windows;

namespace AppiumTests
{
    public class AppiumTests
    {
        private WindowsDriver<WindowsElement> driver;
        private const string AppiumServer = "http://127.0.0.1:4723/wd/hub";
        private AppiumOptions options;

        [SetUp]
        public void Setup()
        {
            this.options = new AppiumOptions() { PlatformName = "Windows" };
            options.AddAdditionalCapability(MobileCapabilityType.App, @"C:\ContactBook-DesktopClient (1)\ContactBook-DesktopClient.exe");
            this.driver = new WindowsDriver<WindowsElement>(new Uri(AppiumServer), options);
        }

        [TearDown]
        public void CloseApp()
        {
           this.driver.Quit();
        }

        [Test]
        public void TestSearchedByName()
        {
            var buttonConnect = driver.FindElementByAccessibilityId("buttonConnect");
            buttonConnect.Click();

            string windowsName = driver.WindowHandles[0];
            driver.SwitchTo().Window(windowsName);

            var editForm = driver.FindElementByAccessibilityId("textBoxSearch");
            editForm.SendKeys("albert");
            var buttonSearch = driver.FindElementByAccessibilityId("buttonSearch");
            buttonSearch.Click();

            Thread.Sleep(2000);

            var firstName = driver.FindElementByXPath("//Edit[@Name=\"FirstName Row 0, Not sorted.\"]").Text;
            var lastName = driver.FindElementByXPath("//Edit[@Name=\"LastName Row 0, Not sorted.\"]").Text;

            Assert.That(firstName, Is.EqualTo("Albert"));
            Assert.That(lastName, Is.EqualTo("Einstein"));
        }
    }
}