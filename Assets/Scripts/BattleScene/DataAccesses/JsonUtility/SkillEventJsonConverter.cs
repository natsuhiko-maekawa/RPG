using System;
using System.Text.Json;
using System.Text.Json.Serialization;
using BattleScene.Domain.ValueObjects.SkillEventValueObject.Interface;

namespace BattleScene.DataAccesses.JsonUtility
{
    public class SkillEventJsonConverter : JsonConverter<ISkillEventValueObject>
    {
        public override ISkillEventValueObject Read(
            ref Utf8JsonReader reader,
            Type typeToConvert,
            JsonSerializerOptions options)
        {
            throw new NotImplementedException();
        }

        public override void Write(Utf8JsonWriter writer, ISkillEventValueObject value, JsonSerializerOptions options)
        {
            var type = value.GetType();
            JsonSerializer.Serialize(writer, value, type, options);
        }
    }
}