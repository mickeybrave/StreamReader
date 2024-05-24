namespace StreamReader.Core
{
    public class CharacterInfoCalculator : IStreamInfoCalculator
    {
        public IStreamInfo GetStreamInfo(string text)
        {
            var chars = text.ToCharArray();
            var freq = new Dictionary<char, int>();

            for (int i = 0; i < chars.Length; i++)
            {
                if (chars[i] == ' ' || chars[i] == '.' || chars[i] == ',')
                    continue;

                if (!freq.ContainsKey(char.ToLower(chars[i])))
                {
                    freq.Add(char.ToLower(chars[i]), 1);
                }
                else
                {
                    freq[char.ToLower(chars[i])]++;
                }

            }

            return new CharactersInfo()
            {
                AllCharacters = chars,
                CharactersFrequency = freq
            };
        }

    }
}
