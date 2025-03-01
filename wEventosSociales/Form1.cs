using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using wEventosSociales;
using System.Drawing.Drawing2D;

namespace wEventosSociales
{
    public partial class FormCrearEvento : Form
    {
        private clsGuardarBD evento = new clsGuardarBD(); // Declaración del objeto clsEvento
        private clsDatosCompartidos datosCompartidos = new clsDatosCompartidos(); // Declaración del objeto clsDatosCompartidos
        public FormCrearEvento()
        {
            InitializeComponent();
            // Cambiar el tamaño de la fuente del TextBox txtDescripcion
            txtDescripcion.Font = new Font(txtDescripcion.Font.FontFamily, 14, FontStyle.Regular);          
        }

        private void formCrearEvento_Load(object sender, EventArgs e)
        {
            // Agregar opciones al ComboBox cboTipoEvento
            cboTipoEvento.Items.Add("Boda");
            cboTipoEvento.Items.Add("Cumpleaños");
            cboTipoEvento.Items.Add("Conferencia");
            cboTipoEvento.Items.Add("Reunion");
            // Agregar más opciones si es necesario
        }
        private void btnPlanificacion_Click(object sender, EventArgs e)
        {
            try
            {
                // Verificar si todos los campos están llenos
                if (cboTipoEvento.SelectedItem != null && !string.IsNullOrEmpty(txtUbicacion.Text)
                    && dtpFecha.Value != null && dtpHora.Value != null && !string.IsNullOrEmpty(txtDescripcion.Text) && !string.IsNullOrEmpty(txtInvitadosAprox.Text))
                {
                    if (!int.TryParse(txtInvitadosAprox.Text, out int invitados))
                    {
                        MessageBox.Show("Por favor, ingrese solo valores numéricos en el campo de invitados.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                    // Crear una instancia del evento con los datos ingresados
                    evento.intCodigoEvento = int.Parse(txtIntCodigoEvento.Text);
                    evento.TipoEvento = cboTipoEvento.SelectedItem.ToString();
                    evento.strUbicacion = txtUbicacion.Text;
                    evento.datFecha = dtpFecha.Value;
                    evento.datHora = dtpHora.Value.TimeOfDay;
                    evento.strDescripcion = cboTipoEvento.SelectedItem.ToString()+" \n"+txtDescripcion.Text;
                    evento.intInvitadosAprox = int.Parse(txtInvitadosAprox.Text);
                    // Crear una instancia del formulario FormPlanificacion
                    clsDatosCompartidos datosCompartidos = new clsDatosCompartidos();
                    FormPlanificacion formPlanificacion = new FormPlanificacion(evento, datosCompartidos);

                    // Mostrar el formulario FormPlanificacion y esconder el form actual
                    this.Hide();
                    formPlanificacion.Show();

                    SqlConnection conexion = new SqlConnection("server=B13-204-22891;database=dboCreacionEventos; integrated security=true");
                    conexion.Open();

                    // Insertar los datos en la base de datos
                    clsGuardarBD p1 = new clsGuardarBD(
                        Convert.ToInt32(txtIntCodigoEvento.Text),
                        dtpFecha.Value,
                        dtpHora.Value.TimeOfDay,
                        txtUbicacion.Text,
                        Convert.ToInt32(txtInvitadosAprox.Text),
                        evento.strDescripcion
                    );
                    p1.insertarDato();

                    // Mostrar mensaje de éxito
                    MessageBox.Show("Datos Ingresados");

                 
                }
                else
                {
                    MessageBox.Show("Por favor complete todos los campos antes de continuar.", "Campos incompletos", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
            catch (Exception ex)
            {
                // Mostrar mensaje de error
                MessageBox.Show("Error al ingresar el dato: " + ex.Message);
            }
        }


        private void cboTipoEvento_SelectedIndexChanged(object sender, EventArgs e)
        {
            string tipoEvento = cboTipoEvento.SelectedItem.ToString();
            

            switch (tipoEvento)
            {
                case "Boda":
                    lblDescripcionEvento.Visible=true;
                    break;
                case "Cumpleaños":
                    lblDescripcionEvento.Visible = true;
                    break;
                case "Conferencia":
                    lblDescripcionEvento.Visible=true;
                    break;
                case "Reunion":
                    lblDescripcionEvento.Visible=true;
                    break;
                default:
                    txtDescripcion.Text = "";
                    break;
            }

            // Establecer el tipo de evento seleccionado en el objeto clsEvento
            evento.TipoEvento = tipoEvento;
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            this.Close();
            FormInicio formInicio = new FormInicio();
            formInicio.Show();
        }              
    }
}
