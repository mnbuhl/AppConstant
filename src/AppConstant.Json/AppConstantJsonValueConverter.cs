using System.Text.Json;
using System.Text.Json.Serialization;

namespace AppConstant.Json;

public class AppConstantJsonValueConverter<TConst, TValue> : JsonConverter<TConst> 
    where TConst : AppConstant<TConst, TValue>, new()
    where TValue : IEquatable<TValue>, IComparable<TValue>
{
    public override TConst? Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
    {
        if (reader.TokenType is JsonTokenType.Null)
            return null;

        TValue value = ReadJsonValue(ref reader);

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
        WriteJsonValue(ref writer, value.Value);
    }

    private static TValue ReadJsonValue(ref Utf8JsonReader reader)
    {
        var type = typeof(TValue);

        if (type == typeof(bool))
            return (TValue)(object)reader.GetBoolean();
        if (type == typeof(byte))
            return (TValue)(object)reader.GetByte();
        if (type == typeof(sbyte))
            return (TValue)(object)reader.GetSByte();
        if (type == typeof(short))
            return (TValue)(object)reader.GetInt16();
        if (type == typeof(ushort))
            return (TValue)(object)reader.GetUInt16();
        if (type == typeof(int))
            return (TValue)(object)reader.GetInt32();
        if (type == typeof(uint))
            return (TValue)(object)reader.GetUInt32();
        if (type == typeof(long))
            return (TValue)(object)reader.GetInt64();
        if (type == typeof(ulong))
            return (TValue)(object)reader.GetUInt64();
        if (type == typeof(float))
            return (TValue)(object)reader.GetSingle();
        if (type == typeof(double))
            return (TValue)(object)reader.GetDouble();
        if (type == typeof(decimal))
            return (TValue)(object)reader.GetDecimal();
        if (type == typeof(string))
            return (TValue)(object)reader.GetString()!;
        if (type == typeof(Guid))
            return (TValue)(object)reader.GetGuid();
        if (type == typeof(DateTime))
            return (TValue)(object)reader.GetDateTime();
        if (type == typeof(DateTimeOffset))
            return (TValue)(object)reader.GetDateTimeOffset();


        throw new ArgumentException($"Unsupported type: ${type.Name}");
    }

    private static void WriteJsonValue(ref Utf8JsonWriter writer, TValue? value)
    {
        if (value is null)
            writer.WriteNullValue();
        
        switch (value)
        {
            case bool b:
                writer.WriteBooleanValue(b);
                break;
            case byte by:
                writer.WriteNumberValue(by);
                break;
            case short sh:
                writer.WriteNumberValue(sh);
                break;
            case ushort ush:
                writer.WriteNumberValue(ush);
                break;
            case int i:
                writer.WriteNumberValue(i);
                break;
            case uint ui:
                writer.WriteNumberValue(ui);
                break;
            case long l:
                writer.WriteNumberValue(l);
                break;
            case ulong ul:
                writer.WriteNumberValue(ul);
                break;
            case float f:
                writer.WriteNumberValue(f);
                break;
            case double d:
                writer.WriteNumberValue(d);
                break;
            case decimal de:
                writer.WriteNumberValue(de);
                break;
            case string s:
                writer.WriteStringValue(s);
                break;
            case DateTime dt:
                writer.WriteStringValue(dt);
                break;
            case DateTimeOffset dto:
                writer.WriteStringValue(dto);
                break;
            case Guid g:
                writer.WriteStringValue(g);
                break;
            default:
                writer.WriteStringValue(value?.ToString());
                break;
        }
    }
}