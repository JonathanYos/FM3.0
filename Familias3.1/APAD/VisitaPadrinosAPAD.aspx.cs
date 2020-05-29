using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.Apadrinamiento
{
    public partial class VisitaPadrinosAPAD : System.Web.UI.Page
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
        protected static String P;
        protected static int numAccion;
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);





        //////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--////////////////////////////////////////////
        protected void accionar(object sender, EventArgs e)
        {

            switch (numAccion)
            {
                case 1:
                    mst.redir("BusquedaPadrinosAPAD.aspx");
                    break;
                case 2:
                    try
                    {


                        string nombre = HttpUtility.HtmlDecode(lblVnombre.Text.Replace(dic.nombre + " : ", ""));
                        string sql = "SELECT MemberId miembro FROM dbo.Member WHERE RecordStatus=' ' AND FirstNames+' '+LastNames =@nombre";
                        DataTable tabledata = new DataTable();
                        SqlConnection conexion = new SqlConnection(ConnectionString);
                        conexion.Open();
                        SqlCommand comando = new SqlCommand(sql, conexion);
                        comando.Parameters.AddWithValue("@nombre", nombre);
                        SqlDataAdapter adaptador = new SqlDataAdapter();
                        adaptador.SelectCommand = comando;

                        adaptador.Fill(tabledata);
                        conexion.Close();

                        string conteo = tabledata.Rows[0]["miembro"].ToString();

                        string sql2 = "INSERT dbo.SponsorMemberVisit SELECT Project,SponsorId,MemberId,VisitDateTime,GETDATE() CreationDateTime,'H' RecordStatus,'" + U + "' UserId,GETDATE() ExpirationDateTime, Notes FROM dbo.SponsorMemberVisit WHERE RecordStatus=' ' AND SponsorId='" + P + "' AND MemberId='" + conteo + "' AND CONVERT(VARCHAR,VisitDateTime,120)='" + txtfechav.Text + "' AND Notes='" + lblvalornotas.Text + "'";
                        string actualizar = "UPDATE dbo.SponsorMemberVisit SET RecordStatus='H', ExpirationDateTime=GETDATE() WHERE RecordStatus=' ' AND SponsorId='" + P + "' AND MemberId='" + conteo + "' AND CONVERT(VARCHAR,VisitDateTime,120)='" + txtfechav.Text + "' AND Notes='" + lblvalornotas.Text + "'";

                        SqlCommand cmd3 = null;
                        cmd3 = new SqlCommand(actualizar, con);
                        SqlCommand cmd4 = null;
                        cmd4 = new SqlCommand(sql2, con);
                        con.Open();
                        int fil = cmd4.ExecuteNonQuery();
                        int fil2 = cmd3.ExecuteNonQuery();
                        if (fil < 0) { mst.mostrarMsjAdvNtf("errorsql2"); }
                        if (fil2 < 0) { mst.mostrarMsjAdvNtf("erroractualizar"); }
                        limpiarCampos();
                        llenarhistorial();
                        traducir();
                        mst.mostrarMsjNtf(dic.msjSeHaEliminado);
                    }
                    catch (Exception ex)
                    {
                        mst.mostrarMsjAdvNtf(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                    }
                    break;
            }
        }
        private void apadrinado()
        {
            string sql2 = "SELECT CASE WHEN '" + L + "'='es' THEN O.DescSpanish ELSE O.DescEnglish END AS Sitio, SMR.MemberId,M.FirstNames+' '+M.LastNames 'Nombre' FROM dbo.SponsorMemberRelation SMR INNER JOIN dbo.Member M ON SMR.MemberId=M.MemberId AND SMR.RecordStatus=M.RecordStatus AND SMR.Project=M.Project INNER JOIN dbo.CdSponsorMemberRelationType SMT ON SMR.Type=SMT.Code INNER JOIN dbo.FwCdOrganization O ON O.Code=SMR.Project WHERE SMR.RecordStatus=' ' AND SMR.SponsorId='" + P + "' AND SMR.EndDate IS NULL ";
            DataTable tabledata2;
            SqlConnection conexion2 = new SqlConnection(ConnectionString);
            conexion2.Open();
            SqlDataAdapter adaptador2 = new SqlDataAdapter(sql2, conexion2);
            DataSet setDatos2 = new DataSet();
            adaptador2.Fill(setDatos2, "listado");
            tabledata2 = setDatos2.Tables["listado"];
            conexion2.Close();
            gvapadrinado.DataSource = tabledata2;
            gvapadrinado.DataBind();
        }
        private void GuardarRegitro()
        {
            string miembro, notas, notasv;
            APD = new BDAPAD();
            foreach (GridViewRow gvrow in gvapadrinado.Rows)
            {
                miembro = gvrow.Cells[2].Text;
                CheckBox chk = (CheckBox)gvrow.FindControl("cbagregar");
                if (chk.Checked)
                {
                    CheckBox chv = (CheckBox)gvrow.FindControl("cbagregarv");
                    TextBox txt2 = (TextBox)gvrow.FindControl("txtnviveres");
                    string sitio;
                    sitio = gvrow.Cells[3].Text;
                    string sitio2 = APD.obtienePalabra("SELECT Code FROM dbo.FwCdOrganization WHERE DescSpanish='" + miembro + "' OR DescEnglish='" + miembro + "'", "Code");
                    if (chv.Checked)
                    {
                        string familia = "SELECT LastFamilyId FROM dbo.Member WHERE RecordStatus=' ' AND MemberId='" + sitio + "' AND Project='" + sitio2 + "'";
                        int idfamilia = APD.ObtenerEntero(familia, "LastFamilyId");
                        string sql2 = "INSERT dbo.FamilyHelp VALUES('" + sitio2 + "', '" + idfamilia + "', 'VIPA', GETDATE(), GETDATE(), ' ', '" + U + "', NULL,NULL, '"+txt2.Text+ "',NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL,NULL)";
                        APD.ejecutarSQL(sql2);
                    }
                    TextBox txt = (TextBox)gvrow.FindControl("txtnvisita");
                    notas = txt.Text;
                    
                   
                    try
                    {
                        string sql = "INSERT dbo.SponsorMemberVisit VALUES('" + sitio2 + "','" + P + "','" + sitio + "','" + txtfechav.Text + "',GETDATE(),' ','" + U + "',NULL,'" + txt.Text + "')";

                        SqlCommand cmd = null;
                        cmd = new SqlCommand(sql, con);
                        con.Open();
                        cmd.ExecuteNonQuery();
                        llenarhistorial();
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
            }
        }
        private void limpiarCampos()
        {
            foreach (GridViewRow gvrow in gvapadrinado.Rows)
            {
                CheckBox chk = (CheckBox)gvrow.FindControl("cbagregar");
                TextBox txt = (TextBox)gvrow.FindControl("txtnvisita");
                chk.Checked = true;
                txt.Text = "";
            }
            txtfechav.Text = "";
            gvapadrinado.Visible = true;
            Button3.Visible = true;
        }
        private void llenarhistorial()
        {
            DataTable tabledata = new DataTable();
            string sql = "select CONVERT(VARCHAR,SMV.VisitDateTime,120) 'Fecha de Visita', M.FirstNames+' '+M.LastNames 'Nombres', SMV.Notes Notas FROM dbo.SponsorMemberVisit SMV INNER JOIN dbo.Member M ON SMV.MemberId = M.MemberId AND SMV.Project = M.Project AND SMV.RecordStatus=M.RecordStatus WHERE  SMV.RecordStatus=' ' AND SMV.SponsorId='" + P + "'  AND M.RecordStatus=' ' ORDER BY SMV.VisitDateTime DESC";
            SqlConnection conexion = new SqlConnection(ConnectionString);
            conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataSet setDatos = new DataSet();
            adaptador.Fill(setDatos, "listado");
            tabledata = setDatos.Tables["listado"];
            conexion.Close();
            gvhistorial.DataSource = tabledata;
            gvhistorial.DataBind();
        }
        private void traducir()
        {
            btnnuevab.Text = dic.nuevaBusqueda;
            lblfechav.Text = dic.FechaVisitaAPAD;
            txtfechav.Enabled = true;
            lblVnombre.Visible = false;
            lblnotas.Visible = false;
            txtmnotas.Visible = false;
            btneliminar.Visible = false;
            btnmodificar.Visible = false;
            btneliminar.Text = dic.EliminarAPAD;
            btnmodificar.Text = dic.ModificarAPAD;
            lblvalornotas.Visible = false;
            Button3.Text = dic.AceptarAPAD;
            Button4.Text = dic.CancelarAPAD;
            revtxtfecha.ErrorMessage = dic.msjFechaVaciaAPAD;
        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////
        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            P = mast.P;
            dic = new Diccionario(L, S);
            P = (String)Session["P"];
            mst = (mast)Master;
            APD = new BDAPAD();
            mst.contentCallEvent += new EventHandler(accionar);
            if (String.IsNullOrEmpty(P))
            {
                string notiene;
                if (L == "es")
                {
                    notiene = "No has seleccionado ningún padrino";
                }
                else
                {
                    notiene = "You have not selected any sponsors";
                }
                numAccion = 1;
                mst.mostrarMsjOpcionMdl(notiene);
            }
            else
            {
                if (!IsPostBack)
                {
                    try
                    {
                        BDM = new BDMiembro();
                        APD = new BDAPAD();
                        traducir();
                        llenarhistorial();
                        apadrinado();
                    }
                    catch (Exception ex)
                    {
                        mst.mostrarMsjAdvNtf("Error: " + ex.Message);
                    }
                }
            }
        }
        protected void Button3_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrEmpty(txtfechav.Text) || string.IsNullOrWhiteSpace(txtfechav.Text))
            {
                mst.mostrarMsjAdvNtf(dic.msjFechaVaciaAPAD);
            }
            else
            {
                int conteoa = 0;
                foreach (GridViewRow gvrow in gvapadrinado.Rows)
                {
                    CheckBox chk = (CheckBox)gvrow.FindControl("cbagregar");
                    if (chk.Checked)
                    {
                        conteoa = conteoa + 1;
                    }
                }
                if (conteoa == 0)
                {
                    mst.mostrarMsjAdvNtf(dic.msjNoEncontroMiembro);
                }
                else
                {
                    string diferencia = "SELECT DATEDIFF(day,'" + txtfechav.Text + "',CONVERT(VARCHAR,GETDATE(),120)) residuo";
                    SqlDataAdapter daUser;
                    DataTableReader adap;
                    DataTable tableData = new DataTable();
                    int residuo;

                    try
                    {
                        con.Open();
                        daUser = new SqlDataAdapter(diferencia, ConnectionString);
                        daUser.Fill(tableData);
                        adap = new DataTableReader(tableData);
                        con.Close();
                        DataRow row = tableData.Rows[0];
                        residuo = Convert.ToInt32(row["residuo"]);

                    }
                    catch (Exception ex)
                    {
                        mst.mostrarMsjAdvNtf(ex.Message);
                        residuo = 5;
                    }

                    if (residuo > 8 || residuo < 0)
                    {
                        mst.mostrarMsjAdvNtf(dic.MsjLimitacion8dias);
                    }
                    else
                    {
                        GuardarRegitro();
                        llenarhistorial();
                        limpiarCampos();
                        mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);
                    }
                }
            }
        }
        protected void Button4_Click(object sender, EventArgs e)
        {
            limpiarCampos();
            llenarhistorial();
            traducir();
        }

        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            string miembro = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
            string fecha = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
            string notas = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            txtfechav.Text = fecha;
            txtfechav.Enabled = false;
            gvapadrinado.Visible = false;
            lblnotas.Visible = true;
            lblVnombre.Visible = true;
            lblVnombre.Text = dic.nombre + " : " + miembro;
            lblnotas.Text = dic.notasAPAD;
            txtmnotas.Text = HttpUtility.HtmlDecode(notas);
            txtmnotas.Visible = true;
            btnmodificar.Visible = true;
            btneliminar.Visible = true;
            lblvalornotas.Text = HttpUtility.HtmlDecode(notas);
            Button3.Visible = false;
        }

        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            string nombre = HttpUtility.HtmlDecode(lblVnombre.Text.Replace(dic.nombre + " : ", ""));
            string sql = "SELECT MemberId miembro FROM dbo.Member WHERE RecordStatus=' ' AND FirstNames+' '+LastNames =@nombre";
            DataTable tabledata = new DataTable();
            SqlConnection conexion = new SqlConnection(ConnectionString);
            conexion.Open();
            SqlCommand comando = new SqlCommand(sql, conexion);
            comando.Parameters.AddWithValue("@nombre", nombre);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;

            adaptador.Fill(tabledata);
            conexion.Close();

            string conteo = tabledata.Rows[0]["miembro"].ToString();

            string consulta = "INSERT dbo.SponsorMemberVisit SELECT Project,SponsorId,MemberId,VisitDateTime,GETDATE() CreationDateTime,RecordStatus,'" + U + "' UserId,ExpirationDateTime,'" + txtmnotas.Text + "' Notes FROM dbo.SponsorMemberVisit WHERE RecordStatus=' ' AND SponsorId='" + P + "' AND MemberId='" + conteo + "' AND CONVERT(VARCHAR,VisitDateTime,120)='" + txtfechav.Text + "' AND Notes='" + lblvalornotas.Text + "'";

            SqlCommand cmd = null;

            cmd = new SqlCommand(consulta, con);

            try
            {
                con.Open();
                cmd.ExecuteNonQuery();//  llenarhistorial();                
                limpiarCampos();
                llenarhistorial();
                traducir();
                mst.mostrarMsjNtf(dic.RegistroModificadoAPAD);
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

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            numAccion = 2;
            mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
        }

        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = dic.FechaVisitaAPAD;
                e.Row.Cells[1].Text = dic.nombre;
                e.Row.Cells[2].Text = dic.notas;
            }
        }

        protected void btnnuevab_Click(object sender, EventArgs e)
        {
            Response.Redirect("BusquedaPadrinosAPAD.aspx");
        }

        protected void gvapadrinado_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            string addvisita, addviveres,notasvisita,nviveres;
            addvisita = L == "es" ?"Agregar Visita":"Add Visit";
            addviveres = L == "es" ? "Agregar Viveres" : "Add Family Help";
            notasvisita = L == "es" ? "Notas de Visita" : "Visit Notes";
            nviveres = L == "es" ? "Notas de Viveres" : "Family Help Notes";
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[0].Text = addvisita;
                e.Row.Cells[1].Text = addviveres;
                e.Row.Cells[2].Text = dic.sitio;
                e.Row.Cells[3].Text = dic.miembro;
                e.Row.Cells[4].Text = dic.nombre;
                e.Row.Cells[5].Text = notasvisita;
                e.Row.Cells[6].Text = nviveres;
            }
        }
    }
}