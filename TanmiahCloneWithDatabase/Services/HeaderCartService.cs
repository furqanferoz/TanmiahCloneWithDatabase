using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using TanmiahCloneWithDatabase.Models;

using SqlItmes;
namespace TanmiahCloneWithDatabase.Services
{
    public class HeaderCartService
    {
        GetHeader getHeaderClass = new GetHeader();
        HeaderCartEditModel headerCartEditModel = new HeaderCartEditModel();
        DataTable dataTable;

        public HeaderCartEditModel FillData(int id)
        {
            dataTable = new DataTable();
            dataTable = getHeaderClass.GetHeaderData(id);
            if (dataTable.Rows.Count == 1)
            {
                headerCartEditModel.ID = Convert.ToInt32(dataTable.Rows[0][0]);
                headerCartEditModel.Tile = dataTable.Rows[0][1].ToString();
                headerCartEditModel.Name = dataTable.Rows[0][2].ToString();
                headerCartEditModel.Description = dataTable.Rows[0][3].ToString();
                headerCartEditModel.Image = dataTable.Rows[0][4].ToString();
                return headerCartEditModel;
            }
            return headerCartEditModel;
        }
    }


    public class GetHeader
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;
        SqlDataReader sqlDataReader;
        DataTable dataTable;
        public DataTable GetHeaderData(int ID)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterBannerDetail", sqlConnection);
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


    public class UpdateHeader
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;

        public SqlCommand UpdateHeaderData(HeaderCartEditModel headerCartEditModel, string type)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterBannerDetail", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", type);
                sqlCommand.Parameters.AddWithValue("@ID", headerCartEditModel.ID);
                sqlCommand.Parameters.AddWithValue("@BannerTitle", headerCartEditModel.Tile);
                sqlCommand.Parameters.AddWithValue("@BannerName", headerCartEditModel.Name);
                sqlCommand.Parameters.AddWithValue("@BannerDesc", headerCartEditModel.Description);
                sqlCommand.Parameters.AddWithValue("@BannerImage", headerCartEditModel.Image);
                sqlCommand.ExecuteNonQuery();
                return sqlCommand;
            }
        }
    }

    public class CreateHeader
    {
        SqlCommand sqlCommand;
        SqlConnection sqlConnection;

        public SqlCommand CreateHeaderData(HeaderCartEditModel headerCartEditModel)
        {
            using (sqlConnection = new SqlConnection(SqlConn.ConnectionString))
            {
                sqlConnection.Open();
                sqlCommand = new SqlCommand("SpMasterBannerDetail", sqlConnection);
                sqlCommand.CommandType = CommandType.StoredProcedure;
                sqlCommand.Parameters.AddWithValue("@StatementType", "Insert");
                sqlCommand.Parameters.AddWithValue("@BannerTitle", headerCartEditModel.Tile);
                sqlCommand.Parameters.AddWithValue("@BannerName", headerCartEditModel.Name);
                sqlCommand.Parameters.AddWithValue("@BannerDesc", headerCartEditModel.Description);
                sqlCommand.Parameters.AddWithValue("@BannerImage", headerCartEditModel.Image);
                sqlCommand.Parameters.AddWithValue("@IsActive", "1");
                sqlCommand.ExecuteNonQuery();
            }
            return sqlCommand;
        }
    }
}