using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Familias3._1.bd;
using System.Data;
using System.Drawing;
using System.Data.SqlClient;
using System.Configuration;

namespace Familias3._1.MISC
{
    public partial class PerfilMiembro : System.Web.UI.Page
    {
        protected BDMiembro BDM;
        protected Diccionario dic;
        protected static String S;
        protected static String M;
        protected static String L;
        protected static String F;
        protected static mast mst;
        string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        SqlConnection con = new SqlConnection(System.Web.Configuration.WebConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString);
        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            if (S.Equals("E") || S.Equals("A"))
            {
                tblAfiliacion.Visible = false;
                gdvPadres.Visible = false;
                gdvPadrinos.Visible = false;
                tblEduc.Visible = false;
                pnlAvisos.Visible = false;
            }
            if (!IsPostBack)
            {
                mst = (mast)Master;
                try
                {
                    BDM = new BDMiembro();
                    dic = new Diccionario(L, S);
                    string consulta = "SELECT COUNT(*) conteo FROM dbo.Member WHERE Project='" + S + "' AND RecordStatus=' ' AND MemberId='" + M + "' AND AffiliationStatus='AFIL' ";
                    int apadrinado = ObtenerEntero(consulta, "conteo");
                    if (apadrinado == 1)
                    {
                        agregaPalabras();
                        agregaInfoGen();
                        agregaDerechos();
                        agregaAvisos();
                        agregaPadrinos();
                        agregaPadres();
                    }
                    else
                    {
                        agregaPalabras();
                        agregaInfoGen();
                        agregaDerechos();
                        agregaAvisos();
                        agregaPadres();
                        //tblAfiliacion.Visible = false;
                        pnlPadrinos.Visible = false;
                    }


                }
                catch
                {

                }
            }
        }

        protected void agregaInfoGen()
        {
            DataTable dtMiembro = BDM.obtenerDatos(S, M, L);
            DataRow rowM = dtMiembro.Rows[0];
            lblVNombres.Text = rowM["FirstNames"].ToString();
            lblVApellidos.Text = rowM["LastNames"].ToString();
            lblVNUsual.Text = rowM["PreferredName"].ToString();
            String vivoOMuerto = rowM["LiveDead"].ToString();
            lblVGenero.Text = rowM["Gender"].ToString() + " - " + vivoOMuerto;
            if (!vivoOMuerto.Equals("Muerto") && !vivoOMuerto.Equals("Deceased"))
            {
                String fechaNacimiento = rowM["BirthDate"].ToString();
                if (!String.IsNullOrEmpty(fechaNacimiento))
                {
                    lblVBirth.Text = fechaNacimiento + " (" + rowM["Edad"].ToString() + ")";
                }
            }
            else
            {
                lblVBirth.Text = rowM["BirthDate"].ToString();
            }
            lblVGrado.Text = rowM["Grado"].ToString();
            String semaf = rowM["Semaphore"].ToString();
            if (semaf == "VERD")
            {
                pnlVSemaforo.BackColor = Color.MediumSeaGreen;
            }
            else if (semaf == "ROJO")
            {
                pnlVSemaforo.BackColor = Color.Crimson;
            }
            else if (semaf == "AMAR")
            {
                pnlVSemaforo.BackColor = Color.Yellow;
            }
            else
            {
                pnlVSemaforo.Visible = false;
            }
            lblVLee.Text = rowM["Literacy"].ToString();
            lblVPhone.Text = rowM["Telefono"].ToString();
            String afilStatus = rowM["AffiliationStatus"].ToString();
            if (!String.IsNullOrEmpty(afilStatus))
            {
                lblVAfilStatus.Text = afilStatus + " (" + rowM["AffiliationStatusDate"].ToString() + ")";
            }
            lblVTAfil.Text = rowM["AffiliationType"].ToString();
            lblVDPI.Text = rowM["Id"].ToString();
        }

