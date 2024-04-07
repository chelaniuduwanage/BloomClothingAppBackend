using static Trendy_BackEnd.Config.EnumCollection;
using System.Data.SqlClient;
using Trendy_BackEnd.Config;
using Trendy_BackEnd.Models;
using System.Data;
using System.Drawing;
using System.Xml.Linq;

namespace Trendy_BackEnd.Services
{
    public class ItemServices
    {
        #region Get Item By Id 

        internal async Task<ModelItem> GetItemById(string Id)
        {
            SqlCommand command;
            ModelItem r;
            r = new()
            {
                Id = 0,
                Name = null
            };
            try
            {
                ModelItem v = new ModelItem();
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
                            CommandText = "sp_Item_GetById"
                        };
                        command.Parameters.AddWithValue("@Id", Id);
                        using (SqlDataReader dr = await command.ExecuteReaderAsync())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    v = new()
                                    {
                                        Id = Convert.ToInt32(dr["Id"]),
                                        Name = dr["Name"].ToString(),
                                        Category = dr["Category"].ToString(),
                                        Link1 = dr["Link1"].ToString(),
                                        Link2 = dr["Link2"].ToString(),
                                        Link3 = dr["Link3"].ToString(),
                                        Description = dr["Description"].ToString(),
                                        Color = dr["Color"].ToString(),
                                        ItemSize = dr["ItemSize"].ToString(),
                                        Price = Convert.ToDecimal(dr["Price"])

                                    };

                                }
                                r = v;
                            }
                            else
                            {
                                r.Id = 0;
                                r.Name = null;
                            }
                        }

                        command.Dispose();
                    }
                    catch (Exception ex)
                    {
                        r.Id = 0;
                        r.Name = null;
                    }
                    finally
                    { con.Close(); }
                }
            }
            catch (Exception ex)
            {

                r.Id = 0;
                r.Name = null;
            }

            return r;
        }

        #endregion

        #region Search Products Details
        internal async Task<List<ModelItem>> SearchItems(string Name, string Category, string Color, string ItemSize, int nextPage,int rowCount,decimal MinPrice, decimal MaxPrice)
        {
            SqlCommand command;
            List<ModelItem> r;
            r = new()
            {
               
            };
            try
            {
                List<ModelItem> list = new();
                ModelItem v;
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
                            CommandText = "sp_Item_Search"
                        };
                        command.Parameters.AddWithValue("@Name", Name);
                        command.Parameters.AddWithValue("@Category", Category);
                        command.Parameters.AddWithValue("@Color", Color);
                        command.Parameters.AddWithValue("@ItemSize", ItemSize);
                        command.Parameters.AddWithValue("@offset", nextPage * rowCount);
                        command.Parameters.AddWithValue("@rowcount", rowCount);
                        command.Parameters.AddWithValue("@MinPrice", MinPrice);
                        command.Parameters.AddWithValue("@MaxPrice", MaxPrice);

                        using (SqlDataReader dr = await command.ExecuteReaderAsync())
                        {
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    v = new()
                                    {
                                        Id = Convert.ToInt32(dr["Id"]),
                                        Name = dr["Name"].ToString(),
                                        Category = dr["Category"].ToString(),
                                        Link1 = dr["Link1"].ToString(),
                                        Link2 = dr["Link2"].ToString(),
                                        Link3 = dr["Link3"].ToString(),
                                        Description = dr["Description"].ToString(),
                                        Color = dr["Color"].ToString(),
                                        ItemSize = dr["ItemSize"].ToString(),
                                        Price = Convert.ToDecimal(dr["Price"])

                                    };
                                    list.Add(v);
                                }

                                 r = list;
                            }
                            else
                            {
                            }
                        }
                        
                        command.Dispose();
                    }
                    catch (Exception ex)
                    {
                        return r;
                    }
                    finally
                    { con.Close(); }
                }
            }
            catch (Exception ex)
            {
                return r;
            }

            return r;
        }
        #endregion
    }
}
