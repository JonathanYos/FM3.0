using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Familias3._1.bd
{
    public class BDUsuario
    {
        static String conexionString;
        public BDUsuario()
        {
            conexionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }

        public int consultarCredenciales(String nombreUsuario, String contraseña)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                String comandoString = @"SELECT dbo.fn_GEN_verificarCredenciales(@usuario,@contraseña) R";
                SqlCommand comando = new SqlCommand(comandoString, conexion);
                comando.Parameters.AddWithValue("@usuario", nombreUsuario);
                comando.Parameters.AddWithValue("@contraseña", contraseña);
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

        public void cambiarContraseña(String usuario, String contraseña)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"UPDATE FwEmployeePassword SET Pass3 = Pass2, Pass2 = Pass1, Pass1 = Password, Password = @psw, PasswordDate = GETDATE() WHERE EmployeeId = @usuario"; 
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@psw", contraseña);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public Boolean verificarFuncion(String sitio, String usuario, String funcion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT COUNT(*) AS R  FROM FwRoleSecurity FRS 
                                        INNER JOIN FwEmployeeRole FER ON FRS.Role = FER.Role AND FER.Status = 'ACTV'
                                        INNER JOIN FwCdTransaction FCDT ON (FRS.Trans = FCDT.Code OR FRS.Trans = '****') AND FCDT.Inactive != 1
                                        WHERE FER.Organization = @sitio AND EmployeeId = @usuario AND FCDT.Code = @funcion";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@funcion", funcion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            int result = (int)tablaDatos.Rows[0]["R"];
            if (result > 0)
                return true;
            return false;
        }

        public void cambiarPreferencias(String usuario, String sitio, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"UPDATE dbo.FwEmployee SET DefaultOrganization=@sitio,PreferredLanguage = @idioma WHERE EmployeeId = @usuario";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        private DataTable consultarInfo(String nombreUsuario)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT FEMP.DefaultOrganization, FEMP.PreferredLanguage,
                CASE WHEN FEMP.PreferredLanguage = 'es'
                     THEN ORG.DescSpanish 
                     ELSE ORG.DescEnglish 
                 END Org 
                FROM dbo.FwEmployee FEMP INNER JOIN FwCdOrganization ORG ON ORG.Code = FEMP.DefaultOrganization WHERE FEMP.EmployeeId = @nombreUsuario";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@nombreUsuario", nombreUsuario);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public String consultarIdioma(String nombreUsuario)
        {
            DataTable tablaDatos = this.consultarInfo(nombreUsuario);
            return (String)tablaDatos.Rows[0]["PreferredLanguage"];
        }

        public String consultarSitio(String nombreUsuario)
        {
            DataTable tablaDatos = this.consultarInfo(nombreUsuario);
            return (String)tablaDatos.Rows[0]["DefaultOrganization"];
        }

        public String consultarSNombre(String nombreUsuario)
        {
            DataTable tablaDatos = this.consultarInfo(nombreUsuario);
            return (String)tablaDatos.Rows[0]["Org"];
        }
        public DataTable obtenerAreas(String sitio, String idioma)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' THEN DescSpanish ELSE DescEnglish END AS Des FROM FwCdFunctionalArea ORDER BY CASE WHEN @idioma = 'es' THEN DescSpanish ELSE DescEnglish END";
                SqlCommand comando = new SqlCommand(comandoString, conexion);
                comando.Parameters.AddWithValue("@sitio", sitio);
                comando.Parameters.AddWithValue("@idioma", idioma);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tablaDatos = new DataTable();
                adaptador.Fill(tablaDatos);
                conexion.Close();
                return tablaDatos;
            }
            catch (Exception e)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public DataTable obtenerAreasDeUsuario(String usuario, String sitio, String idioma)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                String comandoString = @"SELECT  
                             CASE WHEN @idioma = 'es'
                                 THEN FA.DescSpanish 
                                 ELSE FA.DescEnglish 
                             END Area,
                             FA.Code as Code
                             FROM FwEmployeeRole ER 
                             INNER JOIN FwRoleSecurity RS ON ER.Role = RS.Role 
                             INNER JOIN FwCdFunctionalArea FA ON FA.Code = RS.FunctionalArea OR RS.FunctionalArea = '****'
                             INNER JOIN FwEmployee E ON E.EmployeeId = ER.EmployeeId
                             WHERE ((ER.Status = 'ACTV' AND ER.EmployeeId = @usuario AND @sitio = ER.Organization) AND FA.Code != '****')
                             GROUP BY CASE WHEN @idioma = 'es'
                                 THEN FA.DescSpanish 
                                 ELSE FA.DescEnglish 
                             END, FA.Code
                             ORDER BY CASE WHEN @idioma = 'es'
                                 THEN FA.DescSpanish 
                                 ELSE FA.DescEnglish 
                             END";
                SqlCommand comando = new SqlCommand(comandoString, conexion);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@sitio", sitio);
                comando.Parameters.AddWithValue("@idioma", idioma);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tablaDatos = new DataTable();
                adaptador.Fill(tablaDatos);
                conexion.Close();
                return tablaDatos;
            }
            catch (Exception e)
            {
                return new DataTable();
            }

            //DataTable areas = new DataTable();
            //areas.Columns.Add("Code", typeof(string));
            //areas.Columns.Add("Area", typeof(string));
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //areas.Rows.Add("APAD", "APADRINAMIENTO");
            //areas.Rows.Add("APAD", "SOL");
            //return areas;
        }

        public DataTable obtenerFuncionesDeUsuarioTodas(String usuario, String sitio, String idioma, String codArea)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                String comandoString = @"SELECT DISTINCT 
                TS.Code,
                CASE WHEN @idioma = 'es'
					THEN TS.DescSpanish
					ELSE TS.DescEnglish
				END Trans
				FROM FwEmployeeRole ER
				INNER JOIN FwRoleSecurity RS on RS.Role = ER.Role
				INNER JOIN FwCdTransaction TS ON TS.Code = RS.Trans OR (RS.Trans = '****' AND ((RS.FunctionalArea = '****') OR (RS.FunctionalArea = TS.FunctionalArea)))
				WHERE ER.Status = 'ACTV' AND TS.Inactive !=1 AND TS.HasRoleSecurity = 1 AND ER.EmployeeId = @usuario and ER.Organization = @sitio AND TS.FunctionalArea = @codArea               
                UNION ALL
				SELECT 
                TS.Code,
                CASE WHEN @idioma = 'es'
					THEN TS.OutlookBarNameSpanish
					ELSE TS.OutlookBarNameEnglish
				END Trans
				FROM FwCdTransaction TS
				WHERE TS.FunctionalArea = @codArea  AND TS.Inactive !=1 AND TS.HasRoleSecurity = 0 AND TS.JspFileNameForOutlookBar  IS NOT NULL";
                SqlCommand comando = new SqlCommand(comandoString, conexion);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@sitio", sitio);
                comando.Parameters.AddWithValue("@idioma", idioma);
                comando.Parameters.AddWithValue("@codArea", codArea);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tablaDatos = new DataTable();
                adaptador.Fill(tablaDatos);
                conexion.Close();
                return tablaDatos;
            }
            catch (Exception e)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }


        public DataTable obtenerFuncionesDeUsuario(String usuario, String sitio, String idioma, String codArea)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                String comandoString = @"SELECT DISTINCT 
                TS.Code,
                CASE WHEN @idioma = 'es'
					THEN TS.OutlookBarNameSpanish
					ELSE TS.OutlookBarNameEnglish
				END Trans
				FROM FwEmployeeRole ER
				INNER JOIN FwRoleSecurity RS on RS.Role = ER.Role
				INNER JOIN FwCdTransaction TS ON TS.Code = RS.Trans OR (RS.Trans = '****' AND ((RS.FunctionalArea = '****') OR (RS.FunctionalArea = TS.FunctionalArea)))
				WHERE ER.Status = 'ACTV' AND TS.Inactive !=1 AND TS.HasRoleSecurity = 1 AND ER.EmployeeId = @usuario and ER.Organization = @sitio AND TS.FunctionalArea = @codArea AND TS.JspFileNameForOutlookBar  IS NOT NULL               
                UNION ALL
				SELECT 
                TS.Code,
                CASE WHEN @idioma = 'es'
					THEN TS.OutlookBarNameSpanish
					ELSE TS.OutlookBarNameEnglish
				END Trans
				FROM FwCdTransaction TS
				WHERE TS.FunctionalArea = @codArea  AND TS.Inactive !=1 AND TS.HasRoleSecurity = 0 AND TS.JspFileNameForOutlookBar  IS NOT NULL";
                SqlCommand comando = new SqlCommand(comandoString, conexion);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@sitio", sitio);
                comando.Parameters.AddWithValue("@idioma", idioma);
                comando.Parameters.AddWithValue("@codArea", codArea);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tablaDatos = new DataTable();
                adaptador.Fill(tablaDatos);
                conexion.Close();
                return tablaDatos;
            }
            catch (Exception e)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public DataTable obtenerFuncionesDeUsuario(String usuario, String sitio)
        {
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);
                conexion.Open();
                String comandoString = @"SELECT FCDT.Code, FCDT.FunctionalArea  FROM FwRoleSecurity FRS 
                                        INNER JOIN FwEmployeeRole FER ON FRS.Role = FER.Role AND FER.Status = 'ACTV'
                                        INNER JOIN FwCdTransaction FCDT ON (FRS.Trans = FCDT.Code OR FRS.Trans = '****') AND FCDT.Inactive != 1
                                        WHERE FER.Organization = @sitio AND EmployeeId = @usuario 
                                        UNION ALL
				                        SELECT 
                                        TS.Code, TS.FunctionalArea
				                        FROM FwCdTransaction TS
				                        WHERE TS.Inactive != 1 AND TS.HasRoleSecurity = 0";
                SqlCommand comando = new SqlCommand(comandoString, conexion);
                comando.Parameters.AddWithValue("@usuario", usuario);
                comando.Parameters.AddWithValue("@sitio", sitio);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable tablaDatos = new DataTable();
                adaptador.Fill(tablaDatos);
                conexion.Close();
                return tablaDatos;
            }
            catch (Exception e)
            {
                DataTable dt = new DataTable();
                return dt;
            }
        }

        public DataTable ObtenerEnterodbs(String nombreUsuario)
        {
            string sql = "DECLARE @Z int DECLARE @D int DECLARE @Q int DECLARE @W datetime SET @W=(SELECT DATEADD(MONTH,6,PasswordDate) Fecha FROM dbo.FwEmployeePassword WHERE EmployeeId='" + nombreUsuario + "')  SET @D=(SELECT DATEDIFF(MM,GETDATE(),@W) Diferencia FROM dbo.FwEmployeePassword WHERE EmployeeId='" + nombreUsuario + "')  IF @D = 1 OR @D=0 OR @D<0 SET @Z = (SELECT DATEDIFF(dd,GETDATE(),@W) dias )  IF @Z<16 AND @Z>0 SELECT @Z Resultado ELSE IF @Z<=0 SELECT 0 Resultado ELSE SELECT 35 Resultado";
            SqlConnection con = new SqlConnection(conexionString);
            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();


            con.Open();
            daUser = new SqlDataAdapter(sql, conexionString);
            daUser.Fill(tableData);
            adap = new DataTableReader(tableData);
            con.Close();
            DataRow row = tableData.Rows[0];
            return tableData;
        }


        public DataTable obtenerSitios(String idioma, String sitio)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT
                CASE WHEN @idioma = 'es'
					THEN DescSpanish
					ELSE DescEnglish
				END Site,Code
				FROM FwCdOrganization
                WHERE Code != '*' AND Code != 'S' AND  Code != @sitio
                ORDER BY CASE WHEN @idioma = 'es'
				    THEN DescSpanish 
				    ELSE DescEnglish 
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

            //DataTable funciones = new DataTable();
            //funciones.Columns.Add("Site", typeof(string));
            //funciones.Columns.Add("Code", typeof(string));
            //funciones.Rows.Add("Antigua", "F");
            //funciones.Rows.Add("Nueva Esperanza", "N");
            //funciones.Rows.Add("San Rafael", "R");
            //funciones.Rows.Add("San Miguel", "M");
            //return funciones;
        }
        public int verificarContraseña(String usuario, String contraseña)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT dbo.fn_GEN_VerifyUserPassword(@usuario, @psw) R";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@psw", contraseña);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            int result = (int)tablaDatos.Rows[0]["R"];
            return result;
            //return 1;
        }
        public int fechapass(String usuario, String contraseña)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT dbo.fn_GEN_VerifyUserPassword(@usuario, @psw) R";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@psw", contraseña);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            int result = (int)tablaDatos.Rows[0]["R"];
            return result;
        }
        public DataTable verificarUltimas4Psw(String user, String pass)
        {
            string sql = "DECLARE @SI INT DECLARE @PAS varchar(30) SET @PAS = (SELECT FwEmployeePassword.Password FROM FwEmployeePassword WHERE EmployeeId= '" + user + "') DECLARE @PAS1 varchar(30) SET @PAS1 = (SELECT FwEmployeePassword.Pass1 FROM FwEmployeePassword WHERE EmployeeId='" + user + "') DECLARE @PAS2 varchar(30) SET @PAS2 = (SELECT FwEmployeePassword.Pass2 FROM FwEmployeePassword WHERE EmployeeId='" + user + "') DECLARE @PAS3 varchar(30) SET @PAS3 = (SELECT FwEmployeePassword.Pass3 FROM FwEmployeePassword WHERE EmployeeId='" + user + "') if '" + pass + "' = @PAS OR '" + pass + "' = @PAS1 OR '" + pass + "' = @PAS2 OR '" + pass + "' = @PAS3 BEGIN SET @SI=0 END ELSE BEGIN SET @SI=1 END SELECT @SI";
            SqlConnection con = new SqlConnection(conexionString);
            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();
            con.Open();
            daUser = new SqlDataAdapter(sql, conexionString);
            daUser.Fill(tableData);
            adap = new DataTableReader(tableData);
            con.Close();
            DataRow row = tableData.Rows[0];
            return tableData;
        }
        public int verificarNombrePsw(String nueva, String usuario)
        {
            BDUsuario objBD = new BDUsuario();
            BDAPAD APD = new BDAPAD();
            string sql = "SELECT CompleteName FROM dbo.FwEmployee WHERE EmployeeId= '" + usuario + "'";
            string nombre2 = APD.obtienePalabra(sql, "CompleteName");
            string[] nombre3 = nombre2.Split(' ');
            int conteo3 = 0;
            foreach (string numero in nombre3)
            {

                string sql3 = "SELECT COUNT(*) conteo WHERE '" + nueva + "' like('%" + numero + "%')";
                string numero5 = APD.obtienePalabra(sql3, "conteo");
                int conteo6 = Convert.ToInt32(numero5);
                conteo3 = conteo3 + conteo6;

                string remplazo = numero.Replace("á", "a");
                remplazo = remplazo.Replace("é", "e");
                remplazo = remplazo.Replace("í", "i");
                remplazo = remplazo.Replace("ó", "o");
                remplazo = remplazo.Replace("ú", "u");
                remplazo = remplazo.Replace("Á", "A");
                remplazo = remplazo.Replace("É", "E");
                remplazo = remplazo.Replace("Í", "I");
                remplazo = remplazo.Replace("Ó", "O");
                remplazo = remplazo.Replace("Ú", "U");
                string sql2 = "SELECT COUNT(*) conteo WHERE '" + nueva + "' like('%" + remplazo + "%')";
                string numero4 = APD.obtienePalabra(sql2, "conteo");
                int conteo2 = Convert.ToInt32(numero4);
                conteo3 = conteo3 + conteo2;

            }
            return conteo3;
        }
        public string verificarCaracteresPsw(String nueva, String usuario)
        {
            string[] mayus = { "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q", "R", "S", "T", "U", "V", "W", "X", "Y", "Z", "Ñ" };
            string[] minus = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "ñ", "o", "p", "q", "r", "s", "t", "u", "v", "w", "x", "y", "z" };
            string[] num = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };
            string sql = "SELECT CompleteName FROM dbo.FwEmployee WHERE EmployeeId= '" + usuario + "'";
            //string nombre = APD.obtienePalabra(sql, "CompleteName");
            string res;
            if (mayus.Any(nueva.Contains) && minus.Any(nueva.Contains) && num.Any(nueva.Contains) && nueva.Length > 7)
            {
                res = "ok";
            }
            else
            {
                res = "no";
                if ((mayus.Any(nueva.Contains)))
                {

                }
                else
                {
                    res = res + "M";
                }
                if (minus.Any(nueva.Contains))
                {

                }
                else
                {
                    res = res + "m";
                }
                if (num.Any(nueva.Contains))
                {

                }
                else
                {
                    res = res + "1";
                }
                if (nueva.Length > 7)
                {

                }
                else
                {
                    res = res + "8";
                }

            }
            return res;
        }
        public void ejecutarSQL(String sql)
        {
            SqlConnection cn = null;
            SqlCommand cmd = null;
            cn = new SqlConnection(conexionString);
            cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cn.Close();
            }
        }
        public void ejecutarSQL2(String sql)
        {
            SqlConnection cn = null;
            SqlCommand cmd = null;
            cn = new SqlConnection(conexionString);
            cmd = new SqlCommand(sql, cn);
            try
            {
                cn.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                cn.Close();
            }
        }
    }
}