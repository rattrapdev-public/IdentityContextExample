namespace Geonetric.Identity.Tests.Domain.Applications
{
    using System;

    using Geonetric.Identity.Domain.Applications;

    using NUnit.Framework;

    using Shouldly;

    [TestFixture]
	public class AppSearchResultTests
	{
		[Test]
		public void Constructor_sets_values() 
		{
			var id = Guid.NewGuid();
			var item = new AppSearchResult (id, "Name");
			item.ApplicationId.ShouldBe (id);
			item.Name.ShouldBe ("Name");
		}

		[Test]
		public void Equals_different_references_can_still_be_equal() 
		{
			var id = Guid.NewGuid();
			var item1 = new AppSearchResult (id, "Name");
			var item2 = new AppSearchResult (id, "Name");

			item1.Equals (item2).ShouldBeTrue ();
		}
	}
}

