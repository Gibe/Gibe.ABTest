using System;
using System.Collections.Generic;
using System.Linq;
using Gibe.Cookies;

namespace Gibe.AbTest
{
	public class DefaultExperimentService : IExperimentService
	{
		private const string CookieKey = "GCEXP";
		private readonly ICookieService _cookieService;
		private readonly IAbTest _abTest;
		private readonly IExperimentCookieValueFactory _experimentCookieValueFactory;

		public DefaultExperimentService(ICookieService cookieService,
			IAbTest abTest,
			IExperimentCookieValueFactory experimentCookieValueFactory)
		{
			_cookieService = cookieService;
			_abTest = abTest;
			_experimentCookieValueFactory = experimentCookieValueFactory;
		}

		//TODO: Not sure we need this, being in no experiments isn't a special case
		public bool IsCurrentUserInExperiment()
		{
			var allVariations = _abTest.AllCurrentVariations().ToList();
			var cookieVariations = CookieVariations(_cookieService.GetValue<string>(CookieKey)).ToList();

			return allVariations.Any(v => cookieVariations.Any(c => c.ExperimentId == v.ExperimentId));
		}

		public IEnumerable<Variation> CurrentUserVariations()
		{
			return CookieVariations(_cookieService.GetValue<string>(CookieKey));
		}

		public Variation CurrentUserVariation(string experimentId)
		{
			return CookieVariations(_cookieService.GetValue<string>(CookieKey))
				.First(v => v.ExperimentId == experimentId);
		}

		public IEnumerable<Variation> AssignUserVariations()
		{
			var variationsFromCookie = UserVariationsFromCookie();

			SaveVariationsCookie(variationsFromCookie);
			return variationsFromCookie;
		}

		///experiments/change/?value=vapBwUPvTEuGcEVEKThGCA~0
		public IEnumerable<Variation> AssignUserVariations(string value)
		{
			var variationToSet = CookieVariations(value).First();
			var variationsFromCookie = UserVariationsFromCookie();

			if (variationToSet != null)
			{
				for (var i = 0; i < variationsFromCookie.Count; i++)
				{
					if (variationsFromCookie[i].ExperimentId == variationToSet.ExperimentId)
					{
						variationsFromCookie[i] = variationToSet;
					}
				}
			}

			SaveVariationsCookie(variationsFromCookie);
			return variationsFromCookie;
		}


		private List<Variation> UserVariationsFromCookie()
		{
			var cookieVariations = CookieVariations(_cookieService.GetValue<string>(CookieKey)).ToList();

			var allVariations = _abTest.AllCurrentVariations().ToList();

			foreach (var cookieVariation in cookieVariations)
			{
				for (var i = 0; i < allVariations.Count; i++)
				{
					if (allVariations[i].ExperimentId == cookieVariation.ExperimentId)
					{
						allVariations[i] = cookieVariation;
					}
				}
			}

			return allVariations;
		}

		private void SaveVariationsCookie(IEnumerable<Variation> variations)
		{
			var variationsCookie = _experimentCookieValueFactory.ExperimentCookieValue(variations);

			_cookieService.Create(CookieKey, variationsCookie.RawValue, DateTime.UtcNow.AddDays(120));
		}

		private IEnumerable<Variation> CookieVariations(string cookieValue)
		{
			if (string.IsNullOrEmpty(cookieValue))
			{
				return new List<Variation>();
			}

			return _experimentCookieValueFactory.ExperimentCookieValue(cookieValue)
				.Variations();
		}
	}
}
