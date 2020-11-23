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
    public class CartService : ICartService
    {
      
        CartEditModel cartEditModel = new CartEditModel();
        DataTable dataTable;
        private IGetCart _getCart;

        public CartService(IGetCart getCart)
        {
            this._getCart = getCart;
        }

        public CartEditModel FillData(int id)
        {
            dataTable = new DataTable();
            dataTable = _getCart.GetCartData(id);
            if (dataTable.Rows.Count == 1)
            {
                cartEditModel.ID = Convert.ToInt32(dataTable.Rows[0][0]);
                cartEditModel.Title = dataTable.Rows[0][1].ToString();
                cartEditModel.Description = dataTable.Rows[0][2].ToString();
                cartEditModel.Image = dataTable.Rows[0][3].ToString();
                return cartEditModel;
            }
            return cartEditModel;
        }
    }

    
    public class GetCart : IGetCart
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;
        SqlDataReader sqlDataReader;
        DataTable dataTable;
        public DataTable GetCartData(int ID)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterCart", sqlConnection);
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


    public class CreateCart : ICreateCart
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;

        public SqlCommand CreateCartData(CartEditModel cartEditModel)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterCart", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", "Insert");
                sqlCommand.Parameters.AddWithValue("@CartTitle", cartEditModel.Title);
                sqlCommand.Parameters.AddWithValue("@CartDesc", cartEditModel.Description);
                sqlCommand.Parameters.AddWithValue("@CartImage", cartEditModel.Image);
                sqlCommand.ExecuteNonQuery();
            }
            return sqlCommand;
        }
    }

    public class UpdateCart : IUpdateCart
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;

        public SqlCommand UpdateCartData(CartEditModel CartEditModel, string type)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterCart", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", type);
                sqlCommand.Parameters.AddWithValue("@ID", CartEditModel.ID);
                sqlCommand.Parameters.AddWithValue("@CartTitle", CartEditModel.Title);
                sqlCommand.Parameters.AddWithValue("@CartDesc", CartEditModel.Description);
                sqlCommand.Parameters.AddWithValue("@CartImage", CartEditModel.Image);
                sqlCommand.ExecuteNonQuery();
                return sqlCommand;
            }
        }
    }
}