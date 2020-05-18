using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Familias3._1.APJO
{
    public partial class AsistenciasGrupoArchivo : System.Web.UI.Page
    {

        public static BDPROE bdAPJO;
        public static String U;
        public static String S;
        public static String L;
        protected static mast mst;
        protected static Diccionario dic;
        protected static DataTable dtAsistenciasAux;
        protected static DataTable dtAsistencias;
        protected static Boolean tieneAdvertencias;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                dtAsistenciasAux = new DataTable();
                dtAsistencias = new DataTable();
                mst = (mast)Master;
                bdAPJO = new BDPROE();
                S = mast.S;
                U = mast.U;
                L = mast.L;
                dic = new Diccionario(L, S);
            }
        }

        protected void btnCargar_Click(object sender, EventArgs e)
        {
            if ((FileUpload.HasFile))
            {
                if (!Convert.IsDBNull(FileUpload.PostedFile) &
                        FileUpload.PostedFile.ContentLength > 0)
                {
                    FileUpload.SaveAs(Server.MapPath(".") + "\\" + FileUpload.FileName);

                    OleDbConnection myExcelConn = new OleDbConnection
                        ("Provider=Microsoft.ACE.OLEDB.12.0; " +
                            "Data Source=" + Server.MapPath(".") + "\\" + FileUpload.FileName +
                            ";Extended Properties=Excel 12.0;");
                    try
                    {
                        myExcelConn.Open();

                        OleDbCommand objOleDB = new OleDbCommand("SELECT *FROM [Hoja1$]", myExcelConn);

                        OleDbDataReader objBulkReader = null;
                        objBulkReader = objOleDB.ExecuteReader();
                        dtAsistenciasAux.Load(objBulkReader);

                        realizarValidaciones();

                        

                        llenarGdvAsistencias();
                    }
                    catch (Exception ex)
                    {
                    }
                    finally
                    {
                        myExcelConn.Close(); myExcelConn = null;
                    }
                }
                btnGuardar.Visible = true;
                btnNuevaCarga.Visible = true;
                FileUpload.Visible = false;
                btnCargar.Visible = false;
            }
        }

        protected void llenarGdvAsistencias()
        {
            //gdvAsistencias.Columns[0].Visible = true;
            //gdvAsistencias.Columns[1].Visible = true;
            //gdvAsistencias.Columns[2].Visible = true;
            gdvAsistencias.DataSource = dtAsistencias;
            gdvAsistencias.DataBind();
            //gdvAsistencias.Columns[0].Visible = false;
            //gdvAsistencias.Columns[1].Visible = false;
            //gdvAsistencias.Columns[2].Visible = false;
        }
        protected void realizarValidaciones()
        {
            String usuario;
            String fecha;
            String actividad;
            String notas;
            String miembro;
            String nombre;
            String advertencia;
            dtAsistencias = new DataTable();
            Boolean existeUsuario;
            Boolean existeActividad;
            Boolean existeMiembro;
            dtAsistencias.Columns.Add("ExisteUsuario");
            dtAsistencias.Columns.Add("ExisteActividad");
            dtAsistencias.Columns.Add("ExisteMiembro");
            dtAsistencias.Columns.Add("Usuario");
            dtAsistencias.Columns.Add("Fecha");
            dtAsistencias.Columns.Add("Actividad");
            dtAsistencias.Columns.Add("Notas");
            dtAsistencias.Columns.Add("Miembro");
            dtAsistencias.Columns.Add("Nombre");
            dtAsistencias.Columns.Add("Advertencia");
            tieneAdvertencias = false;
            foreach(DataRow rowAsistencia in dtAsistenciasAux.Rows) 
            {
                usuario = "";
                fecha = "";
                actividad = "";
                notas = "";
                miembro = "";
                nombre = "";
                advertencia = "";
                existeUsuario = true;
                existeActividad = true;
                existeMiembro = true;
                usuario = rowAsistencia[0].ToString();
                fecha = rowAsistencia[1].ToString();
                actividad = rowAsistencia[2].ToString();
                notas = rowAsistencia[3].ToString();
                miembro = rowAsistencia[4].ToString();
                nombre = rowAsistencia[5].ToString();
                if (bdAPJO.verificarUsuario(S, usuario) == 0)
                {
                    existeUsuario = false;
                    tieneAdvertencias = true;
                }
                if (bdAPJO.verificarActividad(S, actividad) == 0)
                {
                    existeActividad = false;
                    tieneAdvertencias = true;
                }
                if (bdAPJO.verificarMiembro(S, miembro) == 0)
                {
                    existeMiembro = false;
                    tieneAdvertencias = true;
                }
                advertencia =  (!existeUsuario ? "No. de Usuario no válido. " : "") + (!existeActividad ? "No. de Actividad no válido. " : "") + (!existeMiembro ? "No. de Miembro no válido. " : "");
                dtAsistencias.Rows.Add((existeUsuario ? "S" : "N"), existeActividad ? "S" : "N", existeMiembro ? "S" : "N", usuario, fecha, actividad, notas, miembro, nombre, advertencia);
            }
        }
        protected void btnGuardar_Click(object sender, EventArgs e)
        {
            try
            {
                String existeUsuario;
                String existeActividad;
                String existeMiembro;
                String usuario;
                String miembro;
                String actividad;
                DateTime fechaActividad;
                foreach (GridViewRow row in gdvAsistencias.Rows)
                {
                    existeUsuario = row.Cells[0].Text;
                    existeActividad = row.Cells[1].Text;
                    existeMiembro = row.Cells[2].Text;
                    usuario = row.Cells[3].Text;
                    actividad = row.Cells[5].Text;
                    miembro = row.Cells[7].Text;
                    fechaActividad = Convert.ToDateTime(row.Cells[4].Text);
                    if (existeUsuario.Equals("S") && existeActividad.Equals("S") && existeMiembro.Equals("S"))
                    {
                        bdAPJO.ingresarAsistenciaArchivo(S, miembro, fechaActividad.ToString("yyyy-MM-dd HH:mm:ss"), actividad, DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"), usuario, "", "", "");
                    }
                }
                mst.mostrarMsjNtf(dic.msjSeHanIngresado);
            }
            catch (Exception ex)
            {
                mst.mostrarMsj(dic.msjNoSeRealizoExcp + ex.Message.ToString());
            }
        }
        protected void gdvAsistencias_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            if (e.Row.RowType == DataControlRowType.DataRow)
            {
                String usuario = e.Row.Cells[0].Text;
                String actividad = e.Row.Cells[1].Text;
                String miembro = e.Row.Cells[2].Text;
                e.Row.Cells[3].BackColor = usuario.Equals("N") ? Color.Thistle : Color.White;
                e.Row.Cells[5].BackColor = actividad.Equals("N") ? Color.Thistle : Color.White;
                e.Row.Cells[7].BackColor = miembro.Equals("N") ? Color.Thistle : Color.White;
                e.Row.Cells[9].BackColor = usuario.Equals("N") || actividad.Equals("N") || miembro.Equals("N") ? Color.Thistle : Color.White;
            }
        }
        protected void btnNuevaCarga_Click(object sender, EventArgs e)
        {
            btnGuardar.Visible = false;
            btnNuevaCarga.Visible = false;
            btnCargar.Visible = true;
            FileUpload.Visible = true;
        }
    }
}