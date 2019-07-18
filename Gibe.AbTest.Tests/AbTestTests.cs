using System;
using System.Linq;
using NUnit.Framework;

namespace Gibe.AbTest.Tests
{
	[TestFixture]
	public class AbTestTests
	{
		private const string MobileUserAgent = "Mozilla/5.0 (Android; Mobile; rv:13.0) Gecko/13.0 Firefox/13.0";
		private const string DesktopUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:68.0) Gecko/20100101 Firefox/68.0";

		[Test]
		public void AssignVariation_assigns_first_experiment_variation_when_random_number_is_0()
		{
			var fakeAbTestingService = new FakeAbTestingService();

			var abTest = new AbTest(fakeAbTestingService, new FakeRandomNumber(new [] { 0, 0 }));

			var variation = abTest.AssignVariation(MobileUserAgent);

			Assert.AreEqual(fakeAbTestingService.GetExperiments().First().Variations.First().Id, variation.Id);
		}

		[Test]
		public void AssignVariation_assigns_second_experiment_variation_when_random_number_is_1()
		{
			var fakeAbTestingService = new FakeAbTestingService();

			var abTest = new AbTest(fakeAbTestingService, new FakeRandomNumber(new[] { 1, 1 }));

			var variation = abTest.AssignVariation(MobileUserAgent);

			Assert.AreEqual(fakeAbTestingService.GetExperiments().ElementAt(1).Variations.ElementAt(1).Id, variation.Id);
		}

		[Test]
		public void AssignVariation_does_not_assign_mobile_user_to_desktop_variant()
		{
			var fakeAbTestingService = new FakeAbTestingService();
			
			var abTest = new AbTest(fakeAbTestingService, new FakeRandomNumber(new[] { 1, 1 }));

			var variation = abTest.AssignVariation(MobileUserAgent);
			
			Assert.IsFalse(variation.DesktopOnly);
		}

		[Test]
		public void AssignVariation_assigns_desktop_user_to_desktop_variant()
		{
			var fakeAbTestingService = new FakeAbTestingService();
			
			var abTest = new AbTest(fakeAbTestingService, new FakeRandomNumber(new[] { 1, 1 }));

			var variation = abTest.AssignVariation(DesktopUserAgent);
			
			Assert.IsTrue(variation.DesktopOnly);
		}

		[Test]
		public void GetAssignedVariation_returns_variation_from_AbTestingService()
		{
			const string experimentId = "ABC";
			const int variationNo = 1;

			var fakeAbTestingService = new FakeAbTestingService();

			var abTest = new AbTest(fakeAbTestingService, new FakeRandomNumber(new int[] {}));

			var variation = abTest.GetAssignedVariation(experimentId, variationNo);

			Assert.AreEqual(experimentId, variation.ExperimentId);
			Assert.AreEqual(variationNo, variation.VariationNumber);

		}
	}
}
