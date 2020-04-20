using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Web.UI.WebControls;

namespace Familias3._1.bd
{
    public class BDAPAD
    {
        static String conexionString;
        static String conexi;
        static String conexin;
        public BDAPAD()
        {
            conexionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conexi = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            conexin = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        SqlConnection cn = new SqlConnection(conexionString);
        SqlConnection nc = new SqlConnection(conexi);
        SqlConnection sq = new SqlConnection(conexin);

        public int ObtenerEntero(String SQL, String title)
        {


            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();
            int temp;

            try
            {
                sq.Open();
                daUser = new SqlDataAdapter(SQL, conexin);
                daUser.Fill(tableData);
                adap = new DataTableReader(tableData);
                sq.Close();
                DataRow row = tableData.Rows[0];
                temp = Convert.ToInt32(row[title]);
                return temp;
            }
            catch (Exception ex)
            {
                
                temp = 5;
                return temp;
            }
        }
        public DataTable ObtenerEnterodbs(String nombreUsuario)
        {
            string sql = "DECLARE @Z int DECLARE @D int DECLARE @Q int DECLARE @W datetime SET @W=(SELECT DATEADD(MONTH,6,PasswordDate) Fecha FROM dbo.FwEmployeePassword WHERE EmployeeId='" + nombreUsuario + "')  SET @D=(SELECT DATEDIFF(MM,GETDATE(),@W) Diferencia FROM dbo.FwEmployeePassword WHERE EmployeeId='" + nombreUsuario + "')  IF @D = 1 OR @D=0 OR @D<0 SET @Z = (SELECT DATEDIFF(dd,GETDATE(),@W) dias )  IF @Z<16 AND @Z>0 SELECT @Z Resultado ELSE IF @Z<=0 SELECT 0 Resultado ELSE SELECT 35 Resultado";
            SqlConnection con = new SqlConnection(conexi);
            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();


            con.Open();
            daUser = new SqlDataAdapter(sql, conexi);
            daUser.Fill(tableData);
            adap = new DataTableReader(tableData);
            con.Close();
            DataRow row = tableData.Rows[0];
            return tableData;
        }



        public DataTable InfoGen(String proje, String memid, String lang)//carga la informacion general del apadrinado
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            DataTable dt = new DataTable();

            conexion.Open();
            string sql = @"IF @L='es'
SELECT FirstNames + ' ' + LastNames 'Nombre', cdAS.DescSpanish 'Estado', 
dbo.fn_GEN_FormatDate(M.BirthDate,'es') + ' - ' + dbo.fn_GEN_edad(M.BirthDate, 'es') 'Edad'
, dbo.fn_BECA_InfoEducUltimo(@S, @M) 'Educ', cdSR.DescSpanish  'Restriccion'
FROM dbo.Member M
LEFT JOIN dbo.CdAffiliationStatus cdAS ON M.AffiliationStatus = cdAS.Code
LEFT JOIN dbo.MiscMemberSponsorInfo MMSI ON MMSI.RecordStatus = M.RecordStatus AND MMSI.Project = M.Project AND MMSI.MemberId = M.Memberid
LEFT JOIN dbo.CdSponsorshipRestriction cdSR ON cdSR.Code = MMSI.Restriction
WHERE M.RecordStatus = ' ' AND M.Project = @S AND M.MemberId = @M 
ELSE
SELECT FirstNames + ' ' + LastNames 'Nombre', cdAS.DescEnglish 'Estado', 
dbo.fn_GEN_FormatDate(M.BirthDate,'en') + ' - ' + dbo.fn_GEN_edad(M.BirthDate, 'en') 'Edad'
, dbo.fn_BECA_InfoEducUltimoIn(@S, @M) 'Educ', cdSR.DescEnglish  'Restriccion'
FROM dbo.Member M
LEFT JOIN dbo.CdAffiliationStatus cdAS ON M.AffiliationStatus = cdAS.Code
LEFT JOIN dbo.MiscMemberSponsorInfo MMSI ON MMSI.RecordStatus = M.RecordStatus AND MMSI.Project = M.Project AND MMSI.MemberId = M.Memberid
LEFT JOIN dbo.CdSponsorshipRestriction cdSR ON cdSR.Code = MMSI.Restriction
WHERE M.RecordStatus = ' ' AND M.Project = @S AND M.MemberId = @M
";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@L", lang);
            comando.Parameters.AddWithValue("@S", proje);
            comando.Parameters.AddWithValue("@M", memid);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;

