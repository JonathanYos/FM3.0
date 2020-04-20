using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.Apadrinamiento
{
    public partial class RegistroRegaloAPAD : System.Web.UI.Page
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
        protected static String Site;
        protected static String Member;
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);


        //////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--//////////////////////////////////////////
        private void accionar(object sender, EventArgs e)
        {
            dic = new Diccionario(L, Site);
            eliminar();
        }
        private void eliminar()
        {
            ///////////////////////////////////////////////
            ddlcategoria.Enabled = true;
            ddltipo.Enabled = true;
            chkentrega.Enabled = true;
            string sql2 = "INSERT INTO dbo.MemberGift SELECT Project,MemberId,Category,SelectionDateTime,GETDATE() CreationDateTime,'H' RecordStatus,'" + U + "' UserId,GETDATE() ExpirationDateTime,Type, Notes,DeliveryDateTime,SizeGift FROM dbo.MemberGift WHERE RecordStatus=' ' AND MemberId='" + Member + "' AND Project='" + Site + "' AND Notes='" + lblVnotas2.Text + "' AND Type ='" + ddltipo.SelectedValue + "' AND Category='" + ddlcategoria.SelectedValue + "' AND dbo.fn_GEN_FormatDate(SelectionDateTime,'" + L + "') ='" + lblVseleccion.Text + "'";
            string actualizar = "UPDATE dbo.MemberGift SET RecordStatus='H', ExpirationDateTime=GETDATE() WHERE RecordStatus=' ' AND MemberId='" + Member + "' AND Project='" + Site + "' AND Notes='" + lblVnotas2.Text + "' AND Type ='" + ddltipo.SelectedValue + "' AND Category='" + ddlcategoria.SelectedValue + "' AND dbo.fn_GEN_FormatDate(SelectionDateTime,'" + L + "') ='" + lblVseleccion.Text + "'";
            SqlCommand cmd = null;
            cmd = new SqlCommand(sql2, con);
            SqlCommand cmd2 = null;
            cmd2 = new SqlCommand(actualizar, con);
            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                mst.mostrarMsjNtf(dic.RegistroEliminadoAPAD);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
            finally
            {
                con.Close();
            }

            LimpiarCampos();
            /////////////////////////////////////////////////////////////
        }
        private void guardar(string S, string M, string categoria, int seleccion, string U, string tipo, string notas)
        {
            string sql;
            if (seleccion == 1)
            {
                sql = "INSERT INTO dbo.MemberGift VALUES('" + Site + "', '" + Member + "', '" + categoria + "', GETDATE(),GETDATE(),' ', '" + U + "', NULL, '" + tipo + "', '" + notas + "', GETDATE(),NULL)";
            }
            else
            {
                sql = "INSERT INTO dbo.MemberGift VALUES('" + Site + "', '" + Member + "', '" + categoria + "', GETDATE(),GETDATE(),' ', '" + U + "', NULL, '" + tipo + "', '" + notas + "', NULL,NULL)";
            }
            SqlCommand cmd = null;

            cmd = new SqlCommand(sql, con);

            try
            {
                if (cmd.Connection.State == ConnectionState.Closed)
                {
                    con.Open();
                }
                cmd.ExecuteNonQuery();
                LlenarHistorial();
                LimpiarCampos();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
            finally
            {

                con.Close();
            }
        }
        private void LimpiarCampos()
        {
            btnaceptar.Visible = true;
            lblVseleccion.Text = "";
            ddltipo.Enabled = true;
            ddlcategoria.Enabled = true;
            chkentrega.Enabled = true;
            ddlcategoria.SelectedIndex = 0;
            ddltipo.SelectedIndex = 0;
            txtnotas.Text = "";
            chkentrega.Checked = false;
            btnmodificar.Visible = false;
            btneliminar.Visible = false;
            lblVnotas2.Text = "";
            LlenarHistorial();
        }
        private void llenarcombos(String L, String S)
        {
            string sql = "IF '" + L + "'='es' SELECT Code, DescSpanish descripcion FROM dbo.CdGiftCategory  WHERE Project='" + Site + "' AND Active =1 ORDER BY DescSpanish ASC  ELSE SELECT Code, DescEnglish descripcion FROM dbo.CdGiftCategory WHERE Project='" + Site + "' AND Active=1 ORDER BY DescEnglish ASC";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, ConnectionString);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlcategoria.DataSource = datos;
            ddlcategoria.DataValueField = "Code";
            ddlcategoria.DataTextField = "descripcion";
            ddlcategoria.DataBind();
            ddlcategoria.Items.Insert(0, new ListItem(String.Empty, String.Empty));


            string sentencia = "IF '" + L + "'='es' SELECT Code, DescSpanish descripcion FROM dbo.CdGiftType WHERE Project='" + Site + "' AND Active=1 ORDER BY DescSpanish ASC ELSE SELECT Code, DescEnglish descripcion FROM dbo.CdGiftType WHERE Project='" + Site + "' AND Active=1 ORDER BY DescEnglish ASC";
            SqlDataAdapter adapter1 = new SqlDataAdapter(sentencia, ConnectionString);
            DataTable tr = new DataTable();
            adapter1.Fill(tr);
            ddltipo.DataSource = tr;
            ddltipo.DataValueField = "Code";
            ddltipo.DataTextField = "descripcion";
            ddltipo.DataBind();
            ddltipo.Items.Insert(0, new ListItem(String.Empty, String.Empty));

            sql = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.FwCdOrganization WHERE Code NOT LIKE'A' AND Code NOT LIKE'E' AND Code NOT LIKE'S' ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END ASC";
            SqlDataAdapter adapter2 = new SqlDataAdapter(sql, con);
            DataTable datos2 = new DataTable();
            adapter2.Fill(datos2);
            ddlsitio.DataSource = datos2;
            ddlsitio.DataValueField = "Code";
            ddlsitio.DataTextField = "descripcion";
            ddlsitio.DataBind();
            ddlsitio.Items.Insert(0, new ListItem(String.Empty, String.Empty));

        }
        private void LlenarHistorial()
        {
            DataTable tr = new DataTable();
            tr = APD.RegaloRegistro(Member, L, Site);
            gvhistorial.DataSource = tr;
            gvhistorial.DataBind();
        }
        private void LlenarLabelsyOtros()
        {
            txtmiembro.Attributes.Add("maxlength", "6");
            lblcategoria.Text = dic.categoriaAPAD;
            lbltipo.Text = dic.TipoAPAD;
            lblnotas.Text = dic.notasAPAD;
            lblfechaentrega.Text = dic.FechaEntregaAPAD;
            btnaceptar.Text = dic.AceptarAPAD;
            btncancelar.Text = dic.CancelarAPAD;
            btneliminar.Text = dic.EliminarAPAD;
            btnmodificar.Text = dic.ModificarAPAD;
            btnmodificar.Visible = false;
            btneliminar.Visible = false;
            lblsitio.Text = dic.sitio;
            lblmiembro.Text = dic.miembro;
            btnbuscar.Text = dic.buscar;
        }
        private void modificar()
        {
            string cheke;///////////////////////////////////////////////
            if (chkentrega.Checked == true && chkentrega.Enabled == true)
            {
                cheke = "GETDATE() ";
            }
            else
            {
                cheke = "";
            }
            string sql = "INSERT INTO dbo.MemberGift SELECT Project,MemberId,Category,SelectionDateTime,GETDATE() CreationDateTime,RecordStatus,'" + U + "' UserId,ExpirationDateTime,Type,'" + txtnotas.Text + "' Notes," + cheke + "DeliveryDateTime,SizeGift FROM dbo.MemberGift WHERE RecordStatus=' ' AND MemberId='" + Member + "' AND Project='" + Site + "' AND Notes='" + lblVnotas2.Text + "' AND Type ='" + ddltipo.SelectedValue + "' AND Category='" + ddlcategoria.SelectedValue + "' AND dbo.fn_GEN_FormatDate(SelectionDateTime,'" + L + "') ='" + lblVseleccion.Text + "'";

            SqlCommand cmd = null;

            cmd = new SqlCommand(sql, con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
            finally
            {

                con.Close();
            }
            LlenarHistorial();
            LimpiarCampos();
            ///////////////////////////////////////////////////
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
                mst.mostrarMsjAdvNtf(ex.Message);
                temp = 5;
                return temp;
            }
        }
        private void valoresiniciales(String Sitio, String Miembro)
        {
            dic = new Diccionario(L, Sitio);
            LlenarHistorial();
            llenarcombos(L, Sitio);
            LlenarLabelsyOtros(); ;
            ddlsitio.SelectedValue = Sitio;
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
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////

        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;

            mst = (mast)Master;
            APD = new BDAPAD();
            dic = new Diccionario(L, S);
            BDM = new BDMiembro();
            mst.contentCallEvent += new EventHandler(accionar);
            if (!IsPostBack)
            {
                mst = (mast)Master;
                BDM = new BDMiembro();
                APD = new BDAPAD();
                try
                {
                    if (string.IsNullOrEmpty(M))
                    {
                        gvhistorial.Visible = false;
                        tbregistro.Visible = false;
                        tbfiltros.Visible = false;
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
                    mst.mostrarMsjAdvNtf(ex.Message);
                }
            }
        }

        protected void btnaceptar_Click(object sender, EventArgs e)
        {
            dic = new Diccionario(L, Site);
            string tipo, notas, categoria;
            if (ddlcategoria.SelectedIndex < 1 || ddltipo.SelectedIndex < 1)
            {
                mst.mostrarMsjAdvNtf(dic.CampoVacioAPAD);
            }
            else
            {
                int seleccion;
                if (chkentrega.Checked == true)
                {
                    seleccion = 1;
                }
                else
                {
                    seleccion = 0;
                }
                categoria = ddlcategoria.SelectedValue.ToString();
                tipo = ddltipo.SelectedValue.ToString();
                notas = txtnotas.Text;
                guardar(Site, Member, categoria, seleccion, U, tipo, notas);
            }
        }
        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ddltipo.Enabled = true;
            ddlcategoria.Enabled = true;
            chkentrega.Enabled = true;
            string entrega = HttpUtility.HtmlDecode(gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text);
            string notas = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            string categoria = HttpUtility.HtmlDecode(gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text);
            string tipo = HttpUtility.HtmlDecode(gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text);
            string seleccion = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
            lblVseleccion.Text = seleccion;
            txtnotas.Text = notas.Replace("&nbsp;", " ");
            lblVnotas2.Text = notas.Replace("&nbsp;", " ");
            if (string.IsNullOrWhiteSpace(entrega) || string.IsNullOrEmpty(entrega)) { chkentrega.Enabled = true; chkentrega.Checked = false; } else { chkentrega.Enabled = false; chkentrega.Checked = true; }
            try
            {
                string sql = "SELECT Code  FROM dbo.CdGiftCategory WHERE Project='" + Site + "' AND DescSpanish='" + categoria + "' AND Active=1 OR Project='" + Site + "' AND DescEnglish='" + categoria + "' AND Active=1";
                string codcategoria = APD.obtienePalabra(sql, "Code");
                ddlcategoria.SelectedValue = codcategoria;
            }
            catch (Exception ex)
            {
                string sql = "SELECT Code  FROM dbo.CdGiftCategory WHERE Project='" + Site + "' AND DescSpanish='" + categoria + "' AND Active=1 OR Project='" + Site + "' AND DescEnglish='" + categoria + "' AND Active=1";
                mst.mostrarMsjAdvNtf("Categoria " + sql);
            }
            try
            {
                string sql2 = "SELECT Code  FROM dbo.CdGiftType WHERE Project='" + Site + "' AND DescSpanish='" + tipo + "' AND Active=1 OR Project='" + Site + "' AND DescEnglish='" + tipo + "' AND Active=1";
                string codtipo = APD.obtienePalabra(sql2, "Code");
                ddltipo.SelectedValue = codtipo;
            }
            catch (Exception ex)
            {
                string sql2 = "SELECT Code  FROM dbo.CdGiftType WHERE Project='" + Site + "' AND DescSpanish='" + tipo + "' AND Active=1 OR Project='" + Site + "' AND DescEnglish='" + tipo + "' AND Active=1";

                mst.mostrarMsjAdvNtf("Tipo " + sql2);
            }
            ddltipo.Enabled = false;
            ddlcategoria.Enabled = false;
            btnmodificar.Visible = true;
            btnaceptar.Visible = false;
            btneliminar.Visible = true;
        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            LimpiarCampos();
        }

        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            dic = new Diccionario(L, S);
            modificar();
        }



        protected void btneliminar_Click(object sender, EventArgs e)
        {
            dic = new Diccionario(L, S);
            mst = (mast)Master;
            mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
        }

        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            try
            {
                if (ddlsitio.SelectedIndex == 0 || string.IsNullOrEmpty(txtmiembro.Text))
                {
                    mst.mostrarMsjAdvNtf(dic.msjCampoNecesario);
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

        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string seleccion;
            if (L == "es")
            {
                seleccion = "Seleccion";
            }
            else
            {
                seleccion = "Selection";
            }
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = seleccion;
                e.Row.Cells[1].Text = dic.categoriaAPAD;
                e.Row.Cells[2].Text = dic.TipoAPAD;
                e.Row.Cells[3].Text = dic.notas;
                e.Row.Cells[4].Text = dic.FechaEntregaAPAD;
            }
        }
    }

}
