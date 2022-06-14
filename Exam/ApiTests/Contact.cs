using System.Text.Json.Serialization;

namespace Exam
{
    public class Contact
    {
        [JsonPropertyName("id")]
        public int id { get; set; }

        [JsonPropertyName("firstName")]
        public string firstName { get; set; }

        [JsonPropertyName("lastName")]
        public string lastName { get; set; }

        [JsonPropertyName("email")]
        public string email { get; set; }

    
       //[JsonPropertyName("phone")]
      //  public string phone { get; set; }


        [JsonPropertyName("dateCreated")]
        public string dateCreated { get; set; }


        [JsonPropertyName("comments")]
        public string comments { get; set; }

    }
}