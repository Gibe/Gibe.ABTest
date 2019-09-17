using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Gibe.NPoco;
using NUnit.Framework;

namespace Gibe.AbTest.Tests
{
	[TestFixture]
	public class AbTestRepositoryTests
	{
		private const string ValidExperimentId = "vapBwUPvTEuGcEVEKThGCA";
		private const string InvalidExperimentId = "NotRealId";
		
		[Test]
		public void GetExperiments_Returns_All_Experiments()
		{
			var experiments = Repo().GetEnabledExperiments().ToArray();

			Assert.That(experiments.Length, Is.EqualTo(2));
		}

		[Test]
		public void GetExperiment_Returns_Experiment_When_Id_Exists()
		{
			var experiment = Repo().GetExperiment(ValidExperimentId);

			Assert.That(experiment, Is.Not.Null);
			Assert.That(experiment.Id, Is.EqualTo(ValidExperimentId));
		}

		[Test]
		public void GetExperiment_Returns_Null_When_Id_Does_Not_Exist()
		{
			var experiment = Repo().GetExperiment(InvalidExperimentId);

			Assert.That(experiment, Is.Null);
		}

		[Test]
		public void GetVariations_Returns_All_Variations_For_Experiment_When_Id_Exists()
		{
			var variations = Repo().GetVariations(ValidExperimentId).ToArray();

			Assert.That(variations.Count(), Is.EqualTo(2));
			Assert.That(variations.All(v => v.ExperimentId == ValidExperimentId), Is.True);
		}

		[Test]
		public void GetVariations_Returns_Empty_Enumerable_For_Experiment_When_Id_Does_Not_Exist()
		{
			var variations = Repo().GetVariations(InvalidExperimentId);

			Assert.That(variations.Count(), Is.EqualTo(0));
		}

		private IAbTestRepository Repo() => new AbTestRepository(new DefaultDatabaseProvider("GibeCommerce"));


	}
}
