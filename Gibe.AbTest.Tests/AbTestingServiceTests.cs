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
		public void GetExperiments_Returns_Experiments()
		{
			var experiments = Service(AbTestRepositoryEnabledExperimentsAndVariations()).GetExperiments();

			AssertExperiment(experiments.First(), TestExperiment());
		}

		[Test]
		public void GetExperiments_Returns_Empty_Experiment_When_No_Experiments()
		{
			var experiments = Service(AbTestRepositoryNoExperiments()).GetExperiments();

			AssertExperiment(experiments.First(), EmptyExperiment());
		}

		[Test]
		public void GetExperiments_Returns_Empty_Experiment_When_No_Enabled_Experiments()
		{
			var experiments = Service(AbTestRepositoryNoEnabledExperiments()).GetExperiments();

			AssertExperiment(experiments.First(), EmptyExperiment());
		}

		[Test]
		public void GetExperiments_Returns_Empty_Experiment_When_No_Variations()
		{
			var experiments = Service(AbTestRepositoryNoVariations()).GetExperiments();

			AssertExperiment(experiments.First(), EmptyExperiment());
		}

		[Test]
		public void GetExperiments_Returns_Empty_Experiment_When_No_Enabled_Variations()
		{
			var experiments = Service(AbTestRepositoryNoEnabledVariations()).GetExperiments();

			AssertExperiment(experiments.First(), EmptyExperiment());
		}

		[Test]
		public void GetVariation_Returns_Variations()
		{
			var variation = Service(AbTestRepositoryEnabledExperimentsAndVariations()).GetVariation("exp1", 1);

			AssertVariation(variation, TestVariation());
		}

		[Test]
		public void GetVariation_Returns_Empty_Variation_When_No_Experiments()
		{
			var variation = Service(AbTestRepositoryNoExperiments()).GetVariation("exp1", 1);

			AssertVariation(variation, EmptyVariation());
		}

		[Test]
		public void GetVariation_Returns_Empty_Variation_When_No_Enabled_Experiments()
		{
			var variation = Service(AbTestRepositoryNoEnabledExperiments()).GetVariation("exp1", 1);

			AssertVariation(variation, EmptyVariation());
		}

		[Test]
		public void GetVariation_Returns_Empty_Variation_When_No_Variations()
		{
			var variation = Service(AbTestRepositoryNoVariations()).GetVariation("exp1", 1);

			AssertVariation(variation, EmptyVariation());
		}

		[Test]
		public void GetVariation_Returns_Empty_Variation_When_No_Enabled_Variations()
		{
			var variation = Service(AbTestRepositoryNoEnabledVariations()).GetVariation("exp1", 1);

			AssertVariation(variation, EmptyVariation());
		}

		[Test]
		public void GetVariation_Returns_Empty_Variation_When_Given_Variation_Is_Not_Enabled_When_There_Is_another_Variation_Which_Is_Enabled()
		{
			var variation = Service(AbTestRepositoryOneEnabledAndOneDisabledVariations()).GetVariation("exp1", 1);

			AssertVariation(variation, EmptyVariation());
		}

		[Test]
		public void GetVariations_Returns_Variations()
		{
			var variations = Service(AbTestRepositoryEnabledExperimentsAndVariations()).GetVariations("exp1");

			AssertVariation(variations.First(), TestVariation());
			Assert.That(variations.Count(), Is.EqualTo(1));
		}

		[Test]
		public void GetVariations_Returns_Empty_Variation_When_No_Experiements()
		{
			var variations = Service(AbTestRepositoryNoExperiments()).GetVariations("exp1");

			AssertVariation(variations.First(), EmptyVariation());
			Assert.That(variations.Count(), Is.EqualTo(1));
		}

		[Test]
		public void GetVariations_Returns_Empty_Variation_When_No_Enabled_Experiements()
		{
			var variations = Service(AbTestRepositoryNoEnabledExperiments()).GetVariations("exp1");

			AssertVariation(variations.First(), EmptyVariation());
			Assert.That(variations.Count(), Is.EqualTo(1));
		}

		[Test]
		public void GetVariations_Returns_Empty_Variation_When_No_Variations()
		{
			var variations = Service(AbTestRepositoryNoVariations()).GetVariations("exp1");

			AssertVariation(variations.First(), EmptyVariation());
			Assert.That(variations.Count(), Is.EqualTo(1));
		}

		[Test]
		public void GetVariations_Returns_Empty_Variation_When_No_Enabled_Variations()
		{
			var variations = Service(AbTestRepositoryNoEnabledVariations()).GetVariations("exp1");

			AssertVariation(variations.First(), EmptyVariation());
			Assert.That(variations.Count(), Is.EqualTo(1));
		}

		private Experiment TestExperiment()
		{
			return new Experiment(TestExperimentDto(), new[] { TestVariation() });
		}

		private ExperimentDto TestExperimentDto()
		{
			return new ExperimentDto
			{
				Id = "exp1",
				Key = "exp1",
				Description = "test experiment",
				StartDate = new System.DateTime(2019, 01, 01),
				EndDate = null,
				Weight = 1,
				Enabled = true
			};
		}

		private Variation TestVariation()
		{
			return new Variation(TestVariationDto());
		}

		private VariationDto TestVariationDto()
		{
			return new VariationDto
			{
				Id = 0,
				ExperimentId = "exp1",
				VariationNumber = 1,
				Weight = 1,
				Enabled = true,
				Definition = "",
				DesktopOnly = false
			};
		}

		private Experiment EmptyExperiment()
		{
			return new Experiment(new ExperimentDto
			{
				Id = "",
				Key = "",
				Description = "",
				StartDate = new System.DateTime(2019, 01, 01),
				EndDate = null,
				Weight = 1,
				Enabled = true
			}, new[] { EmptyVariation() });
		}

		private Variation EmptyVariation()
		{
			return new Variation(0, 1, 1, true, "", "", false);
		}

		private FakeAbTestRepository AbTestRepositoryEnabledExperimentsAndVariations() => new FakeAbTestRepository(new List<ExperimentDto> { TestExperimentDto() }, new List<VariationDto> { TestVariationDto() });

		private FakeAbTestRepository AbTestRepositoryNoExperiments() => new FakeAbTestRepository(new List<ExperimentDto>(), new List<VariationDto> { new VariationDto { ExperimentId = "exp1", Enabled = true } });

		private FakeAbTestRepository AbTestRepositoryNoEnabledExperiments() => new FakeAbTestRepository(new List<ExperimentDto> { new ExperimentDto { Enabled = false } }, new List<VariationDto> { new VariationDto { ExperimentId = "exp1", Enabled = true } });

		private FakeAbTestRepository AbTestRepositoryNoVariations() => new FakeAbTestRepository(new List<ExperimentDto> { new ExperimentDto { Enabled = true } }, new List<VariationDto>());

		private FakeAbTestRepository AbTestRepositoryNoEnabledVariations() => new FakeAbTestRepository(new List<ExperimentDto> { new ExperimentDto { Enabled = true } }, new List<VariationDto> { new VariationDto { ExperimentId = "exp1", Enabled = false } });

		private FakeAbTestRepository AbTestRepositoryOneEnabledAndOneDisabledVariations() => new FakeAbTestRepository(new List<ExperimentDto> { new ExperimentDto { Enabled = true } }, new List<VariationDto> { new VariationDto { ExperimentId = "exp1", Enabled = false }, new VariationDto { ExperimentId = "exp2", Enabled = true } });


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
