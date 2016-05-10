namespace Geonetric.Identity.Tests.Domain.Applications
{
    using System;

    using Geonetric.Identity.Domain.Applications;

    using NUnit.Framework;

    using Shouldly;

    [TestFixture]
	public class AppMetadataTests
	{
		[Test]
		public void Constructor_sets_values() 
		{
			var metadata = new AppMetadata ("Name", "Description");
			metadata.Name.ShouldBe ("Name");
			metadata.Description.ShouldBe ("Description");
		}

		[TestCase("")]
		[TestCase(" ")]
		[TestCase(null)]
		public void Constructor_invalid_name_throws_exception(string invalidName) 
		{
			Should.Throw<ArgumentException> (() => new AppMetadata (invalidName, string.Empty));
		}

		[Test]
		public void Equals_different_references_can_still_be_equal() 
		{
			var metadata1 = new AppMetadata ("Name", "Description");
			var metadata2 = new AppMetadata ("Name", "Description");

			metadata1.Equals (metadata2).ShouldBeTrue();
		}
	}
}

