using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Base.Standard.Serializer
{
    public class XmlSerializer : ISerializer
    {
        public void SerializeToFile<T>(T data, string fileName) where T : class
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var writer = new StreamWriter(fileName))
            {
                serializer.Serialize(writer, data);
            }
        }

        public T DeserializeFromFile<T>(string fileName) where T : class
        {
            var serializer = new System.Xml.Serialization.XmlSerializer(typeof(T));
            using (var reader = new StreamReader(fileName))
            {
                var result = serializer.Deserialize(reader);
                return (T)result;
            }
        }
    }
}
