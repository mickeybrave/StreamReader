namespace StreamReader.Core
{
    public class CharactersInfo : IStreamInfo
    {
        public char[] AllCharacters { get; set; }
        public Dictionary<char, int> CharactersFrequency { get; set; }
    }
}
