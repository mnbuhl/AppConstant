using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppConstant.Json;

public class AppConstantJsonValueConverter<TConst, TValue> : JsonConverter<TConst> 
    where TConst : AppConstant<TConst, TValue>, new()
    where TValue : IEquatable<TValue>, IComparable<TValue>
{
    public override TConst Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType != JsonTokenType.String)
        {
            throw new JsonException($"Expected a string, but got {reader.TokenType}.");
        }
        
        var value = reader.GetString();

        try
        {
            return AppConstant<TConst, TValue>.Set((TValue)Convert.ChangeType(value, typeof(TValue)));
        }
        catch (Exception e)
        {
            throw new JsonException($"Expected a {typeof(TValue)}, but got {value}.", e);
        }
    }

    public override void Write(Utf8JsonWriter writer, TConst value, JsonSerializerOptions options)
    {
        writer.WriteStringValue(value.Value.ToString());
    }
}