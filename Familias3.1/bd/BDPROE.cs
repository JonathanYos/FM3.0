using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Familias3._1.bd
{
    public class BDPROE
    {
        static String conexionString;
        public BDPROE()
        {
            conexionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public void ingresarAsistencia(String sitio, String miembro, String fechaAsistencia, String tipoActividad, String fechaCreacion, String usuario, String nota, String impresiones, String fechaFin)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO MemberAssistanceProgram (Project, MemberId, AssistanceDateTime, CreationDateTime, Type, RecordStatus, UserId, Notes, PrintNumber, EndDateTime) 
                            VALUES (@sitio, @miembro, @fechaAsistencia, @fechaCreacion, @tipo, ' ', @usuario, @nota, @impresiones, @fechaFin)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@tipo", tipoActividad);
            comando.Parameters.AddWithValue("@fechaAsistencia", fechaAsistencia);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@nota", nota);
            comando.Parameters.AddWithValue("@impresiones", impresiones);
            if (!fechaFin.Equals(""))
            {
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);
            }
            else
            {
                comando.Parameters.AddWithValue("@fechaFin", DBNull.Value);
            }
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void ingresarHistorico(String sitio, String miembro, String fechaAsistencia, String tipoActividad, String fechaCreacion, String usuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO MemberAssistanceProgram (Project, MemberId, AssistanceDateTime, Type, CreationDateTime, RecordStatus, ExpirationDateTime, UserId, Notes, PrintNumber, EndDateTime) 
                                    SELECT TOP 1 Project, MemberId, AssistanceDateTime, Type, @fechaCreacion, 'H', @fechaCreacion, @usuario, Notes, PrintNumber, EndDateTime
                                    FROM MemberAssistanceProgram
                                    WHERE RecordStatus = ' ' AND Project = @sitio AND MemberId = @miembro AND AssistanceDateTime = @fechaAsistencia AND Type = @tipoActividad";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@fechaAsistencia", fechaAsistencia);
            comando.Parameters.AddWithValue("@tipoActividad", tipoActividad);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
            String fechaExpiracion = fechaCreacion;
            convertirAsistenciaHistorico(sitio, miembro, fechaAsistencia, tipoActividad, fechaExpiracion);
        }

        public void ingresarAsistenciaArchivo(String sitio, String miembro, String fechaAsistencia, String tipoActividad, String fechaCreacion, String intUsuario, String nota, String impresiones, String fechaFin)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"DECLARE @usuario VARCHAR(20);
                            SELECT @usuario = EmployeeId FROM FwEmployee WHERE CodeInt = @intUsuario
                            INSERT INTO MemberAssistanceProgram (Project, MemberId, AssistanceDateTime, CreationDateTime, Type, RecordStatus, UserId, Notes, PrintNumber, EndDateTime) 
                            VALUES (@sitio, @miembro, @fechaAsistencia, @fechaCreacion, @tipo, ' ', @usuario, @nota, @impresiones, @fechaFin)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@tipo", tipoActividad);
            comando.Parameters.AddWithValue("@fechaAsistencia", fechaAsistencia);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@intUsuario", intUsuario);
            comando.Parameters.AddWithValue("@nota", nota);
            comando.Parameters.AddWithValue("@impresiones", impresiones);
            if (!fechaFin.Equals(""))
            {
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);
            }
            else
            {
                comando.Parameters.AddWithValue("@fechaFin", DBNull.Value);
            }
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected void convertirAsistenciaHistorico(String sitio, String miembro, String fechaAsistencia, String tipoActividad, String fechaExpiracion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE MemberAssistanceProgram SET RecordStatus = 'H', ExpirationDateTime = @fechaExpiracion
                                    FROM MemberAssistanceProgram
                                    WHERE RecordStatus = ' ' AND Project = @sitio AND MemberId = @miembro AND AssistanceDateTime = @fechaAsistencia AND Type = @tipoActividad";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@fechaAsistencia", fechaAsistencia);
            comando.Parameters.AddWithValue("@tipoActividad", tipoActividad);
            comando.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public int verificarActividad(String sitio, String tipo)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT COUNT(*) AS C FROM CdMemberEducationActivityType CEAT 
                                    WHERE (CEAT.Project = @sitio OR CEAT.Project = '*') AND CEAT.Code = @tipo";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@tipo", tipo);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            int conteo = Int32.Parse(tablaDatos.Rows[0]["C"].ToString());
            return conteo;
        }
        public int verificarUsuario(String sitio, String intUsuario)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT COUNT(*) AS C FROM FwEmployee FE
                                    INNER JOIN FwEmployeeRole FER ON FE.EmployeeId = FER.EmployeeId
                                    WHERE FE.CodeInt = @intUsuario AND FER.Organization = @sitio AND (FER.Role = 'UPAC' OR FER.Role = 'SUPE') AND FER.Status = 'ACTV'";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@intUsuario", intUsuario);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            int conteo = Int32.Parse(tablaDatos.Rows[0]["C"].ToString());
            return conteo;
        }
        public int verificarMiembro(String sitio, String miembro)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT COUNT(*) AS C FROM Member M
                                    INNER JOIN FamilyMemberRelation FMR ON M.RecordStatus = FMR.RecordStatus AND M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND FMR.InactiveReason IS NULL
                                    INNER JOIN Family F ON FMR.RecordStatus = F.RecordStatus AND FMR.Project = F.Project AND FMR.FamilyId = F.FamilyId AND M.LastFamilyId = F.FamilyId 
                                    WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND M.MemberId = @miembro AND F.AffiliationStatus = 'AFIL'";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            int conteo = Int32.Parse(tablaDatos.Rows[0]["C"].ToString());
            return conteo;
        }

