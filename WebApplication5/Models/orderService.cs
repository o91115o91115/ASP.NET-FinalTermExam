using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace WebApplication5.Models
{
    public class orderService
    {
        private string GetDBConnectionString()
        {
            return @"data source=OLY-PC\SQLEXPRESS;initial catalog=TSQL2012;integrated security=True;multipleactiveresultsets=True;application name=EntityFramework";
        }


        public List<Models.orderSeach> SeachCust(orderSeach data)
        {
            DataTable dt = new DataTable();
            String sql = @"SELECT [CustomerID],[CompanyName],[ContactName],[ContactTitle]
                           FROM [Sales].[Customers]
                           WHERE ([CustomerID] LIKE @custid OR @custid='')
                           AND ([CompanyName] LIKE @custname OR @custname='')
                           AND ([ContactName] LIKE @contname OR @contname='')
                           AND ([ContactTitle] LIKE @conttitle OR @conttitle='')";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                cmd.Parameters.Add(new SqlParameter("@custid", "%" + data.CustomerID + "%"));
                cmd.Parameters.Add(new SqlParameter("@custname", "%" + data.CompanyName + "%"));
                cmd.Parameters.Add(new SqlParameter("@contname", "%" + data.ContactName + "%"));
                cmd.Parameters.Add(new SqlParameter("@conttitle", "%" + data.ContactTitle + "%"));

                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.CustSeachToList(dt);
        }
        /// <summary>
        ///將搜尋結果轉成Lsist
        /// </summary>
        /// <param name="orderdata"></param>
        /// <returns></returns>
        public List<Models.orderSeach> CustSeachToList(DataTable orderdata)
        {
            List<Models.orderSeach> result = new List<Models.orderSeach>();
            foreach (DataRow row in orderdata.Rows)
            {
                result.Add(new Models.orderSeach()
                {
                    CustomerID = Convert.ToString(row["CustomerID"]),
                    CompanyName = Convert.ToString(row["CompanyName"]),
                    ContactName = Convert.ToString(row["ContactName"]),
                    ContactTitle = Convert.ToString(row["ContactTitle"]),
                });
            }
            return result;
        }




        /// <summary>
        /// 聯絡人title
        /// </summary>
        /// <returns></returns>
        public List<Models.Customers> GetCustData()
        {
            DataTable dt = new DataTable();
            String sql = @"select distinct [ContactTitle] from [Sales].[Customers] ";
            using (SqlConnection conn = new SqlConnection(this.GetDBConnectionString()))
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand(sql, conn);
                SqlDataAdapter sqlAdapter = new SqlDataAdapter(cmd);
                sqlAdapter.Fill(dt);
                conn.Close();
            }
            return this.CustDataToList(dt);
        }
        /// <summary>
        /// 將聯絡人title資料轉型為List
        /// </summary>
        /// <param name="orderdata"></param>
        /// <returns></returns>
        private List<Models.Customers> CustDataToList(DataTable orderdata)
        {

            List<Models.Customers> result = new List<Models.Customers>();
            foreach (DataRow row in orderdata.Rows)
            {
                result.Add(new Models.Customers()
                {
                    ContactTitle = Convert.ToString(row["ContactTitle"]),
                });
            }
            return result;
        }
    }
}