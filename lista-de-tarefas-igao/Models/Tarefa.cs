using System.ComponentModel.DataAnnotations;
using System;
using System.Data;
using System.Data.SqlClient;

namespace lista_de_tarefas_igao.Models
{
    public class Tarefa
    {
        //Campos da classe
        [Key]
        public int Id { get; set; }
        [Required]
        public string? Nome { get; set; }
        public string? Descricao { get; set; }
        public bool Completa { get; set; }
    

        // Construtor vazio
        public Tarefa() { }

        // Construtor que recebe variáveis da view
        public Tarefa(string nome, string descricao, bool completa)
        {
            Nome = nome;
            Descricao = descricao;
            Completa = completa;
        }

        //Método para pegar as tarefas 
        public List<Tarefa> SelectTarefas() 
        {
            List<Tarefa> tarefas = new List<Tarefa>();
            SqlConnection Conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            try
            {
                using(SqlCommand sql =  new SqlCommand("dbo.SelectTarefas", Conexao))
                {
                    sql.CommandType = CommandType.StoredProcedure;

                    if (Conexao.State == ConnectionState.Closed)
                    {
                        Conexao.Open();
                    }
                    using(SqlDataReader reader = sql.ExecuteReader())
                    {
                        while(reader.Read())
                        {
                            Tarefa tarefa = new Tarefa 
                            {
                                Id = Convert.ToInt32(reader["Id"]),
                                Nome = reader["Nome"].ToString(),
                                Descricao = reader["Descricao"].ToString(),
                                Completa = Convert.ToBoolean(reader["Completa"])
                            };
                            
                            tarefas.Add(tarefa);
                        }
                    }

                }
            }
            catch (Exception)
            {
                if (Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
                //throw;
            }
            finally
            {
                if (Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }
            return tarefas;
        }
        //Método para pegar uma tarefa pelo Id
        public Tarefa SelectTarefaById(int id) 
        {
            Tarefa tarefa = new Tarefa();
            SqlConnection Conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            try
            {
                using(SqlCommand sql = new SqlCommand("dbo.SelectTarefaById", Conexao))
                {
                    sql.CommandType = CommandType.StoredProcedure;
                    if (Conexao.State == ConnectionState.Closed)
                    {
                        Conexao.Open();
                    }
                    sql.Parameters.AddWithValue("@TarefaId", id);

                    SqlDataReader reader = sql.ExecuteReader();

                    if (reader.Read())
                    {
                        tarefa = new Tarefa
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            Nome = reader["Nome"].ToString(),
                            Descricao = reader["Descricao"].ToString(),
                            Completa = Convert.ToBoolean(reader["Completa"])                            
                        };
                    }
                }
            }
            catch (Exception)
            {
                if (Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
                //throw;
            }
            finally
            {
                if (Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }
            return tarefa;
        }
        //Método para selecionar as tarefas completas
        public List<Tarefa> SelectTarefasCompletas(bool completa)
        {
            List<Tarefa> tarefas = new List<Tarefa>();
            SqlConnection conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            try
            {
                using(SqlCommand sql = new SqlCommand("dbo.SelectTarefasCompletas", conexao))
                {
                    sql.CommandType = CommandType.StoredProcedure;
                    sql.Parameters.AddWithValue("@Completa", completa ?  1 : 0);

                    if (conexao.State == ConnectionState.Closed)
                    {
                        conexao.Open();
                    }
                    using(SqlDataReader reader = sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tarefa tarefa = new Tarefa
                            {
                                Id = Convert.ToInt32(reader["ID"]),
                                Nome = reader["Nome"].ToString(),
                                Descricao = reader["Descricao"].ToString(),
                                Completa = Convert.ToBoolean(reader["Completa"])
                            };

                            tarefas.Add(tarefa);
                        }
                    }

                }
            }
            catch (Exception)
            {
                if (conexao.State == ConnectionState.Open)
                {
                    conexao.Close();
                }
                //throw;
            }
            finally
            {
                if (conexao.State == ConnectionState.Open)
                {
                    conexao.Close();
                }
            }

            return tarefas;
        }
        //Método para selecionar as tarefas dentre as completas filtradas com uma string de busca
        public List<Tarefa> SelectTarefasCompletasFiltradas(bool completa, string nome)
        {
            List<Tarefa> tarefas = new List<Tarefa>();
            SqlConnection conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);

            try
            {
                using(SqlCommand sql = new SqlCommand("dbo.SelectTarefasCompletasFiltradas", conexao))
                {
                    sql.CommandType = CommandType.StoredProcedure;

                    sql.Parameters.AddWithValue("@Completa", completa ? 1 : 0);

                    if (!string.IsNullOrEmpty(nome))
                    {
                        sql.Parameters.AddWithValue("@SearchString", nome);
                    }
                    else
                    {
                        sql.Parameters.AddWithValue("@SearchString", "");
                    }
                    if (conexao.State == ConnectionState.Closed)
                    {
                        conexao.Open();
                    }
                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tarefa tarefa = new Tarefa
                            {
                                Id = Convert.ToInt32(reader["id"]),
                                Nome = reader["Nome"].ToString(),
                                Descricao = reader["Descricao"].ToString(),
                                Completa = Convert.ToBoolean(reader["Completa"])
                            };

                            tarefas.Add(tarefa);
                        }
                    }
                }
            }
            catch (Exception)
            {
                if (conexao.State == ConnectionState.Open)
                {
                    conexao.Close();
                }
                //throw;
            }
            finally
            {
                if (conexao.State == ConnectionState.Open)
                {
                    conexao.Close();
                }
            }