//        public DataTable obtenerAsistencias(String sitio, String idioma, String actividad)
//        {
//            DateTime now = DateTime.Now;
//            SqlConnection conexion = new SqlConnection(conexionString);
//            conexion.Open();
//            String comandoString = @"SELECT M.MemberId, CONVERT(VARCHAR, MEA.AssistanceDateTime, 21) AS AssistanceDateTime, MEA.Type, M.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre,
//										CONVERT(VARCHAR, MEA.AssistanceDateTime, 8) AS Hora,
//                                        MEA.Notes AS Notas,
//										CASE WHEN @idioma = 'es' THEN dbo.fn_GEN_CalcularEdad(M.BirthDate) ELSE dbo.fn_GEN_CalculateAge(M.BirthDate) END AS Edad,                   							
//                                        CASE WHEN @idioma = 'es' THEN 
//                    						CASE WHEN (M.AffiliationStatus = 'AFIL')
//                    							THEN 'Apadrinado'
//                    						WHEN (M.AffiliationStatus = 'DESA')
//                    							THEN 'Desafiliado'
//                    						WHEN (M.AffiliationStatus = 'GRAD')
//                    							THEN 'Graduados'
//                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
//                    							THEN 'Afiliado'
//                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
//                    							THEN 'Miembro Familia Desafiliada'
//                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
//                    							THEN 'Miembro Familia Graduada'
//                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
//                    							THEN 'Afiliado (Inactivo)'
//                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
//                    							THEN 'Miembro Familia Desafiliada (Inactivo)'
//                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
//                    							THEN 'Miembro Familia Graduada (Inactivo)'
//                    						END
//                    					ELSE 
//                    				        CASE WHEN (M.AffiliationStatus = 'AFIL')
//                    					        THEN 'Sponsored'
//                    				        WHEN (M.AffiliationStatus = 'DESA')
//                    				        	THEN 'Disaffiliate'
//                    				        WHEN (M.AffiliationStatus = 'GRAD')
//                    					        THEN 'Graduate'
//                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
//                    					        THEN 'Relatives of Affiliate Family'
//                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
//                    					        THEN 'Relatives of Disaffiliate Family'
//                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
//                    					        THEN 'Relatives of Graduate Family'
//                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
//                    					        THEN 'Relatives of Affiliate Family (Inactive)'
//                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
//                    					        THEN 'Relatives of Disaffiliate Family (Inactive)'
//                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
//                    					        THEN 'Relatives of Graduate Family (Inactive)'
//                    				    END 
//                    			END AS TipoMiembro,
//								dbo.fn_GEN_infoUltimoAñoEscolar(@sitio, M.MemberId, @idioma) AS Educacion,
//								dbo.fn_GEN_Semaforo(M.Project, M.MemberId) AS Semaforo,
//								dbo.fn_GEN_TS(F.Project, F.FamilyId) AS TS,
//								M.LastFamilyId AS Familia
//                                FROM MemberAssistanceProgram MEA
//                                INNER JOIN Member M ON MEA.RecordStatus = M.RecordStatus AND MEA.Project = M.Project AND MEA.MemberId = M.MemberId
//                                INNER JOIN CdMemberEducationActivityType cdMEAT ON MEA.Type = cdMEAT.Code
//                                INNER JOIN Family F ON M.RecordStatus = F.RecordStatus AND M.Project = F.Project AND M.LastFamilyId = F.FamilyId
//                                INNER JOIN FamilyMemberRelation FMR ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND M.MemberId = FMR.MemberId AND F.FamilyId = FMR.FamilyId
//                                WHERE MEA.RecordStatus = ' ' AND MEA.Project = @sitio 
//                                AND YEAR(MEA.AssistanceDateTime) = YEAR(GETDATE()) AND MONTH(MEA.AssistanceDateTime) = MONTH(GETDATE()) AND  DAY(MEA.AssistanceDateTime) = DAY(GETDATE())
//                                AND MEA.Type = @actividad
//                                ORDER BY AssistanceDateTime DESC";
//            SqlCommand comando = new SqlCommand(comandoString, conexion);
//            comando.Parameters.AddWithValue("@sitio", sitio);
//            comando.Parameters.AddWithValue("@idioma", idioma);
//            comando.Parameters.AddWithValue("@actividad", actividad);
//            SqlDataAdapter adaptador = new SqlDataAdapter();
//            adaptador.SelectCommand = comando;
//            DataTable tablaDatos = new DataTable();
//            adaptador.Fill(tablaDatos);
//            conexion.Close();
//            return tablaDatos;
//        }

        public DataTable obtenerAsistencias(String sitio, String idioma, String actividad, String fechaActividad)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT M.MemberId, CONVERT(VARCHAR, MEA.AssistanceDateTime, 21) AS AssistanceDateTime, CONVERT(VARCHAR, MEA.EndDateTime, 21) AS EndDateTime, MEA.Type, M.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre,
										CONVERT(VARCHAR, MEA.AssistanceDateTime, 8) AS Hora,
                                        CONVERT(VARCHAR, MEA.EndDateTime, 8) AS HoraSalida,
                                        MEA.PrintNumber AS NumeroImpresiones,
                                        MEA.Notes AS Notas,
										CASE WHEN @idioma = 'es' THEN dbo.fn_GEN_CalcularEdad(M.BirthDate) ELSE dbo.fn_GEN_CalculateAge(M.BirthDate) END AS Edad,                   							
                                        CASE WHEN @idioma = 'es' THEN 
                    						CASE WHEN (M.AffiliationStatus = 'AFIL')
                    							THEN 'Apadrinado'
                    						WHEN (M.AffiliationStatus = 'DESA')
                    							THEN 'Desafiliado'
                    						WHEN (M.AffiliationStatus = 'GRAD')
                    							THEN 'Graduados'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Afiliado'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Miembro Familia Desafiliada'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Miembro Familia Graduada'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Afiliado (Inactivo)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Miembro Familia Desafiliada (Inactivo)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Miembro Familia Graduada (Inactivo)'
                    						END
                    					ELSE 
                    				        CASE WHEN (M.AffiliationStatus = 'AFIL')
                    					        THEN 'Sponsored'
                    				        WHEN (M.AffiliationStatus = 'DESA')
                    				        	THEN 'Disaffiliate'
                    				        WHEN (M.AffiliationStatus = 'GRAD')
                    					        THEN 'Graduate'
                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
                    					        THEN 'Relatives of Affiliate Family'
                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
                    					        THEN 'Relatives of Disaffiliate Family'
                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
                    					        THEN 'Relatives of Graduate Family'
                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
                    					        THEN 'Relatives of Affiliate Family (Inactive)'
                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
                    					        THEN 'Relatives of Disaffiliate Family (Inactive)'
                    				        WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
                    					        THEN 'Relatives of Graduate Family (Inactive)'
                    				    END 
                    			END AS TipoMiembro,
								dbo.fn_GEN_infoUltimoAñoEscolar(@sitio, M.MemberId, @idioma) AS Educacion,
								dbo.fn_GEN_Semaforo(M.Project, M.MemberId) AS Semaforo,
								dbo.fn_GEN_TS(F.Project, F.FamilyId) AS TS,
								M.LastFamilyId AS Familia
                                FROM MemberAssistanceProgram MEA
                                INNER JOIN Member M ON MEA.RecordStatus = M.RecordStatus AND MEA.Project = M.Project AND MEA.MemberId = M.MemberId
                                INNER JOIN CdMemberEducationActivityType cdMEAT ON MEA.Type = cdMEAT.Code
                                INNER JOIN Family F ON M.RecordStatus = F.RecordStatus AND M.Project = F.Project AND M.LastFamilyId = F.FamilyId
                                INNER JOIN FamilyMemberRelation FMR ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND M.MemberId = FMR.MemberId AND F.FamilyId = FMR.FamilyId
                                WHERE MEA.RecordStatus = ' ' AND MEA.Project = @sitio 
                                AND YEAR(MEA.AssistanceDateTime) = YEAR(@fechaActividad) AND MONTH(MEA.AssistanceDateTime) = MONTH(@fechaActividad) AND  DAY(MEA.AssistanceDateTime) = DAY(@fechaActividad)
                                AND MEA.Type = @actividad
                                ORDER BY AssistanceDateTime DESC";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@actividad", actividad);
            comando.Parameters.AddWithValue("@fechaActividad", fechaActividad);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public void recuperarEstadoReferencia(String sitio, String miembro, String tipoActividad, String usuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT COUNT(*) AS N FROM MemberEducationReference MER
                                    WHERE MER.RecordStatus = ' ' AND MER.Project = @sitio AND MER.MemberId = @miembro AND MER.EducationActivityType = @tipoActividad AND MER.Status = 'ASIS' AND
                                    1 = (SELECT COUNT(*) FROM MemberAssistanceProgram MAP WHERE MER.RecordStatus = MAP.RecordStatus AND MER.Project = MAP.Project AND MER.MemberId = MAP.MemberId AND MER.EducationActivityType = MAP.Type)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@tipoActividad", tipoActividad);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            int num = Int32.Parse(tablaDatos.Rows[0]["N"].ToString());
            if (num == 1)
            {
                this.cambiarEstadoReferencia(sitio, miembro, tipoActividad, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), "REFE", null, usuario);
            }
        }
        public void cambiarEstadoReferencia(String sitio, String miembro, String tipoActividad, String fechaCreacion, String estado, String notaEncargado, String usuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO MemberEducationReference (Project, MemberId, EducationActivityType, CreationDateTime, UserId, RecordStatus, ExpirationDateTime, ReferenceDateTime, ReferenceBy, Status, StatusDate, Reason, ReferenceNotes, AttendanceNotes, AttendedBy) 
                                    SELECT TOP 1 Project, MemberId, EducationActivityType, @fechaCreacion, @usuario, RecordStatus, ExpirationDateTime, ReferenceDateTime, ReferenceBy, @estado, StatusDate, Reason, ReferenceNotes, " + ( String.IsNullOrEmpty(notaEncargado) ? "AttendanceNotes": "@notaEncargado") + @", AttendedBy
                                    FROM MemberEducationReference
                                    WHERE RecordStatus = ' ' AND Project = @sitio AND MemberId = @miembro AND EducationActivityType = @tipoActividad";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@tipoActividad", tipoActividad);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@estado", estado);
            comando.Parameters.AddWithValue("@notaEncargado", notaEncargado);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public DataTable obtenerReferencias(String sitio, String idioma, String actividad, String fechaAsistencia)
        {
            //AND YEAR(MER.ReferenceDateTime) = YEAR() AND MONTH(MER.ReferenceDateTime) = MONTH(@fechaAsistencia) AND  DAY(MER.ReferenceDateTime) = DAY(@fechaAsistencia)
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT M.MemberId, MER.EducationActivityType, MER.Status, M.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre,
										dbo.fn_GEN_FormatDate(MER.ReferenceDateTime, @idioma) AS Fecha,
                                        MER.ReferenceNotes AS NotasRef,
										CASE WHEN @idioma = 'es' THEN dbo.fn_GEN_CalcularEdad(M.BirthDate) ELSE dbo.fn_GEN_CalculateAge(M.BirthDate) END AS Edad,                   							
                                        CASE WHEN @idioma = 'es' THEN 
                    						CASE WHEN (M.AffiliationStatus = 'AFIL')
                    							THEN 'Apadrinado'
                    						WHEN (M.AffiliationStatus = 'DESA')
                    							THEN 'Desafiliado'
                    						WHEN (M.AffiliationStatus = 'GRAD')
                    							THEN 'Graduados'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Afiliado'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Miembro Familia Desafiliada'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Miembro Familia Graduada'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Afiliado (Inactivo)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Miembro Familia Desafiliada (Inactivo)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Miembro Familia Graduada (Inactivo)'
                    						END
                    					ELSE 
                    						CASE WHEN (M.AffiliationStatus = 'AFIL')
                    							THEN 'Sponsored'
                    						WHEN (M.AffiliationStatus = 'DESA')
                    							THEN 'Disaffiliate'
                    						WHEN (M.AffiliationStatus = 'GRAD')
                    							THEN 'Graduate'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Relatives of Affiliate Family'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Relatives of Disaffiliate Family'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Relatives of Graduate Family'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Relatives of Affiliate Family (Inactive)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Relatives of Disaffiliate Family (Inactive)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Relatives of Graduate Family (Inactive)'
                    					END 
                    			END AS TipoMiembro,
								dbo.fn_GEN_infoUltimoAñoEscolar(@sitio, M.MemberId, @idioma) AS Educacion,
								dbo.fn_GEN_Semaforo(M.Project, M.MemberId) AS Semaforo,
								dbo.fn_GEN_TS(F.Project, F.FamilyId) AS TS,
								M.LastFamilyId AS Familia
                                FROM MemberEducationReference MER
                                INNER JOIN Member M ON MER.RecordStatus = M.RecordStatus AND MER.Project = M.Project AND MER.MemberId = M.MemberId
                                INNER JOIN CdMemberEducationActivityType cdMEAT ON MER.EducationActivityType = cdMEAT.Code
                                INNER JOIN Family F ON M.RecordStatus = F.RecordStatus AND M.Project = F.Project AND M.LastFamilyId = F.FamilyId
                                INNER JOIN FamilyMemberRelation FMR ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND M.MemberId = FMR.MemberId AND F.FamilyId = FMR.FamilyId
                                WHERE MER.RecordStatus = ' ' AND MER.Project = @sitio 
                                AND MER.EducationActivityType = @actividad  AND (MER.Status = 'REFE' OR MER.Status = 'ASIS') AND 0 = (SELECT COUNT(*) FROM MemberAssistanceProgram  MAP WHERE MAP.RecordStatus = MER.RecordStatus AND MAP.Project = MER.Project AND MAP.MemberId = MER.MemberId AND MAP.Type = MER.EducationActivityType AND YEAR(MAP.AssistanceDateTime) = YEAR(@fechaAsistencia) AND MONTH(MAP.AssistanceDateTime) = MONTH(@fechaAsistencia) AND  DAY(MAP.AssistanceDateTime) = DAY(@fechaAsistencia))
                                ORDER BY ReferenceDateTime DESC";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@actividad", actividad);
            comando.Parameters.AddWithValue("@fechaAsistencia", fechaAsistencia);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerReferencias(String sitio, String idioma, String actividad)
        {
            //AND YEAR(MER.ReferenceDateTime) = YEAR() AND MONTH(MER.ReferenceDateTime) = MONTH(@fechaAsistencia) AND  DAY(MER.ReferenceDateTime) = DAY(@fechaAsistencia)
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT M.MemberId, MER.EducationActivityType, MER.Status, M.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre,
										dbo.fn_GEN_FormatDate(MER.ReferenceDateTime, @idioma) AS Fecha,
                                        MER.ReferenceNotes AS NotasRef,
                                        MER.AttendanceNotes AS NotasEnc,
										CASE WHEN @idioma = 'es' THEN dbo.fn_GEN_CalcularEdad(M.BirthDate) ELSE dbo.fn_GEN_CalculateAge(M.BirthDate) END AS Edad,                   							
                                        CASE WHEN @idioma = 'es' THEN 
                    						CASE WHEN (M.AffiliationStatus = 'AFIL')
                    							THEN 'Apadrinado'
                    						WHEN (M.AffiliationStatus = 'DESA')
                    							THEN 'Desafiliado'
                    						WHEN (M.AffiliationStatus = 'GRAD')
                    							THEN 'Graduados'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Afiliado'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Miembro Familia Desafiliada'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Miembro Familia Graduada'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Afiliado (Inactivo)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Miembro Familia Desafiliada (Inactivo)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Miembro Familia Graduada (Inactivo)'
                    						END
                    					ELSE 
                    						CASE WHEN (M.AffiliationStatus = 'AFIL')
                    							THEN 'Sponsored'
                    						WHEN (M.AffiliationStatus = 'DESA')
                    							THEN 'Disaffiliate'
                    						WHEN (M.AffiliationStatus = 'GRAD')
                    							THEN 'Graduate'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Relatives of Affiliate Family'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Relatives of Disaffiliate Family'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Relatives of Graduate Family'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Relatives of Affiliate Family (Inactive)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Relatives of Disaffiliate Family (Inactive)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Relatives of Graduate Family (Inactive)'
                    					END 
                    			END AS TipoMiembro,
								dbo.fn_GEN_infoUltimoAñoEscolar(@sitio, M.MemberId, @idioma) AS Educacion,
								dbo.fn_GEN_Semaforo(M.Project, M.MemberId) AS Semaforo,
								dbo.fn_GEN_TS(F.Project, F.FamilyId) AS TS,
								M.LastFamilyId AS Familia
                                FROM MemberEducationReference MER
                                INNER JOIN Member M ON MER.RecordStatus = M.RecordStatus AND MER.Project = M.Project AND MER.MemberId = M.MemberId
                                INNER JOIN CdMemberEducationActivityType cdMEAT ON MER.EducationActivityType = cdMEAT.Code
                                INNER JOIN Family F ON M.RecordStatus = F.RecordStatus AND M.Project = F.Project AND M.LastFamilyId = F.FamilyId
                                INNER JOIN FamilyMemberRelation FMR ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND M.MemberId = FMR.MemberId AND F.FamilyId = FMR.FamilyId
                                WHERE MER.RecordStatus = ' ' AND MER.Project = @sitio 
                                AND MER.EducationActivityType = @actividad  AND MER.Status IN ('REFE', 'ASIS')
                                ORDER BY ReferenceDateTime DESC";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@actividad", actividad);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerProgramas(String sitio, String idioma, String usuario)
        {

             //cdEAT.Day = '" + obtenerDiaSemana() + @"' AND cdEAT.WorkDay = '" + obtenerJornada() + @"' 
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT cdP.Code, CASE WHEN '" + idioma + @"' = 'es' THEN cdP.DescSpanish ELSE cdP.DescEnglish END Des FROM CdProgram cdP WHERE  cdP.Project = '" + sitio + @"' AND cdP.Active = 1 
                        AND  0 < (SELECT COUNT(*) FROM CdSubProgram cdSP 
						INNER JOIN CdEmployeeSubProgramRelation cdESPR ON cdSP.Code = cdESPR.SubProgram AND cdESPR.EmployeeId = '" + usuario + @"'
                        WHERE cdSP.Program = cdP.Code AND cdSP.Active = cdP.Active AND cdSP.Project = cdP.Project AND cdSP.Active = 1)";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable obtenerSubProgramas(String sitio, String idioma, String programa, String usuario)
        {
            //AND cdEAT.Day = 'LU' AND cdEAT.WorkDay = '" + obtenerJornada() + "'
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT cdSP.Code, CASE WHEN '" + idioma + @"' = 'es' THEN cdSP.DescSpanish ELSE cdSP.DescEnglish END Des FROM CdSubProgram cdSP 
                        INNER JOIN CdEmployeeSubProgramRelation cdESPR ON cdSP.Code = cdESPR.SubProgram AND cdESPR.EmployeeId = '" + usuario + @"'                  
                        WHERE cdSP.Program = '" + programa + @"' AND cdSP.Active = 1 AND cdSP.Project = '" + sitio + @"'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable obtenerValoresPredeterminados(String sitio, String usuario)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT cdSP.Code AS DefaultSubProgram, cdSP.Program AS DefaultProgram FROM CdSubProgram cdSP 
                            INNER JOIN CdEmployeeSubProgramRelation cdESPR ON cdSP.Code = cdESPR.SubProgram AND cdESPR.EmployeeId = '" + usuario + @"' AND cdESPR.IsDefault = 1
                            WHERE cdSP.Project = '" + sitio + @"' AND cdSP.Active = 1";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        
        public DataTable obtenerActividades(String sitio, String idioma, String programa, String subPrograma, Boolean mostrarTodas)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT cdMEAT.Code, CASE WHEN '" + idioma + @"' = 'es' THEN cdMEAT.DescSpanish ELSE cdMEAT.DescEnglish END + '  ' + cdMEAT.Schedule + ' ' + CASE WHEN '" + idioma + @"' = 'es' THEN cdD.DescSpanish ELSE cdD.DescEnglish END Des
                         FROM CdMemberEducationActivityType cdMEAT INNER JOIN CdSubProgram cdSP ON cdMEAT.SubPrograma = cdSP.Code AND cdMEAT.Active = cdSP.Active AND cdSP.Code = '" + subPrograma + @"'
                         INNER JOIN CdDay cdD ON cdMEAT.Day = cdD.Code 
                         INNER JOIN CdProgram cdP ON cdSP.Program = cdP.Code AND cdSP.Active = cdP.Active AND cdP.Project = cdMEAT.Project AND cdP.Code = '" + programa + @"' WHERE cdMEAT.Active = 1 AND cdMEAT.Project = '" + sitio + "' " + (!mostrarTodas?" AND cdMEAT.Day = '" + obtenerDiaSemana() + "'" : "") + @" 
                         ORDER BY CASE WHEN '" + idioma + @"' = 'es' THEN cdMEAT.DescSpanish ELSE cdMEAT.DescEnglish END";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public String obtenerDiaSemana()
        {
            int intDia = (int)DateTime.Now.DayOfWeek;
            String strDia = "";
            switch (intDia)
            {
                case 1:
                    strDia = "LU";
                    break;
                case 2:
                    strDia = "MA";
                    break;
                case 3:
                    strDia = "MI";
                    break;
                case 4:
                    strDia = "JU";
                    break;
                case 5:
                    strDia = "VI";
                    break;
                case 6:
                    strDia = "SA";
                    break;
                case 7:
                    strDia = "DO";
                    break;
                default:
                    strDia = "MA";
                    break;
            }
            return strDia;
        }

        public String obtenerJornada()
        {
            TimeSpan jornadaMatutinaInicio = new TimeSpan(8, 0, 0);
            TimeSpan jornadaMatutinaFin = new TimeSpan(13, 0, 0);
            TimeSpan jornadaVespertinaInicio = new TimeSpan(14, 0, 0);
            TimeSpan jornadaVespertinaFin = new TimeSpan(17, 0, 0);
            if ((jornadaMatutinaInicio <= DateTime.Now.TimeOfDay) && (jornadaMatutinaFin >= DateTime.Now.TimeOfDay))
            {
                return "MATU";
            }
            else if ((jornadaVespertinaInicio <= DateTime.Now.TimeOfDay) && (jornadaVespertinaFin >= DateTime.Now.TimeOfDay))
            {
                return "VESP";
            }
            return "VESP";
        }
    }
}