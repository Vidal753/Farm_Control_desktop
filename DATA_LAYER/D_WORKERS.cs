﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using ENTITIES_LAYER;
namespace DATA_LAYER
{
    public class D_WORKERS
    {
        SqlConnection conexion = new SqlConnection(ConfigurationManager.ConnectionStrings["conectar"].ConnectionString);
        public List<E_WORKERS> WorkersList(string Search, String LastName, String Cedula)
        {
            SqlDataReader ReadRow;
            SqlCommand cmd = new SqlCommand("SP_SEARCHWORKER", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@NAMES", Search);
            cmd.Parameters.AddWithValue("@LASTNAME", LastName);
            cmd.Parameters.AddWithValue("@CEDULA", Cedula);

            ReadRow = cmd.ExecuteReader();

            List<E_WORKERS> Listing = new List<E_WORKERS>();


            while (ReadRow.Read())
            {
                Listing.Add(new E_WORKERS
                {
                    IDWORKERS = ReadRow.GetInt32(0),
                    CEDULA = ReadRow.GetString(1),
                    NAMES = ReadRow.GetString(2),
                    LASTNAME = ReadRow.GetString(3),
                    PHONE = ReadRow.GetString(4),
                  


                });
            }
            conexion.Close();
            ReadRow.Close();
            return Listing;
        }

        public void DeleteWorker(int id)
        {
            SqlCommand cmd = new SqlCommand("SP_DELETEWORKER", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDWORKER", id);
            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        public void UpdateWorker(E_WORKERS entities)
        {
            SqlCommand cmd = new SqlCommand("SP_UPDATEWORKER", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();

            cmd.Parameters.AddWithValue("@IDWORKER", entities.IDWORKERS);
            cmd.Parameters.AddWithValue("@CEDULA", entities.CEDULA);
            cmd.Parameters.AddWithValue("@NAMES", entities.NAMES);
            cmd.Parameters.AddWithValue("@LASTNAME", entities.LASTNAME);
            cmd.Parameters.AddWithValue("@PHONE", entities.PHONE);
            


            cmd.ExecuteNonQuery();
            conexion.Close();
        }
        public void InsertWorker(E_WORKERS entities)
        {
            SqlCommand cmd = new SqlCommand("SP_INSERTWORKER", conexion);
            cmd.CommandType = CommandType.StoredProcedure;
            conexion.Open();


            cmd.Parameters.AddWithValue("@CEDULA", entities.CEDULA);
            cmd.Parameters.AddWithValue("@NAMES", entities.NAMES);
            cmd.Parameters.AddWithValue("@LASTNAME", entities.LASTNAME);
            cmd.Parameters.AddWithValue("@PHONE", entities.PHONE);
     


            cmd.ExecuteNonQuery();
            conexion.Close();
        }
    }
}
