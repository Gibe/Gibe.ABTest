using System.Collections.Generic;
using System.Linq;
using Gibe.AbTest.Dto;
using NUnit.Framework;

namespace Gibe.AbTest.Tests
{
	[TestFixture]
	public class AbTestingServiceTests
	{
		private IAbTestingService Service(IAbTestRepository repository)
		{
			return new AbTestingService(repository);
		}

		[Test]
		public void GetExperiments_Returns_Empty_Experiment_When_No_Experiments()
		{
			var fakeAbTestingService = new FakeAbTestRepository(new List<ExperimentDto>(), new List<VariationDto>());

			var experiments = Service(fakeAbTestingService).GetEnabledExperiments();

			var expected = new Experiment(new ExperimentDto
			{
				Id = "",
				Key = "",
				Description = "",
				StartDate = new System.DateTime(2019, 01, 01),
				EndDate = null,
				Weight = 1,
				Enabled = true
			}, new[] { new Variation(0, 1, 1, true, "", "", false) });

			AssertExperiment(experiments.First(), expected);
		}

		[Test]
		public void GetVariation_Returns_Empty_Variation_When_No_Experiments()
		{
			var fakeAbTestingService = new FakeAbTestRepository(new List<ExperimentDto>(), new List<VariationDto>());

			var variation = Service(fakeAbTestingService).GetVariation("exp1", 1);

			var expected = new Variation(0, 1, 1, true, "", "", false);


			AssertVariation(variation, expected);
		}

		[Test]
		public void GetVariations_Returns_Empty_Variation_When_No_Experiments()
		{
			var fakeAbTestingService = new FakeAbTestRepository(new List<ExperimentDto>(), new List<VariationDto>());

			var variations = Service(fakeAbTestingService).GetVariations("exp1");

			var expected = new Variation(0, 1, 1, true, "", "", false);

			AssertVariation(variations.First(), expected);
			Assert.That(variations.Count(), Is.EqualTo(1));
		}

		private void AssertExperiment(Experiment actual, Experiment expected)
		{
			Assert.That(actual.Id, Is.EqualTo(expected.Id));
			Assert.That(actual.Key, Is.EqualTo(expected.Key));
			Assert.That(actual.Description, Is.EqualTo(expected.Description));
			Assert.That(actual.StartDate, Is.EqualTo(expected.StartDate));
			Assert.That(actual.EndDate, Is.EqualTo(expected.EndDate));
			Assert.That(actual.Weight, Is.EqualTo(expected.Weight));
			Assert.That(actual.Enabled, Is.EqualTo(expected.Enabled));

			for (var i = 0; i < actual.Variations.Count(); i++)
			{
				var actualVariation = actual.Variations.ElementAt(i);
				var expectedVariation = expected.Variations.ElementAt(i);
				AssertVariation(actualVariation, expectedVariation);
			}
		}

		private void AssertVariation(Variation actual, Variation expected)
		{
			Assert.That(actual.Id, Is.EqualTo(expected.Id));
			Assert.That(actual.VariationNumber, Is.EqualTo(expected.VariationNumber));
			Assert.That(actual.Weight, Is.EqualTo(expected.Weight));
			Assert.That(actual.Enabled, Is.EqualTo(expected.Enabled));
			Assert.That(actual.Definition, Is.EqualTo(expected.Definition));
			Assert.That(actual.ExperimentId, Is.EqualTo(expected.ExperimentId));
			Assert.That(actual.DesktopOnly, Is.EqualTo(expected.DesktopOnly));
		}

	}
}
