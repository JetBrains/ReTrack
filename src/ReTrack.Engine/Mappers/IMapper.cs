namespace ReTrack.Engine.Mappers
{
    public interface IMapper<TFrom, TTo>
    {
        TTo Map(TFrom origin);
    }
}