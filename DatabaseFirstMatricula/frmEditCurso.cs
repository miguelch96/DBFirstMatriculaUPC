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
    public partial class frmEditCurso : Form
    {
       

        public MatriculaEntities context { get; set; }
        public Int32? CursoId { get; set; }

        public frmEditCurso(Int32? cursoId)
        {
            CursoId = cursoId;
            InitializeComponent();
            Cargar();
        }

        private void Cargar()
        {
            context = new MatriculaEntities();
            if (CursoId.HasValue)
            {
                this.Text = "Actualizar curso";
                btnGuardar.Text = "Actualizar";
                var curso=context.Curso.Find(CursoId);
                txtCodigo.Text = curso.Codigo;
                txtNombre.Text = curso.Nombre;
                checkActivo.Checked = curso.Estado == "ACT";
               
            }

            else
            {
                this.Text = "Registrar curso";
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

            if (!String.IsNullOrEmpty(error))
                MessageBox.Show("Ha ocurrido un error,revisar:" + Environment.NewLine+error);


            return String.IsNullOrEmpty(error);



        }

        private void frmEditCurso_Load(object sender, EventArgs e)
        {

        }

        private void btnGuardar_Click(object sender, EventArgs e)
        {
            if (!ValidarDatos())
                return;

            String resultado;
            var curso = new Curso();
            if(CursoId.HasValue)
            {
                curso = context.Curso.Find(CursoId);
                resultado = "Se actulizo correctamente";
            }
            else
            {
                context.Curso.Add(curso);
                //Asignar las propiedades por defecto
                resultado = "Se registro correctamente";
            }

            curso.Codigo = txtCodigo.Text;
            curso.Nombre = txtNombre.Text;
            curso.Estado = checkActivo.Checked ? "ACT" : "INA";
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
