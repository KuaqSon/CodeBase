namespace CodeBase.Infrastructure.Data
{
    public interface IDbFactory
    {
        ApplicationDbContext Init();
    }
}
