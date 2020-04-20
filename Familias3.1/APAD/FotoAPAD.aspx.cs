using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.Apadrinamiento
{
    public partial class FotoAPAD : System.Web.UI.Page
    {
        protected BDMiembro BDM;
        protected Diccionario dic;
        protected static BDAPAD APD;
        protected static BDFamilia BDF;
        protected static String S;
        protected static String L;
        protected static String F;
        protected static mast mst;
        protected static String M;
        protected static String U;
        protected static String fotoanterior;
        protected static String fotoactual;
        protected static String Site;
        protected static String Member;
        protected String prueba;
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string ConnectionString2 = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con2 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string locacion = @"G:\";
        protected static string locacion2;// = @"\\SISTEMAS02\Carpeta Compartida\FotosAPAD";
        protected static int desicion2;
        protected static String rutaCarpetaFuente;// = @"\\SVRAPP\FamilyFotos\Apadrinados\";
        protected static String rutaCarpetaCopia;// = @"\\SVRAPP\FamilyFotos\HistorialFotos\";
        string carpeta;


        //////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--//////////////////////////////////////////
        private void Estadisticas()
        {

            string sql = "SELECT COUNT(*) conteo FROM Member WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = '" + Site + "'";
            int sql1 = ObtenerEntero(sql, "conteo");
            lblVtotal.Text = Convert.ToString(sql1);

            sql = "SELECT COUNT(*) conteo FROM Member M INNER JOIN MiscMemberSponsorInfo MMSI ON M.RecordStatus = MMSI.RecordStatus AND M.Project = MMSI.Project AND M.MemberId = MMSI.MemberId AND M.RecordStatus = ' ' AND M.AffiliationStatus = 'AFIL' WHERE M.Project = '" + Site + "' AND YEAR(MMSI.PhotoDate) = YEAR(GETDATE()) AND MMSI.RetakePhotoDate IS NULL";
            int sql4 = ObtenerEntero(sql, "conteo");
            lblVfoto.Text = Convert.ToString(sql4);

            sql = "SELECT COUNT(*) conteo FROM Member M INNER JOIN MiscMemberSponsorInfo MMSI ON M.RecordStatus = MMSI.RecordStatus AND M.Project = MMSI.Project AND M.MemberId = MMSI.MemberId AND M.RecordStatus = ' ' AND M.AffiliationStatus = 'AFIL' WHERE M.Project = '" + Site + "' AND YEAR(MMSI.PhotoDate) != YEAR(GETDATE())";
            int sql2 = ObtenerEntero(sql, "conteo");
            lblVfotono.Text = Convert.ToString(sql2);

            sql = "SELECT COUNT(*) conteo FROM Member M INNER JOIN MiscMemberSponsorInfo MMSI ON M.RecordStatus = MMSI.RecordStatus AND M.Project = MMSI.Project AND M.MemberId = MMSI.MemberId AND M.RecordStatus = ' ' AND M.AffiliationStatus = 'AFIL'WHERE M.Project = '" + Site + "' AND YEAR(MMSI.PhotoDate) = YEAR(GETDATE()) AND MMSI.RetakePhotoDate IS NOT NULL";
            int sql3 = ObtenerEntero(sql, "conteo");
            lblVretomar.Text = Convert.ToString(sql3);

            lblcontretomar.Text = dic.RetomarFotoAPAD;
            lblcontfotono.Text = dic.MsjNoContieneFotoAPAD;
            lblcontfoto.Text = dic.ContieneFotoAPAD;
            lblconttotal.Text = dic.total;
        }
        public void Foto(int desicion)
        {
            try
            {
                con.Open();
                String sql = "SELECT CASE WHEN dbo.fn_SPON_LastMemberPhoto ('" + Site + "','" + Member + "') IS NULL THEN ' ' ELSE dbo.fn_SPON_LastMemberPhoto ('" + Site + "','" + Member + "') END resultado";
                string photourl = obtienePalabra(sql, "resultado");
                String[] newurl = photourl.Split(':');

                string newsubstring = photourl.Replace(@"G:\", "");
                newsubstring = newsubstring.Replace(@"g:\", "");
                newsubstring = newsubstring.Replace(@"V:\", "");
                Boolean foto;
                string ruta4 = newurl[1];
                foto = verificarRuta(locacion2, ruta4);
                if (foto == false)
                {
                    lblimgact.Text = dic.MsjNoContieneFotoAPAD;
                    Image1.Attributes["src"] = @"../Images/CommonHope_Heart_RGB.png";

                }
                else
                {
                    if (desicion == 1)
                    {
                        //desa
                        Image1.Attributes["src"] = "../ImagenG.ashx?imageID=" + newurl[1].ToString();
                        fotoanterior = newurl[1];
                        fotoactual = newsubstring;
                    }
                    else
                    {
                        Image1.Attributes["src"] = "../Imagen.ashx?imageID=" + newurl[1].ToString();
                        fotoanterior = newurl[1];
                        fotoactual = newsubstring;
                    }

                }

            }
            catch (Exception e)
            {
                mst.mostrarMsjAdvNtf("Error: " + e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void imagen()
        {
            try
            {
                string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);

                DateTime fehchaA = DateTime.Now;
                string cadena = fehchaA.ToString("yyyyMMddHHmmss");
                string nuevoarchivo = S + M + "_" + cadena + ".jpg";
                string archivo = Path.GetFileName(FileUpload1.PostedFile.FileName);
                string carpeta_final = Path.Combine(locacion2, nuevoarchivo);

                FileUpload1.PostedFile.SaveAs(carpeta_final);
                mst.mostrarMsjNtf(dic.CambiodeImagenAPAD);
                SubirImagenBD(nuevoarchivo);
                Foto(desicion2);

            }
            catch (Exception ex)
            {
                string error = "Error: " + ex.Message;
                mst.mostrarMsjAdvNtf(error);
            }
        }
        private void llenarcombo()
        {
            string SQL = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.FwCdOrganization WHERE Code NOT LIKE'A' AND Code NOT LIKE'E' AND Code NOT LIKE'S' ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END ASC";
            SqlDataAdapter adapter2 = new SqlDataAdapter(SQL, con);
            DataTable datos2 = new DataTable();
            adapter2.Fill(datos2);
            ddlsitio.DataSource = datos2;
            ddlsitio.DataValueField = "Code";
            ddlsitio.DataTextField = "descripcion";
            ddlsitio.DataBind();
            ddlsitio.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlsitio.SelectedValue = Site;
        }
        public int ObtenerEntero(String SQL, String title)
        {
            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();
            int temp;

            try
            {
                con.Open();
                daUser = new SqlDataAdapter(SQL, ConnectionString);
                daUser.Fill(tableData);
                adap = new DataTableReader(tableData);
                con.Close();
                DataRow row = tableData.Rows[0];
                temp = Convert.ToInt32(row[title]);
                return temp;
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf("Error: " + ex.Message);
                temp = -1;
                return temp;
            }
        }
        public string obtienePalabra(string stringSQL, string title)
        {
            SqlConnection cn = new SqlConnection(ConnectionString2);
            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();
            string temp = "";

            try
            {
                cn.Open();
                daUser = new SqlDataAdapter(stringSQL, ConnectionString2);
                daUser.Fill(tableData);
                adap = new DataTableReader(tableData);
                cn.Close();
                temp = tableData.Rows[0][title].ToString();

            }
            catch (Exception ex)
            {
                temp = "";

            }
            finally
            {
                if (cn.State == ConnectionState.Open)
                {
                    cn.Close();
                }
            }

            return temp;
        }
        private void SubirImagenBD(string nombre)
        {
            string i = locacion + nombre;
            string sql = "INSERT INTO dbo.MiscMemberSponsorInfo SELECT Project, MemberId, GETDATE() CreationDateTime, RecordStatus, '" + U + "' UserId, ExpirationDateTime, '" + i + "' Photo,GETDATE() PhotoDate,NULL  RetakePhotoDate,NULL RetakePhotoUserId, LastCarnetPrintDate, SponsorshipLevel, SponsorshipType, Restriction, RestrictionDate, ExceptionPhotoDate FROM dbo.MiscMemberSponsorInfo WHERE RecordStatus = ' '  AND Project = '" + Site + "' AND MemberId = " + Member + " ";
            SqlCommand cmd = new SqlCommand(sql, con2);
            try
            {
                con2.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                lblimgnew.Text = "Error: " + ex.Message;
            }
            finally
            {
                con2.Close();
            }
        }
        public void traducir()
        {

            string palabrasubir;
            if (L == "es")
            {
                palabrasubir = "Subir";
            }
            else
            {
                palabrasubir = "Upload";
            }
            ingreso.Visible = true;
            lblimgact.Text = dic.ImagenActualAPAD;
            lblimgnew.Text = dic.ImagenNuevaAPAD;
            lbltitulo.Text = dic.FotoAPAD + " (" + Site + Member + ")";
            btnsubir.Text = palabrasubir;
            btncanc.Text = dic.CancelarAPAD;
            btnbuscar.Text = dic.buscar;
            lblsitio.Text = dic.sitio;
            lblmiembro.Text = dic.miembro;

        }
        public String transferirArchivo(String nombreArchivoFuente, String nombreArchivoCopia)
        {


            DirectoryInfo infoCarpetaFuente = new DirectoryInfo(rutaCarpetaFuente);
            DirectoryInfo infoCarpetaDestino = new DirectoryInfo(rutaCarpetaCopia);
            nombreArchivoFuente = nombreArchivoFuente.Replace(@"G:\", "");
            nombreArchivoFuente = nombreArchivoFuente.Replace(@"g:\", "");
            FileInfo[] archivosACopiar = infoCarpetaFuente.GetFiles(nombreArchivoFuente.Replace(@"V:\", ""));

            FileInfo[] archivosExistentesCarpetaDestino;
            Boolean yaExiste = false;
            if (!Directory.Exists(rutaCarpetaCopia))
            {
                infoCarpetaDestino.Create();
            }
            foreach (FileInfo archivoACopiar in archivosACopiar)
            {
                nombreArchivoCopia = nombreArchivoCopia.Replace(@"V:\", "");
                nombreArchivoCopia = nombreArchivoCopia.Replace(@"g:\", "");
                archivosExistentesCarpetaDestino = infoCarpetaDestino.GetFiles(nombreArchivoCopia.Replace(@"G:\", ""));
                foreach (FileInfo archivoExistenteCarpetaDestino in archivosExistentesCarpetaDestino)
                {
                    if (archivoExistenteCarpetaDestino.Exists)
                    {
                        yaExiste = true;
                    }
                }
                if (archivoACopiar.Exists)
                {
                    if (!yaExiste)
                    {
                        nombreArchivoCopia = nombreArchivoCopia.Replace(@"V:\", "");
                        nombreArchivoCopia = nombreArchivoCopia.Replace(@"g:\", "");
                        archivoACopiar.MoveTo(rutaCarpetaCopia + nombreArchivoCopia.Replace(@"G:\", ""));
                    }
                }
            }
            return rutaCarpetaCopia;
        }
        public void valoresiniciales(string sitio, string miembro)
        {
            try
            {
                dic = new Diccionario(L, sitio);
                DataTable dt = APD.InfoGen(sitio, miembro, L);
                string A = dt.Rows[0][1].ToString();
                if (A == "Affiliated" || A == "Afiliado")
                {
                    locacion2 = @"\\SVRAPP\FamilyFotos\Apadrinados";
                    rutaCarpetaFuente = @"\\SVRAPP\FamilyFotos\Apadrinados\";
                    rutaCarpetaCopia = @"\\SVRAPP\FamilyFotos\HistorialFotos\";
                    fotoactual = "";
                    fotoanterior = "";
                    traducir();
                    desicion2 = 0;
                    Foto(desicion2);
                    Estadisticas();
                    Image2.Attributes["src"] = @"../Images/cambiouser.png";
                    llenarcombo();
                }
                else
                {
                    if (string.IsNullOrEmpty(A))
                    {

                    }
                    else
                    {
                        locacion2 = @"\\SVRAPP\FamilyFotos\ExApadrinados";
                        rutaCarpetaFuente = @"\\SVRAPP\FamilyFotos\ExApadrinados\";
                        rutaCarpetaCopia = @"\\SVRAPP\FamilyFotos\HistorialFotos\";

                        fotoactual = "";
                        fotoanterior = "";
                        traducir();
                        desicion2 = 1;
                        Foto(desicion2);
                        Estadisticas();
                        Image2.Attributes["src"] = @"../Images/cambiouser.png";
                        llenarcombo();
                    }
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }

        }
        private void VerificarApadrinamiento(string Miem, string Sit)
        {
            string sql = "SELECT COUNT(*) conteo FROM dbo.Member WHERE Project='" + Sit + "' AND MemberId='" + Miem + "' AND dbo.fn_GEN_Npadrinos('" + Sit + "','" + Miem + "')>0 AND RecordStatus=' ' AND AffiliationStatus='AFIL'";
            int conteo = ObtenerEntero(sql, "conteo");
            if (conteo == 0)
            {
                mst.mostrarMsjAdvNtf(dic.MsjmiembronoApadrinado);
            }
            else
            {
                Member = Miem;
                Site = Sit;
                valoresiniciales(Site, Member);
            }
        }
        protected bool verificarRuta(string ruta, string archivo)
        {
            bool decision = System.IO.File.Exists(ruta + archivo);
            return decision;
        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {

            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;

            BDM = new BDMiembro();
            APD = new BDAPAD();
            dic = new Diccionario(L, S);

            //carpeta = locacion;
            mst = (mast)Master;
            if (!IsPostBack)
            {
                try
                {
                    if (string.IsNullOrEmpty(M))
                    {
                        tbfiltro.Visible = false;
                        ingreso.Visible = false;
                    }
                    else
                    {
                        Site = S;
                        Member = M;
                        valoresiniciales(Site, Member);
                    }

                }
                catch (Exception ex)
                {
                }
            }


        }

        protected void btnsubir_Click1(object sender, EventArgs e)
        {
            try
            {
                if (FileUpload1.HasFile == null)
                {
                    mst.mostrarMsjAdvNtf(dic.NohaSeleccionadoarchivoAPAD);
                }
                else
                {
                    string extension = Path.GetExtension(FileUpload1.PostedFile.FileName);
                    switch (extension.ToLower())
                    {

                        case ".jpg":
                            transferirArchivo(fotoactual, fotoactual);
                            imagen();
                            // Estadisticas();
                            valoresiniciales(Site, Member);
                            break;
                        case "":
                            mst.mostrarMsjAdvNtf(dic.NohaSeleccionadoarchivoAPAD);
                            break;


                        default:
                            mst.mostrarMsjAdvNtf(dic.ExtensionvalidaAPAD);

                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
            dic = new Diccionario(L, S);

        }
        protected void btncanc_Click(object sender, EventArgs e)
        {

        }
        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsitio.SelectedIndex == 0 || string.IsNullOrEmpty(txtmiembro.Text))
                {
                    mst.mostrarMsjAdvNtf(dic.msjDebeingresarUno);
                }
                else
                {
                    string miembro = txtmiembro.Text;
                    string sitio = ddlsitio.SelectedValue;
                    VerificarApadrinamiento(miembro, sitio);
                }
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }

        }
    }
}