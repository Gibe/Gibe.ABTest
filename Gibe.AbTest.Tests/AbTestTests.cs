using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Gibe.AbTest.Tests
{
	[TestFixture]
	public class AbTestTests
	{
		private const string MobileUserAgent = "Mozilla/5.0 (Android; Mobile; rv:13.0) Gecko/13.0 Firefox/13.0";
		private const string DesktopUserAgent = "Mozilla/5.0 (Windows NT 10.0; Win64; x64; rv:68.0) Gecko/20100101 Firefox/68.0";
		private IAbTestingService _abTestingService;

		[SetUp]
		public void Setup()
		{
			_abTestingService = new FakeAbTestingService(new List<Experiment>
			{
				new Experiment("Ex1", "Exp1", "Experiment 1", 1, true, DateTime.Now, null,
					new []{
						new Variation(1, 0, 1, true, "{Exp1:'Variant 1'}", "Exp1", false),
						new Variation(2, 1, 1, true, "{Exp1:'Variant 2'}", "Exp1", false)

					}),
				new Experiment("Ex2", "Exp2", "Experiment 2", 1, true, DateTime.Now, null,
					new []{
						new Variation(3, 0, 1, true, "{Exp2:'Variant 1'}", "Exp2", false),
						new Variation(4, 1, 1, true, "{Exp2:'Variant 2'}", "Exp2", false)
					}),
				new Experiment("Ex3", "Exp3", "Experiment 3", 1, false, DateTime.Now, null,
					new []{
						new Variation(5, 0, 1, true, "{Exp3:'Variant 1'}", "Exp3", false),
						new Variation(6, 1, 1, true, "{Exp2:'Variant 2'}", "Exp3", false)
					})
			});
		}

		[Test]
		public void AssignVariation_Assigns_First_Experiment_Variation_When_Random_Number_Is_0()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new [] { 0, 0 }));

			var variation = abTest.AssignVariation(MobileUserAgent);
			Assert.AreEqual(_abTestingService.GetEnabledExperiments().First().Variations.First().Id, variation.Id);
		}

		[Test]
		public void AssignVariation_Assigns_Second_Experiment_Variation_When_Random_Number_Is_1()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new[] { 1, 1 }));

			var variation = abTest.AssignRandomVariation();

			Assert.AreEqual(_abTestingService.GetEnabledExperiments().ElementAt(1).Variations.ElementAt(1).Id, variation.Id);
		}

		[Test]
		public void AssignVariation_Assigns_Second_Experiment_Variation_When_Random_Number_Is_2()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new[] { 2, 2 }));

			var variation = abTest.AssignVariation(MobileUserAgent);
			Assert.AreEqual(_abTestingService.GetEnabledExperiments().ElementAt(1).Variations.ElementAt(1).Id, variation.Id);
		}

		[Test]
		public void AssignVariation_Assigns_Given_Experiment_First_Variation()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new[] { 0, 0 }));

			var variation = abTest.AssignVariation("Exp1");

			Assert.That(_abTestingService.GetEnabledExperiments().First(x => x.Key == "Exp1").Variations.First().Id, Is.EqualTo(variation.Id));
		}

		[Test]
		public void AssignVariation_does_not_assign_mobile_user_to_desktop_variant()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new[] { 1, 1 }));

			var variation = abTest.AssignVariation(MobileUserAgent);
			
			Assert.IsFalse(variation.DesktopOnly);
		}

		[Test]
		public void AssignVariation_assigns_desktop_user_to_desktop_variant()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new[] { 1, 1 }));

			var variation = abTest.AssignVariation(DesktopUserAgent);
			
			Assert.IsTrue(variation.DesktopOnly);
		}

		[Test]
		public void AssignVariations_Assigns_First_Experiment_Variation_From_Each_Enabled_Experiment()
		{
			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new[] { 0, 0 }));

			var variations = abTest.AllCurrentVariations().ToList();

			Assert.That(_abTestingService.GetEnabledExperiments().Where(e => e.Enabled).Select(e => e.Variations.First().Id), Is.EqualTo(variations.Select(v => v.Id)));
			Assert.That(variations.Count, Is.EqualTo(2));
		}

		[Test]
		public void GetAssignedVariation_returns_variation_from_AbTestingService()
		{
			const string experimentId = "Exp4";
			const int variationNo = 1;

			var abTest = new AbTest(_abTestingService, new FakeRandomNumber(new int[] {}));

			var variation = abTest.Variation(experimentId, variationNo);

			Assert.AreEqual(experimentId, variation.ExperimentId);
			Assert.AreEqual(variationNo, variation.VariationNumber);
		}
	}
}
