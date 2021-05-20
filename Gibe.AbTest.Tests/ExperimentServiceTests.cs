using System.Collections.Generic;
using System.Linq;
using Gibe.Cookies;
using Moq;
using NUnit.Framework;

namespace Gibe.AbTest.Tests
{
	[TestFixture]
	public class ExperimentServiceTests
	{
		private const string CookieKey = "GCEXP";

		private Mock<ICookieService> _cookieService;
		private IAbTest _abTest;

		[SetUp]
		public void Setup()
		{
			_cookieService = new Mock<ICookieService>();
			_abTest = new FakeAbTest(
				new List<Variation>{
					new Variation(1, 0, 1,true,"{Test:'test1'}", "vapBwUPvTEuGcEVEKThGCA", false),
					new Variation(2, 1, 1,true,"{Test:'test1'}", "vapBwUPvTEuGcEVEKThGCA", false),
					new Variation(3, 0, 1,true,"{Test:'test2'}", "vapBwUPvTEuGcEVEKThGCB", false),
					new Variation(4, 1, 1,true,"{Test:'test2'}", "vapBwUPvTEuGcEVEKThGCB", false),
					new Variation(5, 0, 1,true,"{Test:'test3'}", "vapBwUPvTEuGcEVEKThGCC", false),
					new Variation(6, 1, 1,true,"{Test:'test3'}", "vapBwUPvTEuGcEVEKThGCC", false)
				});
		}

		private IExperimentService ExperimentService()
		{
			return new DefaultExperimentService(_cookieService.Object, _abTest, new ExperimentCookieValueFactory(_abTest));
		}

		[Test]
		public void IsCurrentUserInExperiment_Returns_True_When_Cookie_Contains_Experiment()
		{
			_cookieService.Setup(s => s.GetValue<string>(It.IsAny<string>())).Returns("vapBwUPvTEuGcEVEKThGCA~0-vapBwUPvTEuGcEVEKThGCB~1");

			var result = ExperimentService().IsCurrentUserInExperiment();

			Assert.That(result, Is.True);
		}

		[Test]
		public void IsCurrentUserInExperiment_Returns_False_When_No_Experiment_Cookie_Found()
		{
			_cookieService.Setup(s => s.GetValue<string>(It.IsAny<string>())).Returns("");

			var result = ExperimentService().IsCurrentUserInExperiment();

			Assert.That(result, Is.False);
		}

		[Test]
		public void CurrentUserVariations_Returns_Enumerable_Of_Variations_Based_On_The_Cookie_Value()
		{
			_cookieService.Setup(s => s.GetValue<string>(It.IsAny<string>())).Returns("vapBwUPvTEuGcEVEKThGCA~0-vapBwUPvTEuGcEVEKThGCB~1");

			var results = ExperimentService().CurrentUserVariations();

			AssertVariations(results, "vapBwUPvTEuGcEVEKThGCA~0-vapBwUPvTEuGcEVEKThGCB~1");
		}

		[Test]
		public void CurrentUserVariation_Returns_Variation_For_ExperiementId_Based_On_The_Cookie_Value()
		{
			_cookieService.Setup(s => s.GetValue<string>(It.IsAny<string>())).Returns("vapBwUPvTEuGcEVEKThGCA~0-vapBwUPvTEuGcEVEKThGCB~1");

			var results = ExperimentService().CurrentUserVariation("vapBwUPvTEuGcEVEKThGCA");

			AssertVariations(new []{results}, "vapBwUPvTEuGcEVEKThGCA~0");
		}

		[Test]
		public void AssignUserVariations_Assigns_User_To_Random_Variations_For_Each_Experiment()
		{

			var results = ExperimentService().AssignUserVariations();

			Assert.That(results.Count(), Is.EqualTo(3));
		}

