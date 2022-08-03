using System;
using System.Collections.Generic;

namespace Khaled.Helpers
{
	public class SupportedLanguages
	{
		public static List<DisplayedLanguage> ReturnLanguages()
		{
			List<DisplayedLanguage> res = new List<DisplayedLanguage>();
			DisplayedLanguage ger = new DisplayedLanguage
			{
				languageName = "deutsch",
				languageCode = "german",
			};
			DisplayedLanguage eng = new DisplayedLanguage
			{
				languageName = "english",
				languageCode = "english",
			};
			DisplayedLanguage ar = new DisplayedLanguage
			{
				languageName = "عربي",
				languageCode = "arabic",
			};

			res.Add(ger);
			res.Add(eng);
			res.Add(ar);

			return res; 
        }
	}


	public class DisplayedLanguage
	{
		public string languageName { get; set; }
		public string languageCode { get; set; }
	}
}

