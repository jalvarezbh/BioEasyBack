using BioEasyBase.Model;
using System;
using System.Data;
using System.Data.SqlClient;

namespace BioEasyBase
{

    public class Endereco : Base
    {
        public AutonomoModel BuscarAutonomo(AutonomoModel autonomo)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM ENDERECO
                                        WHERE ID = '{autonomo.IdEndereco}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe endereço com esse identificador.");

                            while (reader.Read())
                            {
                                autonomo.CEP = reader["CEP"].ToString();
                                autonomo.Logradouro = reader["LOGRADOURO"].ToString();
                                autonomo.Numero = reader["NUMERO"].ToString();
                                autonomo.Complemento = reader["COMPLEMENTO"].ToString();
                                autonomo.Bairro = reader["BAIRRO"].ToString();
                                autonomo.Cidade = reader["CIDADE"].ToString();
                                autonomo.UF = reader["UF"].ToString();
                            }

                            return autonomo;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EmpresaModel BuscarEmpresa(EmpresaModel empresa)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM ENDERECO
                                        WHERE ID = '{empresa.IdEndereco}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe endereço com esse identificador.");

                            while (reader.Read())
                            {
                                empresa.CEP = reader["CEP"].ToString();
                                empresa.Logradouro = reader["LOGRADOURO"].ToString();
                                empresa.Numero = reader["NUMERO"].ToString();
                                empresa.Complemento = reader["COMPLEMENTO"].ToString();
                                empresa.Bairro = reader["BAIRRO"].ToString();
                                empresa.Cidade = reader["CIDADE"].ToString();
                                empresa.UF = reader["UF"].ToString();
                            }

                            return empresa;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public PacienteModel BuscarPaciente(PacienteModel paciente)
        {
            try
            {
                if (string.IsNullOrEmpty(paciente.IdEndereco.ToString()) || paciente.IdEndereco.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    return paciente;
                }

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM ENDERECO
                                        WHERE ID = '{paciente.IdEndereco}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe endereço com esse identificador.");

                            while (reader.Read())
                            {
                                paciente.CEP = reader["CEP"].ToString();
                                paciente.Logradouro = reader["LOGRADOURO"].ToString();
                                paciente.Numero = reader["NUMERO"].ToString();
                                paciente.Complemento = reader["COMPLEMENTO"].ToString();
                                paciente.Bairro = reader["BAIRRO"].ToString();
                                paciente.Cidade = reader["CIDADE"].ToString();
                                paciente.UF = reader["UF"].ToString();
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

        public EnderecoModel GravarAutonomo(AutonomoModel autonomo)
        {
            try
            {
                EnderecoModel endereco = new EnderecoModel();
                endereco.Id = autonomo.IdEndereco;
                endereco.CEP = autonomo.CEP;
                endereco.Logradouro = autonomo.Logradouro;
                endereco.Numero = autonomo.Numero;
                endereco.Complemento = autonomo.Complemento;
                endereco.Bairro = autonomo.Bairro;
                endereco.Cidade = autonomo.Cidade;
                endereco.UF = autonomo.UF;

                if (string.IsNullOrEmpty(endereco.Id.ToString()) || endereco.Id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    var registro = Incluir(endereco);
                    return registro;
                }
                else
                {
                    Alterar(endereco);
                }

                return endereco;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EnderecoModel GravarEmpresa(EmpresaModel empresa)
        {
            try
            {
                EnderecoModel endereco = new EnderecoModel();
                endereco.Id = empresa.IdEndereco;
                endereco.CEP = empresa.CEP;
                endereco.Logradouro = empresa.Logradouro;
                endereco.Numero = empresa.Numero;
                endereco.Complemento = empresa.Complemento;
                endereco.Bairro = empresa.Bairro;
                endereco.Cidade = empresa.Cidade;
                endereco.UF = empresa.UF;

                if (string.IsNullOrEmpty(endereco.Id.ToString()) || endereco.Id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    var registro = Incluir(endereco);
                    return registro;
                }
                else
                {
                    Alterar(endereco);
                }

                return endereco;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EnderecoModel GravarPaciente(PacienteModel paciente)
        {
            try
            {
                Configuracao configuracao = new Configuracao();
                var campos = configuracao.Buscar(paciente.IdUsuario.ToString());

                if (!campos.PacienteEndCompleto)
                    return new EnderecoModel();

                EnderecoModel endereco = new EnderecoModel();
                endereco.Id = paciente.IdEndereco;
                endereco.CEP = paciente.CEP;
                endereco.Logradouro = paciente.Logradouro;
                endereco.Numero = paciente.Numero;
                endereco.Complemento = paciente.Complemento;
                endereco.Bairro = paciente.Bairro;
                endereco.Cidade = paciente.Cidade;
                endereco.UF = paciente.UF;

                if (string.IsNullOrEmpty(endereco.Id.ToString()) || endereco.Id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    var registro = Incluir(endereco);
                    return registro;
                }
                else
                {
                    Alterar(endereco);
                }

                return endereco;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public EnderecoModel Incluir(EnderecoModel endereco)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" INSERT INTO ENDERECO ( 
                                       CEP,              
                                       LOGRADOURO,           
                                       NUMERO,    
                                       COMPLEMENTO,
                                       BAIRRO,
                                       CIDADE,   
                                       UF)  
                                       OUTPUT INSERTED.ID
                                       VALUES (
                                       '{endereco.CEP}',
                                       '{endereco.Logradouro}',
                                       '{endereco.Numero}',
                                       '{endereco.Complemento}',
                                       '{endereco.Bairro}',
                                       '{endereco.Cidade}',
                                       '{endereco.UF}')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        endereco.Id = (Guid)command.ExecuteScalar();
                    }
                }

                return endereco;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Alterar(EnderecoModel endereco)
        {
            try
            {
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE ENDERECO SET 
                                       CEP = '{endereco.CEP}',              
                                       LOGRADOURO = '{endereco.Logradouro}',           
                                       NUMERO =  '{endereco.Numero}',   
                                       COMPLEMENTO =  '{endereco.Complemento}',   
                                       BAIRRO =  '{endereco.Bairro}',   
                                       CIDADE = '{endereco.Cidade}',   
                                       UF = '{endereco.UF}'
                                       WHERE ID = '{endereco.Id}'";

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
