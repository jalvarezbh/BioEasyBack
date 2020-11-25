using BioEasyBase.Model;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace BioEasyBase
{
    public class Bioimpedancia : Base
    {
        public List<BioimpedanciaModel> Listar(string idPaciente)
        {
            try
            {
                List<BioimpedanciaModel> bioimpedancias = new List<BioimpedanciaModel>();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM BIOIMPEDANCIA
                                        WHERE ID_PACIENTE = '{idPaciente}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader())
                        {
                            while (reader.Read())
                            {
                                BioimpedanciaModel bioimpedancia = new BioimpedanciaModel();
                                bioimpedancia.Id = Guid.Parse(reader["ID"].ToString());
                                bioimpedancia.Nome = reader["NOME"].ToString();
                                bioimpedancia.Idade = Convert.ToInt16(reader["IDADE"]);
                                bioimpedancia.Sexo = reader["SEXO"].ToString();                                                                                                                             

                                if (!string.IsNullOrEmpty(reader["DATA_AVALIACAO"].ToString()))
                                {
                                    bioimpedancia.DataAvaliacao = Convert.ToDateTime(reader["DATA_AVALIACAO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_PACIENTE"].ToString()))
                                {
                                    bioimpedancia.IdPaciente = Guid.Parse(reader["ID_PACIENTE"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_USUARIO"].ToString()))
                                {
                                    bioimpedancia.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    bioimpedancia.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }

                                bioimpedancias.Add(bioimpedancia);
                            }
                            return bioimpedancias;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public BioimpedanciaModel Buscar(string id)
        {
            try
            {
                BioimpedanciaModel bioimpedancia = new BioimpedanciaModel();
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@"  SELECT *
                                        FROM BIOIMPEDANCIA
                                        WHERE ID = '{id}'";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        using (SqlDataReader reader = command.ExecuteReader(CommandBehavior.SingleRow))
                        {
                            if (!reader.HasRows)
                                throw new Exception("Não existe dados bioimpedancia com esse identificador.");

                            while (reader.Read())
                            {
                                bioimpedancia.Id = Guid.Parse(reader["ID"].ToString());
                                bioimpedancia.Nome = reader["NOME"].ToString();
                                bioimpedancia.Idade = Convert.ToInt16(reader["IDADE"]);
                                bioimpedancia.Sexo = reader["SEXO"].ToString();
                                bioimpedancia.Peso = Convert.ToDecimal(reader["PESO"]);
                                bioimpedancia.Altura = Convert.ToDecimal(reader["ALTURA"]);

                                bioimpedancia.NivelGorduraTotal = Convert.ToDecimal(reader["NIVEL_GORDURA_TOTAL"]);
                                bioimpedancia.NivelGorduraPD = Convert.ToDecimal(reader["NIVEL_GORDURA_PD"]);
                                bioimpedancia.NivelGorduraPE = Convert.ToDecimal(reader["NIVEL_GORDURA_PE"]);
                                bioimpedancia.NivelGorduraBD = Convert.ToDecimal(reader["NIVEL_GORDURA_BD"]);
                                bioimpedancia.NivelGorduraBE = Convert.ToDecimal(reader["NIVEL_GORDURA_BE"]);
                                bioimpedancia.NivelGorduraT = Convert.ToDecimal(reader["NIVEL_GORDURA_T"]);

                                bioimpedancia.MassaAdiposaTotal = Convert.ToDecimal(reader["MASSA_ADIPOSA_TOTAL"]);
                                bioimpedancia.MassaAdiposaPD = Convert.ToDecimal(reader["MASSA_ADIPOSA_PD"]);
                                bioimpedancia.MassaAdiposaPE = Convert.ToDecimal(reader["MASSA_ADIPOSA_PE"]);
                                bioimpedancia.MassaAdiposaBD = Convert.ToDecimal(reader["MASSA_ADIPOSA_BD"]);
                                bioimpedancia.MassaAdiposaBE = Convert.ToDecimal(reader["MASSA_ADIPOSA_BE"]);
                                bioimpedancia.MassaAdiposaT = Convert.ToDecimal(reader["MASSA_ADIPOSA_T"]);

                                bioimpedancia.MassaNAdiposaTotal = Convert.ToDecimal(reader["MASSAN_ADIPOSA_TOTAL"]);
                                bioimpedancia.MassaNAdiposaPD = Convert.ToDecimal(reader["MASSAN_ADIPOSA_PD"]);
                                bioimpedancia.MassaNAdiposaPE = Convert.ToDecimal(reader["MASSAN_ADIPOSA_PE"]);
                                bioimpedancia.MassaNAdiposaBD = Convert.ToDecimal(reader["MASSAN_ADIPOSA_BD"]);
                                bioimpedancia.MassaNAdiposaBE = Convert.ToDecimal(reader["MASSAN_ADIPOSA_BE"]);
                                bioimpedancia.MassaNAdiposaT = Convert.ToDecimal(reader["MASSAN_ADIPOSA_T"]);

                                bioimpedancia.MassaMuscularTotal = Convert.ToDecimal(reader["MASSA_MUSCULAR_TOTAL"]);
                                bioimpedancia.MassaMuscularPD = Convert.ToDecimal(reader["MASSA_MUSCULAR_PD"]);
                                bioimpedancia.MassaMuscularPE = Convert.ToDecimal(reader["MASSA_MUSCULAR_PE"]);
                                bioimpedancia.MassaMuscularBD = Convert.ToDecimal(reader["MASSA_MUSCULAR_BD"]);
                                bioimpedancia.MassaMuscularBE = Convert.ToDecimal(reader["MASSA_MUSCULAR_BE"]);
                                bioimpedancia.MassaMuscularT = Convert.ToDecimal(reader["MASSA_MUSCULAR_T"]);

                                bioimpedancia.AguaCorporal = Convert.ToDecimal(reader["AGUA_CORPORAL"]);
                                bioimpedancia.MassaOssea = Convert.ToDecimal(reader["MASSA_OSSEA"]);
                                bioimpedancia.IngestaoCalorica = Convert.ToDecimal(reader["INGESTAO_CALORICA"]);
                                bioimpedancia.GorduraViceral = Convert.ToDecimal(reader["GORDURA_VICERAL"]);
                                bioimpedancia.IdadeMetabolica = Convert.ToDecimal(reader["IDADE_METABOLICA"]);
                                bioimpedancia.TaxaBasal = Convert.ToDecimal(reader["TAXA_BASAL"]);
                                bioimpedancia.MassaCorporal = Convert.ToDecimal(reader["MASSA_CORPORAL"]);

                                bioimpedancia.QualidadeMuscular = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR"]);
                                bioimpedancia.QualidadeMuscularPD = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_PD"]);
                                bioimpedancia.QualidadeMuscularPE = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_PE"]);
                                bioimpedancia.QualidadeMuscularBD = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_BD"]);
                                bioimpedancia.QualidadeMuscularBE = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_BE"]);
                                bioimpedancia.QualidadeMuscularT = Convert.ToDecimal(reader["QUALIDADE_MUSCULAR_T"]);

                                bioimpedancia.PhysiqueRating = Convert.ToDecimal(reader["PHYSIQUE_RATING"]);
                               
                                if (!string.IsNullOrEmpty(reader["DATA_AVALIACAO"].ToString()))
                                {
                                    bioimpedancia.DataAvaliacao = Convert.ToDateTime(reader["DATA_AVALIACAO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_PACIENTE"].ToString()))
                                {
                                    bioimpedancia.IdPaciente = Guid.Parse(reader["ID_PACIENTE"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_USUARIO"].ToString()))
                                {
                                    bioimpedancia.IdUsuario = Guid.Parse(reader["ID_USUARIO"].ToString());
                                }

                                if (!string.IsNullOrEmpty(reader["ID_EMPRESA"].ToString()))
                                {
                                    bioimpedancia.IdEmpresa = Guid.Parse(reader["ID_EMPRESA"].ToString());
                                }
                            }

                            return bioimpedancia;
                        }
                    }
                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public BioimpedanciaModel Gravar(BioimpedanciaModel bioimpedancia)
        {
            try
            {
                if (string.IsNullOrEmpty(bioimpedancia.Id.ToString()) || bioimpedancia.Id.ToString().Equals("00000000-0000-0000-0000-000000000000"))
                {
                    var registro = Incluir(bioimpedancia);
                    return registro;
                }
                else
                {
                    Alterar(bioimpedancia);
                }

                return bioimpedancia;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public BioimpedanciaModel Incluir(BioimpedanciaModel bioimpedancia)
        {
            try
            {               
                string dataAvaliacao = bioimpedancia.DataAvaliacao.ToString("yyyy-MM-dd HH:mm:ss");
                
                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" INSERT INTO BIOIMPEDANCIA ( 
                                       NOME,              
                                       IDADE,           
                                       SEXO,    
                                       PESO,
                                       ALTURA,
                                       NIVEL_GORDURA_TOTAL,
                                       NIVEL_GORDURA_PD ,
                                       NIVEL_GORDURA_PE ,
                                       NIVEL_GORDURA_BD ,
                                       NIVEL_GORDURA_BE ,
                                       NIVEL_GORDURA_T ,
                                       MASSA_ADIPOSA_TOTAL ,
                                       MASSA_ADIPOSA_PD ,
                                       MASSA_ADIPOSA_PE ,
                                       MASSA_ADIPOSA_BD ,
                                       MASSA_ADIPOSA_BE ,
                                       MASSA_ADIPOSA_T ,
                                       MASSAN_ADIPOSA_TOTAL ,
                                       MASSAN_ADIPOSA_PD ,
                                       MASSAN_ADIPOSA_PE ,
                                       MASSAN_ADIPOSA_BD ,
                                       MASSAN_ADIPOSA_BE ,
                                       MASSAN_ADIPOSA_T ,
                                       MASSA_MUSCULAR_TOTAL ,
                                       MASSA_MUSCULAR_PD ,
                                       MASSA_MUSCULAR_PE ,
                                       MASSA_MUSCULAR_BD ,
                                       MASSA_MUSCULAR_BE ,
                                       MASSA_MUSCULAR_T ,
                                       AGUA_CORPORAL ,
                                       MASSA_OSSEA ,
                                       INGESTAO_CALORICA ,
                                       GORDURA_VICERAL ,
                                       IDADE_METABOLICA ,
                                       TAXA_BASAL ,
                                       MASSA_CORPORAL ,
                                       QUALIDADE_MUSCULAR , 
                                       QUALIDADE_MUSCULAR_PD , 
                                       QUALIDADE_MUSCULAR_PE , 
                                       QUALIDADE_MUSCULAR_BD , 
                                       QUALIDADE_MUSCULAR_BE , 
                                       QUALIDADE_MUSCULAR_T ,
                                       PHYSIQUE_RATING ,
                                       DATA_AVALIACAO,
                                       ID_PACIENTE,
                                       ID_USUARIO,
                                       ID_EMPRESA)  
                                       OUTPUT INSERTED.ID
                                       VALUES (
                                       '{bioimpedancia.Nome}', 
                                        {bioimpedancia.Idade},
                                       '{bioimpedancia.Sexo}', 
                                        {bioimpedancia.Peso.ToString().Replace(',', '.')},
                                        {bioimpedancia.Altura.ToString().Replace(',', '.')},
                                        {bioimpedancia.NivelGorduraTotal.ToString().Replace(',', '.')},
                                        {bioimpedancia.NivelGorduraPD.ToString().Replace(',', '.')},
                                        {bioimpedancia.NivelGorduraPE.ToString().Replace(',', '.')},
                                        {bioimpedancia.NivelGorduraBD.ToString().Replace(',', '.')},
                                        {bioimpedancia.NivelGorduraBE.ToString().Replace(',', '.')},
                                        {bioimpedancia.NivelGorduraT.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaAdiposaTotal.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaAdiposaPD.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaAdiposaPE.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaAdiposaBD.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaAdiposaBE.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaAdiposaT.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaNAdiposaTotal.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaNAdiposaPD.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaNAdiposaPE.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaNAdiposaBD.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaNAdiposaBE.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaNAdiposaT.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaMuscularTotal.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaMuscularPD.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaMuscularPE.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaMuscularBD.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaMuscularBE.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaMuscularT.ToString().Replace(',', '.')},
                                        {bioimpedancia.AguaCorporal.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaOssea.ToString().Replace(',', '.')},
                                        {bioimpedancia.IngestaoCalorica.ToString().Replace(',', '.')},
                                        {bioimpedancia.GorduraViceral.ToString().Replace(',', '.')},
                                        {bioimpedancia.IdadeMetabolica.ToString().Replace(',', '.')},
                                        {bioimpedancia.TaxaBasal.ToString().Replace(',', '.')},
                                        {bioimpedancia.MassaCorporal.ToString().Replace(',', '.')},
                                        {bioimpedancia.QualidadeMuscular.ToString().Replace(',', '.')},          
                                        {bioimpedancia.QualidadeMuscularPD.ToString().Replace(',', '.')},          
                                        {bioimpedancia.QualidadeMuscularPE.ToString().Replace(',', '.')},          
                                        {bioimpedancia.QualidadeMuscularBD.ToString().Replace(',', '.')},          
                                        {bioimpedancia.QualidadeMuscularBE.ToString().Replace(',', '.')},          
                                        {bioimpedancia.QualidadeMuscularT.ToString().Replace(',', '.')}, 
                                        {bioimpedancia.PhysiqueRating.ToString().Replace(',', '.')}, 
                                       '{dataAvaliacao}',                                       
                                       '{bioimpedancia.IdPaciente}',
                                       '{bioimpedancia.IdUsuario}',
                                       '{bioimpedancia.IdEmpresa}')";

                    using (SqlCommand command = new SqlCommand(query, connection))
                    {
                        bioimpedancia.Id = (Guid)command.ExecuteScalar();
                    }
                }

                return bioimpedancia;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void Alterar(BioimpedanciaModel bioimpedancia)
        {
            try
            {                
                string dataAvaliacao = bioimpedancia.DataAvaliacao.ToString("yyyy-MM-dd HH:mm:ss");

                using (SqlConnection connection = new SqlConnection(sqlConnection.ToString()))
                {
                    connection.Open();

                    String query = $@" UPDATE BIOIMPEDANCIA SET 
                                       NOME = '{bioimpedancia.Nome}',              
                                       IDADE = {bioimpedancia.Idade},              
                                       SEXO = '{bioimpedancia.Sexo}',              
                                       PESO = {bioimpedancia.Peso.ToString().Replace(',', '.')},
                                       ALTURA = {bioimpedancia.Altura.ToString().Replace(',', '.')},
                                       NIVEL_GORDURA_TOTAL = {bioimpedancia.NivelGorduraTotal.ToString().Replace(',','.')},
                                       NIVEL_GORDURA_PD    = {bioimpedancia.NivelGorduraPD.ToString().Replace(',','.')},
                                       NIVEL_GORDURA_PE    = {bioimpedancia.NivelGorduraPE.ToString().Replace(',','.')},
                                       NIVEL_GORDURA_BD    = {bioimpedancia.NivelGorduraBD.ToString().Replace(',','.')},
                                       NIVEL_GORDURA_BE    = {bioimpedancia.NivelGorduraBE.ToString().Replace(',','.')},
                                       NIVEL_GORDURA_T     = {bioimpedancia.NivelGorduraT.ToString().Replace(',','.')},
                                       MASSA_ADIPOSA_TOTAL = {bioimpedancia.MassaAdiposaTotal.ToString().Replace(',','.')},
                                       MASSA_ADIPOSA_PD    = {bioimpedancia.MassaAdiposaPD.ToString().Replace(',','.')},
                                       MASSA_ADIPOSA_PE    = {bioimpedancia.MassaAdiposaPE.ToString().Replace(',','.')},
                                       MASSA_ADIPOSA_BD    = {bioimpedancia.MassaAdiposaBD.ToString().Replace(',','.')},
                                       MASSA_ADIPOSA_BE    = {bioimpedancia.MassaAdiposaBE.ToString().Replace(',','.')},
                                       MASSA_ADIPOSA_T     = {bioimpedancia.MassaAdiposaT.ToString().Replace(',','.')},
                                       MASSAN_ADIPOSA_TOTAL= {bioimpedancia.MassaNAdiposaTotal.ToString().Replace(',','.')},
                                       MASSAN_ADIPOSA_PD   = {bioimpedancia.MassaNAdiposaPD.ToString().Replace(',','.')},
                                       MASSAN_ADIPOSA_PE   = {bioimpedancia.MassaNAdiposaPE.ToString().Replace(',','.')},
                                       MASSAN_ADIPOSA_BD   = {bioimpedancia.MassaNAdiposaBD.ToString().Replace(',','.')},
                                       MASSAN_ADIPOSA_BE   = {bioimpedancia.MassaNAdiposaBE.ToString().Replace(',','.')},
                                       MASSAN_ADIPOSA_T    = {bioimpedancia.MassaNAdiposaT.ToString().Replace(',','.')},
                                       MASSA_MUSCULAR_TOTAL= {bioimpedancia.MassaMuscularTotal.ToString().Replace(',','.')},
                                       MASSA_MUSCULAR_PD   = {bioimpedancia.MassaMuscularPD.ToString().Replace(',','.')},
                                       MASSA_MUSCULAR_PE   = {bioimpedancia.MassaMuscularPE.ToString().Replace(',','.')},
                                       MASSA_MUSCULAR_BD   = {bioimpedancia.MassaMuscularBD.ToString().Replace(',','.')},
                                       MASSA_MUSCULAR_BE   = {bioimpedancia.MassaMuscularBE.ToString().Replace(',','.')},
                                       MASSA_MUSCULAR_T    = {bioimpedancia.MassaMuscularT.ToString().Replace(',','.')},
                                       AGUA_CORPORAL       = {bioimpedancia.AguaCorporal.ToString().Replace(',','.')},
                                       MASSA_OSSEA         = {bioimpedancia.MassaOssea.ToString().Replace(',','.')},
                                       INGESTAO_CALORICA   = {bioimpedancia.IngestaoCalorica.ToString().Replace(',','.')},
                                       GORDURA_VICERAL     = {bioimpedancia.GorduraViceral.ToString().Replace(',','.')},
                                       IDADE_METABOLICA    = {bioimpedancia.IdadeMetabolica.ToString().Replace(',','.')},
                                       TAXA_BASAL          = {bioimpedancia.TaxaBasal.ToString().Replace(',','.')},
                                       MASSA_CORPORAL      = {bioimpedancia.MassaCorporal.ToString().Replace(',','.')},
                                       QUALIDADE_MUSCULAR  = {bioimpedancia.QualidadeMuscular.ToString().Replace(',','.')},
                                       QUALIDADE_MUSCULAR_PD  = {bioimpedancia.QualidadeMuscularPD.ToString().Replace(',', '.')},
                                       QUALIDADE_MUSCULAR_PE  = {bioimpedancia.QualidadeMuscularPE.ToString().Replace(',', '.')},
                                       QUALIDADE_MUSCULAR_BD  = {bioimpedancia.QualidadeMuscularBD.ToString().Replace(',', '.')},
                                       QUALIDADE_MUSCULAR_BE  = {bioimpedancia.QualidadeMuscularBE.ToString().Replace(',', '.')},
                                       QUALIDADE_MUSCULAR_T  = {bioimpedancia.QualidadeMuscularT.ToString().Replace(',', '.')},
                                       PHYSIQUE_RATING = {bioimpedancia.PhysiqueRating.ToString().Replace(',', '.')},
                                       DATA_AVALIACAO =  '{dataAvaliacao}'";


                    query += $"  WHERE ID = '{bioimpedancia.Id}'";

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
