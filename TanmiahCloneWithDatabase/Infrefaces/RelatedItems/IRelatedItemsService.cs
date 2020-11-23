using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IRelatedItemsService
    {
        RelatedItemsEditModel FillData(int id);
    }
}