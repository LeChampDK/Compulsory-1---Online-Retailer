namespace OrderApi.Data.Facade
{
    public interface IDbInitializer
    {
        void Initialize(OrderApiContext context);
    }
}
