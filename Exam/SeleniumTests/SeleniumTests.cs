using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace SeleniumTests
{
    public class SeleniumTests
    {

        private WebDriver driver;

        [SetUp]
        public void Setup()
        {
            this.driver = new ChromeDriver();
            driver.Manage().Window.Maximize();
            driver.Url = "https://contactbook.nataliadimchovs.repl.co/";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(5);
        }

        [TearDown]
        public void Shutdown()
        {
            this.driver.Quit();

        }

        [Test]
        public void TestFirstContactName()
        {
            var contacts = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(2) > a"));
            contacts.Click();
            var firstName = driver.FindElement(By.CssSelector("tr.fname > td")).Text;
            var lastName = driver.FindElement(By.CssSelector("tr.lname > td")).Text;

            Assert.That(firstName, Is.EqualTo("Steve"));
            Assert.That(lastName, Is.EqualTo("Jobs"));
        }

        [Test]

        public void TestSearchContactByName()
        {
            var search = driver.FindElement(By.XPath("/html/body/aside/ul/li[4]/a"));
            search.Click();
            var searchedField = driver.FindElement(By.Id("keyword"));
            searchedField.Click();
            searchedField.SendKeys("albert");
            var searchButton = driver.FindElement(By.Id("search"));
            searchButton.Click();
            var firstName = driver.FindElement(By.CssSelector("tr.fname > td")).Text;
            var lastName = driver.FindElement(By.CssSelector("tr.lname > td")).Text;

            Assert.That(firstName, Is.EqualTo("Albert"));
            Assert.That(lastName, Is.EqualTo("Einstein"));
        }

        [Test]

        public void TestSearchContactByInvalidName()
        {
            var search = driver.FindElement(By.XPath("/html/body/aside/ul/li[4]/a"));
            search.Click();
            var searchedField = driver.FindElement(By.Id("keyword"));
            searchedField.Click();
            searchedField.SendKeys("invalid2635");
            var searchButton = driver.FindElement(By.Id("search"));
            searchButton.Click();
            var message = driver.FindElement(By.CssSelector("#searchResult")).Text;

            Assert.That(message, Is.EqualTo("No contacts found."));
            
        }

        [Test]

        public void TestCreateContactWithInvalidData()
        {
            var create = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(3) > a"));
            create.Click();
            var firstNameField = driver.FindElement(By.Id("firstName"));
            firstNameField.Click();
            firstNameField.SendKeys("Mateo");
            var emailField = driver.FindElement(By.Id("email"));
            emailField.Click();
            emailField.SendKeys("mati@jac.ob");
            var createButton = driver.FindElement(By.Id("create"));
            createButton.Click();
            var message = driver.FindElement(By.CssSelector("body > main > div")).Text;

            Assert.That(message, Is.EqualTo("Error: Last name cannot be empty!"));

        }

        [Test]

        public void TestCreateContactWithValidData()
        {
            var create = driver.FindElement(By.CssSelector("body > aside > ul > li:nth-child(3) > a"));
            create.Click();
            var firstNameField = driver.FindElement(By.Id("firstName"));
            firstNameField.Click();
            firstNameField.SendKeys("Mateo");
            var lastNameField = driver.FindElement(By.Id("lastName"));
            lastNameField.Click();
            lastNameField.SendKeys("Mati");
            var emailField = driver.FindElement(By.Id("email"));
            emailField.Click();
            emailField.SendKeys("mati@jac.ob");
            var createButton = driver.FindElement(By.Id("create"));
            createButton.Click();
            //var allContacts = driver.FindElement(By.XPath("/html/body/main/div"));
            //var lastContact = allContacts.Last();
            var allContacts = driver.FindElements(By.CssSelector("table.contact-entry"));
            var lastContact = allContacts.Last();
            var lastContactFirstName = lastContact.FindElement(By.CssSelector("tr.fname > td")).Text;
            var lastContactLastName = lastContact.FindElement(By.CssSelector("tr.lname > td")).Text;


            Assert.That(lastContactFirstName, Is.EqualTo("Mateo"));
            Assert.That(lastContactLastName, Is.EqualTo("Mati"));

        }
    }
}