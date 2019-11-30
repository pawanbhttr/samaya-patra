using SamayaPatra.Common;
using SamayaPatra.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SamayaPatra.Business
{
    public class BALTransaction
    {
        readonly RepoDAO _dao;
        public BALTransaction()
        {
            _dao = new RepoDAO();
        }

        public async Task<DataTable> SaveTripInfoAsync(Transaction transaction)
        {
            try
            {
                return await _dao.GetDataTableAsync("[usp_SaveTransactions]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@TrasactionID", transaction.TrasactionID);
                    args.Add("@VehicleID", transaction.VehicleID);
                    args.Add("@RouteID", transaction.RouteID);
                    args.Add("@CreatedBy", transaction.CreatedBy);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataTable> GetActiveTripAsync(int? userId = null)
        {
            try
            {
                return await _dao.GetDataTableAsync("[usp_GetActiveTransactions]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@UserID", userId);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task DeleteTripAsync(Transaction transaction)
        {
            try
            {
                await _dao.ExecuteNonQueryAsync("[usp_DeleteTransactions]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@TrasactionID", transaction.TrasactionID);
                    args.Add("@DeletedBy", transaction.CreatedBy);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataTable> GetReportDetails(ReportInfo reportInfo)
        {
            try
            {
                return await _dao.GetDataTableAsync("[usp_GetTransactionsReport]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@UserID", reportInfo.UserID);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
