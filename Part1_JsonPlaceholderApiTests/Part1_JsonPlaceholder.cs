namespace AgAssessment
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

        [Test]
        [TestCase("Updated Title 1", "Updated Body 1")]
        public void UpdatePost(string title, string body)
        {
            // PUT to update an existing post
            RestRequest request = new RestRequest($"/posts/{_postId}", Method.Put);
            request.AddJsonBody(new
            {
                id = _postId,
                title = title,
                body = body,
                userId = _userId
            });

            RestResponse response = _client.Execute(request);
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Failed to update post with ID {_postId}, title: {title}, body: {body}. {response.Content}");
        }

        [Test]
        [TestCase("Test Comment 1", "test1@example.com", "This is a test comment 1")]
        public void AddCommentToPost(string name, string email, string body)
        {
            // POST to add a comment to an existing post
            RestRequest request = new RestRequest($"/posts/{_postId}/comments", Method.Post);
            request.AddJsonBody(new
            {
                name = name,
                email = email,
                body = body
            });

            RestResponse response = _client.Execute(request);
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.Created), $"Failed to add comment to post with ID {_postId}, name: {name}, email: {email}, body: {body}. {response.Content}");
        }

        [Test]
        public void DeletePost()
        {
            // DELETE an existing post
            RestRequest request = new RestRequest($"/posts/{_postId}", Method.Delete);

            RestResponse response = _client.Execute(request);
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Failed to delete post with ID {_postId}. {response.Content}");
        }

        [Test]
        public void InvalidPostIdReturnsNotFound()
        {
            // Use an invalid post ID for the request
            string invalidPostId = "abcde";

            RestRequest request = new RestRequest($"/posts/{invalidPostId}", Method.Get);
            RestResponse response = _client.Execute(request);

            // Expect a NotFound response
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), $"Expected NotFound for post ID {invalidPostId}, but got {response.StatusCode}. {response.Content}");
        }

    }
}
