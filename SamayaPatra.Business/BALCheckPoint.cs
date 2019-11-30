using SamayaPatra.Common;
using SamayaPatra.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SamayaPatra.Business
{
    public class BALCheckPoint
    {
        readonly RepoDAO _dao;
        public BALCheckPoint()
        {
            _dao = new RepoDAO();
        }
        public async Task<DataTable> GetCheckPoint(int? checkPointId = null)
        {
            try
            {
                return await _dao.GetDataTableAsync("[usp_GetCheckPoints]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@CheckPointID", checkPointId);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveOrUpdateCheckPoint(CheckPoint checkPoint)
        {
            try
            {
                await _dao.ExecuteNonQueryAsync("[usp_SaveOrUpdateCheckPoints]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@CheckPointID", checkPoint.CheckPointID);
                    args.Add("@CheckPointName", checkPoint.CheckPointName);
                    args.Add("@CreatedBy", checkPoint.CreatedBy);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateCheckPointStatus(CheckPoint checkPoint)
        {
            try
            {
                await _dao.ExecuteNonQueryAsync("[usp_UpdateCheckPointsStatus]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@CheckPointID", checkPoint.CheckPointID);
                    args.Add("@DeletedBy", checkPoint.CreatedBy);
                    args.Add("@IsActive", checkPoint.IsActive);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
