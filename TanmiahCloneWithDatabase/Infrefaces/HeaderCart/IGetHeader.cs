using System.Data;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IGetHeader
    {
        DataTable GetHeaderData(int ID);
    }
}