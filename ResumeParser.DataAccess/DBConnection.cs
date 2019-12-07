using System;
using System.Data;
using System.Data.SqlClient;

namespace ResumeParser.DataAccess
{
    public class DBConnection
    {
        private SqlDataAdapter myAdapter;
        private SqlConnection conn;

        /// <constructor>
        /// Initialise Connection
        /// </constructor>
        public DBConnection()
        {
            myAdapter = new SqlDataAdapter();
            conn = new SqlConnection(@"Data Source=Harish-PC\SQLEXPRESS;Initial Catalog=Marbale;Trusted_Connection=True;");
        }

        /// <method>
        /// Open Database Connection if Closed or Broken
        /// </method>
        private SqlConnection openConnection()
        {
            if (conn.State == ConnectionState.Closed || conn.State == ConnectionState.Broken)
            {
                conn.Open();
            }
            return conn;
        }
        /// <summary>
        /// select data by stored procedure
        /// </summary>
        /// <param name="sp"></param>
        /// <returns></returns>
        public DataTable executeSelectQuery(String sp)
        {
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand(sp, conn))
                {
                    cmd.Connection = openConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                    myAdapter.SelectCommand = cmd;
                    myAdapter.Fill(ds);
                    dataTable = ds.Tables[0];
                    return dataTable;

                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        /// <method>
        /// Select with parameter
        /// </method>
        public DataTable executeSelectQuery(String sp, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            DataTable dataTable = new DataTable();
            dataTable = null;
            DataSet ds = new DataSet();
            try
            {
                using (SqlCommand cmd = new SqlCommand(sp, conn))
                {
                    cmd.Connection = openConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqlParameter);
                    cmd.ExecuteNonQuery();
                    myAdapter.SelectCommand = cmd;
                    myAdapter.Fill(ds);
                    dataTable = ds.Tables[0];
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
            return dataTable;
        }
        /// <method>
        /// Insert sp
        /// </method>
        public int executeInsertQuery(String sp, SqlParameter[] sqlParameter)
        {
            try
            {
                using (SqlCommand cmd = new SqlCommand(sp, conn))
                {
                    cmd.Connection = openConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqlParameter);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
        /// <method>
        /// insert/Update sp
        /// </method>
        public int executeUpdateQuery(String sp, SqlParameter[] sqlParameter)
        {
            SqlCommand myCommand = new SqlCommand();
            try
            {
                using (SqlCommand cmd = new SqlCommand(sp, conn))
                {
                    cmd.Connection = openConnection();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.AddRange(sqlParameter);
                    return cmd.ExecuteNonQuery();
                }
            }
            catch (SqlException e)
            {
                throw e;
            }
        }
    }
}
