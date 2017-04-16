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
    public partial class frmLogin : Form
    {
        public MatriculaEntities context { get; set; }
        public frmLogin()
        {
            InitializeComponent();
            context = new MatriculaEntities();
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

            if (!String.IsNullOrEmpty(error))
                MessageBox.Show("Ha ocurrido un error,revisar:" + Environment.NewLine+error);


            return String.IsNullOrEmpty(error);



        }


        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;


            var alumno = new Alumno();
            var codigo = txtCodigo.Text;

            if(context.Alumno.Any(x=>x.Codigo==codigo))
            {
                alumno = context.Alumno.Where(x=>x.Codigo==codigo).First();
                MessageBox.Show("Ud se logueo correctamente");
                var frmmatriculaAlumno = new frmMatriculaAlumno(alumno.AlumnoId);
                this.Close();
                frmmatriculaAlumno.Show();
            }
            else
            {
                MessageBox.Show("Usuario no existe");
            }
            
     
            
        }

        private void btnCancelar_Click(object sender, EventArgs e)
        {

        }
    }
}