        private void agregaDerechos()
        {
            lblNoDerechos.Text = dic.noTiene;
            tblBenef.Rows[1].Visible = true;
            int esAfil = BDM.esAfiliado(S, M);
            int esApad = BDM.esApadrinado(S, M);
            int tienProy = BDM.tieneProyeccion(S, M, L);
            String derSalud = BDM.derechosSalud(S, M, L);
            if (tienProy == 1)
            {
                lblVvnd.Visible = false;
                tblBenef.Rows[2].Visible = false;
                lblEduc.Visible = false;
                tblBenef.Rows[3].Visible = false;
                lblSalud.Visible = false;
                tblBenef.Rows[4].Visible = false;
            }
            if ((esApad == 1) || (esAfil == 1))
            {
                lblVvnd.Visible = true;
                lblEduc.Visible = true;
                tblBenef.Rows[1].Visible = false;
            }
            else
            {
                lblVvnd.Visible = false;
                tblBenef.Rows[3].Visible = false;
                lblEduc.Visible = false;
                tblBenef.Rows[4].Visible = false;
            }
            if (!derSalud.Equals("NO TIENE DERECHOS") && (!derSalud.Equals("NO RIGHTS")))
            {
                lblSalud.Visible = true;
                lblSalud.Text = lblSalud.Text + "(" + BDM.derechosSalud(S, M, L) + ")";
                tblBenef.Rows[1].Visible = false;
            }
            else
            {
                lblSalud.Visible = false;
                tblBenef.Rows[2].Visible = false;
            }
        }

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

        private void agregaPadrinos()
        {
            DataTable dtPadrinos = BDM.obtenerPadrinos(S, M);
            if (dtPadrinos.Rows.Count == 0)
            {
                gdvPadrinos.Visible = false;
                lblNoPadrinos.Visible = true;
            }
            else
            {
                gdvPadrinos.DataSource = dtPadrinos;
                gdvPadrinos.DataBind();
            }
        }

        private void agregaPadres()
        {
            DataTable dtPadres = BDM.obtenerPadres(S, M);
            if (dtPadres.Rows.Count == 0)
            {
                pnlPadres.Visible = false;
            }
            else
            {
                gdvPadres.DataSource = dtPadres;
                gdvPadres.DataBind();
            }
        }

        private void agregaPalabras()
        {
            lblAfilStatus.Text = dic.afilEstado + ":";
            lblAfil.Text = dic.afiliacion;
            lblApellidos.Text = dic.apellidos + ":";
            lblBenef.Text = dic.beneficios;
            lblBirth.Text = dic.fechaNacimiento + ":";
            lblDPI.Text = dic.DPI + ":";
            lblEduc.Text = dic.educacion;
            lblEducacion.Text = dic.educacion;
            lblGenero.Text = dic.genero + " - " + dic.vivoOmuerto + ":";
            lblGeneralInfo.Text = dic.infoGeneral;
            lblGrado.Text = dic.grado + ":";
            lblLee.Text = dic.lee + ":";
            lblNombres.Text = dic.nombres + ":";
            lblNoPadrinos.Text = dic.noTiene;
            lblNUsual.Text = dic.nombreUsual + ":";
            lblOtraAFil.Text = dic.otraAfil + ":";
            lblPadres.Text = dic.padresBiologicos;
            lblPadrinos.Text = dic.padrinos;
            lblPhone.Text = dic.telefono + ":";
            lblSalud.Text = dic.clinica;
            lblSemaforo.Text = dic.semaforo + ":";
            lblTAfil.Text = dic.afilTIpo + ":";
            lblVvnd.Text = dic.vivienda;
            gdvPadrinos.Columns[0].HeaderText = dic.id;
            gdvPadrinos.Columns[1].HeaderText = dic.id;
            gdvPadrinos.Columns[2].HeaderText = dic.nombre;
            gdvPadres.Columns[0].HeaderText = dic.id;
            gdvPadres.Columns[1].HeaderText = dic.id;
            gdvPadres.Columns[2].HeaderText = dic.nombre;
        }

        protected void gdvPadres_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "cmdPName")
            {
                LinkButton link = (LinkButton)gdvPadres.Rows[Convert.ToInt32(e.CommandArgument)].FindControl("btnPName");
                M = link.Text;
                Session["M"] = M;
                Session["F"] = mast.F;
                Response.Redirect("~/MISC/PerfilMiembro.aspx");
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
                return -1;
            }
        }

    }
}