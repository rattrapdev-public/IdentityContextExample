using System;
using NUnit.Framework;
using Shouldly;
using RattrapDev.Identity.Domain.Applications;

namespace RattrapDev.Identity.Tests.Domain.Application
{
	[TestFixture]
	public class AppIdentifierTests
	{
		[Test]
		public void Constructor_parameterless_constructor_generates_new_identifier() 
		{
			var identifier1 = new AppIdentifier ();
			identifier1.Id.ShouldNotBe (Guid.Empty);

			var identifier2 = new AppIdentifier ();
			identifier1.Id.ShouldNotBe (identifier2.Id);
		}

		[Test]
		public void Constructor_sets_value() 
		{
			Guid id = Guid.NewGuid ();
			var identifier = new AppIdentifier (id);
			identifier.Id.ShouldBe (id);
		}

		[Test]
		public void Constructor_empty_guid_throws_exception() 
		{
			Should.Throw<ArgumentException> (() => new AppIdentifier (Guid.Empty));
		}

		[Test]
		public void Equals_different_references_can_still_be_equal() 
		{
			Guid id = Guid.NewGuid ();
			var identifier1 = new AppIdentifier (id);
			var identifier2 = new AppIdentifier (id);
			identifier1.Equals (identifier2).ShouldBeTrue ();
		}
	}
}

