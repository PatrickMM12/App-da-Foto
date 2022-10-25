
using app_da_foto.Domain.Model;
using App_da_Foto.Models;
using System.Data;
using System.Data.SqlClient;

namespace Repositorio
{
    public class DalHelper
    {
        public static T ConvertFromDBVal<T>(object obj)
        {
            if (obj == null || obj == DBNull.Value)
            {
                return default(T); // returns the default value for the type
            }
            else
            {
                return (T)obj;
            }
        }

        protected static string GetStringConection()
        {
            return "Data Source=appdafotoapidbserver.database.windows.net;Initial Catalog=APPDAFOTO;User Id=USUARIO;Password=SENHA";
        }

        public static List<Fotografo> Fotografos()
        {
            List<Fotografo> _fotografos = new();

            using SqlConnection conn = new(GetStringConection());
            conn.Open();
            using SqlCommand cmd = new("SELECT IDFOTOGRAFO, NOME, ESPECIALIDADE, EMAIL, SENHA FROM FOTOGRAFO", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    var fotografo = new Fotografo
                    {
                        Id = Convert.ToInt32(reader["IDFOTOGRAFO"]),
                        Nome = reader["NOME"].ToString(),
                        Especialidade = reader["ESPECIALIDADE"].ToString(),
                        //Nascimento = reader["NASCIMENTO"]?.ToString();
                        //Sexo = reader["SEXO"].ToString();
                        Email = reader["EMAIL"].ToString(),
                        Senha = reader["SENHA"].ToString()
                    };
                    _fotografos.Add(fotografo);
                }
            }
            return _fotografos;
        }

