using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace wEventosSociales
{
    public class clsGuardarBD
    {
        public int intCodigoHistorial { get; set; }
        public int intCodigoEvento {  get; set; }

        public DateTime datFecha {  get; set; }
        public TimeSpan datHora { get; set; }
        public string strUbicacion { get; set; }
        public int intInvitadosAprox {  get; set; }
        public string strDescripcion { get; set; }

        public string TipoEvento { get; set; }

        public string ContenidoLista { get; set; }
        public string ContenidoActividades { get; set;}
        public string ContenidoInvitados { get;}


        public clsGuardarBD() 
        {

        }

        public clsGuardarBD(int intCodigoEvento,DateTime datfecha, TimeSpan dathora, string strubicacion, int intinvitadosAprox, string strdescripcion)
        {
            this.intCodigoEvento = intCodigoEvento;
            this.datFecha = datfecha;
            this.datHora = dathora;
            this.strUbicacion = strubicacion;
            this.intInvitadosAprox = intinvitadosAprox;
            this.strDescripcion = strdescripcion;

            this.intCodigoHistorial = intCodigoEvento;

        }
        public bool insertarDato()
        {
            try
            {
                string cadenaConexion = "server=B13-204-22891;database=dboCreacionEventos; integrated security=true";

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();
                    

                    string insertarEvento = "insert into tblEvento values (@intCodigoEvento, @datFecha, @datHora, @strUbicacion, @intInvitadosAprox, @strDescripcion)";
                    SqlCommand cmdEvento = new SqlCommand(insertarEvento, conexion);

                    cmdEvento.Parameters.AddWithValue("@intCodigoEvento", this.intCodigoEvento);
                    cmdEvento.Parameters.AddWithValue("@datFecha", this.datFecha);
                    cmdEvento.Parameters.AddWithValue("@datHora", this.datHora);
                    cmdEvento.Parameters.AddWithValue("@strUbicacion", this.strUbicacion);
                    cmdEvento.Parameters.AddWithValue("@intInvitadosAprox", this.intInvitadosAprox);
                    cmdEvento.Parameters.AddWithValue("@strDescripcion", this.strDescripcion);
                    cmdEvento.ExecuteNonQuery();

                    string insertarHistorial = "insert into tblHistorial values (@intCodigoHistorial, @intCodigoEvento, @datFecha, @datHora, @strUbicacion, @intInvitadosAprox, @strDescripcion)";
                    SqlCommand cmdHistorial = new SqlCommand(insertarHistorial, conexion);
                    cmdHistorial.Parameters.AddWithValue("@intCodigoHistorial", this.intCodigoHistorial);
                    cmdHistorial.Parameters.AddWithValue("@intCodigoEvento", this.intCodigoEvento);
                    cmdHistorial.Parameters.AddWithValue("@datFecha", this.datFecha);
                    cmdHistorial.Parameters.AddWithValue("@datHora", this.datHora);
                    cmdHistorial.Parameters.AddWithValue("@strUbicacion", this.strUbicacion);
                    cmdHistorial.Parameters.AddWithValue("@intInvitadosAprox", this.intInvitadosAprox);
                    cmdHistorial.Parameters.AddWithValue("@strDescripcion", this.strDescripcion);
                    cmdHistorial.ExecuteNonQuery();                                                    
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al insertar datos:" + ex.Message);
                return false;
            }

        }

        public bool eliminarDato(int intCodigoEvento)
        {
            try
            {
                string cadenaConexion = "server=B13-204-22891;database=dboCreacionEventos;integrated security=true";

                using (SqlConnection conexion = new SqlConnection(cadenaConexion))
                {
                    conexion.Open();

                    // Eliminar de tblHistorial primero debido a la clave foránea
                    string eliminarHistorial = "DELETE FROM tblHistorial WHERE intCodigoEvento = @intCodigoEvento";
                    SqlCommand cmdEliminarHistorial = new SqlCommand(eliminarHistorial, conexion);
                    cmdEliminarHistorial.Parameters.AddWithValue("@intCodigoEvento", intCodigoEvento);
                    cmdEliminarHistorial.ExecuteNonQuery();

                    string eliminarInvitados = "DELETE FROM tblInvitado WHERE intCodigoEvento = @intCodigoEvento";
                    SqlCommand cmdEliminarInvitados = new SqlCommand(eliminarInvitados,conexion);
                    cmdEliminarInvitados.Parameters.AddWithValue("@intCodigoEvento", intCodigoEvento);
                    cmdEliminarInvitados.ExecuteNonQuery();

                    // Eliminar de tblEvento
                    string eliminarEvento = "DELETE FROM tblEvento WHERE intCodigoEvento = @intCodigoEvento";
                    SqlCommand cmdEliminarEvento = new SqlCommand(eliminarEvento, conexion);
                    cmdEliminarEvento.Parameters.AddWithValue("@intCodigoEvento", intCodigoEvento);
                    cmdEliminarEvento.ExecuteNonQuery();
                }
                return true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error al eliminar datos: " + ex.Message);
                return false;
            }
        }
    }
}
