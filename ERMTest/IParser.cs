namespace CSVTesting
{
    public interface IParser<T>
    {
        T ParseFile(string filePath);
    }
}
