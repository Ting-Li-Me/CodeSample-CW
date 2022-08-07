namespace BLL.Interfaces
{
    public interface IMapping<T1,T2> where T1: class where T2: class
    {
        T1 MapToEntity(T2 t);
        T2 MapToBO(T1 t);

    }
}
