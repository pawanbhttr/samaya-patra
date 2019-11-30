using SamayaPatra.Common;
using SamayaPatra.Repository;
using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Threading.Tasks;

namespace SamayaPatra.Business
{
    public class BALUser
    {
        readonly RepoDAO _dao;
        public BALUser()
        {
            _dao = new RepoDAO();
        }
        public async Task<DataTable> AuthenticateUserAsync(User user)
        {
            try
            {
                return await _dao.GetDataTableAsync("[usp_AuthenticateUser]", CommandType.StoredProcedure, args =>
                    {
                        args.Add("UserName", user.UserName);
                        args.Add("Password", user.Password);
                    });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataTable> GetUserRoleAsync(int? userRoleId = null)
        {
            try
            {
                return await _dao.GetDataTableAsync("[usp_GetUserRoles]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@UserRoleID", userRoleId);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<DataTable> GetUserAsync(int? userId = null)
        {
            try
            {
                return await _dao.GetDataTableAsync("[usp_GetUsers]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@UserID", userId);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task SaveOrUpdateUserAsync(User user)
        {
            try
            {
                await _dao.ExecuteNonQueryAsync("[usp_SaveOrUpdateUser]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@UserID", user.UserID);
                    args.Add("@UserName", user.UserName);
                    args.Add("@Password", user.Password);
                    args.Add("@FullName", user.FullName);
                    args.Add("@UserRoleID", user.UserRoleID);
                    args.Add("@Contact", user.Contact);
                    args.Add("@Email", user.Email);
                    args.Add("@Address", user.Address);
                    args.Add("@VehicleID", user.VehicleID);
                    args.Add("@CreatedBy", user.CreatedBy);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task UpdateUserStatusAsync(User user)
        {
            try
            {
                await _dao.ExecuteNonQueryAsync("[usp_UpdateUserStatus]", CommandType.StoredProcedure, args =>
                {
                    args.Add("@UserID", user.UserID);
                    args.Add("@DeletedBy", user.CreatedBy);
                    args.Add("@IsActive", user.IsActive);
                });
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
