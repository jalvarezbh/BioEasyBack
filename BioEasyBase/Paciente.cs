using BioEasyBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Text.RegularExpressions;

namespace BioEasyBase
{
    public class Paciente : Base
    {
        public List<PacienteModel> Listar(string idUsuario, string idEmpresa)
        {
            try
            {
                if (!ValidarTempoAcesso(idUsuario))
                    throw new Exception("Tempo expirado necessário novo login.");

                List<PacienteModel> pacientes = new List<PacienteModel>();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM PACIENTE
                                        WHERE ID_USUARIO = '{idUsuario}'";

                    if (!string.IsNullOrEmpty(idEmpresa) && !idEmpresa.Equals("00000000-0000-0000-0000-000000000000"))
                    {
                        query += $"  OR ID_EMPRESA = '{idEmpresa}'";
                    }

                    query += $"  ORDER BY NOME";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                PacienteModel paciente = new PacienteModel();
                                paciente.Id = Guid.Parse(reader["ID"].ToString());
                                paciente.Nome = reader["NOME"].ToString();
                                paciente.DataNascimento = Convert.ToDateTime(reader["DATA_NASCIMENTO"].ToString());
                                paciente.Altura = Convert.ToDecimal(reader["ALTURA"]);
                                paciente.Sexo = reader["SEXO"].ToString();
                                paciente.Email = reader["EMAIL"].ToString();
                                paciente.Celular = reader["CELULAR"].ToString();
                                paciente.CPF = reader["CPF"].ToString();
                                paciente.EnderecoLinha = reader["ENDERECO_LINHA"].ToString();
                                paciente.DataCadastro = Convert.ToDateTime(reader["DATA_CADASTRO"].ToString());
                                paciente.Comentario = reader["COMENTARIO"].ToString();

                                if (!string.IsNullOrEmpty(reader["ULTIMA_CONSULTA"].ToString()))
                                {
                                    paciente.UltimaConsulta = Convert.ToDateTime(reader["ULTIMA_CONSULTA"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_ENDERECO"].ToString()))
                                {
                                    paciente.IdEndereco = Guid.Parse(reader["ID_ENDERECO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_USUARIO"].ToString()))
                                {
                                    paciente.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    paciente.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }

                                pacientes.Add(paciente);
                            }
                            return pacientes;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PacienteModel Buscar(string id)
        {
            try
            {
                PacienteModel paciente = new PacienteModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM PACIENTE
                                        WHERE ID = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe paciente com esse identificador.");

                            while (reader.Read())
                            {                               
                                paciente.Id = Guid.Parse(reader["ID"].ToString());
                                paciente.Nome = reader["NOME"].ToString();
                                paciente.DataNascimento = Convert.ToDateTime(reader["DATA_NASCIMENTO"].ToString());
                                paciente.Altura = Convert.ToDecimal(reader["ALTURA"]);
                                paciente.Sexo = reader["SEXO"].ToString();
                                paciente.Email = reader["EMAIL"].ToString();
                                paciente.Celular = reader["CELULAR"].ToString();
                                paciente.CPF = reader["CPF"].ToString();
                                paciente.EnderecoLinha = reader["ENDERECO_LINHA"].ToString();
                                paciente.DataCadastro = Convert.ToDateTime(reader["DATA_CADASTRO"].ToString());
                                paciente.Comentario = reader["COMENTARIO"].ToString();

                                if (!string.IsNullOrEmpty(reader["ULTIMA_CONSULTA"].ToString()))
                                {
                                    paciente.UltimaConsulta = Convert.ToDateTime(reader["ULTIMA_CONSULTA"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_ENDERECO"].ToString()))
                                {
                                    paciente.IdEndereco = Guid.Parse(reader["ID_ENDERECO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_USUARIO"].ToString()))
                                {
                                    paciente.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    paciente.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }
                            }

                            return paciente;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PacienteModel Gravar(PacienteModel paciente)
        {
            try
            {
                if (!ValidarTempoAcesso(paciente.IdUsuario.ToString()))
                    throw new Exception("Tempo expirado necessário novo login.");

                if (string.IsNullOrEmpty(paciente.Id.ToString()) || paciente.Id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    var registro = Incluir(paciente);
                    return registro;
                }
                else
                {
                    Alterar(paciente);
                }

                return paciente;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PacienteModel Incluir(PacienteModel paciente)
        {
            try
            {
                if (!string.IsNullOrEmpty(paciente.Celular))
                {
                    String celular = Regex.Replace(paciente.Celular, @"[^\w\d]", "");
                    paciente.Celular = celular;
                }

                string dataNascimento = paciente.DataNascimento.ToString("yyyy-MM-dd");
                string dataCadastro = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                String altura = paciente.Altura.ToString().Replace(',', '.');
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" INSERT INTO PACIENTE ( 
                                       NOME,              
                                       DATA_NASCIMENTO,           
                                       ALTURA,    
                                       SEXO,
                                       EMAIL,
                                       CELULAR,   
                                       CPF,
                                       ID_ENDERECO,
                                       ENDERECO_LINHA,
                                       DATA_CADASTRO,
                                       ULTIMA_CONSULTA,
                                       COMENTARIO,
                                       ID_USUARIO,
                                       ID_EMPRESA)  
                                       OUTPUT INSERTED.ID
                                       VALUES (
                                       '{paciente.Nome}',
                                       '{dataNascimento}',
                                       {altura},
                                       '{paciente.Sexo}',
                                       '{paciente.Email}',
                                       '{paciente.Celular}',
                                       '{paciente.CPF}',
                                       '{paciente.IdEndereco}',
                                       '{paciente.EnderecoLinha}',
                                       '{dataCadastro}',
                                       '{dataCadastro}',
                                       '{paciente.Comentario}',
                                       '{paciente.IdUsuario}',
                                       '{paciente.IdEmpresa}')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        paciente.Id = (Guid)command.ExecuteScalar();
                    }
                }

                return paciente;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Alterar(PacienteModel paciente)
        {
            try
            {
                Configuracao configuracao = new Configuracao();
                var campos = configuracao.Buscar(paciente.IdUsuario.ToString());

                if (!string.IsNullOrEmpty(paciente.Celular))
                {
                    String celular = Regex.Replace(paciente.Celular, @"[^\w\d]", "");
                    paciente.Celular = celular;
                }

                string dataNascimento = paciente.DataNascimento.ToString("yyyy-MM-dd");               
                String altura = paciente.Altura.ToString().Replace(',', '.');
                              

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE PACIENTE SET 
                                       NOME = '{paciente.Nome}',              
                                       DATA_NASCIMENTO = '{dataNascimento}',           
                                       ALTURA =  {paciente.Altura.ToString().Replace(',', '.')},   
                                       SEXO =  '{paciente.Sexo}' ";

                    if (campos.PacienteEmail)
                        query += $" , EMAIL = '{paciente.Email}'";

                    if (campos.PacienteCelular)
                        query += $" , CELULAR = '{paciente.Celular}'";

                    if (campos.PacienteCPF)
                        query += $" , CPF = '{paciente.CPF}'";

                    if (campos.PacienteComentario)
                        query += $" , COMENTARIO = '{paciente.Comentario}'";

                    if (campos.PacienteEndLinha)
                        query += $" , ENDERECO_LINHA = '{paciente.EnderecoLinha}'";

                    if (campos.PacienteEndCompleto)
                        query += $" , ID_ENDERECO = '{paciente.IdEndereco}'";

                    query += $"  WHERE ID = '{paciente.Id}'";

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

        public void Deletar(string id)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  DELETE
                                        FROM BIOIMPEDANCIA
                                        WHERE ID_PACIENTE = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  DELETE
                                        FROM LABORATORIAL
                                        WHERE ID_PACIENTE = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        command.ExecuteNonQuery();
                    }
                }

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  DELETE
                                        FROM PACIENTE
                                        WHERE ID = '{id}'";

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
