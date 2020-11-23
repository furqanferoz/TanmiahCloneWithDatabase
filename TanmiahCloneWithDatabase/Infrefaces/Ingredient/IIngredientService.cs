using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IIngredientService
    {
        IngredientEditModel FillData(int id);
    }
}