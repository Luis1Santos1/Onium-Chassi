using System.Text;

namespace VehicleAPI.Services
{
    public static class ChassiConverter
    {
        public static string ConvertToBinaryAndHex(string input)
        {
            List<string> chassiAsciiList = new List<string>();
            foreach (char c in input)
            {
                chassiAsciiList.Add(((int)c).ToString());
            }

            List<string> chassiBinarioList = new List<string>();
            List<string> chassiHexadecimalList = new List<string>();
            foreach (string asciiValue in chassiAsciiList)
            {
                int asciiDecimal = int.Parse(asciiValue);
                string asciiBinario = Convert.ToString(asciiDecimal, 2);
                string asciiHexadecimal = Convert.ToString(asciiDecimal, 16);
                chassiBinarioList.Add(asciiBinario);
                chassiHexadecimalList.Add(asciiHexadecimal);
            }

            string chassiBinario = string.Join(" ", chassiBinarioList);
            string chassiHexadecimal = string.Join(" ", chassiHexadecimalList);

            return chassiHexadecimal;
        }

        public static string ConvertFromBinaryAndHex(string hexValue)
        {
            string[] hexChunks = hexValue.Split(' ');

            List<string> asciiCharacters = new List<string>();

            foreach (string hexChunk in hexChunks)
            {
                if (!string.IsNullOrEmpty(hexChunk))
                {
                    int asciiDecimal = Convert.ToInt32(hexChunk, 16);
                    asciiCharacters.Add(((char)asciiDecimal).ToString());
                }
            }

            string originalText = string.Join("", asciiCharacters);

            return originalText;
        }


    }
}
