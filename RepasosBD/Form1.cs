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
            consulta();
        }

        public void consulta()
        {
            SqlDataAdapter da = new SqlDataAdapter("SELECT * FROM alumnos", cn);
            DataSet ds = new DataSet();
            da.Fill(ds);

            dataGridView2.DataSource = ds.Tables[0];
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

            for(int i=0; i<=20; i++)
            {
                comboBox2.Items.Add("" + i);
                comboBox3.Items.Add("" + i);
                comboBox4.Items.Add("" + i);
            }
        }

        private void LimpiaDatos()
        {
            textBox1.Text = "";
            textBox2.Text = "";
            comboBox2.SelectedIndex = 0;
            comboBox3.SelectedIndex = 0;
            comboBox4.SelectedIndex = 0;
        }

        private void dataGridView2_Click(object sender, EventArgs e)
        {
            int fils_sel = dataGridView2.Rows.GetRowCount(DataGridViewElementStates.Selected);

            if (fils_sel > 0)
            {
                textBox1.Text = dataGridView2.SelectedRows[0].Cells[0].Value.ToString();
                textBox2.Text = dataGridView2.SelectedRows[0].Cells[1].Value.ToString();
                comboBox2.SelectedIndex = Convert.ToInt16(dataGridView2.SelectedRows[0].Cells[2].Value.ToString());
                comboBox3.SelectedIndex = Convert.ToInt16(dataGridView2.SelectedRows[0].Cells[3].Value.ToString());
                comboBox4.SelectedIndex = Convert.ToInt16(dataGridView2.SelectedRows[0].Cells[4].Value.ToString());
            }
            else
            {
                MessageBox.Show("Seleccione fila");
            }
        }

        private void button8_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim().Length > 0)
            {
                SqlCommand cm = new SqlCommand();
                cm.Connection = cn;
                cm.CommandText = "INSERT INTO alumnos VALUES('" + 
                                    textBox2.Text + "', " + 
                                    comboBox2.SelectedIndex + ", " + 
                                    comboBox3.SelectedIndex + ", " + 
                                    comboBox4.SelectedIndex + ")";
                //MessageBox.Show(cm.CommandText); // PARA SABER LOS POSIBLES ERRORES AL HACER LA CONSULTA
                cn.Open();
                cm.ExecuteNonQuery();
                cn.Close();

                consulta();
                LimpiaDatos();
            }
            else
            {
                MessageBox.Show("Digite nombre de Alumno");
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Length > 0)
            {
                SqlCommand cm = new SqlCommand();
                
                cm.Connection = cn;
                cm.CommandText = "DELETE FROM alumnos WHERE idalumno = " + textBox1.Text;
                cn.Open();
                cm.ExecuteNonQuery();
                cn.Close();

                consulta();
                LimpiaDatos();
            }
            else
            {
                MessageBox.Show("Seleccione alumno a retirar");
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            if (textBox2.Text.Trim().Length > 0)
            {
                SqlCommand cm = new SqlCommand();

                cm.Connection = cn;
                cm.CommandText = "UPDATE alumnos SET nombre = '" + textBox2.Text + "', " +
                                 "nota1 = " + comboBox2.SelectedIndex + ", " +
                                 "nota2 = " + comboBox3.SelectedIndex + ", " +
                                 "nota3 = " + comboBox4.SelectedIndex + 
                                 " WHERE idalumno = " + textBox1.Text;
                //MessageBox.Show(cm.CommandText); PARA SABER LOS POSIBLES ERRORES AL HACER LA CONSULTA
                cn.Open();
                cm.ExecuteNonQuery();
                cn.Close();

                consulta();
                LimpiaDatos();
            }
            else
            {
                MessageBox.Show("Seleccione alumno para actualizar datos");
            }
        }
    }
}
