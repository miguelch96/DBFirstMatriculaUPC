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
    public partial class frmPrincipal : Form
    {
        public frmPrincipal()
        {
            InitializeComponent();
        }

        private void frmPrincipal_Load(object sender, EventArgs e)
        {
            //context que trae,almacena y envia datos a la BBDD
            var context = new MatriculaEntities();

            //nuevo alumno
            var alumno = new Alumno()
            {
                Codigo = "200511477",
                Nombre = "Marco",
                Apellido = "Bruggmann",
                Estado = "ACT"
            };

            //añadimos alumno al context
            context.Alumno.Add(alumno);
            //hace lo mismo del commit
            context.SaveChanges();


            
            alumno.Nombre = "Miguel";
            alumno.Codigo = "201412510";
            context.SaveChanges();
            
        }

        private void cursosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var frmlistCursos = new frmListCursos();
            frmlistCursos.Show();

        }
    }
}
