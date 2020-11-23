using System.Data;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IGetRelatedItems
    {
        DataTable GetRelatedItemsData();
        DataTable GetRelatedItemsData(int id);
    }
}