namespace Base.Standard.Serializer
{
    public interface ISerializer
    {
        T DeserializeFromFile<T>(string fileName) where T : class;
        void SerializeToFile<T>(T data, string fileName) where T : class;
    }
}