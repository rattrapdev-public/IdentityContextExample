using System;
using NUnit.Framework;
using RattrapDev.Identity.Domain.Applications;
using Shouldly;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class ApplicationSearchResultTests
	{
		[Test]
		public void Constructor_sets_values() 
		{
			var id = Guid.NewGuid();
			var item = new ApplicationSearchResult (id, "Name");
			item.ApplicationId.ShouldBe (id);
			item.Name.ShouldBe ("Name");
		}
	}
}

