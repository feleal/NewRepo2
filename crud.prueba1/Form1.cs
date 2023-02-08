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

namespace crud.prueba1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            Conexion.conectar(); // llama a la clase conexion y al metodo de acceso
            MessageBox.Show(" estas conectado hijo..");

            dataGridView1.DataSource = Consulta();
        }

        public DataTable Consulta()
        {
            Conexion.conectar();
            DataTable dt = new DataTable();
            string consultar = "select * from empleados";
            SqlCommand cmd = new SqlCommand(consultar, Conexion.conectar());
            SqlDataAdapter data = new SqlDataAdapter(cmd);
            data.Fill(dt);
            return dt;
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            // marcar los registros desde el datagridviw
            // carga los txt para ver los resultados de las mismas se marca pero esta lento
            txtnombre.Text = dataGridView1.CurrentRow.Cells[0].Value.ToString();
            txtapellido.Text = dataGridView1.CurrentRow.Cells[1].Value.ToString();
            txtcodigo.Text = dataGridView1.CurrentRow.Cells[2].Value.ToString();
            txtdireccion.Text = dataGridView1.CurrentRow.Cells[3].Value.ToString();


        }

        private void btninsertar_Click(object sender, EventArgs e)
        {
            Conexion.conectar();
            string insertar = "insert into empleados (nombre,apellido,direccion) values (@nombre,@apellido,@direccion)";
            SqlCommand cmd = new SqlCommand(insertar, Conexion.conectar());
            //cmd.Parameters.AddWithValue("@codigo", txtcodigo.Text);
            cmd.Parameters.AddWithValue("@nombre",txtnombre.Text);
            cmd.Parameters.AddWithValue("@apellido",txtapellido.Text);
            cmd.Parameters.AddWithValue("@direccion",txtdireccion.Text);

            cmd.ExecuteNonQuery();// ejecuta
            MessageBox.Show("Se cargo el empleado...");
            dataGridView1.DataSource= Consulta();// muestra en el data source

            // el codigo no se carga pues es autoincremental, pero sino si se carga bien
        }

        private void btnactualizar_Click(object sender, EventArgs e)
        {
            // lamado a la cadena de conecion

            Conexion.conectar();
            string update = " UPDATE  empleados set nombre=@nombre,apellido=@apellido,direccion=@direccion where nombre=@nombre";
            SqlCommand cmd = new SqlCommand(update, Conexion.conectar());
            //cmd.Parameters.AddWithValue("@codigo", txtcodigo.Text);
            cmd.Parameters.AddWithValue("@nombre", txtnombre.Text);
            cmd.Parameters.AddWithValue("@apellido", txtapellido.Text);
            cmd.Parameters.AddWithValue("@direccion", txtdireccion.Text);
            cmd.ExecuteNonQuery();// ejecuta
            MessageBox.Show("Se ACTUALIZO el empleado..."); // hasta aca si anda... 
            dataGridView1.DataSource = Consulta();
            // no esta entrando la carga de las cosas nuevas no se si cerrando se hace de nuevo
        }

        private void btnBorrar_Click(object sender, EventArgs e)
        {
            Conexion.conectar();
            string eliminar = "delete from empleados where codigo=@codigo";
            SqlCommand cmd = new SqlCommand(eliminar,Conexion.conectar());
            cmd.Parameters.AddWithValue("@codigo",txtcodigo.Text);
            cmd.ExecuteNonQuery();
            MessageBox.Show("El emplado se ELIMINO correctamente....");
            dataGridView1.DataSource = Consulta();
            // sigue sin andar la actualizacion del grid
        }

        private void btnlimpiarceldas_Click(object sender, EventArgs e)
        {
            txtapellido.Clear(); 
            txtdireccion.Clear();  
            txtcodigo.Clear(); 
            txtnombre.Clear();
            txtnombre.Focus();
        }
    }
}