            return tarefas;
        }
        //Método para selecionar as tarefas filtradas com uma string de busca
        public List<Tarefa> SelectTarefasFiltradas(string nome)
        {
            List<Tarefa> tarefas = new List<Tarefa>();
            SqlConnection conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            try
            {
                using(SqlCommand sql = new SqlCommand("dbo.SelectTarefasFiltradas", conexao))
                {
                    sql.CommandType = CommandType.StoredProcedure;
                    if (!string.IsNullOrEmpty(nome))
                    {
                        sql.Parameters.AddWithValue("@SearchString", nome);
                    }
                    else
                    {
                        sql.Parameters.AddWithValue("@SearchString", "");
                    }
                    if (conexao.State == ConnectionState.Closed)
                    {
                        conexao.Open();
                    }
                    using (SqlDataReader reader = sql.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            Tarefa tarefa = new Tarefa
                            {
                                Id = reader.GetInt32("Id"),
                                Nome = reader.GetString("Nome"),
                                Descricao = reader.GetString("Descricao"),
                                Completa = reader.GetBoolean("Completa")
                            };

                            tarefas.Add(tarefa);
                        }
                    }
                }
            }
            catch (Exception)
            {
                if (conexao.State == ConnectionState.Open) { conexao.Close(); }
                //throw;
            }
            finally
            {
                if(conexao.State == ConnectionState.Open) { conexao.Close(); }
            }
            return tarefas;
        }
        // Método para inserir uma nova tarefa
        public int CreateTarefa(Tarefa tarefa)
        {
            //variável de retorno do Id
            int identificador = default(int);

            //Determina qual a conexão com o Banco de dados a ser usada na variavel Conexao
            SqlConnection Conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            try
            {
                //Cria um novo objeto do tipo SqlCommand que representa uma instrução Transact-SQL ou Stored Procedure para execução em um banco de dados SQL Server
                //Passa como CommandText(Primeiro param) o nome da Procedure, ou o texto da query desejada, e a Connection como a variavel Conexao instanciada acima
                using (SqlCommand sql = new SqlCommand("dbo.CreateTarefa", Conexao)) 
                {
                    //Define a propriedade CommandType do objeto como uma Stored Procedure
                    sql.CommandType = CommandType.StoredProcedure;
                    //Passagem do parâmetro @Nome usando if para evitar erros, caso o valor seja vazio passa o DBNull
                    if (!string.IsNullOrEmpty(tarefa.Nome)) { 
                        sql.Parameters.AddWithValue("@Nome", tarefa.Nome);
                    } else
                    {
                        sql.Parameters.AddWithValue("@Nome", DBNull.Value);
                    }
                    //Passagem do parâmetro @Descricao usando if para evitar erros, caso o valor seja vazio passa o DBNull
                    if (!string.IsNullOrEmpty(tarefa.Descricao))
                    {
                        sql.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
                    }
                    else
                    {
                        sql.Parameters.AddWithValue("@Descricao", DBNull.Value);
                    }
                    //Passagem do parâmetro @Completa usando operador ternário para definir o booleano como bit
                    sql.Parameters.AddWithValue("@Completa", tarefa.Completa ? 1 : 0);
                    //Verifica se a conexão já está aberta, caso não esteja abre a conexão
                    if (Conexao.State == ConnectionState.Closed)
                    {
                        Conexao.Open();
                    }
                    //Pega o retorno da procedure, converte em um número inteiro e armazena na variável identificador
                    identificador = Convert.ToInt32(sql.ExecuteScalar());

                };
            }
            catch (Exception)
            {
                if (Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
                //throw;
            }
            finally
            {
                if (Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }

            return identificador;
        }
        // Método para atualizar uma tarefa por ID
        public void UpdateTarefaById(Tarefa tarefa)
        {
            SqlConnection Conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);

            try
            {
                using (SqlCommand sql = new SqlCommand("dbo.UpdateTarefaById", Conexao))
                {
                    sql.CommandType = CommandType.StoredProcedure;
                    if (!string.IsNullOrEmpty(tarefa.Nome))
                    {
                        sql.Parameters.AddWithValue("@Nome", tarefa.Nome);
                    }
                    else
                    {
                        sql.Parameters.AddWithValue("@Nome", DBNull.Value);
                    }

                    if (!string.IsNullOrEmpty(tarefa.Descricao))
                    {
                        sql.Parameters.AddWithValue("@Descricao", tarefa.Descricao);
                    }
                    else
                    {
                        sql.Parameters.AddWithValue("@Descricao", DBNull.Value);
                    }

                    sql.Parameters.AddWithValue("@Completa", tarefa.Completa ? 1 : 0);
                    sql.Parameters.AddWithValue("@TarefaId", tarefa.Id);

                    if (Conexao.State == ConnectionState.Closed)
                    {
                        Conexao.Open();
                    }

                    sql.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {
                if (Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
                //throw;
            }
            finally
            {
                if (Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            }
        }
        // Método para excluir uma tarefa por ID
        public void DeleteTarefaById(int id)
        {
            SqlConnection Conexao = new SqlConnection(System.Configuration.ConfigurationManager.ConnectionStrings["Conexao"].ConnectionString);
            try
            {
                using (SqlCommand sql = new SqlCommand("dbo.DeleteTarefaById", Conexao))
                {
                    sql.CommandType = CommandType.StoredProcedure;
                    
                    sql.Parameters.AddWithValue("@TarefaId", id);
                  
                    if (Conexao.State == ConnectionState.Closed)
                    {
                        Conexao.Open();
                    }
                    sql.ExecuteNonQuery();
                };
            }
            catch (Exception)
            {
                if (Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
                //throw;
            }
            finally 
            { 
                if(Conexao.State == ConnectionState.Open)
                {
                    Conexao.Close();
                }
            
            }
            

        }
    }

}
