using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;

namespace hkhCoreLib
{
    public enum DateTimeContent
    {
        DateOnly,
        TimeOnly,
        DateAndTime
    };

    public static class DebugHelp
    {
        private const int SpacesPerIndentLevel = 2;
        private const long DateTimeTicksPerDay = 864000000000L;
        private const string BooleanFalseRepresentation = "False/No/Off/0";
        private const string BooleanTrueRepresentation = "True/Yes/On/1";
        private const string DateFormat = "dd.MM.yyyy";
        private const string TimeFormat = "HH:mm:ss";
        private const string DateAndTimeFormat = "dd.MM.yyyy HH:mm:ss";
        private const string NumericIntegerFormat = "N0";
        private const string Numeric2DecimalsFormat = "N2";
        private const string Numeric4DecimalsFormat = "N4";

        private static readonly List<string> RecognizedClasses = new List<string>
        {
            "String"
        };

        public static string Properties(object o, int indentLevel = 0, bool recursive = false)
        {
            string indent = new string(' ', indentLevel * SpacesPerIndentLevel);
            StringBuilder sb = new StringBuilder();
            Type type = o.GetType();
            if (!recursive)
            {
                sb.Append(indent);
                sb.Append(type.Name);
            }
            indent = new string(' ', (indentLevel + 1) * SpacesPerIndentLevel);
            IList<PropertyInfo> props = new List<PropertyInfo>(type.GetProperties(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance));
            foreach (PropertyInfo prop in props)
            {
                sb.AppendLine();
                sb.Append(indent);
                sb.Append(prop.Name);
                sb.Append(" = ");
                Type propType = prop.PropertyType;
                object propertyValue = prop.GetValue(o, new object[] { });
                if (propType.IsClass && (!RecognizedClasses.Contains(propType.Name)))
                {
                    HandleClass(sb, propertyValue, indentLevel);
                }
                else if (propType.IsEnum)
                {
                    HandleEnum(sb, propertyValue);
                }
                else
                {
                    HandlePlainType(sb, propertyValue);
                }
            }
            return sb.ToString();
        }

        private static void HandleClass(StringBuilder sb, object propertyValue, int indentLevel)
        {
            Type propType = propertyValue.GetType();
            sb.Append(propType.Name);
            sb.Append(Properties(propertyValue, indentLevel + 1, true));
        }

        private static void HandleEnum(StringBuilder sb, object propertyValue)
        {
            sb.Append("Enum");
            Type propType = propertyValue.GetType();
            Type underlyingType = Enum.GetUnderlyingType(propType);
            string underlyingTypeName = underlyingType.Name;
            sb.Append('(');
            sb.Append(ClrAndCSharpNameFor(underlyingTypeName));
            sb.Append("): ");
            sb.Append(propType.Name);
            sb.Append('.');
            if (propType.IsEnumDefined(propertyValue))
            {
                sb.Append(propertyValue);
            }
            else
            {
                sb.Append("<undefined>");
            }
            sb.Append(" (");
            PlainTypeNameAndValue(sb, underlyingTypeName, propertyValue);
            sb.Append(')');
        }

        private static void HandlePlainType(StringBuilder sb, object propertyValue)
        {
            Type propType = propertyValue.GetType();
            string propTypeName = propType.Name;
            sb.Append(ClrAndCSharpNameFor(propTypeName));
            sb.Append(": ");
            PlainTypeNameAndValue(sb, propTypeName, propertyValue);
        }

        private static void PlainTypeNameAndValue(StringBuilder sb, string typeName, object value)
        {
            switch (typeName)
            {
                case "Boolean":
                    sb.Append(((bool)value).DebugString());
                    break;
                case "Byte":
                    sb.Append(((byte)value).DebugString());
                    break;
                case "Char":
                    sb.Append(((char)value).DebugString());
                    break;
                case "DateTime":
                    sb.Append(((DateTime)value).DebugString());
                    break;
                case "Decimal":
                    sb.Append(((decimal)value).DebugString());
                    break;
                case "Double":
                    sb.Append(((double)value).DebugString());
                    break;
                case "Int16":
                    sb.Append(((short)value).DebugString());
                    break;
                case "Int32":
                    sb.Append(((int)value).DebugString());
                    break;
                case "Int64":
                    sb.Append(((long)value).DebugString());
                    break;
                case "SByte":
                    sb.Append(((sbyte)value).DebugString());
                    break;
                case "Single":
                    sb.Append(((float)value).DebugString());
                    break;
                case "String":
                    sb.Append(((string)value).DebugString());
                    break;
                case "UInt16":
                    sb.Append(((ushort)value).DebugString());
                    break;
                case "UInt32":
                    sb.Append(((uint)value).DebugString());
                    break;
                case "UInt64":
                    sb.Append(((ulong)value).DebugString());
                    break;
                default:
                    sb.Append(value);
                    break;
            }
        }

