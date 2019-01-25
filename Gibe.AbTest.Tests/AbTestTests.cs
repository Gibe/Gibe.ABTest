using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gibe.AbTest.Tests
{
	[TestFixture]
	public class AbTestTests
	{
		private IAbTestingService _abTestingService;

		[SetUp]
		public void Setup()
		{
			_abTestingService = new FakeAbTestingService(new List<Experiment>
			{
				new Experiment("Ex1", "Exp1", "Experiement 1", 1, true, DateTime.Now, null,
					new []{
						new Variation(1, 0, 1, true, "{Exp1:'Variant 1'}", "Exp1"),
						new Variation(2, 1, 1, true, "{Exp1:'Variant 2'}", "Exp1")

					}),
				new Experiment("Ex2", "Exp2", "Experiement 2", 1, true, DateTime.Now, null,
					new []{
						new Variation(3, 0, 1, true, "{Exp2:'Variant 1'}", "Exp2"),
						new Variation(4, 1, 1, true, "{Exp2:'Variant 2'}", "Exp2")
					}),
				new Experiment("Ex3", "Exp3", "Experiement 3", 1, false, DateTime.Now, null,
					new []{
						new Variation(5, 0, 1, true, "{Exp3:'Variant 1'}", "Exp3"),
						new Variation(6, 1, 1, true, "{Exp2:'Variant 2'}", "Exp3")
					})
			});
		}

		[Test]
		public void AssignVariation_Assigns_First_Experiment_Variation_When_Random_Number_Is_0()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new [] { 0, 0 }));

			var variation = abTest.AssignVariation();

			Assert.AreEqual(_abTestingService.GetExperiments().First().Variations.First().Id, variation.Id);
		}

		[Test]
		public void AssignVariation_Assigns_Second_Experiment_Variation_When_Random_Number_Is_1()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new[] { 1, 1 }));

			var variation = abTest.AssignVariation();

			Assert.AreEqual(_abTestingService.GetExperiments().ElementAt(1).Variations.ElementAt(1).Id, variation.Id);
		}

		[Test]
		public void AssignVariation_Assigns_Second_Experiment_Variation_When_Random_Number_Is_2()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new[] { 2, 2 }));

			var variation = abTest.AssignVariation();

			Assert.AreEqual(_abTestingService.GetExperiments().ElementAt(1).Variations.ElementAt(1).Id, variation.Id);
		}

		[Test]
		public void AssignVariation_Assigns_Given_Experiment_First_Variation()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new[] { 0, 0 }));

			var variation = abTest.AssignVariation("Exp1");

			Assert.That(_abTestingService.GetExperiments().First(x => x.Key == "Exp1").Variations.First().Id, Is.EqualTo(variation.Id));
		}

		[Test]
		public void AssignVariations_Assigns_First_Experiment_Variation_From_Each_Enabled_Experiment()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new[] { 0, 0 }));

			var variations = abTest.AssignVariations().ToList();

			Assert.That(_abTestingService.GetExperiments().Where(e => e.Enabled).Select(e => e.Variations.First().Id), Is.EqualTo(variations.Select(v => v.Id)));
			Assert.That(variations.Count, Is.EqualTo(2));
		}

		[Test]
		public void GetAssignedVariation_returns_variation_from_AbTestingService()
		{
			const string experimentId = "Exp4";
			const int variationNo = 1;

			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new int[] {}));

			var variation = abTest.GetAssignedVariation(experimentId, variationNo);

			Assert.AreEqual(experimentId, variation.ExperimentId);
			Assert.AreEqual(variationNo, variation.VariationNumber);
		}
	}
}
