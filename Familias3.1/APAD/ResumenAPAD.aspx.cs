using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Familias3._1.bd;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Drawing;
using System.Data.Objects;
namespace Familias3._1.Apadrinamiento
{
    public partial class Resumen : System.Web.UI.Page
    {
        protected BDMiembro BDM;
        protected Diccionario dic;
        protected static BDAPAD APD;
        protected static BDFamilia BDF;
        protected static String S;
        protected static String L;
        protected static String P;
        protected static String F;
        protected static mast mst;
        protected static String M;
        protected static String U;
        protected static int desicion;
        protected static string ruta;
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        string ConnectionString2 = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con2 = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);


        //////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--///////////////////////////////////////////
        private void agregaAvisos()
        {
            DataTable dtAvisos = new BDFamilia().obtenerAvisos(S, F, L, false);
            gdvAvisos.Columns[0].HeaderText = dic.avisos;
            if (dtAvisos.Rows.Count > 0)
            {
                //gdvAvisos.Columns[1].Visible = false;
                gdvAvisos.DataSource = dtAvisos;
                gdvAvisos.DataBind();
            }
            else
            {
                //gdvAvisos.Columns[1].HeaderText = dic.avisos;
                //gdvAvisos.Columns[0].Visible = false;
                DataTable dtAvisosAux = new DataTable();
                dtAvisosAux.Columns.Add("Aviso");
                dtAvisosAux.Rows.Add(dic.noTiene);
                gdvAvisos.DataSource = dtAvisosAux;
                gdvAvisos.DataBind();
            }
        }
        private void avisoretormar()
        {
            DataTable tr = new DataTable();
            tr = APD.retomarfoto(S, M);
            string retomar = tr.Rows[0]["Retomar"].ToString();
            string usuario = tr.Rows[0]["Usuario"].ToString();
            if (retomar == "1/01/1900 00:00:00" || retomar == "1/1/1900 12:00:00 AM" || usuario == " " || string.IsNullOrEmpty(usuario))
            {
                lblretomar.Text = "";
            }
            else
            {
                if (retomar == null || usuario == null)
                {
                    lblretomar.Text = "";
                }
                else
                {
                    if (L == "es")
                    {
                        string sa = "Fecha: ";
                        lblretomar.Text = sa;
                    }
                    else
                    {
                        string sa = "Date: ";
                        lblretomar.Text = sa;
                    }
                    lblretomar.Text = lblretomar.Text + " ( " + retomar + " - " + usuario + " )";
                }
            }
        }
        public void Foto(string S, string M, int grad, string direccion)
        {
            try
            {
                con.Open();
                String sql = "SELECT CASE WHEN dbo.fn_SPON_LastMemberPhoto ('" + S + "','" + M + "') IS NULL THEN ' ' ELSE dbo.fn_SPON_LastMemberPhoto ('" + S + "','" + M + "') END resultado";
                string photourl = obtienePalabra(sql, "resultado");
                String[] newurl = photourl.Split(':');

                string newsubstring = photourl.Replace(@"G:\", "");
                newsubstring = newsubstring.Replace(@"V:\", "");
                newsubstring = newsubstring.Replace(@"g:\", "");
                Boolean foto;
                foto = System.IO.File.Exists(direccion + newurl[1]);
                if (foto == false)
                {
                    lblfecha.Text = dic.MsjNoContieneFotoAPAD;
                    imgApadFoto.Attributes["src"] = "../Images/CommonHope_Heart_RGB.png";
                }
                else
                {
                    if (grad == 0)
                    {
                        imgApadFoto.Attributes["src"] = "../Imagen.ashx?imageID=" + newurl[1].ToString();
                    }
                    else
                    {
                        imgApadFoto.Attributes["src"] = "../ImagenG.ashx?imageID=" + newurl[1].ToString();
                    }

                }
            }
            catch (Exception e)
            {
                mst.mostrarMsjAdvNtf(e.Message);
            }
            finally
            {
                con.Close();
            }
        }
        public void GCarta(string sitio, string padrino, string miembro, string fechaescrita, string usuario, string categoria, string notas, string tipo)
        {
            bool IsUpdated = false;
            string query;
            if (tipo == "M")
            {
                query = @"Insert into MemberSponsorLetter (Project, SponsorId, MemberId, SponsorOrMember, DateTimeWritten, CreationDateTime, RecordStatus, UserId, Category, Notes) values('" + sitio + "', '" + padrino + "', '" + miembro + "', '" + tipo + "'," + fechaescrita + ", getdate(), ' ', '" + usuario + "', '" + categoria + "','" + notas + "')";
            }
            else
            {
                query = @"Insert into MemberSponsorLetter (Project, SponsorId, MemberId, SponsorOrMember, DateTimeWritten, CreationDateTime, RecordStatus, UserId, Category, Notes) values('" + sitio + "', '" + padrino + "', '" + miembro + "', '" + tipo + "'," + fechaescrita + ", getdate(), ' ', '" + usuario + "', '" + categoria + "','" + notas + "')";


            }

            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {

                        cmd.CommandText = query;
                        cmd.Connection = sqlCon;
                        sqlCon.Open();
                        IsUpdated = cmd.ExecuteNonQuery() > 0;

                    }
                    catch (Exception e)
                    {
                        mst.mostrarMsjAdvNtf(e.Message);
                    }
                    finally
                    {
                        sqlCon.Close();
                    }
                }
            }
        }
        public void GraduadoDesa()
        {

            ingreso.Visible = false;
            ingreso2.Visible = false;
        }
        public void GuardarCarta()
        {

            string me = string.Empty;
            string me2 = string.Empty;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("chkagregar");
                if (chk.Checked)
                {
                    me = gvrow.Cells[1].Text;
                    string categoria = ddlcat.SelectedValue;
                    string notas = txtnotas.Text;
                    GCarta(S, me, M, "GETDATE()", U, categoria, notas, "M");

                }
            }
            registrocarta();
            limpiarcampos_carta();
            mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);
        }

        public void InformacionPersonal()
        {
            try
            {
                DataTable dt = APD.InfoGen(S, M, L);
                string educ = dt.Rows[0]["Educ"].ToString();
                string Name = dt.Rows[0]["Nombre"].ToString();
                string Afil = dt.Rows[0]["Estado"].ToString();
                string Age = dt.Rows[0]["Edad"].ToString();
                string rest = dt.Rows[0]["Restriccion"].ToString();

                lblNombre.Text = Name;
                lblRestriccion.Text = dic.restriccionAPAD + " : " + rest;
                lblEstado.Text = dic.afilEstado + " : " + Afil;
                lblNacimiento.Text = dic.fechaNacimiento + " : " + Age;
                lblGrad.Text = dic.grado + " : " + educ;

                string sql2 = "select CASE WHEN dbo.fn_SPON_LastMemberPhotoDate ('" + S + "','" + M + "','" + L + "') IS NULL THEN CASE WHEN '" + L + "'='es' THEN 'No tiene fecha'  ELSE 'No date' END ELSE dbo.fn_SPON_LastMemberPhotoDate ('" + S + "','" + M + "','" + L + "') END  'Fecha de Foto'";
                string m = APD.obtienePalabra(sql2, "Fecha de Foto");
                lblfecha.Text = dic.FechaFotoAPAD + m;

                btnRetomar.Text = dic.RetomarFotoAPAD;
                DataTable tel = APD.NoTelefono(S, F, L);
                string temp = tel.Rows[0][0].ToString();

                lbltelefonos.Text = dic.telefono + ": " + temp;
            }
            finally
            {

            }
        }
        private void limpiarcampos_carta()
        {
            txtnotas.Text = "";
            ddlcat.SelectedIndex = 0;
        }
        private void limpiarRegalos()
        {
            ddlcatrel.SelectedIndex = 0;
            ddltyperel.SelectedIndex = 0;
            txtnotasrel.Text = "";
        }
        public void llenarcombos(String Ls, String Sl)
        {

            string SQL = "DECLARE @L varchar(2) SET @L='" + Ls + "' IF @L='es' BEGIN SELECT Code,DescSpanish descripcion  FROM dbo.CdLetterCategory WHERE Active=1 END ELSE BEGIN SELECT Code,DescEnglish descripcion  FROM dbo.CdLetterCategory WHERE Active=1 END";
            SqlDataAdapter adapter = new SqlDataAdapter(SQL, ConnectionString);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlcat.DataSource = datos;
            ddlcat.DataValueField = "Code";
            ddlcat.DataTextField = "descripcion";
            ddlcat.DataBind();
            ddlcat.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlcat.SelectedIndex = 0;

            string SQ = "DECLARE @L varchar(2) SET @L='" + Ls + "' IF @L='es' BEGIN SELECT Code,DescSpanish descripcion  FROM dbo.CdGiftCategory WHERE Active=1 AND Project='" + Sl + "' END ELSE BEGIN SELECT Code,DescEnglish descripcion  FROM dbo.CdGiftCategory WHERE Active=1  AND Project='" + Sl + "' END";
            SqlDataAdapter adapte = new SqlDataAdapter(SQ, ConnectionString);
            DataTable dat = new DataTable();
            adapte.Fill(dat);
            ddlcatrel.DataSource = dat;
            ddlcatrel.DataValueField = "Code";
            ddlcatrel.DataTextField = "descripcion";
            ddlcatrel.DataBind();
            ddlcatrel.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlcatrel.SelectedIndex = 0;

            string S = "DECLARE @L varchar(2) SET @L='" + Ls + "' IF @L='es' BEGIN SELECT Code,DescSpanish descripcion  FROM dbo.CdGiftType WHERE Active=1 AND Project='" + Sl + "' END ELSE BEGIN SELECT Code,DescEnglish descripcion  FROM dbo.CdGiftType WHERE Active=1 AND Project='" + Sl + "' END";
            SqlDataAdapter adapt = new SqlDataAdapter(S, ConnectionString);
            DataTable dato = new DataTable();
            adapt.Fill(dato);
            ddltyperel.DataSource = dato;
            ddltyperel.DataValueField = "Code";
            ddltyperel.DataTextField = "descripcion";
            ddltyperel.DataBind();
            ddltyperel.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddltyperel.SelectedIndex = 0;

        }
        public string obtienePalabra(string stringSQL, string title)
        {
            SqlConnection cn = new SqlConnection(ConnectionString);
            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();
            string temp = "";

            try
            {
                cn.Open();
                daUser = new SqlDataAdapter(stringSQL, ConnectionString);
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
        public void Padrinos(string pro, string memb)//busca los padrinos de un miembro
        {

            try
            {
                con2.Open();
                var select = "SELECT SponsorMemberRelation.SponsorId, Sponsor.SponsorNames FROM dbo.SponsorMemberRelation inner join dbo.Sponsor on SponsorMemberRelation.SponsorId = Sponsor.SponsorId where MemberId like '" + memb + "' and SponsorMemberRelation.Project like '" + pro + "' and SponsorMemberRelation.RecordStatus like ' ' and EndDate is null and Sponsor.RecordStatus like ' '";
                var dataAdapter = new SqlDataAdapter(select, con2);
                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();

                dataAdapter.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
                con2.Close();

            }
            catch (Exception ex)
            {
                if (L == "es")
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('No tiene Padrinos');window.location ='../SearchProf.aspx';", true);
                }
                else
                {
                    Page.ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('Member with out Sponsor');window.location ='../SearchProf.aspx';", true);
                }
            }
            finally
            {
                con.Close();
            }
        }
        public void registrocarta()
        {
            DataTable ds = APD.CartasRegistro(M, L, S);
            gvcara.DataSource = ds;
            gvcara.DataBind();
        }
        public void registroregalo()
        {
            DataTable ds = APD.RegaloRegistro(M, L, S);
            gvregalo.DataSource = ds;
            gvregalo.DataBind();
        }
        public void traducir()
        {
            lbltp.Text = dic.padrinos;
            lblTitleP.Text = dic.infoGeneralAPAD;
            lbltitcart.Text = dic.titIngCartAPAD;
            lblcat.Text = dic.categoriaAPAD;
            lblnotas.Text = dic.notasAPAD;
            btncaracep.Text = dic.AceptarAPAD;
            btncarcan.Text = dic.CancelarAPAD;
            btnregacep.Text = dic.AceptarAPAD;
            btnregcan.Text = dic.CancelarAPAD;
            lblcatrel.Text = dic.categoriaAPAD;
            lbltyperel.Text = dic.TipoAPAD;
            lblrelnotas.Text = dic.notasAPAD;
            lbltitreg.Text = dic.IngresarRegaloAPAD;
            lbltp.Text = dic.padrinos;
        }
        public void VerificarPadrinos()
        {
            DataTable dtPadrinos = APD.VerPadrinos(S, M, L);
            if (dtPadrinos.Rows.Count == 0)
            {
                gvpadrinos.Visible = false;
                // lblNoPadrinos.Visible = true;
            }
            else
            {
                gvpadrinos.DataSource = dtPadrinos;
                gvpadrinos.DataBind();
            }

        }
        public void utlimafoto(String proje, String memid, String lang)
        {
            SqlConnection conexion = new SqlConnection(ConnectionString);
            con.Open();
            string sql = "select dbo.fn_SPON_LastMemberPhotoDate ('" + proje + "','" + memid + "','" + lang + "') as 'Fecha de Foto'";
            SqlCommand comando = new SqlCommand(sql, con);

            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable dt = new DataTable();
            adaptador.Fill(dt);
            conexion.Close();
            string fecha = dt.Rows[0][0].ToString();
            lblfecha.Text = fecha;
        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                M = mast.M;
                L = mast.L;
                S = mast.S;
                P = mast.P;
                U = mast.U;
                F = mast.F;
                dic = new Diccionario(L, S);
                mst = (mast)Master;
                APD = new BDAPAD();
                if (!IsPostBack)
                {
                    mst = (mast)Master;



                    BDM = new BDMiembro();
                    BDF = new BDFamilia();
                    APD = new BDAPAD();
                    dic = new Diccionario(L, S);
                    DataTable dt = APD.InfoGen(S, M, L);
                    try
                    {
                        string A = dt.Rows[0][1].ToString();
                        if (A == "Afiliado" || A == "Affiliated")
                        {
                            ruta = @"\\SVRAPP\FamilyFotos\Apadrinados";
                            desicion = 0;

                            agregaAvisos();
                            Padrinos(S, M);

                            traducir();
                            llenarcombos(L, S);
                            registrocarta();
                            registroregalo();
                            InformacionPersonal();
                            avisoretormar();
                            Foto(S, M, desicion, ruta);
                            VerificarPadrinos();

                        }
                        else if (string.IsNullOrEmpty(A))
                        {

                        }
                        else
                        {
                            ruta = @"\\SVRAPP\FamilyFotos\ExApadrinados";
                            desicion = 1;
                            GraduadoDesa();
                            agregaAvisos();
                            VerificarPadrinos();
                            traducir();
                            InformacionPersonal();
                            Foto(S, M, desicion, ruta);
                            btnRetomar.Visible = false;
                        }
                    }
                    catch (Exception ex)
                    {
                    }

                }
                if (IsPostBack)
                {
                    BDM = new BDMiembro();
                    APD = new BDAPAD();
                }
                mst = (mast)Master;
                // mst.contentCallEvent += EventHandler();
            }

            catch (Exception ez)
            {
                Session["M"] = null;
                Session["F"] = null;

            }
        }
        protected void btncaracep_Click(object sender, EventArgs e)
        {
            if (ddlcat.SelectedIndex > 0)
            {
                int count = 0;
                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("chkagregar");
                    if (chk.Checked)
                    {
                        count = count + 1;
                    }
                }

                if ((count == 0))
                {
                    mst.mostrarMsjAdvNtf(dic.CampoVacioAPAD);
                }
                else
                {
                    GuardarCarta();
                }
            }
            else
            {
                string categoria;
                if (L == "en")
                {
                    categoria = "You have not selected any categories";
                }
                else
                {
                    categoria = "No ha seleccionado una categoria";
                }
                mst.mostrarMsjAdvNtf(categoria);
            }
        }//fin clase
        protected void btnregacep_Click(object sender, EventArgs e)
        {
            if (ddlcatrel.SelectedIndex > 0 && ddltyperel.SelectedIndex > 0)
            {
                string categoria = ddlcatrel.SelectedValue;
                string notas = txtnotasrel.Text;
                string tipo = ddltyperel.SelectedValue;
                string sql = @"INSERT INTO dbo.MemberGift VALUES('" + S + "', '" + M + "', '" + categoria + "', GETDATE(),GETDATE(),' ', '" + U + "', NULL, '" + tipo + "','" + notas + "', NULL,NULL)";
                SqlCommand cmd = null;

                cmd = new SqlCommand(sql, con);

                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    registroregalo();
                    limpiarRegalos();
                    agregaAvisos();
                    mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjAdvNtf("Error: " + ex);

                }
                finally
                {

                    con.Close();
                }

            }
            else
            {

                string campos;
                if (L == "en")
                {
                    campos = "You have not filled the fields.";
                }
                else
                {
                    campos = "No llenado los campos";
                }
                mst.mostrarMsjAdvNtf(campos);
            }
        }

        protected void btncarcan_Click(object sender, EventArgs e)
        {
            agregaAvisos();
            ddlcatrel.SelectedIndex = 0;
            ddltyperel.SelectedIndex = 0;
            txtnotasrel.Text = "";
        }

        protected void btnRetomar_Click(object sender, EventArgs e)
        {
            DataTable tr = new DataTable();
            tr = APD.retomarfoto(S, M);
            string retomar = tr.Rows[0]["Retomar"].ToString();
            string usuario = tr.Rows[0]["Usuario"].ToString();

            if (retomar == "1/01/1900 00:00:00" || retomar == "1/1/1900 12:00:00 AM" || usuario == " " || string.IsNullOrEmpty(usuario))
            {
                string sql = @"INSERT INTO dbo.MiscMemberSponsorInfo SELECT Project, MemberId, GETDATE() CreationDateTime, RecordStatus, '" + U + "' UserId, ExpirationDateTime, Photo, PhotoDate, GETDATE() RetakePhotoDate, '" + U + "' RetakePhotoUserId, LastCarnetPrintDate, SponsorshipLevel, SponsorshipType, Restriction, RestrictionDate, ExceptionPhotoDate FROM dbo.MiscMemberSponsorInfo WHERE RecordStatus = ' '  AND Project = '" + S + "' AND MemberId = " + M + "";

                SqlCommand cmd = new SqlCommand(sql, con);
                try
                {
                    con.Open();
                    cmd.ExecuteNonQuery();
                    agregaAvisos();
                }
                catch (Exception ex)
                {
                }
                con.Close();

                //APD.ejecutarSQL(sql);
                avisoretormar();
            }
            else
            {
                mst.mostrarMsjAdvNtf(dic.MsjYaingresoRegistro);
            }
        }
        protected void btnregcan_Click(object sender, EventArgs e)
        {
            agregaAvisos();
            limpiarRegalos();
        }

        protected void gvpadrinos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = dic.NumeroPadrino;
                e.Row.Cells[1].Text = dic.nombre;
                e.Row.Cells[2].Text = dic.fechaInicio;
                e.Row.Cells[3].Text = dic.fechaFin;
                e.Row.Cells[4].Text = dic.TipoAPAD.Replace(":", "");
                e.Row.Cells[5].Text = dic.genero;
                e.Row.Cells[6].Text = dic.HablaEspanolAPAD;
            }
        }
        protected void gvpadrinos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LinkButton link = (LinkButton)gvpadrinos.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("btnSName");
            P = link.Text;
            Session["P"] = P;
            Response.Redirect("PerfilPadrinoAPAD.aspx");
        }
    }
}