            adaptador.Fill(dt);
            conexion.Close();
            return dt;
        }
        public DataTable Carta(String S, String M, String L, String CS)//carga la informacion general del apadrinado
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            DataTable dt = new DataTable();

            conexion.Open();
            string sql = @"SELECT M.Category,CONVERT (char(10), M.DateTimeWritten, 103) DateTimeWritten,M.SponsorId,S.SponsorNames,M.DateSent,M.Notes FROM dbo.MemberSponsorLetter M INNER JOIN  dbo.Sponsor S ON S.SponsorId= M.SponsorId WHERE M.RecordStatus=' ' AND m.MemberId=@M  AND dbo.fn_GEN_FormatDate(M.DateTimeWritten,@L)=@CS AND SponsorNames=@S ORDER BY M.DateTimeWritten DESC";

            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@L", L);
            comando.Parameters.AddWithValue("@S", S);
            comando.Parameters.AddWithValue("@M", M);
            comando.Parameters.AddWithValue("@CS", CS);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;

            adaptador.Fill(dt);
            conexion.Close();
            return dt;
        }

        public DataTable obtenerDatos(String sitio, String idMiembro, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT FirstNames, LastNames, PreferredName, Gender, BirthDate, FamilyId, Grado, Semaphore, LiveDead, Literacy, Id, Telefono, Fase, AffiliationType, AffiliationStatus, YEAR(AffiliationStatusDate) Date, Edad 
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
        public DataTable restriccion(String sitio, String idMiembro, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT FirstNames, LastNames, PreferredName, Gender, BirthDate, FamilyId, Grado, Semaphore, LiveDead, Literacy, Id, Telefono, Fase, AffiliationType, AffiliationStatus, YEAR(AffiliationStatusDate) Date, Edad 
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

        public String MemPhoto(String proje, String memid)//carga la foto del apadrinado
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            try
            {

                conexion.Open();
                string sql = "select dbo.fn_SPON_LastMemberPhoto ('" + proje + "','" + memid + "')";
                SqlCommand showresult = new SqlCommand(sql, conexion);
                string photourl = Convert.ToString(showresult.ExecuteScalar());
                string[] newurl = photourl.Split(':');
                String URL = "~/Image.ashx?imageID=" + newurl[1];
                //imag.Attributes["src"] = @"\\SVRFAMILIAS\FamilyFotos" + newurl[1];
                return URL;
            }
            finally
            {
                conexion.Close();
            }

        }
        public DataTable InfoEducacion(String project, String MemberId, String Language)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            string sql = @"IF  @L = 'es'
            SELECT M.Project,M.MemberId,G.DescSpanish,C.DescSpanish, C.RequiredYears,M.SchoolYear, S.Name, E.DescSpanish FROM Dbo.MemberEducationYear M left join Dbo.CdGrade G ON M.Grade = G.Code left join Dbo.CdEducationCareer C on M.Career = C.Code left join Dbo.School S ON M.SchoolCode = S.Code left join Dbo.CdEducationStatus E ON M.Status=E.Code WHERE M.RecordStatus=' ' AND S.RecordStatus=' ' and SchoolYear=Year(GETDATE()) and M.MemberId=@M AND M.Project=@S OR M.RecordStatus=' ' AND S.RecordStatus=' ' AND M.Status='GRAD' AND M.MemberId=@M AND M.Project=@S OR M.RecordStatus=' ' AND S.RecordStatus=' ' AND M.MemberId=@M AND M.ReasonNotToContinue NOT LIKE''  ORDER BY M.MemberId ASC
            ELSE
            SELECT M.Project,M.MemberId,G.DescSpanish,C.DescSpanish, C.RequiredYears,M.SchoolYear, S.Name, E.DescSpanish FROM Dbo.MemberEducationYear M left join Dbo.CdGrade G ON M.Grade = G.Code left join Dbo.CdEducationCareer C on M.Career = C.Code left join Dbo.School S ON M.SchoolCode = S.Code left join Dbo.CdEducationStatus E ON M.Status=E.Code WHERE M.RecordStatus=' ' AND S.RecordStatus=' ' and SchoolYear=Year(GETDATE()) and M.MemberId=@M AND M.Project=@S OR M.RecordStatus=' ' AND S.RecordStatus=' ' AND M.Status='GRAD' AND M.MemberId=@M AND M.Project=@S OR M.RecordStatus=' ' AND S.RecordStatus=' ' AND M.MemberId=@M AND M.ReasonNotToContinue NOT LIKE''  ORDER BY M.MemberId ASC";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@L", Language);
            comando.Parameters.AddWithValue("@S", project);
            comando.Parameters.AddWithValue("@M", MemberId);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable dr = new DataTable();
            adaptador.Fill(dr);
            conexion.Close();
            return dr;
        }
        public DataTable NoTelefono(String project, String family, String Language)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(conexionString))
                {
                    cn.Open();
                    string sql = @"SELECT CASE WHEN dbo.fn_GEN_telefonos(@S, @F) IS NULL THEN CASE WHEN @L='es' THEN 'No Tiene números de telefono' ELSE 'Does not have a phone number' END ELSE dbo.fn_GEN_telefonos(@S, @F) END Telefonos";
                    SqlCommand comando = new SqlCommand(sql, cn);
                    comando.Parameters.AddWithValue("@L", Language);
                    comando.Parameters.AddWithValue("@S", project);
                    comando.Parameters.AddWithValue("@F", family);

                    SqlDataAdapter adaptador = new SqlDataAdapter();
                    adaptador.SelectCommand = comando;
                    DataTable dr = new DataTable();
                    adaptador.Fill(dr);

                    return dr;
                    cn.Close();
                }

            }
            catch (Exception ex)
            {
                throw new Exception("El error esta en BDAPAD/NoTelefono", ex);
            }

        }
        public DataTable VerPadrinos(string proje, string memid, string lang)//muestra el listado de padrinos
        {
            try
            {
                SqlConnection conexion = new SqlConnection(conexionString);

                conexion.Open();

                var sql = @"IF @L='es'
SELECT SMR.SponsorId, S.SponsorNames Name, dbo.fn_GEN_FormatDate(SMR.StartDate, @L) as StartDate, dbo.fn_GEN_FormatDate(SMR.EndDate, @L) as EndDate,
(CASE WHEN @L = 'es' THEN cT.DescSpanish ELSE cT.DescEnglish END ) AS 'Type Sponsor',
(CASE WHEN @L = 'es' THEN cG.DescSpanish ELSE cG.DescEnglish END ) AS Gender, (case when  S.SpeaksSpanish = 1 and @L = 'es' then 'Si' else (case when S.SpeaksSpanish =0 and @L = 'es' then 'No' else (case when S.SpeaksSpanish =1 and @L = 'en' then 'Yes' else(case when S.SpeaksSpanish =0 and @L = 'en' then 'Not' end ) end )  end ) end ) as 'Speak Spanish'
  FROM dbo.SponsorMemberRelation SMR INNER JOIN
       dbo.Sponsor S ON SMR.SponsorId = S.SponsorId AND SMR.RecordStatus = S.RecordStatus INNER JOIN
       dbo.CdSponsorMemberRelationType cT ON SMR.Type = cT.Code INNER JOIN
       dbo.CdGender cG ON S.Gender = cG.Code
  WHERE  SMR.RecordStatus = ' ' AND SMR.Project = @S AND SMR.MemberId = @M
ELSE 
SELECT SMR.SponsorId , S.SponsorNames Name, dbo.fn_GEN_FormatDate(SMR.StartDate, @L) as StartDate, dbo.fn_GEN_FormatDate(SMR.EndDate, @L) as EndDate,
(CASE WHEN @L = 'es' THEN cT.DescSpanish ELSE cT.DescEnglish END ) AS 'Type Sponsor',
(CASE WHEN @L = 'es' THEN cG.DescSpanish ELSE cG.DescEnglish END ) AS Gender, (case when  S.SpeaksSpanish = 1 and @L = 'es' then 'Si' else (case when S.SpeaksSpanish =0 and @L = 'es' then 'No' else (case when S.SpeaksSpanish =1 and @L = 'en' then 'Yes' else(case when S.SpeaksSpanish =0 and @L = 'en' then 'Not' end ) end )  end ) end ) as 'Speak Spanish'
  FROM dbo.SponsorMemberRelation SMR INNER JOIN
       dbo.Sponsor S ON SMR.SponsorId = S.SponsorId AND SMR.RecordStatus = S.RecordStatus INNER JOIN
       dbo.CdSponsorMemberRelationType cT ON SMR.Type = cT.Code INNER JOIN
       dbo.CdGender cG ON S.Gender = cG.Code
  WHERE  SMR.RecordStatus = ' ' AND SMR.Project = @S AND SMR.MemberId = @M";
                SqlCommand comando = new SqlCommand(sql, conexion);
                comando.Parameters.AddWithValue("@L", lang);
                comando.Parameters.AddWithValue("@S", proje);
                comando.Parameters.AddWithValue("@M", memid);
                SqlDataAdapter adaptador = new SqlDataAdapter();
                adaptador.SelectCommand = comando;
                DataTable dr = new DataTable();
                adaptador.Fill(dr);
                conexion.Close();

                return dr;
            }
            catch(Exception ex){
            DataTable dr=new DataTable();
                return dr;
            }
        }
        public DataTable CartasRegistro(String M, String L, String S)
        {
            SqlConnection conexin = new SqlConnection(conexionString);
            conexin.Open();

            var sql = @"IF @L='es'
SELECT L.DescSpanish 'Categoria', dbo.fn_GEN_FormatDate(R.DateTimeWritten,'es') 'Escrita',S.SponsorNames 'Padrinos',dbo.fn_GEN_FormatDate(R.DateSent,'es') 'Envio',R.Notes 'Notas'  
FROM Dbo.MemberSponsorLetter R
INNER JOIN dbo.Sponsor S on R.SponsorId = S.SponsorId AND R.RecordStatus = S.RecordStatus
INNER JOIN Dbo.CdLetterCategory L ON R.Category = L.Code
WHERE dbo.fn_GEN_mesesEntreFechas(R.DateTimeWritten) <= 12 AND R.RecordStatus=' ' AND R.MemberId=@M AND R.Project=@S 
ORDER BY R.DateTimeWritten DESC
ELSE
SELECT L.DescEnglish 'Category', dbo.fn_GEN_FormatDate(R.DateTimeWritten,'en') 'Written',S.SponsorNames 'Sponsors',dbo.fn_GEN_FormatDate(R.DateSent,'en') 'Sent',R.Notes 'Notes'  
FROM Dbo.MemberSponsorLetter R
INNER JOIN dbo.Sponsor S on R.SponsorId = S.SponsorId AND R.RecordStatus = S.RecordStatus
INNER JOIN Dbo.CdLetterCategory L ON R.Category = L.Code
WHERE dbo.fn_GEN_mesesEntreFechas(R.DateTimeWritten) <= 12 AND R.RecordStatus=' ' AND R.MemberId=@M AND R.Project=@S 
ORDER BY R.DateTimeWritten DESC
";
            SqlCommand comando = new SqlCommand(sql, conexin);
            comando.Parameters.AddWithValue("@L", L);
            comando.Parameters.AddWithValue("@S", S);
            comando.Parameters.AddWithValue("@M", M);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable dr = new DataTable();
            adaptador.Fill(dr);
            conexin.Close();

            return dr;
        }


        public DataTable RegaloRegistro(String M, String L, String S)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();

            var sql = @"IF @L='es'
