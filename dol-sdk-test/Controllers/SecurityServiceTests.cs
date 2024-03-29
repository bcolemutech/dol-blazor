﻿using dol_sdk.Services;
using Firebase.Auth;
using FluentAssertions;
using Microsoft.Extensions.Configuration;
using NSubstitute;
using Xunit;

namespace dol_sdk_test.Controllers
{
    public class SecurityServiceTests
    {
        private readonly ISecurityService _sut;
        private readonly IFirebaseAuthProvider _authProvider;

        public SecurityServiceTests()
        {
            _authProvider = Substitute.For<IFirebaseAuthProvider>();
            var config = Substitute.For<IConfiguration>();
            
            _sut = new SecurityService(_authProvider, config);
        }

        [Fact]
        public void LoginShouldGetAndSaveUserIdentity()
        {
            var expected = new FirebaseAuthLink(_authProvider, new FirebaseAuth())
            {
                FirebaseToken =
                    "TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBtYWxlc3VhZGEgZWxlbWVudHVtIGFudGUgYXQgZmV1Z2lh",
                User = new User {Email = "test@mail.com", LocalId = "TG9yZW0gaXBzdW0="}
            };

            _authProvider.SignInWithEmailAndPasswordAsync("test@mail.com", "pass").Returns(expected);

            _sut.Login("test@mail.com", "pass");

            var actual = _sut.Identity;

            actual.FirebaseToken.Should()
                .Be(
                    "TG9yZW0gaXBzdW0gZG9sb3Igc2l0IGFtZXQsIGNvbnNlY3RldHVyIGFkaXBpc2NpbmcgZWxpdC4gQ3JhcyBtYWxlc3VhZGEgZWxlbWVudHVtIGFudGUgYXQgZmV1Z2lh");
            actual.User.LocalId.Should().Be("TG9yZW0gaXBzdW0=");
        }
    }
}