
using app_da_foto.Domain.Model;
using System.Data;
using System.Data.SqlClient;

namespace Repositorio
{
    public class DalHelper
    {
        protected static string GetStringConexão()
        {
            return "Server=PC-PMM\\SQLEXPRESS;Database=APPDAFOTO;User Id=sa;Password=072068";
        }

        public static List<Fotografo> BuscarFotografos()
        {
            List<Fotografo> _fotografos = new List<Fotografo>();

            using (SqlConnection conn = new SqlConnection(GetStringConexão()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT IDFOTOGRAFO, NOME, ESPECIALIDADE FROM FOTOGRAFO", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                var fotografo = new Fotografo();
                                fotografo.Id = Convert.ToInt32(reader["IDFOTOGRAFO"]);
                                fotografo.Nome = reader["NOME"].ToString();
                                fotografo.Especialidade = reader["ESPECIALIDADE"].ToString();
                                _fotografos.Add(fotografo);
                            }
                        }
                        return _fotografos;
                    }
                }
            }
        }

        public static Fotografo BuscarFotografoPorId(int id)
        {
            Fotografo fotografo = null;

            using (SqlConnection conn = new SqlConnection(GetStringConexão()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT IDFOTOGRAFO, NOME, ESPECIALIDADE FROM FOTOGRAFO WHERE IDFOTOGRAFO=" + id, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                fotografo = new Fotografo();
                                fotografo.Id = Convert.ToInt32(reader["IDFOTOGRAFO"]);
                                fotografo.Nome = reader["NOME"].ToString();
                                fotografo.Especialidade = reader["ESPECIALIDADE"].ToString();
                            }
                        }
                        return fotografo;
                    }
                }
            }
        }

        public static Fotografo BuscarFotografo(string email, string senha)
        {
            Fotografo fotografo = null;

            using (SqlConnection conn = new SqlConnection(GetStringConexão()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT F.IDFOTOGRAFO, F.NOME, F.ESPECIALIDADE, F.NASCIMENTO, F.SEXO, F.EMAIL, F.SENHA FROM FOTOGRAFO F WHERE F.EMAIL = '" + email +"' AND F.SENHA = '" + senha + "'" , conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                fotografo = new Fotografo();
                                fotografo.Id = Convert.ToInt32(reader["IDFOTOGRAFO"]);
                                fotografo.Nome = reader["NOME"].ToString();
                                fotografo.Especialidade = reader["ESPECIALIDADE"].ToString();
                                fotografo.Nascimento = (DateTime)reader["NASCIMENTO"];
                                fotografo.Sexo = reader["SEXO"].ToString();
                                fotografo.Email = reader["EMAIL"].ToString();
                                fotografo.Senha = reader["SENHA"].ToString();
                            }
                        }
                        return fotografo;
                    }
                }
            }
        }

        public static int AdicionarFotografo(Fotografo fotografo)
        {
            int reg = 0;
            using (SqlConnection conn = new SqlConnection(GetStringConexão()))
            {
                string sql = "INSERT INTO FOTOGRAFO (NOME, ESPECIALIDADE, SEXO, NASCIMENTO, EMAIL, SENHA) VALUES (@NOME, @ESPECIALIDADE, @SEXO, @NASCIMENTO, @EMAIL, @SENHA)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@NOME", fotografo.Nome);
                    cmd.Parameters.AddWithValue("@ESPECIALIDADE", fotografo.Especialidade);
                    cmd.Parameters.AddWithValue("@SEXO", fotografo.Sexo.ToUpper());
                    cmd.Parameters.AddWithValue("@NASCIMENTO", fotografo.Nascimento);
                    cmd.Parameters.AddWithValue("@EMAIL", fotografo.Email);
                    cmd.Parameters.AddWithValue("@SENHA", fotografo.Senha);

                    conn.Open();
                    reg = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return reg;
            }
        }

        public static int AtualizarFotografo(Fotografo fotografo)
        {
            int reg = 0;
            using (SqlConnection conn = new SqlConnection(GetStringConexão()))
            {
                string sql = "UPDATE FOTOGRAFO SET NOME=@NOME, ESPECIALIDADE=@ESPECIALIDADE, SEXO=@SEXO, NASCIMENTO=@NASCIMENTO, SENHA=@SENHA WHERE IDFOTOGRAFO = " + fotografo.Id;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@NOME", fotografo.Nome);
                    cmd.Parameters.AddWithValue("@ESPECIALIDADE", fotografo.Especialidade);
                    cmd.Parameters.AddWithValue("@SEXO", fotografo.Sexo.ToUpper());
                    cmd.Parameters.AddWithValue("@NASCIMENTO", fotografo.Nascimento);
                    cmd.Parameters.AddWithValue("@SENHA", fotografo.Senha);

                    conn.Open();
                    reg = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return reg;
            }
        }

        public static int DeletarFotografo(int id)
        {
            int reg = 0;
            using (SqlConnection conn = new SqlConnection(GetStringConexão()))
            {
                string sql = "DELETE FROM FOTOGRAFO WHERE IDFOTOGRAFO =" + id;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IDFOTOGRAFO", id);

                    conn.Open();
                    reg = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return reg;
            }
        }
    }
}