SELECT dbo.fn_GEN_FormatDate(G.SelectionDateTime,'es') 'Selección', CGC.DescSpanish 'Categoria',  
 CASE WHEN G.Type is null THEN ' ' ELSE CGT.DescSpanish END 'Tipo', G.Notes 'Notas', 
 dbo.fn_GEN_FormatDate(G.DeliveryDateTime, 'es')  'Entrega' 
FROM MemberGift G 
 INNER JOIN CdGiftCategory CGC ON G.Category = CGC.Code and G.Project=CGC.Project 
 LEFT JOIN CdGiftType CGT ON G.Type = CGT.Code and G.Project=CGT.Project
WHERE dbo.fn_GEN_mesesEntreFechas(G.SelectionDateTime)<=12 AND G.RecordStatus like ' ' and G.Project like @S and MemberId = @M 
ORDER BY SelectionDateTime desc
ELSE
SELECT dbo.fn_GEN_FormatDate(G.SelectionDateTime,'es') 'Selección', CGC.DescSpanish 'Categoria',  
 CASE WHEN G.Type is null THEN ' ' ELSE CGT.DescSpanish END 'Tipo', G.Notes 'Notas', 
 dbo.fn_GEN_FormatDate(G.DeliveryDateTime, 'es')  'Entrega' 
