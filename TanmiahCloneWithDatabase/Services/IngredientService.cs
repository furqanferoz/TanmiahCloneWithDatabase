using SqlItmes;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TanmiahCloneWithDatabase.Models;

namespace TanmiahCloneWithDatabase.Services
{
    public class IngredientService
    {
        GetIngredient getIngredientClass = new GetIngredient();
        IngredientEditModel ingredientEditModel = new IngredientEditModel();
        DataTable dataTable;

        public IngredientEditModel FillData(int id)
        {
            dataTable = new DataTable();
            dataTable = getIngredientClass.GetIngredientData(id);
            if (dataTable.Rows.Count == 1)
            {
                ingredientEditModel.ID = Convert.ToInt32(dataTable.Rows[0][0].ToString());
                ingredientEditModel.Title = dataTable.Rows[0][1].ToString();
                ingredientEditModel.TitleOne = dataTable.Rows[0][2].ToString();
                ingredientEditModel.DescriptionOne = dataTable.Rows[0][3].ToString();
                ingredientEditModel.DescriptionSec = dataTable.Rows[0][4].ToString();
                ingredientEditModel.Protein = dataTable.Rows[0][5].ToString();
                ingredientEditModel.Fat = dataTable.Rows[0][7].ToString();
                ingredientEditModel.Carbohydrate = dataTable.Rows[0][6].ToString();
                ingredientEditModel.OrderTitle = dataTable.Rows[0][8].ToString();
                
                return ingredientEditModel;
            }
            return ingredientEditModel;
        }
    }
    public class UpdateIngredient
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;

        public SqlCommand UpdateIngredientData(IngredientEditModel ingredientEditModel, string type)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterIngredientsDetail", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", type);
                sqlCommand.Parameters.AddWithValue("@ID",ingredientEditModel.ID);
                sqlCommand.Parameters.AddWithValue("@IngredientTitle",ingredientEditModel.Title);
                sqlCommand.Parameters.AddWithValue("@IngredientTitleOne",ingredientEditModel.TitleOne);
                sqlCommand.Parameters.AddWithValue("@IngredientDesc",ingredientEditModel.DescriptionOne);
                sqlCommand.Parameters.AddWithValue("@IngredientDescSec",ingredientEditModel.DescriptionSec);
                sqlCommand.Parameters.AddWithValue("@IngredientProtein",ingredientEditModel.Protein);
                sqlCommand.Parameters.AddWithValue("@IngredientCarbohydrate",ingredientEditModel.Carbohydrate);
                sqlCommand.Parameters.AddWithValue("@IngredientFat",ingredientEditModel.Fat);
                sqlCommand.Parameters.AddWithValue("@IngredientOrderTitle",ingredientEditModel.OrderTitle);
                sqlCommand.Parameters.AddWithValue("@IsActive", "1");
                sqlCommand.ExecuteNonQuery();
                return sqlCommand;
            }
        }
    }


    public class CreateIngredient
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;

        public SqlCommand CreateIngredientData(IngredientEditModel ingredientEditModel)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterIngredientsDetail", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", "Insert");
                sqlCommand.Parameters.AddWithValue("@IngredientTitle", ingredientEditModel.Title);
                sqlCommand.Parameters.AddWithValue("@IngredientTitleOne", ingredientEditModel.TitleOne);
                sqlCommand.Parameters.AddWithValue("@IngredientDesc", ingredientEditModel.DescriptionOne);
                sqlCommand.Parameters.AddWithValue("@IngredientDescSec", ingredientEditModel.DescriptionSec);
                sqlCommand.Parameters.AddWithValue("@IngredientProtein", ingredientEditModel.Protein);
                sqlCommand.Parameters.AddWithValue("@IngredientCarbohydrate", ingredientEditModel.Carbohydrate);
                sqlCommand.Parameters.AddWithValue("@IngredientFat", ingredientEditModel.Fat);
                sqlCommand.Parameters.AddWithValue("@IngredientOrderTitle", ingredientEditModel.OrderTitle);
                sqlCommand.Parameters.AddWithValue("@IsActive", "1");
                sqlCommand.ExecuteNonQuery();
            }
            return sqlCommand;
        }
    }

    public class GetIngredient
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;
        SqlDataReader sqlDataReader;
        DataTable dataTable;
        public DataTable GetIngredientData(int ID)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterIngredientsDetail", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", "Select");
                sqlCommand.Parameters.AddWithValue("@ID", ID);
                sqlDataReader = sqlCommand.ExecuteReader();
                dataTable = new DataTable();
                dataTable.Load(sqlDataReader);
                return dataTable;
            }
        }
    }
}