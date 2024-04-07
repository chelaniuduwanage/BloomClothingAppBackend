using static Trendy_BackEnd.Config.EnumCollection;
using Trendy_BackEnd.Models;
using System.Data.SqlClient;
using System.Data;
using Trendy_BackEnd.Config;
using System.Reflection;
using System.Text;

namespace Trendy_BackEnd.Services
{
    public class CustomerOrderServices
    {
        #region Genertae Customer Order Code
        static string GenerateRandomString(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder stringBuilder = new StringBuilder(length);
            Random random = new Random();

            for (int i = 0; i < length; i++)
            {
                int index = random.Next(chars.Length);
                stringBuilder.Append(chars[index]);
            }

            return stringBuilder.ToString();
        }
        #endregion

        #region Manage Customer Order Save
        internal async Task<ResponseResult> ManageCustomerOrderSave(ModelCustomerOrder account)
        {
            string CustomerOrdeCode = "";
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
                account.CustomerOrderID = GenerateRandomString(10);
                CustomerOrdeCode = account.CustomerOrderID;
                if (string.IsNullOrWhiteSpace(account.CustomerOrderID))
                {
                    account.CustomerOrderID = GenerateRandomString(10);
                    CustomerOrdeCode = account.CustomerOrderID;
                }
                r = await SaveCustomerOrder(account, CustomerOrdeCode);
                if (r.Message == "success" || r.Status == ApiRespond.Success.ToString())
                {
                    r = await SaveCustomerOrderItems(account.Items, CustomerOrdeCode);
                    if (r.Message == "success" || r.Status == ApiRespond.Success.ToString())
                    {
                        r.Content = CustomerOrdeCode;
                    }
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

        #region Save Customer Order
        internal async Task<ResponseResult> SaveCustomerOrder(ModelCustomerOrder account , string CustomerOrdeCode)
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
                            CommandText = "sp_CustomerOrder_save"
                        };
                        command.Parameters.AddWithValue("@CustomerOrderID", CustomerOrdeCode);
                        command.Parameters.AddWithValue("@UserId", account.UserId);
                        command.Parameters.AddWithValue("@Address", account.Address);
                        command.Parameters.AddWithValue("@FirstName", account.FirstName);
                        command.Parameters.AddWithValue("@Subtotal", account.Subtotal);
                        command.Parameters.AddWithValue("@Discount", account.Discount);
                        command.Parameters.AddWithValue("@FullTotal", account.FullTotal);
                        command.Parameters.AddWithValue("@PaymentMethod", account.PaymentMethod);
                        command.Parameters.AddWithValue("@CardLastDigits", account.CardLastDigits);
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
                        else
                        {
                            r.Status = ApiRespond.Fail.ToString();
                            r.Content = count;
                            r.Message = "Error! Something Went Wrong,Customer Order Save Failed";
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

        #region Save Customer Order items
        internal async Task<ResponseResult> SaveCustomerOrderItems(List<ModelCustomerOrderItem> accounts,string CustomerOrdeCode)
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

                foreach (var account in accounts)
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
                                CommandText = "sp_CustomerOrderItem_save"
                            };
                            command.Parameters.AddWithValue("@CustomerOrderID", CustomerOrdeCode);
                            command.Parameters.AddWithValue("@ItemId", account.ItemId);
                            command.Parameters.AddWithValue("@Name", account.Name);
                            command.Parameters.AddWithValue("@Link", account.Link);
                            command.Parameters.AddWithValue("@Price", account.Price);
                            command.Parameters.AddWithValue("@Qty", account.Qty);
                            command.Parameters.AddWithValue("@Total", account.Total);
                            command.Parameters.AddWithValue("@size", account.size);
                            command.Parameters.AddWithValue("@Color", account.Color);
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
                            else
                            {
                                r.Status = ApiRespond.Fail.ToString();
                                r.Content = count;
                                r.Message = "Something Went Wrong, Got an Unknown Error";
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
