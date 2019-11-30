using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Threading.Tasks;

namespace SamayaPatra.Repository
{
    public class RepoDAO
    {
        readonly SqlConnection _connection;
        public RepoDAO()
        {
            _connection = new SqlConnection(GetConnectionString());
        }

        string GetConnectionString()
        {
            return "Server=DESKTOP-EHK8GNB;Database=SamayaPatra;Trusted_Connection=True;MultipleActiveResultSets=true";
        }

        async Task OpenConnectionAsync()
        {
            if (_connection.State == System.Data.ConnectionState.Open)
                _connection.Close();
            await _connection.OpenAsync();
        }

        void CloseConnection()
        {
            _connection.Close();
        }

        public async Task<DataTable> GetDataTableAsync(string query, CommandType commandType, Action<Dictionary<string, object>> args)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                args?.Invoke(parameters);
                var ds = await ExecuteReaderAsync(query, commandType, parameters);
                return ds.Tables[0];
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataSet> GetDataSetAsync(string query, CommandType commandType, Action<Dictionary<string, object>> args)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                args?.Invoke(parameters);
                var ds = await ExecuteReaderAsync(query, commandType, parameters);
                return ds;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task ExecuteNonQueryAsync(string query, CommandType commandType, Action<Dictionary<string, object>> args)
        {
            try
            {
                Dictionary<string, object> parameters = new Dictionary<string, object>();
                args?.Invoke(parameters);
                await ExecuteNonQueryAsync(query, commandType, parameters);
            }
            catch (Exception)
            {
                throw;
            }
        }

        async Task<DataSet> ExecuteReaderAsync(string query, CommandType commandType, Dictionary<string, object> parameters)
        {
            DataSet ds = new DataSet();
            try
            {
                await OpenConnectionAsync();
                SqlCommand cmd = new SqlCommand(query, _connection);
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
                cmd.CommandType = commandType;
                cmd.CommandTimeout = 600;
                using (var da = new SqlDataAdapter(cmd))
                    da.Fill(ds);
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
            return ds;
        }

        async Task ExecuteNonQueryAsync(string query, CommandType commandType, Dictionary<string, object> parameters)
        {
            try
            {
                await OpenConnectionAsync();
                SqlCommand cmd = new SqlCommand(query, _connection);
                foreach (var parameter in parameters)
                {
                    cmd.Parameters.AddWithValue(parameter.Key, parameter.Value);
                }
                cmd.CommandType = commandType;
                cmd.CommandTimeout = 120;
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                CloseConnection();
            }
        }
    }
}
