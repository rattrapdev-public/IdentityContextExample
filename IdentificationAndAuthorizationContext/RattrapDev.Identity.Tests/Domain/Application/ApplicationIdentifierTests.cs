using System;
using NUnit.Framework;
using Shouldly;
using RattrapDev.Identity.Domain.Application;

namespace RattrapDev.Identity.Tests.Domain.Application
{
	[TestFixture]
	public class ApplicationIdentifierTests
	{
		[Test]
		public void Constructor_parameterless_constructor_generates_new_identifier() 
		{
			var identifier1 = new ApplicationIdentifier ();
			identifier1.Id.ShouldNotBe (Guid.Empty);

			var identifier2 = new ApplicationIdentifier ();
			identifier1.Id.ShouldNotBe (identifier2.Id);
		}

		[Test]
		public void Constructor_sets_value() 
		{
			Guid id = Guid.NewGuid ();
			var identifier = new ApplicationIdentifier (id);
			identifier.Id.ShouldBe (id);
		}

		[Test]
		public void Constructor_empty_guid_throws_exception() 
		{
			Should.Throw<ArgumentException> (() => new ApplicationIdentifier (Guid.Empty));
		}

		[Test]
		public void Equals_different_references_can_still_be_equal() 
		{
			Guid id = Guid.NewGuid ();
			var identifier1 = new ApplicationIdentifier (id);
			var identifier2 = new ApplicationIdentifier (id);
			identifier1.Equals (identifier2).ShouldBeTrue ();
		}
	}
}

