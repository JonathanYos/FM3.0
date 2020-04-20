using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Familias3._1.bd
{
    public class BDMiembro
    {
        static String conexionString;
        public BDMiembro()
        {
            conexionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public int esAfiliado(String sitio, String idMiembro)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                String comandoString = @"SELECT dbo.fn_GEN_esAfiliado(@sitio,@idMiembro) R";
                SqlCommand comando = new SqlCommand(comandoString, conexion);
                comando.Parameters.AddWithValue("@sitio", sitio);
                comando.Parameters.AddWithValue("@idMiembro", idMiembro);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tablaDatos = new DataTable();
                adaptador.Fill(tablaDatos);
                conexion.Close();
                int result = (int)tablaDatos.Rows[0]["R"];
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
            //return 1;
        }

        public int esApadrinado(String sitio, String idMiembro)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                String comandoString = @"SELECT dbo.fn_GEN_esApadrinado (@sitio,@idMiembro) R";
                SqlCommand comando = new SqlCommand(comandoString, conexion);
                comando.Parameters.AddWithValue("@sitio", sitio);
                comando.Parameters.AddWithValue("@idMiembro", idMiembro);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tablaDatos = new DataTable();
                adaptador.Fill(tablaDatos);
                conexion.Close();
                int result = (int)tablaDatos.Rows[0]["R"];
                return result;
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        public String derechosSalud(String sitio, String idMiembro, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT dbo.fn_GEN_derechosSalud(@sitio,@idMiembro,@idioma) R";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idMiembro", idMiembro);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            String result = (String)tablaDatos.Rows[0]["R"];
            return result;
        }

        public int tieneProyeccion(String sitio, String idMiembro, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT dbo.fn_GEN_esProyeccionSocial(@sitio,@idMiembro,@idioma) R";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idMiembro", idMiembro);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            int result = (int)tablaDatos.Rows[0]["R"];
            return result;
        }

        public DataTable obtenerDatos(String sitio, String idMiembro, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT FirstNames, LastNames, PreferredName, Gender, BirthDate, FamilyId, dbo.fn_GEN_infoUltimoAñoEscolar(@sitio, @idMiembro, @idioma) AS Grado, Semaphore, LiveDead, Literacy, Id, Telefono, Fase, AffiliationType, AffiliationStatus, AffiliationStatusDate, Edad 
                    FROM fn_GEN_MemberGetInfoEd(@sitio, @idMiembro, @idioma, @now)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idMiembro", idMiembro);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@now", now);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public String obtenerFamId(String sitio, String idMiembro)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT FMR.FamilyId 
                FROM FamilyMemberRelation FMR 
                INNER JOIN Member M ON FMR.Project = M.Project AND FMR.MemberId = M.MemberId AND FMR.RecordStatus = M.RecordStatus 
                WHERE FMR.InactiveReason IS NULL AND FMR.RecordStatus = ' ' AND M.Project = @sitio AND M.MemberId= @idMiembro";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idMiembro", idMiembro);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            String result = (int)tablaDatos.Rows[0]["FamilyId"] + "";
            return result;
        }

        public DataTable obtenerPadres(String sitio, String idMiembro)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
//            String comandoString = @"SELECT  M.MemberId, M.FirstNames + ' ' + M.LastNames AS MemberNames
//                        FROM dbo.FamilyMemberRelation FMR 
//                        INNER JOIN dbo.Member M ON FMR.Project = M.Project AND FMR.MemberId = M.MemberId AND FMR.RecordStatus = M.RecordStatus 
//                        INNER JOIN dbo.CdFamilyMemberRelationType cdFMR ON FMR.Type = cdFMR.Code 
//                        WHERE   FMR.RecordStatus = ' ' AND FMR.InactiveDate IS NULL AND cdFMR.HeadOfHouse = 1  AND @sitio = FMR.Project AND FMR.FamilyId = (SELECT FMR2.FamilyId FROM FamilyMemberRelation FMR2 WHERE @idMiembro = FMR2.MemberId AND @sitio = FMR2.Project AND FMR2.Type IN ('HIJA','HIJO') AND FMR2.RecordStatus = ' ' AND FMR2.InactiveDate IS NULL)";
            String comandoString = @"SELECT  bpM.MemberId, bpM.FirstNames + ' ' + bpM.LastNames AS MemberNames
                        FROM Member M
                        INNER JOIN Member bpM ON M.RecordStatus = bpM.RecordStatus AND M.Project = bpM.Project AND (M.BiologicalMotherMemberId = bpM.MemberId OR M.BiologicalFatherMemberId = bpM.MemberId)
                        WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND M.MemberId = @idMiembro
                        ORDER BY bpM.MemberId";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idMiembro", idMiembro);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
            //DataTable dt = new DataTable();
            //dt.Columns.Add("MemberId", typeof(string));
            //dt.Columns.Add("MemberNames", typeof(string));
            //dt.Rows.Add("123", "Juan Pablo");
            //dt.Rows.Add("23", "Juan Dios");
            //return dt;
        }

        public DataTable obtenerPadrinos(String sitio, String idMiembro)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT SponsorMemberRelation.SponsorId, Sponsor.SponsorNames FROM dbo.SponsorMemberRelation inner join dbo.Sponsor on SponsorMemberRelation.SponsorId = Sponsor.SponsorId where MemberId like @idMiembro and SponsorMemberRelation.Project like @sitio and SponsorMemberRelation.RecordStatus like ' ' and EndDate is null and Sponsor.RecordStatus like ' '";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idMiembro", idMiembro);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
            //DataTable dt = new DataTable();
            //dt.Columns.Add("SponsorId", typeof(string));
            //dt.Columns.Add("SponsorNames", typeof(string));
            //dt.Rows.Add("123", "Juan Padrino Juan Padrino");
            //dt.Rows.Add("23", "Padrino Dios");
            //return dt;
        }

    }
}