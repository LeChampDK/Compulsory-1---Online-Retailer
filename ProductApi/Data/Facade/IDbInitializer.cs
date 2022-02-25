namespace ProductApi.Data.Facade
{
    public interface IDbInitializer
    {
        void Initialize(ProductApiContext context);
    }
}
