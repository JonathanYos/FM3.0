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
    public partial class RegistrarNADFAS : System.Web.UI.Page
    {
        public static BDTS bdTS;
        public static BDGEN bdGEN;
        public static BDFamilia BDF;
        public static String U;
        public static String F;
        public static String S;
        public static String M;
        public static String L;
        protected static Boolean vista;
        protected static mast mst;
        protected static Diccionario dic;
        protected static String año;
        protected void Page_Load(object sender, EventArgs e)
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
                vista = mast.vista;
                dic = new Diccionario(L, S);
                año = (DateTime.Now.Year + 1) + "";
                try
                {
                    llenarGdvMiembros();
                    DataTable dt = BDF.obtenerDatos(S, F, L);
                    DataRow rowF = dt.Rows[0];
                    lblDirec.Text = dic.direccion + ":";
                    lblTelef.Text = dic.telefono + ":";
                    lblTS.Text = dic.trabajadorS + ":";
                    lblClasif.Text = dic.clasificacion + ":";
                    lblVDirec.Text = rowF["Address"].ToString() + ", " + rowF["Area"].ToString();
                    lblVClasif.Text = rowF["Classification"].ToString();
                    lblVTS.Text = rowF["TS"].ToString();
                    lblVTelef.Text = rowF["Phone"].ToString();
                    btnGuardar.Text = dic.guardar;
                    if (vista)
                    {
                        cargarConSeguridad();
                    }
                    if (gdvMiembros.Rows.Count == 0)
                    {
                        if (L.Equals("es"))
                        {
                            mst.mostrarMsjStc("Esta familia no tiene miembros aptos para registrar NADFAS.");
                        }
                        else
                        {
                            mst.mostrarMsjStc("This family has no members eligible to register NADFAS.");
                        }
                    }
                    else
                    {
                        
                    }
                }
                catch
                {

                }
            }
        }

        protected void cargarConSeguridad()
        {
            foreach (GridViewRow row in gdvMiembros.Rows)
            {
                CheckBox chkAplica = row.FindControl("chkAplica") as CheckBox;
                chkAplica.Visible = false;
                Label lblAplica = row.FindControl("lblAplica") as Label;
                lblAplica.Visible = true;
            }
            btnGuardar.Visible = false;
        }

        protected void llenarGdvMiembros()
        {
            lblMiembros.Text = dic.miembros;
            gdvMiembros.Columns[0].Visible = true;
            gdvMiembros.Columns[1].Visible = true;
            gdvMiembros.Columns[2].Visible = true;
            gdvMiembros.Columns[3].HeaderText = dic.miembro;
            gdvMiembros.Columns[4].HeaderText = dic.nombre;
            gdvMiembros.Columns[5].HeaderText = dic.edad;
            gdvMiembros.Columns[6].HeaderText = dic.aplica;
            gdvMiembros.DataSource = bdTS.NADFASObtenerMiembrosNADFAS(S, F, L);
            gdvMiembros.DataBind();
            gdvMiembros.Columns[0].Visible = false;
            gdvMiembros.Columns[1].Visible = false;
            gdvMiembros.Columns[2].Visible = false;
        }


        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                gdvMiembros.Columns[0].Visible = true;
                gdvMiembros.Columns[1].Visible = true;
                gdvMiembros.Columns[2].Visible = true;
                foreach (GridViewRow row in gdvMiembros.Rows)
                {
                    CheckBox check = row.FindControl("chkAplica") as CheckBox;
                    String idMiembro = row.Cells[0].Text;
                    String fechaCreacion = row.Cells[1].Text;
                    String fechaInicio = row.Cells[2].Text;
                    if (!fechaCreacion.Equals("&nbsp;"))
                    {
                        fechaCreacion = Convert.ToDateTime(fechaCreacion).ToString("yyyy-MM-dd HH:mm:ss");
                        fechaInicio = Convert.ToDateTime(fechaInicio).ToString("yyyy-MM-dd HH:mm:ss");
                    }
                    else
                    {
                        fechaCreacion = "";
                        fechaInicio = "";
                    }
                    if (check.Checked)
                    {
                        if (!bdTS.NADFASVerificarNADFASActivo(S, idMiembro, año))
                        {
                            bdTS.NADFASNuevoNADFAS(S, idMiembro, año, U);
                        }
                    }
                    else
                    {
                        if (bdTS.NADFASVerificarNADFASActivo(S, idMiembro, año))
                        {
                            bdTS.NADFASDesactivarNADFAS(S, idMiembro, fechaCreacion, año, U, fechaInicio);
                        }
                    }
                }
                llenarGdvMiembros();
                gdvMiembros.Columns[0].Visible = false;
                gdvMiembros.Columns[1].Visible = false;
                gdvMiembros.Columns[2].Visible = false;
                mst.mostrarMsjNtf(dic.msjSeHaActualizado);
            }catch(Exception ex)
            {
                mst.mostrarMsjMdl(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
            }
        }



        //protected void gdvNADFAS_RowCommand(object sender, GridViewCommandEventArgs e)
        //{
        //    int numFilaEsp = Int32.Parse(e.CommandArgument.ToString());
        //    fechaCreacionSLCT = Convert.ToDateTime(gdvNADFAS.Rows[numFilaEsp].Cells[1].Text);
        //    miembroSLCT = gdvNADFAS.Rows[numFilaEsp].Cells[0].Text;
        //    añoSLCT = gdvNADFAS.Rows[numFilaEsp].Cells[2].Text;
        //    DataRow dtRow = bdTS.NADFASobtenerNADFASesp(S, miembroSLCT, fechaCreacionSLCT.ToString("yyyy-MM-dd HH:mm:ss"), añoSLCT + "").Rows[0];
        //    String proximoGrado = dtRow["NextGrade"].ToString();
        //    String centroEducProximoGrado = dtRow["NextGradeSchool"].ToString();
        //    String notas = dtRow["Notes"].ToString();
        //    if (e.CommandName == "cmdActualizar")
        //    {
        //        try
        //        {
        //            lblVActMiembro.Text = gdvNADFAS.Rows[numFilaEsp].Cells[3].Text;
        //            lblVActAño.Text = gdvNADFAS.Rows[numFilaEsp].Cells[4].Text;
        //            if (notas.Equals("&nbsp;"))
        //            {
        //                notas = "";
        //            }
        //            llenarCentrosEduc(ddlActCentroEduc, proximoGrado);
        //            ddlActCentroEduc.SelectedValue = centroEducProximoGrado;
        //            ddlActProximoGrado.SelectedValue = proximoGrado;
        //            txbActNotas.Text = notas;
        //            pnlIngresarNADFAS.Visible = false;
        //            pnlActualizarNADFAS.Visible = true;
        //        }
        //        catch (Exception ex)
        //        {
        //            mst.mostrarMsjAdv(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
        //        }
        //    }
        //    else if (e.CommandName == "cmdEliminar")
        //    {
        //        mst.mostrarMensajeOpciones(dic.msjEliminarRegistro);
        //    }
        //}

        //protected void btnIngresar_Click(object sender, EventArgs e)
        //{
        //    DateTime fechaCreacion = DateTime.Now;
        //    //String miembro = ddlMiembro.SelectedValue;
        //    String miembro = miembroSLCT;
        //    String notas = txbNotas.Text;
        //    String proximoGrado = ddlProximoGrado.SelectedValue;
        //    String centroEducProximoGrado = ddlCentroEduc.SelectedValue;
        //    if (bdTS.NADFASNuevoNADFAS(S, miembro, fechaCreacion.ToString("MM/dd/yyyy HH:mm:ss"), año, U, notas, proximoGrado, centroEducProximoGrado))
        //    {
        //        llenarGdvNADFAS();
        //        prepararPnlInsertar();
        //        mst.mostrarMsj(dic.msjSeHaIngresado);
        //    }
        //    else
        //    {
        //        if (L.Equals("es"))
        //        {
        //            mst.mostrarMsjAdv("Un miembro solo puede tener un NADFAS por año.");
        //        }
        //        else
        //        {
        //            mst.mostrarMsjAdv("A member can only have one NADFAS per year.");
        //        }
        //    }
        //}

        //protected void btnActualizar_Click(object sender, EventArgs e)
        //{
        //    String notas = txbActNotas.Text;
        //    String proximoGrado = ddlActProximoGrado.SelectedValue;
        //    String centroEducProximoGrado = ddlActCentroEduc.SelectedValue;
        //    if (bdTS.NADFASActualizarNADFAS(S, miembroSLCT, fechaCreacionSLCT.ToString("MM/dd/yyyy HH:mm:ss"), añoSLCT, U, notas, proximoGrado, centroEducProximoGrado))
        //    {
        //        prepararPnlInsertar();
        //        llenarGdvNADFAS();
        //        mst.mostrarMsj(dic.msjSeHaActualizado);
        //    }
        //    else
        //    {
        //        if (L.Equals("es"))
        //        {
        //            mst.mostrarMsjAdv("Un miembro solo puede tener un NADFAS por año.");
        //        }
        //        else
        //        {
        //            mst.mostrarMsjAdv("A member can only have one NADFAS per year.");
        //        }
        //    }

        //}


        //protected void eliminarNADFAS(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        bdTS.NADFASEliminarNADFAS(S, miembroSLCT, fechaCreacionSLCT.ToString("MM/dd/yyyy HH:mm:ss"), añoSLCT, U);
        //        llenarGdvNADFAS();
        //        mst.mostrarMsj(dic.msjSeHaEliminado);
        //    }
        //    catch (Exception ex)
        //    {
        //        mst.mostrarMsjAdv(dic.msjNoSeRealizoExcp + ex.Message.ToString() + ".");
        //    }
        //}
    }
}