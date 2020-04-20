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

namespace Familias3._1.TS
{
    public partial class Viveres : System.Web.UI.Page
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
        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            vista = mast.vista;
            mst = (mast)Master;
            mst.contentCallEvent += new EventHandler(eliminarAyuda);
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
            btnaceptar.Text = dic.TSingresarVivere;
            btncancelar.Text = dic.TSnuevoVivere;
            lblrazon.Text = "*" + dic.razon + ":";
            lblCantidad.Text = "*" + dic.cantidad + ":";
            lblFrecuencia.Text = "*" + dic.frecuencia + ":";
            lblnotas.Text = "&nbsp;" + dic.nota + ":";
            txtnotas.MaxLength = 40;
            btnmodificar.Text = dic.actualizar;
            btnmodificar.Visible = false;
            lblNoTieneAF.Text = dic.noTiene;
            lblHistorialViveres.Text = dic.TShistorialViveres;
            revDdlrazon.ErrorMessage = dic.msjCampoNecesario;
            revDdlCantidad.ErrorMessage = dic.msjCampoNecesario;
            revDdlFrecuencia.ErrorMessage = dic.msjCampoNecesario;
            llenarCombo(ddlrazon, "SELECT Code, CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des FROM CdFamilyHelpReason ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            llenarCombo(ddlFrecuencia, "SELECT Code, CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des FROM CdFamilyHelpFrecuency WHERE Active = 1 ORDER BY CASE WHEN '" + L + "' = 'es' THEN DescSpanish ELSE DescEnglish END");
            llenarComboNumeros(ddlCantidad);
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
                gdvHistorialViveres.DataSource = tabledata;
                gdvHistorialViveres.DataBind();
                gdvHistorialViveres.Columns[0].Visible = false;
                gdvHistorialViveres.Columns[1].Visible = false;
            }
            else
            {
                lblNoTieneAF.Visible = true;
            }

        }

        protected void btncancelar_Click(object sender, EventArgs e)
        {
            limpiar();
        }
        private void limpiar()
        {
            ddlrazon.Enabled = true;
            ddlrazon.SelectedIndex = 0;
            ddlrazon.SelectedValue = "";
            ddlCantidad.SelectedValue = "";
            ddlFrecuencia.SelectedValue = "";
            txtnotas.Text = "";
            btnmodificar.Visible = false;
            btncancelar.Visible = false;
            ddlrazon.Visible = true;
            btnaceptar.Visible = true;
            lblVRazon.Visible = false;
            lblrazon.Text = "*" + dic.razon + ":";
        }
        protected void gvhistorial_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            gdvHistorialViveres.Columns[0].Visible = true;
            gdvHistorialViveres.Columns[1].Visible = true;
            if (e.CommandName == "cmdActualizar")
            {
                try
                {
                    btncancelar.Visible = true;
                    ddlrazon.Enabled = true;
                    lblVRazon.Visible = true;
                    fechaAutorizacionSLCT = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                    razonSLCT = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                    String notas;
                    String cantidad;
                    String frecuencia;
                    String sql = "SELECT Quantity, Frecuency, Notes FROM dbo.FamilyHelp WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND Reason = '" + razonSLCT + "' AND CONVERT(varchar, AuthorizationDateTime, 21) = '" + fechaAutorizacionSLCT + "'";
                    SqlDataAdapter daUser;
                    DataTableReader adap;
                    DataTable tableData = new DataTable();


                    con.Open();
                    daUser = new SqlDataAdapter(sql, ConnectionString);
                    daUser.Fill(tableData);
                    adap = new DataTableReader(tableData);
                    con.Close();
                    cantidad = tableData.Rows[0]["Quantity"].ToString();
                    frecuencia = tableData.Rows[0]["Frecuency"].ToString();
                    notas = tableData.Rows[0]["Notes"].ToString();
                    ddlrazon.SelectedValue = razonSLCT;
                    ddlCantidad.SelectedValue = cantidad;
                    ddlFrecuencia.SelectedValue = frecuencia;
                    lblVRazon.Text = "&nbsp;&nbsp;" + ddlrazon.SelectedItem;
                    txtnotas.Text = notas;
                    ddlrazon.Enabled = false;
                    btnmodificar.Visible = true;
                    btnaceptar.Visible = false;
                    //ddlrazon.ValidationGroup = "grpActualizar";
                    ddlrazon.Visible = false;
                    lblrazon.Text = "&nbsp;" + dic.razon + ":";
                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
            }
            else
                if (e.CommandName == "cmdEliminar")
                {
                    try
                    {
                        ddlrazon.Enabled = true;
                        fechaAutorizacionSLCT = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[0].Text;
                        razonSLCT = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[1].Text;
                        //string notas = gdvHistorialViveres.Rows[Convert.ToInt32(e.CommandArgument)].Cells[3].Text;
                        //string sql = "SELECT Reason, Quantity FROM dbo.FamilyHelp WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND CONVERT(varchar, AuthorizationDateTime, 21) = '" + fecha + "'";

                        //SqlDataAdapter daUser;
                        //DataTableReader adap;
                        //DataTable tableData = new DataTable();


                        //con.Open();
                        //daUser = new SqlDataAdapter(sql, ConnectionString);
                        //daUser.Fill(tableData);
                        //adap = new DataTableReader(tableData);
                        //con.Close();
                        //string razon = tableData.Rows[0]["Reason"].ToString();
                        //string cantidad = tableData.Rows[0]["Quantity"].ToString();
                        //ddlrazon.SelectedValue = razon;
                        //txtnotas.Text = HttpUtility.HtmlDecode(notas.Replace("&nbsp;", "")); ;
                        //ddlrazon.Enabled = false;
                        //btnmodificar.Visible = true;
                        //btnaceptar.Visible = false;
                        mst.mostrarMsjOpcionesMdl(dic.msjEliminarRegistro);
                    }
                    catch (Exception ex)
                    {
                        mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                    }
                }
            gdvHistorialViveres.Columns[0].Visible = false;
            gdvHistorialViveres.Columns[1].Visible = false;
        }

        protected void btnaceptar_Click(object sender, EventArgs e)
        {
            guardarviveres();
        }

        private void guardarviveres()
        {
            String razon = ddlrazon.SelectedValue;
            String cantidad = ddlCantidad.SelectedValue;
            String frecuencia = ddlFrecuencia.SelectedValue;
            DateTime hoy = DateTime.Now;
            if (bdTS.vvrVerificarIngreso(S, F, razon, hoy.ToString("yyyy-MM-dd HH:mm:ss")))
            {
                string sql = "INSERT dbo.FamilyHelp (Project, FamilyId, Reason, AuthorizationDateTime, CreationDateTime, RecordStatus, UserId, ExpirationDateTime, Notes, Quantity, Frecuency, AuthorizedBy) VALUES('" + S + "', '" + F + "', '" + razon + "', '" + hoy.ToString("yyyy-MM-dd HH:mm:ss") + "', '" + hoy.ToString("yyyy-MM-dd HH:mm:ss") + "', ' ', '" + U + "', NULL, '" + txtnotas.Text + "', '" + cantidad + "', '" + frecuencia + "', '" + U + "')";
                SqlCommand cmd = null;

                cmd = new SqlCommand(sql, con);

                try
                {
                    dic = new Diccionario(L, S);
                    con.Open();
                    cmd.ExecuteNonQuery();
                    llenarhistorial();
                    limpiar();
                    mst.mostrarMsjNtf(dic.msjSeHaIngresado);

                }
                catch (Exception ex)
                {
                    mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
                }
                finally
                {

                    con.Close();
                }
            }
            else
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("Una familia solo puede recibir víveres por cierta razón, por día.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("A family can only receive helps for a certain reason, per day.");
                }
            }
        }
        static String fechaAutorizacionSLCT;
        static String razonSLCT;
        protected void btnmodificar_Click(object sender, EventArgs e)
        {
            String notas = txtnotas.Text;
            String cantidad = ddlCantidad.SelectedValue;
            String frecuencia = ddlFrecuencia.SelectedValue;
            string sql = "INSERT INTO dbo.FamilyHelp (Project, FamilyId, Reason, AuthorizationDateTime, CreationDateTime, RecordStatus, UserId, ExpirationDateTime, Notes, Quantity, Frecuency, AuthorizedBy, DeliveryDateTime1, DeliveredBy1, DeliveryDateTime2, DeliveredBy2, DeliveryDateTime3, DeliveredBy3, DeliveryDateTime4, DeliveredBy4) SELECT Project, FamilyId, Reason, AuthorizationDateTime, CONVERT(varchar, GETDATE(), 20) CreationDateTime, RecordStatus, '" + U + "' UserId, ExpirationDateTime, '" + notas + "' Notes, '" + cantidad + "' Quantity, '" + frecuencia + "' Frecuency,'" + U + "', DeliveryDateTime1, DeliveredBy1, DeliveryDateTime2, DeliveredBy2, DeliveryDateTime3, DeliveredBy3,DeliveryDateTime4, DeliveredBy4 FROM dbo.FamilyHelp WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND Reason = '" + razonSLCT + "' AND CONVERT(varchar, AuthorizationDateTime, 21) = '" + fechaAutorizacionSLCT + "'";
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
                limpiar();
                mst.mostrarMsjNtf(dic.msjSeHaActualizado);

            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
            finally
            {

                con.Close();
            }

        }




        protected void eliminarAyuda(object sender, EventArgs e)
        {
            string sql = "INSERT INTO dbo.FamilyHelp (Project, FamilyId, Reason, AuthorizationDateTime, CreationDateTime, RecordStatus, UserId, ExpirationDateTime, Notes, Quantity, Frecuency, AuthorizedBy, DeliveryDateTime1, DeliveredBy1, DeliveryDateTime2, DeliveredBy2, DeliveryDateTime3, DeliveredBy3, DeliveryDateTime4, DeliveredBy4) SELECT Project, FamilyId, Reason, AuthorizationDateTime, CONVERT(varchar, GETDATE(), 20) AS CreationDateTime,'H' RecordStatus, '" + U + "' UserId,GETDATE() ExpirationDateTime, Notes, Quantity, Frecuency, AuthorizedBy, DeliveryDateTime1, DeliveredBy1, DeliveryDateTime2, DeliveredBy2, DeliveryDateTime3, DeliveredBy3, DeliveryDateTime4, DeliveredBy4 FROM dbo.FamilyHelp WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND Reason = '" + razonSLCT + "' AND CONVERT(varchar, AuthorizationDateTime, 21) = '" + fechaAutorizacionSLCT + "'";
            string actualizar = "UPDATE dbo.FamilyHelp SET RecordStatus='H', ExpirationDateTime = GETDATE() WHERE RecordStatus = ' ' AND Project = '" + S + "' AND FamilyId = '" + F + "' AND Reason = '" + razonSLCT + "' AND CONVERT(varchar, AuthorizationDateTime, 21) = '" + fechaAutorizacionSLCT + "'";
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
                mst.mostrarMsjNtf(dic.msjSeHaEliminado);
            }
            catch (Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
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
                e.Row.Cells[2].Text = dic.razon;
                e.Row.Cells[3].Text = dic.cantidad;
                e.Row.Cells[4].Text = dic.frecuencia;
                e.Row.Cells[5].Text = dic.fechaAutorizacion;
                e.Row.Cells[6].Text = dic.entregas;
                e.Row.Cells[7].Text = dic.nota;
                e.Row.Cells[8].Text = dic.autorizadoPor;
                e.Row.Cells[9].Text = dic.usuario;
                e.Row.Cells[10].Text = dic.acciones;
            }
        }
    }
}