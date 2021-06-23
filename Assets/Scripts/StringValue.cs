using System;
using System.Reflection;

namespace Utility
{
    public class StringValue : System.Attribute 
    { 

        private string _value; 

        public StringValue(string value) 
        { 
            _value = value; 
        } 

        public string Value 
        { 
            get {return _value;}
        } 

        public static string GetStringValue<EnumT>(EnumT value)
        {
            string output = null;
            
            Type type = value.GetType();
            
            FieldInfo fi = type.GetField(value.ToString());
            StringValue[] attrs = fi.GetCustomAttributes(typeof(StringValue), false) as StringValue[];
            
            if(attrs.Length > 0)
            {
                output = attrs[0].Value;
            }
                
            return output;
        }

        public static T GetEnumValue<T>(string value) where T : Enum
        {
            Type type = value.GetType();

            T[] TArr = Enum.GetValues(typeof(T)) as T[];

            foreach (T t in TArr)
            {
                FieldInfo Tfi = type.GetField(t.ToString());
                StringValue attr = (Tfi.GetCustomAttributes(typeof(StringValue), false) as StringValue[])[0];
                
                if (attr.Value == value)
                {
                    return t;
                }
            }

            
            return TArr[0];
        }
    }
}