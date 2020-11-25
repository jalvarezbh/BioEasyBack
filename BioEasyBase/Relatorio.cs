using BioEasyBase.Model;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Configuration;
using System.Net.Mail;
using System.Web;

namespace BioEasyBase
{
    public class Relatorio : Base
    {
        public RelatorioPrevencaoModel Prevencao(string idPaciente)
        {
            try
            {               
                RelatorioPrevencaoModel prevencao = new RelatorioPrevencaoModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT TOP 1
                                        U.NOME AS USUARIO_NOME,
                                        U.DOCUMENTO,
                                        U.ESPECIALIDADE,
                                        CASE WHEN E.BALANCA IS NULL
                                              THEN A.BALANCA
                                              ELSE E.BALANCA
                                        END AS BALANCA,
                                        CASE WHEN E.NOME IS NULL
                                              THEN U.NOME
                                              ELSE E.NOME
                                        END AS EMPRESA_NOME,
                                        CASE WHEN E.INSTAGRAM IS NULL
                                              THEN A.INSTAGRAM
                                              ELSE E.INSTAGRAM
                                        END AS INSTAGRAM,
                                        CASE WHEN E.TELEFONE IS NULL
                                              THEN A.TELEFONE
                                              ELSE E.TELEFONE
                                        END AS TELEFONE,
                                        CASE WHEN E.LOGO IS NULL
                                              THEN A.LOGO
                                              ELSE E.LOGO
                                        END AS LOGO,
                                        B.*
                                        FROM BIOIMPEDANCIA B
                                        INNER JOIN USUARIO U ON U.ID = B.ID_USUARIO
                                        LEFT JOIN EMPRESA E ON E.ID = U.ID_EMPRESA 
                                        LEFT JOIN AUTONOMO A ON A.ID = U.ID_AUTONOMO 
                                        WHERE B.ID_PACIENTE = '{idPaciente}'
                                        ORDER BY B.DATA_AVALIACAO DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe dados bioimpedancia com esse identificador.");

                            while (reader.Read())
                            {
                                prevencao.Id = Guid.Parse(reader["ID"].ToString());
                                prevencao.UsuarioNome = reader["USUARIO_NOME"].ToString();
                                prevencao.EmpresaNome = reader["EMPRESA_NOME"].ToString();
                                prevencao.Instagram = reader["INSTAGRAM"].ToString();
                                prevencao.Telefone = reader["TELEFONE"].ToString();
                                prevencao.Documento = reader["DOCUMENTO"].ToString();
                                prevencao.Especialidade = reader["ESPECIALIDADE"].ToString();
                                prevencao.Balanca = reader["BALANCA"].ToString();
                                prevencao.Nome = reader["NOME"].ToString();
                                prevencao.Idade = Convert.ToInt16(reader["IDADE"]);
                                prevencao.Sexo = reader["SEXO"].ToString();
                                prevencao.Peso = Convert.ToDecimal(reader["PESO"]);
                                prevencao.Altura = Convert.ToDecimal(reader["ALTURA"]);
                                prevencao.Logo = reader["LOGO"].ToString();

                                prevencao.NivelGorduraTotal = Convert.ToDecimal(reader["NIVEL_GORDURA_TOTAL"]);
                                prevencao.NivelGorduraPD = Convert.ToDecimal(reader["NIVEL_GORDURA_PD"]);
                                prevencao.NivelGorduraPE = Convert.ToDecimal(reader["NIVEL_GORDURA_PE"]);
                                prevencao.NivelGorduraBD = Convert.ToDecimal(reader["NIVEL_GORDURA_BD"]);
                                prevencao.NivelGorduraBE = Convert.ToDecimal(reader["NIVEL_GORDURA_BE"]);
                                prevencao.NivelGorduraT = Convert.ToDecimal(reader["NIVEL_GORDURA_T"]);

                                prevencao.MassaAdiposaTotal = Convert.ToDecimal(reader["MASSA_ADIPOSA_TOTAL"]);
                                prevencao.MassaAdiposaPD = Convert.ToDecimal(reader["MASSA_ADIPOSA_PD"]);
                                prevencao.MassaAdiposaPE = Convert.ToDecimal(reader["MASSA_ADIPOSA_PE"]);
                                prevencao.MassaAdiposaBD = Convert.ToDecimal(reader["MASSA_ADIPOSA_BD"]);
                                prevencao.MassaAdiposaBE = Convert.ToDecimal(reader["MASSA_ADIPOSA_BE"]);
                                prevencao.MassaAdiposaT = Convert.ToDecimal(reader["MASSA_ADIPOSA_T"]);

                                prevencao.MassaNAdiposaTotal = Convert.ToDecimal(reader["MASSAN_ADIPOSA_TOTAL"]);
                                prevencao.MassaNAdiposaPD = Convert.ToDecimal(reader["MASSAN_ADIPOSA_PD"]);
                                prevencao.MassaNAdiposaPE = Convert.ToDecimal(reader["MASSAN_ADIPOSA_PE"]);
                                prevencao.MassaNAdiposaBD = Convert.ToDecimal(reader["MASSAN_ADIPOSA_BD"]);
                                prevencao.MassaNAdiposaBE = Convert.ToDecimal(reader["MASSAN_ADIPOSA_BE"]);
                                prevencao.MassaNAdiposaT = Convert.ToDecimal(reader["MASSAN_ADIPOSA_T"]);

                                prevencao.MassaMuscularTotal = Convert.ToDecimal(reader["MASSA_MUSCULAR_TOTAL"]);
                                prevencao.MassaMuscularPD = Convert.ToDecimal(reader["MASSA_MUSCULAR_PD"]);
                                prevencao.MassaMuscularPE = Convert.ToDecimal(reader["MASSA_MUSCULAR_PE"]);
                                prevencao.MassaMuscularBD = Convert.ToDecimal(reader["MASSA_MUSCULAR_BD"]);
                                prevencao.MassaMuscularBE = Convert.ToDecimal(reader["MASSA_MUSCULAR_BE"]);
                                prevencao.MassaMuscularT = Convert.ToDecimal(reader["MASSA_MUSCULAR_T"]);

                                prevencao.AguaCorporal = Convert.ToDecimal(reader["AGUA_CORPORAL"]);
                                prevencao.MassaOssea = Convert.ToDecimal(reader["MASSA_OSSEA"]);
                                prevencao.IngestaoCalorica = Convert.ToDecimal(reader["INGESTAO_CALORICA"]);
                                prevencao.GorduraViceral = Convert.ToDecimal(reader["GORDURA_VICERAL"]);
                                prevencao.IdadeMetabolica = Convert.ToDecimal(reader["IDADE_METABOLICA"]);
                                prevencao.TaxaBasal = Convert.ToDecimal(reader["TAXA_BASAL"]);
                                prevencao.MassaCorporal = Convert.ToDecimal(reader["MASSA_CORPORAL"]);
                                prevencao.QualidadeMuscular = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR"]);

                                prevencao.QualidadeMuscularPD = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_PD"]);
                                prevencao.QualidadeMuscularPE = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_PE"]);
                                prevencao.QualidadeMuscularBD = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_BD"]);
                                prevencao.QualidadeMuscularBE = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_BE"]);
                                prevencao.PhysiqueRating = Convert.ToDecimal(reader["PHYSIQUE_RATING"]);

                                if (!string.IsNullOrEmpty(reader["DATA_AVALIACAO"].ToString()))
                                {
                                    prevencao.DataAvaliacao = Convert.ToDateTime(reader["DATA_AVALIACAO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_PACIENTE"].ToString()))
                                {
                                    prevencao.IdPaciente = Guid.Parse(reader["ID_PACIENTE"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_USUARIO"].ToString()))
                                {
                                    prevencao.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    prevencao.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }
                            }

                            return prevencao;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<RelatorioPrevencaoModel> PrevencaoDatas(string idPaciente)
        {
            try
            {
                List<RelatorioPrevencaoModel> registros = new List<RelatorioPrevencaoModel>();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT TOP 5
                                        U.NOME AS USUARIO_NOME,
                                        U.DOCUMENTO,
                                        U.ESPECIALIDADE,
                                        CASE WHEN E.BALANCA IS NULL
                                              THEN A.BALANCA
                                              ELSE E.BALANCA
                                        END AS BALANCA,
                                        CASE WHEN E.NOME IS NULL
                                              THEN U.NOME
                                              ELSE E.NOME
                                        END AS EMPRESA_NOME,
                                        CASE WHEN E.INSTAGRAM IS NULL
                                              THEN A.INSTAGRAM
                                              ELSE E.INSTAGRAM
                                        END AS INSTAGRAM,
                                        CASE WHEN E.TELEFONE IS NULL
                                              THEN A.TELEFONE
                                              ELSE E.TELEFONE
                                        END AS TELEFONE,
                                        B.*
                                        FROM BIOIMPEDANCIA B
                                        INNER JOIN USUARIO U ON U.ID = B.ID_USUARIO
                                        LEFT JOIN EMPRESA E ON E.ID = U.ID_EMPRESA 
                                        LEFT JOIN AUTONOMO A ON A.ID = U.ID_AUTONOMO 
                                        WHERE B.ID_PACIENTE = '{idPaciente}'
                                        ORDER BY B.DATA_AVALIACAO DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe dados bioimpedancia com esse identificador.");

                            while (reader.Read())
                            {
                                RelatorioPrevencaoModel prevencao = new RelatorioPrevencaoModel();
                                prevencao.Id = Guid.Parse(reader["ID"].ToString());
                                prevencao.UsuarioNome = reader["USUARIO_NOME"].ToString();
                                prevencao.EmpresaNome = reader["EMPRESA_NOME"].ToString();
                                prevencao.Instagram = reader["INSTAGRAM"].ToString();
                                prevencao.Telefone = reader["TELEFONE"].ToString();
                                prevencao.Documento = reader["DOCUMENTO"].ToString();
                                prevencao.Especialidade = reader["ESPECIALIDADE"].ToString();
                                prevencao.Balanca = reader["BALANCA"].ToString();
                                prevencao.Nome = reader["NOME"].ToString();
                                prevencao.Idade = Convert.ToInt16(reader["IDADE"]);
                                prevencao.Sexo = reader["SEXO"].ToString();
                                prevencao.Peso = Convert.ToDecimal(reader["PESO"]);
                                prevencao.Altura = Convert.ToDecimal(reader["ALTURA"]);

                                prevencao.NivelGorduraTotal = Convert.ToDecimal(reader["NIVEL_GORDURA_TOTAL"]);
                                prevencao.NivelGorduraPD = Convert.ToDecimal(reader["NIVEL_GORDURA_PD"]);
                                prevencao.NivelGorduraPE = Convert.ToDecimal(reader["NIVEL_GORDURA_PE"]);
                                prevencao.NivelGorduraBD = Convert.ToDecimal(reader["NIVEL_GORDURA_BD"]);
                                prevencao.NivelGorduraBE = Convert.ToDecimal(reader["NIVEL_GORDURA_BE"]);
                                prevencao.NivelGorduraT = Convert.ToDecimal(reader["NIVEL_GORDURA_T"]);

                                prevencao.MassaAdiposaTotal = Convert.ToDecimal(reader["MASSA_ADIPOSA_TOTAL"]);
                                prevencao.MassaAdiposaPD = Convert.ToDecimal(reader["MASSA_ADIPOSA_PD"]);
                                prevencao.MassaAdiposaPE = Convert.ToDecimal(reader["MASSA_ADIPOSA_PE"]);
                                prevencao.MassaAdiposaBD = Convert.ToDecimal(reader["MASSA_ADIPOSA_BD"]);
                                prevencao.MassaAdiposaBE = Convert.ToDecimal(reader["MASSA_ADIPOSA_BE"]);
                                prevencao.MassaAdiposaT = Convert.ToDecimal(reader["MASSA_ADIPOSA_T"]);

                                prevencao.MassaNAdiposaTotal = Convert.ToDecimal(reader["MASSAN_ADIPOSA_TOTAL"]);
                                prevencao.MassaNAdiposaPD = Convert.ToDecimal(reader["MASSAN_ADIPOSA_PD"]);
                                prevencao.MassaNAdiposaPE = Convert.ToDecimal(reader["MASSAN_ADIPOSA_PE"]);
                                prevencao.MassaNAdiposaBD = Convert.ToDecimal(reader["MASSAN_ADIPOSA_BD"]);
                                prevencao.MassaNAdiposaBE = Convert.ToDecimal(reader["MASSAN_ADIPOSA_BE"]);
                                prevencao.MassaNAdiposaT = Convert.ToDecimal(reader["MASSAN_ADIPOSA_T"]);

                                prevencao.MassaMuscularTotal = Convert.ToDecimal(reader["MASSA_MUSCULAR_TOTAL"]);
                                prevencao.MassaMuscularPD = Convert.ToDecimal(reader["MASSA_MUSCULAR_PD"]);
                                prevencao.MassaMuscularPE = Convert.ToDecimal(reader["MASSA_MUSCULAR_PE"]);
                                prevencao.MassaMuscularBD = Convert.ToDecimal(reader["MASSA_MUSCULAR_BD"]);
                                prevencao.MassaMuscularBE = Convert.ToDecimal(reader["MASSA_MUSCULAR_BE"]);
                                prevencao.MassaMuscularT = Convert.ToDecimal(reader["MASSA_MUSCULAR_T"]);

                                prevencao.AguaCorporal = Convert.ToDecimal(reader["AGUA_CORPORAL"]);
                                prevencao.MassaOssea = Convert.ToDecimal(reader["MASSA_OSSEA"]);
                                prevencao.IngestaoCalorica = Convert.ToDecimal(reader["INGESTAO_CALORICA"]);
                                prevencao.GorduraViceral = Convert.ToDecimal(reader["GORDURA_VICERAL"]);
                                prevencao.IdadeMetabolica = Convert.ToDecimal(reader["IDADE_METABOLICA"]);
                                prevencao.TaxaBasal = Convert.ToDecimal(reader["TAXA_BASAL"]);
                                prevencao.MassaCorporal = Convert.ToDecimal(reader["MASSA_CORPORAL"]);

                                prevencao.QualidadeMuscular = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR"]);
                                prevencao.QualidadeMuscularPD = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_PD"]);
                                prevencao.QualidadeMuscularPE = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_PE"]);
                                prevencao.QualidadeMuscularBD = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_BD"]);
                                prevencao.QualidadeMuscularBE = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_BE"]);

                                prevencao.PhysiqueRating = Convert.ToDecimal(reader["PHYSIQUE_RATING"]);

                                if (!string.IsNullOrEmpty(reader["DATA_AVALIACAO"].ToString()))
                                {
                                    prevencao.DataAvaliacao = Convert.ToDateTime(reader["DATA_AVALIACAO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_PACIENTE"].ToString()))
                                {
                                    prevencao.IdPaciente = Guid.Parse(reader["ID_PACIENTE"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_USUARIO"].ToString()))
                                {
                                    prevencao.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    prevencao.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }

                                registros.Add(prevencao);
                            }

                            return registros.OrderBy(o => o.DataAvaliacao).ToList();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public LaboratorialModel PrevencaoLaboratorial(string idPaciente, DateTime dataAvaliacao)
        {
            try
            {
                string dataInicio = dataAvaliacao.AddDays(-1).ToString("yyyy-MM-dd");
                string dataFinal = dataAvaliacao.AddDays(1).ToString("yyyy-MM-dd");
                LaboratorialModel laboratorial = new LaboratorialModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT TOP 1 *
                                        FROM LABORATORIAL
                                        WHERE ID_PACIENTE = '{idPaciente}'
                                        AND DATA_AVALIACAO > '{dataInicio}'
                                        AND DATA_AVALIACAO < '{dataFinal}' 
                                        ORDER BY DATA_AVALIACAO DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                return null;

                            while (reader.Read())
                            {
                                laboratorial.Id = Guid.Parse(reader["ID"].ToString());
                                laboratorial.Nome = reader["NOME"].ToString();
                                laboratorial.Colesterol = Convert.ToDecimal(reader["COLESTEROL"]);
                                laboratorial.LDL = Convert.ToDecimal(reader["LDL"]);
                                laboratorial.HDL = Convert.ToDecimal(reader["HDL"]);
                                laboratorial.Trigliceres = Convert.ToDecimal(reader["TRIGLICERES"]);
                                laboratorial.Acucar = Convert.ToDecimal(reader["ACUCAR"]);
                                laboratorial.UltimaRefeicao = Convert.ToInt16(reader["ULTIMA_REFEICAO"]);

                                if (!string.IsNullOrEmpty(reader["DATA_EXAME"].ToString()))
                                {
                                    laboratorial.DataExame = Convert.ToDateTime(reader["DATA_EXAME"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["DATA_AVALIACAO"].ToString()))
                                {
                                    laboratorial.DataAvaliacao = Convert.ToDateTime(reader["DATA_AVALIACAO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_PACIENTE"].ToString()))
                                {
                                    laboratorial.IdPaciente = Guid.Parse(reader["ID_PACIENTE"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_USUARIO"].ToString()))
                                {
                                    laboratorial.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    laboratorial.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }
                            }

                            return laboratorial;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<LaboratorialModel> LaboratorialDatas(string idPaciente)
        {
            try
            {
                List<LaboratorialModel> registros = new List<LaboratorialModel>();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT TOP 5 *
                                        FROM LABORATORIAL
                                        WHERE ID_PACIENTE = '{idPaciente}'
                                        ORDER BY DATA_AVALIACAO DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                return null;

                            while (reader.Read())
                            {
                                LaboratorialModel laboratorial = new LaboratorialModel();

                                laboratorial.Id = Guid.Parse(reader["ID"].ToString());
                                laboratorial.Nome = reader["NOME"].ToString();
                                laboratorial.Colesterol = Convert.ToDecimal(reader["COLESTEROL"]);
                                laboratorial.LDL = Convert.ToDecimal(reader["LDL"]);
                                laboratorial.HDL = Convert.ToDecimal(reader["HDL"]);
                                laboratorial.Trigliceres = Convert.ToDecimal(reader["TRIGLICERES"]);
                                laboratorial.Acucar = Convert.ToDecimal(reader["ACUCAR"]);
                                laboratorial.UltimaRefeicao = Convert.ToInt16(reader["ULTIMA_REFEICAO"]);

                                if (!string.IsNullOrEmpty(reader["DATA_EXAME"].ToString()))
                                {
                                    laboratorial.DataExame = Convert.ToDateTime(reader["DATA_EXAME"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["DATA_AVALIACAO"].ToString()))
                                {
                                    laboratorial.DataAvaliacao = Convert.ToDateTime(reader["DATA_AVALIACAO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_PACIENTE"].ToString()))
                                {
                                    laboratorial.IdPaciente = Guid.Parse(reader["ID_PACIENTE"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_USUARIO"].ToString()))
                                {
                                    laboratorial.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    laboratorial.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }

                                registros.Add(laboratorial);
                            }

                            return registros.OrderBy(o => o.DataAvaliacao).ToList();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public List<RelatorioAtualProgressoModel> AtualProgresso(string idPaciente)
        {
            try
            {
                List<RelatorioAtualProgressoModel> atuais = new List<RelatorioAtualProgressoModel>();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT TOP 3                                        
                                        B.*
                                        FROM BIOIMPEDANCIA B
                                        WHERE B.ID_PACIENTE = '{idPaciente}'
                                        ORDER BY B.DATA_AVALIACAO DESC";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            if (!reader.HasRows)
                                return atuais;

                            while (reader.Read())
                            {
                                RelatorioAtualProgressoModel atual = new RelatorioAtualProgressoModel();
                                atual.Id = Guid.Parse(reader["ID"].ToString());

                                atual.NivelGorduraTotal = Convert.ToDecimal(reader["NIVEL_GORDURA_TOTAL"]);
                                atual.NivelGorduraPD = Convert.ToDecimal(reader["NIVEL_GORDURA_PD"]);
                                atual.NivelGorduraPE = Convert.ToDecimal(reader["NIVEL_GORDURA_PE"]);
                                atual.NivelGorduraBD = Convert.ToDecimal(reader["NIVEL_GORDURA_BD"]);
                                atual.NivelGorduraBE = Convert.ToDecimal(reader["NIVEL_GORDURA_BE"]);
                                atual.NivelGorduraT = Convert.ToDecimal(reader["NIVEL_GORDURA_T"]);

                                atual.MassaAdiposaTotal = Convert.ToDecimal(reader["MASSA_ADIPOSA_TOTAL"]);
                                atual.MassaAdiposaPD = Convert.ToDecimal(reader["MASSA_ADIPOSA_PD"]);
                                atual.MassaAdiposaPE = Convert.ToDecimal(reader["MASSA_ADIPOSA_PE"]);
                                atual.MassaAdiposaBD = Convert.ToDecimal(reader["MASSA_ADIPOSA_BD"]);
                                atual.MassaAdiposaBE = Convert.ToDecimal(reader["MASSA_ADIPOSA_BE"]);
                                atual.MassaAdiposaT = Convert.ToDecimal(reader["MASSA_ADIPOSA_T"]);

                                atual.MassaNAdiposaTotal = Convert.ToDecimal(reader["MASSAN_ADIPOSA_TOTAL"]);
                                atual.MassaNAdiposaPD = Convert.ToDecimal(reader["MASSAN_ADIPOSA_PD"]);
                                atual.MassaNAdiposaPE = Convert.ToDecimal(reader["MASSAN_ADIPOSA_PE"]);
                                atual.MassaNAdiposaBD = Convert.ToDecimal(reader["MASSAN_ADIPOSA_BD"]);
                                atual.MassaNAdiposaBE = Convert.ToDecimal(reader["MASSAN_ADIPOSA_BE"]);
                                atual.MassaNAdiposaT = Convert.ToDecimal(reader["MASSAN_ADIPOSA_T"]);

                                atual.MassaMuscularTotal = Convert.ToDecimal(reader["MASSA_MUSCULAR_TOTAL"]);
                                atual.MassaMuscularPD = Convert.ToDecimal(reader["MASSA_MUSCULAR_PD"]);
                                atual.MassaMuscularPE = Convert.ToDecimal(reader["MASSA_MUSCULAR_PE"]);
                                atual.MassaMuscularBD = Convert.ToDecimal(reader["MASSA_MUSCULAR_BD"]);
                                atual.MassaMuscularBE = Convert.ToDecimal(reader["MASSA_MUSCULAR_BE"]);
                                atual.MassaMuscularT = Convert.ToDecimal(reader["MASSA_MUSCULAR_T"]);

                                atual.QualidadeMuscularTotal = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR"]);
                                atual.QualidadeMuscularPD = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_PD"]);
                                atual.QualidadeMuscularPE = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_PE"]);
                                atual.QualidadeMuscularBD = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_BD"]);
                                atual.QualidadeMuscularBE = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_BE"]);
                                atual.PhysiqueRating = Convert.ToDecimal(reader["PHYSIQUE_RATING"]);

                                if (!string.IsNullOrEmpty(reader["DATA_AVALIACAO"].ToString()))
                                {
                                    atual.DataAvaliacao = Convert.ToDateTime(reader["DATA_AVALIACAO"].ToString());
                                }

                                atuais.Add(atual);

                            }

                            return atuais.OrderBy(o => o.DataAvaliacao).ToList();
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void EnviarEmail(RelatorioEnvioModel registro)
        {
            try
            {
                PacienteModel paciente = new PacienteModel();
                UsuarioModel usuario = new UsuarioModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = @" SELECT P.ID, P.NOME, P.EMAIL, 
                                             U.NOME AS RESPONSAVEL, U.ESPECIALIDADE 
                                       FROM PACIENTE P
                                       INNER JOIN USUARIO U ON P.ID_USUARIO = U.ID
                                       WHERE P.ID = '{0}'";
                    query = string.Format(query, registro.IdPaciente);

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("E-mail não cadastrado.");

                            while (reader.Read())
                            {
                                paciente.Id = Guid.Parse(reader["ID"].ToString());
                                paciente.Nome = reader["NOME"].ToString();
                                paciente.Email = reader["EMAIL"].ToString();
                                usuario.Nome = reader["RESPONSAVEL"].ToString();
                                usuario.Especialidade = reader["ESPECIALIDADE"].ToString();                                
                            }
                        }
                    }
                }

                if (string.IsNullOrEmpty(paciente.Email))
                {
                    throw new Exception("E-mail não cadastrado.");
                }

                string path = HttpContext.Current.Server.MapPath("\\Template.html");
                string html = string.Empty;
                var mailMessage = new MailMessage();
                

                mailMessage.Subject = "BIOEASY - AVALIAÇÃO DE BIOIMPEDANCIA";
                using (var arquivoHtml = new StreamReader(path))
                {
                    html = arquivoHtml.ReadToEnd();
                    html = html.Replace("[TITULO]", @"Avaliação de Bioimpedancia <br /> " + usuario.Nome + " <br /> " + usuario.Especialidade);
                    html = html.Replace("[SAUDACAO]", @"Prezado(a)," + paciente.Nome);


                    string linha0 = Linha(false, "Segue o link para download de sua avaliação de Bioimpedancia.", null);
                    string linha1 = Linha(false, "Dúvidas, entrar em contato com seu profissional de saúde.", null);
                    string linha2 = string.Empty;

                    if (registro.Tipo.Equals("atual"))
                        linha2 = LinhaLinkPDF("Arquivo em formato PDF.", $"https://bioeasypro.club/relatorioatual?idP={registro.IdPaciente}&email=true");

                    if (registro.Tipo.Equals("prevencao"))
                        linha2 = LinhaLinkPDF("Arquivo em formato PDF.", $"https://bioeasypro.club/relatorioprevencao?idP={registro.IdPaciente}&email=true");

                    if (registro.Tipo.Equals("progresso"))
                        linha2 = LinhaLinkPDF("Arquivo em formato PDF.", $"https://bioeasypro.club/relatorioprogresso?idP={registro.IdPaciente}&email=true");

                    html = html.Replace("[LINHA]",linha0 + linha1 + linha2);
                }

                mailMessage.IsBodyHtml = true;
                mailMessage.Body = html;
                mailMessage.To.Add(paciente.Email);

                var dadosConta = new Sistema().BuscarEmailSistema();
                var smtp = new SmtpClient
                {
                    Host = "relay-hosting.secureserver.net",//"smtp.gmail.com",
                    Port = 25,//587,
                    EnableSsl = false,
                    Timeout = 10000,
                    DeliveryMethod = SmtpDeliveryMethod.Network,
                    UseDefaultCredentials = false,
                    Credentials = new NetworkCredential(dadosConta.Email, dadosConta.Senha)
                };
                mailMessage.From = new MailAddress(dadosConta.Email);
                smtp.Send(mailMessage);
                smtp.Dispose();


            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

    }
}
