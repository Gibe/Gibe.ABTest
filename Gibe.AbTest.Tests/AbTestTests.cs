using System;
using System.Linq;
using NUnit.Framework;

namespace Gibe.AbTest.Tests
{
	[TestFixture]
	public class AbTestTests
	{
		[Test]
		public void AssignVariation_assigns_first_experiment_variation_when_random_number_is_0()
		{
			var fakeAbTestingService = new FakeAbTestingService();

			var abTest = new AbTest(fakeAbTestingService, new FakeRandomNumber(new [] { 0, 0 }));

			var variation = abTest.AssignVariation();

			Assert.AreEqual(fakeAbTestingService.GetExperiments().First().Variations.First().Id, variation.Id);
		}

		[Test]
		public void AssignVariation_assigns_second_experiment_variation_when_random_number_is_1()
		{
			var fakeAbTestingService = new FakeAbTestingService();

			var abTest = new AbTest(fakeAbTestingService, new FakeRandomNumber(new[] { 1, 1 }));

			var variation = abTest.AssignVariation();

			Assert.AreEqual(fakeAbTestingService.GetExperiments().ElementAt(1).Variations.ElementAt(1).Id, variation.Id);
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
