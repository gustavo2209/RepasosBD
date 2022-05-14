using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using System.Data.SqlClient;

namespace RepasosBD
{
    public partial class Form1 : Form
    {
        private SqlConnection cn;

        public Form1()
        {
            InitializeComponent();
            cn = new SqlConnection("Data Source=(local);Initial Catalog=parainfo;Integrated Security=SSPI;");
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM alumnos", cn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView1.DataSource = ds.Tables[0];
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand();
            cm.Connection = cn;
            cm.CommandText = "INSERT INTO alumnos VALUES('eeeeee', 15, 16, 17)";
            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Operación exitosa");
        }

        private void button3_Click(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand();
            cm.Connection = cn;
            cm.CommandText = "DELETE FROM alumnos WHERE idalumno=6";
            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Operación exitosa");
        }

        private void button4_Click(object sender, EventArgs e)
        {
            SqlCommand cm = new SqlCommand();
            cm.Connection = cn;
            cm.CommandText = "UPDATE alumnos SET nombre = 'yyyyyy' WHERE idalumno = 5";
            cn.Open();
            cm.ExecuteNonQuery();
            cn.Close();
            MessageBox.Show("Operación exitosa");
        }

        private void button5_Click(object sender, EventArgs e)
        {
            // CANTIDAD DE FILAS SELECCIONADAS
            int fils_sel = dataGridView1.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if(fils_sel > 0)
            {
                // TOMAR UN DATO (SEGUNDA COLUMNA) DE LA FILA
                MessageBox.Show("" + dataGridView1.SelectedRows[0].Cells[1].Value);
            }
            else
            {
                MessageBox.Show("Seleccione fila");
            }
        }

        public void PoblarCombo()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT idalumno, nombre FROM alumnos", cn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            comboBox1.DataSource = ds.Tables[0];
            comboBox1.ValueMember = "idalumno";
            comboBox1.DisplayMember = "nombre";
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            PoblarCombo();
        }
    }
}
