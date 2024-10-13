namespace AgDataAssessment
{
    using NUnit.Framework;
    using RestSharp;
    using Newtonsoft.Json.Linq;
    using System.Net;

    public class Part1_JsonPlaceholder
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com";
        private RestClient _client;
        private int _postId;
        private int _userId;

        [SetUp]
        public void Setup()
        {
            _client = new RestClient(BaseUrl);

            // Step 1: GET an existing post ID for use in subsequent requests
            RestRequest getRequest = new RestRequest("/posts", Method.Get);
            RestResponse getResponse = _client.Execute(getRequest);
            Assert.That(getResponse?.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Failed to retrieve posts from the API.");

            // Assert that the content is not null and contains posts
            Assert.That(getResponse?.Content, Is.Not.Null, "Response content is null.");

            // Parse the content and ensure there are posts available
            JArray posts = JArray.Parse(getResponse.Content);
            Assert.That(posts.Count, Is.GreaterThan(0), "No posts found in the response.");

            // Set the post ID and user ID to the first post in the response
            _postId = (int)posts.First["id"];
            _userId = (int)posts.First["userId"];
        }

        [Test]
        [TestCase("Title 1", "Body 1", 1)]
        public void CreatePost(string title, string body, int userId)
        {
            // POST to create a new post
            RestRequest request = new RestRequest("/posts", Method.Post);
            request.AddJsonBody(new
            {
                title = title,
                body = body,
                userId = userId
            });

            RestResponse response = _client.Execute(request);
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.Created), $"Failed to create post with title: {title}, body: {body}, userId: {userId}. {response.Content}");
        }

    }
}
