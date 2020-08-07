using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Practica_No._6.Models
{
    public class RegistroProducto
    {
        private SqlConnection con;

        //Conectar a DB
        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionDBProducto"].ToString();
            con = new SqlConnection(constr);
        }

        //Registrar
        public int GrabarProducto(Producto prod)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Insert Into Productos (Descripcion, Tipo, Precio) " +
                                                "Values (@Descripcion, @Tipo, @Precio)", con);
            comando.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            comando.Parameters.Add("@Tipo", SqlDbType.VarChar);
            comando.Parameters.Add("@Precio", SqlDbType.Float);

            comando.Parameters["@Descripcion"].Value = prod.Descripcion;
            comando.Parameters["@Tipo"].Value = prod.Tipo;
            comando.Parameters["@Precio"].Value = prod.Precio;

            con.Open();
            int i = comando.ExecuteNonQuery();
            return i;
        }

        //Obtener las peliculas
        public List<Producto> RecuperarTodos()
        {
            Conectar();
            List<Producto> productos = new List<Producto>();

            SqlCommand cmd = new SqlCommand("select Id, Descripcion, Tipo, Precio from Productos", con);
            con.Open();
            SqlDataReader registros = cmd.ExecuteReader();
            while (registros.Read())
            {
                Producto peli = new Producto
                {
                    Id = int.Parse(registros["Id"].ToString()),
                    Descripcion = registros["Descripcion"].ToString(),
                    Tipo = registros["Tipo"].ToString(),
                    Precio = double.Parse(registros["Precio"].ToString()),

                };
                productos.Add(peli);
            }
            con.Close();
            return productos;
        }

        //Mostrar un registro especifico
        public Producto Recuperar(int id)
        {
            Conectar();
            SqlCommand cmd = new SqlCommand("select Id, Descripcion, Tipo, Precio " +
                                            "from Productos where Id=@Id", con);
            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = id;
            con.Open();
            SqlDataReader registros = cmd.ExecuteReader();
            Producto producto = new Producto();
            if (registros.Read())
            {

                producto.Id = int.Parse(registros["Id"].ToString());
                producto.Descripcion = registros["Descripcion"].ToString();
                producto.Tipo = registros["Tipo"].ToString();
                producto.Precio = double.Parse(registros["Precio"].ToString());
            }
            con.Close();
            return producto;
        }

        //Modificar
        public int Modificar(Producto prod)
        {
            Conectar();
            SqlCommand cmd = new SqlCommand("update Productos set Descripcion=@Descripcion, Tipo=@Tipo, Precio=@Precio " +
                                            "where Id=@Id", con);

            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = prod.Id;

            cmd.Parameters.Add("@Descripcion", SqlDbType.VarChar);
            cmd.Parameters["@Descripcion"].Value = prod.Descripcion;
            cmd.Parameters.Add("@Tipo", SqlDbType.VarChar);
            cmd.Parameters["@Tipo"].Value = prod.Tipo;
            cmd.Parameters.Add("@Precio", SqlDbType.Float);
            cmd.Parameters["@Precio"].Value = prod.Precio;

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public int Borrar(int id)
        {
            Conectar();
            SqlCommand cmd = new SqlCommand("delete from Productos where Id=@Id", con);
            cmd.Parameters.Add("@Id", SqlDbType.Int);
            cmd.Parameters["@Id"].Value = id;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }
    }
}