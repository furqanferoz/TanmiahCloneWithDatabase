using System.Data;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IGetBreadCrumb
    {
        DataTable GetBreadCrumbData(int ID);
    }
}