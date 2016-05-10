namespace Geonetric.Identity.Tests.Domain.Users
{
    using System;

    using Geonetric.Identity.Domain.Users;

    using NUnit.Framework;

    using Shouldly;

    [TestFixture]
	public class UserIdentifierTests
	{
		[Test]
		public void Constructor_parameterless_constructor_generates_new_identifier() 
		{
			var identifier1 = new UserIdentifier ();
			identifier1.Id.ShouldNotBe (Guid.Empty);

			var identifier2 = new UserIdentifier ();
			identifier1.Id.ShouldNotBe (identifier2.Id);
		}

		[Test]
		public void Constructor_sets_value() 
		{
			Guid id = Guid.NewGuid ();
			var identifier = new UserIdentifier (id);
			identifier.Id.ShouldBe (id);
		}

		[Test]
		public void Constructor_empty_guid_throws_exception() 
		{
			Should.Throw<ArgumentException> (() => new UserIdentifier (Guid.Empty));
		}

		[Test]
		public void Equals_different_references_can_still_be_equal() 
		{
			Guid id = Guid.NewGuid ();
			var identifier1 = new UserIdentifier (id);
			var identifier2 = new UserIdentifier (id);
			identifier1.Equals (identifier2).ShouldBeTrue ();
		}
	}
}

