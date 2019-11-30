using SamayaPatra.Common;
using SamayaPatra.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SamayaPatra.Business
{
    public class BALRoute
    {
        readonly RepoDAO _dao;
        public BALRoute()
        {
            _dao = new RepoDAO();
        }
        public async Task<DataTable> GetRoutesAsync(int? routeId = null)
        {
            try
            {
                return await _dao.GetDataTableAsync("[usp_GetRoutes]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@RouteID", routeId);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataSet> GetRouteDetailsAsync(int routeId)
        {
            try
            {
                return await _dao.GetDataSetAsync("[usp_GetRouteDetails]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@RouteID", routeId);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveOrUpdateRouteAsync(Route route)
        {
            try
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("CheckPointID", typeof(int));
                dt.Columns.Add("EstimatedKM", typeof(decimal));
                dt.Columns.Add("EstimatedArrivalMinutes", typeof(int));
                foreach(var checkPoint in route.CheckPoints)
                {
                    var dr = dt.NewRow();
                    dr["CheckPointID"] = checkPoint.CheckPointID;
                    dr["EstimatedKM"] = checkPoint.EstimatedKM;
                    dr["EstimatedArrivalMinutes"] = checkPoint.EstimatedArrivalMinutes;
                    dt.Rows.Add(dr);
                }
                await _dao.ExecuteNonQueryAsync("[usp_SaveOrUpdateRoute]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@RouteID", route.RouteID);
                    args.Add("@RouteName", route.RouteName);
                    args.Add("@TotalKM", route.TotalKM);
                    args.Add("@CreatedBy", route.CreatedBy);
                    args.Add("@CheckPoints", dt);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateRouteStatus(Route route)
        {
            try
            {
                await _dao.ExecuteNonQueryAsync("[usp_UpdateRouteStatus]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@RouteID", route.RouteID);
                    args.Add("@DeletedBy", route.CreatedBy);
                    args.Add("@IsActive", route.IsActive);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
