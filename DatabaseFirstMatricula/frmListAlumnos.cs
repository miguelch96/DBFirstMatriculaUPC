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
    public partial class frmListAlumnos : Form
    {
        public MatriculaEntities context { get; set; }
        public frmListAlumnos()
        {
            InitializeComponent();
            dgvListaAlumnos.AutoGenerateColumns = false;
            Cargar();
        }

        private void Cargar()
        {
            context = new MatriculaEntities();
            CargarResultados();
        }

        private void CargarResultados()
        {
            var filtro = txtFiltro.Text;
           /* var query = context.Alumno.AsQueryable();
            foreach(var token in filtro.Split(','))
            {
                query = query.Where(x => x.Codigo.Contains(token) ||
                                    x=>x.Nombre.Contains(token) ||
                                  x=>x.Apellido.Contains(token));
            }
            */
            context = new MatriculaEntities();

            if (cbbTipoFiltro.SelectedIndex == 0)//Busqueda por codigo
            {
                var resultados = context.Alumno.Where(x => x.Codigo.Contains(filtro))
                .ToList();
                dgvListaAlumnos.DataSource = resultados;
            }

            else if (cbbTipoFiltro.SelectedIndex == 1)//Busqueda por nombre
            {
                var resultados = context.Alumno.Where(x => x.Nombre.Contains(filtro)).ToList();
                dgvListaAlumnos.DataSource = resultados;
            }

            else if (cbbTipoFiltro.SelectedIndex == 2)//Busqueda por apellido
            {
                var resultados = context.Alumno.Where(x => x.Apellido.Contains(filtro)).ToList();
                dgvListaAlumnos.DataSource = resultados;
            }

            else if (cbbTipoFiltro.SelectedIndex == 3)//Busqueda por estado
            {
                var resultados = context.Alumno.Where(x => x.Estado.Contains(filtro)).ToList();
                dgvListaAlumnos.DataSource = resultados;
            }

            else // Tipo filtro vacio
            {
                var resultados = context.Alumno.Where(x => x.Codigo.Contains(filtro) 
                || x.Nombre.Contains(filtro)
                || x.Apellido.Contains(filtro)
                || x.Estado.Contains(filtro))
               .ToList();
                dgvListaAlumnos.DataSource = resultados;
            }
            //context.Alumno.OrderBy(x => x.AlumnoId);
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var frmeditAlumno = new frmEditAlumno(null);
            frmeditAlumno.ShowDialog();
            CargarResultados();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvListaAlumnos.SelectedRows.Count == 0)//si no se selecciono ninguna fila
                return;

            var alumnoId = Convert.ToInt32(dgvListaAlumnos.SelectedRows[0].Cells["AlumnoId"].Value);
            var frmeditAlumno = new frmEditAlumno(alumnoId);
            frmeditAlumno.ShowDialog();
            CargarResultados();
            
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvListaAlumnos.SelectedRows.Count == 0)//si no se selecciono ninguna fila
               // MessageBox.Show("Esta seguro de elimianr este alumno?","Eliminar",MessageBoxButtons.YesNo)!=DialogResult.Yes);
                return;


            var alumnoId = Convert.ToInt32(dgvListaAlumnos.SelectedRows[0].Cells["AlumnoId"].Value);

            context.Alumno.Remove(context.Alumno.Where(x => x.AlumnoId == alumnoId).First());
            context.SaveChanges();

            //var alumno = context.Alumno.Find(alumnoId);
            //context.Alumno.Remove(alumno);
            CargarResultados();
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarResultados();
        }
    }
}
