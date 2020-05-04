using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.Apadrinamiento
{
    public partial class RegistroCartasAPAD : System.Web.UI.Page
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
        protected static int accion;
        protected static DataTable dtd = new DataTable();
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);

        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            mst = (mast)Master;
            mst.contentCallEvent += new EventHandler(accionar);
            dic = new Diccionario(L, S);
            APD = new BDAPAD();
            BDM = new BDMiembro();

            if (!IsPostBack)
            {
                BDM = new BDMiembro();
                APD = new BDAPAD();
                try
                {
                    Site = S;
                    Member = M;
                    valoresiniciales(Member, Site);
                    pnlimprimir.Visible = true;
                    LtbParaImprimir.Visible = false;
                }
                catch (Exception ex)
                {

                }
                //pnlimprimir.Visible = false;
            }
        }
        //////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--//////////////////////////////////////////
        private void accionar(object sender, EventArgs e)
        {

            try
            {
                if (accion == 1)
                {
                    string sql = @"SELECT UserId fecha FROM dbo.MemberSponsorLetter WHERE RecordStatus=' ' AND Project='" + Site + "' AND MemberId='" + Member + "' AND SponsorId='" + lblVidpadrino.Text + "' AND Category='" + ddlcat.SelectedValue + "' AND Notes='" + lblVnota.Text + "'";
                    string usuario = APD.obtienePalabra(sql, "fecha");
                    string ts = "SELECT FER.EmployeeId ts FROM Member M INNER JOIN Family F ON M.Project = F.Project AND M.LastFamilyId = F.FamilyId AND M.RecordStatus = F.RecordStatus AND M.RecordStatus = ' ' LEFT JOIN FamilyEmployeeRelation FER ON FER.Project = F.Project AND FER.FamilyId = F.FamilyId AND FER.RecordStatus = F.RecordStatus AND FER.EndDate IS NULL WHERE M.Project = '" + Site + "' AND M.MemberId = '" + Member + "'";
                    string nombrets = APD.obtienePalabra(ts, "ts");
                    string apadrinado = "SELECT FirstNames+' '+LastNames nombre  FROM dbo.Member WHERE RecordStatus=' ' AND Project='" + Site + "' AND MemberId='" + Member + "'";
                    string nombreapad = APD.obtienePalabra(apadrinado, "nombre");
                    string padrino3 = "SELECT (select case when SpeaksSpanish = 1 then 'Sí Habla Español' else 'No Habla Español' end) 'Español' FROM dbo.Sponsor WHERE RecordStatus=' ' AND SponsorId='" + lblVidpadrino.Text + "' ";
                    string Espanol = APD.obtienePalabra(padrino3, "Español");
                    string padrino2 = @"SELECT dbo.fn_GEN_FormatDate(DateTimeWritten,'" + L + "') escrita FROM dbo.MemberSponsorLetter WHERE RecordStatus=' ' AND Project='" + Site + "' AND MemberId='" + Member + "' AND Notes='" + lblVnota.Text + "' AND Category='" + ddlcat.SelectedValue + "' AND UserId='" + usuario + "' AND SponsorId='" + lblVidpadrino.Text + "'";
                    string escrita = APD.obtienePalabra(padrino2, "escrita");


                    if (dtd.Columns.Count == 0)
                    {
                        dtd.Columns.Add("Sitio");
                        dtd.Columns.Add("Miembro");
                        dtd.Columns.Add("Padrino");
                        dtd.Columns.Add("IdPadrino");
                        dtd.Columns.Add("IdCategoria");
                        dtd.Columns.Add("Notas");
                    }
                    DataRow dr = dtd.NewRow();
                    dr[0] = Site;
                    dr[1] = Member;
                    dr[2] = lblVpadrino.Text.Replace("(" + lblVidpadrino.Text + ")", "");
                    dr[3] = lblVidpadrino.Text;
                    dr[4] = ddlcat.SelectedValue;
                    dr[5] = txtnotas.Text;

                    dtd.Rows.Add(dr);


                    LtbParaImprimir.Items.Add("-------------------------------------------------------------------------------------------");
                    LtbParaImprimir.Items.Add(dic.padrinos + ": " + lblVpadrino.Text.Replace("(" + lblVidpadrino.Text + ")", "") + "(" + Espanol + ")");
                    LtbParaImprimir.Items.Add(dic.nombre + ": " + nombreapad + " (" + Site + Member + ") ");
                    LtbParaImprimir.Items.Add(dic.tipo + ": " + ddlcat.SelectedItem.Text);
                    LtbParaImprimir.Items.Add(dic.EscritaAPAD + ": " + escrita);
                    LtbParaImprimir.Visible = true;
                    btnimpval.Visible = true;
                    limpiartodo();
                }
                if (accion == 2)
                {
                    ddlcat.Enabled = true;
                    txtnotas.Enabled = true;
                    string padrino, cat, notas;
                    padrino = lblVidpadrino.Text;
                    cat = ddlcat.SelectedValue;
                    notas = txtnotas.Text;
                    eliminaregistro(padrino, cat);
                }

            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }

        }
        private void eliminaregistro(string padrino, string categoria)
        {

            dic = new Diccionario(L, Site);
            string sql = "INSERT INTO dbo.MemberSponsorLetter SELECT Project,SponsorId,MemberId,SponsorOrMember,DateTimeWritten,GETDATE() CreationDateTime,'H' RecordStatus,'" + U + "' UserId,GETDATE() ExpirationDateTime,DateSent,Category, Notes,Translated,SentToGuatemala FROM dbo.MemberSponsorLetter WHERE RecordStatus=' ' AND Project='" + Site + "' AND MemberId='" + Member + "' AND SponsorId='" + padrino + "' AND Category='" + categoria + "' AND Notes='" + lblnotas.Text + "'";
            string actualizar = "UPDATE dbo.MemberSponsorLetter SET RecordStatus='H', ExpirationDateTime=GETDATE() WHERE RecordStatus=' ' AND Project='" + Site + "' AND MemberId='" + Member + "' AND SponsorId='" + padrino + "' AND Category='" + categoria + "' AND Notes='" + lblVnota.Text + "'";

            SqlCommand cmd = null;

            cmd = new SqlCommand(sql, con);
            SqlCommand cmd2 = null;

            cmd2 = new SqlCommand(actualizar, con);
            try
            {
                dic = new Diccionario(L, Site);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                limpiartodo();
                HistorialCarta();
                mst.mostrarMsjNtf(dic.RegistroEliminadoAPAD);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }

            ///  registrocarta();
        }
        public void GCarta(string sitio, string padrino, string miembro, string usuario, string categoria, string notas, string tipo)
        {
            bool IsUpdated = false;
            string query;
            if (tipo == "M")
            {
                query = "INSERT INTO MemberSponsorLetter (Project, SponsorId, MemberId, SponsorOrMember, DateTimeWritten, CreationDateTime, RecordStatus, UserId, Category, Notes) VALUES('" + sitio + "', '" + padrino + "', '" + miembro + "', '" + tipo + "',GETDATE(), getdate(), ' ', '" + usuario + "', '" + categoria + "','" + notas + "')";
            }
            else
            {
                query = "INSERT INTO MemberSponsorLetter (Project, SponsorId, MemberId, SponsorOrMember, DateTimeWritten, CreationDateTime, RecordStatus, UserId, Category, Notes) VALUES('" + sitio + "', '" + padrino + "', '" + miembro + "', '" + tipo + "',GETDATE(), getdate(), ' ', '" + usuario + "', '" + categoria + "','" + notas + "')";
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
        public void GuardarCarta()
        {
            string me = string.Empty;
            string me2 = string.Empty;
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("CheckBox1");
                if (chk.Checked)
                {
                    me = gvrow.Cells[1].Text;
                    string categoria = ddlcat.SelectedValue.ToString();
                    string notas = txtnotas.Text;

                    Imprimir(Site, me, Member);
                    GCarta(Site, me, Member, U, categoria, notas, "M");
                }
            }
            HistorialCarta();
        }
        private void HistorialCarta()
        {
            DataTable ds = APD.CartasRegistro(Member, L, Site);
            gvcarta.DataSource = ds;
            gvcarta.DataBind();
        }
        public void Imprimir(string S, string P, string M)//procedimiento que imprimi slip de carta ingresada
        {
            try
            {
                SqlDataReader dr;
                SqlCommand cmd = new SqlCommand();
                con.Open();
                cmd.Connection = con;
                cmd.CommandText = "select M.FirstNames + ' ' + M.LastNames as Name, M.LastFamilyId, FER.EmployeeId,(select case when SpeaksSpanish = 1 then 'Sí Habla Español' else 'No Habla Español' end as Español from Sponsor where SponsorId = '" + P + "' and RecordStatus like ' ') as Español, (select SponsorNames from Sponsor where SponsorId = '" + P + "' and RecordStatus like ' ') as Sponsor from Member M inner join FamilyEmployeeRelation FER on M.LastFamilyId = FER.FamilyId and M.Project = FER.Project and M.RecordStatus = FER.RecordStatus and FER.EndDate is null where M.RecordStatus like ' ' and M.Project like '" + Site + "'  and M.MemberId = '" + Member + "' ";
                dr = cmd.ExecuteReader();
                DataTable dt = new DataTable();
                dt.Load(dr);
                string membername = dt.Rows[0][0].ToString();
                string FamId = dt.Rows[0][1].ToString();
                string TS = dt.Rows[0][2].ToString();
                string Speaks = dt.Rows[0][3].ToString();
                string spnam = dt.Rows[0][4].ToString();
                pnlimprimir.Controls.Add(new LiteralControl("----------------------------------------------------------------------" + DateTime.Now.ToString("yyyy-MM-dd")));
                pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                pnlimprimir.Controls.Add(new LiteralControl("No :  " + P));
                pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                pnlimprimir.Controls.Add(new LiteralControl(dic.nombrePadrino + ":  " + spnam + "  (" + Speaks + ")"));
                pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                pnlimprimir.Controls.Add(new LiteralControl(dic.miembro + ":  " + Site + Member + ""));
                pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                pnlimprimir.Controls.Add(new LiteralControl(dic.nombreMiembro + ":  " + membername + ""));
                pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                pnlimprimir.Controls.Add(new LiteralControl(dic.idFamilia + ":  " + FamId + ""));
                pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                pnlimprimir.Controls.Add(new LiteralControl(dic.trabajadorS + ":  " + TS + ""));
                pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                pnlimprimir.Controls.Add(new LiteralControl("<br />"));

            }
            catch (Exception e)
            {

            }
            finally
            {
                con.Close();
            }
        }
        public void llenarcombos(String Ls)
        {

            string SQL = "DECLARE @L varchar(2) SET @L='" + Ls + "' SELECT Code, CASE WHEN @L='es' THEN DescSpanish  ELSE DescEnglish END descripcion FROM dbo.CdLetterCategory WHERE Active=1";
            SqlDataAdapter adapter = new SqlDataAdapter(SQL, con);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlcat.DataSource = datos;
            ddlcat.DataValueField = "Code";
            ddlcat.DataTextField = "descripcion";
            ddlcat.DataBind();
            ddlcat.Items.Insert(0, new ListItem(String.Empty, String.Empty));

            SQL = "SELECT Code, CASE WHEN '" + Ls + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.FwCdOrganization WHERE Code NOT LIKE'A' AND Code NOT LIKE'E' AND Code NOT LIKE'S' ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END ASC";
            SqlDataAdapter adapter2 = new SqlDataAdapter(SQL, con);
            DataTable datos2 = new DataTable();
            adapter2.Fill(datos2);
            ddlsitio.DataSource = datos2;
            ddlsitio.DataValueField = "Code";
            ddlsitio.DataTextField = "descripcion";
            ddlsitio.DataBind();
            ddlsitio.Items.Insert(0, new ListItem(String.Empty, String.Empty));

        }

        private void limpiartodo()
        {
            foreach (GridViewRow gvrow in GridView1.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("CheckBox1");
                chk.Checked = true;
            }
            ddlcat.Enabled = true;
            dic = new Diccionario(L, Site);
            ddlcat.SelectedIndex = 0;
            txtnotas.Text = "";
            lblVpadrino.Text = "";
            GridView1.Visible = true;
            btncaracep.Text = dic.AceptarAPAD;
            lbltitpadrino.Text = "";
            lbltitpadrino.Visible = false;
            lblVpadrino.Visible = false;
            btnimprimir.Visible = false;
            btneliminar.Visible = false;
            lblVfecha.Visible = false;
            lbltitFecha.Visible = false;
        }
        public void MCarta(string sitio, string padrino, string miembro, string usuario, string categoria, string notas, string tipo)
        {
            bool IsUpdated = false;
            string query1;

            query1 = "INSERT INTO dbo.MemberSponsorLetter SELECT Project,SponsorId,MemberId,SponsorOrMember,DateTimeWritten,GETDATE() CreationDateTime,RecordStatus,'" + U + "' UserId,ExpirationDateTime,DateSent,Category,'" + notas + "' Notes,Translated,SentToGuatemala FROM dbo.MemberSponsorLetter WHERE RecordStatus=' ' AND Project='" + Site + "' AND MemberId='" + Member + "' AND SponsorId='" + padrino + "' AND Category='" + categoria + "' AND Notes='" + lblVnota.Text + "'";
            using (SqlConnection sqlCon = new SqlConnection(ConnectionString))
            {
                using (SqlCommand cmd = new SqlCommand())
                {
                    try
                    {
                        cmd.CommandText = query1;
                        cmd.Connection = sqlCon;
                        sqlCon.Open();
                        IsUpdated = cmd.ExecuteNonQuery() > 0;

                        mst.mostrarMsjNtf(dic.RegistroModificadoAPAD);
                        limpiartodo();
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
        public void Padrinos(string pro, string memb)//busca los padrinos de un miembro
        {
            try
            {
                con.Open();
                var select = "SELECT SponsorMemberRelation.SponsorId, Sponsor.SponsorNames FROM dbo.SponsorMemberRelation inner join dbo.Sponsor on SponsorMemberRelation.SponsorId = Sponsor.SponsorId where MemberId like '" + memb + "' and SponsorMemberRelation.Project like '" + pro + "' and SponsorMemberRelation.RecordStatus like ' ' and EndDate is null and Sponsor.RecordStatus like ' '";
                var dataAdapter = new SqlDataAdapter(select, con);
                var commandBuilder = new SqlCommandBuilder(dataAdapter);
                var ds = new DataSet();

                dataAdapter.Fill(ds);
                GridView1.DataSource = ds;
                GridView1.DataBind();
                con.Close();
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
        }
        public void traducir()
        {
            string imp;
            if (L == "es") { imp = "Agregar para imprimir"; } else { imp = "Add to Print"; }
            txtmiembro.Attributes.Add("maxlength", "6");
            btnimpval.Text = dic.ImprimirAPAD;
            lbltitcart.Text = dic.titIngCartAPAD + " (" + Member + ")";
            lblcat.Text = dic.categoriaAPAD;
            btnimprimir.Text = imp;
            lblnotas.Text = dic.notasAPAD;
            btncaracep.Text = dic.AceptarAPAD;
            btncarcan.Text = dic.CancelarAPAD;
            lblmiembro.Text = dic.miembro;
            lblsitio.Text = dic.sitio;
            btnbuscar.Text = dic.buscar;

            //No mostrar

            btneliminar.Visible = false;
            btnimprimir.Visible = false;
            lbltitFecha.Visible = false;
            lblVfecha.Visible = true;
        }
        private void valoresiniciales(string M, string S)
        {
            dic = new Diccionario(L, Site);
            traducir();
            llenarcombos(L);
            ddlsitio.SelectedValue = Site;
            Padrinos(Site, Member);
            HistorialCarta();
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
                valoresiniciales(Member, Site);
            }
        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////

        protected void btncaracep_Click(object sender, EventArgs e)
        {
            dic = new Diccionario(L, Site);
            if (btncaracep.Text == dic.ModificarAPAD)
            {
                string padrino, cat, notas;
                padrino = lblVidpadrino.Text;
                cat = ddlcat.SelectedValue;
                notas = txtnotas.Text;

                MCarta(Site, padrino, Member, U, cat, notas, "M");
                HistorialCarta();

            }
            else
            {
                int count = 0;
                foreach (GridViewRow gvrow in GridView1.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("CheckBox1");
                    if (chk.Checked)
                    {
                        count = count + 1;
                    }
                }

                if ((count == 0))
                {
                    mst.mostrarMsjAdvNtf(dic.MsjNohaSeleccionadopadrino);
                }
                else
                {

                    GuardarCarta();
                    limpiartodo();
                    //  Page.ClientScript.RegisterStartupScript(this.GetType(), "yourMessage", "alert('" + dic.RegistroIngresadoAPAD + "');CallPrint('divprint');", true);
                    mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);
                    ClientScript.RegisterStartupScript(GetType(), "mostrar", "CallPrint('divprint');", true);

                }
            }
        }
        protected void gvcara_SelectedIndexChanged(object sender, EventArgs e)
        {
            dic = new Diccionario(L, Site);
            DataTable Carta = new DataTable();
            GridViewRow row = gvcarta.SelectedRow;
            int p = row.RowIndex;

            string categoria = gvcarta.Rows[p].Cells[1].Text;
            string escrita = gvcarta.Rows[p].Cells[2].Text;
            string padrinos = gvcarta.Rows[p].Cells[3].Text;
            Carta = APD.Carta(padrinos, Member, L, escrita);
            string cat = Carta.Rows[0]["Category"].ToString();
            string notes = Carta.Rows[0]["Notes"].ToString();
            string pad = Carta.Rows[0]["SponsorNames"].ToString();
            string esc = Carta.Rows[0]["DateTimeWritten"].ToString();
            string envio = Carta.Rows[0]["DateSent"].ToString();
            ddlcat.SelectedValue = cat;
            ddlcat.Enabled = false;
            string idpad = Carta.Rows[0]["SponsorId"].ToString();
            lbltitpadrino.Visible = true;
            lblVpadrino.Visible = true;
            lblVnota.Text = notes;
            txtnotas.Text = notes;
            GridView1.Visible = false;
            lbltitpadrino.Text = dic.padrinos + " :";
            lblVpadrino.Text = "(" + idpad + ") " + pad;
            btnimprimir.Visible = true;
            btneliminar.Visible = true;
            lblVfecha.Visible = true;
            lbltitFecha.Visible = true;
            btneliminar.Text = dic.EliminarAPAD;
            btnimprimir.Text = dic.ImprimirAPAD;
            btncaracep.Text = dic.ModificarAPAD;
            lbltitFecha.Text = dic.FechaAPAD + " :";
            lblVfecha.Text = esc;
            lblVidpadrino.Text = idpad;
        }
        protected void btncarcan_Click(object sender, EventArgs e)
        {
            limpiartodo();
        }
        protected void btneliminar_Click(object sender, EventArgs e)
        {
            accion = 2;
            mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
        }
        protected void btnimprimir_Click(object sender, EventArgs e)
        {
            try
            {
                accion = 1;
                mst.mostrarMsjOpcionesMdl("¿Desea Agrergar a split?");


            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
        }
        protected void btnbuscar_Click(object sender, EventArgs e)
        {
            string miembro = txtmiembro.Text;
            string sitio = ddlsitio.SelectedValue;
            VerificarApadrinamiento(miembro, sitio);
        }

        protected void btnimpval_Click(object sender, EventArgs e)
        {
            if (dtd.Rows.Count == 0)
            {
                mst.mostrarMsjAdvNtf("No se ha encontrado ningun split para imprimir");
            }
            else
            {
                foreach (DataRow dr in dtd.Rows)
                {
                    string sit = dr["Sitio"].ToString();
                    string miem = dr["Miembro"].ToString();
                    string pad = dr["Padrino"].ToString();
                    string idpad = dr["IdPadrino"].ToString();
                    string cate = dr["IdCategoria"].ToString();
                    string not = dr["Notas"].ToString();

                    

                    string sql = @"SELECT UserId fecha FROM dbo.MemberSponsorLetter WHERE RecordStatus=' ' AND Project='" + sit + "' AND MemberId='" + miem + "' AND SponsorId='" + idpad + "' AND Category='" + cate + "' AND Notes='" + not + "'";
                    string usuario = APD.obtienePalabra(sql, "fecha");

                    string ts = "SELECT FER.EmployeeId ts FROM Member M INNER JOIN Family F ON M.Project = F.Project AND M.LastFamilyId = F.FamilyId AND M.RecordStatus = F.RecordStatus AND M.RecordStatus = ' ' LEFT JOIN FamilyEmployeeRelation FER ON FER.Project = F.Project AND FER.FamilyId = F.FamilyId AND FER.RecordStatus = F.RecordStatus AND FER.EndDate IS NULL WHERE M.Project = '" + sit + "' AND M.MemberId = '" + miem + "'";
                    string nombrets = APD.obtienePalabra(ts, "ts");

                    string apadrinado = "SELECT FirstNames+' '+LastNames nombre  FROM dbo.Member WHERE RecordStatus=' ' AND Project='" + sit + "' AND MemberId='" + miem + "'";
                    string nombreapad = APD.obtienePalabra(apadrinado, "nombre");

                    string padrino3 = "SELECT (select case when SpeaksSpanish = 1 then 'Sí Habla Español' else 'No Habla Español' end) 'Español' FROM dbo.Sponsor WHERE RecordStatus=' ' AND SponsorId='" + idpad + "' ";
                    string Espanol = APD.obtienePalabra(padrino3, "Español");

                    string padrino2 = @"SELECT dbo.fn_GEN_FormatDate(DateTimeWritten,'" + L + "') escrita FROM dbo.MemberSponsorLetter WHERE RecordStatus=' ' AND Project='" + sit + "' AND MemberId='" + miem + "' AND Notes='" + not + "' AND Category='" + cate + "' AND UserId='" + usuario + "' AND SponsorId='" + idpad + "'";
                    string escrita = APD.obtienePalabra(padrino2, "escrita");


                    pnlimprimir.Controls.Add(new LiteralControl("----------------------------------------------------------------------" + escrita));
                    pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                    pnlimprimir.Controls.Add(new LiteralControl("No:  " + idpad));
                    pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                    pnlimprimir.Controls.Add(new LiteralControl(dic.nombrePadrino + ":  " + pad + "  (" + Espanol + ")"));
                    pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                    pnlimprimir.Controls.Add(new LiteralControl(dic.miembro + ": " + sit + miem + ""));
                    pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                    pnlimprimir.Controls.Add(new LiteralControl(dic.nombreMiembro + ":  " + nombreapad + ""));
                    pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                    pnlimprimir.Controls.Add(new LiteralControl(dic.trabajadorS + ":  " + nombrets + ""));
                    pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                    pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                    pnlimprimir.Controls.Add(new LiteralControl("<br />"));
                }
                ClientScript.RegisterStartupScript(GetType(), "mostrar", "CallPrint('divprint');", true);


            }

        }
    }
}
