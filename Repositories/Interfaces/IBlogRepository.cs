namespace Blog.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        Task<Models.Blog> FindByIdAsync(int id);

        Task<IEnumerable<Models.Blog>> FindAllAsync();

        Task CreateBlogAsync(Models.Blog blog);

        Task UpdateBlogAsync(int id, Models.Blog blog);
    }
}
