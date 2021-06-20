using System;
using Bunit;
using dol_sdk.Services;
using DolBlazor.Pages;
using Firebase.Auth;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using Xunit;

namespace DolBlazorTest
{
    public class TravelTests: IDisposable
    {
        private readonly TestContext _ctx;
        private readonly ISecurityService _securityService;
        private readonly IRenderedComponent<Travel> _cut;

        public TravelTests()
        {
            _ctx = new TestContext();
            
            _securityService = Substitute.For<ISecurityService>();
            _ctx.Services.AddSingleton(_securityService);
            
            _cut = _ctx.RenderComponent<Travel>();
        }

        [Fact]
        public void ShouldHaveLoginLink()
        {
            // Arrange
            _securityService.Identity.Returns(new FirebaseAuthLink(Substitute.For<IFirebaseAuthProvider>(),
                new FirebaseAuth()));

            // Act: TODO
            // cut.Find("button").Click();

            // Assert: first find the <p> element, then verify its content
            _cut.Find("h4").MarkupMatches("<h4>Log in <a href=\"/Login?returnUrl=\">here</a></h4>");
        }

        public void Dispose()
        {
            _ctx.Dispose();
        }
    }
}