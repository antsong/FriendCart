namespace MZBlog.Core
{
    public interface IViewProjection<tIn, tOut>
    {
        tOut Project(tIn input);
    }

    public interface IViewProjectionFactory
    {
        TOut Get<TIn, TOut>(TIn input);
    }
}