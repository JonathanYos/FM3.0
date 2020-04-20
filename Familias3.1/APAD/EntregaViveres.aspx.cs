using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.APAD
{
    public partial class EntregaViveres : System.Web.UI.Page
    {
        protected BDMiembro BDM;
        protected Diccionario dic;
        protected static BDFamilia BDF;
        protected static BDTS bdTS;
        protected static String S;
        protected static String L;
        protected static String F;
        protected static mast mst;
        protected static String M;
        protected static String U;
        protected static Boolean vista;
        protected static Color colorEntregado = Color.MediumSeaGreen;
        protected static Color colorPendiente = Color.White;
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected static Boolean hizoEntrega1;
        protected static Boolean hizoEntrega2;
        protected static Boolean hizoEntrega3;
        protected static Boolean hizoEntrega4;
        protected static Boolean haceEntrega1;
        protected static Boolean haceEntrega2;
        protected static Boolean haceEntrega3;
        protected static Boolean haceEntrega4;
        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            vista = mast.vista;
            mst = (mast)Master;
            mst.contentCallEvent += new EventHandler(modificar);
            dic = new Diccionario(L, S);

            if (!IsPostBack)
            {
                BDF = new BDFamilia();
                BDM = new BDMiembro();
                bdTS = new BDTS();
                dic = new Diccionario(L, S);
                try
                {
                    valoresiniciales();
                    DataTable dt = BDF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblVDirec.Text = rowF["Address"].ToString() + ", " + rowF["Area"].ToString();
                    lblVClasif.Text = rowF["Classification"].ToString();
                    lblVTS.Text = rowF["TS"].ToString();
                    lblVTelef.Text = rowF["Phone"].ToString();
                    if (vista)
                    {
                        cargarConSeguridad();
                    }
                }
                catch
                {
                }
            }

        }
        private void valoresiniciales()
        {
            llenaryTraducir();
            llenarhistorial();
        }
        private void cargarConSeguridad()
        {
            pnlRegistro.Visible = false;
            gdvHistorialViveres.Columns[5].Visible = false;
        }
        private void llenarCombo(DropDownList ddl, String sql)
        {
            SqlDataAdapter adapter = new SqlDataAdapter(sql, con);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddl.DataSource = datos;
            ddl.DataValueField = "Code";
            ddl.DataTextField = "Des";
            ddl.DataBind();
            ddl.Items.Insert(0, new ListItem(String.Empty, String.Empty));
        }
        protected void llenarComboNumeros(DropDownList ddl)
        {
            DataTable tblNumeros = new DataTable();
            tblNumeros.Columns.Add("Code");
            tblNumeros.Columns.Add("Des");
            tblNumeros.Rows.Add("");
            ListItem item;
            item = new ListItem("", "");
            ddl.Items.Add(item);
            for (int i = 1; i < 5; i++)
            {
                item = new ListItem(i + "", i + "");
                ddl.Items.Add(item);
            }
            ddl.SelectedValue = "";
        }
        private void llenaryTraducir()
        {
            lblTS.Text = dic.trabajadorS + ":";
            lblTelef.Text = dic.telefono + ":";
            lblDirec.Text = dic.direccion + ":";
            lblClasif.Text = dic.clasificacion + ":";

            btnmodificar.Text = dic.actualizar;
            btnCancelar.Text = dic.cancelar;
            pnlRegistro.Visible = false;
            lblNoTieneAF.Text = dic.noTiene;
            lblHistorialViveres.Text = dic.TShistorialViveres;

        }
        private void llenarhistorial()
        {
            DataTable tabledata = new DataTable();
            string sql = "SELECT FH.Reason, DeliveryDateTime1, DeliveryDateTime2, DeliveryDateTime3, DeliveryDateTime4, AuthorizedBy AS AutorizadoPor, dbo.fn_GEN_FormatDate(FH.DeliveryDateTime1,'" + L + "') AS FechaEntrega1, dbo.fn_GEN_FormatDate(FH.DeliveryDateTime2,'" + L + "') AS FechaEntrega2, dbo.fn_GEN_FormatDate(FH.DeliveryDateTime3,'" + L + "') AS FechaEntrega3, dbo.fn_GEN_FormatDate(FH.DeliveryDateTime4,'" + L + "') AS FechaEntrega4, DeliveredBy1, DeliveredBy2, DeliveredBy3, DeliveredBy4, CASE WHEN '" + L + "'='es' THEN cFHR.DescSpanish ELSE cFHR.DescEnglish END AS Razon, CASE WHEN FH.Quantity IS NOT NULL THEN FH.Quantity ELSE 0 END AS Cantidad, CASE WHEN '" + L + "'='es' THEN cFHF.DescSpanish ELSE cFHF.DescEnglish END AS Frecuencia, dbo.fn_GEN_FormatDate(FH.AuthorizationDateTime,'" + L + "') AS FechaAutorizacion, CONVERT(varchar, FH.AuthorizationDateTime, 21) AS AuthorizationDateTime, FH.Notes AS Notas, FH.UserId AS Usuario FROM dbo.FamilyHelp FH INNER JOIN dbo.CdFamilyHelpReason cFHR ON FH.Reason = cFHR.Code LEFT JOIN CdFamilyHelpFrecuency cFHF ON FH.Frecuency = cFHF.Code WHERE RecordStatus=' ' AND Project='" + S + "' AND FamilyId='" + F + "' ORDER BY FH.AuthorizationDateTime DESC";
            SqlConnection conexion = new SqlConnection(ConnectionString);
            conexion.Open();
            SqlDataAdapter adaptador = new SqlDataAdapter(sql, conexion);
            DataSet setDatos = new DataSet();
            adaptador.Fill(setDatos, "listado");
            tabledata = setDatos.Tables["listado"];
            conexion.Close();
            lblNoTieneAF.Visible = false;
            gdvHistorialViveres.Visible = false;
            if (tabledata.Rows.Count > 0)
            {
                gdvHistorialViveres.Visible = true;
                gdvHistorialViveres.Columns[0].Visible = true;
                gdvHistorialViveres.Columns[1].Visible = true;
                gdvHistorialViveres.Columns[2].Visible = true;
                gdvHistorialViveres.Columns[3].Visible = true;
                gdvHistorialViveres.Columns[4].Visible = true;
                gdvHistorialViveres.Columns[5].Visible = true;
                gdvHistorialViveres.DataSource = tabledata;
                gdvHistorialViveres.DataBind();
                gdvHistorialViveres.Columns[0].Visible = false;
                gdvHistorialViveres.Columns[1].Visible = false;
                gdvHistorialViveres.Columns[2].Visible = false;
                gdvHistorialViveres.Columns[3].Visible = false;
                gdvHistorialViveres.Columns[4].Visible = false;
                gdvHistorialViveres.Columns[5].Visible = false;
            }
            else
            {
                lblNoTieneAF.Visible = true;
            }

        }

        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gdvHistorialViveres.Columns[0].Visible = true;
            gdvHistorialViveres.Columns[1].Visible = true;
            gdvHistorialViveres.Columns[2].Visible = true;
            gdvHistorialViveres.Columns[3].Visible = true;
            gdvHistorialViveres.Columns[4].Visible = true;
            gdvHistorialViveres.Columns[5].Visible = true;
            if (e.CommandName == "cmdActualizar")
            {
                try
                {
                    pnlRegistro.Visible = true;
                    fechaAutorizacionSLCT = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                    razonSLCT = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                    String fechaEntrega1 = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[2].Text;
                    String fechaEntrega2 = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                    String fechaEntrega3 = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[4].Text;
                    String fechaEntrega4 = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[5].Text;
                    lblVFechaAutorizacion.Text = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[9].Text;
                    lblVRazon.Text = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[6].Text;
                    lblVFrecuencia.Text = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[8].Text;
                    lblVNotas.Text = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[11].Text;
                    fechaEntrega1 = HttpUtility.HtmlDecode(fechaEntrega1.Replace("&nbsp;", ""));
                    fechaEntrega2 = HttpUtility.HtmlDecode(fechaEntrega2.Replace("&nbsp;", ""));
                    fechaEntrega3 = HttpUtility.HtmlDecode(fechaEntrega3.Replace("&nbsp;", ""));
                    fechaEntrega4 = HttpUtility.HtmlDecode(fechaEntrega4.Replace("&nbsp;", ""));
                    hizoEntrega1 = !String.IsNullOrEmpty(fechaEntrega1) ? true : false;
                    hizoEntrega2 = !String.IsNullOrEmpty(fechaEntrega2) ? true : false;
                    hizoEntrega3 = !String.IsNullOrEmpty(fechaEntrega3) ? true : false;
                    hizoEntrega4 = !String.IsNullOrEmpty(fechaEntrega4) ? true : false;
                    String sql = "SELECT Quantity, Frecuency, Notes FROM dbo.FamilyHelp WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND Reason = '" + razonSLCT + "' AND CONVERT(varchar, AuthorizationDateTime, 21) = '" + fechaAutorizacionSLCT + "'";
                    SqlDataAdapter daUser;
                    DataTableReader adap;
                    DataTable tableData = new DataTable();
                    con.Open();
                    daUser = new SqlDataAdapter(sql, ConnectionString);
                    daUser.Fill(tableData);
                    adap = new DataTableReader(tableData);
                    con.Close();
                    cantidadSLCT = tableData.Rows[0]["Quantity"].ToString();
                    frecuenciaSLCT = tableData.Rows[0]["Frecuency"].ToString();
                    notasSLCT = tableData.Rows[0]["Notes"].ToString();
                    chkEntrega1.Checked = hizoEntrega1;
                    chkEntrega2.Checked = hizoEntrega2;
                    chkEntrega3.Checked = hizoEntrega3;
                    chkEntrega4.Checked = hizoEntrega4;
                    chkEntrega1.Visible = lblEntrega1.Visible = Int32.Parse(cantidadSLCT) >= 1 ? true : false;
                    chkEntrega2.Visible = lblEntrega2.Visible = Int32.Parse(cantidadSLCT) >= 2 ? true : false;
                    chkEntrega3.Visible = lblEntrega3.Visible = Int32.Parse(cantidadSLCT) >= 3 ? true : false;
                    chkEntrega4.Visible = lblEntrega4.Visible = Int32.Parse(cantidadSLCT) >= 4 ? true : false;
                    btnmodificar.Visible = true;
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
            gdvHistorialViveres.Columns[0].Visible = false;
            gdvHistorialViveres.Columns[1].Visible = false;
            gdvHistorialViveres.Columns[2].Visible = false;
            gdvHistorialViveres.Columns[3].Visible = false;
            gdvHistorialViveres.Columns[4].Visible = false;
            gdvHistorialViveres.Columns[5].Visible = false;
        }

        static String fechaAutorizacionSLCT;
        static String razonSLCT;
        static String frecuenciaSLCT;
        static String cantidadSLCT;
        static String notasSLCT;
        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            haceEntrega1 = chkEntrega1.Checked;
            haceEntrega2 = chkEntrega2.Checked;
            haceEntrega3 = chkEntrega3.Checked;
            haceEntrega4 = chkEntrega4.Checked;
            Boolean mostrarMensajeAdvertencia = (hizoEntrega1 && !haceEntrega1) || (hizoEntrega2 && !haceEntrega2) || (hizoEntrega3 && !haceEntrega3) || (hizoEntrega4 && !haceEntrega4) ? true : false;
            if (mostrarMensajeAdvertencia)
            {
                String mensajeAdvertencia = "<table><tr><td>¿Está seguro de eliminar los siguientes registros?</td></tr>";
                mensajeAdvertencia = hizoEntrega1 && !haceEntrega1 ? mensajeAdvertencia + "<tr><td>Primera Entrega. </td></tr>" : mensajeAdvertencia;
                mensajeAdvertencia = hizoEntrega2 && !haceEntrega2 ? mensajeAdvertencia + "<tr><td>Segunda Entrega. </td></tr>" : mensajeAdvertencia;
                mensajeAdvertencia = hizoEntrega3 && !haceEntrega3 ? mensajeAdvertencia + "<tr><td>Tercera Entrega. </td></tr>" : mensajeAdvertencia;
                mensajeAdvertencia = hizoEntrega4 && !haceEntrega4 ? mensajeAdvertencia + "<tr><td>Cuarta Entrega. </td></tr>" : mensajeAdvertencia;
                mensajeAdvertencia = mensajeAdvertencia + "</table>";
                mst.mostrarMsjOpcionesMdl(mensajeAdvertencia);
            }
            else
            {
                modificar();
            }
        }
        protected void btnCancelar_Click(object sender, EventArgs e)
        {
            pnlRegistro.Visible = false;
        }
        protected void modificar()
        {
            string sql = "INSERT INTO dbo.FamilyHelp (Project, FamilyId, Reason, AuthorizationDateTime, CreationDateTime, RecordStatus, UserId, ExpirationDateTime, Notes, Quantity, Frecuency, AuthorizedBy, DeliveryDateTime1, DeliveredBy1, DeliveryDateTime2, DeliveredBy2, DeliveryDateTime3, DeliveredBy3, DeliveryDateTime4, DeliveredBy4) SELECT Project, FamilyId, Reason, AuthorizationDateTime, CONVERT(varchar, GETDATE(), 20) CreationDateTime, RecordStatus, '" + U + "' UserId, ExpirationDateTime, '" + notasSLCT + "' Notes, '" + cantidadSLCT + "' Quantity, '" + frecuenciaSLCT + "' Frecuency, AuthorizedBy, " + (haceEntrega1 ? "CONVERT(varchar, GETDATE(), 20)" : "NULL") + ", " + (haceEntrega1 ? "'" + U + "'" : "NULL") + ",  " + (haceEntrega2 ? "CONVERT(varchar, GETDATE(), 20)" : "NULL") + ", " + (haceEntrega2 ? "'" + U + "'" : "NULL") + ",  " + (haceEntrega3 ? "CONVERT(varchar, GETDATE(), 20)" : "NULL") + ", " + (haceEntrega3 ? "'" + U + "'" : "NULL") + ",  " + (haceEntrega4 ? "CONVERT(varchar, GETDATE(), 20)" : "NULL") + ", " + (haceEntrega4 ? "'" + U + "'" : "NULL") + " FROM dbo.FamilyHelp WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND Reason = '" + razonSLCT + "' AND CONVERT(varchar, AuthorizationDateTime, 21) = '" + fechaAutorizacionSLCT + "'";
            string actualizar = "UPDATE dbo.FamilyHelp SET RecordStatus='H', ExpirationDateTime = CONVERT(varchar, GETDATE(),20) WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND Reason = '" + razonSLCT + "' AND CONVERT(varchar, AuthorizationDateTime, 21) = '" + fechaAutorizacionSLCT + "' AND CONVERT(varchar, CreationDateTime, 21) != (SELECT MAX(CONVERT(varchar, FH2.CreationDateTime, 21)) FROM FamilyHelp FH2 WHERE FH2.RecordStatus = ' ' AND FH2.Project = '" + S + "' AND FH2.FamilyId = " + F + " AND Reason = '" + razonSLCT + "' AND CONVERT(varchar, AuthorizationDateTime, 21) = '" + fechaAutorizacionSLCT + "')";
            SqlCommand cmd = null;
            cmd = new SqlCommand(sql, con);
            SqlCommand cmd2 = null;
            cmd2 = new SqlCommand(actualizar, con);
            try
            {
                dic = new Diccionario(L, S);
                con.Open();
                cmd.ExecuteNonQuery();
                cmd2.ExecuteNonQuery();
                llenarhistorial();
                mst.mostrarMsjNtf(dic.msjSeHaActualizado);

            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
            finally
            {

                con.Close();
                pnlRegistro.Visible = false;
            }
        }

        protected void modificar(object sender, EventArgs e)
        {
            modificar();
        }

        protected void gvhistorial_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.Header)
            {
                e.Row.Cells[6].Text = dic.razon;
                e.Row.Cells[7].Text = dic.cantidad;
                e.Row.Cells[8].Text = dic.frecuencia;
                e.Row.Cells[9].Text = dic.fechaAutorizacion;
                e.Row.Cells[10].Text = dic.entregas;
                e.Row.Cells[11].Text = dic.nota;
                e.Row.Cells[12].Text = dic.autorizadoPor;
                e.Row.Cells[13].Text = dic.usuario;
                e.Row.Cells[14].Text = dic.accion;
            }
        }
    }
}