FROM MemberGift G 
 INNER JOIN CdGiftCategory CGC ON G.Category = CGC.Code and G.Project=CGC.Project 
 LEFT JOIN CdGiftType CGT ON G.Type = CGT.Code and G.Project=CGT.Project
WHERE dbo.fn_GEN_mesesEntreFechas(G.SelectionDateTime)<=12 AND G.RecordStatus like ' ' and G.Project like @S and MemberId = @M 
ORDER BY SelectionDateTime desc
  ";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@L", L);
            comando.Parameters.AddWithValue("@S", S);
            comando.Parameters.AddWithValue("@M", M);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable dr = new DataTable();
            adaptador.Fill(dr);
            conexion.Close();

            return dr;
        }




        public DataTable LlenarDataTable(String SQL, DataTable tabledata)
        {

            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(SQL, conexion);
            DataSet setDatos = new DataSet();
            adaptador.Fill(setDatos, "listado");
            tabledata = setDatos.Tables["listado"];
            conexion.Close();
            return tabledata;
        }//fin clase
        public DataTable Vericontra(String user, String pass)
        {
            string sql = "DECLARE @SI INT DECLARE @PAS varchar(30) SET @PAS = (SELECT FwEmployeePassword.Password FROM FwEmployeePassword WHERE EmployeeId= '" + user + "') DECLARE @PAS1 varchar(30) SET @PAS1 = (SELECT FwEmployeePassword.Pass1 FROM FwEmployeePassword WHERE EmployeeId='" + user + "') DECLARE @PAS2 varchar(30) SET @PAS2 = (SELECT FwEmployeePassword.Pass2 FROM FwEmployeePassword WHERE EmployeeId='" + user + "') DECLARE @PAS3 varchar(30) SET @PAS3 = (SELECT FwEmployeePassword.Pass3 FROM FwEmployeePassword WHERE EmployeeId='" + user + "') if '" + pass + "' = @PAS OR '" + pass + "' = @PAS1 OR '" + pass + "' = @PAS2 OR '" + pass + "' = @PAS3 BEGIN SET @SI=0 END ELSE BEGIN SET @SI=1 END SELECT @SI";
            SqlConnection con = new SqlConnection(conexi);
            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();


            con.Open();
            daUser = new SqlDataAdapter(sql, conexi);
            daUser.Fill(tableData);
            adap = new DataTableReader(tableData);
            con.Close();
            DataRow row = tableData.Rows[0];
            return tableData;


        }

        public void ejecutarSQL(String sql)
        {
            SqlConnection cn = null;
            SqlCommand cmd = null;
            cn = new SqlConnection(conexi);
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
        }//fin clase
        public DataTable MostrarCartas(String S, String M)
        {


            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            var sql = @"SELECT R.SponsorId , Sponsor.SponsorNames FROM Dbo.MemberSponsorLetter R 
                        inner join dbo.Sponsor on R.SponsorId = Sponsor.SponsorId 
                        WHERE R.RecordStatus='' AND YEAR(R.CreationDateTime)=YEAR(GETDATE()) AND R.MemberId=@M AND R.Project=@S AND R.Category='PRIM' AND Sponsor.RecordStatus=''";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@S", S);
            comando.Parameters.AddWithValue("@M", M);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable dr = new DataTable();
            adaptador.Fill(dr);
            conexion.Close();
            return dr;
        }//fin clase
        public string obtienePalabra(String sql, String titulo)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            SqlDataAdapter daUser = new SqlDataAdapter();
            DataTableReader adap;
            DataTable tableData = new DataTable();
            string temp = "";


            conexion.Open();
            daUser = new SqlDataAdapter(sql, conexionString);
            daUser.Fill(tableData);
            adap = new DataTableReader(tableData);
            cn.Close();
            temp = Convert.ToString(tableData.Rows[0][titulo]);

            return temp;
        }//fin funcion
        public DataTable retomarfoto(string S, string M)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            var sql = @"SELECT CASE WHEN RetakePhotoDate IS NULL THEN '' ELSE RetakePhotoDate END Retomar, CASE WHEN RetakePhotoUserId  IS NULL THEN ' ' ELSE RetakePhotoUserId  END Usuario 
                         FROM dbo.MiscMemberSponsorInfo 
                         WHERE RecordStatus=' ' AND Project=@S AND MemberId= @M";
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@S", S);
            comando.Parameters.AddWithValue("@M", M);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable dr = new DataTable();
            adaptador.Fill(dr);
            conexion.Close();
            return dr;

        }



    }


}
