using System.Data;

namespace TanmiahCloneWithDatabase.Services
{
    public interface IGetIngredient
    {
        DataTable GetIngredientData(int ID);
    }
}