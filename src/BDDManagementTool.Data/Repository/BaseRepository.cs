namespace Bohrium.Tools.BDDManagementTool.Data.Repository
{
    public abstract class BaseRepository
    {
        protected IDataContext DataContext { get; set; }

        protected BaseRepository(IDataContext dataContext)
        {
            this.DataContext = dataContext;
        }
    }
}