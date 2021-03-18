namespace Vizhiner_cipher
{
    public static class Cipher
    {
        public static string Encrypt(string text, string keyword, string alphabet)
        {
            char[] result = text.ToCharArray();
            int lastKeyChar = 0;
            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                if (!alphabet.Contains(text[i]))
                {
                    result[i] = text[i];
                    continue;
                }
                char currentSymbol = alphabet[(alphabet.IndexOf(text[i]) +
                        alphabet.IndexOf(keyword[lastKeyChar % keyword.Length])) % alphabet.Length];
                lastKeyChar++;

                if (char.IsUpper(result[i]))
                {
                    currentSymbol = char.ToUpper(currentSymbol);
                }
                result[i] = currentSymbol;
            }
            return new string(result);
        }

        public static string Decrypt(string text, string keyword, string alphabet)
        {
            char[] result = text.ToCharArray();
            int lastKeyChar = 0;
            text = text.ToLower();
            for (int i = 0; i < text.Length; i++)
            {
                if (!alphabet.Contains(text[i]))
                {
                    result[i] = text[i];
                    continue;
                }
                char currentSymbol = alphabet[(alphabet.IndexOf(text[i]) + alphabet.Length -
                    alphabet.IndexOf(keyword[lastKeyChar % keyword.Length])) % alphabet.Length];
                lastKeyChar++;

                if (char.IsUpper(result[i]))
                {
                    currentSymbol = char.ToUpper(currentSymbol);
                }
                result[i] = currentSymbol;
            }
            return new string(result);
        }
    }
}
