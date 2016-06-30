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
    public class SpammersEntity
    {
        public int Id { get; set; }
        public string PhoneNo { get; set; }
        public string Name { get; set; }

        public SpammersEntity()
        {
            Id = 0;
            PhoneNo = string.Empty;
            Name = string.Empty;
        }

        public List<SpammersModel> CheckSpammerByPhone(string phoneNo)
        {
            List<SpammersModel> lstSpams = new List<SpammersModel>();
            var query = string.Format("SELECT * FROM tbl_SpamPhone WHERE spam_phone = '{0}'", phoneNo);
            var _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InfoWebAppDbStr"].ConnectionString);
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            //Create command store procedure
            var command = new SqlCommand(query, _conn);
            try
            {
                var reader = command.ExecuteReader();
                while (reader.Read())
                {
                    var model = new SpammersModel();
                    model.Id = reader.GetInt32(1);
                    model.PhoneNo = reader.GetValue(2) as string;
                    model.Name = reader.GetValue(3) as string;
                    lstSpams.Add(model);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return lstSpams;
        }

        public bool IsSpammer(string phoneNo)
        {
            var query = string.Format("SELECT COUNT(*) AS ReturnCount FROM tbl_SpamPhone WHERE spam_phone = '{0}'", phoneNo);
            var _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InfoWebAppDbStr"].ConnectionString);
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            //Create command store procedure
            var command = new SqlCommand(query, _conn);
            try
            {
                var count = (int)command.ExecuteScalar();
                return (count > 0);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }

        public void AddSpammer(SpammersModel model)
        {
            var _conn = new SqlConnection(ConfigurationManager.ConnectionStrings["InfoWebAppDbStr"].ConnectionString);
            if (_conn.State == ConnectionState.Closed)
                _conn.Open();
            var command = new SqlCommand();
            try
            {
                command.CommandText = "Add_tblSpamPhone";
                command.Connection = _conn;
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@SpamPhone", model.PhoneNo).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@SpamName", model.Name).Direction = ParameterDirection.Input;
                command.Parameters.AddWithValue("@Status", model.Status).Direction = ParameterDirection.Input;
                command.Prepare();
                command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                _conn.Close();
            }
        }
    }
}