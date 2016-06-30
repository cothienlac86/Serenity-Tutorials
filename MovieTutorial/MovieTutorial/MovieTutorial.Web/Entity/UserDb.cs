using MovieTutorial.Generate;
using MovieTutorial.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MovieTutorial.Entity
{
    public class UserDb
    {
        public UserDb()
        {
            UserId = 0;
            Password = String.Empty;
            FullName = String.Empty;
            PhoneNumber = String.Empty;
            Email = String.Empty;
            Address = String.Empty;
            Permission = String.Empty;
            Status = String.Empty;
        }

        #region Propeties
        public int UserId { get; set; }
        public string Password { get; set; }
        public string FullName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
        public string Permission { get; set; }
        public string Status { get; set; }
        #endregion

        public LoginUserModel Login(LoginUserModel model)
        {
            var _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InfoWebAppDbStr"].ConnectionString);
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            //Create command store procedure
            var command = new SqlCommand("LoginUser");
            command.Connection = _conn;
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                var PhoneNumberParam = new SqlParameter("@PhoneNumber", model.PhoneNumber);
                PhoneNumberParam.Direction = ParameterDirection.Input;
                command.Parameters.Add(PhoneNumberParam);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        if (model.Password == Crypto.DecryptStringAES(reader.GetString(1)))
                        {
                            model.UserId = reader.GetInt32(0);
                            model.FullName = reader.GetString(2);
                            model.Permission = reader.GetString(6);
                            model.Status = reader.GetString(7);
                        }
                        break;
                    }
                    reader.Close();
                }
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return model;
        }

        public void ChangePassword(ChangePasswordModel model)
        {
            var _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InfoWebAppDbStr"].ConnectionString);
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            //Create command store procedure
            var command = new SqlCommand("ChangePassword");
            command.Connection = _conn;
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                var PasswordParam = new SqlParameter("@Password", Crypto.EncryptStringAES(model.NewPassword));
                PasswordParam.Direction = ParameterDirection.Input;
                var PhoneNumberParam = new SqlParameter("@PhoneNumber", model.PhoneNumber);
                command.Parameters.Add(PasswordParam);
                command.Parameters.Add(PhoneNumberParam);
                command.ExecuteNonQuery();
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
        }

        public bool CheckPhoneNumberExist(String PhoneNumber)
        {
            var status = false;
            var _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InfoWebAppDbStr"].ConnectionString);
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            //Create command store procedure
            var command = new SqlCommand("CheckPhoneNumber");
            command.Connection = _conn;
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                var IdParam = new SqlParameter("@PhoneNumber", PhoneNumber);
                IdParam.Direction = ParameterDirection.Input;
                command.Parameters.Add(IdParam);
                SqlDataReader reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    status = true;
                    reader.Close();
                }
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return status;
        }

        public int Register(RegisterUserModel model)
        {
            var _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InfoWebAppDbStr"].ConnectionString);
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            //Create command store procedure
            var command = new SqlCommand("Add_tblUser_Register");
            command.Connection = _conn;
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                var PasswordParam = new SqlParameter("@Password", Crypto.EncryptStringAES(model.Password));
                PasswordParam.Direction = ParameterDirection.Input;
                var FullNameParam = new SqlParameter("@FullName", model.FullName);
                FullNameParam.Direction = ParameterDirection.Input;
                var PhoneNumberParam = new SqlParameter("@PhoneNumber", model.PhoneNumber);
                PhoneNumberParam.Direction = ParameterDirection.Input;
                var EmailParam = new SqlParameter("@Email", model.Email);
                EmailParam.Direction = ParameterDirection.Input;
                var AddressParam = new SqlParameter("@Address", model.Address);
                AddressParam.Direction = ParameterDirection.Input;

                command.Parameters.Add(PasswordParam);
                command.Parameters.Add(FullNameParam);
                command.Parameters.Add(PhoneNumberParam);
                command.Parameters.Add(EmailParam);
                command.Parameters.Add(AddressParam);
                model.Id = int.Parse(command.ExecuteScalar().ToString());
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return model.Id;
        }

        public InfoUserModel GetUserInfo(String PhoneNumber)
        {
            var model = new InfoUserModel();
            var _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InfoWebAppDbStr"].ConnectionString);
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            string query = string.Format(@"SELECT
	                        U.*,
	                        WM.IsConfirmed,
	                        ro.RoleName FROM Users U
                        Left JOIN webpages_Membership WM on U.Id = WM.UserId
                        Left JOIN webpages_UsersInRoles WU on U.Id = WU.UserId
                        Left JOIN webpages_Roles ro on WU.RoleId = ro.RoleId
                        Where U.UserName = '{0}'",PhoneNumber);
            //Create command store procedure
            var command = new SqlCommand(query, _conn);
            command.Connection = _conn;
            try
            {
                var reader = command.ExecuteReader();
                if (reader.HasRows)
                {
                    while (reader.Read())
                    {
                        model.Id = reader.GetInt32(0);
                        model.UserName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                        model.FullName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                        model.Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                        model.Address = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    }
                    reader.Close();
                }
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
            return model;
        }

        public void UpdateInfo(InfoUserModel model)
        {
            var _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InfoWebAppDbStr"].ConnectionString);
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            //Create command store procedure
            var command = new SqlCommand("Update_UserInfo");
            command.Connection = _conn;
            command.CommandType = CommandType.StoredProcedure;
            try
            {
                var FullNameParam = new SqlParameter("@FullName", model.FullName);
                FullNameParam.Direction = ParameterDirection.Input;
                var EmailParam = new SqlParameter("@Email", model.Email);
                EmailParam.Direction = ParameterDirection.Input;
                var AddressParam = new SqlParameter("@Address", model.Address);
                AddressParam.Direction = ParameterDirection.Input;
                var PhoneNumberParam = new SqlParameter("@PhoneNumer", model.UserName);
                PhoneNumberParam.Direction = ParameterDirection.Input;
                command.Parameters.Add(FullNameParam);
                command.Parameters.Add(EmailParam);
                command.Parameters.Add(AddressParam);
                command.Parameters.Add(PhoneNumberParam);
                command.ExecuteScalar();
            }
            finally
            {
                command.Connection.Close();
                command.Connection.Dispose();
            }
        }

        public List<UserModels> GetListUser2ndRole()
        {
            var lstUser = new List<UserModels>();

            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
            string query = @"SELECT
	                        U.*,
	                        WM.IsConfirmed,
	                        ro.RoleName FROM Users U
                        Left JOIN webpages_Membership WM on U.Id = WM.UserId
                        Left JOIN webpages_UsersInRoles WU on U.Id = WU.UserId
                        Left JOIN webpages_Roles ro on WU.RoleId = ro.RoleId";
            var cmd = new SqlCommand(query, con);
            try
            {
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    var model = new UserModels();
                    model.UserId = reader.GetInt32(0);
                    model.UserName = reader.IsDBNull(1) ? string.Empty : reader.GetString(1);
                    model.FullName = reader.IsDBNull(2) ? string.Empty : reader.GetString(2);
                    model.Email = reader.IsDBNull(3) ? string.Empty : reader.GetString(3);
                    model.Address = reader.IsDBNull(4) ? string.Empty : reader.GetString(4);
                    model.isActived = reader.GetBoolean(5);
                    model.RoleName = reader.IsDBNull(6) ? string.Empty : reader.GetString(6);
                    lstUser.Add(model);
                }
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }

            return lstUser;
        }

        public void ConfirmAccount(int userId)
        {
            var con = new SqlConnection(ConfigurationManager.ConnectionStrings["DBConnection"].ToString());
            if (con.State == System.Data.ConnectionState.Closed) con.Open();
            string query = string.Format(@"UPDATE webpages_Membership SET IsConfirmed = 1 WHERE UserId = {0}", userId);
            var cmd = new SqlCommand(query, con);
            try
            {
                cmd.ExecuteNonQuery();
            }
            catch
            {
                throw;
            }
            finally
            {
                con.Close();
                cmd.Dispose();
            }
        }
    }
}