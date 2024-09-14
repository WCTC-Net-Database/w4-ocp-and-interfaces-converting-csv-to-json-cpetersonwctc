using W4_assignment_template.Interfaces;
using W4_assignment_template.Models;

namespace W4_assignment_template.Services;

public class CsvFileHandler : IFileHandler
{
    public List<Character> ReadCharacters(string filePath)
    {
        var characters = new List<Character>();
        string[] allLines = File.ReadAllLines(filePath);
        for (int c = 1; c < allLines.Length; c++)
        {
            string thisLine = allLines[c];
            string name;
            if (thisLine.StartsWith("\""))
            {
                thisLine = thisLine.Substring(1, thisLine.Length - 1);
                name = thisLine.Substring(0, thisLine.IndexOf('"'));
                thisLine = thisLine.Substring(thisLine.IndexOf('"') + 2, thisLine.Length - thisLine.IndexOf('"') - 2);
            }
            else
            {
                name = thisLine.Substring(0, thisLine.IndexOf(','));
                thisLine = thisLine.Substring(thisLine.IndexOf(',') + 1, thisLine.Length - thisLine.IndexOf(',') - 1);
            }
            var charData = thisLine.Split(",");
            string characterClass = charData[0];
            int level = Convert.ToInt32(charData[1]);
            int hitPoints = Convert.ToInt32(charData[2]);
            List<string> equipment = new List<string>();
            foreach (string item in charData[3].Split('|'))
            {
                equipment.Add(item);
            }
            characters.Add(new Character { Name = name, Class = characterClass, Level = level, HP = hitPoints, Equipment = equipment });
        }
        return characters;
    }

    public void WriteCharacters(string filePath, List<Character> characters)
    {
        StreamWriter writer = new StreamWriter(filePath, false);
        writer.WriteLine("Name,Class,Level,HP,Equipment");

        foreach (Character characterToSave in characters)
        {
            string itemString = "";
            foreach (string item in characterToSave.Equipment)
            {
                if (itemString == "")
                {
                    itemString = item;
                }
                else
                {
                    itemString = itemString + "|" + item;
                }
            }
            writer.WriteLine($"\"{characterToSave.Name}\",{characterToSave.Class},{characterToSave.Level},{characterToSave.HP},{itemString}");
        }
        writer.Flush();
        writer.Close();
    }
}