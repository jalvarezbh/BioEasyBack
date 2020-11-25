using BioEasyBase.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BioEasyBase
{
    public class Configuracao : Base
    {
        public ConfiguracaoModel Buscar(string idUsuario)
        {            
            try
            {
                ConfiguracaoModel configuracao = new ConfiguracaoModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM CONFIGURACAO
                                        WHERE ID_USUARIO = '{idUsuario}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe configuração para este usuário.");

                            while (reader.Read())
                            {
                                configuracao.Id = Guid.Parse(reader["ID"].ToString());
                                configuracao.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                configuracao.PacienteAluno = Convert.ToBoolean(reader["PACIENTE_ALUNO"]);
                                configuracao.PacienteEmail = Convert.ToBoolean(reader["PACIENTE_EMAIL"]);
                                configuracao.PacienteCelular = Convert.ToBoolean(reader["PACIENTE_CELULAR"]);
                                configuracao.PacienteCPF = Convert.ToBoolean(reader["PACIENTE_CPF"]);
                                configuracao.PacienteEndCompleto = Convert.ToBoolean(reader["PACIENTE_ENDCOMPLETO"]);
                                configuracao.PacienteEndLinha = Convert.ToBoolean(reader["PACIENTE_ENDLINHA"]);
                                configuracao.PacienteComentario = Convert.ToBoolean(reader["PACIENTE_COMENTARIO"]);
                                configuracao.DataAtualizacao = Convert.ToDateTime(reader["DATA_ATUALIZACAO"].ToString());
                            }
                                                       
                            return configuracao;
                        }
                    }
                }   
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Incluir(Guid idUsuario)
        {
            try
            {
                string dataAtualizacao = DateTime.Now.ToString("yyyy-MM-dd HH:mm");
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" INSERT INTO CONFIGURACAO ( 
                                       PACIENTE_EMAIL,              
                                       PACIENTE_CELULAR,
                                       PACIENTE_CPF,
                                       PACIENTE_ENDCOMPLETO,
                                       PACIENTE_ENDLINHA,
                                       PACIENTE_COMENTARIO,   
                                       PACIENTE_ALUNO,
                                       DATA_ATUALIZACAO,
                                       ID_USUARIO )                                        
                                       VALUES (
                                       1,
                                       1,
                                       1,
                                       0,
                                       1,
                                       1,
                                       0,
                                       '{dataAtualizacao}',
                                       '{idUsuario}')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                       command.ExecuteNonQuery();
                    }
                }                
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Alterar(ConfiguracaoModel configuracao)
        {
            try
            {                
                var dataAtualizacao = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE CONFIGURACAO SET 
                                       PACIENTE_ALUNO = '{configuracao.PacienteAluno}',                                       
                                       PACIENTE_EMAIL = '{configuracao.PacienteEmail}',
                                       PACIENTE_CELULAR = '{configuracao.PacienteCelular}',
                                       PACIENTE_CPF = '{configuracao.PacienteCPF}',
                                       PACIENTE_ENDCOMPLETO = '{configuracao.PacienteEndCompleto}',
                                       PACIENTE_ENDLINHA = '{configuracao.PacienteEndLinha}',
                                       PACIENTE_COMENTARIO = '{configuracao.PacienteComentario}',
                                       DATA_ATUALIZACAO = '{dataAtualizacao}'
                                       WHERE ID = '{configuracao.Id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
