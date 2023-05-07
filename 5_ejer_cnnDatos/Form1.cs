using System;
using System.Data;
using System.Data.SqlClient;
using System.Net;
using System.Runtime.CompilerServices;

namespace _5_ejer_cnnDatos
{
    public partial class Form1 : Form
    {
        static string cadenaDeConexion = string.Empty;
        static SqlConnection conexion = null;
        static SqlDataAdapter dataAdapter = null;
        static DataTable dt = null;
        static SqlCommand sqlCom = null;



        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {

            string nombres = textBox2.Text;
            string apellidos = textBox3.Text;
            String dni = textBox1.Text;
            int edad = Int32.Parse(textBox4.Text);
            String facultad = comboBox1.Text;


            InsertarNuevoRegistro(dni, nombres, apellidos, edad, facultad);

            textBox1.Text = "";
            textBox2.Text = "";
            textBox3.Text = "";
            textBox4.Text = "";
            comboBox1.SelectedIndex = -1;

            textBox2.Focus();


            //MessageBox.Show(nombres + "-" + apellidos + "- " + dni + "-" + "-" + edad + "-" + facultad) ;


        }

        private void Form1_Load(object sender, EventArgs e)
        {

            String[] facultades = new string[8];

            facultades[0] = "FACULTAD DE CIENCIAS AGROPECUARIAS";
            facultades[1] = "FACULTAD DE CIENCIAS ECONOMICAS";
            facultades[2] = "FACULTAD DE CIENCIAS FISICAS Y MATEMATICAS";
            facultades[3] = "FACULTAD DE CIENCIAS SOCIALES";
            facultades[4] = "FACULTAD DE CIENCIAS DERECHO Y CIENCIAS POLITICAS";
            facultades[5] = "FACULTAD DE CIENCIAS ENFERMERIA";
            facultades[6] = "FACULTAD DE CIENCIAS ESTOMATOLOGIA";
            facultades[7] = "FACULTAD DE CIENCIAS FARMACIA Y BIOQUIMICA";


            foreach (string i in facultades)
            {
                comboBox1.Items.Add(i);

            }

            ConectarASQLServer();
            mostrarDatos();


        }

        private static void ConectarASQLServer()
        {

            try
            {

                cadenaDeConexion = "server=DESKTOP-A56BFI3 ; database=UNIVERSIDAD ; integrated security = true";

                conexion = new SqlConnection(cadenaDeConexion);
                conexion.Open();


            }


            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);

            }

        }


        private void mostrarDatos()
        {


            try
            {

                string sqlQuery = "SELECT * FROM PROFESORES";
                dataAdapter = new SqlDataAdapter(sqlQuery, conexion);
                dt = new DataTable();
                dataAdapter.Fill(dt);
                dataGridView1.DataSource = null;
                dataGridView1.DataSource = dt;


            }

            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }


        }


        private void InsertarNuevoRegistro(string dni, string nombres, string apellidos, int edad, string facultad)
        {
            try
            {
                string sqlQuery = "INSERT INTO PROFESORES(DNI,NOMBRES,APELLIDOS,EDAD,FACULTAD) VALUES (@DNI,@NOMBRES,@APELLIDOS,@EDAD,@FACULTAD)";
                sqlCom = new SqlCommand(sqlQuery, conexion);
                sqlCom.Parameters.AddWithValue("DNI", dni);
                sqlCom.Parameters.AddWithValue("NOMBRES", nombres);
                sqlCom.Parameters.AddWithValue("APELLIDOS", apellidos);
                sqlCom.Parameters.AddWithValue("EDAD", edad);
                sqlCom.Parameters.AddWithValue("FACULTAD", facultad);
                sqlCom.ExecuteNonQuery();
                MessageBox.Show("Se registro Correctamente.");
                mostrarDatos();

            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }


        private void EliminarRegistro(int prof_id)
        {
            try
            {


                string sqlQuery = "DELETE FROM PROFESORES WHERE PROF_ID = @prof_id";
                sqlCom = new SqlCommand(sqlQuery, conexion);
                sqlCom.Parameters.AddWithValue("prof_id", prof_id);
                sqlCom.ExecuteNonQuery();
                MessageBox.Show("Se elimino Correctamente.");
                mostrarDatos();

            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }
        }




        private static void CerrarConexion()
        {

            try
            {
                conexion.Close();
            }
            catch (SqlException ex)
            {

                MessageBox.Show(ex.Message);
            }

        }


        private void button2_Click(object sender, EventArgs e)
        {

            if (dataGridView1.SelectedRows.Count > 0)
            {

                int regElim = 0;


                regElim = Int32.Parse(dataGridView1.Rows[dataGridView1.CurrentRow.Index].Cells[0].Value.ToString());

                //MessageBox.Show(regElim.ToString());

                EliminarRegistro(regElim);

            }
            else
            {

                MessageBox.Show("No se ha seleccionado ningun elemento");
            }






        }
    }
}