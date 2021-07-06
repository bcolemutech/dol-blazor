using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using dol_sdk.Controllers;
using dol_sdk.Enums;
using dol_sdk.POCOs;
using dol_sdk.Services;
using dol_sdk_test.TestHelpers;
using Firebase.Auth;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using NSubstitute;
using Xunit;

namespace dol_sdk_test.Controllers
{
    public class AreaControllerTests
    {
        private readonly IHttpClientFactory _factory;
        private readonly IConfiguration _configuration;
        private readonly ISecurityService _securityService;

        public AreaControllerTests()
        {
            _factory = Substitute.For<IHttpClientFactory>();
            var configuration = Substitute.For<IConfiguration>();

            _configuration = configuration;
            _configuration["DolApiUri"].Returns("https://bogus.run.app/");

            var security = Substitute.For<ISecurityService>();
            var provider = Substitute.For<IFirebaseAuthProvider>();
            _securityService = security;
            _securityService.Identity.Returns(new FirebaseAuthLink(provider,
                new FirebaseAuth
                {
                    FirebaseToken = "fakeToken",
                    User = new Firebase.Auth.User
                    {
                        LocalId = "12345"
                    }
                }));
        }

        [Fact]
        public async Task GetAreaShouldUseTokenToRetrieveAnAreaAndReturnArea()
        {
            var expected = new Area
            {
                X = 1,
                Y = 1,
                Description = "Test desc",
                Region = "Test region",
                Ecosystem = Ecosystem.Arctic,
                Navigation = Navigation.Wild
            };
            
            var expectedJson = JsonConvert.SerializeObject(expected);

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK,
                Content = new StringContent(
                    expectedJson,
                    Encoding.UTF8, "application/json")
            });
            var fakeHttpClient = Substitute.For<HttpClient>(fakeHttpMessageHandler);

            _factory.CreateClient().Returns(fakeHttpClient);

            var sut = new AreaController(_factory, _configuration, _securityService);

            var actual = await sut.GetArea(1, 1);
            
            fakeHttpMessageHandler.RequestMessage.Method.Should().Be(HttpMethod.Get);
            fakeHttpMessageHandler.RequestMessage.RequestUri.Should().Be("https://bogus.run.app/area/1/1");
            Debug.Assert(fakeHttpMessageHandler.RequestMessage.Headers.Authorization != null,
                "Authorization should not be null");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Scheme.Should().Be("Bearer");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Parameter.Should().Be("fakeToken");
            

            actual.Should().BeEquivalentTo(expected);
        }
        
        [Fact]
        public async Task GivenAreaDoesNotExistWhenGetAreaThenReturnNull()
        {

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.NotFound,
            });
            var fakeHttpClient = Substitute.For<HttpClient>(fakeHttpMessageHandler);

            _factory.CreateClient().Returns(fakeHttpClient);

            var sut = new AreaController(_factory, _configuration, _securityService);

            var actual = await sut.GetArea(1, 2);
            
            fakeHttpMessageHandler.RequestMessage.Method.Should().Be(HttpMethod.Get);
            fakeHttpMessageHandler.RequestMessage.RequestUri.Should().Be("https://bogus.run.app/area/1/2");
            Debug.Assert(fakeHttpMessageHandler.RequestMessage.Headers.Authorization != null,
                "Authorization should not be null");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Scheme.Should().Be("Bearer");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization.Parameter.Should().Be("fakeToken");
            

            actual.Should().BeNull();
        }

        [Fact]
        public async Task EditAreaShouldSendPutRequestToAreaWithObjectContent()
        {
            var area = new Area
            {
                X = 1,
                Y = 1,
                Description = "Test desc",
                Region = "Test region",
                Ecosystem = Ecosystem.Arctic,
                Navigation = Navigation.Wild,
                IsCoastal = true
            };

            var fakeHttpMessageHandler = new FakeHttpMessageHandler(new HttpResponseMessage()
            {
                StatusCode = HttpStatusCode.OK
            });
            var fakeHttpClient = new HttpClient(fakeHttpMessageHandler);

            _factory.CreateClient().Returns(fakeHttpClient);
            
            var sut = new AreaController(_factory, _configuration, _securityService);

            await sut.EditArea(area);
            
            fakeHttpMessageHandler.RequestMessage.Method.Should().Be(HttpMethod.Put);
            fakeHttpMessageHandler.RequestMessage.RequestUri.Should().Be("https://bogus.run.app/area/1/1");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization?.Scheme.Should().Be("Bearer");
            fakeHttpMessageHandler.RequestMessage.Headers.Authorization?.Parameter.Should().Be("fakeToken");
            fakeHttpMessageHandler.RequestMessage.Content?.ReadAsStringAsync().Result.Should()
                .Be("{\"X\":1,\"Y\":1,\"Region\":\"Test region\",\"Description\":\"Test desc\",\"IsCoastal\":true,\"Ecosystem\":12,\"Navigation\":1}");

        }

    }
}