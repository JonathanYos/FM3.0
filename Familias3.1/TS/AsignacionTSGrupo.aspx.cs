using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.TS
{
    public partial class AsignarTSGrupo : System.Web.UI.Page
    {
        public static BDTS bdTS;
        public static BDGEN bdGEN;
        public static BDFamilia BDF;
        public static String U;
        public static String F;
        public static String S;
        public static String M;
        public static String L;
        protected static mast mst;
        protected static Diccionario dic;
        protected static String area = "";
        protected static String TS = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            iniciarElementos();
        }

        protected void iniciarElementos()
        {
            mst = (mast)Master;
            if (!IsPostBack)
            {
                bdTS = new BDTS();
                bdGEN = new BDGEN();
                BDF = new BDFamilia();
                F = mast.F;
                S = mast.S;
                U = mast.U;
                L = mast.L;
                dic = new Diccionario(L, S);
                llenarElementos();
                if (Session["AsignoTSGrupo"] != null)
                {
                    String asigno = (String)Session["AsignoTSGrupo"];
                    if (asigno.Equals("SI"))
                    {
                        mst.mostrarMsjNtf(dic.msjSeHaActualizado);
                    }
                    else
                    {
                        mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + asigno + ".");
                    }
                    Session["AsignoTSGrupo"] = null;
                }
            }
        }

        protected void llenarElementos()
        {
            llenarFormAsignarTS();
            llenarFormBuscar();
        }
        protected void llenarFormAsignarTS()
        {
            revDdlNuevoTS.ErrorMessage = dic.msjCampoNecesario;
            lblTS.Text = dic.trabajadorS + ":";
            lblArea.Text = dic.area + ":";
            btnBuscar.Text = dic.buscar;
            llenarTSs();
            llenarAreas();
        }
        protected void llenarAreas()
        {
            ddlArea.Items.Clear();
            ddlArea.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerAreas(S, L);
            String codigo = "";
            String area = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                codigo = row["Code"].ToString();
                area = row["Des"].ToString();
                item = new ListItem(area, codigo);
                ddlArea.Items.Add(item);
            }
        }
        protected void llenarFormBuscar()
        {
            lblNuevoTS.Text = "*" + dic.nuevoTS + ":";
            btnAsignar.Text = dic.asignar;
            btnNuevaBusqueda.Text = dic.nuevaBusqueda;
            llenarNuevoTSs();
        }
        protected void llenarTSs()
        {
            ddlTS.Items.Clear();
            ddlTS.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerTS2(S);
            String codigo = "";
            String empleado = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                codigo = row["EmployeeId"].ToString();
                empleado = row["EmployeeId"].ToString();
                item = new ListItem(empleado, codigo);
                ddlTS.Items.Add(item);
            }
            if (L.Equals("es"))
            {
                ddlTS.Items.Add(new ListItem("Sin Trabajador Social", "NT"));
            }
            else
            {
                ddlTS.Items.Add(new ListItem("Without Social Worker", "NT"));
            }
        }

        protected void llenarNuevoTSs()
        {
            ddlNuevoTS.Items.Clear();
            ddlNuevoTS.Items.Add(new ListItem("", ""));
            DataTable tblTipos;
            tblTipos = bdGEN.obtenerTS2(S);
            String codigo = "";
            String empleado = "";
            ListItem item;
            foreach (DataRow row in tblTipos.Rows)
            {
                codigo = row["EmployeeId"].ToString();
                empleado = row["EmployeeId"].ToString();
                item = new ListItem(empleado, codigo);
                ddlNuevoTS.Items.Add(item);
            }
        }

        protected void btnAsignar_Click(object sender, EventArgs e)
        {
            String TSNuevo = ddlNuevoTS.SelectedValue;
            if (!String.IsNullOrEmpty(TSNuevo))
            {
                try
                {
                    gdvFamilias.Columns[0].Visible = true;
                    foreach (GridViewRow row in gdvFamilias.Rows)
                    {
                        CheckBox check = row.FindControl("chkModificar") as CheckBox;
                        String TS = row.Cells[2].ToString();
                        if (check.Checked)
                        {
                            String familia = row.Cells[0].Text;
                            bdTS.asgTSNuevaAsignacion(S, familia, TSNuevo, U, "");
                        }
                    }
                    Session["AsignoTSGrupo"] = "SI";
                    //mst.mostrarMsj(dic.msjSeHaActualizado);
                }
                catch (Exception ex)
                {
                    //mst.mostrarMsj(dic.msjNoSeRealizoExcp + ex.Message.ToString());
                    Session["AsignoTSGrupo"] = ex.Message.ToString();
                }
                finally
                {
                    gdvFamilias.Columns[0].Visible = false;
                    Response.Redirect("~/TS/AsignacionTSGrupo.aspx");
                }


            }
        }

        protected void btnBuscar_Click(object sender, EventArgs e)
        {
            pnlAsignar.Visible = false;
            area = ddlArea.SelectedValue;
            TS = ddlTS.SelectedValue;
            if (!String.IsNullOrEmpty(area) || !String.IsNullOrEmpty(TS))
            {
                DataTable dtFamilias = BDF.obtenerFamiliasAfiliadas(S, L, TS, area);
                if (dtFamilias.Rows.Count > 0)
                {
                    gdvFamilias.Columns[1].HeaderText = dic.familia;
                    gdvFamilias.Columns[2].HeaderText = dic.trabajadorS;
                    gdvFamilias.Columns[3].HeaderText = dic.fechaInicio;
                    gdvFamilias.Columns[4].HeaderText = dic.jefeCasa;
                    gdvFamilias.Columns[5].HeaderText = dic.direccion;
                    gdvFamilias.Columns[6].HeaderText = dic.area;
                    gdvFamilias.DataSource = dtFamilias;
                    gdvFamilias.DataBind();
                    gdvFamilias.Columns[0].Visible = false;
                    pnlBuscar.Visible = false;
                    pnlAsignar.Visible = true;
                }
                else
                {
                    mst.mostrarMsjAdvNtf(dic.msjNoEncontroResultados);
                }
            }
            else
            {
                if (L.Equals("es"))
                {
                    mst.mostrarMsjAdvNtf("Por favor, ingrese al menos un parámetro.");
                }
                else
                {
                    mst.mostrarMsjAdvNtf("Please, enter at least one parameter.");
                }
            }
        }

        protected void btnNuevaBusqueda_Click(object sender, EventArgs e)
        {
            pnlAsignar.Visible = false;
            ddlArea.SelectedValue = "";
            ddlTS.SelectedValue = "";
            pnlBuscar.Visible = true;
            //iniciarElementos();
            mst.redir("~/TS/AsignacionTSGrupo.aspx");
        }
    }
}