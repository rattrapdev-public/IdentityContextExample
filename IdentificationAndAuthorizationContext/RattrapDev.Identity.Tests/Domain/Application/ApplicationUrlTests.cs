using System;
using NUnit.Framework;
using Shouldly;

namespace RattrapDev.Identity.Tests
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
	}
}

