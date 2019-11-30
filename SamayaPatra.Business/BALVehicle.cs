using SamayaPatra.Common;
using SamayaPatra.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SamayaPatra.Business
{
    public class BALVehicle
    {
        readonly RepoDAO _dao;
        public BALVehicle()
        {
            _dao = new RepoDAO();
        }
        public async Task<DataTable> GetVehiclesAsync(int? vehicleId = null)
        {
            try
            {
                return await _dao.GetDataTableAsync("usp_GetVehicles", CommandType.StoredProcedure, args =>
                {
                    args.Add("@VehicleID", vehicleId);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveOrUpdateVehicle(Vehicle vehicle)
        {
            try
            {
                await _dao.ExecuteNonQueryAsync("usp_SaveOrUpdateVehicle", CommandType.StoredProcedure, args =>
                {
                    args.Add("@VehicleID", vehicle.VehicleID);
                    args.Add("@VehicleNo", vehicle.VehicleNo);
                    args.Add("@ModelNo", vehicle.ModelNo);
                    args.Add("@Color", vehicle.Color);
                    args.Add("@PassengerCapacity", vehicle.PassengerCapacity);
                    args.Add("@CubicCapacity", vehicle.CubicCapacity);
                    args.Add("@PurchasedDate", vehicle.PurchasedDate);
                    args.Add("@OwnerName", vehicle.OwnerName);
                    args.Add("@OwnerContact", vehicle.OwnerContact);
                    args.Add("@OwnerAddress", vehicle.OwnerAddress);
                    args.Add("@DriverName", vehicle.DriverName);
                    args.Add("@DriverContact", vehicle.DriverContact);
                    args.Add("@DriverAdddress", vehicle.DriverAdddress);
                    args.Add("@CreatedBy", vehicle.CreatedBy);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateVehicleStatus(Vehicle vehicle)
        {
            try
            {
                await _dao.ExecuteNonQueryAsync("usp_UpdateVehicleStatus", CommandType.StoredProcedure, args =>
                {
                    args.Add("@VehicleID", vehicle.VehicleID);
                    args.Add("@DeletedBy", vehicle.CreatedBy);
                    args.Add("@IsActive", vehicle.IsActive);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
