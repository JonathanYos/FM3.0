using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace Familias3._1.bd
{
    public class BDTS
    {
        static String conexionString;
        static String conexionStringCautionOriginal;
        public BDTS()
        {
            conexionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conexionStringCautionOriginal = ConfigurationManager.ConnectionStrings["XXX"].ConnectionString;
        }

        public DataTable actObtenerActividadesFamiliares(String sitio, String idFamilia, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT FA.Type, FA.ActivityDateTime, FA.CreationDateTime,"
                        + " CASE WHEN '" + idioma + "' = 'es' "
                        + " THEN CDFAT.DescSpanish"
                        + " ELSE CDFAT.DescEnglish"
                        + " END AS Des, dbo.fn_GEN_FormatDate(FA.ActivityDateTime,'" + idioma + "') AS ActivityDateU, FA.UserId, FA.Notes"
                        + " FROM FamilyActivity FA INNER JOIN CdFamilyActivityType CDFAT ON FA.Type = CDFAT.Code AND FA.FamilyId = " + idFamilia + " AND FA.Project = '" + sitio + "' AND FA.RecordStatus = ' ' AND CDFAT.FunctionalArea = 'TS' AND CDFAT.Active = 1"
                        + " ORDER BY FA.ActivityDateTime DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public Boolean actActualizarActividad(String sitio, String familia, String tipo, String fechaActividad, String fechaCreacionHST, String usuario, String nota)
        {
            if (!actVerificarActualizacion(sitio, familia, tipo, fechaActividad, fechaCreacionHST))
            {
                return false;
            }
            DateTime fechaCreacion = DateTime.Now;
            DateTime fechaExpiracion = fechaCreacion;
            actIngresarActividad(sitio, familia, tipo, fechaActividad, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), usuario, nota);
            actPasarHistorico(sitio, familia, tipo, fechaActividad, fechaCreacionHST, fechaExpiracion.ToString("yyyy-MM-dd HH:mm:ss"));
            return true;
        }

        protected Boolean actVerificarActualizacion(String sitio, String idFamilia, String tipo, String fechaActividad, String fechaCreacion)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM FamilyActivity FA "
                         + " WHERE FA.FamilyId = " + idFamilia + " AND FA.Project = '" + sitio + "' AND FA.RecordStatus = ' ' "
                         + " AND FA.Type = '" + tipo + "' AND YEAR(FA.ActivityDateTime) = YEAR('" + fechaActividad + "') AND MONTH(FA.ActivityDateTime) = MONTH('" + fechaActividad + "') AND DAY(FA.ActivityDateTime) = DAY('" + fechaActividad + "') AND FA.CreationDateTime != '" + fechaCreacion + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }

        protected void actIngresarActividad(String sitio, String familia, String tipo, String fechaActividad, String fechaCreacion, String usuario, String nota)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO FamilyActivity (Project, FamilyId, Type, ActivityDateTime, CreationDateTime, RecordStatus, UserId, Notes) 
                            VALUES (@sitio, @familia, @tipo, @fechaActividad, @fechaCreacion, ' ', @usuario, @nota)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@fechaActividad", fechaActividad);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@nota", nota);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected void actIngresarHistoricoActividad(String sitio, String familia, String tipo, String fechaActividad, String fechaCreacion, String fechaCreacionSLCT, String usuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO FamilyActivity (Project, FamilyId, Type, ActivityDateTime, CreationDateTime, RecordStatus, ExpirationDateTime, UserId, Notes) 
                            SELECT Project, FamilyId, Type, ActivityDateTime, @fechaCreacion, 'H', @fechaCreacion, @usuario, Notes
                            FROM FamilyActivity
                            WHERE Project = @sitio AND FamilyId = @familia AND Type = @tipo AND ActivityDateTime = @fechaActividad AND CreationDateTime = @fechaCreacionSLCT";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@fechaActividad", fechaActividad);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaCreacionSLCT", fechaCreacionSLCT);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void actEliminarActividad(String sitio, String familia, String tipo, String fechaActividad, String fechaCreacionSLCT, String usuario)
        {
            DateTime fechaCreacion = DateTime.Now;
            actIngresarHistoricoActividad(sitio, familia, tipo, fechaActividad, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), fechaCreacionSLCT, usuario);
            actPasarHistorico(sitio, familia, tipo, fechaActividad, fechaCreacionSLCT, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        protected void actPasarHistorico(String sitio, String familia, String tipo, String fechaActividad, String fechaCreacion, String fechaExpiracion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE FamilyActivity SET RecordStatus = 'H', ExpirationDateTime = @fechaExpiracion WHERE Project = @sitio AND FamilyId = @familia AND Type = @tipo AND ActivityDateTime = @fechaActividad AND CreationDateTime = @fechaCreacion;";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@fechaActividad", fechaActividad);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public Boolean actNuevaActividad(String sitio, String familia, String tipo, String fechaActividad, String usuario, String nota)
        {
            if (!actVerificarIngreso(sitio, familia, tipo, fechaActividad))
            {
                return false;
            }
            DateTime fechaCreacion = DateTime.Now;
            actIngresarActividad(sitio, familia, tipo, fechaActividad, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), usuario, nota);
            return true;
        }

        protected Boolean actVerificarIngreso(String sitio, String idFamilia, String tipo, String fechaActividad)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM FamilyActivity FA "
                         + " WHERE FA.FamilyId = " + idFamilia + " AND FA.Project = '" + sitio + "' AND FA.RecordStatus = ' '"
                         + " AND FA.Type = '" + tipo + "' AND YEAR(FA.ActivityDateTime) = YEAR('" + fechaActividad + "') AND MONTH(FA.ActivityDateTime) = MONTH('" + fechaActividad + "') AND DAY(FA.ActivityDateTime) = DAY('" + fechaActividad + "')";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }


        public void anlVvnNuevoAnalisis(String sitio, String familia, String usuario, String diagnostico, String nota, String post)
        {
            DateTime ahora = DateTime.Now;
            String strFechaCreacion = ahora.ToString("yyyy-MM-dd HH:mm:ss");
            String strFechaAnalisis = strFechaCreacion;
            int año = ahora.Year;
            String idAnalisis = sitio + familia + "_" + año;
            anlVvnIngresarAnalisis(idAnalisis, sitio, familia, strFechaAnalisis, strFechaCreacion, usuario, diagnostico, nota, post);
        }

        protected void anlVvnIngresarAnalisis(String idAnalisis, String sitio, String familia, String fechaAnalisis, String fechaCreacion, String usuario, String diagnostico, String nota, String post)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"DECLARE @aplica BIT;
                                    SET @aplica = (SELECT TOP 1 cdD.Applies FROM CdFamilyLivingDiagnosis cdD WHERE cdD.Code =  @diagnostico AND (cdD.Project = @sitio OR cdD.Project = '*'));
                                    INSERT INTO FamilyLivingAnalysis (IdAnalysis, Project, FamilyId, AnalysisDateTime, CreationDateTime, RecordStatus, UserId, ExpirationDateTime, Applies, Diagnosis, Notes, PostAnalysis) 
                                    VALUES (@idAnalisis, @sitio, @familia, @fechaAnalisis, @fechaCreacion, ' ', @usuario, NULL, @aplica, @diagnostico, @nota, @post)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idAnalisis", idAnalisis);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@fechaAnalisis", fechaAnalisis);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@diagnostico", diagnostico);
            comando.Parameters.AddWithValue("@nota", nota);
            comando.Parameters.AddWithValue("@post", post);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public DataTable anlVvnObtenerFamiliasTS(String sitio, String idioma, String TS, String region)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"
						  SELECT F.FamilyId, F.Project + CONVERT(VARCHAR, F.FamilyId) AS Familia, CASE WHEN FLA.AnalysisDateTime IS NOT NULL THEN CASE WHEN '" + idioma + "' = 'es' THEN 'Sí' ELSE 'Yes' END ELSE 'No' END AS Tiene, F.Classification AS Clasificacion, dbo.fn_GEN_TS(F.Project, F.FamilyId) AS TS, CASE WHEN '" + idioma + "' = 'es' THEN CGA.DescSpanish ELSE CGA.DescEnglish END AS Area,  F.Address AS Direccion, M.FirstNames + ' ' + M.LastNames AS JefeCasa"
                         + " FROM Family F"
                         + " LEFT JOIN FamilyLivingAnalysis FLA ON F.RecordStatus = FLA.RecordStatus AND F.Project = FLA.Project AND F.FamilyId = FLA.FamilyId AND YEAR(FLA.AnalysisDateTime) = YEAR(GETDATE())"
                         + " LEFT JOIN CdGeographicArea CGA ON CGA.Code = F.Area"
                         + " LEFT JOIN CdGeographicPueblo CGP ON F.Pueblo = CGP.Pueblo"
                         + " LEFT JOIN FamilyMemberRelation FMR ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND F.FamilyId = FMR.FamilyId AND FMR.Type IN ('JEFE', 'JEFM') AND FMR.InactiveReason IS NULL"
                         + " LEFT JOIN FamilyEmployeeRelation FER ON F.RecordStatus = FER.RecordStatus AND F.Project = FER.Project AND F.FamilyId = FER.FamilyId AND FER.EndDate IS NULL"
                         + " LEFT JOIN Member M ON FMR.RecordStatus = M.RecordStatus AND FMR.Project = M.Project AND FMR.MemberId = M.MemberId AND FMR.FamilyId = M.LastFamilyId"
                         + " WHERE F.RecordStatus = ' '"
                         + " AND F.Project = '" + sitio + "'"
                         + " AND F.AffiliationStatus = 'AFIL'"
                         + " AND ((FER.EmployeeId = '" + TS + "') OR ('' = '" + TS + "'))"
                         + " AND ((CGP.Region = '" + region + "') OR ('' = '" + region + "'))"
                         + " ORDER BY TS, Area, F.FamilyId";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable anlVvnObtenerAnalisis(String idioma, String sitio, String familia)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT YEAR(AnalysisDateTime) Año, CASE WHEN FLA.Applies = 1 THEN CASE WHEN '" + idioma + "' = 'es' THEN 'SÍ' ELSE 'YES' END ELSE 'NO' END Aplica"
                        + ", CASE WHEN FLA.PostAnalysis = 0 THEN 'PRE' ELSE 'POST' END AS Tipo"   
                        + ", CASE WHEN '" + idioma + "' = 'es' THEN cdD.DescSpanish ELSE cdD.DescEnglish END AS Diagnostico"
                        + ", cdD.Comments AS Comentario"
                        + ", FLA.Notes Notas"
                        + ", CONVERT(varchar, FS.Quantity) +  ' ' + CASE WHEN '" + idioma + "' = 'es' THEN cdT.DescSpanish ELSE cdT.DescEnglish END + ' (' + CONVERT(varchar, FS.DimensionX) + 'x'+CONVERT(varchar, FS.DimensionY) + ')' Solicitud"
                        + ", CASE WHEN '" + idioma + "' = 'es' THEN cdS.DescSpanish ELSE cdS.DescEnglish END AS Estado, FS.TotalHours 'Hrs Requeridas'"
                        + ", CASE WHEN FS.Exoneration IS NULL OR FS.Exoneration = 0 THEN '' ELSE 'Si' END Exoneracion"
                        + ", FS.HoursWorked 'Hrs Trabajadas'"
                        + " FROM dbo.FamilyLivingAnalysis FLA"
                        + " INNER JOIN dbo.CdFamilyLivingDiagnosis cdD ON cdD.Code = FLA.Diagnosis"
                        + " LEFT JOIN dbo.FamilyAmbFamSolicitude FS ON FLA.IdAnalysis = FS.IdAnalysis AND FLA.RecordStatus = FS.RecordStatus"
                        + " LEFT JOIN dbo.CdFamilyProgramStatus cdS ON cdS.Code = FS.Status"
                        + " LEFT JOIN dbo.CdSolicitudeType cdT ON cdT.Code = FS.Material"
                        + " WHERE FLA.RecordStatus = ' ' AND FLA.Project  = '" + sitio + "' AND FLA.FamilyId = " + familia
                        + " ORDER BY YEAR(AnalysisDateTime) DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable anlVvnObtenerAnalisisAñoActual(String idioma, String sitio, String familia)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT YEAR(AnalysisDateTime) Año, CASE WHEN FLA.Applies = 1 THEN CASE WHEN '" + idioma + "' = 'es' THEN '<b>SÍ</b>' ELSE '<b>YES</b>' END ELSE '<b>NO</b>' END Aplica"
                //+ ", CASE WHEN '" + idioma + "' = 'es' THEN cdD.DescSpanish ELSE cdD.DescEnglish END AS Diagnostico"
                        + ", FLA.Diagnosis AS Diagnostico"
                        + ", cdD.Comments AS Comentario"
                        + ", FLA.Notes Notas"
                        + " FROM dbo.FamilyLivingAnalysis FLA"
                        + " INNER JOIN dbo.CdFamilyLivingDiagnosis cdD ON cdD.Code = FLA.Diagnosis"
                        + " WHERE FLA.RecordStatus = ' ' AND FLA.Project  = '" + sitio + "' AND FLA.FamilyId = " + familia + " AND YEAR(AnalysisDateTime) = YEAR(GETDATE())"
                        + " ORDER BY YEAR(AnalysisDateTime) DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public void anlVvnEliminarAnalisisAñoActual(String sitio, String familia, String usuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO FamilyLivingAnalysis (IdAnalysis, Project, FamilyId, AnalysisDateTime, CreationDateTime, RecordStatus, UserId, ExpirationDateTime, Applies, Diagnosis, Notes, PostAnalysis) 
                                    SELECT FLA.IdAnalysis, FLA.Project, FLA.FamilyId, FLA.AnalysisDateTime, CONVERT(char(26), GETDATE(), 20), 'H', @usuario, CONVERT(char(26), GETDATE(), 20), FLA.Applies, FLA.Diagnosis, FLA.Notes, FLA.PostAnalysis
                                    FROM FamilyLivingAnalysis FLA
                                    WHERE FLA.RecordStatus = ' ' AND YEAR(FLA.AnalysisDateTime) = YEAR(GETDATE()) AND FLA.Project = @sitio AND FamilyId = @familia
                                    UPDATE FamilyLivingAnalysis SET RecordStatus = 'H', ExpirationDateTime = CONVERT(char(26), GETDATE(), 20) WHERE RecordStatus = ' ' AND FamilyId = @familia AND Project = @sitio AND YEAR(AnalysisDateTime) = YEAR(GETDATE()) ";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        //public DataTable añoObtenerAñoInferior(String sitio, String miembro, String año)
        //{
        //    SqlConnection con = new SqlConnection(conexionString);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlDataReader dr;
        //    String sql = @"SELECT MEYA.SchoolYear AS Año, MEYA.Grade AS Grado, MEYA.Status AS Estado FROM MemberEducationYearAfil MEYA WHERE MEYA.RecordStatus = ' ' AND MEYA.Project = '" + sitio + "' AND MEYA.MemberId = " + miembro + " AND MEYA.SchoolYear = (SELECT MAX(MEYA2.SchoolYear) FROM MemberEducationYearAfil MEYA2 WHERE MEYA2.RecordStatus = MEYA.RecordStatus AND MEYA2.Project = MEYA.Project AND MEYA2.MemberId = MEYA.MemberId AND MEYA2.SchoolYear < " + año + ")";
        //    SqlCommand showresult = new SqlCommand(sql, con);
        //    con.Open();
        //    dr = showresult.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    dr.Close();
        //    return dt;
        //}

        public DataTable añoObtenerAñoInferior(String sitio, String miembro, String año)
        {
            SqlConnection conexion = new SqlConnection(conexionStringCautionOriginal);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT MEYA.SchoolYear AS Año, MEYA.Grade AS Grado, MEYA.Status AS Estado FROM MemberEducationYearAfil MEYA 
                            WHERE MEYA.RecordStatus = ' ' AND MEYA.Project = @sitio AND MEYA.MemberId = @miembro AND MEYA.SchoolYear = (SELECT MAX(MEYA2.SchoolYear) FROM MemberEducationYearAfil MEYA2 WHERE MEYA2.RecordStatus = MEYA.RecordStatus AND MEYA2.Project = MEYA.Project AND MEYA2.MemberId = MEYA.MemberId AND MEYA2.SchoolYear < @año)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@año", año);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable añoObtenerAñoSuperior(String sitio, String miembro, String año)
        {
            SqlConnection conexion = new SqlConnection(conexionStringCautionOriginal);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT MEYA.SchoolYear AS Año, MEYA.Grade AS Grado, MEYA.Status AS Estado FROM MemberEducationYearAfil MEYA 
                            WHERE MEYA.RecordStatus = ' ' AND MEYA.Project = @sitio AND MEYA.MemberId = @miembro AND MEYA.SchoolYear = (SELECT MIN(MEYA2.SchoolYear) FROM MemberEducationYearAfil MEYA2 WHERE MEYA2.RecordStatus = MEYA.RecordStatus AND MEYA2.Project = MEYA.Project AND MEYA2.MemberId = MEYA.MemberId AND MEYA2.SchoolYear > @año)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@año", año);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public Boolean añoVerificarAñoCronologicamente(int iteracion, String grado, Boolean esIncremento, String gradoEsperado)
        {
            if (iteracion >= 0)
            {
                if (grado.Equals(gradoEsperado))
                {
                    return true;
                }
                else
                {
                    SqlConnection con = new SqlConnection(conexionString);
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader dr;
                    String sql = "";
                    if (esIncremento)
                    {
                        sql = @"SELECT Code FROM CdGrade CG
                            WHERE CG.PreviousGrade = '" + grado + "'";
                    }
                    else
                    {
                        sql = @"SELECT Code FROM CdGrade CG
                            WHERE CG.NextGrade = '" + grado + "'";
                    }
                    SqlCommand showresult = new SqlCommand(sql, con);
                    con.Open();
                    dr = showresult.ExecuteReader();
                    DataTable dt = new DataTable();
                    dt.Load(dr);
                    dr.Close();
                    if (dt.Rows.Count > 0)
                    {
                        String gradoResultado = "";
                        gradoResultado = dt.Rows[0]["Code"].ToString();
                        return añoVerificarAñoCronologicamente(--iteracion, gradoResultado, esIncremento, gradoEsperado);
                    }
                    else
                    {
                        return false;
                    }
                }
            }
            return false;
        }

        public DataTable añoObtenerMiembros(String sitio, String familia, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT M.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre, CASE WHEN '" + idioma + "' = 'es' THEN dbo.fn_GEN_CalcularEdad(M.BirthDate) ELSE dbo.fn_GEN_CalculateAge(M.BirthDate) END AS Edad,"
                        + " CASE WHEN DATEDIFF(mm, M.BirthDate, GETDATE()) < 12"
                        + " THEN DATEDIFF(mm, M.BirthDate, GETDATE())"
                        + " ELSE CONVERT(varchar(50), (DATEDIFF(dd, M.BirthDate, GETDATE())/365)) END AS Age"
                        + "  FROM Member M"
                        + " INNER JOIN FamilyMemberRelation FMR ON M.RecordStatus = FMR.RecordStatus AND M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.LastFamilyId = FMR.FamilyId AND FMR.InactiveDate IS NULL"
                        + " INNER JOIN Family F ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND F.FamilyId = FMR.FamilyId"
                        + " WHERE F.AffiliationStatus = 'AFIL' AND M.AffiliationStatus IS NULL AND FMR.RecordStatus = ' ' AND FMR.Project = '" + sitio + "' AND FMR.FamilyId = '" + familia + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable añoObtenerAñosEscolares(String sitio, String idMiembro, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT MEYA.MemberId, MEYA.CreationDateTime, MEYA.SchoolYear,  M.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre, MEYA.SchoolYear Año,  CASE WHEN '" + idioma + "' = 'es' THEN CG.DescSpanish ELSE CG.DescEnglish END AS Grado, CASE WHEN '" + idioma + "' = 'es' THEN CEC.DescSpanish ELSE CEC.DescEnglish END AS Carrera, CASE WHEN '" + idioma + "' = 'es' THEN CS.DescSpanish ELSE CS.DescEnglish END AS Estado, MEYA.Notes AS Notas, MEYA.ClassSection AS Seccion"
                       + " FROM MemberEducationYearAfil MEYA"
                       + " LEFT JOIN CdGrade CG ON MEYA.Grade = CG.Code"
                       + " LEFT JOIN CdEducationStatus CS ON MEYA.Status = CS.Code"
                       + " LEFT JOIN CdEducationCareer CEC ON MEYA.Career = CEC.Code"
                       + " LEFT JOIN Member M ON M.RecordStatus = MEYA.RecordStatus AND M.Project = MEYA.Project AND M.MemberId = MEYA.MemberId"
                       + " WHERE MEYA.RecordStatus = ' ' AND MEYA.Project = '" + sitio + "' AND M.MemberId = '" + idMiembro + "'"
                       + " ORDER BY MEYA.SchoolYear DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable añoObtenerAñoEscolarEsp(String sitio, String miembro, String año, String fechaCreacionSLCT)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT M.FirstNames + ' ' + M.LastNames AS Nombre, MEYA.SchoolYear AS Año, MEYA.Grade AS Grado, MEYA.Career AS Carrera, MEYA.Status AS Estado, MEYA.Notes AS Notas, MEYA.ClassSection AS Seccion"
                       + " FROM MemberEducationYearAfil MEYA"
                       + " INNER JOIN Member M ON MEYA.RecordStatus = M.RecordStatus AND MEYA.Project = M.Project AND MEYA.MemberId = M.MemberId"
                       + " WHERE MEYA.RecordStatus = ' ' AND MEYA.Project = '" + sitio + "' AND MEYA.MemberId = '" + miembro + "' AND MEYA.CreationDateTime = '" + fechaCreacionSLCT + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public void añoActualizarAñoEscolar(String sitio, String miembro, String añoEscolar, String fechaCreacionSLCT, String usuario, String grado, String estado, String carrera, String notas, String seccion)
        {
            DateTime fechaCreacion = DateTime.Now;
            DateTime fechaExpiracion = fechaCreacion;
            añoIngresarAñoEscolar(sitio, miembro, añoEscolar, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), usuario, grado, estado, carrera, notas, seccion);
            añoPasarHistorico(sitio, miembro, añoEscolar, fechaCreacionSLCT, fechaExpiracion.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        public Boolean añoVerificarActualizacion(String sitio, String idMiembro, String añoEscolar, String fechaCreacion)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM MemberEducationYearAfil MEYA"
                         + " WHERE MEYA.MemberId = " + idMiembro + " AND MEYA.Project = '" + sitio + "' AND MEYA.RecordStatus = ' '"
                         + " AND MEYA.SchoolYear = " + añoEscolar + "  AND MEYA.CreationDateTime != '" + fechaCreacion + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }

        protected void añoIngresarAñoEscolar(String sitio, String miembro, String añoEscolar, String fechaCreacion, String usuario, String grado, String estado, String carrera, String notas, String seccion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO MemberEducationYearAfil (Project, MemberId, SchoolYear, CreationDateTime, RecordStatus, UserId, Grade, Status, Career, Notes, ClassSection) 
                            VALUES (@sitio, @miembro, @añoEscolar, @fechaCreacion, ' ', @usuario, @grado, @estado, @carrera, @notas, @seccion)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@añoEscolar", añoEscolar);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@grado", grado);
            comando.Parameters.AddWithValue("@estado", estado);
            if (!carrera.Equals(""))
            {
                comando.Parameters.AddWithValue("@carrera", @carrera);
            }
            else
            {
                comando.Parameters.AddWithValue("@carrera", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@notas", notas);
            comando.Parameters.AddWithValue("@seccion", seccion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected void añoIngresarHistoricoAño(String sitio, String miembro, String añoEscolar, String fechaCreacion, String fechaCreacionSLCT, String usuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO MemberEducationYearAfil(Project, MemberId, SchoolYear, CreationDateTime, RecordStatus, ExpirationDateTime, UserId, Grade, Status, Career, Notes, ClassSection) 
                            SELECT Project, MemberId, SchoolYear, @fechaCreacion, 'H', ExpirationDateTime, @usuario, Grade, Status, Career, Notes, ClassSection
                            FROM MemberEducationYearAfil 
                            WHERE Project = @sitio AND MemberId = @miembro AND SchoolYear = @añoEscolar AND CreationDateTime = @fechaCreacionSLCT";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@añoEscolar", añoEscolar);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaCreacionSLCT", fechaCreacionSLCT);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void añoEliminarAñoEscolar(String sitio, String miembro, String añoEscolar, String usuario, String fechaCreacionSLCT)
        {
            DateTime fechaCreacion = DateTime.Now;
            añoIngresarHistoricoAño(sitio, miembro, añoEscolar, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), fechaCreacionSLCT, usuario);
            añoPasarHistorico(sitio, miembro, añoEscolar, fechaCreacionSLCT, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"));
        }

        protected void añoPasarHistorico(String sitio, String miembro, String añoEscolar, String fechaCreacion, String fechaExpiracion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE MemberEducationYearAfil SET RecordStatus = 'H', ExpirationDateTime = @fechaExpiracion WHERE Project = @sitio AND MemberId = @miembro AND SchoolYear = @añoEscolar AND CreationDateTime = @fechaCreacion";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@añoEscolar", añoEscolar);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void añoNuevoAño(String sitio, String miembro, String añoEscolar, String usuario, String grado, String estado, String carrera, String notas, String seccion)
        {
            DateTime fechaCreacion = DateTime.Now;
            añoIngresarAñoEscolar(sitio, miembro, añoEscolar, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), usuario, grado, estado, carrera, notas, seccion);
        }

        public Boolean añoVerificarIngreso(String sitio, String idMiembro, String añoEscolar)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT * FROM MemberEducationYearAfil
                            WHERE Project = '" + sitio + "' AND MemberId = '" + idMiembro + "' AND SchoolYear = " + añoEscolar + " AND RecordStatus = ' '";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }

        public int asgTSActualizarAsignacion(String sitio, String familia, String empleado, String fechaCreacion, String usuario, String fechaInicio, String fechaFin, String fechaFinN)
        {
            int soloUnActivo = 0;
            if (String.IsNullOrEmpty(fechaFin))
            {
                if (asgTSExisteActivo(sitio, familia, fechaCreacion) == 0)
                {
                    String fechaCreacionN = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    String fechaExpiracion = fechaCreacionN;
                    asgTSEliminarAsignacion(sitio, familia, empleado, fechaCreacion, fechaExpiracion);
                    asgTSIngresarAsignacion(sitio, familia, fechaCreacionN, empleado, usuario, fechaInicio, fechaFinN);
                    soloUnActivo = 1;
                }
            }
            else if (!String.IsNullOrEmpty(fechaFin))
            {
                if (asgTSExisteActivo(sitio, familia, fechaCreacion) != 0)
                {
                    String fechaCreacionN = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
                    String fechaExpiracion = fechaCreacionN;
                    asgTSEliminarAsignacion(sitio, familia, empleado, fechaCreacion, fechaExpiracion);
                    asgTSIngresarAsignacion(sitio, familia, fechaCreacionN, empleado, usuario, fechaInicio, fechaFinN);
                    soloUnActivo = 1;
                }
            }
            return soloUnActivo;
        }

        protected void asgTSEliminarAsignacion(String sitio, String familia, String empleado, String fechaCreacion, String fechaExpiracion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE FamilyEmployeeRelation SET RecordStatus = 'H', ExpirationDateTime = @fechaExpiracion
                                    WHERE Project = @sitio AND FamilyId = @familia AND EmployeeId = @empleado AND CreationDateTime = @fechaCreacion";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@empleado", empleado);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void asgTSEliminarAsignacion(String sitio, String familia, String empleado, String fechaCreacion)
        {
            String fechaExpiracion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE FamilyEmployeeRelation SET RecordStatus = 'H', ExpirationDateTime = @fechaExpiracion
                                    WHERE Project = @sitio AND FamilyId = @familia AND EmployeeId = @empleado AND CreationDateTime = @fechaCreacion";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@empleado", empleado);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected void asgTSIngresarAsignacion(String sitio, String familia, String fechaCreacion, String empleado, String usuario, String fechaInicio, String fechaFin)
        {
            String rolTS = "TS";
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO FamilyEmployeeRelation (Project, FamilyId, EmployeeId, CreationDateTime, RecordStatus, UserId, Role, StartDate, EndDate) 
                            VALUES (@sitio, @familia, @empleado, @fechaCreacion, ' ', @usuario, @rol, @fechaInicio, @fechaFin)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@empleado", empleado);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@rol", rolTS);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
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

        public Boolean asgTSNuevaAsignacion(String sitio, String familia, String empleado, String usuario, String fechaInicio)
        {
            if (!asgTSVerificarIngreso(sitio, familia, empleado))
            {
                return false;
            }
            String fechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            if (fechaInicio.Equals(""))
            {
                fechaInicio = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            }
            DataTable dtAsignacionActiva = this.asgTSObtenerAsignacionActiva(sitio, familia);
            if (dtAsignacionActiva.Rows.Count > 0)
            {
                DataRow rowAsignacionActiva = dtAsignacionActiva.Rows[0];
                String empleadoACTV = rowAsignacionActiva["EmployeeId"].ToString();
                DateTime fechaCreacionACTV = Convert.ToDateTime(rowAsignacionActiva["CreationDateTime"].ToString());
                String rolACTV = rowAsignacionActiva["Role"].ToString();
                DateTime fechaInicioACTV = Convert.ToDateTime(rowAsignacionActiva["StartDate"].ToString());
                String fechaFinACTV = fechaInicio;
                String fechaExpiracion = fechaCreacion;
                this.asgTSIngresarAsignacion(sitio, familia, fechaCreacion, empleadoACTV, usuario, fechaInicioACTV.ToString("yyyy-MM-dd HH:mm:ss"), fechaFinACTV);
                this.asgTSEliminarAsignacion(sitio, familia, empleadoACTV, fechaCreacionACTV.ToString("yyyy-MM-dd HH:mm:ss"), fechaExpiracion);
            }
            this.asgTSIngresarAsignacion(sitio, familia, fechaCreacion, empleado, usuario, fechaInicio, "");
            return true;
        }

        protected DataTable asgTSObtenerAsignacionActiva(String sitio, String familia)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT EmployeeId, CreationDateTime, Role, StartDate "
                        + " FROM FamilyEmployeeRelation FER "
                        + " WHERE FER.Project = '" + sitio + "' AND FER.FamilyId = " + familia + " AND FER.RecordStatus = ' ' AND FER.EndDate IS NULL";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public int asgTSExisteActivo(String sitio, String familia, String fechaCreacion)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT COUNT(*) AS existeActivo FROM FamilyEmployeeRelation FER "
                        + "WHERE FER.Project = '" + sitio + "' AND FER.FamilyId = " + familia + " AND RecordStatus = ' '"
                        + "AND FER.CreationDateTime != '" + fechaCreacion + "' AND FER.EndDate IS NULL";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            int existeActivo = (int)dt.Rows[0]["existeActivo"];
            return existeActivo;
        }

        public DataTable asgTSObtenerAsignaciones(String sitio, String idFamilia, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT FER.CreationDateTime, FER.EmployeeId, FER.StartDate, FER.EndDate, CASE WHEN FER.EndDate IS NULL THEN FER.EmployeeId + ' (' + CASE WHEN '" + idioma + "' = 'es' THEN 'Activo' ELSE 'Active' END + ')' ELSE FER.EmployeeId END AS EmployeeIdU, dbo.fn_GEN_FormatDate(FER.StartDate,'" + idioma + "') AS StartDateU, dbo.fn_GEN_FormatDate(FER.EndDate,'" + idioma + "') AS EndDateU"
                        + " FROM FamilyEmployeeRelation FER"
                        + " WHERE FER.FamilyId = " + idFamilia + " AND Project = '" + sitio + "' AND RecordStatus = ' '"
                        + " ORDER BY FER.StartDate DESC, FER.CreationDateTime DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        protected Boolean asgTSVerificarIngreso(String sitio, String idFamilia, String TS)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM FamilyEmployeeRelation FER "
                         + " WHERE FER.FamilyId = " + idFamilia + " AND FER.Project = '" + sitio + "' AND FER.RecordStatus = ' '"
                         + " AND FER.EmployeeId = '" + TS + "' AND FER.EndDate IS NULL";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }
        //public DataTable asgTSObtenerFamiliasSinTS(String sitio, String idioma)
        //{
        //    SqlConnection con = new SqlConnection(conexionString);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlDataReader dr;
        //    String sql = @"SELECT F.Project, F.FamilyId, M.FirstNames + ' ' + M.LastNames, F.Address, CASE WHEN '" + idioma + "' = 'es' THEN CFMRT.DescSpanish ELSE CFMRT.DescEnglish END  FROM Family F "
        //                + " INNER JOIN FamilyMemberRelation FMR ON F.Project = FMR.Project AND F.FamilyId = FMR.FamilyId AND F.RecordStatus = FMR.RecordStatus AND F.RecordStatus = ' '"
        //                + " INNER JOIN CdFamilyMemberRelationType CFMRT ON CFMRT.Code = FMR.Type AND CFMRT.Code IN ('1JEF','1JEM','3VOA','3VOO','JEFE','JEFM') AND F.Project IN ('F','M','R','N')"
        //                + " INNER JOIN Member M ON M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.RecordStatus = FMR.RecordStatus"
        //                + " WHERE 0 = (SELECT COUNT(*) FROM FamilyEmployeeRelation FER WHERE FER.Project = F.Project AND FER.FamilyId = F.FamilyId AND FER.RecordStatus = F.RecordStatus AND FER.EndDate IS NULL) "
        //                + " AND F.AffiliationStatus = 'AFIL' AND F.Project = '" + sitio +"' "
        //                + " ORDER BY F.Project,F.FamilyId ";
        //    SqlCommand showresult = new SqlCommand(sql, con);
        //    con.Open();
        //    dr = showresult.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    dr.Close();
        //    return dt;
        //}


        public void avsIngresarNota(String sitio, String familia, String usuario, String nota)
        {
            DateTime ahora = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO MiscFamilyInfo (Project, FamilyId, CreationDateTime, RecordStatus, UserId, TSNotes, SignedAgreement) 
                            SELECT Project, FamilyId, @ahora, ' ', @usuario, @nota, SignedAgreement
                            FROM MiscFamilyInfo
                            WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@ahora", ahora.ToString("yyyy-MM-dd HH:mm:ss"));
            comando.Parameters.AddWithValue("@nota", nota);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public String avsObtenerNota(String sitio, String idFamilia)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT TSNotes Nota"
                        + " FROM MiscFamilyInfo MFI "
                        + " WHERE MFI.RecordStatus = ' ' AND MFI.FamilyId = " + idFamilia + " AND MFI.Project = '" + sitio + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            String nota = "";
            if (dt.Rows.Count > 0)
            {
                nota = dt.Rows[0]["Nota"].ToString();
            }
            return nota;
        }

        public DataTable avsObtenerAvisosFamiliares(String sitio, String idFamilia, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT FW.FunctionalArea, FW.Warning, CONVERT(char(26), FW.WarningDate, 21) AS WarningDate, CONVERT(char(26), FW.CreationDateTime, 21) AS CreationDateTime,"
                        + " CASE WHEN '" + idioma + "' = 'es' "
                        + " THEN CFWT.DescSpanish"
                        + " ELSE CFWT.DescEnglish"
                        + " END AS Des, dbo.fn_GEN_FormatDate(FW.WarningDate,'" + idioma + "') AS WarningDateU, FW.UserId, FW.Inactive"
                        + " FROM FamilyWarning FW INNER JOIN CdFamilyWarningType CFWT ON FW.Warning = CFWT.Code AND FW.FamilyId = " + idFamilia + " AND FW.Project = '" + sitio + "' AND FW.RecordStatus = ' '"
                        + " ORDER BY FW.WarningDate DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public Boolean avsActualizarAviso(String sitio, String familia, String area, String aviso, String fechaAviso, String fechaCreacionHST, String usuario, Boolean inactivo)
        {
            if (!avsVerificarActualizacion(sitio, familia, aviso, fechaAviso, fechaCreacionHST))
            {
                return false;
            }
            DateTime fechaCreacion = DateTime.Now;
            DateTime fechaExpiracion = fechaCreacion;
            avsIngresarAviso(sitio, familia, area, aviso, fechaAviso, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), usuario, inactivo);
            avsPasarHistorico(sitio, familia, area, aviso, fechaAviso, fechaCreacionHST, fechaExpiracion.ToString("yyyy-MM-dd HH:mm:ss"));
            return true;
        }



        protected Boolean avsVerificarActualizacion(String sitio, String idFamilia, String aviso, String fechaAviso, String fechaCreacion)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM FamilyWarning FW "
                         + " WHERE FW.FamilyId = " + idFamilia + " AND FW.Project = '" + sitio + "' AND FW.RecordStatus = ' ' "
                         + " AND FW.Warning = '" + aviso + "' AND FW.FunctionalArea = 'TS' AND YEAR(FW.WarningDate) = YEAR('" + fechaAviso + "') AND MONTH(FW.WarningDate) = MONTH('" + fechaAviso + "') AND DAY(FW.WarningDate) = DAY('" + fechaAviso + "') AND FW.CreationDateTime != '" + fechaCreacion + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }


        protected void avsIngresarAviso(String sitio, String familia, String area, String aviso, String fechaAviso, String fechaCreacion, String usuario, Boolean inactivo)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO FamilyWarning (Project, FamilyId, FunctionalArea, Warning, WarningDate, CreationDateTime, RecordStatus, UserId, Inactive) 
                            VALUES (@sitio, @familia, @area, @aviso, @fechaAviso, @fechaCreacion, ' ', @usuario, @inactivo)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@area", area);
            comando.Parameters.AddWithValue("@aviso", aviso);
            comando.Parameters.AddWithValue("@fechaAviso", fechaAviso);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@inactivo", inactivo);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected void avsIngresarHistoricoAviso(String sitio, String familia, String area, String aviso, String fechaAviso, String fechaCreacion, String fechaCreacionSLCT, String usuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO FamilyWarning (Project, FamilyId, FunctionalArea, Warning, WarningDate, CreationDateTime, RecordStatus, ExpirationDateTime, UserId, Inactive) 
                            SELECT Project, FamilyId, FunctionalArea, Warning, WarningDate, @fechaCreacion, 'H', @fechaCreacion, @usuario, Inactive
                            FROM FamilyWarning 
                            WHERE Project = @sitio AND FamilyId = @familia AND FunctionalArea = @area AND Warning = @aviso AND WarningDate = @fechaAviso AND CreationDateTime = @fechaCreacionSLCT";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@area", area);
            comando.Parameters.AddWithValue("@aviso", aviso);
            comando.Parameters.AddWithValue("@fechaAviso", fechaAviso);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaCreacionSLCT", fechaCreacionSLCT);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public void avsEliminarAviso(String sitio, String familia, String area, String aviso, String fechaAviso, String fechaCreacionSLCT, String usuario)
        {
            DateTime fechaCreacion = DateTime.Now;
            avsIngresarHistoricoAviso(sitio, familia, area, aviso, fechaAviso, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), fechaCreacionSLCT, usuario);
            avsPasarHistorico(sitio, familia, area, aviso, fechaAviso, fechaCreacionSLCT, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"));
        }
        protected void avsPasarHistorico(String sitio, String familia, String area, String aviso, String fechaAviso, String fechaCreacion, String fechaExpiracion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE FamilyWarning SET RecordStatus = 'H', ExpirationDateTime = @fechaExpiracion WHERE Project = @sitio AND FamilyId = @familia AND FunctionalArea = @area AND Warning = @aviso AND WarningDate = @fechaAviso AND CreationDateTime = @fechaCreacion;";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@area", area);
            comando.Parameters.AddWithValue("@aviso", aviso);
            comando.Parameters.AddWithValue("@fechaAviso", fechaAviso);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public Boolean avsNuevoAviso(String sitio, String familia, String area, String aviso, String fechaAviso, String usuario)
        {
            if (!avsVerificarIngreso(sitio, familia, aviso))
            {
                return false;
            }
            DateTime fechaCreacion = DateTime.Now;
            avsIngresarAviso(sitio, familia, area, aviso, fechaAviso, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), usuario, false);
            return true;
        }
        protected Boolean avsVerificarIngreso(String sitio, String idFamilia, String aviso)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM FamilyWarning FW "
                         + " WHERE FW.FamilyId = " + idFamilia + " AND FW.Project = '" + sitio + "' AND FW.RecordStatus = ' '"
                         + " AND FW.Warning = '" + aviso + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }

        public DataTable famPorTSPorAldea(String sitio, String TS)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT CDGA.Code, (SELECT COUNT(*) FROM Family F "
                       + " LEFT JOIN FamilyEmployeeRelation FER ON F.RecordStatus = FER.RecordStatus AND F.Project = FER.Project AND F.FamilyId = FER.FamilyId AND FER.EndDate IS NULL "
                       + " WHERE F.RecordStatus = ' ' AND F.Project = CDGA.Project AND F.Area = CDGA.Code AND"
                       + " F.AffiliationStatus = 'AFIL' AND (FER.EmployeeId = '" + TS + "' OR '*' = '" + TS + "')) AS Num"
                       + " FROM CdGeographicArea CDGA "
                       + " WHERE  CDGA.Project = '" + sitio + "' AND Active = 1"
                       + " ORDER BY CDGA.Code ";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable famPorTS(String sitio, String TS)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT COUNT(*) AS Num FROM FamilyEmployeeRelation FER"
                        + " INNER JOIN Family F ON FER.RecordStatus = F.RecordStatus AND FER.FamilyId = F.FamilyId AND FER.Project = F.Project AND FER.EndDate IS NULL"
                        + " WHERE F.RecordStatus = ' ' AND F.AffiliationStatus = 'AFIL' AND F.Project = '" + sitio + "' AND (FER.EmployeeId = '" + TS + "' OR '*' = '" + TS + "')";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }


        public DataTable famSinTS(String sitio)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT Code, ( "
                          + "      SELECT COUNT(*)    "
                          + "      FROM Family F   "
                          + "      WHERE F.RecordStatus = ' ' AND F.Project = cdGA.Project AND F.AffiliationStatus = 'AFIL' AND F.Area = cdGA.Code AND 0 = (SELECT COUNT(*) FROM FamilyEmployeeRelation FER WHERE FER.RecordStatus = F.RecordStatus AND FER.Project = F.Project AND     FER.FamilyId = F.FamilyId )) AS Num"
                          + "  FROM  CdGeographicArea cdGA WHERE"
                          + "  cdGA.Project = '" + sitio + "' AND cdGA.Active = 1"
                          + "  ORDER BY cdGA.Code";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public String famTotalSinTS(String sitio)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT COUNT(*) AS Num FROM Family F WHERE F.RecordStatus = ' ' AND F.Project = 'F' AND F.AffiliationStatus = 'AFIL' AND 0 = (SELECT COUNT(*) FROM FamilyEmployeeRelation FER WHERE FER.RecordStatus = F.RecordStatus AND FER.Project = F.Project AND FER.FamilyId = F.FamilyId)";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt.Rows[0]["Num"].ToString();
        }

        public DataTable famTotalFamiliasporArea(String sitio)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT Code, (SELECT COUNT(*) FROM Family F WHERE F.RecordStatus = ' ' AND F.Project = cdGA.Project AND F.AffiliationStatus = 'AFIL' AND F.Area = cdGA.Code) AS Num"
                        + "	FROM  CdGeographicArea cdGA"
                        + "	WHERE cdGA.Project = '" + sitio + "' AND cdGA.Active = 1"
                        + " ORDER BY cdGA.Code";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public String famTotal(String sitio)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT COUNT(*) AS Num FROM Family F WHERE F.RecordStatus = ' ' AND F.Project = 'F' AND F.AffiliationStatus = 'AFIL'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt.Rows[0]["Num"].ToString();
        }

        public void clsHstCambiarClasificacion(String sitio, String familia, String usuario, String clasificacion)
        {
            DateTime ahora = DateTime.Now;
            String fechaCreacion = ahora.ToString("yyyy-MM-dd HH:mm:ss");
            String fechaClasificacion = fechaCreacion;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO Family (Project, FamilyId, CreationDateTime, RecordStatus, UserId, AffiliationLevel, AffiliationStatus, AffiliationStatusDate, Area, Pueblo, Address, TelephoneNumber, Ethnicity, LastUpdateDate, Classification, ClassificationDate, Municipality, TimeOnPlace, NextClassification, RFaroNumber)
                                    SELECT Project, FamilyId, @fechaCreacion, RecordStatus, @usuario, AffiliationLevel, AffiliationStatus, AffiliationStatusDate, Area, Pueblo, Address, TelephoneNumber, Ethnicity, LastUpdateDate, @clasificacion, @fechaClasificacion, Municipality, TimeOnPlace, NextClassification, RFaroNumber
                                    FROM Family
                                    WHERE FamilyId = @familia AND Project = @sitio AND RecordStatus = ' '";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@clasificacion", clasificacion);
            comando.Parameters.AddWithValue("@fechaClasificacion", fechaClasificacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public DataTable clsHistorialClasificacion(String sitio, String familia, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT CASE WHEN FCH.CreationDateTime IS NOT NULL THEN CONVERT(char(26), FCH.CreationDateTime, 21) ELSE CONVERT(char(26), F.CreationDateTime, 21) END AS CreationDateTime, CASE WHEN FCH.YearClassification IS NOT NULL THEN FCH.YearClassification ELSE YEAR(GETDATE()) END AS YearClassification, CASE WHEN FCH.Classification IS NOT NULL THEN FCH.Classification ELSE F.Classification END Classification, CASE WHEN FCH.Classification IS NOT NULL THEN FCH.Inactive ELSE 0 END Inactive, CASE WHEN FCH.YearClassification IS NOT NULL THEN FCH.YearClassification ELSE YEAR(GETDATE()) END AS Año, CASE WHEN FCH.Classification IS NOT NULL THEN FCH.Classification ELSE F.Classification END AS Clasif, CASE WHEN FCH.RegisteredDate IS NOT NULL THEN dbo.fn_GEN_FormatDate(FCH.RegisteredDate,'" + idioma + @"') ELSE dbo.fn_GEN_FormatDate(F.ClassificationDate,'" + idioma + @"') END AS FechaRegistro,
	                        CASE WHEN FCH.Classification IS NOT NULL THEN
                                CASE WHEN (FCH.Inactive = 0) THEN 
		                                CASE WHEN '" + idioma + @"' = 'es'
			                            THEN 'Sí' 
			                            ELSE 'Yes' 
		                                END 
	                            ELSE 
                    		        'No' 
	                            END
                            ELSE 
                                 CASE WHEN '" + idioma + @"' = 'es'
			                            THEN 'Sí' 
			                            ELSE 'Yes' 
		                                END
                            END
                            AS Activa, 
                            CASE WHEN FCH.UserId IS NOT NULL THEN FCH.UserId ELSE '-' END AS Usuario FROM Family F 
							LEFT JOIN FamilyClassificationHistory FCH ON F.RecordStatus = FCH.RecordStatus AND F.Project = FCH.Project AND F.FamilyId = FCH.FamilyId AND FCH.YearClassification <= YEAR(GETDATE()) 
                            WHERE F.RecordStatus = ' ' AND F.Project = '" + sitio + "' AND F.FamilyId = " + familia + @"
                            ORDER BY Año DESC, FCH.RegisteredDate DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public void clsCambiarSiguienteClasificacion(String sitio, String familia, String usuario, String siguienteClasificacion)
        {
            DateTime ahora = DateTime.Now;
            String fechaCreacion = ahora.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO Family (Project, FamilyId, CreationDateTime, RecordStatus, UserId, AffiliationLevel, AffiliationStatus, AffiliationStatusDate, Area, Pueblo, Address, TelephoneNumber, Ethnicity, LastUpdateDate, Classification, ClassificationDate, Municipality, TimeOnPlace, NextClassification, RFaroNumber)
                                    SELECT Project, FamilyId, @fechaCreacion, RecordStatus, @usuario, AffiliationLevel, AffiliationStatus, AffiliationStatusDate, Area, Pueblo, Address, TelephoneNumber, Ethnicity, LastUpdateDate, Classification, ClassificationDate, Municipality, TimeOnPlace, @siguienteClasificacion, RFaroNumber
                                    FROM Family
                                    WHERE FamilyId = @familia AND Project = @sitio AND RecordStatus = ' '";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@siguienteClasificacion", siguienteClasificacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void clsIngresarClasificacion(String sitio, String familia, String clasificacion, String añoClasificacion, String usuario, String inactivo, String camposCondiciones, String valoresCondiciones)
        {
            DateTime fechaCreacion = DateTime.Now;
            DateTime fechaRegistro = fechaCreacion;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"INSERT INTO FamilyClassificationHistory (Project, Familyid, Classification, YearClassification, CreationDateTime, RecordStatus, UserId, RegisteredDate, Inactive, " + camposCondiciones + ")"
                                   + " VALUES (@sitio, @familia, @clasificacion, @añoClasificacion, @fechaCreacion, ' ', @usuario, @fechaRegistro, @inactivo, " + valoresCondiciones + ")";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@clasificacion", clasificacion);
            comando.Parameters.AddWithValue("@añoClasificacion", añoClasificacion);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"));
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@fechaRegistro", fechaRegistro.ToString("yyyy-MM-dd HH:mm:ss"));
            comando.Parameters.AddWithValue("@inactivo", inactivo);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public DataTable clsObtenerFamiliasTS(String sitio, String idioma, String TS, String region)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT CASE WHEN (FCH.YearClassification != (YEAR(GETDATE()) + 1)  OR (FCH.YearClassification IS NULL AND F.Classification IS NOT  NULL)) THEN 'I' ELSE 'A' END AS Accion, F.FamilyId,  F.Project + '' + CONVERT(varchar(10),F.FamilyId) AS Familia, F.Address AS Direccion, CASE WHEN '" + idioma + "' = 'es' THEN CGA.DescSpanish ELSE CGA.DescEnglish END AS Area, M.FirstNames + ' ' + M.LastNames AS JefeCasa, FER.EmployeeId AS TS, F.Classification AS ClasificacionActual, CASE WHEN (YEAR(GETDATE()) + 1) = FCH.YearClassification  THEN FCH.Classification ELSE '-' END AS ClasificacionSiguiente FROM Family F "
                        + " LEFT JOIN FamilyClassificationHistory FCH ON F.Project = FCH.Project AND F.FamilyId = FCH.FamilyId AND F.RecordStatus = FCH.RecordStatus AND FCH.YearClassification = (SELECT MAX(FCH2.YearClassification) FROM FamilyClassificationHistory FCH2 WHERE FCH2.Project = FCH.Project AND FCH2.FamilyId = FCH.FamilyId AND FCH2.RecordStatus = FCH.RecordStatus)"
                        + " INNER JOIN FamilyEmployeeRelation FER ON F.Project = FER.Project AND F.FamilyId = FER.FamilyId AND F.RecordStatus = FER.RecordStatus AND FER.EndDate IS NULL"
                        + " INNER JOIN FamilyMemberRelation FMR ON F.Project = FMR.Project AND F.FamilyId = FMR.FamilyId AND F.RecordStatus = FMR.RecordStatus AND FMR.Type IN ('JEFE','JEFM') AND FMR.InactiveReason IS NULL"
                        + " INNER JOIN Member M ON M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.RecordStatus = FMR.RecordStatus"
                        + " INNER JOIN CdGeographicArea CGA ON F.Area = CGA.Code"
                        + " LEFT JOIN CdGeographicPueblo CGP ON F.Pueblo = CGP.Pueblo"
                        + " WHERE F.RecordStatus = ' ' "
                        + " AND F.AffiliationStatus = 'AFIL'"
                        + " AND F.Project = '" + sitio + "' "
                        + " AND ((FER.EmployeeId = '" + TS + "') OR ('' = '" + TS + "'))"
                         + " AND ((CGP.Region = '" + region + "') OR ('' = '" + region + "'))"
                        + " ORDER BY TS, Accion, Area, F.FamilyId";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable clsObtenerClasificacionSiguienteAño(String sitio, String familia, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT CASE WHEN 0<(SELECT COUNT(*) FROM FamilyClassificationHistory"
                                   + "        WHERE RecordStatus = ' ' AND Project = '" + sitio + "' AND FamilyId = '" + familia + "'"
                                   + "            AND YearClassification = YEAR(GETDATE()) + 1) "
                                   + " THEN (SELECT Classification FROM FamilyClassificationHistory"
                                   + "        WHERE RecordStatus = ' ' AND Project = '" + sitio + "' AND FamilyId = '" + familia + "'"
                                   + "        AND YearClassification = YEAR(GETDATE()) + 1)"
                                   + " ELSE ''"
                                   + " END AS Classification";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable clsObtenerCondiciones(String sitio, String familia, String idioma, int año)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT "
                    + " CASE WHEN FCH.Condition1= CFCC.Code THEN FCH.PointsC1"
                    + " WHEN FCH.Condition2= CFCC.Code THEN FCH.PointsC2"
                    + " WHEN FCH.Condition3= CFCC.Code THEN FCH.PointsC3"
                    + " WHEN FCH.Condition4= CFCC.Code THEN FCH.PointsC4"
                    + " WHEN FCH.Condition5= CFCC.Code THEN FCH.PointsC5"
                    + " WHEN FCH.Condition6= CFCC.Code THEN FCH.PointsC6"
                    + " WHEN FCH.Condition7= CFCC.Code THEN FCH.PointsC7"
                    + " WHEN FCH.Condition8= CFCC.Code THEN FCH.PointsC8"
                    + " WHEN FCH.Condition9= CFCC.Code THEN FCH.PointsC9"
                    + " WHEN FCH.Condition10= CFCC.Code THEN FCH.PointsC10"
                    + " WHEN FCH.Condition11= CFCC.Code THEN FCH.PointsC11"
                    + " WHEN FCH.Condition12= CFCC.Code THEN FCH.PointsC12"
                    + " ELSE 0 END AS Points,"
                    + " CFCC.Code, CFCC.DescSpanish, CFCC.Comments FROM CdFamilyClassificationCondition CFCC"
                    + " LEFT JOIN FamilyClassificationHistory FCH ON FCH.FamilyId = '" + familia + "' AND FCH.Project = '" + sitio + "' AND FCH.RecordStatus = ' ' AND FCH.YearClassification = " + año
                    + " WHERE CFCC.Project = 'F' AND CFCC.Active = 1"
                    + " ORDER BY CFCC.Orden";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable clsObtenerCondicionesHistorial(String sitio, String familia, String idioma, int año, String fechaCreacion)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT "
                    + " CASE WHEN FCH.Condition1= CFCC.Code THEN FCH.PointsC1"
                    + " WHEN FCH.Condition2= CFCC.Code THEN FCH.PointsC2"
                    + " WHEN FCH.Condition3= CFCC.Code THEN FCH.PointsC3"
                    + " WHEN FCH.Condition4= CFCC.Code THEN FCH.PointsC4"
                    + " WHEN FCH.Condition5= CFCC.Code THEN FCH.PointsC5"
                    + " WHEN FCH.Condition6= CFCC.Code THEN FCH.PointsC6"
                    + " WHEN FCH.Condition7= CFCC.Code THEN FCH.PointsC7"
                    + " WHEN FCH.Condition8= CFCC.Code THEN FCH.PointsC8"
                    + " WHEN FCH.Condition9= CFCC.Code THEN FCH.PointsC9"
                    + " WHEN FCH.Condition10= CFCC.Code THEN FCH.PointsC10"
                    + " WHEN FCH.Condition11= CFCC.Code THEN FCH.PointsC11"
                    + " WHEN FCH.Condition12= CFCC.Code THEN FCH.PointsC12"
                    + " ELSE 0 END AS Points,"
                    + " CFCC.Code, CFCC.DescSpanish, CFCC.Comments FROM CdFamilyClassificationCondition CFCC"
                    + " LEFT JOIN FamilyClassificationHistory FCH ON FCH.FamilyId = '" + familia + "' AND FCH.Project = '" + sitio + "' AND FCH.RecordStatus = ' ' AND FCH.YearClassification = " + año + " AND FCH.CreationDateTime = '" + fechaCreacion + "'"
                    + " WHERE CFCC.Project = 'F' AND CFCC.Active = 1"
                    + " ORDER BY CFCC.Orden";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }


        public DataTable clsHstClasificaciones(String sitio, String familia)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT "
                    + " CASE WHEN FCH.Condition1= CFCC.Code THEN FCH.PointsC1"
                    + " WHEN FCH.Condition2= CFCC.Code THEN FCH.PointsC2"
                    + " WHEN FCH.Condition3= CFCC.Code THEN FCH.PointsC3"
                    + " WHEN FCH.Condition4= CFCC.Code THEN FCH.PointsC4"
                    + " WHEN FCH.Condition5= CFCC.Code THEN FCH.PointsC5"
                    + " WHEN FCH.Condition6= CFCC.Code THEN FCH.PointsC6"
                    + " WHEN FCH.Condition7= CFCC.Code THEN FCH.PointsC7"
                    + " WHEN FCH.Condition8= CFCC.Code THEN FCH.PointsC8"
                    + " WHEN FCH.Condition9= CFCC.Code THEN FCH.PointsC9"
                    + " WHEN FCH.Condition10= CFCC.Code THEN FCH.PointsC10"
                    + " WHEN FCH.Condition11= CFCC.Code THEN FCH.PointsC11"
                    + " WHEN FCH.Condition12= CFCC.Code THEN FCH.PointsC12"
                    + " ELSE 0 END AS Points,"
                    + " CFCC.Code, CFCC.DescSpanish, CFCC.Comments FROM CdFamilyClassificationCondition CFCC"
                    + " LEFT JOIN FamilyClassificationHistory FCH ON FCH.FamilyId = '" + familia + "' AND FCH.Project = '" + sitio + "' AND FCH.RecordStatus = ' ' AND FCH.YearClassification = (YEAR(GETDATE()) + 1) "
                    + " WHERE CFCC.Project = 'F' AND CFCC.Active = 1"
                    + " ORDER BY CFCC.Orden";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public String estObtenerNumeroVisitas(String sitio, String usuario, String tipoVisita, String fechaInicio, String fechaFin)
        {
            SqlConnection conexion = new SqlConnection(conexionStringCautionOriginal);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT COUNT(*) Numero FROM FamilyVisit2
                                    WHERE RecordStatus = ' ' AND 
                                    Project = @sitio AND 
                                    ((UserId = @usuario) OR (@usuario IN ('Otros', 'Others') AND 0 = (SELECT COUNT(*) FROM FwEmployeeRole FER
                                                                                    WHERE FER.Status = 'ACTV' AND FER.Role = 'TS' AND FER.EmployeeId = UserId)
                                    ) OR ('Total' = @usuario)) AND 
                                    (VisitType = @tipoVisita OR 'Total' = @tipoVisita) AND
                                    (VisitDate BETWEEN @fechaInicio AND DATEADD(DAY, 1,@fechaFin))";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@tipoVisita", tipoVisita);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@fechaFin", fechaFin);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            String numeroVisitas = tablaDatos.Rows[0]["Numero"].ToString();
            return numeroVisitas;
        }

        public Boolean extVerificarIngreso(String sitio, String familia, String tipo, String fechaCreacion, String fechaInicio, int esActualizacion)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                        + " FROM FamilyAdditionalIncome FAI "
                        + " WHERE FAI.Project = '" + sitio + "' AND FAI.FamilyId = " + familia + " AND FAI.RecordStatus = ' '"
                        + " AND FAI.Type = '" + tipo + "' AND YEAR(FAI.StartDate) = YEAR('" + fechaInicio + "') AND MONTH(FAI.StartDate) = MONTH('" + fechaInicio + "') AND DAY(FAI.StartDate) = DAY('" + fechaInicio + "') AND (FAI.CreationDateTime != '" + fechaCreacion + "' OR 0 = " + esActualizacion + ")";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }

        public void extIngresarIngresoExtra(String sitio, String familia, String tipo, String fechaCreacion, String fechaInicio, String usuario, String fechaFin, String ingresoMensual, String notas)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO FamilyAdditionalIncome (Project, FamilyId, Type, CreationDateTime, StartDate, RecordStatus, UserId, EndDate, MonthlyIncome, Notes) 
                            VALUES (@sitio, @familia, @tipo, @fechaCreacion, @fechaInicio, ' ', @usuario, @fechaFin, @ingresoMensual, @notas)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@usuario", usuario);
            if (!fechaFin.Equals(""))
            {
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);
            }
            else
            {
                comando.Parameters.AddWithValue("@fechaFin", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@ingresoMensual", ingresoMensual);
            comando.Parameters.AddWithValue("@notas", notas);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void extIngresarIngresoExtra(String sitio, String familia, String tipo, String fechaCreacion, String fechaInicio, String usuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO FamilyAdditionalIncome (Project, FamilyId, Type, CreationDateTime, StartDate, RecordStatus, ExpirationDateTime, UserId, EndDate, MonthlyIncome, Notes) 
                                    SELECT TOP 1 Project, FamilyId, Type, @fechaCreacion, StartDate,  'H', @fechaCreacion, @usuario, EndDate, MonthlyIncome, Notes
                                    FROM FamilyAdditionalIncome
                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND Type = @tipo AND StartDate = @fechaInicio";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
            String fechaExpiracion = fechaCreacion;
            extConvertirIngresoExtraAHistorico(sitio, familia, tipo, fechaInicio, fechaExpiracion);
        }

        public void extConvertirIngresoExtraAHistorico(String sitio, String familia, String tipo, String fechaInicio, String fechaExpiracion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE FamilyAdditionalIncome SET RecordStatus = 'H', ExpirationDateTime = @fechaExpiracion
                                    FROM FamilyAdditionalIncome
                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND Type = @tipo AND StartDate = @fechaInicio";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public DataTable extobtenerIngresosExtras(String sitio, String familia, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT  CONVERT(char(26), FAI.CreationDateTime, 21) AS CreationDateTime, CAIT.Code, CONVERT(char(26), FAI.StartDate, 21) AS StartDate, CONVERT(char(26), FAI.EndDate) AS EndDate, FAI.MonthlyIncome, FAI.Notes, CASE WHEN '" + idioma + "'  = 'es'"
                        + " THEN CAIT.DescSpanish"
                        + " ELSE CAIT.DescEnglish"
                        + " END AS Des,"
                        + " (SELECT dbo.fn_GEN_FormatDate(FAI.StartDate,'" + idioma + "')) AS StartDateU,"
                        + " (SELECT dbo.fn_GEN_FormatDate(FAI.EndDate,'" + idioma + "')) AS EndDateU, FAI.UserId"
                        + " FROM FamilyAdditionalIncome FAI"
                        + " INNER JOIN CdAdditionalIncomeType CAIT ON CAIT.Code = FAI.Type"
                        + " WHERE FAI.RecordStatus = ' ' AND FAI.Project = '" + sitio + "' AND FAI.FamilyId = '" + familia + "'"
                        + " ORDER BY FAI.StartDate DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }


        public Boolean ocpActualizarOcupacion(String sitio, String miembro, String ocupacion, String fechaCreacion, String fechaInicio, String nuevaFechaInicio, String usuario, String fechaFin, String horasSemanales, String razonFin, String ingresos, String ingresosSemanal, String cantSemanas, String jornada, String lugarTrabajo, Boolean tieneIGGS, Boolean tieneConstancia, String aporte)
        {
            if (!ocpVerificarActualizacion(sitio, miembro, ocupacion, fechaInicio, fechaCreacion))
            {
                return false;
            }
            ocpPasarHistorico(sitio, miembro, ocupacion, fechaCreacion, fechaInicio);
            ocpIngresarOcupacion(sitio, miembro, ocupacion, nuevaFechaInicio, usuario, fechaFin, horasSemanales, razonFin, ingresos, ingresosSemanal, cantSemanas, jornada, lugarTrabajo, tieneIGGS, tieneConstancia, aporte);
            return true;
        }

        public void ocpEliminarOcupacion(String sitio, String miembro, String ocupacion, String fechaCreacion, String fechaInicio, String usuario)
        {
            ocpIngresarOcupacionHistorico(sitio, miembro, ocupacion, fechaInicio, usuario, fechaCreacion);
            ocpPasarHistorico(sitio, miembro, ocupacion, fechaCreacion, fechaInicio);
        }

        public void ocpPasarHistorico(String sitio, String miembro, String ocupacion, String fechaCreacion, String fechaInicio)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"UPDATE MemberOccupation SET RecordStatus = 'H', ExpirationDateTime = @fechaCreacion WHERE Project = @sitio AND MemberId = @miembro AND Occupation = @ocupacion AND CreationDateTime = @fechaCreacion AND StartDate = @fechaInicio;";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@ocupacion", ocupacion);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected Boolean ocpVerificarActualizacion(String sitio, String idMiembro, String ocupacion, String fechaInicio, String fechaCreacion)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM MemberOccupation MO "
                         + " WHERE MO.MemberId = " + idMiembro + " AND MO.Project = '" + sitio + "' AND MO.RecordStatus = ' '"
                         + " AND MO.Occupation = '" + ocupacion + "' AND YEAR(MO.StartDate) = YEAR('" + fechaInicio + "') AND MONTH(MO.StartDate) = MONTH('" + fechaInicio + "') AND DAY(MO.StartDate) = DAY('" + fechaInicio + "') AND MO.CreationDateTime != '" + fechaCreacion + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }
        protected Boolean ocpVerificarIngreso(String sitio, String idMiembro, String ocupacion, String fechaInicio)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                        + " FROM MemberOccupation MO "
                        + " WHERE MO.MemberId = " + idMiembro + " AND MO.Project = '" + sitio + "' AND MO.RecordStatus = ' '"
                        + " AND MO.Occupation = '" + ocupacion + "' AND YEAR(MO.StartDate) = YEAR('" + fechaInicio + "') AND MONTH(MO.StartDate) = MONTH('" + fechaInicio + "') AND DAY(MO.StartDate) = DAY('" + fechaInicio + "')";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }

        public Boolean ocpIngresarOcupacion(String sitio, String miembro, String ocupacion, String fechaInicio, String usuario, String fechaFin, String horasSemanales, String razonFin, String ingresos, String ingresosSemanal, String cantSemanas, String jornada, String lugarTrabajo, Boolean tieneIGGS, Boolean tieneConstancia, String aporte)
        {
            if (!ocpVerificarIngreso(sitio, miembro, ocupacion, fechaInicio))
            {
                return false;
            }
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO MemberOccupation (Project, MemberId, Occupation, CreationDateTime, StartDate, RecordStatus, UserId, EndDate, WeeklyHours, TerminationReason, MonthlyIncome, WeeklyIncome, WorkedWeeks, Workday, WorkPlace, HasIGSSAfil, HasWorkCertificate, MonthlyContribution) 
                            VALUES (@sitio, @miembro, @ocupacion, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', @fechaInicio, ' ', @usuario, @fechaFin, @horasSemanales, @razonFin, @ingresos, @ingresosSemanal, @cantSemanas, @jornada, @lugarTrabajo, @tieneIGGS, @tieneConstancia, @aporte);";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@ocupacion", ocupacion);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@usuario", usuario);
            if (!fechaFin.Equals(""))
            {
                comando.Parameters.AddWithValue("@fechaFin", fechaFin);
            }
            else
            {
                comando.Parameters.AddWithValue("@fechaFin", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@horasSemanales", horasSemanales);

            if (!razonFin.Equals(""))
            {
                comando.Parameters.AddWithValue("@razonFin", razonFin);
            }
            else
            {
                comando.Parameters.AddWithValue("@razonFin", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@ingresos", ingresos);

            if (!ingresosSemanal.Equals(""))
            {
                comando.Parameters.AddWithValue("@ingresosSemanal", ingresosSemanal);
            }
            else
            {
                comando.Parameters.AddWithValue("@ingresosSemanal", DBNull.Value);
            }

            if (!cantSemanas.Equals(""))
            {
                comando.Parameters.AddWithValue("@cantSemanas", cantSemanas);
            }
            else
            {
                comando.Parameters.AddWithValue("@cantSemanas", DBNull.Value);
            }

            if (!jornada.Equals(""))
            {
                comando.Parameters.AddWithValue("@jornada", @jornada);
            }
            else
            {
                comando.Parameters.AddWithValue("@jornada", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@lugarTrabajo", lugarTrabajo);
            comando.Parameters.AddWithValue("@tieneIGGS", tieneIGGS);
            comando.Parameters.AddWithValue("@tieneConstancia", tieneConstancia);
            if (!aporte.Equals(""))
            {
                comando.Parameters.AddWithValue("@aporte", aporte);
            }
            else
            {
                comando.Parameters.AddWithValue("@aporte", DBNull.Value);
            }
            comando.ExecuteNonQuery();
            conexion.Close();
            return true;
        }

        public void ocpIngresarOcupacionHistorico(String sitio, String miembro, String ocupacion, String fechaInicio, String usuario, String fechaCreacion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO MemberOccupation (Project, MemberId, Occupation, CreationDateTime, StartDate, RecordStatus,ExpirationDateTime, UserId, EndDate, WeeklyHours, TerminationReason, MonthlyIncome, WeeklyIncome, Workday, WorkPlace, HasIGSSAfil) 
                            SELECT @sitio, @miembro, @ocupacion, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', @fechaInicio, 'H', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"', @usuario, EndDate, WeeklyHours, TerminationReason, MonthlyIncome, WeeklyIncome, Workday, WorkPlace, HasIGSSAfil
                            FROM MemberOccupation
                            WHERE Project = @sitio AND MemberId = @miembro AND Occupation = @ocupacion AND @fechaInicio = StartDate AND @fechaCreacion = CreationDateTime";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@ocupacion", ocupacion);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public DataTable ocpObtenerMiembros(String sitio, String idFamilia, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT 
                    CASE WHEN FMR.InactiveReason IS NOT NULL
                        THEN 'S'
                        ELSE 'N'
                    END Inactivo, M.MemberId, M.FirstNames + ' ' + M.LastNames AS Nombre,
                    CASE WHEN @idioma = 'es'
                        THEN cdMRT.DescSpanish 
                        ELSE cdMRT.DescEnglish
                    END Relacion,
                    CASE WHEN @idioma = 'es'
                        THEN cdFMRIT.DescSpanish 
                        ELSE cdFMRIT.DescEnglish
                    END RazonInactivo,
                    CASE WHEN @idioma = 'es'
                        THEN cdAS.DescSpanish 
                        ELSE cdAS.DescEnglish
                    END TipoAfil,
                    dbo.fn_GEN_ocupacionesMiembro(@sitio, M.MemberId, @idioma) AS Ocupaciones,
                    (SELECT SUM(MO1.MonthlyIncome) FROM MemberOccupation MO1 WHERE M.RecordStatus = MO1.RecordStatus AND M.Project = MO1.Project AND M.MemberId = MO1.MemberId AND MO1.EndDate IS NULL) AS IngresoMensual,
                    (SELECT SUM(MO2.MonthlyContribution) FROM MemberOccupation MO2 WHERE M.RecordStatus = MO2.RecordStatus AND M.Project = MO2.Project AND M.MemberId = MO2.MemberId AND MO2.EndDate IS NULL) AS AporteMensual
             FROM dbo.Member M 
            INNER JOIN FamilyMemberRelation FMR ON M.RecordStatus = FMR.RecordStatus AND M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND (M.LastFamilyId = FMR.FamilyId OR FMR.InactiveReason IS NOT NULL)
            INNER JOIN CdFamilyMemberRelationType cdMRT ON FMR.Type = cdMRT.Code 
            LEFT JOIN CdFamMemRelInactiveReason cdFMRIT ON FMR.InactiveReason = cdFMRIT.Code
            LEFT JOIN CdAffiliationStatus cdAS ON M.AffiliationStatus = cdAS.Code
            WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND FMR.FamilyId = @idFamilia
            ORDER BY CASE WHEN @idioma = 'es' THEN cdFMRIT.DescSpanish ELSE cdFMRIT.DescEnglish END, cdMRT.DisplayOrder";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idFamilia", idFamilia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable ocpObtenerOcupaciones(String sitio, String idMiembro, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT MO.CreationDateTime, CO.Code, MO.StartDate, CASE WHEN '" + idioma + "'  = 'es'"
                        + " THEN CO.DescSpanish"
                        + " ELSE CO.DescEnglish"
                        + " END AS Des,"
                        + " (SELECT dbo.fn_GEN_FormatDate(MO.StartDate,'" + idioma + "')) AS StartDateU,"
                        + " (SELECT dbo.fn_GEN_FormatDate(MO.EndDate,'" + idioma + "')) AS EndDate, MO.MonthlyIncome, MO.WeeklyIncome, MO.MonthlyContribution, MO.WorkPlace, MO.UserId"
                        + " FROM MemberOccupation MO"
                        + " INNER JOIN CdOccupation CO ON CO.Code = MO.Occupation"
                        + " WHERE MO.RecordStatus = ' ' AND MO.Project = '" + sitio + "' AND MO.MemberId = '" + idMiembro + "'"
                        + " ORDER BY MO.StartDate DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public String ocpObtenerCategoria(String ocupacion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT Category FROM CdOccupation CO
                                    WHERE CO.Code = @ocupacion";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@ocupacion", ocupacion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            if (tablaDatos.Rows.Count > 0)
            {
                String categoria = tablaDatos.Rows[0]["Category"].ToString();
                return categoria;
            }
            else
            {
                return "";
            }
        }

        public DataTable ocpObtenerOcupacionEsp(String sitio, String idMiembro, String ocupacion, String fechaCreacion, String fechaInicio)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT CASE WHEN EndDate IS NOT NULL THEN EndDate ELSE NULL END AS EndDate,"
                + " WeeklyHours, TerminationReason, MonthlyIncome, WeeklyIncome, WorkedWeeks, Workday, WorkPlace, HasIGSSAfil, HasWorkCertificate, MonthlyContribution, cdO.Category"
                        + " FROM MemberOccupation MO"
                        + " INNER JOIN CdOccupation cdO ON MO.Occupation = cdO.Code"
                        + " WHERE MO.Project = '" + sitio + "' AND MO.MemberId = '" + idMiembro + "' AND MO.Occupation = '" + ocupacion + "' AND MO.CreationDateTime = '" + fechaCreacion + "' AND MO.StartDate = '" + fechaInicio + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable hstInfoGeneral(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT  M.MemberId, M.FirstNames + ' ' + M.LastNames AS Nombre
             , CASE WHEN M.AffiliationStatus IS NULL THEN '' ELSE (CASE WHEN @idioma = 'es' THEN cdAS.DescSpanish ELSE  cdAS.DescEnglish END ) + '-' + (CASE WHEN @idioma = 'es' THEN cdAT.DescSpanish ELSE  cdAT.DescEnglish END )  END  AS Afiliación
             , dbo.fn_GEN_FormatDate(M.BirthDate, @idioma) + ' - ' +  dbo.fn_GEN_obtenerEdad(M.Project, M.MemberId, GETDATE(), @idioma)AS Nacimiento
             , CellularPhoneNumber Celular
			 , CASE WHEN @idioma = 'es' THEN cdFMR.DescSpanish ELSE cdFMR.DescEnglish END + ' ' + CASE WHEN FMR.InactiveReason IS NOT NULL THEN CASE WHEN @idioma = 'es' THEN '(Inactivo: ' + cdFMRIR.DescSpanish + ')' ELSE '(Inactive: ' + cdFMRIR.DescEnglish + ')' END ELSE '' END AS Relacion
             FROM Member M
             INNER JOIN FamilyMemberRelation FMR ON M.Project = FMR.Project AND M.RecordStatus = FMR.RecordStatus AND M.MemberId = FMR.MemberId
             AND ((M.LastFamilyId = FMR.FamilyId AND FMR.InactiveReason IS NULL) OR FMR.InactiveReason IS NOT NULL)
             LEFT JOIN CdFamMemRelInactiveReason cdFMRIR ON cdFMRIR.Code = FMR.InactiveReason             
             INNER JOIN CdFamilyMemberRelationType cdFMR ON cdFMR.Code = FMR.Type
             LEFT JOIN dbo.CdAffiliationStatus cdAS ON cdAS.Code = M.AffiliationStatus
             LEFT JOIN dbo.CdAffiliationType cdAT ON cdAT.Code = M.AffiliationType
			 LEFT JOIN MiscMemberInfo MMI ON M.Project = MMI.Project AND M.MemberId = MMI.MemberId AND M.RecordStatus = MMI.RecordStatus AND MMI.TSNotes IS NOT NULL 
             WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND FMR.FamilyId = @familia
             ORDER BY cdFMR.DisplayOrder";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstUltimoAñoEduc(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT M.FirstNames + ' ' + M.LastNames AS  Nombre, dbo.fn_GEN_infoUltimoAñoEscolar(@sitio, M.MemberId, @idioma) AS Informacion
                                    FROM Member M
                                    LEFT JOIN FamilyMemberRelation FMR ON M.RecordStatus = FMR.RecordStatus AND M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.LastFamilyId = FMR.FamilyId AND FMR.InactiveReason IS NULL
                                    WHERE M.RecordStatus = ' ' AND  M.Project = @sitio AND M.LastFamilyId = @familia";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstMedioAmbiente(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT CASE WHEN @idioma = 'es' 
                                    THEN CPO.DescSpanish
                                    ELSE CPO.DescEnglish
                                    END AS Tenencia, 
                                    FLC.PropertySizeX AS TamañoPropiedadX,
                                    FLC.PropertySizeY AS TamañoPropiedadY,
                                    FLC.CultivatedPropertySizeX AS TamañoPropiedadXAreaVerde,
                                    FLC.CultivatedPropertySizeY AS TamañoPropiedadYAreaVerde,
                                    FLC.NumberOfRooms AS NumeroCuartos,
                                    F.Address AS Direccion, 
                                    CASE WHEN @idioma = 'es' 
                                    THEN CM.DescSpanish
                                    ELSE CM.DescEnglish
                                    END AS Municipio,
                                    F.Pueblo AS Pueblo,
                                    F.TimeOnPlace AS TiempoEnLugar,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CWM.DescSpanish
                                    ELSE CWM.DescEnglish
                                    END AS MaterialPared,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CQMat.DescSpanish
                                    ELSE CQMat.DescEnglish
                                    END AS CalidadMaterialPared,
                                    FLC.WallNotes AS NotasPared,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CCM.DescSpanish
                                    ELSE CCM.DescEnglish
                                    END AS MaterialTecho,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CQCei.DescSpanish
                                    ELSE CQCei.DescEnglish
                                    END AS CalidadMaterialTecho,
                                    FLC.CeilingNotes AS NotasTecho,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CFM.DescSpanish
                                    ELSE CFM.DescEnglish
                                    END AS MaterialPiso,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CQFlo.DescSpanish
                                    ELSE CQFlo.DescEnglish
                                    END AS CalidadMaterialPiso,
                                    FLC.FloorNotes AS NotasPiso,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CWMKit.DescSpanish
                                    ELSE CWMKit.DescEnglish
                                    END AS MaterialParedCocina,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CQMatKit.DescSpanish
                                    ELSE CQMatKit.DescEnglish
                                    END AS CalidadMaterialParedCocina,
                                    FLC.KitchenWallNotes AS NotasParedCocina,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CCMKit.DescSpanish
                                    ELSE CCMKit.DescEnglish
                                    END AS MaterialTechoCocina,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CQCeiKit.DescSpanish
                                    ELSE CQCeiKit.DescEnglish
                                    END AS CalidadMaterialTechoCocina,
                                    FLC.KitchenCeilingNotes AS NotasTechoCocina,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CE.DescSpanish
                                    ELSE CE.DescEnglish
                                    END AS Electricidad,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CW.DescSpanish
                                    ELSE CW.DescEnglish
                                    END AS Agua,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CD.DescSpanish
                                    ELSE CD.DescEnglish
                                    END AS Drenaje,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CB.DescSpanish
                                    ELSE CB.DescEnglish
                                    END AS Baño,
                                    FLC.NumberOfRooms AS NumeroCuartos,
                                    CASE WHEN @idioma = 'es' 
                                    THEN 
                                    	CASE WHEN Has2ndFloor = 1 
                                    	THEN 'Sí'
                                    	ELSE 'No'
                                    	END
                                    ELSE 
                                    	CASE WHEN Has2ndFloor = 1  
                                    	THEN 'Yes'
                                    	ELSE 'No'
                                    	END
                                    END AS TieneSegundoNivel,
                                    CASE WHEN @idioma = 'es' 
                                    THEN 
                                    	CASE WHEN HouseDeed = 1 
                                    	THEN 'Sí'
                                    	ELSE 'No'
                                    	END
                                    ELSE 
                                    	CASE WHEN HouseDeed = 1  
                                    	THEN 'Yes'
                                    	ELSE 'No'
                                    	END
                                    END AS TieneEscritura,
                                    CASE WHEN @idioma = 'es' 
                                    THEN CQHyg.DescSpanish
                                    ELSE CQHyg.DescEnglish
                                    END AS Higiene,
                                    FLC.HygieneNotes AS NotasHigiene
                                    FROM Family F 
                                    LEFT JOIN FamilyLivingCondition FLC ON F.RecordStatus = FLC.RecordStatus AND F.FamilyId = FLC.FamilyId AND F.Project = FLC.Project
                                    LEFT JOIN CdPropertyOwnership CPO ON FLC.Ownership = CPO.Code
                                    LEFT JOIN CdMunicipality CM ON F.Municipality = CM.Code
                                    LEFT JOIN CdWallMaterial CWM ON FLC.WallMaterial = CWM.Code
                                    LEFT JOIN CdQuality CQMat ON FLC.WallMaterialQuality = CQMat.Code
                                    LEFT JOIN CdCeilingMaterial CCM ON FLC.CeilingMaterial = CCM.Code
                                    LEFT JOIN CdQuality CQCei ON FLC.CeilingMaterialQuality = CQCei.Code
                                    LEFT JOIN CdFloorMaterial CFM ON FLC.FloorMaterial = CFM.Code
                                    LEFT JOIN CdQuality CQFlo ON FLC.FloorMaterialQuality = CQFlo.Code
                                    LEFT JOIN CdWallMaterial CWMKit ON FLC.WallMaterial = CWMKit.Code
                                    LEFT JOIN CdQuality CQMatKit ON FLC.WallMaterialQuality = CQMatKit.Code
                                    LEFT JOIN CdCeilingMaterial CCMKit ON FLC.CeilingMaterial = CCMKit.Code
                                    LEFT JOIN CdQuality CQCeiKit ON FLC.CeilingMaterialQuality = CQCeiKit.Code
                                    LEFT JOIN CdElectricity CE ON FLC.Electricity = CE.Code
                                    LEFT JOIN CdWater CW ON FLC.Water = CW.Code
                                    LEFT JOIN CdBathroom CB ON FLC.Bathroom = CB.Code
                                    LEFT JOIN CdDrainage CD ON FLC.Drainage = CD.Code
                                    LEFT JOIN CdQuality CQHyg ON FLC.Hygiene = CQHyg.Code
                                    WHERE F.RecordStatus = ' ' AND F.Project = @sitio AND F.FamilyId = @familia AND FLC.Active = 1 ";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstOcupaciones(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT M.FirstNames + ' ' + M.LastNames AS Nombre, CASE WHEN @idioma = 'es' THEN cdMO.DescSpanish ELSE cdMO.DescEnglish END AS Ocupacion, dbo.fn_GEN_FormatDate(MO.StartDate,@idioma) AS FechaInicio,  MO.MonthlyIncome AS IngresosMensuales, CASE WHEN @idioma = 'es' THEN cdWD.DescSpanish ELSE cdWD.DescEnglish END AS Jornada, WeeklyHours AS HorasSemanales, CASE WHEN @idioma = 'es' THEN CASE WHEN MO.HasIGSSAfil = 1 THEN  'Sí' ELSE 'No' END ELSE CASE WHEN MO.HasIGSSAfil = 1 THEN  'Yes' ELSE 'No' END END AS TieneIGGSAfil FROM  Member M
                                    INNER JOIN MemberOccupation MO ON MO.RecordStatus = M.RecordStatus AND MO.Project = M.Project AND MO.MemberId = M.MemberId AND MO.EndDate IS NULL
                                    INNER JOIN CdOccupation cdMO ON cdMO.Code = MO.Occupation
                                    INNER JOIN FamilyMemberRelation FMR ON FMR.RecordStatus = M.RecordStatus AND FMR.Project = M.Project AND FMR.MemberId = M.MemberId AND (FMR.InactiveReason IS NOT NULL OR (FMR.InactiveReason IS NULL AND M.LastFamilyId = FMR.FamilyId))
                                    INNER JOIN CdWorkingDay cdWD ON cdWD.Code = MO.WorkDay
                                    WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND FMR.FamilyId = @familia";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable hstIngresosExtra(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT CASE WHEN @idioma = 'es' THEN cdAIT.DescSpanish ELSE cdAIT.DescEnglish END AS Tipo, dbo.fn_GEN_FormatDate(FAI.StartDate,@idioma) AS FechaInicio,  FAI.MonthlyIncome AS IngresosMensuales, FAI.Notes AS Nota FROM  FamilyAdditionalIncome FAI
                                    INNER JOIN CdAdditionalIncomeType cdAIT ON FAI.Type = cdAIT.Code AND FAI.EndDate IS NULL
                                    WHERE FAI.RecordStatus = ' ' AND FAI.Project = @sitio AND FAI.FamilyId = @familia";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstAdicciones(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT TOP 100 PERCENT * FROM 
                                    (
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, CONVERT(NVARCHAR(1000), Addictions) AS Notas FROM FamilyVisit 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND Addictions NOT LIKE ''
                                    ) A ORDER BY VisitDate";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstEducacion(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT TOP 100 PERCENT * FROM 
                                    (
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, CONVERT(NVARCHAR(1000), Education) AS Notas FROM FamilyVisit 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND Education NOT LIKE ''
	                                    UNION ALL
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, E AS Observacion FROM FamilyVisit2 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND E IS NOT NULL AND E != ''
                                    ) A ORDER BY VisitDate";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstFamilia(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT TOP 100 PERCENT * FROM 
                                    (
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, CONVERT(NVARCHAR(1000), FamilyGeneral) AS Notas FROM FamilyVisit 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND FamilyGeneral NOT LIKE ''
	                                    UNION ALL
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, F AS Observacion FROM FamilyVisit2 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND F IS NOT NULL AND F != ''
                                    ) A ORDER BY VisitDate";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstFocoAtencion(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT TOP 100 PERCENT * FROM 
                                    (
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, CONVERT(NVARCHAR(1000), AttentionFocus) AS Notas FROM FamilyVisit 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND AttentionFocus NOT LIKE ''
                                    ) A ORDER BY VisitDate";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstSocialLegal(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT TOP 100 PERCENT * FROM 
                                    (
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, CONVERT(NVARCHAR(1000), SocialLegalProblems) AS Notas FROM FamilyVisit 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND SocialLegalProblems NOT LIKE ''
	                                    UNION ALL
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, L AS Observacion FROM FamilyVisit2 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND L IS NOT NULL AND L != ''
                                    ) A ORDER BY VisitDate";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstOtros(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT TOP 100 PERCENT * FROM 
                                    (
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, CONVERT(NVARCHAR(1000), Others) AS Notas FROM FamilyVisit 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND Others NOT LIKE ''
	                                    UNION ALL
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, V AS Observacion FROM FamilyVisit2 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND V IS NOT NULL AND V != ''
                                    ) A ORDER BY VisitDate";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstProximosPasos(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT TOP 100 PERCENT * FROM 
                                    (
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, CONVERT(NVARCHAR(1000), NextSteps) AS Notas FROM FamilyVisit 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND NextSteps NOT LIKE ''
                                    ) A ORDER BY VisitDate";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstSalud(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT TOP 100 PERCENT * FROM 
                                    (
                                    	SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, CONVERT(NVARCHAR(1000), FamilyHealth) AS Notas FROM FamilyVisit 
                                    	WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND FamilyHealth NOT LIKE ''
                                    	UNION ALL
                                    	SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, S AS Observacion FROM FamilyVisit2 
                                    	WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND S IS NOT NULL AND S != ''
                                    ) A ORDER BY VisitDate";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable hstViolencia(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT TOP 100 PERCENT * FROM 
                                    (
	                                    SELECT VisitDate, dbo.fn_GEN_FormatDate(VisitDate, @idioma) AS Fecha, CONVERT(NVARCHAR(1000), FamilyViolence) AS Notas FROM FamilyVisit 
	                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND FamilyViolence NOT LIKE ''
                                    ) A ORDER BY VisitDate";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }


        protected void gstEliminarGasto(String sitio, String familia, String gasto)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE FamilyExpense SET RecordStatus = 'H', ExpirationDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE Project = @sitio AND FamilyId = @familia AND Expense = @gasto";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@gasto", gasto);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected void gstInsertarGasto(String sitio, String familia, String gasto, String usuario, float monto)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO dbo.FamilyExpense (Project, FamilyId, Expense, CreationDateTime, RecordStatus, UserId, Amount) VALUES (@sitio, @familia, @gasto, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ' ', @usuario, @monto);";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@gasto", gasto);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@monto", monto);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void gstInsertarGastoC(String sitio, String familia, String gasto, String usuario, float monto)
        {
            if (gstVerificarCambiodeGasto(sitio, familia, gasto, monto) != 1)
            {
                //gstEliminarGasto(sitio, familia, gasto);
                gstInsertarGasto(sitio, familia, gasto, usuario, monto);
            }
        }

        protected int gstVerificarCambiodeGasto(String sitio, String familia, String gasto, float monto)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT COUNT(*) AS R FROM FamilyExpense FE"
                        + " WHERE FE.Project = '" + sitio + "' AND FE.FamilyId = '" + familia + "' AND FE.Expense = '" + gasto + "' AND FE.Amount = " + monto + " AND FE.RecordStatus = ' '";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            int result = (int)dt.Rows[0]["R"];
            return result;
        }

        public DataTable gstObtenerGastos(String sitio, String familia, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT 1 AS Edit, CFE.Code AS Expense,"
                         + " CASE WHEN FE.Amount IS NOT NULL"
                         + " THEN FE.Amount"
                         + " ELSE 0"
                         + " END AS Amount,"
                         + " CASE WHEN '" + idioma + "' = 'es'"
                         + " THEN CFE.DescSpanish"
                         + " ELSE CFE.DescEnglish"
                         + " END + ':' AS Des FROM CdFamilyExpense CFE"
                         + " LEFT JOIN FamilyExpense FE ON CFE.Code = FE.Expense AND FE.RecordStatus = ' ' AND FE.Project = '" + sitio + "' AND FE.FamilyId = '" + familia + "'"
                         + " ORDER BY CASE WHEN '" + idioma + "' = 'es'"
                         + " THEN CFE.DescSpanish"
                         + " ELSE CFE.DescEnglish"
                         + " END";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable NADFASObtenerMiembrosNADFAS(String sitio, String familia, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT MN.CreationDateTime, MN.StartDate, M.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre, CASE WHEN '" + idioma + "' = 'es' THEN dbo.fn_GEN_CalcularEdad(M.BirthDate) ELSE dbo.fn_GEN_CalculateAge(M.BirthDate) END AS Edad, CASE WHEN (MN.EndDate IS NULL AND MN.StartDate IS NOT NULL) THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT) END AS Tiene FROM Member M"
                        + " LEFT JOIN MemberNADFAS MN ON MN.RecordStatus = M.RecordStatus AND MN.Project = M.Project AND MN.MemberId = M.MemberId AND MN.Year = (YEAR(GETDATE()) + 1) AND MN.EndDate IS NULL AND MN.CreationDateTime = (SELECT MAX(CreationDateTime) FROM MemberNADFAS MN2 WHERE MN2.RecordStatus = MN.RecordStatus AND MN2.Project = MN.Project AND MN2.MemberId = MN.MemberId AND MN2.Year = MN.Year AND MN2.EndDate IS NULL)"
                        + " INNER JOIN FamilyMemberRelation FMR ON M.RecordStatus = FMR.RecordStatus AND M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.LastFamilyId = FMR.FamilyId AND FMR.InactiveDate IS NULL"
                        + " INNER JOIN FwApplicationProperty FWAPMin ON (M.Project = FWAPMin.Organization OR '*' = FWAPMin.Organization) AND FWAPMin.Category = 'TS' AND FWAPMin.Name = 'MinimalAgeNADFAS'"
                        + " INNER JOIN FwApplicationProperty FWAPMax ON (M.Project = FWAPMax.Organization OR '*' = FWAPMax.Organization) AND FWAPMax.Category = 'TS' AND FWAPMax.Name = 'MaximalAgeNADFAS'"
                        + " INNER JOIN Family F ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND F.FamilyId = FMR.FamilyId"
                        + " WHERE F.AffiliationStatus = 'AFIL' AND M.AffiliationStatus IS NULL AND FMR.RecordStatus = ' ' AND FMR.Project = '" + sitio + "' AND FMR.FamilyId = '" + familia + "'"
                        + " AND (CAST(DATEDIFF(dd, M.BirthDate, GETDATE()) / 365.25 AS INT)) BETWEEN FWAPMin.PropertyValue AND FWAPMax.PropertyValue";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        //public DataTable NADFASobtenerNADFAS(String sitio, String miembro)
        //{
        //    SqlConnection con = new SqlConnection(conexionString);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlDataReader dr;
        //    String sql = @"SELECT MN.MemberId, MN.CreationDateTime, MN.Year, M.FirstNames + ' ' + M.LastNames AS NombreU, MN.Notes, S.Name AS School, CG.DescSpanish AS Grade, MN.UserId FROM MemberNADFAS MN"
        //                + " INNER JOIN Member M ON MN.RecordStatus = M.RecordStatus AND MN.MemberId = M.MemberId AND MN.Project = M.Project"
        //                + " INNER JOIN School S ON S.RecordStatus = MN.RecordStatus AND S.Project = MN.Project AND S.Code = MN.NextGradeSchool"
        //                + " INNER JOIN CdGrade CG ON CG.Code = MN.NextGrade"
        //                + " WHERE MN.RecordStatus = ' ' AND MN.Project = '" + sitio + "' AND M.MemberId = '" + miembro + "'";
        //    SqlCommand showresult = new SqlCommand(sql, con);
        //    con.Open();
        //    dr = showresult.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    dr.Close();
        //    return dt;
        //}

        //public DataTable NADFASobtenerNADFASesp(String sitio, String miembro, String fechaCreacion, String año)
        //{
        //    SqlConnection con = new SqlConnection(conexionString);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlDataReader dr;
        //    String sql = @"SELECT MN.MemberId, MN.CreationDateTime, MN.Year, MN.Notes, MN.NextGrade, MN.NextGradeSchool FROM MemberNADFAS MN"
        //                + " WHERE RecordStatus = ' ' AND MN.Project = '" + sitio + "' AND MN.MemberId = '" + miembro + "' AND MN.CreationDateTime = '" + fechaCreacion + "' AND MN.Year = " + año;
        //    SqlCommand showresult = new SqlCommand(sql, con);
        //    con.Open();
        //    dr = showresult.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    dr.Close();
        //    return dt;
        //}

        public Boolean NADFASDesactivarNADFAS(String sitioSLCT, String miembroSLCT, String fechaCreacionSLCT, String añoSLCT, String usuario, String fechaInicioSLCT)
        {
            DateTime ahora = DateTime.Now;
            DateTime fechaCreacion = ahora;
            DateTime fechaExpiracionSLCT = ahora;
            DateTime fechaFin = ahora;
            NADFASingresarNADFAS(sitioSLCT, miembroSLCT, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), añoSLCT, usuario, fechaInicioSLCT, fechaFin.ToString("yyyy-MM-dd HH:mm:ss"));
            NADFASPasarHistorico(sitioSLCT, miembroSLCT, fechaCreacionSLCT, añoSLCT, fechaExpiracionSLCT.ToString("yyyy-MM-dd HH:mm:ss"));
            return true;
        }

        public void NADFASEliminarNADFAS(String sitio, String miembro, String fechaCreacionSLCT, String año, String usuario)
        {
            DateTime fechaCreacion = DateTime.Now;
            NADFASIngresarHistoricoNADFAS(sitio, miembro, año, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), fechaCreacionSLCT, usuario);
            NADFASPasarHistorico(sitio, miembro, fechaCreacionSLCT, año, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"));

            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE MemberNADFAS SET RecordStatus = 'H', ExpirationDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE Project = @sitio AND MemberId = @miembro AND CreationDateTime = @fechaCreacion AND Year = @año";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@año", año);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected void NADFASIngresarHistoricoNADFAS(String sitio, String miembro, String año, String fechaCreacion, String fechaCreacionSLCT, String usuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO MemberNADFAS (Project, MemberId, CreationDateTime, Year, RecordStatus, ExpirationDateTime, UserId, StartDate, EndDate) 
                            SELECT Project, MemberId, @fechaCreacion, @año, 'H', @fechaExpiracion, @usuario, StartDate, EndDate
                            FROM MemberNADFAS
                            WHERE Project = @sitio AND MemberId = @miembro AND Year = @año AND CreationDateTime = @fechaCreacionSLCT";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaExpiracion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaCreacionSLCT", fechaCreacionSLCT);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected void NADFASPasarHistorico(String sitio, String miembro, String fechaCreacion, String año, String fechaExpiracion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE MemberNADFAS SET RecordStatus = 'H', ExpirationDateTime = @fechaExpiracion WHERE Project = @sitio AND MemberId = @miembro AND CreationDateTime = @fechaCreacion AND Year = @año";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected void NADFASingresarNADFAS(String sitio, String miembro, String fechaCreacion, String año, String usuario, String fechaInicio, String fechaFin)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO dbo.MemberNADFAS (Project, MemberId, CreationDateTime, Year, RecordStatus, UserId, StartDate, EndDate) VALUES (@sitio, @miembro, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', @año, ' ', @usuario, @fechaInicio, @fechaFin)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            if (!String.IsNullOrEmpty(fechaFin))
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

        public void NADFASNuevoNADFAS(String sitio, String miembro, String año, String usuario)
        {
            DateTime ahora = DateTime.Now;
            DateTime fechaCreacion = ahora;
            DateTime fechaInicio = ahora;
            NADFASingresarNADFAS(sitio, miembro, fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), año, usuario, fechaInicio.ToString("yyyy-MM-dd HH:mm:ss"), "");
        }

        //protected Boolean NADFASVerificarActualizacion(String sitio, String idMiembro, String año, String fechaCreacion)
        //{
        //    SqlConnection con = new SqlConnection(conexionString);
        //    SqlCommand cmd = new SqlCommand();
        //    SqlDataReader dr;
        //    String sql = @"SELECT *"
        //                 + " FROM MemberNADFAS MN "
        //                 + " WHERE MN.MemberId = " + idMiembro + " AND MN.Project = '" + sitio + "' AND MN.RecordStatus = ' '"
        //                 + " AND MN.Year = '" + año + "' AND MN.CreationDateTime != '" + fechaCreacion + "'";
        //    SqlCommand showresult = new SqlCommand(sql, con);
        //    con.Open();
        //    dr = showresult.ExecuteReader();
        //    DataTable dt = new DataTable();
        //    dt.Load(dr);
        //    dr.Close();
        //    if (dt.Rows.Count == 0)
        //    {
        //        return true;
        //    }
        //    return false;
        //}

        public Boolean NADFASVerificarNADFASActivo(String sitio, String idMiembro, String año)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM MemberNADFAS MN "
                         + " WHERE MN.MemberId = " + idMiembro + " AND MN.Project = '" + sitio + "' AND MN.RecordStatus = ' '"
                         + " AND MN.Year = '" + año + "' AND MN.EndDate IS NULL";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }

        protected void pssEliminarPosesion(String sitio, String familia, String posesion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE FamilyPossession SET RecordStatus = 'H', ExpirationDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE Project = @sitio AND FamilyId = @familia AND Possession = @posesion";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@posesion", posesion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void pssInsertarPosesion(String sitio, String familia, String posesion, String usuario, int cantidad)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO dbo.FamilyPossession (Project, FamilyId, Possession, CreationDateTime, RecordStatus, UserId, Quantity) VALUES (@sitio, @familia, @posesion, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', ' ', @usuario, @cantidad);";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@posesion", posesion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@cantidad", cantidad);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void pssInsertarPosesionC(String sitio, String familia, String posesion, String usuario, int cantidad)
        {
            if (pssVerificarCambiodePosesion(sitio, familia, posesion, cantidad) != 1)
            {
                //pssEliminarPosesion(sitio, familia, posesion);
                pssInsertarPosesion(sitio, familia, posesion, usuario, cantidad);
            }
        }

        public DataTable pssObtenerPosesiones(String sitio, String familia, String idioma, String categoria)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT CP.Code AS Possession,"
                        + " CASE WHEN '" + idioma + "' = 'es'"
                        + " THEN CP.DescSpanish"
                        + " ELSE CP.DescEnglish"
                        + " END + ':' AS Des,"
                        + " CASE WHEN FP.Quantity IS NOT NULL"
                        + " THEN FP.Quantity"
                        + " ELSE 0"
                        + " END AS Quantity"
                        + " FROM CdPossession CP"
                        + " LEFT JOIN FamilyPossession FP ON CP.Code = FP.Possession AND FP.RecordStatus = ' ' AND FP.FamilyId = '" + familia + "' AND FP.Project = '" + sitio + "'"
                        + " INNER JOIN CdPossessionCategory CPC ON CPC.Code = CP.Category"
                        + " WHERE Category = '" + categoria + "'"
                        + " ORDER BY CASE WHEN '" + idioma + "' = 'es'"
                        + " THEN CP.DescSpanish"
                        + " ELSE CP.DescEnglish"
                        + " END";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        protected int pssVerificarCambiodePosesion(String sitio, String familia, String posesion, int cantidad)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT COUNT(*) AS R FROM FamilyPossession FP"
                        + " WHERE FP.Project = '" + sitio + "' AND FP.FamilyId = '" + familia + "' AND FP.Possession = '" + posesion + "' AND FP.Quantity = " + cantidad + " AND FP.RecordStatus = ' '";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            int result = (int)dt.Rows[0]["R"];
            return result;
        }

        public DataTable rprtObtenerFamilias(String sitio, String idioma, String area, String TS, String tipoVisita, String objetivoVisita, String fecha, String filtro)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT F.FamilyId,
	                                F.Address AS Direccion,
	                                M.FirstNames + ' ' + M.LastNames AS JefeCasa,
	                                CASE WHEN @idioma = 'es' 
	                                THEN CGA.DescSpanish
	                                ELSE CGA.DescEnglish
	                                END Area,
	                                FER.EmployeeId TS, 
	                                dbo.fn_GEN_FormatDate(FV2.VisitDate, @idioma) fechaUltimaVisita, 
	                                CASE WHEN @idioma = 'es' 
	                                THEN CFAT.DescSpanish
	                                ELSE CFAT.DescEnglish
	                                END TipoVisita,
	                                (SELECT COUNT(*) FROM Member M 
					                INNER JOIN FamilyMemberRelation FMR ON M.MemberId = FMR.MemberId AND M.Project = FMR.Project AND M.RecordStatus = FMR.RecordStatus AND FMR.InactiveReason IS NULL
					                WHERE M.RecordStatus = ' ' AND FMR.Project = F.Project AND FMR.FamilyId = F.FamilyId AND dbo.fn_AFIL_faseDesafil(M.Project, M.MemberId) = 'Fase II')	
	                                AS ApadrinadosFase2,
                                    dbo.fn_GEN_enfoquesVisita(@sitio, @idioma, FV2.FamilyVisitId) AS Enfoques	
                                    FROM Family F
                                    LEFT JOIN FamilyEmployeeRelation FER ON F.Project = FER.Project AND F.FamilyId = FER.FamilyId AND F.RecordStatus = FER.RecordStatus AND FER.EndDate IS NULL
                                    LEFT JOIN FamilyVisit2 FV2 ON F.Project = FV2.Project AND F.FamilyId = FV2.FamilyId AND F.RecordStatus = FV2.RecordStatus AND (FV2.VisitType = @tipoVisita OR '' = @tipoVisita) AND FV2.VisitDate = (SELECT MAX(FV22.VisitDate) 
																																	                                                                                                    FROM FamilyVisit2 FV22
                                                                                                                                                                                                                                        WHERE FV2.Project = FV22.Project 
																																				                                                                                        AND FV2.FamilyId = FV22.FamilyId 
																																				                                                                                        AND FV2.RecordStatus = FV22.RecordStatus
																																				                                                                                        AND FV2.VisitType = FV22.VisitType
                                                                                                                                                                                                                                        AND ((FV2.VisitDate >= @fecha AND @filtro = 'R') OR (FV2.VisitDate < @fecha AND @filtro = 'N') OR (@filtro = 'T'))
                                                                                                                                                                                                                                        AND ((0 < (SELECT COUNT(*) FROM FamilyVisitObjective FVO WHERE FV2.FamilyVisitId = FVO.FamilyVisitId AND FV2.RecordStatus = FVO.RecordStatus AND FV2.Project = FVO.Project AND FVO.Objective = @objetivoVisita)) OR ('' = @objetivoVisita)))
                                    INNER JOIN FamilyMemberRelation FMR ON F.Project = FMR.Project AND F.FamilyId = FMR.FamilyId AND F.RecordStatus = FMR.RecordStatus AND FMR.InactiveReason IS NULL AND FMR.Type IN ('JEFE', 'JEFM')
                                    INNER JOIN Member M ON M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.RecordStatus = FMR.RecordStatus
                                    INNER JOIN CdGeographicArea CGA ON F.Area = CGA.Code
                                    LEFT JOIN CdFamilyActivityType CFAT ON FV2.VisitType = CFAT.Code
                                    WHERE F.RecordStatus = ' '
                                    AND F.AffiliationStatus = 'AFIL'
                                    AND F.Project = @sitio
                                    AND (F.Area = @area OR '' = @area)  
                                    AND (FER.EmployeeId = @TS OR '' = @TS)
                                    AND ((@filtro = 'R' AND FV2.VisitDate IS NOT NULL) OR (@filtro = 'N' AND ((FV2.VisitDate IS NOT NULL) OR (( FV2.VisitDate IS NULL) AND ((SELECT COUNT(*) FROM FamilyVisit2 FV22 WHERE FV22.RecordStatus = F.RecordStatus AND FV22.Project = F.Project AND FV22.FamilyId = F.FamilyId AND FV22.VisitType = @tipoVisita AND FV22.VisitDate >= @fecha) = 0)))))";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@area", area);
            comando.Parameters.AddWithValue("@TS", TS);
            comando.Parameters.AddWithValue("@tipoVisita", tipoVisita);
            comando.Parameters.AddWithValue("@objetivoVisita", objetivoVisita);
            comando.Parameters.AddWithValue("@fecha", fecha);
            comando.Parameters.AddWithValue("@filtro", filtro);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

//        public DataTable rprtObtenerFamilias(String sitio, String idioma, String area, String TS, String tipoVisita)
//        {
//            DateTime now = DateTime.Now;
//            SqlConnection conexion = new SqlConnection(conexionString);
//            conexion.Open();
//            String comandoString = @"SELECT F.FamilyId,
//	                                F.Address AS Direccion,
//	                                M.FirstNames + ' ' + M.LastNames AS JefeCasa,
//	                                CASE WHEN @idioma = 'es' 
//	                                THEN CGA.DescSpanish
//	                                ELSE CGA.DescEnglish
//	                                END Area,
//	                                FER.EmployeeId TS, 
//	                                dbo.fn_GEN_FormatDate(FV2.VisitDate, @idioma) fechaUltimaVisita, 
//	                                CASE WHEN @idioma = 'es' 
//	                                THEN CFAT.DescSpanish
//	                                ELSE CFAT.DescEnglish
//	                                END TipoVisita,
//	                                CASE WHEN 0 !=(SELECT COUNT(*) FROM Member M 
//					                INNER JOIN FamilyMemberRelation FMR ON M.MemberId = FMR.MemberId AND M.Project = FMR.Project AND M.RecordStatus = FMR.RecordStatus AND FMR.InactiveReason IS NULL
//					                WHERE M.RecordStatus = ' ' AND FMR.Project = F.Project AND FMR.FamilyId = F.FamilyId AND dbo.fn_AFIL_faseDesafil(M.Project, M.MemberId) = 'Fase II')	
//	                                THEN 'Fase II'
//	                                ELSE ''
//	                                END AS Fase,
//                                    dbo.fn_GEN_enfoquesVisita(@sitio, @idioma, FV2.FamilyVisitId) AS Enfoques		  
//                                    FROM Family F
//                                        INNER JOIN FamilyEmployeeRelation FER ON F.Project = FER.Project AND F.FamilyId = FER.FamilyId AND F.RecordStatus = FER.RecordStatus AND FER.EndDate IS NULL
//                                        LEFT JOIN FamilyVisit2 FV2 ON F.Project = FV2.Project AND F.FamilyId = FV2.FamilyId AND F.RecordStatus = FV2.RecordStatus AND FV2.VisitDate = (SELECT MAX(VisitDate) 
//																																	                                                    FROM FamilyVisit2 FV22 
//																																				                                        WHERE FV2.Project = FV22.Project 
//																																				                                        AND FV2.FamilyId = FV22.FamilyId 
//																																				                                        AND FV2.RecordStatus = FV22.RecordStatus
//																																				                                        AND (FV2.VisitType = @tipoVisita OR '' = @tipoVisita))
//                                    INNER JOIN FamilyMemberRelation FMR ON F.Project = FMR.Project AND F.FamilyId = FMR.FamilyId AND F.RecordStatus = FMR.RecordStatus AND FMR.InactiveReason IS NULL AND FMR.Type IN ('JEFE', 'JEFM')
//                                    INNER JOIN Member M ON M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.RecordStatus = FMR.RecordStatus
//                                    INNER JOIN CdGeographicArea CGA ON F.Area = CGA.Code
//                                    LEFT JOIN CdFamilyActivityType CFAT ON FV2.VisitType = CFAT.Code
//                                    WHERE F.RecordStatus = ' '
//                                    AND F.AffiliationStatus = 'AFIL'
//                                    AND F.Project = 'F'
//                                    AND (F.Area = @area OR '' = @area)  
//                                    AND (FER.EmployeeId = @TS OR '' = @TS) ";
//            SqlCommand comando = new SqlCommand(comandoString, conexion);
//            comando.Parameters.AddWithValue("@sitio", sitio);
//            comando.Parameters.AddWithValue("@idioma", idioma);
//            comando.Parameters.AddWithValue("@area", area);
//            comando.Parameters.AddWithValue("@TS", TS);
//            comando.Parameters.AddWithValue("@tipoVisita", tipoVisita);
//            SqlDataAdapter adaptador = new SqlDataAdapter();
//            adaptador.SelectCommand = comando;
//            DataTable tablaDatos = new DataTable();
//            adaptador.Fill(tablaDatos);
//            conexion.Close();
//            return tablaDatos;
//        }

        public DataTable rprtObtenerFamilias2(String sitio, String idioma, String area, String TS, String tipoVisita, String objetivoVisita, String filtro)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT F.FamilyId,
	                                F.Address AS Direccion,
	                                M.FirstNames + ' ' + M.LastNames AS JefeCasa,
	                                CASE WHEN @idioma = 'es' 
	                                THEN CGA.DescSpanish
	                                ELSE CGA.DescEnglish
	                                END Area,
	                                FER.EmployeeId TS, 
	                                dbo.fn_GEN_FormatDate(FV2.VisitDate, @idioma) fechaUltimaVisita, 
	                                CASE WHEN @idioma = 'es' 
	                                THEN CFAT.DescSpanish
	                                ELSE CFAT.DescEnglish
	                                END TipoVisita,
	                                (SELECT COUNT(*) FROM Member M 
					                INNER JOIN FamilyMemberRelation FMR ON M.MemberId = FMR.MemberId AND M.Project = FMR.Project AND M.RecordStatus = FMR.RecordStatus AND FMR.InactiveReason IS NULL
					                WHERE M.RecordStatus = ' ' AND FMR.Project = F.Project AND FMR.FamilyId = F.FamilyId AND dbo.fn_AFIL_faseDesafil(M.Project, M.MemberId) = 'Fase II')	
	                                AS ApadrinadosFase2,
                                    dbo.fn_GEN_enfoquesVisita(@sitio, @idioma, FV2.FamilyVisitId) AS Enfoques	
                                    FROM Family F
                                    INNER JOIN FamilyEmployeeRelation FER ON F.Project = FER.Project AND F.FamilyId = FER.FamilyId AND F.RecordStatus = FER.RecordStatus AND FER.EndDate IS NULL
                                    LEFT JOIN FamilyVisit2 FV2 ON F.Project = FV2.Project AND F.FamilyId = FV2.FamilyId AND F.RecordStatus = FV2.RecordStatus AND FV2.VisitDate = (SELECT MAX(VisitDate) 
																																	                                                    FROM FamilyVisit2 FV22
                                                                                                                                                                                        WHERE FV2.Project = FV22.Project 
																																				                                        AND FV2.FamilyId = FV22.FamilyId 
																																				                                        AND FV2.RecordStatus = FV22.RecordStatus
																																				                                        AND (FV2.VisitType = @tipoVisita OR '' = @tipoVisita))
                                                                                                                                                                                        AND ((0 < (SELECT COUNT(*) FROM FamilyVisitObjective FVO WHERE FV2.FamilyVisitId = FVO.FamilyVisitId AND FV2.RecordStatus = FVO.RecordStatus AND FV2.Project = FVO.Project AND FVO.Objective = @objetivoVisita)) OR ('' = @objetivoVisita))
                                    INNER JOIN FamilyMemberRelation FMR ON F.Project = FMR.Project AND F.FamilyId = FMR.FamilyId AND F.RecordStatus = FMR.RecordStatus AND FMR.InactiveReason IS NULL AND FMR.Type IN ('JEFE', 'JEFM')
                                    INNER JOIN Member M ON M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.RecordStatus = FMR.RecordStatus
                                    INNER JOIN CdGeographicArea CGA ON F.Area = CGA.Code
                                    LEFT JOIN CdFamilyActivityType CFAT ON FV2.VisitType = CFAT.Code
                                    WHERE F.RecordStatus = ' '
                                    AND F.AffiliationStatus = 'AFIL'
                                    AND F.Project = 'F'
                                    AND (F.Area = @area OR '' = @area)  
                                    AND (FER.EmployeeId = @TS OR '' = @TS)
                                    AND ((@filtro = 'N' AND FV2.VisitDate IS NULL) 
                                    OR (@filtro = 'R' AND FV2.VisitDate IS NOT NULL) 
                                    OR (@filtro = 'T'))
                                    ORDER BY FV2.VisitDate DESC, FV2.FamilyId";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@area", area);
            comando.Parameters.AddWithValue("@TS", TS);
            comando.Parameters.AddWithValue("@tipoVisita", tipoVisita);
            comando.Parameters.AddWithValue("@objetivoVisita", objetivoVisita);
            comando.Parameters.AddWithValue("@filtro", filtro);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable rsmApad(String idioma, String sitio, String familia, int meses)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT  CASE WHEN '" + idioma + "' = 'es' THEN ActivityEs ELSE ActivityEn END AS Actividad, dbo.fn_GEN_FormatDate(ActivityDateTime, '" + idioma + "') Fecha"
                        + " FROM         FamilyActivityReport"
                        + " WHERE   FunctionalArea = 'APAD' AND  Project = '" + sitio + "' AND FamilyId = " + familia + " AND dbo.fn_GEN_mesesEntreFechas(ActivityDateTime) <= " + meses
                        + " ORDER BY ActivityDateTime DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable rsmApjo(String idioma, String sitio, String familia, int año)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT Actividad, Nombre,"
                + " SUM(CASE WHEN Mes = 1 THEN Total ELSE 0 END) AS Ene,"
                + " SUM(CASE WHEN Mes = 2 THEN Total ELSE 0 END) AS Feb,"
                + " SUM(CASE WHEN Mes = 3 THEN Total ELSE 0 END) AS Mar,"
                + " SUM(CASE WHEN Mes = 4 THEN Total ELSE 0 END) AS Abr,"
                + " SUM(CASE WHEN Mes = 5 THEN Total ELSE 0 END) AS May,"
                + " SUM(CASE WHEN Mes = 6 THEN Total ELSE 0 END) AS Jun,"
                + " SUM(CASE WHEN Mes = 7 THEN Total ELSE 0 END) AS Jul,"
                + " SUM(CASE WHEN Mes = 8 THEN Total ELSE 0 END) AS Ago,"
                + " SUM(CASE WHEN Mes = 9 THEN Total ELSE 0 END) AS Sep,"
                + " SUM(CASE WHEN Mes = 10 THEN Total ELSE 0 END) AS Oct,"
                + " SUM(CASE WHEN Mes = 11 THEN Total ELSE 0 END) AS Nov,"
                + " SUM(CASE WHEN Mes = 12 THEN Total ELSE 0 END) AS Dic,"
                + " SUM(Total) AS Total"
                + " FROM dbo.fn_GJOV_AsistenciaPorMiembro('" + sitio + "', " + (año - 1) + ")"
                + " WHERE Familia = " + familia
                + " GROUP BY Actividad, Nombre"
                + " ORDER BY Actividad, Nombre";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable rsmCaja(String idioma, String sitio, String familia)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT CASE WHEN FAA.Active = 1 THEN CASE WHEN '" + idioma + "' = 'es' THEN 'Sí' ELSE 'Yes' END ELSE 'No' END AS Cta_Activa, dbo.fn_GEN_FormatDate(FAA.CreationDateTime, '" + idioma + "') AS Actualización"
                        + " FROM  dbo.FamilyACHAccount AS FAA"
                        + " WHERE (FAA.RecordStatus = ' ') AND (FAA.Active = 1) AND FAA.Project = '" + sitio + "' AND  FAA.FamilyId = " + familia;
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable rsmEduc(String idioma, String sitio, String familia, int meses)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT  CASE WHEN '" + idioma + "' = 'es' THEN ActivityEs ELSE ActivityEn END AS Actividad, dbo.fn_GEN_FormatDate(ActivityDateTime, '" + idioma + "') Fecha"
                        + " FROM         FamilyActivityReport"
                        + " WHERE   FunctionalArea = 'EDUC' AND  Project = '" + sitio + "' AND FamilyId = " + familia + " AND dbo.fn_GEN_mesesEntreFechas(ActivityDateTime) <= " + meses
                        + " ORDER BY ActivityDateTime DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }


        public DataTable rsmMiembros(String idioma, String sitio, String familia)
        {
            SqlConnection conexion = new SqlConnection(conexionStringCautionOriginal);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT  M.MemberId, M.FirstNames + ' ' + M.LastNames AS Nombre
			 , CASE WHEN @idioma = 'es' THEN cdFMR.DescSpanish ELSE cdFMR.DescEnglish END AS Relacion
             , CASE WHEN @idioma = 'es' THEN cdFMRIR.DescSpanish ELSE cdFMRIR.DescEnglish END AS RazonInactivo 
             , CASE WHEN M.AffiliationStatus IS NULL THEN '' ELSE (CASE WHEN @idioma = 'es' THEN cdAS.DescSpanish ELSE  cdAS.DescEnglish END ) + '-' + (CASE WHEN @idioma = 'es' THEN cdAT.DescSpanish ELSE  cdAT.DescEnglish END )  END  AS Afiliación
             , dbo.fn_GEN_FormatDate(M.BirthDate, @idioma) + ' - ' +  dbo.fn_GEN_obtenerEdad(M.Project, M.MemberId, GETDATE(), @idioma)AS Nacimiento
             , dbo.fn_GEN_infoUltimoAñoEscolar(@sitio, M.MemberId, @idioma) AS Educacion  
             , CellularPhoneNumber Celular
             , CASE WHEN dbo.fn_AFIL_faseDesafil(M.Project, M.MemberId) = 'Fase I' THEN 'I' WHEN dbo.fn_AFIL_faseDesafil(M.Project, M.MemberId) = 'Fase II' THEN 'II' ELSE NULL END AS Fase
             FROM Member M
             INNER JOIN FamilyMemberRelation FMR ON M.Project = FMR.Project AND M.RecordStatus = FMR.RecordStatus AND M.MemberId = FMR.MemberId
             AND ((M.LastFamilyId = FMR.FamilyId AND FMR.InactiveReason IS NULL) OR FMR.InactiveReason IS NOT NULL)
             LEFT JOIN CdFamMemRelInactiveReason cdFMRIR ON cdFMRIR.Code = FMR.InactiveReason             
             INNER JOIN CdFamilyMemberRelationType cdFMR ON cdFMR.Code = FMR.Type
             LEFT JOIN dbo.CdAffiliationStatus cdAS ON cdAS.Code = M.AffiliationStatus
             LEFT JOIN dbo.CdAffiliationType cdAT ON cdAT.Code = M.AffiliationType
			 LEFT JOIN MiscMemberInfo MMI ON M.Project = MMI.Project AND M.MemberId = MMI.MemberId AND M.RecordStatus = MMI.RecordStatus AND MMI.TSNotes IS NOT NULL 
			 WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND FMR.FamilyId = @familia
             ORDER BY cdFMR.DisplayOrder";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable rsmMiembrosEduc(String idioma, String sitio, String familia)
        {
            SqlConnection conexion = new SqlConnection(conexionStringCautionOriginal);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"SELECT  M.MemberId, M.FirstNames AS Nombre
             , dbo.fn_AFIL_faseDesafil(M.Project, M.MemberId) AS Fase
			 , CASE WHEN @idioma = 'es' THEN cdFMR.DescSpanish ELSE cdFMR.DescEnglish END + ' ' + CASE WHEN FMR.InactiveReason IS NOT NULL THEN CASE WHEN @idioma = 'es' THEN '(Inactivo: ' + cdFMRIR.DescSpanish + ')' ELSE '(Inactive: ' + cdFMRIR.DescEnglish + ')' END ELSE '' END AS Relacion
             , CASE WHEN M.AffiliationStatus IS NULL THEN '' ELSE (CASE WHEN @idioma = 'es' THEN cdAS.DescSpanish ELSE  cdAS.DescEnglish END ) + '-' + (CASE WHEN @idioma = 'es' THEN cdAT.DescSpanish ELSE  cdAT.DescEnglish END )  END  AS Afiliación
             , dbo.fn_GEN_FormatDate(M.BirthDate, @idioma) + ' - ' +  dbo.fn_GEN_obtenerEdad(M.Project, M.MemberId, GETDATE(), @idioma) AS Nacimiento
             , dbo.fn_GEN_infoUltimoAñoEscolar(@sitio, M.MemberId, @idioma) AS Educacion 
             , CellularPhoneNumber Celular
             FROM Member M
             INNER JOIN FamilyMemberRelation FMR ON M.Project = FMR.Project AND M.RecordStatus = FMR.RecordStatus AND M.MemberId = FMR.MemberId
             AND ((M.LastFamilyId = FMR.FamilyId AND FMR.InactiveReason IS NULL) OR FMR.InactiveReason IS NOT NULL)
             LEFT JOIN CdFamMemRelInactiveReason cdFMRIR ON cdFMRIR.Code = FMR.InactiveReason             
             INNER JOIN CdFamilyMemberRelationType cdFMR ON cdFMR.Code = FMR.Type
             LEFT JOIN dbo.CdAffiliationStatus cdAS ON cdAS.Code = M.AffiliationStatus
             LEFT JOIN dbo.CdAffiliationType cdAT ON cdAT.Code = M.AffiliationType
			 LEFT JOIN MiscMemberInfo MMI ON M.Project = MMI.Project AND M.MemberId = MMI.MemberId AND M.RecordStatus = MMI.RecordStatus AND MMI.TSNotes IS NOT NULL 
			 WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND FMR.FamilyId = @familia
             ORDER BY cdFMR.DisplayOrder";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }


        public DataTable rsmNADFAS(String sitio, String familia, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT  M.FirstNames + ' ' + M.LastNames AS Nombre,  dbo.fn_GEN_FormatDate(MN.StartDate, '" + idioma + "') AS FechaInicio, dbo.fn_GEN_FormatDate(MN.EndDate, '" + idioma + "') AS FechaFin FROM Member M"
                       + " INNER JOIN MemberNADFAS MN ON MN.RecordStatus = M.RecordStatus AND MN.Project = M.Project AND MN.MemberId = M.MemberId AND MN.Year = (YEAR(GETDATE()) + 1) AND MN.EndDate IS NULL AND MN.CreationDateTime = (SELECT MAX(CreationDateTime) FROM MemberNADFAS MN2 WHERE MN2.RecordStatus = MN.RecordStatus AND MN2.Project = MN.Project AND MN2.MemberId = MN.MemberId AND MN2.Year = MN.Year AND MN2.EndDate IS NULL)"
                       + " INNER JOIN FamilyMemberRelation FMR ON M.RecordStatus = FMR.RecordStatus AND M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.LastFamilyId = FMR.FamilyId AND FMR.InactiveDate IS NULL"
                       + " INNER JOIN Family F ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND F.FamilyId = FMR.FamilyId AND FMR.FamilyId = '" + familia + "'"
                       + " WHERE F.AffiliationStatus = 'AFIL' AND M.AffiliationStatus IS NULL AND FMR.RecordStatus = ' ' AND FMR.Project = '" + sitio + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable rsmNotas(String idioma, String sitio, String familia, int todas)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT M.MemberId AS miembroId, M.FirstNames AS Nombre"
                        + " , CASE WHEN dbo.fn_AFIL_faseDesafil(M.Project, M.MemberId) = 'Fase I' THEN 'I' WHEN dbo.fn_AFIL_faseDesafil(M.Project, M.MemberId) = 'Fase II' THEN 'II' ELSE NULL END AS fase"
                        + " , MECG.Unit Unidad"
                        + " , dbo.fn_EDUC_semaforoEsp(MECG.Project, MECG.MemberId, MECG.SchoolYear, MECG.Unit) AS semaforo"
                        + " , CASE WHEN MECG.ApprovedAll = 1 THEN CASE WHEN '" + idioma + "' = 'es' THEN 'Sí' ELSE 'Yes' END ELSE '' END AS ganoTodas"
                        + " , MECG.FailedClasses numPerdidas"
                        + " , CASE WHEN '" + idioma + "' = 'es' THEN cdS.DescSpanish ELSE cdS.DescEnglish END AS fuente"
                        + " FROM  dbo.MemberEducationClassGrade MECG"
                        + " INNER JOIN dbo.Member M ON MECG.RecordStatus = M.RecordStatus AND MECG.Project = M.Project AND MECG.MemberId = M.MemberId"
                        + " INNER JOIN dbo.CdSchoolGradeSource cdS ON cdS.Code = MECG.Source"
                        + " WHERE M.RecordStatus = ' '  AND M.LastFamilyId = '" + familia + "' AND M.Project = '" + sitio + "'"
                        + " AND (MECG.Unit = (SELECT MAX(CSGU.Code) FROM CdSchoolGradeUnit CSGU INNER JOIN MemberEducationClassGrade MECG2 ON CSGU.Code = MECG2.Unit AND MECG2.Project = MECG.Project AND MECG2.MemberId = MECG.MemberId  AND MECG2.RecordStatus = MECG.RecordStatus) OR " + todas + " = 0)";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable rsmObtenerUnidades(String sitio, String miembro, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT CSGU.Code, CASE WHEN '" + idioma + "' = 'es' THEN CSGU.DescSpanish ELSE CSGU.DescEnglish END AS Des"
                        + " FROM CdSchoolGradeUnit CSGU"
                        + " INNER JOIN MemberEducationClassGrade MECG ON CSGU.Code = MECG.Unit AND Project = '" + sitio + "' AND MemberId = " + miembro + " AND RecordStatus = ' '";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable rsmPsic(String idioma, String sitio, String familia, int meses)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT  CASE WHEN '" + idioma + "' = 'es' THEN ActivityEs ELSE ActivityEn END AS Actividad, dbo.fn_GEN_FormatDate(ActivityDateTime, '" + idioma + "') Fecha"
                        + " FROM         FamilyActivityReport"
                        + " WHERE   FunctionalArea = 'PSIC' AND  Project = '" + sitio + "' AND FamilyId = " + familia + " AND dbo.fn_GEN_mesesEntreFechas(ActivityDateTime) <= " + meses
                        + " ORDER BY ActivityDateTime DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable rsmSalud(String idioma, String sitio, String familia, int meses)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT  CASE WHEN '" + idioma + "' = 'es' THEN ActivityEs ELSE ActivityEn END AS Actividad, dbo.fn_GEN_FormatDate(ActivityDateTime, '" + idioma + "') Fecha"
                        + " FROM         FamilyActivityReport"
                        + " WHERE   FunctionalArea = 'CLIN' AND  Project = '" + sitio + "' AND FamilyId = " + familia + " AND dbo.fn_GEN_mesesEntreFechas(ActivityDateTime) <= " + meses
                        + " ORDER BY ActivityDateTime DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable rsmSecAfil(String idioma, String sitio, String familia)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT M.FirstNames + ' (' + CONVERT(VARCHAR, M.MemberId) + ')' AS Nombre,"
                         + " CONVERT(varchar, MMI.Candidate2ndAffiliationEnd, 111)  Finalizó"
                         + " FROM dbo.MiscMemberInfo MMI"
                         + " INNER JOIN dbo.Member M ON M.RecordStatus = MMI.RecordStatus AND M.Project = MMI.Project AND M.MemberId = MMI.MemberId"
                         + " WHERE MMI.RecordStatus = ' ' AND MMI.Project = '" + sitio + "' AND M.LastFamilyID = " + familia
                         + " AND MMI.Candidate2ndAffiliationStart IS NOT NULL ";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable rsmTotalIngresosExtra(String sitio, String familia)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT CASE WHEN SUM(FAI.MonthlyIncome) IS NOT NULL THEN SUM(FAI.MonthlyIncome) ELSE 0 END AS TotalIngresos FROM FamilyAdditionalIncome FAI
                            WHERE FAI.RecordStatus = ' ' AND FAI.Project = '" + sitio + "' AND FAI.FamilyId = " + familia + " AND FAI.EndDate IS NULL";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable rsmTotalesOcupaciones(String sitio, String familia)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT CASE WHEN SUM(MO.MonthlyIncome) IS NOT NULL THEN SUM(MO.MonthlyIncome) ELSE 0 END AS TotalIngresos, CASE WHEN SUM(MO.MonthlyContribution) IS NOT NULL THEN SUM(MO.MonthlyContribution) ELSE 0 END AS TotalAportes FROM MemberOccupation MO"
                       + "  INNER JOIN Member M ON MO.RecordStatus = M.RecordStatus AND MO.Project = M.Project AND MO.MemberId = M.MemberId  AND MO.EndDate IS NULL"
                       + "  INNER JOIN FamilyMemberRelation FMR ON FMR.RecordStatus = M.RecordStatus AND FMR.Project = M.Project AND FMR.FamilyId = M.LastFamilyId AND FMR.MemberId = M.MemberId AND FMR.InactiveReason IS NULL"
                       + "  WHERE MO.RecordStatus = ' ' AND MO.Project = '" + sitio + "' AND M.LastFamilyId = " + familia;
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public int rsmTotalGastos(String sitio, String familia)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT CASE WHEN SUM(FE.Amount) IS NOT NULL THEN SUM(FE.Amount) ELSE 0 END AS SumaGastos FROM FamilyExpense FE"
                       + " WHERE FE.RecordStatus = ' ' AND FE.Project = '" + sitio + "' AND FE.FamilyId = " + familia;
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count > 0)
            {
                int sumaGastos = Int32.Parse(dt.Rows[0]["SumaGastos"].ToString());
                return sumaGastos;
            }
            else
            {
                return 0;
            }
        }
        public DataTable rsmTS(String idioma, String sitio, String familia, int meses)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT  CASE WHEN '" + idioma + "' = 'es' THEN ActivityEs ELSE ActivityEn END AS Actividad, dbo.fn_GEN_FormatDate(ActivityDateTime, '" + idioma + "') Fecha"
                        + " FROM         FamilyActivityReport"
                        + " WHERE   FunctionalArea = 'TS' AND  Project = '" + sitio + "' AND FamilyId = " + familia + " AND dbo.fn_GEN_mesesEntreFechas(ActivityDateTime) <= " + meses
                        + " ORDER BY ActivityDateTime DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable rsmVvnd(String idioma, String sitio, String familia)
        {
            SqlConnection con = new SqlConnection(conexionStringCautionOriginal);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @" SELECT YEAR(AnalysisDateTime) Año, CASE WHEN FLA.PostAnalysis = 0 THEN 'PRE' ELSE 'POST' END AS Tipo, CASE WHEN FLA.Applies = 1 THEN CASE WHEN '" + idioma + "' = 'es' THEN 'Sí' ELSE 'Yes' END ELSE 'No' END Aplica"
                        + ", CASE WHEN '" + idioma + "' = 'es' THEN cdD.DescSpanish ELSE cdD.DescEnglish END AS Diagnostico"
                        + ", FLA.Notes Notas"
                        + ", CONVERT(varchar,FS.Quantity) +  ' ' + CASE WHEN '" + idioma + "' = 'es' THEN cdT.DescSpanish ELSE cdT.DescEnglish END + ' (' + CONVERT(varchar, FS.DimensionX) + 'x'+CONVERT(varchar, FS.DimensionY) + ')' Solicitud"
                        + ", CASE WHEN '" + idioma + "' = 'es' THEN cdS.DescSpanish ELSE cdS.DescEnglish END AS Estado, FS.TotalHours 'Hrs Requeridas'"
                        + ", CASE WHEN FS.Exoneration IS NULL OR FS.Exoneration = 0 THEN '' ELSE 'Si' END Exoneracion"
                        + ", FS.HoursWorked 'Hrs Trabajadas'"
                        + " FROM dbo.FamilyLivingAnalysis FLA"
                        + " INNER JOIN dbo.CdFamilyLivingDiagnosis cdD ON cdD.Code = FLA.Diagnosis"
                        + " LEFT JOIN dbo.FamilyAmbFamSolicitude FS ON FLA.IdAnalysis = FS.IdAnalysis AND FLA.RecordStatus = FS.RecordStatus"
                        + " LEFT JOIN dbo.CdFamilyProgramStatus cdS ON cdS.Code = FS.Status"
                        + " LEFT JOIN dbo.CdSolicitudeType cdT ON cdT.Code = FS.Material"
                        + " WHERE FLA.RecordStatus = ' ' AND FLA.Project  = '" + sitio + "' AND FLA.FamilyId = " + familia
                        + " ORDER BY YEAR(AnalysisDateTime) DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public int vstVerificarSiExisteObjetivo(String sitio, String objetivo, String idVisita)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String sql = @"SELECT * FROM FamilyVisitObjective FVO 
                            WHERE FVO.RecordStatus = ' ' AND FVO.Objective = @objetivo AND FVO.Project = @sitio AND FVO.FamilyVisitId = @idVisita";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@objetivo", objetivo);
            comando.Parameters.AddWithValue("@idVisita", idVisita);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos.Rows.Count;
        }

        public DataTable vstObjObtenerObjetivosdeVisita(String sitio, String idioma, String idVisita)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String sql = @"SELECT CdFVO.Code AS Objetivo, CASE WHEN @idioma = 'es' THEN CdFVO.DescSpanish ELSE CdFVO.DescEnglish END AS Des, CASE WHEN FVO.FamilyVisitId IS NULL THEN 0 ELSE 1 END AS Tiene FROM CdFamilyVisitObjective CdFVO 
                            LEFT JOIN FamilyVisitObjective FVO ON FVO.RecordStatus = ' ' AND CdFVO.Code = FVO.Objective AND CdFVO.Project = FVO.Project AND FVO.FamilyVisitId = @idVisita
                            WHERE CdFVO.Project = @sitio  AND CdFVO.Inactive = 0
                            ORDER BY CASE WHEN @idioma = 'es' THEN CdFVO.DescSpanish ELSE CdFVO.DescEnglish END";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idVisita", idVisita);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public void vstObjIngresar(String sitio, String idVisita, String objetivo, String usuario)
        {
            String fechaCreacion = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"INSERT INTO FamilyVisitObjective (FamilyVisitId, Project, CreationDateTime, Objective, UserId, RecordStatus) 
                                    VALUES (@idVisita, @sitio, @fechaCreacion, @objetivo, @usuario, ' ')";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idVisita", idVisita);
            comando.Parameters.AddWithValue("@objetivo", objetivo);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void vstObjEliminar(String sitio, String idVisita, String objetivo, String usuario)
        {
            vstObjIngresarHistorico(sitio, idVisita, objetivo, usuario);
            vstObjPasarHistorico(sitio, idVisita, objetivo);
        }

        protected void vstObjPasarHistorico(String sitio, String idVisita, String objetivo)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE FamilyVisitObjective SET RecordStatus = 'H', ExpirationDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE Project = '" + sitio + "' AND FamilyVisitId = '" + idVisita + "' AND Objective = '" + objetivo + "' AND RecordStatus = ' '";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected Boolean vstObjIngresarHistorico(String sitio, String idVisita, String objetivo, String usuario)
        {
            String ahora = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            String fechaCreacion = ahora;
            String fechaExpiracion = ahora;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"INSERT INTO FamilyVisitObjective (FamilyVisitId, Project, CreationDateTime, Objective, UserId, RecordStatus, ExpirationDateTime) 
                                    SELECT FamilyVisitId, Project, @fechaCreacion, Objective, @usuario, 'H' , @fechaExpiracion FROM FamilyVisitObjective
                                    WHERE Project = @sitio AND FamilyVisitId = @idVisita AND Objective = @objetivo AND RecordStatus = ' '";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idVisita", idVisita);
            comando.Parameters.AddWithValue("@objetivo", objetivo);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
            comando.ExecuteNonQuery();
            conexion.Close();
            return true;
        }



        public Boolean vstActualizar(String sitio, String familia, String tipo, String fecha, String creacionFecha, String visitaNuevaFecha, String usuario, String F1, Boolean F1V, String F2, Boolean F2V, String F3, Boolean F3V, String F4, Boolean F4V, String F, String S1, Boolean S1V, String S2, Boolean S2V, String S3, Boolean S3V, String S, String E1, Boolean E1V, String E2, Boolean E2V, String E3, Boolean E3V, String E4, Boolean E4V, String E, String V, String L1, Boolean L1V, String L2, Boolean L2V, String L3, Boolean L3V, String L4, Boolean L4V, String L5, Boolean L5V, String L6, Boolean L6V, String L, String idVisita)
        {
            if (!vstVerificarActualizacion(sitio, familia, tipo, visitaNuevaFecha, creacionFecha))
            {
                return false;
            }
            vstPasarHistorico(sitio, familia, tipo, fecha, creacionFecha);
            vstIngresar(sitio, familia, tipo, visitaNuevaFecha, usuario, F1, F1V, F2, F2V, F3, F3V, F4, F4V, F, S1, S1V, S2, S2V, S3, S3V, S, E1, E1V, E2, E2V, E3, E3V, E4, E4V, E, V, L1, L1V, L2, L2V, L3, L3V, L4, L4V, L5, L5V, L6, L6V, L, idVisita);
            return true;
        }
        public Boolean vstEliminar(String sitio, String familia, String tipo, String fecha, String creacionFecha, String usuario)
        {
            vstIngresarHistorico(sitio, familia, tipo, fecha, usuario, creacionFecha);
            vstPasarHistorico(sitio, familia, tipo, fecha, creacionFecha);
            return true;
        }
        public String vstPasarHistorico(String sitio, String familia, String visitaTipo, String visitaFecha, String creacionFecha)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE dbo.FamilyVisit2 SET RecordStatus = 'H', ExpirationDateTime = '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "' WHERE Project= '" + sitio + "' AND FamilyId = " + familia + " AND VisitType = '" + visitaTipo + "' AND VisitDate = '" + visitaFecha + "' AND CreationDateTime = '" + creacionFecha + "'";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.ExecuteNonQuery();
            conexion.Close();
            return comandoString;
        }
        public Boolean vstIngresar(String sitio, String familia, String tipo, String fecha, String usuario, String F1, Boolean F1V, String F2, Boolean F2V, String F3, Boolean F3V, String F4, Boolean F4V, String F, String S1, Boolean S1V, String S2, Boolean S2V, String S3, Boolean S3V, String S, String E1, Boolean E1V, String E2, Boolean E2V, String E3, Boolean E3V, String E4, Boolean E4V, String E, String V, String L1, Boolean L1V, String L2, Boolean L2V, String L3, Boolean L3V, String L4, Boolean L4V, String L5, Boolean L5V, String L6, Boolean L6V, String L, String idVisita)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            if (!vstVerificarIngreso(sitio, familia, tipo, fecha))
            {
                return false;
            }
            String comandoString = @"INSERT INTO dbo.FamilyVisit2 (Project, FamilyId, VisitType, VisitDate, CreationDateTime, UserId, RecordStatus, F1,F1V,F2,F2V,F3,F3V,F4,F4V,F,S1,S1V,S2,S2V,S3,S3V,S,E1,E1V,E2,E2V,E3,E3V,E4,E4V,E,V,L1,L1V,L2,L2V,L3,L3V,L4,L4V,L5,L5V,L6,L6V,L,FamilyVisitId) VALUES (@sitio, @idFamilia, @tipo, @fecha, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', @usuario, ' ', @F1, @F1V, @F2, @F2V, @F3, @F3V, @F4, @F4V, @F, @S1, @S1V, @S2, @S2V, @S3, @S3V, @S, @E1, @E1V, @E2, @E2V, @E3, @E3V, @E4, @E4V, @E, @V, @L1, @L1V, @L2, @L2V, @L3, @L3V, @L4, @L4V, @L5, @L5V, @L6, @L6V, @L,@idVisita);";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idFamilia", familia);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@fecha", fecha);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@F1", F1);
            comando.Parameters.AddWithValue("@F1V", F1V);
            comando.Parameters.AddWithValue("@F2", F2);
            comando.Parameters.AddWithValue("@F2V", F2V);
            comando.Parameters.AddWithValue("@F3", F3);
            comando.Parameters.AddWithValue("@F3V", F3V);
            comando.Parameters.AddWithValue("@F4", F4);
            comando.Parameters.AddWithValue("@F4V", F4V);
            comando.Parameters.AddWithValue("@F", F);
            comando.Parameters.AddWithValue("@S1", S1);
            comando.Parameters.AddWithValue("@S1V", S1V);
            comando.Parameters.AddWithValue("@S2", S2);
            comando.Parameters.AddWithValue("@S2V", S2V);
            comando.Parameters.AddWithValue("@S3", S3);
            comando.Parameters.AddWithValue("@S3V", S3V);
            comando.Parameters.AddWithValue("@S", S);
            comando.Parameters.AddWithValue("@E1", E1);
            comando.Parameters.AddWithValue("@E1V", E1V);
            comando.Parameters.AddWithValue("@E2", E2);
            comando.Parameters.AddWithValue("@E2V", E2V);
            comando.Parameters.AddWithValue("@E3", E3);
            comando.Parameters.AddWithValue("@E3V", E3V);
            comando.Parameters.AddWithValue("@E4", E4);
            comando.Parameters.AddWithValue("@E4V", E4V);
            comando.Parameters.AddWithValue("@E", E);
            comando.Parameters.AddWithValue("@V", V);
            comando.Parameters.AddWithValue("@L1", L1);
            comando.Parameters.AddWithValue("@L1V", L1V);
            comando.Parameters.AddWithValue("@L2", L2);
            comando.Parameters.AddWithValue("@L2V", L2V);
            comando.Parameters.AddWithValue("@L3", L3);
            comando.Parameters.AddWithValue("@L3V", L3V);
            comando.Parameters.AddWithValue("@L4", L4);
            comando.Parameters.AddWithValue("@L4V", L4V);
            comando.Parameters.AddWithValue("@L5", L5);
            comando.Parameters.AddWithValue("@L5V", L5V);
            comando.Parameters.AddWithValue("@L6", L6);
            comando.Parameters.AddWithValue("@L6V", L6V);
            comando.Parameters.AddWithValue("@L", L);
            comando.Parameters.AddWithValue("@idVisita", idVisita);
            comando.ExecuteNonQuery();
            conexion.Close();
            return true;
        }

        public Boolean vstIngresarHistorico(String sitio, String familia, String tipo, String fecha, String usuario, String fechaCreacionSLCT)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"INSERT INTO dbo.FamilyVisit2 (Project, FamilyId, VisitType, VisitDate, CreationDateTime, UserId, RecordStatus, ExpirationDateTime, F1,F1V,F2,F2V,F3,F3V,F4,F4V,F,S1,S1V,S2,S2V,S3,S3V,S,E1,E1V,E2,E2V,E3,E3V,E4,E4V,E,V,L1,L1V,L2,L2V,L3,L3V,L4,L4V,L5,L5V,L6,L6V,L,FamilyVisitId) 
            SELECT Project, FamilyId, VisitType, VisitDate, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', @usuario, 'H', '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + @"',F1,F1V,F2,F2V,F3,F3V,F4,F4V,F,S1,S1V,S2,S2V,S3,S3V,S,E1,E1V,E2,E2V,E3,E3V,E4,E4V,E,V,L1,L1V,L2,L2V,L3,L3V,L4,L4V,L5,L5V,L6,L6V,L,FamilyVisitId
            FROM FamilyVisit2
            WHERE Project = @sitio AND FamilyId = @idFamilia AND VisitType = @tipo AND VisitDate = @fecha AND CreationDateTime = @fechaCreacion";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idFamilia", familia);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@fecha", fecha);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacionSLCT);
            comando.ExecuteNonQuery();
            conexion.Close();
            return true;
        }
        protected Boolean vstVerificarActualizacion(String sitio, String idFamilia, String tipoVisita, String fechaVisita, String fechaCreacion)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM FamilyVisit2 FV2 "
                         + " WHERE FV2.FamilyId = " + idFamilia + " AND FV2.Project = '" + sitio + "' AND FV2.RecordStatus = ' '"
                         + " AND FV2.VisitType = '" + tipoVisita + "' AND YEAR(FV2.VisitDate) = YEAR('" + fechaVisita + "') AND MONTH(FV2.VisitDate) = MONTH('" + fechaVisita + "') AND DAY(FV2.VisitDate) = DAY('" + fechaVisita + "') AND FV2.CreationDateTime != '" + fechaCreacion + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }
        protected Boolean vstVerificarIngreso(String sitio, String idFamilia, String tipoVisita, String fechaVisita)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM FamilyVisit2 FV2 "
                         + " WHERE FV2.FamilyId = " + idFamilia + " AND FV2.Project = '" + sitio + "' AND FV2.RecordStatus = ' '"
                         + " AND FV2.VisitType = '" + tipoVisita + "' AND YEAR(FV2.VisitDate) = YEAR('" + fechaVisita + "') AND MONTH(FV2.VisitDate) = MONTH('" + fechaVisita + "') AND DAY(FV2.VisitDate) = DAY('" + fechaVisita + "')";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }
        public DataTable vstObtenerVisitaEspecifica(String sitio, String idFamilia, String idioma, String tipoVisita, String fechaVisita, String fechaCreacion)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT dbo.fn_GEN_FormatDate(VisitDate, '" + idioma + "') AS fechaVisita, *"
                         + " FROM FamilyVisit2 FV2 "
                         + " WHERE FV2.FamilyId = " + idFamilia + " AND FV2.Project = '" + sitio + "' AND FV2.RecordStatus = ' '"
                         + " AND FV2.VisitType = '" + tipoVisita + "' AND FV2.VisitDate = '" + fechaVisita + "' AND FV2.CreationDateTime = '" + fechaCreacion + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable vstObtenerSituaciones(String idioma, String categ)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT CASE WHEN '" + idioma + "'='es'"
                + " THEN DescSpanish"
                + " ELSE DescEnglish"
                + " END AS Des, Code"
                + " FROM CdFamilySubCategoryVisit WHERE PROJECT = 'F' AND Category='" + categ + "' AND ACTIVE = 1";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable vstObtenerTipos(String sitio, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT CASE WHEN @idioma='es' 
                THEN cdFVT.DescSpanish 
                ELSE cdFVT.DescEnglish 
                END AS Des, cdFVT.Code 
                FROM CdFamilyVisitType cdFVT
                INNER JOIN CdFamilyVisitTypeProjectRelation cdFVTPR ON cdFVT.Code = cdFVTPR.Type AND cdFVTPR.Project = @sitio
                ORDER BY CASE WHEN @idioma='es' 
                THEN cdFVT.DescSpanish 
                ELSE cdFVT.DescEnglish 
                END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@sitio", sitio);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable vstObtenerVisitas(String sitio, String idFamilia, String idioma)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT CASE WHEN '" + idioma + "'  = 'es'"
                         + " THEN CFAT.DescSpanish "
                         + " ELSE CFAT.DescEnglish "
                         + " END AS Des, VisitDate, (SELECT dbo.fn_GEN_FormatDate(VisitDate, '" + idioma + "')) AS VisitDateUser, UserId, VisitType, CONVERT(char(26), CreationDateTime, 21) AS CreationDateTime"
                         + " FROM FamilyVisit2 FV2"
                         + " INNER JOIN CdFamilyVisitType CFAT ON FV2.VisitType = CFAT.Code"
                         + " WHERE FV2.FamilyId = " + idFamilia + " AND FV2.Project = '" + sitio + "' AND FV2.RecordStatus = ' ' ORDER BY FV2.VisitDate DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public String vvnActualizarCondiciones(String sitio, String familia, String fechaInicio, String usuario, String tenencia, String tamañoX, String tamañoY, String numeroCuartos, String materialPared, String calidadPared, String materialTecho, String calidadTecho, String materialPiso, String calidadPiso, String ccnaMaterialPared, String ccnaCalidadPared, String agua, String electricidad, String baño, String drenaje, String higiene, String higieneNotas, String ccnaMaterialTecho, String ccnaCalidadTecho, String jardinTamañoX, String jardinTamañoY, String techoNotas, String paredNotas, String ccnaTechoNotas, String ccnaParedNotas, String pisoNotas, Boolean segundoPiso, Boolean tieneEscritura, Boolean activo)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO dbo.FamilyLivingCondition (Project, FamilyId, CreationDateTime, StartDate, RecordStatus, UserId, Ownership, PropertySizeX, PropertySizeY, NumberOfRooms, WallMaterial, WallMaterialQuality, CeilingMaterial, CeilingMaterialQuality, FloorMaterial, FloorMaterialQuality, KitchenWallMaterial, KitchenWallMaterialQuality, Water, Electricity, Bathroom, Drainage, Hygiene, HygieneNotes, KitchenCeilingMaterial, KitchenCeilingMaterialQuality, CultivatedPropertySizeX, CultivatedPropertySizeY, CeilingNotes, WallNotes, KitchenCeilingNotes, KitchenWallNotes, FloorNotes, Has2ndFloor, HouseDeed, Active) VALUES (@sitio, @idFamilia, '" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', @fechaInicio, ' ', @usuario, @tenencia, @tamañoX, @tamañoY, @numeroCuartos, @materialPared, @calidadPared, @materialTecho, @calidadTecho, @materialPiso, @calidadPiso, @ccnaMaterialPared, @ccnaCalidadPared, @agua, @electricidad, @baño, @drenaje, @higiene, @higieneNotas, @ccnaMaterialTecho, @ccnaCalidadTecho, @jardinTamañoX, @jardinTamañoY, @techoNotas, @paredNotas, @ccnaTechoNotas, @ccnaParedNotas, @pisoNotas, @segundoPiso, @tieneEscritura, @activo);";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idFamilia", familia);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@tenencia", tenencia);
            comando.Parameters.AddWithValue("@tamañoX", tamañoX);
            comando.Parameters.AddWithValue("@tamañoY", tamañoY);
            comando.Parameters.AddWithValue("@numeroCuartos", numeroCuartos);
            comando.Parameters.AddWithValue("@materialPared", materialPared);
            comando.Parameters.AddWithValue("@calidadPared", calidadPared);
            comando.Parameters.AddWithValue("@materialTecho", materialTecho);
            comando.Parameters.AddWithValue("@calidadTecho", calidadTecho);
            comando.Parameters.AddWithValue("@materialPiso", materialPiso);
            comando.Parameters.AddWithValue("@calidadPiso", calidadPiso);
            comando.Parameters.AddWithValue("@ccnaMaterialPared", ccnaMaterialPared);
            comando.Parameters.AddWithValue("@ccnaCalidadPared", ccnaCalidadPared);
            comando.Parameters.AddWithValue("@agua", agua);
            comando.Parameters.AddWithValue("@electricidad", electricidad);
            comando.Parameters.AddWithValue("@baño", baño);
            comando.Parameters.AddWithValue("@drenaje", drenaje);
            comando.Parameters.AddWithValue("@higiene", higiene);
            comando.Parameters.AddWithValue("@higieneNotas", higieneNotas);
            comando.Parameters.AddWithValue("@ccnaMaterialTecho", ccnaMaterialTecho);
            comando.Parameters.AddWithValue("@ccnaCalidadTecho", ccnaCalidadTecho);
            comando.Parameters.AddWithValue("@jardinTamañoX", jardinTamañoX);
            comando.Parameters.AddWithValue("@jardinTamañoY", jardinTamañoY);
            comando.Parameters.AddWithValue("@techoNotas", techoNotas);
            comando.Parameters.AddWithValue("@paredNotas", paredNotas);
            comando.Parameters.AddWithValue("@ccnaTechoNotas", ccnaTechoNotas);
            comando.Parameters.AddWithValue("@ccnaParedNotas", ccnaParedNotas);
            comando.Parameters.AddWithValue("@pisoNotas", pisoNotas);
            comando.Parameters.AddWithValue("@segundoPiso", segundoPiso);
            comando.Parameters.AddWithValue("@tieneEscritura", tieneEscritura);
            comando.Parameters.AddWithValue("@activo", activo);
            comando.ExecuteNonQuery();
            conexion.Close();
            return comandoString;
        }

        public String vvnCambiarEstadoAInactivo(String sitio, String familia, String usuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO dbo.FamilyLivingCondition (Project, FamilyId, CreationDateTime, StartDate, RecordStatus, UserId, Ownership, PropertySizeX, PropertySizeY, NumberOfRooms, WallMaterial, WallMaterialQuality, CeilingMaterial, CeilingMaterialQuality, FloorMaterial, FloorMaterialQuality, KitchenWallMaterial, KitchenWallMaterialQuality, Water, Electricity, Bathroom, Drainage, Hygiene, HygieneNotes, KitchenCeilingMaterial, KitchenCeilingMaterialQuality, CultivatedPropertySizeX, CultivatedPropertySizeY, CeilingNotes, WallNotes, KitchenCeilingNotes, KitchenWallNotes, FloorNotes, Has2ndFloor, HouseDeed, Active) 
                                     SELECT Project, FamilyId, CONVERT(VARCHAR, GETDATE(), 20), StartDate, RecordStatus, @usuario, Ownership, PropertySizeX, PropertySizeY, NumberOfRooms, WallMaterial, WallMaterialQuality, CeilingMaterial, CeilingMaterialQuality, FloorMaterial, FloorMaterialQuality, KitchenWallMaterial, KitchenWallMaterialQuality, Water, Electricity, Bathroom, Drainage, Hygiene, HygieneNotes, KitchenCeilingMaterial, KitchenCeilingMaterialQuality, CultivatedPropertySizeX, CultivatedPropertySizeY, CeilingNotes, WallNotes, KitchenCeilingNotes, KitchenWallNotes, FloorNotes, Has2ndFloor, HouseDeed, 0
                                     FROM FamilyLivingCondition FLC
                                     WHERE FLC.RecordStatus = ' ' AND FLC.Project = @sitio AND FLC.FamilyId = @familia AND FLC.Active = 1";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@usuario", usuario); 
            comando.ExecuteNonQuery();
            conexion.Close();
            return comandoString;
        }

        public void vvnIngresarHistorico(String sitio, String familia, String fechaInicio, String usuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO FamilyLivingCondition (Project, FamilyId, CreationDateTime, StartDate, RecordStatus, ExpirationDateTime, UserId, Ownership, PropertySizeX, PropertySizeY, NumberOfRooms, WallMaterial, WallMaterialQuality, CeilingMaterial, CeilingMaterialQuality, FloorMaterial, FloorMaterialQuality, KitchenWallMaterial, KitchenWallMaterialQuality, Water, Electricity, Bathroom, Drainage, Hygiene, HygieneNotes, KitchenCeilingMaterial, KitchenCeilingMaterialQuality, CultivatedPropertySizeX, CultivatedPropertySizeY, CeilingNotes, WallNotes, KitchenCeilingNotes, KitchenWallNotes, FloorNotes, Has2ndFloor, HouseDeed, Active) 
                                     SELECT Project, FamilyId, CONVERT(VARCHAR, GETDATE(), 20), StartDate, 'H', CONVERT(VARCHAR, GETDATE(), 20), @usuario, Ownership, PropertySizeX, PropertySizeY, NumberOfRooms, WallMaterial, WallMaterialQuality, CeilingMaterial, CeilingMaterialQuality, FloorMaterial, FloorMaterialQuality, KitchenWallMaterial, KitchenWallMaterialQuality, Water, Electricity, Bathroom, Drainage, Hygiene, HygieneNotes, KitchenCeilingMaterial, KitchenCeilingMaterialQuality, CultivatedPropertySizeX, CultivatedPropertySizeY, CeilingNotes, WallNotes, KitchenCeilingNotes, KitchenWallNotes, FloorNotes, Has2ndFloor, HouseDeed, Active
                                     FROM FamilyLivingCondition FLC
                                     WHERE FLC.RecordStatus = ' ' AND FLC.Project = @sitio AND FLC.FamilyId = @familia AND FLC.StartDate = @fechaInicio";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
            vvnConvertirAHistorico(sitio, familia, fechaInicio);
        }

        protected void vvnConvertirAHistorico(String sitio, String familia, String fechaInicio)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE FamilyLivingCondition SET RecordStatus = 'H', ExpirationDateTime = CONVERT(VARCHAR, GETDATE(), 20)
                                    FROM FamilyLivingCondition
                                    WHERE RecordStatus = ' ' AND Project = @sitio AND FamilyId = @familia AND StartDate = @fechaInicio";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@fechaInicio", fechaInicio);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public DataTable vvnObtenerCondicionesAct(String sitio, String familia)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM FamilyLivingCondition FLC "
                         + " WHERE FLC.RecordStatus = ' ' AND FLC.FamilyId = " + familia + " AND FLC.Project = '" + sitio + "' AND FLC.Active = 1";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable vvnObtenerCondicionesEsp(String sitio, String familia, String fechaInicio)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                         + " FROM FamilyLivingCondition FLC "
                         + " WHERE FLC.RecordStatus = ' ' AND FLC.FamilyId = " + familia + " AND FLC.Project = '" + sitio + "' AND FLC.StartDate = '" + fechaInicio + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable vvnObtenerCondiciones(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String sql = @"SELECT CONVERT(VARCHAR, FLC.CreationDateTime, 21) AS CreationDateTime, 
                                  CONVERT(VARCHAR, FLC.StartDate, 21) AS StartDate, 
                                  FLC.Active AS Active,
                                  dbo.fn_GEN_FormatDate(FLC.StartDate, @idioma) AS FechaInicio, 
                                  CASE WHEN @idioma = 'es' THEN CASE WHEN FLC.Active = 1 THEN 'Activo' ELSE 'Inactivo' END ELSE CASE WHEN FLC.Active = 1 THEN 'Active' ELSE 'Inactive' END END AS Estado,
                                  FLC.UserId AS Usuario   
                            FROM FamilyLivingCondition FLC
                            WHERE FLC.RecordStatus = ' ' AND FLC.Project = @sitio AND FLC.FamilyId = @familia AND Active = 0
                            ORDER BY FLC.StartDate DESC";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }


        public Boolean vvrVerificarIngreso(String sitio, String idFamilia, String razon, String fechaEntrega)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT *"
                        + " FROM FamilyHelp FH "
                        + " WHERE FH.FamilyId = " + idFamilia + " AND FH.Project = '" + sitio + "' AND FH.RecordStatus = ' '"
                        + " AND DAY(FH.AuthorizationDateTime) = DAY('" + fechaEntrega + "') AND MONTH(FH.AuthorizationDateTime) = MONTH('" + fechaEntrega + "') AND YEAR(FH.AuthorizationDateTime) = YEAR('" + fechaEntrega + "') AND FH.Reason = '" + razon + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            if (dt.Rows.Count == 0)
            {
                return true;
            }
            return false;
        }
    }
}