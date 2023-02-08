using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;

namespace crud.prueba1
{
    public class Conexion
    {
        public static SqlConnection conectar()
        {
            SqlConnection con = new SqlConnection("Server=FERNANDOLEAL;DataBase=crud;integrated security=true;");
            con.Open(); 
            return con; 
        }
    }
}
