using reexmonkey.xmisc.backbone.io.jil.serializers;
using reexmonkey.xmisc.backbone.io.messagepack.serializers;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Runtime.Serialization;
using System.Text;

namespace xmisc.core.system.net.http.tests.fixtures
{
    [DataContract]
    public class Answer
    {
        [DataMember(Name = "choice")]
        public string Choice { get; set; }

        [DataMember(Name = "votes")]
        public int Votes { get; set; }
    }

    [DataContract]
    public class Poll
    {
        [DataMember(Name ="question")]
        public string Question { get; set; }

        [DataMember(Name = "published_at")]
        public string PublishedAt { get; set; }

        [DataMember(Name = "choices")]
        public List<Answer> Choices { get; set; }
    }

    public static class Fixture
    {
        public static string BaseUrl = "http://polls.apiblueprint.org/";

        public static HttpClient Client = new HttpClient()
        {
            BaseAddress = BaseUrl.ToUri()
        };

        public static JilTextSerializer JilTextSerializer = new JilTextSerializer();

        public static JilStreamSerializer JilStreamSerializer = new JilStreamSerializer(new UTF8Encoding(false), 16*1024);

        public static Uri ToUri(this string url) => new Uri(url, UriKind.RelativeOrAbsolute);

        public static Uri Combine(this Uri @base, Uri relative) => new Uri(@base, relative);

        public static List<Poll> Polls = new List<Poll>
        {
            new Poll
            {
                Question = "Favourite programming language?",
                PublishedAt = "2015-08-05T08:40:51.620Z",
                Choices = new List<Answer>
                {
                    new Answer
                    {
                        Choice = "Swift",
                        Votes = 2048
                    },

                    new Answer
                    {
                        Choice = "Python",
                        Votes = 1024
                    },

                    new Answer
                    {
                        Choice = "Objective-C",
                        Votes = 512
                    },

                    new Answer
                    {
                        Choice = "Ruby",
                        Votes = 256
                    },

                }
            }
        };
    }
}