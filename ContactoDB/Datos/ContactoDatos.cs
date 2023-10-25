using ContactoDB.Models;
using System.Data.SqlClient;
using System.Data;
using System.Linq.Expressions;

namespace ContactoDB.Datos
{
    public class ContactoDatos
    {
        public List<ContactoModel> ListarContacto()
        {
            List<ContactoModel> lista = new List<ContactoModel>();
            var conexion = new Conexion();
            using (var conexion1 = new SqlConnection(conexion.CadenaSql()))
            {
                conexion1.Open();
                SqlCommand cmd = new SqlCommand("sp_ListarContacto", conexion1);
                cmd.CommandType = CommandType.StoredProcedure; //Comando de procedimiento almacenado
                using (var dr = cmd.ExecuteReader()) //Leer lo que se devolvió
                {
                    while (dr.Read())
                    {
                        lista.Add(new ContactoModel()
                        {
                            IdContacto = Convert.ToInt32(dr["IdContacto"]),
                            Nombre = dr["Nombre"].ToString(),
                            Telefono = dr["Telefono"].ToString(),
                            Correo = dr["Correo"].ToString(),
                            Clave = dr["Clave"].ToString()
                        });
                    }
                }
            }
            return lista;
        }

        public ContactoModel ObtenerContacto(int IdContacto)
        {
            //Creo un objeto vacío
            ContactoModel contacto = new ContactoModel();
            var conexion = new Conexion();
            //utilizar using para establecer la cadena de conexion
            using (var conexion1 = new SqlConnection(conexion.CadenaSql()))
            {
                conexion1.Open();
                SqlCommand cmd = new SqlCommand("sp_Obtener", conexion1);
                //Enviando un parametro al procedimiento almacenado
                cmd.Parameters.AddWithValue("IdContacto", IdContacto);
                cmd.CommandType = CommandType.StoredProcedure;
                using (var dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        //asigno los valores al objeto oContacto
                        contacto.IdContacto = Convert.ToInt32(dr["IdContacto"]);
                        contacto.Nombre = dr["Nombre"].ToString();
                        contacto.Telefono = dr["Telefono"].ToString();
                        contacto.Correo = dr["Correo"].ToString();
                        contacto.Clave = dr["Clave"].ToString();
                    }
                }
            }
            return contacto;
        }

        public bool GuardarContacto(ContactoModel model)
        {
            //creo una variable boolean
            bool respuesta;
            try
            {
                var conexion = new Conexion();
                //utilizar using para establecer la cadena de conexion
                using (var conexion1 = new SqlConnection(conexion.CadenaSql()))
                {
                    conexion1.Open();
                    SqlCommand cmd = new SqlCommand("sp_GuardarContacto", conexion1);
                    //enviando un parametro al procedimiento almacenado
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("Correo", model.Correo);
                    cmd.Parameters.AddWithValue("Clave", model.Clave);
                    cmd.CommandType = CommandType.StoredProcedure;
                    //Ejecutar el procedimiento almacenado
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }
        public bool EditarContacto(ContactoModel model)
        {
            //Creo una variable boolean
            bool respuesta;
            try
            {
                var conexion = new Conexion();
                //utilizar using para establecer la cadena de conexion
                using (var conexion1 = new SqlConnection(conexion.CadenaSql()))
                {
                    conexion1.Open();
                    SqlCommand cmd = new SqlCommand("sp_EditarContacto", conexion1);
                    // Envío los parámetros al procedimiento almacenado
                    cmd.Parameters.AddWithValue("IdContacto", model.IdContacto);
                    cmd.Parameters.AddWithValue("Nombre", model.Nombre);
                    cmd.Parameters.AddWithValue("Telefono", model.Telefono);
                    cmd.Parameters.AddWithValue("Correo", model.Correo);
                    cmd.Parameters.AddWithValue("Clave", model.Clave);
                    // Ejecuto el procedimiento almacenado
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                // Si no ocurre un error, la variable respuesta será true
                respuesta = true;
            }
            catch (Exception e)
            {
                // Si ocurre un error, la variable respuesta será false
                respuesta = false;
            }
            // Devuelvo la variable respuesta
            return respuesta;
        }


        public bool EliminarContacto(ContactoModel model)
        {
            bool respuesta;
            try
            {
                var conexion = new Conexion();
                using (var conexion1 = new SqlConnection(conexion.CadenaSql()))
                {
                    conexion1.Open();
                    SqlCommand cmd = new SqlCommand("sp_Eliminar", conexion1);
                    cmd.Parameters.AddWithValue("IdContacto", model.IdContacto);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.ExecuteNonQuery();
                }
                respuesta = true;
            }
            catch (Exception e)
            {
                string error = e.Message;
                respuesta = false;
            }
            return respuesta;
        }
    }
}