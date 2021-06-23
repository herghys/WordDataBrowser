using System.Collections.Generic;

namespace WordDataBrowser
{
    public class WordData
    {
        public Difficulties difficulty { get; set; }
        public string difficultyString {get; set;}
        public List<string> wordList { get; set; }
    }
}
