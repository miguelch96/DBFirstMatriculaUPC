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
    public partial class frmMatriculaAlumno : Form
    {

        Int32? AlumnoId;
        public MatriculaEntities context { get; set; }

        public frmMatriculaAlumno(Int32 alumnoId)
        {
            AlumnoId = alumnoId;
            InitializeComponent();
            Cargar();
        }

        private void Cargar()
        {
            context = new MatriculaEntities();
            CargarCursos();
            CargarMatricula();
        }

        private void CargarCursos()
        {
            var lstCursos = context.Curso.Where(x => x.Nombre.Contains("")).ToList();
            //var lstCursos = context.Curso.Select(x => new Curso()).ToList();
            dgvCursos.DataSource = lstCursos;     
        }

        private void CargarMatricula()
        {
            if (context.Matricula.Any(x => x.AlumnoId == AlumnoId)==false)
                return;

            else
            {
                var lstcursosMatriculados = context.Matricula.Where(x => x.AlumnoId == AlumnoId).ToList();
                dgvMatricula.DataSource=lstcursosMatriculados;
            }
            //var lstCursos = context.Curso.Where(x => x.Nombre.Contains(""));
            //var lstCursos = context.Curso.Select(x => new Curso()).ToList();
            //dgvCursos.DataSource = lstCursos;

        }

        private void btnMatricular_Click(object sender, EventArgs e)
        {
            if (dgvCursos.SelectedRows.Count == 0)//si no se selecciono ninguna fila
                return;

            var matricula = new Matricula();
            matricula.AlumnoId = (Int32)AlumnoId;
            matricula.CursoId= Convert.ToInt32(dgvCursos.SelectedRows[0].Cells["CursoId"].Value);
            matricula.Estado = "ACT";

            context.Matricula.Add(matricula);
            context.SaveChanges();
            CargarMatricula();
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvMatricula.SelectedRows.Count == 0)//si no se selecciono ninguna fila
                return;

            var matriculaId = Convert.ToInt32(dgvMatricula.SelectedRows[0].Cells["MatriculaId"].Value);

            context.Matricula.Remove(context.Matricula.Where(x => x.MatriculaId == matriculaId).First());
            context.SaveChanges();
            CargarMatricula();
        }

        private void btnSalir_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
