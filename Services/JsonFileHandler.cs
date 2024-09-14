using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;
using Newtonsoft.Json;

namespace W4_assignment_template.Services;

public class JsonFileHandler : IFileHandler
{
    public List<Character> ReadCharacters(string filePath)
    {
        var characters = JsonConvert.DeserializeObject<List<Character>>(File.ReadAllText(filePath));

        return characters;
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        StreamWriter writer = new StreamWriter(filePath, false);

        writer.WriteLine(JsonConvert.SerializeObject(characters));

        writer.Flush();
        writer.Close();
    }
}