using System;
using NUnit.Framework;
using Shouldly;

namespace RattrapDev.Identity.Tests
{
	[TestFixture]
	public class ApplicationMetadataTests
	{
		[Test]
		public void Constructor_sets_values() 
		{
			var metadata = new ApplicationMetadata ("Name", "Description");
			metadata.Name.ShouldBe ("Name");
			metadata.Description.ShouldBe ("Description");
		}

		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void Constructor_invalid_name_throws_exception(string invalidName) 
		{
			Should.Throw<ArgumentException> (() => new ApplicationMetadata (invalidName, string.Empty));
		}
	}
}

