namespace HackAssembler.Models
{
    public class DataQuality
    {

        public string[] RemoveCommentsAndWhitespace(string[] lines)
        {
            var cleanedLines = new List<string>();

            foreach (var line in lines)
            {
                if (!line.StartsWith("("))
                {
                    // Remove comments and trim whitespace
                    string cleanLine = line.Split("//")[0].Trim();
                    if (!string.IsNullOrEmpty(cleanLine))
                    {
                        cleanedLines.Add(cleanLine);
                    }
                }
            }

            return cleanedLines.ToArray();
        }
    }
}