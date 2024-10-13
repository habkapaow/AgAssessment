namespace Api.Tests
{
    using NUnit.Framework;
    using RestSharp;
    using Newtonsoft.Json.Linq;
    using System.Net;
    using NLog;

    public class JsonPlaceholderAPITests
    {
        private const string BaseUrl = "https://jsonplaceholder.typicode.com";
        private RestClient _client;
        private int _postId;
        private int _userId;
        private static readonly Logger Logger = LogManager.GetCurrentClassLogger();

        [SetUp]
        public void Setup()
        {
            Logger.Info("Starting test setup...");
            _client = new RestClient(BaseUrl);

            // Step 1: GET an existing post ID for use in subsequent requests
            RestRequest getRequest = new RestRequest("/posts", Method.Get);
            Logger.Info("Sending GET request to retrieve posts.");
            RestResponse getResponse = _client.Execute(getRequest);
            Logger.Info($"Received response: {getResponse.StatusCode}");

            Assert.That(getResponse?.StatusCode, Is.EqualTo(HttpStatusCode.OK), "Failed to retrieve posts from the API.");
            Assert.That(getResponse?.Content, Is.Not.Null, "Response content is null.");

            // Parse the content and ensure there are posts available
            JArray posts = JArray.Parse(getResponse.Content);
            Assert.That(posts.Count, Is.GreaterThan(0), "No posts found in the response.");

            // Log the post ID and user ID of the first post
            _postId = (int)posts.First["id"];
            _userId = (int)posts.First["userId"];
            Logger.Info($"Test will use postId: {_postId}, userId: {_userId}");
        }

        [Test]
        public void ValidateGetPostResponse()
        {
            Logger.Info($"Validating GET response for postId: {_postId}");

            // GET an existing post and validate all fields are present and correct
            RestRequest request = new RestRequest($"/posts/{_postId}", Method.Get);
            RestResponse response = _client.Execute(request);

            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Failed to retrieve post with ID {_postId}. {response.Content}");
            Logger.Info("Response content received.");

            // Assert that the content is not null
            Assert.That(response?.Content, Is.Not.Null, "Response content is null.");

            // Parse the response content
            JObject post = JObject.Parse(response.Content);

            // Validate that all required fields exist
            Assert.That(post.ContainsKey("userId"), Is.True, "userId field is missing.");
            Assert.That(post.ContainsKey("id"), Is.True, "id field is missing.");
            Assert.That(post.ContainsKey("title"), Is.True, "title field is missing.");
            Assert.That(post.ContainsKey("body"), Is.True, "body field is missing.");

            Logger.Info("GET post validation completed successfully.");
        }

        [Test]
        [TestCase("Title 1", "Body 1", 1)]
        public void CreatePost(string title, string body, int userId)
        {
            Logger.Info("Starting POST request to create a new post.");
            RestRequest request = new RestRequest("/posts", Method.Post);
            request.AddJsonBody(new
            {
                title,
                body,
                userId
            });

            RestResponse response = _client.Execute(request);
            Logger.Info($"Received response for POST request: {response.StatusCode}");
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.Created), $"Failed to create post with title: {title}, body: {body}, userId: {userId}. {response.Content}");
        }

        [Test]
        [TestCase("Updated Title 1", "Updated Body 1")]
        public void UpdatePost(string title, string body)
        {
            Logger.Info($"Updating post with postId: {_postId}");
            RestRequest request = new RestRequest($"/posts/{_postId}", Method.Put);
            request.AddJsonBody(new
            {
                id = _postId,
                title,
                body,
                userId = _userId
            });

            RestResponse response = _client.Execute(request);
            Logger.Info($"Received response for PUT request: {response.StatusCode}");
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Failed to update post with ID {_postId}, title: {title}, body: {body}. {response.Content}");
        }

        [Test]
        [TestCase("Test Comment 1", "test1@example.com", "This is a test comment 1")]
        public void AddCommentToPost(string name, string email, string body)
        {
            Logger.Info($"Adding comment to post with postId: {_postId}");
            RestRequest request = new RestRequest($"/posts/{_postId}/comments", Method.Post);
            request.AddJsonBody(new
            {
                name,
                email,
                body
            });

            RestResponse response = _client.Execute(request);
            Logger.Info($"Received response for POST comment: {response.StatusCode}");
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.Created), $"Failed to add comment to post with ID {_postId}, name: {name}, email: {email}, body: {body}. {response.Content}");
        }

        [Test]
        public void DeletePost()
        {
            Logger.Info($"Deleting post with postId: {_postId}");
            RestRequest request = new RestRequest($"/posts/{_postId}", Method.Delete);

            RestResponse response = _client.Execute(request);
            Logger.Info($"Received response for DELETE request: {response.StatusCode}");
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.OK), $"Failed to delete post with ID {_postId}. {response.Content}");
        }

        [Test]
        public void InvalidPostIdReturnsNotFound()
        {
            string invalidPostId = "abcde";
            Logger.Info($"Testing invalid GET request with postId: {invalidPostId}");

            RestRequest request = new RestRequest($"/posts/{invalidPostId}", Method.Get);
            RestResponse response = _client.Execute(request);

            Logger.Info($"Received response for invalid GET request: {response.StatusCode}");
            Assert.That(response?.StatusCode, Is.EqualTo(HttpStatusCode.NotFound), $"Expected NotFound for post ID {invalidPostId}, but got {response.StatusCode}. {response.Content}");
        }
    }
}
