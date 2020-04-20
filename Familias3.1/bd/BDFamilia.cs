using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Familias3._1.bd
{
    public class BDFamilia
    {
        static String conexionString;
        public BDFamilia()
        {
            conexionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public DataTable obtenerAvisos(String sitio, String idFam, String idioma, Boolean mostrarNotasTS)
        {

            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"DECLARE @hoy DATETIME;
                                    SET @hoy = GETDATE();
                                    SELECT Aviso 
                                    FROM dbo.fn_GEN_Warnings (@sitio, @idFamilia, @hoy, @idioma, @mostrarNotasTS)
                                    WHERE Aviso IS NOT NULL";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idFamilia", idFam);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@mostrarNotasTS", mostrarNotasTS);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;

            //    DataTable dt = new DataTable();
            //    dt.Columns.Add("Aviso", typeof(string));
            //    dt.Rows.Add("");
            //    dt.Rows.Add("Debe entregar notas.");
            //    dt.Rows.Add("Debe escribir carta.");
            //    return dt;
        }



        public int obtenerIdAPartirDeFaro(String sitio, String idFaro)
        {
            if (idFaro.Equals("0"))
            {
                return 0;
            }
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT F.FamilyId AS Familia 
                                    FROM Family F
                                    WHERE F.RecordStatus = ' ' AND F.Project = @sitio AND F.RFaroNumber = @idFaro";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idFaro", idFaro);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            if (tablaDatos.Rows.Count > 0)
            {
                return Int32.Parse(tablaDatos.Rows[0]["Familia"].ToString());
            }
            else
            {
                return 0;
            }
        }

        public DataTable obtenerDatos(String sitio, String idFamilia, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT  
                CASE WHEN @idioma='es' 
                    THEN CdGeographicArea.DescSpanish 
                ELSE CdGeographicArea.DescEnglish
                    END AS Area, Family.Pueblo, Address, TelephoneNumber AS Phone, 
                CASE WHEN @idioma='es' 
                    THEN CdEthnicity.DescSpanish 
                ELSE CdEthnicity.DescEnglish 
                END AS Etnia,
                CASE WHEN @idioma='es' 
                    THEN CdAffiliationStatus.DescSpanish 
                ELSE CdAffiliationStatus.DescEnglish 
                END AS AfilEstado, 
                (SELECT dbo.fn_GEN_FormatDate(AffiliationStatusDate,@idioma)) AS AfilEstadoDate,
                Classification,
                (SELECT dbo.fn_GEN_FormatDate(ClassificationDate,@idioma)) AS ClassifDate,
                CdGeographicPueblo.Region, RFaroNumber, EmployeeId TS, YEAR(AffiliationStatusDate) AS AfilYear
            FROM Family  LEFT JOIN CdGeographicArea ON Family.Area = CdGeographicArea.Code 
            LEFT JOIN CdEthnicity ON Family.Ethnicity = CdEthnicity.Code 
            LEFT JOIN CdAffiliationStatus ON Family.AffiliationStatus = CdAffiliationStatus.Code 
            LEFT JOIN CdGeographicPueblo ON Family.Pueblo = CdGeographicPueblo.Pueblo 
			LEFT JOIN FamilyEmployeeRelation FER ON FER.RecordStatus = Family.RecordStatus AND FER.Project = Family.Project AND FER.FamilyId = Family.FamilyId  AND FER.EndDate IS NULL
            WHERE Family.FamilyId LIKE @idFamilia AND Family.RecordStatus LIKE ' ' AND Family.Project LIKE @sitio";
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

        public DataTable obtenerDatos(String sitio, String familia)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT * FROM Family F
            WHERE F.RecordStatus = ' ' AND F.FamilyId LIKE @familia AND F.Project = @sitio";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        
        public DataTable obtenerActivos(String sitio, String idFamilia, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT M.MemberId, M.FirstNames + ' ' + M.LastNames AS Nombre,
                    CASE WHEN @idioma = 'es'
                        THEN cdMRT.DescSpanish 
                        ELSE cdMRT.DescEnglish
                    END Relacion,
                    CASE WHEN @idioma = 'es'
                        THEN cdAS.DescSpanish
                        ELSE cdAS.DescEnglish
                    END AfilStatus,
                    (SELECT dbo.fn_GEN_FormatDate(M.BirthDate,@idioma)) AS BirthDate,
                    CASE WHEN @idioma = 'es'
                        THEN cdAT.DescSpanish
                        ELSE cdAT.DescEnglish
                    END TipoAfil,
                    CASE WHEN @idioma = 'es'
                     THEN cdOA.DescSpanish
                     ELSE cdOA.DescEnglish
                 END OtraAfil, CellularPhoneNumber FROM dbo.Member M 
            INNER JOIN dbo.FamilyMemberRelation FMR ON M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.LastFamilyId = FMR.FamilyId AND M.RecordStatus = FMR.RecordStatus 
            INNER JOIN dbo.CdFamilyMemberRelationType cdMRT ON FMR.Type = cdMRT.Code 
            LEFT OUTER JOIN dbo.CdOtherAffiliation cdOA ON M.OtherAffiliation = cdOA.Code 
            LEFT OUTER JOIN dbo.CdAffiliationStatus cdAS ON M.AffiliationStatus = cdAS.Code 
            LEFT OUTER JOIN dbo.CdAffiliationType cdAT ON M.AffiliationType = cdAT.Code 
            WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND FMR.FamilyId = @idFamilia AND FMR.InactiveReason IS NULL
            ORDER BY cdMRT.DisplayOrder";
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
            //DataTable dt = new DataTable();
            //dt.Columns.Add("MemberId", typeof(String));
            //dt.Columns.Add("Nombre", typeof(String));
            //dt.Columns.Add("Relacion", typeof(String));
            //dt.Columns.Add("AfilStatus", typeof(String));
            //dt.Columns.Add("TipoAfil", typeof(String));
            //dt.Columns.Add("BirthDate", typeof(String));
            //dt.Columns.Add("OtraAfil", typeof(String));
            //dt.Rows.Add("15596", "Tipo", "Padre", "Basico", "Tipo", "2019-23-12", "No tiene");
            //dt.Rows.Add("15596", "Tipo", "Padre", "Basico", "Tipo", "2019-23-12", "No tiene");
            //dt.Rows.Add("15596", "Tipo", "Padre", "Basico", "Tipo", "2019-23-12", "No tiene");
            //dt.Rows.Add("15596", "Tipo", "Padre", "Basico", "Tipo", "2019-23-12", "No tiene");
            //return dt;
        }
        public DataTable obtenerInactivos(String sitio, String idFamilia, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT M.MemberId, M.FirstNames + ' ' + M.LastNames AS Nombre, 
                CASE WHEN @idioma = 'es'
                     THEN cdMRT.DescSpanish 
                     ELSE cdMRT.DescEnglish
                 END Relacion,
                CASE WHEN @idioma = 'es'
                     THEN cdAS.DescSpanish
                     ELSE cdAS.DescEnglish
                 END AfilStatus,
                CASE WHEN @idioma = 'es'
                     THEN cdAT.DescSpanish
                     ELSE cdAT.DescEnglish
                 END TipoAfil,
                (SELECT dbo.fn_GEN_FormatDate(M.BirthDate,@idioma)) AS BirthDate,
                CASE WHEN @idioma = 'es'
                     THEN CFMIR.DescSpanish
                     ELSE CFMIR.DescEnglish
                 END InacRazon,
                (SELECT dbo.fn_GEN_FormatDate(FMR.InactiveDate,@idioma)) AS InacFecha 
            FROM dbo.Member M 
            INNER JOIN dbo.FamilyMemberRelation FMR ON M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND (M.LastFamilyId = FMR.FamilyId OR FMR.InactiveReason IS NOT NULL) AND M.RecordStatus = FMR.RecordStatus 
            INNER JOIN dbo.CdFamilyMemberRelationType cdMRT ON FMR.Type = cdMRT.Code 
            LEFT OUTER JOIN dbo.CdAffiliationStatus cdAS ON M.AffiliationStatus = cdAS.Code 
            LEFT OUTER JOIN dbo.CdAffiliationType cdAT ON M.AffiliationType = cdAT.Code 
            LEFT OUTER JOIN CdFamMemRelInactiveReason CFMIR on FMR.InactiveReason=CFMIR.Code 
            WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND FMR.FamilyId = @idFamilia 
            ORDER BY cdMRT.DisplayOrder";
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

            //DataTable dt = new DataTable();
            //dt.Columns.Add("MemberId", typeof(String));
            //dt.Columns.Add("Nombre", typeof(String));
            //dt.Columns.Add("Relacion", typeof(String));
            //dt.Columns.Add("AfilStatus", typeof(String));
            //dt.Columns.Add("TipoAfil", typeof(String));
            //dt.Columns.Add("BirthDate", typeof(String));
            //dt.Columns.Add("InacRazon", typeof(String));
            //dt.Columns.Add("InacFecha", typeof(String));
            //dt.Rows.Add("15596", "Tipo", "Padre", "Basico", "Tipo", "2019-23-12", "No tiene", "2019-23-12");
            //dt.Rows.Add("15596", "Tipo", "Padre", "Basico", "Tipo", "2019-23-12", "No tiene", "2019-23-12");
            //dt.Rows.Add("15596", "Tipo", "Padre", "Basico", "Tipo", "2019-23-12", "No tiene", "2019-23-12");
            //dt.Rows.Add("15596", "Tipo", "Padre", "Basico", "Tipo", "2019-23-12", "No tiene", "2019-23-12");
            //dt.Rows.Add("15596", "Tipo", "Padre", "Basico", "Tipo", "2019-23-12", "No tiene", "2019-23-12");
            //return dt;
        }
        public DataTable obtenerFamiliasAfiliadas(String sitio, String idioma, String TS, String area)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT F.FamilyId, F.Project + '' + CONVERT(varchar(10),F.FamilyId) AS Familia, M.FirstNames + ' ' + M.LastNames AS JefeCasa, FER.EmployeeId AS TS, dbo.fn_GEN_FormatDate(FER.StartDate,'" + idioma + "') AS FechaInicio, F.Address AS Direccion, CASE WHEN '" + idioma + "' = 'es' THEN CGA.DescSpanish ELSE CGA.DescEnglish END AS Area FROM Family F "
                        + " LEFT JOIN FamilyEmployeeRelation FER ON F.Project = FER.Project AND F.FamilyId = FER.FamilyId AND F.RecordStatus = FER.RecordStatus AND FER.EndDate IS NULL"
                        + " INNER JOIN FamilyMemberRelation FMR ON F.Project = FMR.Project AND F.FamilyId = FMR.FamilyId AND F.RecordStatus = FMR.RecordStatus AND FMR.InactiveReason IS NULL AND FMR.Type IN ('JEFE','JEFM')"
                        + " INNER JOIN Member M ON M.Project = FMR.Project AND M.MemberId = FMR.MemberId AND M.RecordStatus = FMR.RecordStatus"
                        + " INNER JOIN CdGeographicArea CGA ON CGA.Code = F.Area"
                        + " WHERE F.RecordStatus = ' ' AND F.AffiliationStatus = 'AFIL' AND ((FER.EmployeeId = '" + TS + "' OR '' = '" + TS + "') OR ('" + TS + "' = 'NT' AND FER.EmployeeId IS NULL)) AND (F.Area = '" + area + "' OR '' = '" + area + "') AND F.Project = '" + sitio + "' AND F.Project IN ('F','M','R','N')"
                        + " ORDER BY Area, Familia, TS, JefeCasa DESC";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }
    }
}