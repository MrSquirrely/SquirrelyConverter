using System;
using System.Collections.Generic;

namespace Converter_Utilities.API {
    public class Quote {
        public static string GetQuote() {
            int random = new Random().Next(Quotes.Count);
            return Quotes[random];
        }

        private static readonly List<string> Quotes = new List<string>() {
            "“You must be the change you wish to see in the world.” — Gandhi",
            "“Everybody is a genius. But if you judge a fish by its ability to climb a tree, it will live its whole life believing that it is stupid.” — Albert Einstein",
            "“A life spent making mistakes is not only more honorable, but more useful than a life spent doing nothing.” — George Bernhard Shaw",
            "“He who fears he will suffer, already suffers because he fears.” — Michel De Montaigne",
            "“Love is a verb. Love — the feeling — is a fruit of love, the verb.” — Stephen Covey",
            "“Life is really simple, but we insist on making it complicated.” — Confucius",
            "“If you don’t like something, change it. If you can’t change it, change the way you think about it.” — Mary Engelbreit",
            "“In seeking happiness for others, you will find it in yourself.” — Unknown",
            "“Life is never made unbearable by circumstances, but only by lack of meaning and purpose.” — Viktor Frankl",
            "“If you want happiness for an hour — take a nap. If you want happiness for a day — go fishing. If you want happiness for a year — inherit a fortune. If you want happiness for a life time — help someone else.” — Chinese proverb",
            "“When one door of happiness closes, another opens, but often we look so long at the closed door that we do not see the one that has been opened for us.” — Helen Keller",
            "“Most people do not listen with the intent to understand; they listen with the intent to reply.” — Stephen Covey",
            "“Before you diagnose yourself with depression or low self-esteem, first make sure that you are not, in fact, just surrounded by assholes.” — Sigmund Freud",
            "“Challenges are what make life interesting and overcoming them is what makes life meaningful.” — Joshua J. Marine",
            "“A mind that is stretched by a new experience can never go back to its old dimensions.” — Oliver Wendell Holmes",
            "“I would rather die a meaningful death than to live a meaningless life.” — Corazon Aquino”"
        };
    }
}
