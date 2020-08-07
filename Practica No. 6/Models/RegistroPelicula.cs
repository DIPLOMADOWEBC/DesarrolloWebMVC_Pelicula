using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Practica_No._6.Models
{
    public class RegistroPelicula
    {
        private SqlConnection con;

        //Conectar a DB
        private void Conectar()
        {
            string constr = ConfigurationManager.ConnectionStrings["ConexionDB"].ToString();
            con = new SqlConnection(constr);
        }
        
        //Registrar
        public int GrabarPelicula(Pelicula peli)
        {
            Conectar();
            SqlCommand comando = new SqlCommand("Insert Into TBL_PELICULA (Titulo, Director, AutorPrincipal, No_Actores, Duracion, Estreno) " +
                                                "Values (@Titulo, @Director, @AutorPrincipal, @No_Actores, @Duracion, @Estreno)", con);
            comando.Parameters.Add("@Titulo", SqlDbType.VarChar);
            comando.Parameters.Add("@Director", SqlDbType.VarChar);
            comando.Parameters.Add("@AutorPrincipal", SqlDbType.VarChar);
            comando.Parameters.Add("@No_Actores", SqlDbType.Int);
            comando.Parameters.Add("@Duracion", SqlDbType.VarChar);
            comando.Parameters.Add("@Estreno", SqlDbType.VarChar);

            comando.Parameters["@Titulo"].Value = peli.Titulo;
            comando.Parameters["@Director"].Value = peli.Director;
            comando.Parameters["@AutorPrincipal"].Value = peli.AutorPrincipal;
            comando.Parameters["@No_Actores"].Value = peli.numAutores;
            comando.Parameters["@Duracion"].Value = peli.Duracion;
            comando.Parameters["@Estreno"].Value = peli.Estreno;

            con.Open();
            int i = comando.ExecuteNonQuery();
            return i;
        }

        //Obtener las peliculas
        public List<Pelicula> RecuperarTodos()
        {
            Conectar();
            List<Pelicula> peliculas = new List<Pelicula>();

            SqlCommand cmd = new SqlCommand("select Codigo, Titulo, Director, AutorPrincipal, No_Actores, Duracion, Estreno from TBL_PELICULA", con);
            con.Open();
            SqlDataReader registros = cmd.ExecuteReader();
            while(registros.Read())
            {
                Pelicula peli = new Pelicula
                {
                    Codigo = int.Parse(registros["Codigo"].ToString()),
                    Titulo = registros["Titulo"].ToString(),
                    Director = registros["Director"].ToString(),
                    AutorPrincipal = registros["AutorPrincipal"].ToString(),
                    numAutores = int.Parse(registros["No_Actores"].ToString()),
                    Duracion = double.Parse(registros["Duracion"].ToString()),
                    Estreno = int.Parse(registros["Estreno"].ToString()),

                };
                peliculas.Add(peli);
            }
            con.Close();
            return peliculas;
        }

        //Mostrar un registro especifico
        public Pelicula Recuperar(int codigo)
        {
            Conectar();
            SqlCommand cmd = new SqlCommand("select Codigo, Titulo, Director, AutorPrincipal, No_Actores, Duracion, Estreno " +
                                            "from TBL_PELICULA where Codigo=@Codigo", con);
            cmd.Parameters.Add("@Codigo", SqlDbType.Int);
            cmd.Parameters["@Codigo"].Value = codigo;
            con.Open();
            SqlDataReader registros = cmd.ExecuteReader();
            Pelicula pelicula = new Pelicula();
            if (registros.Read())
            {
                pelicula.Codigo = int.Parse(registros["Codigo"].ToString());
                pelicula.Titulo = registros["Titulo"].ToString();
                pelicula.Director = registros["Director"].ToString();
                pelicula.AutorPrincipal = registros["AutorPrincipal"].ToString();
                pelicula.numAutores = int.Parse(registros["No_Actores"].ToString());
                pelicula.Duracion = double.Parse(registros["Duracion"].ToString());
                pelicula.Estreno = int.Parse(registros["Estreno"].ToString());
            }
            con.Close();
            return pelicula;
        }

        //Modificar
        public int Modificar(Pelicula peli)
        {
            Conectar();
            SqlCommand cmd = new SqlCommand("update TBL_PELICULA set Titulo=@Titulo, Director=@Director, AutorPrincipal=@AutorPrincipal, No_Actores=@No_Actores, " +
                                            "Duracion=@Duracion, Estreno=@Estreno where Codigo=@Codigo", con);

            cmd.Parameters.Add("@Codigo", SqlDbType.Int);
            cmd.Parameters["@Codigo"].Value = peli.Codigo;

            cmd.Parameters.Add("@Titulo", SqlDbType.VarChar);
            cmd.Parameters["@Titulo"].Value = peli.Titulo;
            cmd.Parameters.Add("@Director", SqlDbType.VarChar);
            cmd.Parameters["@Director"].Value = peli.Director;
            cmd.Parameters.Add("@AutorPrincipal", SqlDbType.VarChar);
            cmd.Parameters["@AutorPrincipal"].Value = peli.AutorPrincipal;
            cmd.Parameters.Add("@No_Actores", SqlDbType.Int);
            cmd.Parameters["@No_Actores"].Value = peli.numAutores;
            cmd.Parameters.Add("@Duracion", SqlDbType.Float);
            cmd.Parameters["@Duracion"].Value = peli.Duracion;
            cmd.Parameters.Add("@Estreno", SqlDbType.VarChar);
            cmd.Parameters["@Estreno"].Value = peli.Estreno;

            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

        public int Borrar(int codigo)
        {
            Conectar();
            SqlCommand cmd = new SqlCommand("delete from TBL_PELICULA where Codigo=@Codigo", con);
            cmd.Parameters.Add("@Codigo", SqlDbType.Int);
            cmd.Parameters["@Codigo"].Value = codigo;
            con.Open();
            int i = cmd.ExecuteNonQuery();
            con.Close();
            return i;
        }

    }
}