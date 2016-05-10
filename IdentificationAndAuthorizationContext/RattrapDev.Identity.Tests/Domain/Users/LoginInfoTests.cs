namespace Geonetric.Identity.Tests.Domain.Users
{
    using System;

    using Geonetric.Identity.Domain.Users;

    using NUnit.Framework;

    using Shouldly;

    [TestFixture]
	public class LoginInfoTests
	{
		[Test]
		public void Constructor_sets_values() 
		{
			var loginInfo = new LoginInfo ("username", "password");
			loginInfo.Username.ShouldBe ("username");
			loginInfo.ValidatePassword("password").ShouldBeTrue();
		}

		[TestCase("")]
		[TestCase(null)]
		[TestCase(" ")]
		public void Constructor_invalid_username_throws_exception(string invalidUsername) 
		{
			Should.Throw<ArgumentException> (() => new LoginInfo(invalidUsername, "password"));
		}

		[TestCase("")]
		[TestCase(null)]
		[TestCase(" ")]
		public void Constructor_invalid_password_throws_exception(string invalidPassword) 
		{
			Should.Throw<ArgumentException> (() => new LoginInfo("username", invalidPassword));
		}

		[Test]
		public void Equals_returns_true_for_same_reference() 
		{
			var loginInfo = new LoginInfo ("username", "password");
			loginInfo.Equals (loginInfo).ShouldBeTrue ();
		}

		[Test]
		public void Equals_returns_false_for_null_other_value() 
		{
			var loginInfo = new LoginInfo ("username", "password");
			loginInfo.Equals (null).ShouldBeFalse ();
		}

		[Test]
		public void Equals_different_references_are_still_equal() 
		{
			var loginInfo1 = new LoginInfo ("username", "password");
			var loginInfo2 = new LoginInfo ("username", "password");
			loginInfo1.Equals (loginInfo2).ShouldBeTrue ();
		}

		[Test]
		public void Equals_returns_false_for_non_equal_values() 
		{
			var loginInfo1 = new LoginInfo ("username1", "password");
			var loginInfo2 = new LoginInfo ("username", "password");
			loginInfo1.Equals (loginInfo2).ShouldBeFalse ();
		}
	}
}

