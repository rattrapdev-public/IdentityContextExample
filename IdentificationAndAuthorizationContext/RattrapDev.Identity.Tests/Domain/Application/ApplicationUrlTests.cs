using System;
using NUnit.Framework;
using Shouldly;
using RattrapDev.Identity.Domain.Application;

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
	}
}

