namespace Mapper
{
    /// <summary>
    /// Interface for mapping
    /// </summary>
    public interface IMapper
    {
        TDestination Map<TSource, TDestination>(TSource dalObject);
    }
}
