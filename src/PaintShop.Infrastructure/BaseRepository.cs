namespace PaintShop.Infrastructure
{
    public abstract class BaseRepository
    {
        protected AppDbContext context;

        protected BaseRepository(AppDbContext context)
        {
            this.context = context;
        }
    } 
}