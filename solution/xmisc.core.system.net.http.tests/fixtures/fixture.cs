using reexmonkey.xmisc.backbone.io.jil.serializers;
using System;
using System.Linq;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;
using Jil;

namespace xmisc.core.system.net.http.tests.fixtures
{
    [DataContract]
    public class Answer: IEquatable<Answer>
    {
        [DataMember(Name = "choice")]
        public string Choice { get; set; }

        [DataMember(Name = "votes")]
        public int Votes { get; set; }

        [DataMember(Name = "url")]
        public string Url { get; set; }

        public Answer()
        {
            Url = string.Empty;
            Choice = string.Empty;
            Votes = 0;
        }

        public Answer(string choice): this()
        {
            if (string.IsNullOrEmpty(choice)) throw new ArgumentException("message", nameof(choice));
            Choice = choice;
        }

        public bool Equals(Answer other) 
            => string.Equals(Choice, other.Choice, StringComparison.OrdinalIgnoreCase) 
            && Votes == other.Votes
            && string.Equals(Url, other.Url, StringComparison.OrdinalIgnoreCase);

        public override bool Equals(object other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other is Answer && Equals((Answer)other);
        }

        public override int GetHashCode()
        {
            var hashCode = 2005528600;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Choice);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Url);
            hashCode = hashCode * -1521134295 + Votes.GetHashCode();
            return hashCode;
        }
    }

    [DataContract]
    public class Poll: IEquatable<Poll>
    {
        [DataMember(Name = "question")]
        public string Question { get; set; }

        [DataMember(Name = "published_at")]
        public string PublishedAt { get; set; }

        [DataMember(Name = "choices")]
        public List<Answer> Choices { get; set; }

        public Poll()
        {
            if (Choices == null) Choices = new List<Answer>();
        }

        public bool Equals(Poll other)
        {
            return string.Equals(Question, other.Question, StringComparison.OrdinalIgnoreCase)
                && string.Equals(PublishedAt, other.PublishedAt, StringComparison.OrdinalIgnoreCase)
                && Choices.SequenceEqual(other.Choices);
        }

        public override bool Equals(object other)
        {
            if (ReferenceEquals(other, null)) return false;
            if (ReferenceEquals(this, other)) return true;
            return other is Poll && Equals((Poll)other);
        }

        public override int GetHashCode()
        {
            var hashCode = 43079896;
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(Question);
            hashCode = hashCode * -1521134295 + EqualityComparer<string>.Default.GetHashCode(PublishedAt);
            hashCode = hashCode * -1521134295 + EqualityComparer<List<Answer>>.Default.GetHashCode(Choices);
            return hashCode;
        }

    }

    public static class Fixture
    {
        public static HttpClient PostmanClient = new HttpClient()
        {
            BaseAddress = "https://postman-echo.com".ToUri()
        };

        public static HttpClient ApiBlueprintClient = new HttpClient()
        {
            BaseAddress = "http://polls.apiblueprint.org/".ToUri()
        };

        public static JilTextSerializer JilTextSerializer = new JilTextSerializer();

        public static JilStreamSerializer JilStreamSerializer = new JilStreamSerializer(new UTF8Encoding(false, false), 16 * 1024);

        public static Uri ToUri(this string url) => new Uri(url, UriKind.RelativeOrAbsolute);


        public static Poll FavoriteTvSeries = new Poll
        {
            Question = "Favourite TV series?",
            PublishedAt = "2018-01-30T08:40:51.620Z",
            Choices = new List<Answer>
            {
                new Answer("Game of Thrones"),
                new Answer ("Bloodline"),
                new Answer ("Ozarks"),
                new Answer ("Fargo")
            }
        };
    }
}