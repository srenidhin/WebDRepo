using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;

namespace MaawinPages.Models
{
    public class DAL
    {
        public int Add2DB(ProductModel p)
        {
            try
            {
                string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection conn = new SqlConnection(constr);
                conn.Open();
                string query = "exec InsertData @name,@code,@desc,@quan,@type,@cate,@img";
                SqlCommand cmd = new SqlCommand(query, conn);
                cmd.Parameters.Add(new SqlParameter("@name", p.pName));
                cmd.Parameters.Add(new SqlParameter("@code", p.pCode));
                cmd.Parameters.Add(new SqlParameter("@desc", p.pDesc));
                cmd.Parameters.Add(new SqlParameter("@quan", p.pQuantity));
                cmd.Parameters.Add(new SqlParameter("@type", p.pType));
                cmd.Parameters.Add(new SqlParameter("@cate", p.pCategory));
                cmd.Parameters.Add(new SqlParameter("@img", p.pImgLocation));
                int i = cmd.ExecuteNonQuery();
                conn.Close();
                return i;
            }catch(Exception e)
            {
                return 0;
            }
        }

        public DataTable GetData()
        {
            DataTable dt = new DataTable();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            String que = "select Name,Code,Category,Type from tblProduct";
            SqlCommand cmd = new SqlCommand(que, conn);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            da.Fill(dt);
            return dt;
        }

        public int DeleteData(string name)
        {
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            String que = "delete from tblProduct where name='{0}'";
            que = String.Format(que, name);
            SqlCommand cmd = new SqlCommand(que, conn);
            try
            {
                int i = cmd.ExecuteNonQuery();
                return i;
            }catch(Exception e)
            {
                return 0;
            }
        }

        public List<ProductModel> GetData(string category,string type)
        {
            List<ProductModel> prds = new List<ProductModel>();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            String que = "exec retrieveProducts @category,@type";
            SqlCommand cmd = new SqlCommand(que, conn);
            cmd.Parameters.Add(new SqlParameter("@category", category));
            cmd.Parameters.Add(new SqlParameter("@type", type));
            SqlDataReader dr = cmd.ExecuteReader();
            while(dr.Read())
            {
                ProductModel a = new ProductModel();
                a.pName = dr[0].ToString();
                a.pCode = dr[1].ToString();
                a.pDesc = dr[2].ToString();
                a.pQuantity = dr[3].ToString();
                a.pType = dr[4].ToString();
                a.pCategory = dr[5].ToString();
                a.pImgLocation = dr[6].ToString();
                prds.Add(a);
            }
            conn.Close();
            return prds;
        }

        public List<ProductModel> GetData(string category, string type, string srctxt)
        {
            List<ProductModel> prds = new List<ProductModel>();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            String que = "exec spSearchPrds @category,@type,@src";
            SqlCommand cmd = new SqlCommand(que, conn);
            cmd.Parameters.Add(new SqlParameter("@category", category));
            cmd.Parameters.Add(new SqlParameter("@type", type));
            cmd.Parameters.Add(new SqlParameter("@src", srctxt));
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ProductModel a = new ProductModel();
                a.pName = dr[0].ToString();
                a.pCode = dr[1].ToString();
                a.pDesc = dr[2].ToString();
                a.pQuantity = dr[3].ToString();
                a.pType = dr[4].ToString();
                a.pCategory = dr[5].ToString();
                a.pImgLocation = dr[6].ToString();
                prds.Add(a);
            }
            conn.Close();
            return prds;
        }

        public List<ProductModel> GetData(string srctxt)
        {
            List<ProductModel> prds = new List<ProductModel>();
            string constr = ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
            SqlConnection conn = new SqlConnection(constr);
            conn.Open();
            String que = "exec searchTxt @txt";
            SqlCommand cmd = new SqlCommand(que, conn);
            cmd.Parameters.Add(new SqlParameter("@txt", srctxt));
            SqlDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ProductModel a = new ProductModel();
                a.pName = dr[0].ToString();
                a.pCode = dr[1].ToString();
                a.pDesc = dr[2].ToString();
                a.pQuantity = dr[3].ToString();
                a.pType = dr[4].ToString();
                a.pCategory = dr[5].ToString();
                a.pImgLocation = dr[6].ToString();
                prds.Add(a);
            }
            conn.Close();
            return prds;
        }
    }
}