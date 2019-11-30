using SamayaPatra.Common;
using SamayaPatra.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SamayaPatra.Business
{
    public class BALCommon
    {
        readonly RepoDAO _dao;
        public BALCommon()
        {
            _dao = new RepoDAO();
        }
        public async Task<DataTable> GetSelectOptions(SelectOption selectOption)
        {
            try
            {
                var query = string.Format(@"SELECT {0},{1} FROM {2}", selectOption.TextField, selectOption.ValueField, selectOption.TableName);
                if (!string.IsNullOrWhiteSpace(selectOption.Condition))
                {
                    query += $" WHERE { selectOption.Condition }";
                }
                if (!string.IsNullOrWhiteSpace(selectOption.OrderBy))
                {
                    query += $" ORDER BY { selectOption.OrderBy }";
                }
                return await _dao.GetDataTableAsync(query, CommandType.Text, args => { });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
