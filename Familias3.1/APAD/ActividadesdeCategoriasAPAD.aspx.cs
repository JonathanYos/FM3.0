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
    public partial class ActividadesdeCategoriasAPAD : System.Web.UI.Page
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

        //////////////////////////////////////////////////////--FUNCIONES Y PROCEDIMIENTOS--//////////////////////////////////////////
        private void detectarcategoria()
        {
            string sql;
            string sitio;
            if (ddlsitio.SelectedValue == "*")
            {
                sitio = "Project";
            }
            else
            {
                sitio = "'" + ddlsitio.SelectedValue + "'";
            }
            switch (ddlcategoria.SelectedIndex)
            {

                case 1:
                    {
                        //carnes

                        sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name,(SELECT COUNT(*) FROM MemberSolicitudeCard WHERE RecordStatus = ' ' AND PrintDate IS NOT NULL AND Project = M.Project AND MemberId = M.MemberId) 'Carnés' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + "";
                        llenargrid(sql);
                    }
                    break;
                case 2:
                    {
                        //cartas
                        sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name, dbo.fn_GEN_Npadrinos(M.Project, M.MemberId) AS Padrinos,(SELECT COUNT(*) FROM dbo.MemberSponsorLetter MSL WHERE MSL.RecordStatus = ' ' AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId  AND YEAR(DateTimeWritten) = " + DateTime.Now.Year + " AND Category = 'PRIM') AS '1aCarta' ,(SELECT COUNT(*) FROM dbo.MemberSponsorLetter MSL WHERE MSL.RecordStatus = ' ' AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId  AND YEAR(DateTimeWritten) = " + DateTime.Now.Year + " AND Category = 'SEGU') AS '2aCarta' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + "";
                        switch (ddlcategoriacarta.SelectedIndex)
                        {
                            case 1:
                                {
                                    llenargrid(sql.Replace(",(SELECT COUNT(*) FROM dbo.MemberSponsorLetter MSL WHERE MSL.RecordStatus = ' ' AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId  AND YEAR(DateTimeWritten) = " + DateTime.Now.Year + " AND Category = 'SEGU') AS '2aCarta'", ""));
                                }
                                break;
                            case 2:
                                {
                                    llenargrid(sql.Replace(",(SELECT COUNT(*) FROM dbo.MemberSponsorLetter MSL WHERE MSL.RecordStatus = ' ' AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId  AND YEAR(DateTimeWritten) = " + DateTime.Now.Year + " AND Category = 'PRIM') AS '1aCarta' ", ""));
                                }
                                break;
                            default:
                                {
                                    llenargrid(sql);
                                }
                                break;
                        }

                    }
                    break;
                case 3:
                    {
                        //fotos
                        sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name,(SELECT DATEDIFF(m, PhotoDate, GETDATE()) FROM MiscMemberSponsorInfo WHERE RecordStatus = ' ' AND YEAR(PhotoDate) = " + DateTime.Now.Year + " AND  MemberId = M.MemberId AND Project = M.Project) AS 'mesesAntigüedad' ,(SELECT RetakePhotoDate FROM MiscMemberSponsorInfo WHERE RecordStatus = ' ' AND YEAR(PhotoDate) = " + DateTime.Now.Year + " AND  MemberId = M.MemberId AND Project = M.Project) AS 'retomarFoto' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + " ORDER BY MemberId ASC";
                        llenargrid(sql);
                        switch (ddlcategoriacarta.SelectedIndex)
                        {
                            case 1:
                                {
                                    llenargrid(sql.Replace(",(SELECT DATEDIFF(m, PhotoDate, GETDATE()) FROM MiscMemberSponsorInfo WHERE RecordStatus = ' ' AND YEAR(PhotoDate) = " + DateTime.Now.Year + " AND  MemberId = M.MemberId AND Project = M.Project) AS 'mesesAntigüedad'", ""));
                                }
                                break;
                            case 2:
                                {
                                    llenargrid(sql.Replace(",(SELECT RetakePhotoDate FROM MiscMemberSponsorInfo WHERE RecordStatus = ' ' AND YEAR(PhotoDate) = " + DateTime.Now.Year + " AND  MemberId = M.MemberId AND Project = M.Project) AS 'retomarFoto'", ""));
                                }
                                break;
                            default:
                                {
                                    llenargrid(sql);
                                }
                                break;
                        }
                    }
                    break;
                case 4:
                    {
                        //regalos
                        sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name ,(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Cumpleaños' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'Cumpleaños',(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Segundo Regalo' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS '2o. Regalo', (SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Padrinos' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'de Padrino' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + " ORDER BY Project ASC";
                        llenargrid(sql);
                        switch (ddlcategoriacarta.SelectedIndex)
                        {
                            case 1:
                                {
                                    sql = sql.Replace(",(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Cumpleaños' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'Cumpleaños'", "");
                                    llenargrid(sql.Replace(", (SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Padrinos' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'de Padrino'", ""));
                                }
                                break;
                            case 2:
                                {
                                    llenargrid(sql.Replace(",(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Segundo Regalo' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS '2o. Regalo', (SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Padrinos' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'de Padrino'", ""));
                                }
                                break;
                            case 3:
                                {
                                    llenargrid(sql.Replace(",(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Cumpleaños' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'Cumpleaños',(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Segundo Regalo' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS '2o. Regalo'", ""));
                                }
                                break;
                            default:
                                {
                                    llenargrid(sql);
                                }
                                break;
                        }

                    }
                    break;
                case 5:
                    {
                        //Visita de Padrinos
                        string fecha;
                        int anio;
                        if (string.IsNullOrEmpty(txbanio.Text))
                        {
                            fecha = "YEAR(GETDATE())";
                            sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name,(SELECT COUNT(*) FROM SponsorMemberVisit WHERE RecordStatus = ' ' AND YEAR(VisitDateTime) = " + fecha + " AND PRoject = M.Project  AND MemberId = M.MemberId) 'Visitas' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + "";
                            llenargrid(sql);
                        }
                        else
                        {
                            anio = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                            int anio2 = Convert.ToInt32(txbanio.Text);
                            fecha = txbanio.Text;
                            if (anio < anio2 || anio2 <= 1980)
                            {
                                mst.mostrarMsjAdvNtf(dic.msjFechaIncorrecta);
                            }
                            else
                            {
                                sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name,(SELECT COUNT(*) FROM SponsorMemberVisit WHERE RecordStatus = ' ' AND YEAR(VisitDateTime) = " + anio2 + " AND PRoject = M.Project  AND MemberId = M.MemberId) 'Visitas' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + "";
                                llenargrid(sql);
                            }
                        }


                    }
                    break;
                case 6:
                    {
                        //Viveres
                        sql = "SELECT Project Sitio, FamilyId Familia, Pueblo, dbo.fn_GEN_TS(F.Project, F.FamilyId) AS TS,(SELECT CASE WHEN SUM(Quantity) IS NULL THEN 0 ELSE SUM(Quantity) END Cantidad FROM dbo.FamilyHelp  WHERE RecordStatus = ' ' AND YEAR(CreationDateTime) = " + DateTime.Now.Year + "  AND FamilyId = F.FamilyId AND Project = F.Project ) Cantidad FROM Family F WHERE RecordStatus = ' ' AND AffiliationStatus = 'AFIL' AND Project = " + sitio + " ORDER BY Pueblo";
                        llenargrid(sql);
                        //switch (ddlcategoriacarta.SelectedIndex)
                        //{
                        //    case 1:
                        //        {
                        //            llenargrid(sql.Replace(",(SELECT COUNT(*) FROM FamilyHelp FH INNER JOIN dbo.CdFamilyHelpReason cdFHR ON cdFHR.Code = FH.Reason WHERE RecordStatus = ' ' AND YEAR(DeliveryDateTime) = " + DateTime.Now.Year + " AND cdFHR.FunctionalArea = 'TS' AND FamilyId = F.FamilyId AND Project = F.Project) 'de TS'", ""));
                        //        } break;
                        //    case 2:
                        //        {
                        //            llenargrid(sql.Replace(",(SELECT COUNT(*) FROM FamilyHelp FH INNER JOIN dbo.CdFamilyHelpReason cdFHR ON cdFHR.Code = FH.Reason WHERE RecordStatus = ' ' AND YEAR(DeliveryDateTime) = " + DateTime.Now.Year + " AND cdFHR.FunctionalArea = 'APAD' AND FamilyId = F.FamilyId AND Project = F.Project) 'de Apadrinamiento'", ""));
                        //        } break;
                        //    default:
                        //        {
                        //            llenargrid(sql);
                        //        } break;
                        //}

                    }
                    break;
                default:
                    {

                    }
                    break;
            }
        }
        private void detectarcategoriaEn()
        {
            string sql;
            string sitio;
            if (ddlsitio.SelectedValue == "*")
            {
                sitio = "Project";
            }
            else
            {
                sitio = "'" + ddlsitio.SelectedValue + "'";
            }
            switch (ddlcategoria.SelectedIndex)
            {

                case 3:
                    {
                        //carnes
                        sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name,(SELECT COUNT(*) FROM MemberSolicitudeCard WHERE RecordStatus = ' ' AND PrintDate IS NOT NULL AND Project = M.Project AND MemberId = M.MemberId) 'Carnés' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + "";
                        llenargrid(sql);
                    }
                    break;
                case 4:
                    {
                        //cartas
                        sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name, dbo.fn_GEN_Npadrinos(M.Project, M.MemberId) AS Padrinos,(SELECT COUNT(*) FROM dbo.MemberSponsorLetter MSL WHERE MSL.RecordStatus = ' ' AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId  AND YEAR(DateTimeWritten) = " + DateTime.Now.Year + " AND Category = 'PRIM') AS '1aCarta' ,(SELECT COUNT(*) FROM dbo.MemberSponsorLetter MSL WHERE MSL.RecordStatus = ' ' AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId  AND YEAR(DateTimeWritten) = " + DateTime.Now.Year + " AND Category = 'SEGU') AS '2aCarta' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + "";
                        sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name, dbo.fn_GEN_Npadrinos(M.Project, M.MemberId) AS Padrinos,(SELECT COUNT(*) FROM dbo.MemberSponsorLetter MSL WHERE MSL.RecordStatus = ' ' AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId  AND YEAR(DateTimeWritten) = " + DateTime.Now.Year + " AND Category = 'PRIM') AS '1aCarta' ,(SELECT COUNT(*) FROM dbo.MemberSponsorLetter MSL WHERE MSL.RecordStatus = ' ' AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId  AND YEAR(DateTimeWritten) = " + DateTime.Now.Year + " AND Category = 'SEGU') AS '2aCarta' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + "";
                        switch (ddlcategoriacarta.SelectedIndex)
                        {
                            case 1:
                                {
                                    llenargrid(sql.Replace(",(SELECT COUNT(*) FROM dbo.MemberSponsorLetter MSL WHERE MSL.RecordStatus = ' ' AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId  AND YEAR(DateTimeWritten) = " + DateTime.Now.Year + " AND Category = 'SEGU') AS '2aCarta'", ""));
                                }
                                break;
                            case 2:
                                {
                                    llenargrid(sql.Replace(",(SELECT COUNT(*) FROM dbo.MemberSponsorLetter MSL WHERE MSL.RecordStatus = ' ' AND MSL.Project = M.Project AND MSL.MemberId = M.MemberId  AND YEAR(DateTimeWritten) = " + DateTime.Now.Year + " AND Category = 'PRIM') AS '1aCarta' ", ""));
                                }
                                break;
                            default:
                                {
                                    llenargrid(sql);
                                }
                                break;
                        }
                    }
                    break;
                case 5:
                    {
                        //fotos
                        sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name,(SELECT DATEDIFF(m, PhotoDate, GETDATE()) FROM MiscMemberSponsorInfo WHERE RecordStatus = ' ' AND YEAR(PhotoDate) = " + DateTime.Now.Year + " AND  MemberId = M.MemberId AND Project = M.Project) AS 'mesesAntigüedad' ,(SELECT RetakePhotoDate FROM MiscMemberSponsorInfo WHERE RecordStatus = ' ' AND YEAR(PhotoDate) = " + DateTime.Now.Year + " AND  MemberId = M.MemberId AND Project = M.Project) AS 'retomarFoto' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + " ORDER BY MemberId ASC";
                        switch (ddlcategoriacarta.SelectedIndex)
                        {
                            case 1:
                                {
                                    llenargrid(sql.Replace(",(SELECT DATEDIFF(m, PhotoDate, GETDATE()) FROM MiscMemberSponsorInfo WHERE RecordStatus = ' ' AND YEAR(PhotoDate) = " + DateTime.Now.Year + " AND  MemberId = M.MemberId AND Project = M.Project) AS 'mesesAntigüedad'", ""));
                                }
                                break;
                            case 2:
                                {
                                    llenargrid(sql.Replace(",(SELECT RetakePhotoDate FROM MiscMemberSponsorInfo WHERE RecordStatus = ' ' AND YEAR(PhotoDate) = " + DateTime.Now.Year + " AND  MemberId = M.MemberId AND Project = M.Project) AS 'retomarFoto'", ""));
                                }
                                break;
                            default:
                                {
                                    llenargrid(sql);
                                }
                                break;
                        }
                    }
                    break;
                case 2:
                    {
                        //regalos
                        sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name ,(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Cumpleaños' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'Cumpleaños',(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Segundo Regalo' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS '2o. Regalo', (SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Padrinos' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'de Padrino' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + " ORDER BY Project,MemberId ASC";
                        switch (ddlcategoriacarta.SelectedIndex)
                        {
                            case 1:
                                {
                                    sql = sql.Replace(",(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Cumpleaños' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'Cumpleaños'", "");
                                    llenargrid(sql.Replace(", (SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Padrinos' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'de Padrino'", ""));
                                }
                                break;
                            case 2:
                                {
                                    llenargrid(sql.Replace(",(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Segundo Regalo' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS '2o. Regalo', (SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Padrinos' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'de Padrino'", ""));
                                }
                                break;
                            case 3:
                                {
                                    llenargrid(sql.Replace(",(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Regalo de Cumpleaños' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS 'Cumpleaños',(SELECT COUNT(*) FROM dbo.MemberGift MG INNER JOIN  CdGiftCategory cdGC ON cdGC.Code = MG.Category WHERE MG.RecordStatus = ' ' AND YEAR(MG.SelectionDateTime) = " + DateTime.Now.Year + " AND cdGC.DescSpanish like 'Segundo Regalo' AND MG.MemberId = M.MemberId AND MG.Project = M.Project) AS '2o. Regalo'", ""));
                                }
                                break;
                            default:
                                {
                                    llenargrid(sql);
                                }
                                break;
                        }
                    }
                    break;
                case 6:
                    {
                        //Visita de Padrinos
                        string fecha;
                        int anio;
                        if (string.IsNullOrEmpty(txbanio.Text))
                        {
                            fecha = "YEAR(GETDATE())";
                            sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name,(SELECT COUNT(*) FROM SponsorMemberVisit WHERE RecordStatus = ' ' AND YEAR(VisitDateTime) = " + fecha + " AND PRoject = M.Project  AND MemberId = M.MemberId) 'Visitas' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + "";
                            llenargrid(sql);
                        }
                        else
                        {
                            anio = Convert.ToInt32(DateTime.Now.ToString("yyyy"));
                            int anio2 = Convert.ToInt32(txbanio.Text);
                            fecha = txbanio.Text;
                            if (anio < anio2 || anio2 <= 1980)
                            {
                                mst.mostrarMsjAdvNtf(dic.msjFechaIncorrecta);
                            }
                            else
                            {
                                sql = "SELECT Project, MemberId, FirstNames + ' ' + LastNames Name,(SELECT COUNT(*) FROM SponsorMemberVisit WHERE RecordStatus = ' ' AND YEAR(VisitDateTime) = " + anio2 + " AND PRoject = M.Project  AND MemberId = M.MemberId) 'Visitas' FROM Member M WHERE AffiliationStatus = 'AFIL' AND RecordStatus = ' ' AND Project = " + sitio + "";
                                llenargrid(sql);
                            }
                        }
                    }
                    break;
                case 1:
                    {
                        //Viveres
                        sql = "SELECT Project, FamilyId, Pueblo, dbo.fn_GEN_TS(F.Project, F.FamilyId) AS TS,(SELECT CASE WHEN SUM(Quantity) IS NULL THEN 0 ELSE SUM(Quantity) END Cantidad FROM dbo.FamilyHelp  WHERE RecordStatus = ' ' AND YEAR(CreationDateTime) = " + DateTime.Now.Year + "  AND FamilyId = F.FamilyId AND Project = F.Project ) Quantity FROM Family F WHERE RecordStatus = ' ' AND AffiliationStatus = 'AFIL' AND Project = '" + sitio + "' ORDER BY Pueblo";

                        llenargrid(sql);
                        //switch (ddlcategoriacarta.SelectedIndex)
                        //{
                        //    case 1:
                        //        {
                        //            llenargrid(sql.Replace(",(SELECT COUNT(*) FROM FamilyHelp FH INNER JOIN dbo.CdFamilyHelpReason cdFHR ON cdFHR.Code = FH.Reason WHERE RecordStatus = ' ' AND YEAR(DeliveryDateTime) = " + DateTime.Now.Year + " AND cdFHR.FunctionalArea = 'APAD' AND FamilyId = F.FamilyId AND Project = F.Project) 'de Apadrinamiento'", ""));
                        //        } break;
                        //    case 2:
                        //        {
                        //            llenargrid(sql.Replace(",(SELECT COUNT(*) FROM FamilyHelp FH INNER JOIN dbo.CdFamilyHelpReason cdFHR ON cdFHR.Code = FH.Reason WHERE RecordStatus = ' ' AND YEAR(DeliveryDateTime) = " + DateTime.Now.Year + " AND cdFHR.FunctionalArea = 'TS' AND FamilyId = F.FamilyId AND Project = F.Project) 'de TS'", ""));
                        //        } break;
                        //    default:
                        //        {
                        //            llenargrid(sql);

                        //        } break;
                        //}
                    }
                    break;
                default:
                    {

                    }
                    break;
            }
        }
        private void eleccion()
        {
            string sql;
            switch (ddlcategoria.SelectedIndex)
            {
                case 1:
                    {
                        ddlcategoriacarta.Items.Clear();
                        ddlcategoriacarta.Enabled = false;
                        lblanio.Visible = false;
                        txbanio.Visible = false;
                        txbanio.Text = "";
                        ddlcategoriacarta.Visible = true;
                        lblcategoriacarta.Visible = true;

                    }
                    break;
                case 2:
                    {
                        sql = "SELECT Code,CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdLetterCategory WHERE Code='PRIM' OR Code='SEGU'";
                        llenarcombo(sql);
                        lblanio.Visible = false;
                        txbanio.Visible = false;
                        txbanio.Text = "";
                        ddlcategoriacarta.Visible = true;
                        lblcategoriacarta.Visible = true;
                    }
                    break;
                case 3:
                    {
                        ddlcategoriacarta.Enabled = true;
                        ddlcategoriacarta.Items.Clear();
                        ddlcategoriacarta.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlcategoriacarta.Items.Insert(1, new ListItem("Retomar Foto", "C" + 1));
                        ddlcategoriacarta.Items.Insert(2, new ListItem("Ultima Foto (Meses)", "C" + 2));
                        lblanio.Visible = false;
                        txbanio.Visible = false;
                        txbanio.Text = "";
                        ddlcategoriacarta.Visible = true;
                        lblcategoriacarta.Visible = true;
                    }
                    break;
                case 4:
                    {
                        ddlcategoriacarta.Enabled = true;
                        ddlcategoriacarta.Items.Clear();
                        ddlcategoriacarta.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlcategoriacarta.Items.Insert(1, new ListItem("2do. Regalo", "C" + 1));
                        ddlcategoriacarta.Items.Insert(2, new ListItem("Regalo de Cumpleaños", "C" + 2));
                        ddlcategoriacarta.Items.Insert(3, new ListItem("Regalo de Padrino", "C" + 3));
                        lblanio.Visible = false;
                        txbanio.Visible = false;
                        txbanio.Text = "";
                        ddlcategoriacarta.Visible = true;
                        lblcategoriacarta.Visible = true;
                    }
                    break;
                case 5:
                    {
                        ddlcategoriacarta.Enabled = false;
                        ddlcategoriacarta.Items.Clear();
                        lblanio.Visible = true;
                        txbanio.Visible = true;
                        txbanio.Text = "";
                        ddlcategoriacarta.Visible = false;
                        lblcategoriacarta.Visible = false;
                    }
                    break;
                case 6:
                    {
                        //ddlcategoriacarta.Enabled = true;
                        //ddlcategoriacarta.Items.Clear();
                        //ddlcategoriacarta.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        //ddlcategoriacarta.Items.Insert(1, new ListItem("Apadrinamiento", "C" + 1));
                        //ddlcategoriacarta.Items.Insert(2, new ListItem("Trabajo Social", "C" + 2));
                        //lblanio.Visible = false;
                        //txbanio.Visible = false;
                        //txbanio.Text = "";
                        //ddlcategoriacarta.Visible = true;
                        //lblcategoriacarta.Visible = true;
                    }
                    break;
                default:
                    {
                        ddlcategoriacarta.Enabled = false;
                        ddlcategoriacarta.Items.Clear();
                        lblanio.Visible = false;
                        txbanio.Visible = false;
                        txbanio.Text = "";
                        ddlcategoriacarta.Visible = true;
                        lblcategoriacarta.Visible = true;
                    }
                    break;
            }
        }
        private void eleccionEn()
        {
            string sql;
            switch (ddlcategoria.SelectedIndex)
            {
                case 6:
                    {
                        ddlcategoriacarta.Items.Clear();
                        ddlcategoriacarta.Enabled = false;
                        lblanio.Visible = true;
                        txbanio.Visible = true;
                        txbanio.Text = "";
                        ddlcategoriacarta.Visible = false;
                        lblcategoriacarta.Visible = false;
                    }
                    break;
                case 4:
                    {
                        sql = "SELECT Code,CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.CdLetterCategory WHERE Code='PRIM' OR Code='SEGU'";
                        llenarcombo(sql);
                        lblanio.Visible = false;
                        txbanio.Visible = false;
                        txbanio.Text = "";
                        ddlcategoriacarta.Visible = true;
                        lblcategoriacarta.Visible = true;
                    }
                    break;
                case 3:
                    {
                        ddlcategoriacarta.Enabled = false;
                        ddlcategoriacarta.Items.Clear();
                        lblanio.Visible = false;
                        txbanio.Visible = false;
                        txbanio.Text = "";
                        ddlcategoriacarta.Visible = true;
                        lblcategoriacarta.Visible = true;
                    }
                    break;
                case 2:
                    {
                        ddlcategoriacarta.Enabled = true;
                        ddlcategoriacarta.Items.Clear();
                        ddlcategoriacarta.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlcategoriacarta.Items.Insert(1, new ListItem("2nd Gift", "C" + 1));
                        ddlcategoriacarta.Items.Insert(2, new ListItem("Birthday Gift", "C" + 2));
                        ddlcategoriacarta.Items.Insert(3, new ListItem("Sponsor Gift", "C" + 3));
                        lblanio.Visible = false;
                        txbanio.Visible = false;
                        txbanio.Text = "";
                        ddlcategoriacarta.Visible = true;
                        lblcategoriacarta.Visible = true;
                    }
                    break;
                case 5:
                    {
                        ddlcategoriacarta.Enabled = true;
                        ddlcategoriacarta.Items.Clear();
                        ddlcategoriacarta.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        ddlcategoriacarta.Items.Insert(1, new ListItem("Retake Photo", "C" + 1));
                        ddlcategoriacarta.Items.Insert(2, new ListItem("Last Photo (Months)", "C" + 2));
                        lblanio.Visible = false;
                        txbanio.Visible = false;
                        txbanio.Text = "";
                        ddlcategoriacarta.Visible = true;
                        lblcategoriacarta.Visible = true;
                    }
                    break;
                case 1:
                    {
                        //ddlcategoriacarta.Enabled = true;
                        //ddlcategoriacarta.Items.Clear();
                        //ddlcategoriacarta.Items.Insert(0, new ListItem(String.Empty, String.Empty));
                        //ddlcategoriacarta.Items.Insert(1, new ListItem("Social Work", "C" + 1));
                        //ddlcategoriacarta.Items.Insert(2, new ListItem("SponsorShip", "C" + 2));
                        //lblanio.Visible = false;
                        //txbanio.Visible = false;
                        //txbanio.Text = "";
                        //ddlcategoriacarta.Visible = true;
                        //lblcategoriacarta.Visible = true;

                    }
                    break;
                default:
                    {
                        ddlcategoriacarta.Enabled = false;
                        lblanio.Visible = false;
                        txbanio.Visible = false;
                        lblcategoriacarta.Visible = true;
                    }
                    break;
            }
        }
        private void llenarcombo(string sql)
        {
            ddlcategoriacarta.Enabled = true;
            ddlcategoriacarta.Items.Clear();
            SqlDataAdapter adapter = new SqlDataAdapter(sql, ConnectionString);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlcategoriacarta.DataSource = datos;
            ddlcategoriacarta.DataValueField = "Code";
            ddlcategoriacarta.DataTextField = "descripcion";
            ddlcategoriacarta.DataBind();
            ddlcategoriacarta.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlcategoriacarta.SelectedIndex = 0;
        }
        private void llenarcombos()
        {
            string SQL = "SELECT Code, CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END descripcion FROM dbo.FwCdOrganization WHERE Code NOT LIKE'A' AND Code NOT LIKE'E' AND Code NOT LIKE'S' ORDER BY CASE WHEN '" + L + "'='es' THEN DescSpanish ELSE DescEnglish END ASC";
            SqlDataAdapter adapter = new SqlDataAdapter(SQL, ConnectionString);
            DataTable datos = new DataTable();
            adapter.Fill(datos);
            ddlsitio.DataSource = datos;
            ddlsitio.DataValueField = "Code";
            ddlsitio.DataTextField = "descripcion";
            ddlsitio.DataBind();
            ddlsitio.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            ddlsitio.SelectedIndex = 0;

            ddlcategoria.Items.Insert(0, new ListItem(String.Empty, String.Empty));
            string[] categoria = { "Cartas", "Regalos", "Fotos", "Carnés", "Visita de Padrinos", "Víveres" };
            string[] category = { "Gifts", "Letters", "Photos", "Identity Card", "Sponsor Visit", "Family Help" };
            Array.Sort(category);
            Array.Sort(categoria);
            if (L == "es")
            {
                for (int i = 0; i < 6; i++)
                {
                    ddlcategoria.Items.Insert(i + 1, new ListItem(categoria[i], "C" + i));
                }
            }
            else
            {
                for (int i = 0; i < 6; i++)
                {
                    ddlcategoria.Items.Insert(i + 1, new ListItem(category[i], "C" + i));
                }
            }
        }
        private void llenargrid(string sql)
        {
            try
            {
                ddlcategoriacarta.Enabled = true;
                DataTable tabledata = new DataTable();
                con.Open();
                SqlDataAdapter adaptador = new SqlDataAdapter(sql, con);
                DataSet setDatos = new DataSet();
                adaptador.Fill(setDatos, "listado");
                tabledata = setDatos.Tables["listado"];
                con.Close();
                gvhistorial.DataSource = tabledata;
                gvhistorial.DataBind();
            }
            catch (Exception ex)
            {
                mst.mostrarMsjAdvNtf(ex.Message);
            }
        }
        private void traducir()
        {
            lblcategoria.Text = dic.categoriaAPAD;
            lblsitio.Text = dic.sitio;
            btnAceptar.Text = dic.AceptarAPAD;
            lblcategoriacarta.Text = dic.TipoAPAD;
            ddlcategoriacarta.Enabled = false;
            lblanio.Text = dic.año;
            lblanio.Visible = false;
            txbanio.Visible = false;
        }
        //////////////////////////////////////////////////////////////-EVENTOS-////////////////////////////////////////////////////////
        protected void ddlcategoria_SelectedIndexChanged(object sender, EventArgs e)
        {
            DataTable ds = new DataTable();
            ds = null;
            gvhistorial.DataSource = ds;
            gvhistorial.DataBind();
            if (L == "es")
            {
                eleccion();
            }
            else
            {
                eleccionEn();
            }

        }
        protected void Page_Load(object sender, EventArgs e)
        {
            M = mast.M;
            L = mast.L;
            S = mast.S;
            F = mast.F;
            U = mast.U;
            BDM = new BDMiembro();
            APD = new BDAPAD();
            dic = new Diccionario(L, S);
            mst = (mast)Master;
            if (!IsPostBack)
            {
                try
                {
                    traducir();
                    llenarcombos();
                }
                catch (Exception ex)
                {
                }
            }
        }
        protected void btnAceptar_Click(object sender, EventArgs e)
        {
            if (ddlsitio.SelectedIndex == 0 || ddlcategoria.SelectedIndex == 0)
            {
                mst.mostrarMsjAdvNtf(dic.CampoVacioAPAD);
            }
            else
            {
                if (L == "es")
                {
                    detectarcategoria();
                }
                else
                {
                    detectarcategoriaEn();
                }
            }
        }
    }
}