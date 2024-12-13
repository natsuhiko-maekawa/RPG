using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

// ReSharper disable UnusedMember.Local

namespace BattleScene.DataAccesses.JsonUtility
{
    public class MyJsonSerializer
    {
        private void Save<T>(T value, string path)
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new SkillEventJsonConverter() },
                IncludeFields = true,
                WriteIndented = true
            };

            var json = JsonSerializer.Serialize(value, options);

            using var fileStream = File.Open(path, FileMode.Open);
            fileStream.SetLength(0);
            using var streamWriter = new StreamWriter(fileStream);
            streamWriter.Write(json);
        }

        private async ValueTask<T> Load<T>(string path)
        {
            var options = new JsonSerializerOptions
            {
                Converters = { new SkillEventJsonConverter() },
                IncludeFields = true,
                WriteIndented = true
            };

            using var fileStream = File.Open(path, FileMode.Open);
            using var streamReader = new StreamReader(fileStream);
            var json = await streamReader.ReadToEndAsync();
            var value = JsonSerializer.Deserialize<T>(json, options);
            return value;
        }
    }
}