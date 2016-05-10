namespace Geonetric.Identity.Tests.Domain.Users
{
    using System;

    using Geonetric.Identity.Domain.Users;

    using NUnit.Framework;

    using Shouldly;

    [TestFixture]
	public class NameTests
	{
		public void Constructor_sets_values() 
		{
			var name = new Name ("John", "Doe");
			name.FirstName.ShouldBe ("John");
			name.LastName.ShouldBe ("Doe");
		}

		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void Constructor_invalid_firstName_throws_exception(string invalidFirstName) 
		{
			Should.Throw<ArgumentException> (() => new Name (invalidFirstName, "Doe"));	
		}

		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void Constructor_invalid_lastName_throws_exception(string invalidLastName) 
		{
			Should.Throw<ArgumentException> (() => new Name ("John", invalidLastName));	
		}

		[Test]
		public void Equals_returns_true_for_same_reference() 
		{
			var name = new Name ("John", "Doe");
			name.Equals (name).ShouldBeTrue ();
		}

		[Test]
		public void Equals_returns_false_for_null_other_value() 
		{
			var name = new Name ("John", "Doe");
			name.Equals (null).ShouldBeFalse ();
		}

		[Test]
		public void Equals_different_references_are_still_equal() 
		{
			var name1 = new Name ("John", "Doe");
			var name2 = new Name ("John", "Doe");
			name1.Equals (name2).ShouldBeTrue ();
		}

		[Test]
		public void Equals_returns_false_for_non_equal_values() 
		{
			var name1 = new Name ("John", "Doe");
			var name2 = new Name ("Johny", "Doe");
			name1.Equals (name2).ShouldBeFalse ();
		}
	}
}

