namespace Geonetric.Identity.Tests.Domain.Users
{
    using System;

    using Geonetric.Identity.Domain.Users;

    using NUnit.Framework;

    using Shouldly;

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

		[Test]
		public void Equals_returns_true_for_same_reference() 
		{
			var email = new Email ("john@doe.com");
			email.Equals (email).ShouldBeTrue ();
		}

		[Test]
		public void Equals_returns_false_for_null_other_value() 
		{
			var email = new Email ("john@doe.com");
			email.Equals (null).ShouldBeFalse ();
		}

		[Test]
		public void Equals_different_references_are_still_equal() 
		{
			var email1 = new Email ("john@doe.com");
			var email2 = new Email ("john@doe.com");
			email1.Equals (email2).ShouldBeTrue ();
		}

		[Test]
		public void Equals_returns_false_for_non_equal_values() 
		{
			var email1 = new Email ("john@doe.com");
			var email2 = new Email ("johny@doe.com");
			email1.Equals (email2).ShouldBeFalse ();
		}
	}
}

