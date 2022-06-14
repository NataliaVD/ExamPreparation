
using RestSharp;
using System.Net;
using System.Text.Json;

namespace Exam
{
    public class ApiTests
    {
        private string url = "https://contactbook.nataliadimchovs.repl.co/api/contacts";
        private RestClient client;
        private RestRequest request;

        [SetUp]
        public void Setup()
        {
            this.client = new RestClient();
        }

        [Test]
        public void TestFirstContactName()
        {
            this.request = new RestRequest(url);
            var response = this.client.Execute(request);

            var contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(contacts.Count(), Is.GreaterThan(0));
            Assert.That(contacts[0].firstName, Is.EqualTo("Steve"));
        }

        [Test]
        public void TestSearchContactByName()
        {
            this.request = new RestRequest(url + "/search/{keyword}");
            request.AddUrlSegment("keyword", "albert");
            var response = this.client.Execute(request);

            var contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(contacts.Count(), Is.GreaterThan(0));
            Assert.That(contacts[0].firstName, Is.EqualTo("Albert"));
            Assert.That(contacts[0].lastName, Is.EqualTo("Einstein"));
        }

        [Test]
        public void TestSearchContactByMissingName()
        {
            this.request = new RestRequest(url + "/search/{keyword}");
            request.AddUrlSegment("keyword", "missingName");
            var response = this.client.Execute(request);

            var contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(contacts.Count(), Is.EqualTo(0));
        }

        [Test]
        public void TestCreateContactWithInvalidData()
        {
            this.request = new RestRequest(url);
            var body = new
            {
                firstName = "Natalia",
                email = "natalia@nat.lia",
                phone = "1223456"
            };
            var response = this.client.Execute(request, Method.Post);

           // var contacts = JsonSerializer.Deserialize<List<Contact>>(response.Content);

            Assert.That(response.Content, Is.EqualTo("{\"errMsg\":\"First name cannot be empty!\"}"));
            
        }

        [Test]
        public void TestCreateContactWithValidData()
        {
            this.request = new RestRequest(url);
            var body = new
            {
                firstName = "Natalia" + DateTime.Now.Ticks,
                lastName = "Natalia" + DateTime.Now.Ticks,
                email = DateTime.Now.Ticks + "natalia@nat.lia",
                //phone = "1223456" + DateTime.Now.Ticks
            };
            request.AddJsonBody(body);

            var response = this.client.Execute(request, Method.Post);

            Assert.That(response.StatusCode, Is.EqualTo(HttpStatusCode.Created));

            var allContact = this.client.Execute(request, Method.Get);

            var contacts = JsonSerializer.Deserialize<List<Contact>>(allContact.Content);
            var lastContact = contacts.Last();

            Assert.That(lastContact.firstName, Is.EqualTo(body.firstName));
            Assert.That(lastContact.lastName, Is.EqualTo(body.lastName));

        }
    }
}