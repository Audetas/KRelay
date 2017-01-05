using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace KRelay.Scripting
{
    static class ScriptFFI
    {
        public static Func<string, string> CreateInterface(string type, string method)
        {
            Type rType = Type.GetType(type);
            if (rType == null)
                throw new Exception("Type '" + type + "' cannot be found");

            // Attempt to bind to a method, property, or field
            MethodInfo rMethod = rType.GetMethod(method, new[] { typeof(string) });
            if (rMethod == null)
                rMethod = rType.GetMethod(method, new[] { typeof(int) });
            if (rMethod == null)
                rMethod = rType.GetMethod(method);
            if (rMethod != null)
                return (s) => { return SafeReturn(rMethod.Invoke(null, EnumerateValues(s, rMethod))); };

            PropertyInfo rProperty = rType.GetProperty(method);
            if (rProperty != null)
                return (s) => { rProperty.SetValue(null, ConvertValue(s, rProperty.PropertyType)); return null; };

            FieldInfo rField = rType.GetField(method);
            if (rField != null)
                return (s) => { rField.SetValue(null, ConvertValue(s, rField.FieldType)); return null; };

            throw new InvalidOperationException("No fitting method, field, or property was found");
        }

        private static object[] EnumerateValues(string operand, MethodInfo method)
        {
            ParameterInfo[] rParameters = method.GetParameters();

            if (rParameters.Length == 0)
                return null;

            object[] parameters = new object[rParameters.Length];
            string[] splits = operand.Split(", "); // This needs to be fixed to allow literal commas

            for (int i = 0; i < rParameters.Length; i++)
                parameters[i] = ConvertValue(splits[i], rParameters[i].ParameterType);

            return parameters;
        }

        private static object ConvertValue(string value, Type type)
        {
            // No need to convert strings
            if (type == typeof(string))
                return value;

            // If type supports a 'Parse(string)' method
            MethodInfo parser = type.GetMethod("Parse", new[] { typeof(string) });
            if (parser != null)
                return parser.Invoke(null, new[] { value });

            // Parse enum value if type is an enum
            if (type.IsEnum)
                return Enum.Parse(type, value);

            // Finish this later
            throw new InvalidOperationException(type.ToString() + " does cannot be implicitly derrived from a string");
        }

        private static string SafeReturn(object value)
        {
            // Ensures a string value is always returned
            if (value == null) return "NULL";
            if (value is string) return (string)value;
            return value.ToString();
        }
    }
}
