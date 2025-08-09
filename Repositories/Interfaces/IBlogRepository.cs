namespace Blog.Repositories.Interfaces
{
    public interface IBlogRepository
    {
        IEnumerable<Models.Blog> Blogs { get; }
        Task CreateBlogAsync(Models.Blog blog);
        Task UpdateBlogAsync(int id, Models.Blog blog);
    }
}
