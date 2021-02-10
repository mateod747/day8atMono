using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Configuration;
using System.Threading.Tasks;
using Model;
using Model.Common;
using Repository.Common;
using System.Data;

namespace Repository
{
    public class PepperRepository : IPepperRepository
    {
        static string con = ConfigurationManager.ConnectionStrings["sqlServer"].ConnectionString;
        static SqlConnection conn = new SqlConnection(con);
        public async Task<bool> SavePepperAsync(IPepperModel model)
        {
            SqlCommand insert = new SqlCommand("insert into Peppers values(@id, @name);", conn);

            insert.Parameters.AddWithValue("@id", model.ID);
            insert.Parameters.AddWithValue("@name", model.Name);

            conn.Open();

            try
            {
                SqlDataAdapter dataAdapter = new SqlDataAdapter();
                dataAdapter.InsertCommand = insert;
                await dataAdapter.InsertCommand.ExecuteNonQueryAsync();

                conn.Close();

                return true;
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }
        public async Task<List<IPepperModel>> GetAllPeppersAsync(int pageSize, int pageNumber, string sort, string filterBy)
        {
            #region Pagination

            Pagination.PageSize = pageSize;
            int numOfPages = 1;

            SqlCommand countNumber = new SqlCommand("select count(*) from Peppers;", conn);

            conn.Open();

            try
            {
                Pagination.RecordCount = (int)countNumber.ExecuteScalar();
            } catch(Exception)
            {
                return null;
            }

            numOfPages = Pagination.GetNumberOfPages();

            if (pageNumber > numOfPages)
            {
                pageNumber = numOfPages;
            } else if(pageNumber < 1)
            {
                pageNumber = 1;
            }
                       
            conn.Close();

            #endregion

            #region Sql query for pagination, sorting and filtering

            SqlCommand show;

            if(sort.ToLower().Equals("asc"))
            {
                show = new SqlCommand("with Number as (select top 100 percent PepperID, PepperName, row_number() over (order by PepperID) as RowNumber from Peppers where PepperName like @name) select PepperID, PepperName from Number where RowNumber between @from and @to;", conn);
            } else if(sort.ToLower().Equals("desc"))
            {
                show = new SqlCommand("with Number as (select top 100 percent PepperID, PepperName, row_number() over (order by PepperID) as RowNumber from Peppers where PepperName like @name order by PepperName desc) select PepperID, PepperName from Number where RowNumber between @from and @to order by PepperName desc;", conn);
            } else
            {
                return null;
            }

            show.Parameters.AddWithValue("@from", (pageNumber - 1) * pageSize);
            show.Parameters.AddWithValue("@to", pageNumber * pageSize);
            show.Parameters.AddWithValue("@sort", sort);
            show.Parameters.AddWithValue("@name", filterBy);

            #endregion

            List<IPepperModel> peppers = new List<IPepperModel>();

            conn.Open();
            SqlDataReader reader = await show.ExecuteReaderAsync();

            try
            {
                string name = "";
                int id;
                if (reader.HasRows)
                {
                    while (await reader.ReadAsync())
                    {
                        id = reader.GetInt32(0);
                        name = reader.GetString(1);

                        PepperModel pepper = new PepperModel();

                        pepper.ID = id;
                        pepper.Name = name;

                        peppers.Add(pepper);
                    }
                }

                reader.Close();
                conn.Close();
                return peppers;
            }
            catch (Exception)
            {
                conn.Close();
                return null;
            }
        }
        public async Task<bool> UpdatePepperAsync(IPepperModel model)
        {
            SqlCommand update = new SqlCommand("update Peppers set PepperName = @name where PepperID = @id;", conn); ;

            update.Parameters.AddWithValue("@id", model.ID);
            update.Parameters.AddWithValue("@name", model.Name);

            conn.Open();

            try
            {
                await update.ExecuteNonQueryAsync();
                conn.Close();
                return true;
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }
        public async Task<bool> DeletePepperAsync(int id)
        {
            SqlCommand delete = new SqlCommand("delete from Peppers where PepperID=@id;", conn);

            delete.Parameters.AddWithValue("@id", id);
            conn.Open();

            try
            {
                await delete.ExecuteNonQueryAsync();
                conn.Close();
                return true;
            }
            catch (Exception)
            {
                conn.Close();
                return false;
            }
        }            
    }
}
