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
    public partial class RegistroViveresAPAD : System.Web.UI.Page
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
            dic = new Diccionario(L, S);
            APD = new BDAPAD();
            if (!IsPostBack)
            {
                BDM = new BDMiembro();
                APD = new BDAPAD();
                dic = new Diccionario(L, S);
                try
                {
                    valoresiniciales();
                }
                catch (Exception ex)
                {
                }
            }

        }
        private void valoresiniciales()
        {
            llenaryTraducir();
            llenarhistorial();
        }
        private void llenarcombos()
        {
            string sql = "IF '" + L + "'='es' SELECT Code, DescSpanish descripcion FROM dbo.CdFamilyHelpReason ELSE SELECT Code, DescEnglish descripcion FROM dbo.CdFamilyHelpReason";
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlrazon.DataSource = datos;
            ddlrazon.DataValueField = "Code";
            ddlrazon.DataTextField = "descripcion";
            ddlrazon.DataBind();
            ddlrazon.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        }
        private void llenaryTraducir()
        {
            btnaceptar.Text = dic.AceptarAPAD;
            btncancelar.Text = dic.CancelarAPAD;
            lblrazon.Text = dic.RazonAPAD;
            lblnotas.Text = dic.notasAPAD;
            btnmodificar.Text = dic.ModificarAPAD;
            btneliminar.Text = dic.EliminarAPAD;
            btneliminar.Visible = false;
            btnmodificar.Visible = false;
            llenarcombos();
        }
        private void llenarhistorial()
        {
            DataTable tabledata = new DataTable();
            string sql = "SELECT CASE WHEN '" + L + "'='es' THEN FR.DescSpanish ELSE FR.DescEnglish END 'Razon',CONVERT(varchar, FH.DeliveryDateTime, 20) 'Fecha y Hora de Entrega',FH.Notes Notas FROM dbo.FamilyHelp FH INNER JOIN dbo.CdFamilyHelpReason FR ON FH.Reason=FR.Code WHERE RecordStatus=' ' AND Project='" + S + "' AND FamilyId='" + F + "' ORDER BY FH.DeliveryDateTime DESC";
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

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            ddlrazon.Enabled = true;
            ddlrazon.SelectedIndex = 0;
            txtnotas.Text = "";
            btneliminar.Visible = false;
            btnmodificar.Visible = false;
            btnaceptar.Visible = true;
            lblfecha.Text = "";
        }
        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            ddlrazon.Enabled = true;
            string fecha = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
            string notas = gvhistorial.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
            string sql = "SELECT Reason, Quantity FROM dbo.FamilyHelp WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND CONVERT(varchar, DeliveryDateTime, 20) = '" + fecha + "'";

            SqlDataAdapter daUser;
            DataTableReader adap;
            DataTable tableData = new DataTable();


            con.Open();
            daUser = new SqlDataAdapter(sql, ConnectionString);
            daUser.Fill(tableData);
            adap = new DataTableReader(tableData);
            con.Close();
            string razon = tableData.Rows[0]["Reason"].ToString();
            string cantidad = tableData.Rows[0]["Quantity"].ToString();
            ddlrazon.SelectedValue = razon;
            lblfecha.Text = fecha;
            txtnotas.Text = HttpUtility.HtmlDecode(notas.Replace("&nbsp;", "")); ;
            ddlrazon.Enabled = false;
            btnmodificar.Visible = true;
            btneliminar.Visible = true;
            btnaceptar.Visible = false;
        }

        protected void btnaceptar_Click(object sender, EventArgs e)
        {

            if (ddlrazon.SelectedIndex == 0)
            {
                mst.mostrarMsjNtf(dic.CampoVacioAPAD);
            }
            else
            {
                guardarviveres();
            }
        }

        private void guardarviveres()
        {
            string sql = "INSERT dbo.FamilyHelp VALUES('" + S + "', '" + F + "', '" + ddlrazon.SelectedValue + "', GETDATE(), GETDATE(), ' ', '" + U + "', NULL, '" + txtnotas.Text + "',NULL)";
            SqlCommand cmd = null;

            cmd = new SqlCommand(sql, con);

            try
            {
                dic = new Diccionario(L, S);
                con.Open();
                cmd.ExecuteNonQuery();
                llenarhistorial();
                limpiar();
                mst.mostrarMsjNtf(dic.RegistroIngresadoAPAD);

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
            finally
            {

                con.Close();
            }

        }

        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            string fechaEntrega, notas, razon;
            fechaEntrega = lblfecha.Text;
            notas = txtnotas.Text;
            razon = ddlrazon.SelectedValue;
            string sql = "INSERT INTO dbo.FamilyHelp SELECT Project, FamilyId, Reason, DeliveryDateTime, GETDATE() CreationDateTime, RecordStatus, '" + U + "' UserId, ExpirationDateTime, '" + notas + "' Notes, Quantity FROM dbo.FamilyHelp WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND Reason = '" + razon + "' AND CONVERT(varchar, DeliveryDateTime, 20) = '" + fechaEntrega + "'";
            SqlCommand cmd = null;

            cmd = new SqlCommand(sql, con);

            try
            {
                dic = new Diccionario(L, S);
                con.Open();
                cmd.ExecuteNonQuery();
                llenarhistorial();
                limpiar();
                mst.mostrarMsjNtf(dic.RegistroModificadoAPAD);

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
            finally
            {

                con.Close();
            }

        }

        protected void btneliminar_Click(object sender, EventArgs e)
        {
            string fechaEntrega, notas, razon;
            fechaEntrega = lblfecha.Text;
            notas = txtnotas.Text;
            razon = ddlrazon.SelectedValue;
            string sql = "INSERT INTO dbo.FamilyHelp SELECT Project, FamilyId, Reason, DeliveryDateTime, GETDATE() CreationDateTime,'H' RecordStatus, '" + U + "' UserId,GETDATE() ExpirationDateTime, Notes, Quantity FROM dbo.FamilyHelp WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND Reason = '" + razon + "' AND CONVERT(varchar, DeliveryDateTime, 20) = '" + fechaEntrega + "'";
            string actualizar = "UPDATE dbo.FamilyHelp SET RecordStatus='H', ExpirationDateTime=GETDATE() WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND Reason = '" + razon + "' AND CONVERT(varchar, DeliveryDateTime, 20) = '" + fechaEntrega + "'";

            SqlCommand cmd2 = null;

            cmd2 = new SqlCommand(actualizar, con);


            SqlCommand cmd = null;

            cmd = new SqlCommand(sql, con);

            try
            {
                dic = new Diccionario(L, S);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                llenarhistorial();
                limpiar();
                mst.mostrarMsjNtf(dic.RegistroEliminadoAPAD);
                // Response.Write(sql);

            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message);
            }
            finally
            {
                con.Close();
            }


        }

        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[1].Text = dic.RazonAPAD;
                e.Row.Cells[2].Text = dic.FechaEntregaAPAD;
                e.Row.Cells[3].Text = dic.notasAPAD;
            }
        }
    }
}