using System;
using NUnit.Framework;
using Shouldly;
using RattrapDev.Identity.Domain.Applications;

namespace RattrapDev.Identity.Tests.Domain.Application
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

		[Test]
		public void Equals_different_references_can_still_be_equal() 
		{
			var metadata1 = new ApplicationMetadata ("Name", "Description");
			var metadata2 = new ApplicationMetadata ("Name", "Description");

			metadata1.Equals (metadata2).ShouldBeTrue();
		}
	}
}