		[Test]
		public void AssignUserVariations_Updates_The_Cookie_Value_For_The_Requested_Variation_Only()
		{
			_cookieService.Setup(s => s.GetValue<string>(It.IsAny<string>())).Returns("vapBwUPvTEuGcEVEKThGCA~0-vapBwUPvTEuGcEVEKThGCB~1-vapBwUPvTEuGcEVEKThGCC~0");

			var results = ExperimentService().AssignUserVariations("vapBwUPvTEuGcEVEKThGCA~1");

			AssertVariations(results, "vapBwUPvTEuGcEVEKThGCA~1-vapBwUPvTEuGcEVEKThGCB~1-vapBwUPvTEuGcEVEKThGCC~0");
		}

		[Test]
		public void AssignUserVariations_Does_Not_Change_The_Cookie_Value_For_The_Requested_Variation_When_The_Same()
		{
			_cookieService.Setup(s => s.GetValue<string>(It.IsAny<string>())).Returns("vapBwUPvTEuGcEVEKThGCA~0-vapBwUPvTEuGcEVEKThGCB~1-vapBwUPvTEuGcEVEKThGCC~0");

			var results = ExperimentService().AssignUserVariations("vapBwUPvTEuGcEVEKThGCA~0");

			AssertVariations(results, "vapBwUPvTEuGcEVEKThGCA~0-vapBwUPvTEuGcEVEKThGCB~1-vapBwUPvTEuGcEVEKThGCC~0");
		}

		[Test]
		public void AssignUserVariations_Does_Not_Change_The_Cookie_Value_When_The_Requested_Experiment_Does_Not_Exist()
		{
			_cookieService.Setup(s => s.GetValue<string>(It.IsAny<string>())).Returns("vapBwUPvTEuGcEVEKThGCA~0-vapBwUPvTEuGcEVEKThGCB~1-vapBwUPvTEuGcEVEKThGCC~0");

			var results = ExperimentService().AssignUserVariations("vapBwUPvTEuGcEVEKThGCD~0");

			AssertVariations(results, "vapBwUPvTEuGcEVEKThGCA~0-vapBwUPvTEuGcEVEKThGCB~1-vapBwUPvTEuGcEVEKThGCC~0");
		}

		[Test]
		public void AssignCurrentUserToVariation_Does_Not_Change_The_Cookie_Value_When_The_Requested_Variation_Does_Not_Exist()
		{
			_cookieService.Setup(s => s.GetValue<string>(It.IsAny<string>())).Returns("vapBwUPvTEuGcEVEKThGCA~0-vapBwUPvTEuGcEVEKThGCB~1-vapBwUPvTEuGcEVEKThGCC~0");

			var results = ExperimentService().AssignUserVariations("vapBwUPvTEuGcEVEKThGCA~3");

			AssertVariations(results, "vapBwUPvTEuGcEVEKThGCA~0-vapBwUPvTEuGcEVEKThGCB~1-vapBwUPvTEuGcEVEKThGCC~0");
		}

		[Test]
		public void AssignUserVariations_Assigns_User_To_Experiments_Not_Currently_In_When_Updating_Variation()
		{
			_cookieService.Setup(s => s.GetValue<string>(It.IsAny<string>())).Returns("vapBwUPvTEuGcEVEKThGCA~0");

			var results = ExperimentService().AssignUserVariations("vapBwUPvTEuGcEVEKThGCA~1");

			AssertVariations(results, "vapBwUPvTEuGcEVEKThGCA~1-vapBwUPvTEuGcEVEKThGCB~0-vapBwUPvTEuGcEVEKThGCC~0");
		}

		private static void AssertVariations(IEnumerable<Variation> results, string variationsString)
		{
			var variations = variationsString.Split('-')
				.Select(e => new Variation(0, int.Parse(e.Split('~')[1]), 1, true, "", e.Split('~')[0], false));

			foreach (var variation in variations)
			{
				Assert.That(results.Any(r => r.ExperimentId == variation.ExperimentId && r.VariationNumber == variation.VariationNumber));
			}
			Assert.That(results.Count(), Is.EqualTo(variations.Count()));
		}
	}
}

