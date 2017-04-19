using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DatabaseFirstMatricula
{
    public partial class frmEditAlumno : Form
    {
        public MatriculaEntities context { get; set; }
        public Int32? AlumnoId { get; set; }

        public frmEditAlumno(Int32? alumnoId)
        {
            AlumnoId = alumnoId;
            InitializeComponent();
            Cargar();
        }

        private void Cargar()
        {
            context = new MatriculaEntities();
            if (AlumnoId.HasValue)
            {
                this.Text = "Actualizar datos alumno";
                btnGuardar.Text = "Actualizar";
                var alumno = context.Alumno.Find(AlumnoId);
                txtCodigo.Text = alumno.Codigo;
                txtNombre.Text = alumno.Nombre;
                txtApellido.Text = alumno.Apellido;

                if (alumno.Estado == "ACT")
                    checkEstado.Checked = true;
                else
                    checkEstado.Checked = false;


            }

            else
            {
                this.Text = "Registrar alumno";
                btnGuardar.Text = "Registrar";
            }
        }

        private Boolean ValidarDatos()
        {
            var error = "";
            if (String.IsNullOrEmpty(txtCodigo.Text))
            {
                //MessageBox.Show("Debe ingresar un codigo");
                error += "Debe ingresar un codigo" + Environment.NewLine;
                return false;

            }

            if (String.IsNullOrEmpty(txtNombre.Text))
            {
                //MessageBox.Show("Debe ingresar un nombre");
                error += "Debe ingresar un nombre" + Environment.NewLine;
                return false;
            }

            if (String.IsNullOrEmpty(txtApellido.Text))
            {
                //MessageBox.Show("Debe ingresar un nombre");
                error += "Debe ingresar el apellido" + Environment.NewLine;
                return false;
            }

            if (!String.IsNullOrEmpty(error))
                MessageBox.Show("Ha ocurrido un error,revisar:" + Environment.NewLine + error);


            return String.IsNullOrEmpty(error);



        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            String resultado;
            var alumno = new Alumno();
            if (AlumnoId.HasValue)
            {
                alumno = context.Alumno.Find(AlumnoId);
                resultado = "Se actulizo correctamente";
            }
            else
            {
                context.Alumno.Add(alumno);
                //Asignar las propiedades por defecto
                resultado = "Se registro correctamente";
            }

            alumno.Codigo = txtCodigo.Text;
            alumno.Nombre = txtNombre.Text;
            alumno.Apellido = txtApellido.Text;
            alumno.Estado = checkEstado.Checked ? "ACT" : "INA";
            context.SaveChanges();
            MessageBox.Show(resultado);
            this.Close();

        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
