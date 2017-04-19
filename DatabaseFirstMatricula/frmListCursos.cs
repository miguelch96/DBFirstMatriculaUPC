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
    public partial class frmListCursos : Form
    {
        public MatriculaEntities context { get; set; }
        public frmListCursos()
        {     
            InitializeComponent();

            dgvListaCursos.AutoGenerateColumns = false;
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
            context = new MatriculaEntities();
            if (cbbTipoFiltro.SelectedIndex==0)//Busqueda por codigo
            {
                var resultados = context.Curso.Where(x => x.Codigo.Contains(filtro))
                .ToList();
                dgvListaCursos.DataSource = resultados;
            }

            else if(cbbTipoFiltro.SelectedIndex==1)//Busqueda por nombre
            {
                var resultados = context.Curso.Where(x => x.Nombre.Contains(filtro)).ToList();
                dgvListaCursos.DataSource = resultados;
            }

            else // Tipo filtro vacio
            {
                var resultados = context.Curso.Where(x => x.Codigo.Contains(filtro) || x.Nombre.Contains(filtro))
               .ToList();
                dgvListaCursos.DataSource = resultados;
            }
        }

        private void btnFiltrar_Click(object sender, EventArgs e)
        {
            CargarResultados();
        }

        private void btnEditar_Click(object sender, EventArgs e)
        {
            if (dgvListaCursos.SelectedRows.Count == 0)//si no se selecciono ninguna fila
                return;

            var cursoId = Convert.ToInt32(dgvListaCursos.SelectedRows[0].Cells["CursoId"].Value);
            var frmeditCurso = new frmEditCurso(cursoId);
            frmeditCurso.ShowDialog();
            CargarResultados();
          
        }

        private void btnEliminar_Click(object sender, EventArgs e)
        {
            if (dgvListaCursos.SelectedRows.Count == 0)//si no se selecciono ninguna fila
                return;

            var cursoId = Convert.ToInt32(dgvListaCursos.SelectedRows[0].Cells["CursoId"].Value);

            context.Curso.Remove(context.Curso.Where(x => x.CursoId == cursoId).First());
            context.SaveChanges();
            CargarResultados();
        }

        private void btnAgregar_Click(object sender, EventArgs e)
        {
            var frmeditCurso = new frmEditCurso(null);
            frmeditCurso.ShowDialog();
            CargarResultados();
        }

        private void frmListCursos_Load(object sender, EventArgs e)
        {

        }

        private void dgvListaCursos_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
