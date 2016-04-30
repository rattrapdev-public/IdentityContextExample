using System;
using NUnit.Framework;
using RattrapDev.Identity.Domain.Users;
using Shouldly;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class EmailTests
	{
		[Test]
		public void Constructor_sets_value() 
		{
			var email = new Email ("john@doe.com");
			email.EmailAddress.ShouldBe ("john@doe.com");
		}

		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void Constructor_invalid_email_throws_exception(string invalidEmail) 
		{
			Should.Throw<ArgumentException> (() => new Email (invalidEmail));	
		}

		[Test]
		public void Constructor_malformed_email_throws_exception() 
		{
			Should.Throw<FormatException> (() => new Email ("john@"));	
		}
	}
}

