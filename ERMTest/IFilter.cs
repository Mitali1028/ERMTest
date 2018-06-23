namespace CSVTesting
{
    public interface IFilter<T>
    {
      T Filter(T data);
    }
}
