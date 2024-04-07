using static Trendy_BackEnd.Config.EnumCollection;
using System.Data.SqlClient;
using System.Dynamic;
using Trendy_BackEnd.Models;
using System.Data;
using Trendy_BackEnd.Config;

namespace Trendy_BackEnd.Services
{
    public class AuthServices
    {
        #region Sign In

        internal async Task<ResponseResult> SignIn(string Email, string Password)
        {
            SqlCommand command;
            ResponseResult r;
            r = new()
            {
                Status = ApiRespond.Fail.ToString(),
                Content = null,
                Message = "Didn't Connect to the SQL Connection"
            };
            try
            {
                ModelUser v = new ModelUser();
                using (SqlConnection con = new(ApiManager.Instance.GetConnectionString().ConnectionString))
                {
                    try
                    {
                        con.Open();
                        command = new()
                        {
                            Connection = con,
                            CommandType = CommandType.StoredProcedure,
                            CommandTimeout = 0,
                            CommandText = "sp_User_Login"
                        };
                        command.Parameters.AddWithValue("@Password", Password);
                        command.Parameters.AddWithValue("@Email", Email);
                        using (SqlDataReader dr = await command.ExecuteReaderAsync())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    v = new()
                                    {
                                        Id = Convert.ToInt32(dr["Id"]),
                                        FirstName = dr["FirstName"].ToString(),
                                        LastName = dr["LastName"].ToString(),
                                        Address1 = dr["Address1"].ToString(),
                                        Address2 = dr["Address2"].ToString(),
                                        ContactNumber = dr["ContactNumber"].ToString(),
                                        Email = dr["Email"].ToString()

                                    };

                                }

                                r.Status = ApiRespond.Success.ToString();
                                r.Content = v;
                                r.Message = "success";
                            }
                            else
                            {
                                r.Status = ApiRespond.Success.ToString();
                                r.Content = null;
                                r.Message = "Error! Incorrect Email or Password";
                            }
                        }

                        command.Dispose();
                    }
                    catch (Exception ex)
                    {
                        r.Status = ApiRespond.Fail.ToString();
                        r.Content = null;
                        r.Message = ex.Message;
                    }
                    finally
                    { con.Close(); }
                }
            }
            catch (Exception ex)
            {

                r.Status = ApiRespond.Fail.ToString();
                r.Content = null;
                r.Message = ex.Message;
            }

            return r;
        }

        #endregion

        #region Sign Up
        internal async Task<ResponseResult> SignUp(ModelUser account)
        {
            SqlCommand command = null;
            ResponseResult r;
            r = new()
            {
                Status = ApiRespond.Fail.ToString(),
                Content = null,
                Message = "Didn't Connect to the SQL Connection"
            };
            try
            {

                using (SqlConnection con = new(ApiManager.Instance.GetConnectionString().ConnectionString))
                {
                    try
                    {

                        con.Open();
                        command = new()
                        {
                            Connection = con,
                            CommandType = CommandType.StoredProcedure,
                            CommandTimeout = 0,
                            CommandText = "sp_User_save"
                        };
                        command.Parameters.AddWithValue("@FirstName", account.FirstName);
                        command.Parameters.AddWithValue("@LastName", account.LastName);
                        command.Parameters.AddWithValue("@Address1", account.Address1);
                        command.Parameters.AddWithValue("@Address2", account.Address2);
                        command.Parameters.AddWithValue("@ContactNumber", account.ContactNumber);
                        command.Parameters.AddWithValue("@Email", account.Email);
                        command.Parameters.AddWithValue("@Password", account.Password);
                        command.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                        int num = await command.ExecuteNonQueryAsync();
                        int count = 0;
                        count = Convert.ToInt32(command.Parameters["RETURN_VALUE"].Value);

                        if (count == 2)
                        {
                            r.Status = ApiRespond.Success.ToString();
                            r.Content = count;
                            r.Message = "success";
                        }
                        else if (count == 1)
                        {
                            r.Status = ApiRespond.Success.ToString();
                            r.Content = count;
                            r.Message = " User Already Exist to this Email";
                        }
                        else
                        {
                            r.Status = ApiRespond.Fail.ToString();
                            r.Content = count;
                            r.Message = "Error! Something Went Wrong, Sign Up Save Failed";
                            return r;
                        }
                        command.Dispose();
                    }
                    catch (Exception ex)
                    {
                        r.Status = ApiRespond.Fail.ToString();
                        r.Content = null;
                        r.Message = ex.Message;
                    }
                    finally
                    { con.Close(); }
                }

            }
            catch (Exception ex)
            {

                r.Status = ApiRespond.Fail.ToString();
                r.Content = null;
                r.Message = ex.Message;
            }
            return r;

        }
        #endregion

        #region Update Address2
        internal async Task<ResponseResult> UpdateAddress2( string Id, string Address2)
        {
            SqlCommand command = null;
            ResponseResult r;
            r = new()
            {
                Status = ApiRespond.Fail.ToString(),
                Content = null,
                Message = "Didn't Connect to the SQL Connection"
            };
            try
            {

                using (SqlConnection con = new(ApiManager.Instance.GetConnectionString().ConnectionString))
                {
                    try
                    {

                        con.Open();
                        command = new()
                        {
                            Connection = con,
                            CommandType = CommandType.StoredProcedure,
                            CommandTimeout = 0,
                            CommandText = "sp_User_Address_Update"
                        };
                        command.Parameters.AddWithValue("@Id", Id);
                        command.Parameters.AddWithValue("@Address2", Address2);
                        command.Parameters.Add("RETURN_VALUE", SqlDbType.Int).Direction = ParameterDirection.ReturnValue;

                        int num = await command.ExecuteNonQueryAsync();
                        int count = 0;
                        count = Convert.ToInt32(command.Parameters["RETURN_VALUE"].Value);

                        if (count == 2)
                        {
                            r.Status = ApiRespond.Success.ToString();
                            r.Content = count;
                            r.Message = "success";
                        }
                        else if (count == 1)
                        {
                            r.Status = ApiRespond.Success.ToString();
                            r.Content = count;
                            r.Message = " User Not Exist ";
                        }
                        else
                        {
                            r.Status = ApiRespond.Fail.ToString();
                            r.Content = count;
                            r.Message = "Error! Something Went Wrong, User Address Update Failed";
                            return r;
                        }
                        command.Dispose();
                    }
                    catch (Exception ex)
                    {
                        r.Status = ApiRespond.Fail.ToString();
                        r.Content = null;
                        r.Message = ex.Message;
                    }
                    finally
                    { con.Close(); }
                }

            }
            catch (Exception ex)
            {

                r.Status = ApiRespond.Fail.ToString();
                r.Content = null;
                r.Message = ex.Message;
            }
            return r;

        }
        #endregion

    }
}
