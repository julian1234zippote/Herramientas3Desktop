using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace wEventosSociales
{
    public partial class FormInicio : Form
    {
        public FormInicio()
        {
            InitializeComponent();
        }

        private void btnCrearEvento_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario formCrearEvento
            FormCrearEvento formCrearEvento = new FormCrearEvento();

            // Mostrar el formulario formCrearEvento y ocultar la ventana actual
            this.Hide();
            formCrearEvento.Show();

        }

        private void btnHistorial_Click(object sender, EventArgs e)
        {
            // Crear una instancia del formulario formCrearEvento
            FormHistorialEventos formHistorialEventos = new FormHistorialEventos();

            // Mostrar el formulario formCrearEvento
            formHistorialEventos.Show();
        }

        private void FormInicio_Load(object sender, EventArgs e)
        {

        }
    }
}

       