        public static List<Fotografo> BuscarFotografos(string nome, string especialidade)
        {
            if (nome == null)
            {
                nome = "";
            }
            
            if (especialidade == null)
            {
                especialidade = "";
            }

            List<Fotografo> _fotografos = new();

            using SqlConnection conn = new(GetStringConection());
            conn.Open();
            using SqlCommand cmd = new("SELECT IDFOTOGRAFO, NOME, ESPECIALIDADE, EMAIL, SENHA FROM FOTOGRAFO WHERE (NOME LIKE '%" + nome + "%') AND (ESPECIALIDADE LIKE '%" + especialidade + "%')", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    var fotografo = new Fotografo
                    {
                        Id = Convert.ToInt32(reader["IDFOTOGRAFO"]),
                        Nome = reader["NOME"].ToString(),
                        Especialidade = reader["ESPECIALIDADE"].ToString(),
                        //Nascimento = reader["NASCIMENTO"]?.ToString();
                        //Sexo = reader["SEXO"].ToString();
                        Email = reader["EMAIL"].ToString(),
                        Senha = reader["SENHA"].ToString()
                    };
                    _fotografos.Add(fotografo);
                }
            }
            return _fotografos;
        }

        public static Fotografo BuscarFotografoPorId(int id)
        {
            Fotografo fotografo = null;
            try
            {

                using SqlConnection conn = new(GetStringConection());
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT F.IDFOTOGRAFO, F.NOME, F.ESPECIALIDADE, F.SEXO, F.NASCIMENTO, F.EMAIL, F.SENHA " +
                                                       "FROM FOTOGRAFO F " +
                                                       "WHERE F.IDFOTOGRAFO = " + id, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                fotografo = new Fotografo
                                {
                                    Id = Convert.ToInt32(reader["IDFOTOGRAFO"]),
                                    Nome = reader["NOME"]?.ToString(),
                                    Especialidade = reader["ESPECIALIDADE"]?.ToString(),
                                    Nascimento = reader["NASCIMENTO"]?.ToString(),
                                    Sexo = reader["SEXO"]?.ToString(),
                                    Email = reader["EMAIL"]?.ToString(),
                                    Senha = reader["SENHA"]?.ToString()
                                };
                            }
                        }
                        return fotografo;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        public static FotografoCompleto BuscarFotografoCompletoPorId(int id)
        {
            FotografoCompleto fotografoCompleto = null;
            Foto foto = null;

            using SqlConnection conn = new(GetStringConection());
            conn.Open();
            using SqlCommand cmd = new("SELECT F.IDFOTOGRAFO, F.NOME, F.ESPECIALIDADE, F.SEXO, F.NASCIMENTO, F.EMAIL, F.SENHA, " +
                                                "E.IDENDERECO, E.LOGRADOURO, E.NUMERO, E.COMPLEMENTO, E.BAIRRO, E.CIDADE, E.ESTADO, E.CEP, E.LATITUDE, E.LONGITUDE, E.ID_FOTOGRAFO, " +
                                                "C.IDCONTATO, C.TELEFONE, C.TIPOTELEFONE, C.INSTAGRAM, C.ID_FOTOGRAFO, " +
                                                "FT.IDFOTO, FT.NOMEARQUIVO, FT.IMAGEM, FT.MIME, FT.PERFIL, FT.ID_FOTOGRAFO " +
                                        "FROM FOTOGRAFO F " +
                                        "LEFT JOIN ENDERECO E " +
                                        "ON E.ID_FOTOGRAFO = F.IDFOTOGRAFO " +
                                        "LEFT JOIN CONTATO C " +
                                        "ON C.ID_FOTOGRAFO = F.IDFOTOGRAFO " +
                                        "LEFT JOIN FOTO FT " +
                                        "ON FT.ID_FOTOGRAFO = F.IDFOTOGRAFO " +
                                        "WHERE F.IDFOTOGRAFO = " + id, conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    var fotografo = new Fotografo()
                    {
                        Id = Convert.ToInt32(reader["IDFOTOGRAFO"]),
                        Nome = reader["NOME"].ToString(),
                        Especialidade = reader["ESPECIALIDADE"].ToString(),
                        Nascimento = reader["NASCIMENTO"]?.ToString(),
                        Sexo = reader["SEXO"]?.ToString(),
                        Email = reader["EMAIL"].ToString(),
                        Senha = reader["SENHA"].ToString()
                    };

                    var endereco = new Endereco()
                    {
                        Id = Convert.ToInt32(reader["IDENDERECO"]),
                        Logradouro = reader["LOGRADOURO"]?.ToString(),
                        Numero = reader["NUMERO"]?.ToString(),
                        Complemento = reader["COMPLEMENTO"]?.ToString(),
                        Bairro = reader["BAIRRO"]?.ToString(),
                        Cidade = reader["CIDADE"]?.ToString(),
                        Estado = reader["ESTADO"]?.ToString(),
                        Cep = reader["CEP"]?.ToString(),
                        Latitude = reader["LATITUDE"]?.ToString(),
                        Longitude = reader["LONGITUDE"]?.ToString(),
                        IdFotografo = Convert.ToInt32(reader["ID_FOTOGRAFO"])
                    };

                    var contato = new Contato()
                    {
                        Id = Convert.ToInt32(reader["IDCONTATO"]),
                        Telefone = reader["TELEFONE"]?.ToString(),
                        TipoTelefone = reader["TIPOTELEFONE"]?.ToString(),
                        Instagram = reader["INSTAGRAM"]?.ToString(),
                        IdFotografo = Convert.ToInt32(reader["ID_FOTOGRAFO"])
                    };

                    fotografoCompleto = new FotografoCompleto()
                    {
                        Fotografo = fotografo,
                        Endereco = endereco,
                        Contato = contato
                    };
                }
            }
            return fotografoCompleto;
        }

        public static Fotografo BuscarFotografoLogin(string email, string senha)
        {
            Fotografo fotografo = null;

            using (SqlConnection conn = new SqlConnection(GetStringConection()))
            {
                conn.Open();
                using (SqlCommand cmd = new ("SELECT IDFOTOGRAFO, NOME, ESPECIALIDADE, EMAIL, SENHA " +
                                             "FROM FOTOGRAFO " +
                                             "WHERE EMAIL = '" + email + "' AND SENHA = '" + senha + "'", conn))
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
                                //fotografo.Nascimento = reader["NASCIMENTO"]?.ToString();
                                //fotografo.Sexo = reader["SEXO"].ToString();
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
            try
            {
                int reg = 0;
                using (SqlConnection conn = new SqlConnection(GetStringConection()))
                {
                    string sql = "INSERT INTO FOTOGRAFO (NOME, ESPECIALIDADE, EMAIL, SENHA) VALUES (@NOME, @ESPECIALIDADE, @EMAIL, @SENHA)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@NOME", fotografo.Nome);
                        cmd.Parameters.AddWithValue("@ESPECIALIDADE", fotografo.Especialidade);
                        cmd.Parameters.AddWithValue("@EMAIL", fotografo.Email);
                        cmd.Parameters.AddWithValue("@SENHA", fotografo.Senha);

                        conn.Open();
                        reg = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    return reg;
                }
            }
            catch 
            {
                return 0;
            }
        }

        public static int AtualizarFotografo(Fotografo fotografo)
        {
            try
            {
                int reg = 0;
                using (SqlConnection conn = new SqlConnection(GetStringConection()))
                {
                    string sql = "UPDATE FOTOGRAFO " +
                                 "SET NOME=@NOME, ESPECIALIDADE=@ESPECIALIDADE, SEXO=@SEXO, NASCIMENTO=CAST(@NASCIMENTO AS DATE), EMAIL=@EMAIL, SENHA=@SENHA " +
                                 "WHERE IDFOTOGRAFO = " + fotografo.Id;
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
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        public static int DeletarFotografo(int id)
        {
            int reg = 0;
            using (SqlConnection conn = new SqlConnection(GetStringConection()))
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

        public static List<Endereco> BuscarEnderecos()
        {
            List<Endereco> _enderecos = new List<Endereco>();

            using (SqlConnection conn = new SqlConnection(GetStringConection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT IDENDERECO, LOGRADOURO, NUMERO, COMPLEMENTO, BAIRRO, CIDADE, ESTADO, CEP, LATITUDE, LONGITUDE, ID_FOTOGRAFO FROM ENDERECO", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                var endereco = new Endereco();
                                endereco.Id = Convert.ToInt32(reader["IDENDERECO"]);
                                endereco.Logradouro = reader["LOGRADOURO"].ToString();
                                endereco.Numero = reader["NUMERO"].ToString();
                                endereco.Complemento = reader["COMPLEMENTO"].ToString();
                                endereco.Bairro = reader["BAIRRO"].ToString();
                                endereco.Cidade = reader["CIDADE"].ToString();
                                endereco.Estado = reader["ESTADO"].ToString();
                                endereco.Cep = reader["CEP"].ToString();
                                endereco.Latitude = reader["LATITUDE"]?.ToString();
                                endereco.Longitude = reader["LONGITUDE"]?.ToString();
                                endereco.IdFotografo = Convert.ToInt32(reader["ID_FOTOGRAFO"]);
                                _enderecos.Add(endereco);
                            }
                        }
                        return _enderecos;
                    }
                }
            }
        }

        public static List<EnderecoFotografo> BuscarEnderecosPorLocalizao(double latitudeSul, double latitudeNorte, double longitudeOeste, double longitudeLeste)
        {
            List<EnderecoFotografo> _enderecoFotografos = new();

            using SqlConnection conn = new(GetStringConection());
            conn.Open();
            using SqlCommand cmd = new("SELECT IDENDERECO, LOGRADOURO, NUMERO, COMPLEMENTO, BAIRRO, CIDADE, ESTADO, CEP, LATITUDE, LONGITUDE, ID_FOTOGRAFO, " +
                                              "IDFOTOGRAFO, NOME, ESPECIALIDADE, SEXO, NASCIMENTO, EMAIL, SENHA " +
                                       "FROM ENDERECO " +
                                       "LEFT JOIN FOTOGRAFO " +
                                       "ON ID_FOTOGRAFO = IDFOTOGRAFO " +
                                       $"WHERE LATITUDE > {latitudeSul - 0.010} " +
                                       $"AND LATITUDE < {latitudeNorte + 0.010} " +
                                       $"AND LONGITUDE > {longitudeOeste - 0.010} " +
                                       $"AND LONGITUDE < {longitudeLeste + 0.010}", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    var fotografo = new Fotografo()
                    {
                       Id = Convert.ToInt32(reader["IDFOTOGRAFO"]?.ToString()),
                       Nome = reader["NOME"]?.ToString(),
                       Especialidade = reader["ESPECIALIDADE"]?.ToString(),
                       Nascimento = reader["NASCIMENTO"]?.ToString(),
                       Sexo = reader["SEXO"]?.ToString(),
                       Email = reader["EMAIL"]?.ToString(),
                       Senha = reader["SENHA"]?.ToString()
                    };

                    var endereco = new Endereco()
                    {
                        Id = Convert.ToInt32(reader["IDENDERECO"]),
                        Logradouro = reader["LOGRADOURO"].ToString(),
                        Numero = reader["NUMERO"].ToString(),
                        Complemento = reader["COMPLEMENTO"]?.ToString(),
                        Bairro = reader["BAIRRO"].ToString(),
                        Cidade = reader["CIDADE"].ToString(),
                        Estado = reader["ESTADO"].ToString(),
                        Cep = reader["CEP"].ToString(),
                        Latitude = reader["LATITUDE"]?.ToString(),
                        Longitude = reader["LONGITUDE"]?.ToString(),
                        IdFotografo = Convert.ToInt32(reader["ID_FOTOGRAFO"]),
                        Fotografo = fotografo
                    };

                    var enderecoFotografo = new EnderecoFotografo()
                    {
                        Endereco = endereco
                    };
                    _enderecoFotografos.Add(enderecoFotografo);
                }
            }
            return _enderecoFotografos;
        }

        public static Endereco BuscarEnderecoPorId(int id)
        {
            Endereco endereco = null;

            using SqlConnection conn = new(GetStringConection());
            conn.Open();
            using SqlCommand cmd = new("SELECT IDENDERECO, LOGRADOURO, NUMERO, COMPLEMENTO, BAIRRO, CIDADE, ESTADO, CEP, LATITUDE, LONGITUDE, ID_FOTOGRAFO FROM ENDERECO WHERE ID_FOTOGRAFO = " + id, conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    endereco = new Endereco
                    {
                        Id = Convert.ToInt32(reader["IDENDERECO"]),
                        Logradouro = reader["LOGRADOURO"]?.ToString(),
                        Numero = reader["NUMERO"]?.ToString(),
                        Complemento = reader["COMPLEMENTO"]?.ToString(),
                        Bairro = reader["BAIRRO"]?.ToString(),
                        Cidade = reader["CIDADE"]?.ToString(),
                        Estado = reader["ESTADO"]?.ToString(),
                        Cep = reader["CEP"]?.ToString(),
                        Latitude = reader["LATITUDE"]?.ToString(),
                        Longitude = reader["LONGITUDE"]?.ToString(),
                        IdFotografo = Convert.ToInt32(reader["ID_FOTOGRAFO"])
                    };
                }
            }
            return endereco;
        }

        public static int AdicionarEndereco(Endereco endereco)
        {
            try
            {
                int reg = 0;
                using (SqlConnection conn = new SqlConnection(GetStringConection()))
                {
                    string sql = "INSERT INTO ENDERECO (LOGRADOURO, NUMERO, COMPLEMENTO, BAIRRO, CIDADE, ESTADO, CEP, LATITUDE, LONGITUDE, ID_FOTOGRAFO) " +
                                 "VALUES (@LOGRADOURO, @NUMERO, @COMPLEMENTO, @BAIRRO, @CIDADE, @ESTADO, @CEP, CAST(@LATITUDE AS FLOAT), CAST(@LONGITUDE AS FLOAT), @ID_FOTOGRAFO)";
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@LOGRADOURO", endereco.Logradouro);
                        cmd.Parameters.AddWithValue("@NUMERO", endereco.Numero);
                        cmd.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);
                        cmd.Parameters.AddWithValue("@BAIRRO", endereco.Bairro);
                        cmd.Parameters.AddWithValue("@CIDADE", endereco.Cidade);
                        cmd.Parameters.AddWithValue("@ESTADO", endereco.Estado.ToUpper());
                        cmd.Parameters.AddWithValue("@CEP", endereco.Cep);
                        cmd.Parameters.AddWithValue("@LATITUDE", endereco.Latitude);
                        cmd.Parameters.AddWithValue("@LONGITUDE", endereco.Longitude);
                        cmd.Parameters.AddWithValue("@ID_FOTOGRAFO", endereco.IdFotografo);

                        conn.Open();
                        reg = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    return reg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public static int AtualizarEndereco(Endereco endereco)
        {
            try
            {
                int reg = 0;
                using (SqlConnection conn = new SqlConnection(GetStringConection()))
                {
                    string sql = "UPDATE ENDERECO SET LOGRADOURO=@LOGRADOURO, NUMERO=@NUMERO, COMPLEMENTO=@COMPLEMENTO, BAIRRO=@BAIRRO, CIDADE=@CIDADE, ESTADO=@ESTADO, CEP=@CEP, LATITUDE=CAST(@LATITUDE AS FLOAT), LONGITUDE=CAST(@LONGITUDE AS FLOAT), ID_FOTOGRAFO=@ID_FOTOGRAFO WHERE ID_FOTOGRAFO = " + endereco.IdFotografo;
                    using (SqlCommand cmd = new SqlCommand(sql, conn))
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("@LOGRADOURO", endereco.Logradouro);
                        cmd.Parameters.AddWithValue("@NUMERO", endereco.Numero);
                        cmd.Parameters.AddWithValue("@COMPLEMENTO", endereco.Complemento);
                        cmd.Parameters.AddWithValue("@BAIRRO", endereco.Bairro);
                        cmd.Parameters.AddWithValue("@CIDADE", endereco.Cidade);
                        cmd.Parameters.AddWithValue("@ESTADO", endereco.Estado.ToUpper());
                        cmd.Parameters.AddWithValue("@CEP", endereco.Cep);
                        cmd.Parameters.AddWithValue("@LATITUDE", endereco.Latitude);
                        cmd.Parameters.AddWithValue("@LONGITUDE", endereco.Longitude);
                        cmd.Parameters.AddWithValue("@ID_FOTOGRAFO", endereco.IdFotografo);

                        conn.Open();
                        reg = cmd.ExecuteNonQuery();
                        conn.Close();
                    }
                    return reg;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message.ToString());
            }
        }

        public static int DeletarEndereco(int id)
        {
            int reg = 0;
            using (SqlConnection conn = new SqlConnection(GetStringConection()))
            {
                string sql = "DELETE * FROM ENDERECO WHERE IDENDERECO = " + id;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IDENDERECO", id);

                    conn.Open();
                    reg = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return reg;
            }
        }

        public static List<Contato> BuscarContatos()
        {
            List<Contato> _contatos = new List<Contato>();

            using (SqlConnection conn = new SqlConnection(GetStringConection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT IDCONTATO, TELEFONE, TIPOTELEFONE, INSTAGRAM, ACESSOSINSTAGRAM, ID_FOTOGRAFO FROM CONTATO", conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                var contato = new Contato();
                                contato.Id = Convert.ToInt32(reader["IDCONTATO"]);
                                contato.Telefone = reader["TELEFONE"].ToString();
                                contato.TipoTelefone = reader["TIPOTELEFONE"].ToString();
                                contato.Instagram = reader["INSTAGRAM"].ToString();
                                contato.AcessosInstagram = ConvertFromDBVal<int>(Convert.ToInt32(reader["ACESSOSINSTAGRAM"]));
                                contato.IdFotografo = Convert.ToInt32(reader["ID_FOTOGRAFO"]);
                                _contatos.Add(contato);
                            }
                        }
                        return _contatos;
                    }
                }
            }
        }

        public static Contato BuscarContatoPorId(int id)
        {
            Contato contato = null;

            using (SqlConnection conn = new SqlConnection(GetStringConection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT IDCONTATO, TELEFONE, TIPOTELEFONE, INSTAGRAM, ACESSOSINSTAGRAM, ID_FOTOGRAFO FROM CONTATO WHERE ID_FOTOGRAFO = " + id, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                contato = new Contato
                                {
                                    Id = Convert.ToInt32(reader["IDCONTATO"]),
                                    Telefone = reader["TELEFONE"].ToString(),
                                    TipoTelefone = reader["TIPOTELEFONE"].ToString(),
                                    Instagram = reader["INSTAGRAM"].ToString(),
                                    AcessosInstagram = (reader["ACESSOSINSTAGRAM"]) != DBNull.Value ? Convert.ToInt32(reader["ACESSOSINSTAGRAM"]) : 0,
                                    IdFotografo = Convert.ToInt32(reader["ID_FOTOGRAFO"])
                                };
                            }
                        }
                        return contato;
                    }
                }
            }
        }

        public static int AdicionarContato(Contato contato)
        {
            int reg = 0;
            using (SqlConnection conn = new SqlConnection(GetStringConection()))
            {
                string sql = "INSERT INTO CONTATO (TELEFONE, TIPOTELEFONE, INSTAGRAM, ID_FOTOGRAFO) VALUES (@TELEFONE, @TIPOTELEFONE, @INSTAGRAM, @ID_FOTOGRAFO)";
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@TELEFONE", contato.Telefone);
                    cmd.Parameters.AddWithValue("@TIPOTELEFONE", contato.TipoTelefone.ToUpper());
                    cmd.Parameters.AddWithValue("@INSTAGRAM", contato.Instagram);
                    cmd.Parameters.AddWithValue("@ACESSOSINSTAGRAM", contato.AcessosInstagram);
                    cmd.Parameters.AddWithValue("@ID_FOTOGRAFO", contato.IdFotografo);

                    conn.Open();
                    reg = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return reg;
            }
        }

        public static int AtualizarContato(Contato contato)
        {
            int reg = 0;
            using (SqlConnection conn = new SqlConnection(GetStringConection()))
            {
                string sql = "UPDATE CONTATO SET TELEFONE=@TELEFONE, TIPOTELEFONE=@TIPOTELEFONE, INSTAGRAM=@INSTAGRAM, ACESSOSINSTAGRAM=@ACESSOSINSTAGRAM, ID_FOTOGRAFO=@ID_FOTOGRAFO WHERE ID_FOTOGRAFO = " + contato.IdFotografo;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@TELEFONE", contato.Telefone);
                    cmd.Parameters.AddWithValue("@TIPOTELEFONE", contato.TipoTelefone);
                    cmd.Parameters.AddWithValue("@INSTAGRAM", contato.Instagram);
                    cmd.Parameters.AddWithValue("@ACESSOSINSTAGRAM", contato.AcessosInstagram);
                    cmd.Parameters.AddWithValue("@ID_FOTOGRAFO", contato.IdFotografo);

                    conn.Open();
                    reg = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return reg;
            }
        }

        public static int DeletarContato(int id)
        {
            int reg = 0;
            using (SqlConnection conn = new SqlConnection(GetStringConection()))
            {
                string sql = "DELETE * FROM CONTATO WHERE IDCONTATO = " + id;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IDCONTATO", id);

                    conn.Open();
                    reg = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return reg;
            }
        }

        public static List<Foto> BuscarFotos()
        {
            List<Foto> _fotos = new();

            using SqlConnection conn = new(GetStringConection());
            conn.Open();
            using SqlCommand cmd = new("SELECT IDFOTO, NOMEARQUIVO, IMAGEM, MIME, HORAUPLOAD, PERFIL, ID_FOTOGRAFO FROM FOTO", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    var foto = new Foto
                    {
                        Id = Convert.ToInt32(reader["IDFOTO"]),
                        NomeArquivo = reader["NOMEARQUIVO"].ToString(),
                        Imagem = (byte[])reader["IMAGEM"],
                        MIME = reader["MIME"].ToString(),
                        Perfil = reader["PERFIL"].ToString(),
                        IdFotografo = Convert.ToInt32(reader["ID_FOTOGRAFO"])
                    };
                    _fotos.Add(foto);
                }
            }
            return _fotos;
        }

        public static Foto BuscarFotoPerfil(int id)
        {
            Foto _foto = new();

            using SqlConnection conn = new(GetStringConection());
            conn.Open();
            using SqlCommand cmd = new("SELECT IDFOTO, NOMEARQUIVO, IMAGEM, MIME, HORAUPLOAD, PERFIL, ID_FOTOGRAFO " +
                                       "FROM FOTO " +
                                       $"WHERE ID_FOTOGRAFO = {id} AND PERFIL = 'S'", conn);
            using SqlDataReader reader = cmd.ExecuteReader();
            if (reader != null)
            {
                while (reader.Read())
                {
                    var foto = new Foto
                    {
                        Id = Convert.ToInt32(reader["IDFOTO"]),
                        NomeArquivo = reader["NOMEARQUIVO"].ToString(),
                        Imagem = (byte[])reader["IMAGEM"],
                        MIME = reader["MIME"].ToString(),
                        Perfil = reader["PERFIL"].ToString(),
                        IdFotografo = Convert.ToInt32(reader["ID_FOTOGRAFO"])
                    };
                    _foto = foto;
                }
            }
            return _foto;
        }

        public static List<Foto> BuscarFotoPorId(int id)
        {
            List<Foto> fotos = new();

            using (SqlConnection conn = new SqlConnection(GetStringConection()))
            {
                conn.Open();
                using (SqlCommand cmd = new SqlCommand("SELECT IDFOTO, NOMEARQUIVO, IMAGEM, MIME, HORAUPLOAD, PERFIL, ID_FOTOGRAFO FROM FOTO WHERE ID_FOTOGRAFO = " + id, conn))
                {
                    using (SqlDataReader reader = cmd.ExecuteReader())
                    {
                        if (reader != null)
                        {
                            while (reader.Read())
                            {
                                Foto foto = new()
                                {
                                    Id = Convert.ToInt32(reader["IDFOTO"]),
                                    NomeArquivo = reader["NOMEARQUIVO"].ToString(),
                                    Imagem = (byte[])reader["IMAGEM"],
                                    MIME = reader["MIME"].ToString(),
                                    Perfil = reader["PERFIL"].ToString(),
                                    IdFotografo = Convert.ToInt32(reader["ID_FOTOGRAFO"])
                                };
                                fotos.Add(foto);
                            }
                        }
                        return fotos;
                    }
                }
            }
        }

        public static int AdicionarFoto(Foto foto)
        {
            int reg = 0;
            using SqlConnection conn = new(GetStringConection());
            string sql = "INSERT INTO FOTO (NOMEARQUIVO, IMAGEM, PERFIL, ID_FOTOGRAFO) VALUES (@NOMEARQUIVO, @IMAGEM, @PERFIL, @ID_FOTOGRAFO)";
            using (SqlCommand cmd = new(sql, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@NOMEARQUIVO", foto.NomeArquivo);
                cmd.Parameters.AddWithValue("@IMAGEM", foto.Imagem);
                cmd.Parameters.AddWithValue("@PERFIL", foto.Perfil);
                cmd.Parameters.AddWithValue("@ID_FOTOGRAFO", foto.IdFotografo);

                conn.Open();
                reg = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return reg;
        }

        public static int AtualizarFoto(Foto foto)
        {
            int reg = 0;
            using SqlConnection conn = new(GetStringConection());
            string sql = "UPDATE FOTO SET NOMEARQUIVO=@NOMEARQUIVO, IMAGEM=@IMAGEM, PERFIL=@PERFIL, ID_FOTOGRAFO=@ID_FOTOGRAFO WHERE IDFOTO = " + foto.Id;
            using (SqlCommand cmd = new(sql, conn))
            {
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@NOMEARQUIVO", foto.NomeArquivo);
                cmd.Parameters.AddWithValue("@IMAGEM", foto.Imagem);
                cmd.Parameters.AddWithValue("@PERFIL", foto.Perfil);
                cmd.Parameters.AddWithValue("@ID_FOTOGRAFO", foto.IdFotografo);

                conn.Open();
                reg = cmd.ExecuteNonQuery();
                conn.Close();
            }
            return reg;
        }

        public static int DeletarFoto(int id)
        {
            int reg = 0;
            using (SqlConnection conn = new SqlConnection(GetStringConection()))
            {
                string sql = "DELETE * FROM FOTO WHERE IDFOTO = " + id;
                using (SqlCommand cmd = new SqlCommand(sql, conn))
                {
                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@IDFOTO", id);

                    conn.Open();
                    reg = cmd.ExecuteNonQuery();
                    conn.Close();
                }
                return reg;
            }
        }
    }
}
