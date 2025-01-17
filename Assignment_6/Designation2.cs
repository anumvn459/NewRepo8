﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;

namespace Assignment_6
{
    public class Designation2
    {
        public SqlConnection con = new SqlConnection();
        public SqlCommand cmd = new SqlCommand();

        public Designation2()
        {
            con.ConnectionString = "Data Source=.;Initial Catalog=Assignment_6;Integrated Security=True;Pooling=False";
            cmd.Connection = con;
        }
        public SqlConnection GetCon()
        {
            if (con.State == ConnectionState.Closed)
            {
                con.Open();
            }
            return con;

        }
        public void dbclose()
        {
            con.Close();
        }
        public int exenonquery(String sql)
        {
            GetCon();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            int i = cmd.ExecuteNonQuery();
            return i;
        }
        public object exescalar(String sql)
        {
            GetCon();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            object obj = cmd.ExecuteScalar(); //scalar for single value
            return obj;

        }
        public SqlDataReader exereader(String sql)
        {
            GetCon();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = sql;
            SqlDataReader dr = cmd.ExecuteReader();// mutiple value retrieval
            return dr;
        }
        //Disconnected Architecture
        public DataSet exedataset(String sql)
        {
            cmd.CommandType = CommandType.Text; //set command type
            cmd.CommandText = sql; //pass sql command to command text
            SqlDataAdapter sd = new SqlDataAdapter(cmd);//Createadapter class because disconnected architecture
            DataSet ds = new DataSet();//create dataset
            sd.Fill(ds);
            return ds;

        }
        public void fillgrid(String sql, GridView dv)
        {
            dv.DataSource = exedataset(sql);
            dv.DataBind();
        }
    }
}