        private static string ClrAndCSharpNameFor(string name)
        {
            switch (name)
            {
                case "Boolean":
                    return name + "/bool";
                case "Byte":
                    return name + "/byte";
                case "Char":
                    return name + "/char";
                case "Decimal":
                    return name + "/decimal";
                case "Double":
                    return name + "/double";
                case "Int16":
                    return name + "/short";
                case "Int32":
                    return name + "/int";
                case "Int64":
                    return name + "/long";
                case "SByte":
                    return name + "/sbyte";
                case "Single":
                    return name + "/float";
                case "UInt16":
                    return name + "/ushort";
                case "UInt32":
                    return name + "/uint";
                case "UInt64":
                    return name + "/ulong";
                default:
                    return name;
            }
        }

        public static string DebugString(this bool value)
        {
            return value ? BooleanTrueRepresentation : BooleanFalseRepresentation;
        }

        public static string DebugString(this byte value)
        {
            return value.ToString(NumericIntegerFormat);
        }

        public static string DebugString(this char value)
        {
            return char.IsControl(value)
                ? string.Format("\\u{0:x4}", (int)value)
                : string.Format("'{0}'", value);
        }

        public static string DebugString(this DateTime value)
        {
            bool datePresent = value.DatePresent();
            bool timePresent = value.TimePresent();
            return datePresent == timePresent
                ? value.DebugString(DateTimeContent.DateAndTime)
                : value.DebugString(datePresent ? DateTimeContent.DateOnly : DateTimeContent.TimeOnly);
        }

        public static string DebugString(this DateTime value, DateTimeContent content)
        {
            switch (content)
            {
                case DateTimeContent.DateOnly:
                    return value.ToString(DateFormat);
                case DateTimeContent.TimeOnly:
                    return value.ToString(TimeFormat);
                default:
                    return value.ToString(DateAndTimeFormat);
            }
        }

        public static bool DatePresent(this DateTime value)
        {
            return (value.Ticks >= DateTimeTicksPerDay);
        }

        public static bool TimePresent(this DateTime value)
        {
            return ((value.Ticks % DateTimeTicksPerDay) > 0);
        }

        public static string DebugString(this decimal value)
        {
            return value.ToString(Numeric2DecimalsFormat);
        }

        public static string DebugString(this double value)
        {
            return value.ToString(Numeric4DecimalsFormat);
        }

        public static string DebugString(this float value)
        {
            return value.ToString(Numeric2DecimalsFormat);
        }

        public static string DebugString(this short value)
        {
            return value.ToString(NumericIntegerFormat);
        }

        public static string DebugString(this int value)
        {
            return value.ToString(NumericIntegerFormat);
        }

        public static string DebugString(this long value)
        {
            return value.ToString(NumericIntegerFormat);
        }

        public static string DebugString(this sbyte value)
        {
            return value.ToString(NumericIntegerFormat);
        }

        public static string DebugString(this string value)
        {
            if (null == value)
            {
                return "<null>";
            }
            if (string.Empty == value)
            {
                return "<empty>";
            }
            StringBuilder sb = new StringBuilder();
            sb.Append('"');
            foreach (char c in value)
            {
                switch (c)
                {
                    case '\0':
                        sb.Append(@"\0");
                        break;
                    case '\a':
                        sb.Append(@"\a");
                        break;
                    case '\b':
                        sb.Append(@"\b");
                        break;
                    case '\f':
                        sb.Append(@"\f");
                        break;
                    case '\n':
                        sb.Append(@"\n");
                        break;
                    case '\r':
                        sb.Append(@"\r");
                        break;
                    case '\t':
                        sb.Append(@"\t");
                        break;
                    case '\v':
                        sb.Append(@"\v");
                        break;
                    case '"':
                        sb.Append(@"\""");
                        break;
                    case '\\':
                        sb.Append(@"\\");
                        break;
                    default:
                        if (char.IsControl(c))
                        {
                            sb.Append(@"\u");
                            int charVal = c;
                            sb.Append(charVal.ToString("x4"));
                        }
                        else
                        {
                            sb.Append(c);
                        }
                        break;
                }
            }
            sb.Append('"');
            return sb.ToString();
        }

        public static string DebugString(this ushort value)
        {
            return value.ToString(NumericIntegerFormat);
        }

        public static string DebugString(this uint value)
        {
            return value.ToString(NumericIntegerFormat);
        }

        public static string DebugString(this ulong value)
        {
            return value.ToString(NumericIntegerFormat);
        }
    }
}
