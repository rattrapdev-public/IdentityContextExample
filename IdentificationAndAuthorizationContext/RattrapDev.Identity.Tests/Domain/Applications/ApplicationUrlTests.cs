using System;
using NUnit.Framework;
using Shouldly;
using RattrapDev.Identity.Domain.Applications;

namespace RattrapDev.Identity.Tests.Domain.Application
{
	[TestFixture]
	public class ApplicationUrlTests
	{
		[Test]
		public void Constructor_strips_url_to_base() 
		{
			var applicationUrl = new ApplicationUrl ("http://www.test.com/somethingextra");
			applicationUrl.BaseUrl.ShouldBe ("http://www.test.com");
		}

		[Test]
		public void Equals_different_references_can_still_be_equal() 
		{
			var applicationUrl1 = new ApplicationUrl ("http://www.test.com");
			var applicationUrl2 = new ApplicationUrl ("http://www.test.com");
			applicationUrl1.Equals (applicationUrl2).ShouldBeTrue ();
		}
	}
}

