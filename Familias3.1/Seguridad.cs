using Familias3._1.bd;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Familias3._1
{
    public enum Selec
    {
        Ninguno = 0,
        FamRegi = 1,
        FamAfilGradDesa = 2,
        FamGrad = 3,
        FamDesa = 4,
        FamAfil = 5,
        MiemFamRegi = 6,
        MiemFamAfilGradDesa = 7,
        MiemFamGradDesa = 8,
        MiemFamGrad = 9,
        MiemFamDesa = 10,
        MiemFamAfil = 11,
        Afil = 12,
        Apad = 13,
        Grad = 14,
        Desa = 15,
        AfilApadGrad = 16,
        Padr = 17
    }
    public class Seguridad
    {
        List<Pagina> listPaginas = new List<Pagina>();
        public static List<Funcion> listFunciones = new List<Funcion>();
        public static BDUsuario bdU = new BDUsuario();
        public Seguridad()
        {
            llenarPaginas();
            llenarFunciones();
        }

        protected void llenarFunciones()
        {
            //TRABAJO SOCIAL

            listFunciones.Add(new Funcion("Buscar.aspx", (int)Selec.Ninguno, "MISC", "MBT"));
            listFunciones.Add(new Funcion("BusquedaFamilias.aspx", (int)Selec.Ninguno, "MISC", "MBT"));
            listFunciones.Add(new Funcion("BusquedaMiembrosInfoEduc.aspx", (int)Selec.Ninguno, "MISC", "MBT"));
            listFunciones.Add(new Funcion("BusquedaMiembrosOtraInfo.aspx", (int)Selec.Ninguno, "MISC", "MBT"));
            listFunciones.Add(new Funcion("PerfilFamilia.aspx", (int)Selec.FamRegi, "MISC", "FP"));
            listFunciones.Add(new Funcion("PerfilMiembro.aspx", (int)Selec.MiemFamRegi, "MISC", "MP"));
            listFunciones.Add(new Funcion("CambiarPreferencias.aspx", (int)Selec.Ninguno, "MISC", "MPP"));
            listFunciones.Add(new Funcion("CambiarContrasena.aspx", (int)Selec.Ninguno, "MISC", "MC"));
            listFunciones.Add(new Funcion("BusquedasOtraInfo.aspx", (int)Selec.Ninguno, "MISC", ""));

            listFunciones.Add(new Funcion("Visitas.aspx", (int)Selec.FamAfil, "TS", "AFVI")); // Agregar Visita
            listFunciones.Add(new Funcion("Visitas.aspx", (int)Selec.FamAfil, "TS", "DFV"));  //Eliminar Visita
            listFunciones.Add(new Funcion("Visitas.aspx", (int)Selec.FamAfil, "TS", "MFVI")); // Modificar Visita
            listFunciones.Add(new Funcion("Visitas.aspx", (int)Selec.FamAfilGradDesa, "TS", "RFV")); // Revisar Visita


            listFunciones.Add(new Funcion("AnoEscolar.aspx", (int)Selec.FamAfilGradDesa, "TS", "IAEA")); // Ingresar Año Escolar
            listFunciones.Add(new Funcion("AnoEscolar.aspx", (int)Selec.FamAfilGradDesa, "TS", "MAEA")); // Modificar Año Escolar
            listFunciones.Add(new Funcion("AnoEscolar.aspx", (int)Selec.FamAfilGradDesa, "TS", "BAEA")); // Borrar Año Escolar
            listFunciones.Add(new Funcion("AnoEscolar.aspx", (int)Selec.FamAfilGradDesa, "TS", "RAEA")); // Revisar Año Escolar


            listFunciones.Add(new Funcion("CondicionesFamiliares.aspx", (int)Selec.FamAfil, "TS", "MCVT")); // Modificar Condiciones Vivienda
            listFunciones.Add(new Funcion("CondicionesFamiliares.aspx", (int)Selec.FamAfilGradDesa, "TS", "RCVT")); //Revisar Condiciones Vivienda

            listFunciones.Add(new Funcion("CondicionesFamiliares.aspx", (int)Selec.FamAfil, "TS", "MGV")); // Modificar Gastos
            listFunciones.Add(new Funcion("CondicionesFamiliares.aspx", (int)Selec.FamAfilGradDesa, "TS", "RGV")); //Revisar Gastos

            listFunciones.Add(new Funcion("CondicionesFamiliares.aspx", (int)Selec.MiemFamAfil, "TS", "AOM")); // Agregar Ocupación
            listFunciones.Add(new Funcion("CondicionesFamiliares.aspx", (int)Selec.MiemFamAfil, "TS", "BOM")); //Revisar Borrar Ocupación
            listFunciones.Add(new Funcion("CondicionesFamiliares.aspx", (int)Selec.MiemFamAfil, "TS", "MOM")); //Modificar Ocupación
            listFunciones.Add(new Funcion("CondicionesFamiliares.aspx", (int)Selec.MiemFamAfilGradDesa, "TS", "RDOM")); //Revisar Detalle de Ocupaciones
            listFunciones.Add(new Funcion("CondicionesFamiliares.aspx", (int)Selec.MiemFamAfilGradDesa, "TS", "ROM")); //Revisar Ocupaciones

            listFunciones.Add(new Funcion("CondicionesFamiliares.aspx", (int)Selec.FamAfil, "TS", "MPFT")); //Modificar Posesiones
            listFunciones.Add(new Funcion("CondicionesFamiliares.aspx", (int)Selec.FamAfilGradDesa, "TS", "RPFT")); //Revisar Posesiones

            listFunciones.Add(new Funcion("Avisos.aspx", (int)Selec.FamAfil, "TS", "AFWT")); // Agregar Aviso Familiar
            listFunciones.Add(new Funcion("Avisos.aspx", (int)Selec.FamAfil, "TS", "DFWT")); //Borrar Aviso Familiar
            listFunciones.Add(new Funcion("Avisos.aspx", (int)Selec.FamAfil, "TS", "MFWT")); // Modificar Aviso Familiar
            listFunciones.Add(new Funcion("Avisos.aspx", (int)Selec.FamAfilGradDesa, "TS", "RTSW")); //Revisar Aviso Familiar



            listFunciones.Add(new Funcion("AsignacionTS.aspx", (int)Selec.FamAfil, "TS", "ATS"));
            listFunciones.Add(new Funcion("AsignacionTS.aspx", (int)Selec.FamAfil, "TS", "DTS"));
            listFunciones.Add(new Funcion("AsignacionTS.aspx", (int)Selec.FamAfil, "TS", "MTS"));
            listFunciones.Add(new Funcion("AsignacionTS.aspx", (int)Selec.FamAfilGradDesa, "TS", "RTS"));

            listFunciones.Add(new Funcion("AsignacionTSGrupo.aspx", (int)Selec.Ninguno, "TS", "MTSG"));

            listFunciones.Add(new Funcion("NADFAS.aspx", 1, "TS", "MCTA"));

            listFunciones.Add(new Funcion("Resumen.aspx", 1, "TS", "RITS"));
            listFunciones.Add(new Funcion("Historial.aspx", 1, "TS", "RHIS"));

            listFunciones.Add(new Funcion("ReportesYEstadisticas.aspx", (int)Selec.Ninguno, "TS", "IFTS"));
            listFunciones.Add(new Funcion("ReportesYEstadisticas.aspx", (int)Selec.Ninguno, "TS", "EATS"));
            listFunciones.Add(new Funcion("ReportesYEstadisticas.aspx", (int)Selec.Ninguno, "TS", "IFAT"));

            listFunciones.Add(new Funcion("InformacionFamiliar.aspx", 1, "TS", "ANM"));
            listFunciones.Add(new Funcion("InformacionFamiliar.aspx", 2, "TS", "MM"));
            listFunciones.Add(new Funcion("InformacionFamiliar.aspx", 1, "TS", "MF"));

            listFunciones.Add(new Funcion("ClasificacionAnoSiguiente.aspx", (int)Selec.Ninguno, "TS", "COCF"));

            listFunciones.Add(new Funcion("Clasificacion.aspx", (int)Selec.FamAfilGradDesa, "TS", "ICF"));
            listFunciones.Add(new Funcion("Clasificacion.aspx", (int)Selec.FamAfilGradDesa, "TS", "MCF"));

            listFunciones.Add(new Funcion("Actividades.aspx", 1, "TS", "AFPS"));
            listFunciones.Add(new Funcion("Actividades.aspx", 1, "TS", "DAFP"));
            listFunciones.Add(new Funcion("Actividades.aspx", 1, "TS", "RAFP"));

            listFunciones.Add(new Funcion("Viveres.aspx", 1, "TS", "BAF"));
            listFunciones.Add(new Funcion("Viveres.aspx", 1, "TS", "IAF"));
            listFunciones.Add(new Funcion("Viveres.aspx", 1, "TS", "MAF"));
            listFunciones.Add(new Funcion("Viveres.aspx", 1, "TS", "RAF"));

            listFunciones.Add(new Funcion("AnalisisConstruccionVivienda.aspx", 1, "TS", "IAPF"));
            listFunciones.Add(new Funcion("AnalisisConstruccionVivienda.aspx", 1, "TS", "RAPV"));



            //APADRINAMIENTO
            listFunciones.Add(new Funcion("ActividadesdeCategoriasAPAD.aspx", (int)Selec.Ninguno, "APAD", "IADA"));
            listFunciones.Add(new Funcion("ActividadesdeCategoriasAPAD.aspx", (int)Selec.Ninguno, "APAD", "PPL"));
            listFunciones.Add(new Funcion("ActividadesdeCategoriasAPAD.aspx", (int)Selec.Ninguno, "APAD", "PRP"));
            listFunciones.Add(new Funcion("ApadrinadosPendAPAD.aspx", (int)Selec.Ninguno, "APAD", "BAP"));
            listFunciones.Add(new Funcion("ApadrinadosPendAPAD.aspx", (int)Selec.Ninguno, "APAD", "CIAP"));
            listFunciones.Add(new Funcion("ApadrinamientosPendAPAD.aspx", (int)Selec.Ninguno, "APAD", "IPE"));
            listFunciones.Add(new Funcion("BusquedaPadrinosAPAD.aspx", (int)Selec.Ninguno, "APAD", "BP"));
            listFunciones.Add(new Funcion("CartasFamiliaAPAD.aspx", 1, "APAD", "ACMP"));
            listFunciones.Add(new Funcion("EnviodeCartaAPAD.aspx", (int)Selec.Ninguno, "APAD", "MCMP"));
            listFunciones.Add(new Funcion("EstadisticasAPAD.aspx", (int)Selec.Ninguno, "APAD", "CEAA"));

            listFunciones.Add(new Funcion("FotoAPAD.aspx", 4, "APAD", "TF"));

            listFunciones.Add(new Funcion("PerfilPadrinoAPAD.aspx", 5, "APAD", "PP"));

            listFunciones.Add(new Funcion("RegalosSelAPAD.aspx", (int)Selec.Ninguno, "APAD", "IRS"));

            listFunciones.Add(new Funcion("RegistroCarnetsAPAD.aspx", (int)Selec.Ninguno, "APAD", "ICP"));

            listFunciones.Add(new Funcion("RegistroCartasAPAD.aspx", 3, "APAD", "ACMP"));
            listFunciones.Add(new Funcion("RegistroCartasAPAD.aspx", 3, "APAD", "BCMP"));
            listFunciones.Add(new Funcion("RegistroCartasAPAD.aspx", 3, "APAD", "MCMP"));
            listFunciones.Add(new Funcion("RegistroCartasAPAD.aspx", 3, "APAD", "RCMP"));
            listFunciones.Add(new Funcion("RegistroCartasAPAD.aspx", 3, "APAD", "ICMP"));

            listFunciones.Add(new Funcion("RegistroRegaloAPAD.aspx", 3, "APAD", "BRM"));
            listFunciones.Add(new Funcion("RegistroRegaloAPAD.aspx", 3, "APAD", "IRM"));
            listFunciones.Add(new Funcion("RegistroRegaloAPAD.aspx", 3, "APAD", "MRM"));
            listFunciones.Add(new Funcion("RegistroRegaloAPAD.aspx", 3, "APAD", "RRM"));

            listFunciones.Add(new Funcion("RegistroRestriccionesAPAD.aspx", 3, "APAD", "MRA"));

            listFunciones.Add(new Funcion("ResumenAPAD.aspx", 3, "APAD", "RCMP"));
            listFunciones.Add(new Funcion("ResumenAPAD.aspx", 3, "APAD", "ACMP"));
            listFunciones.Add(new Funcion("ResumenAPAD.aspx", 3, "APAD", "IRM"));
            listFunciones.Add(new Funcion("ResumenAPAD.aspx", 3, "APAD", "RF"));
            listFunciones.Add(new Funcion("ResumenAPAD.aspx", 3, "APAD", "RIAM"));
            listFunciones.Add(new Funcion("ResumenAPAD.aspx", 3, "APAD", "RRM"));

            listFunciones.Add(new Funcion("VisitaPadrinosAPAD.aspx", 5, "APAD", "BVP"));
            listFunciones.Add(new Funcion("VisitaPadrinosAPAD.aspx", 5, "APAD", "IVP"));
            listFunciones.Add(new Funcion("VisitaPadrinosAPAD.aspx", 5, "APAD", "MVP"));
            listFunciones.Add(new Funcion("VisitaPadrinosAPAD.aspx", 5, "APAD", "RVP"));

            listFunciones.Add(new Funcion("CartaPadrinoAPAD.aspx", 7, "APAD", "BCPM"));
            listFunciones.Add(new Funcion("CartaPadrinoAPAD.aspx", 7, "APAD", "MCPM"));

            listFunciones.Add(new Funcion("EntregaViveres.aspx", 1, "APAD", "BAF"));
            listFunciones.Add(new Funcion("EntregaViveres.aspx", 1, "APAD", "IAF"));
            listFunciones.Add(new Funcion("EntregaViveres.aspx", 1, "APAD", "MAF"));
            listFunciones.Add(new Funcion("EntregaViveres.aspx", 1, "APAD", "RAF"));

            //AFILIACIONES
            listFunciones.Add(new Funcion("AgregarFamilia.aspx", 0, "AFIL", "AF"));
            listFunciones.Add(new Funcion("AgregarSolicitudAfiliacion.aspx", 3, "AFIL", "ASAM"));
            listFunciones.Add(new Funcion("InformeSolicitudesAfiliacion.aspx", 0, "AFIL", "ISAM"));
            listFunciones.Add(new Funcion("AgregarSolicitudDesafiliacion.aspx", 3, "AFIL", "ASDM"));
            listFunciones.Add(new Funcion("InformeSolicitudesDesafiliacion.aspx", 0, "AFIL", "ISDM"));
            listFunciones.Add(new Funcion("InformeNADFAS.aspx", 0, "AFIL", "CCAL"));

            //APOYO A JÓVENES
            listFunciones.Add(new Funcion("Referencias.aspx", (int)Selec.Ninguno, "APJO", "UPAC"));
            listFunciones.Add(new Funcion("Asistencias.aspx", (int)Selec.Ninguno, "APJO", "UPAC"));
            listFunciones.Add(new Funcion("AsistenciasGrupo.aspx", (int)Selec.Ninguno, "APJO", "UPAC"));
            listFunciones.Add(new Funcion("AsistenciasGrupoArchivo.aspx", (int)Selec.Ninguno, "APJO", "UPAC"));
            listFunciones.Add(new Funcion("Seguimiento.aspx", (int)Selec.Ninguno, "APJO", "UPAC"));


            //ADMINISTRACIÓN DE BECAS
            listFunciones.Add(new Funcion("ResumenBecas.aspx", (int)Selec.MiemFamAfilGradDesa, "EDUC", "RHEM"));
            listFunciones.Add(new Funcion("ResumenBecas.aspx", (int)Selec.MiemFamAfilGradDesa, "EDUC", "RIAE"));
            listFunciones.Add(new Funcion("ResumenBecas.aspx", (int)Selec.MiemFamAfilGradDesa, "EDUC", "RPEM"));
            listFunciones.Add(new Funcion("ResumenBecas.aspx", (int)Selec.MiemFamAfilGradDesa, "EDUC", "RREM"));

            listFunciones.Add(new Funcion("HistorialEducati.aspx", (int)Selec.AfilApadGrad, "EDUC", "RHEM"));
            listFunciones.Add(new Funcion("HistorialEducati.aspx", (int)Selec.AfilApadGrad, "EDUC", "RIAE"));
            listFunciones.Add(new Funcion("HistorialEducati.aspx", (int)Selec.AfilApadGrad, "EDUC", "RPEM"));
            listFunciones.Add(new Funcion("HistorialEducati.aspx", (int)Selec.AfilApadGrad, "EDUC", "RREM"));

            listFunciones.Add(new Funcion("RegistroActividadesInd.aspx", (int)Selec.Ninguno, "EDUC", "RPEM"));
            listFunciones.Add(new Funcion("RegistroActividadesInd.aspx", (int)Selec.Ninguno, "EDUC", "RREM"));

            listFunciones.Add(new Funcion("HistorialActividades.aspx", (int)Selec.Apad, "EDUC", "RPEM"));
            listFunciones.Add(new Funcion("HistorialActividades.aspx", (int)Selec.Apad, "EDUC", "RREM"));

            listFunciones.Add(new Funcion("CargaActividadesExcel.aspx", (int)Selec.Ninguno, "EDUC", "RPEM"));
            listFunciones.Add(new Funcion("CargaActividadesExcel.aspx", (int)Selec.Ninguno, "EDUC", "RREM"));

            listFunciones.Add(new Funcion("ActividadesDuplicadas.aspx", (int)Selec.Ninguno, "EDUC", "RPEM"));
            listFunciones.Add(new Funcion("ActividadesDuplicadas.aspx", (int)Selec.Ninguno, "EDUC", "RREM"));
        }
        protected void llenarPaginas()
        {
            listPaginas.Add(new Pagina("Buscar.aspx", "Búsqueda por Número", "Search by Id", (int)Selec.Ninguno, "MISC", false));
            listPaginas.Add(new Pagina("BusquedaFamilias.aspx", "Búsqueda de Familias", "Families Search", (int)Selec.Ninguno, "MISC", false));
            listPaginas.Add(new Pagina("BusquedaMiembrosInfoEduc.aspx", "Búsqueda de Miembros por Info. Educativa", "Members Search by Educational Info.", (int)Selec.Ninguno, "MISC", false));
            listPaginas.Add(new Pagina("BusquedaMiembrosOtraInfo.aspx", "Búsqueda de Miembros por Otra Info.", "Members Search by Other Info.", (int)Selec.Ninguno, "MISC", false));
            listPaginas.Add(new Pagina("PerfilFamilia.aspx", "Perfil de Familia", "Family Profile", (int)Selec.FamRegi, "MISC", false));
            listPaginas.Add(new Pagina("PerfilMiembro.aspx", "Perfil de Miembro", "Member Profile", (int)Selec.MiemFamRegi, "MISC", false));
            listPaginas.Add(new Pagina("CambiarPreferencias.aspx", "Modificar Preferencias", "Modify Preferences", (int)Selec.Ninguno, "MISC", false));
            listPaginas.Add(new Pagina("CambiarContrasena.aspx", "Modificar Contraseña", "Modify Password", (int)Selec.Ninguno, "MISC", false));
            listPaginas.Add(new Pagina("BusquedasOtraInfo.aspx", "Busquedas por Otra Información", "Search by Other Info", (int)Selec.Ninguno, "MISC", false));

            //TRABAJO SOCIAL
            listPaginas.Add(new Pagina("Resumen.aspx", "Resumen de Trabajo Social", "Social Work Information Summary", (int)Selec.FamRegi, "TS", true));
            listPaginas.Add(new Pagina("Historial.aspx", "Historial Familiar", "Family History", 1, "TS", true));
            listPaginas.Add(new Pagina("Visitas.aspx", "Visitas", "Visits", (int)Selec.FamAfilGradDesa, "TS", true));
            listPaginas.Add(new Pagina("CondicionesFamiliares.aspx", "Entorno Familiar", "Family Environment", (int)Selec.FamAfilGradDesa, "TS", true));
            listPaginas.Add(new Pagina("InformacionFamiliar.aspx", "Información General", "General Information", (int)Selec.FamRegi, "TS", true));
            listPaginas.Add(new Pagina("Avisos.aspx", "Avisos Familiares", " Family Warnings", (int)Selec.FamAfilGradDesa, "TS", true));
            listPaginas.Add(new Pagina("AsignacionTS.aspx", "Asignación de Trabajador Social", "Social Worker Assignment", (int)Selec.FamRegi, "TS", true));
            listPaginas.Add(new Pagina("AsignacionTSGrupo.aspx", "Asignación de Trabajador Social por Grupo", "Social Worker Assignment by Group", (int)Selec.Ninguno, "TS", true));
            listPaginas.Add(new Pagina("Clasificacion.aspx", "Clasificación Familiar", "Family Classification", (int)Selec.FamAfilGradDesa, "TS", true));
            listPaginas.Add(new Pagina("ClasificacionAnoSiguiente.aspx", "Clasificación Familiar (Año Siguiente)", "Family Classification (Next Year)", (int)Selec.Ninguno, "TS", true));
            listPaginas.Add(new Pagina("Actividades.aspx", "Actividades Familiares", "Family Activities", (int)Selec.FamAfilGradDesa, "TS", true));
            listPaginas.Add(new Pagina("AnoEscolar.aspx", "Año Escolar (Afiliados)", "School Year (Affiliates)", (int)Selec.FamAfilGradDesa, "TS", true));
            listPaginas.Add(new Pagina("Viveres.aspx", "Autorización de Víveres", "Family Helps Authorization", (int)Selec.FamAfilGradDesa, "TS", true));
            listPaginas.Add(new Pagina("ReportesYEstadisticas.aspx", "Reportes y Estadísticas", "Reports and Statistics", (int)Selec.Ninguno, "TS", true));
            listPaginas.Add(new Pagina("NADFAS.aspx", "NADFAS", "NADFAS", (int)Selec.FamAfilGradDesa, "TS", true));
            listPaginas.Add(new Pagina("AnalisisConstruccionVivienda.aspx", "Análisis Preliminar de Viviendas", "Living Preliminary Analysis", (int)Selec.Ninguno, "TS", true));

            //APADRINAMIENTO
            listPaginas.Add(new Pagina("ResumenAPAD.aspx", "Resumen de Apadrinamiento", "Sponsorship Information Summary", (int)Selec.AfilApadGrad, "APAD", true));
            listPaginas.Add(new Pagina("RegistroRegaloAPAD.aspx", "Regalos", "Gifts", (int)Selec.AfilApadGrad, "APAD", true));
            listPaginas.Add(new Pagina("RegalosSelAPAD.aspx", "Envío de Regalos", "Gifts Sending", (int)Selec.Ninguno, "APAD", true));
            listPaginas.Add(new Pagina("RegistroCartasAPAD.aspx", "Cartas", "Letters", (int)Selec.AfilApadGrad, "APAD", true));
            listPaginas.Add(new Pagina("CartasFamiliaAPAD.aspx", "Cartas por Familia", "Letters by Family", (int)Selec.Ninguno, "APAD", true));
            listPaginas.Add(new Pagina("CartaPadrinoAPAD.aspx", "Cartas de Padrino", "Sponsor Letters", (int)Selec.Ninguno, "APAD", true));
            listPaginas.Add(new Pagina("EnviodeCartaAPAD.aspx", "Envío de Cartas", "Letters Sending", (int)Selec.Ninguno, "APAD", true));
            listPaginas.Add(new Pagina("FotoAPAD.aspx", "Fotos", "Photos", (int)Selec.AfilApadGrad, "APAD", true));
            listPaginas.Add(new Pagina("BusquedaPadrinosAPAD.aspx", "Búsquedas de Padrinos", "Sponsors Search", (int)Selec.Ninguno, "APAD", true));
            listPaginas.Add(new Pagina("PerfilPadrinoAPAD.aspx", "Perfil Padrino", "Sponsor Profile", (int)Selec.Ninguno, "APAD", true));
            listPaginas.Add(new Pagina("VisitaPadrinosAPAD.aspx", " Visitas de Padrinos", "Sponsors Visits", (int)Selec.Ninguno, "APAD", true));
            listPaginas.Add(new Pagina("RegistroCarnetsAPAD.aspx", "Carnés", "Carnets", (int)Selec.Ninguno, "APAD", true));
            listPaginas.Add(new Pagina("RegistroRestriccionesAPAD.aspx", "Restricciones de Apadrinamiento", "Sponsorship Restrictions", (int)Selec.Ninguno, "APAD", true));
            listPaginas.Add(new Pagina("ApadrinadosPendAPAD.aspx", "Apadrinamientos Incompletos", "Incomplete Sponsorship", (int)Selec.Ninguno, "APAD", true));
            listPaginas.Add(new Pagina("ActividadesdeCategoriasAPAD.aspx", "Actividades de Categorias", "Category Activities", (int)Selec.Ninguno, "APAD", true));
            listPaginas.Add(new Pagina("EntregaViveres.aspx", "Entrega de Víveres", "Family Helps Delivery", (int)Selec.FamAfilGradDesa, "APAD", true));
            //  listPaginas.Add(new Pagina("EstadisticasAPAD.aspx", "Estadísticas de Apadrinamiento", "Sponsorship Statics", (int)Selec.Ninguno, "APAD", true));

            //AFILIACIONES
            listPaginas.Add(new Pagina("AgregarFamilia.aspx", "Agregar Familia", "Add Family", 0, "AFIL", true));
            listPaginas.Add(new Pagina("AgregarSolicitudAfiliacion.aspx", "Agregar Solicitud de Afiliación", "Add Affiliation Solicitude", 3, "AFIL", true));
            listPaginas.Add(new Pagina("InformeSolicitudesAfiliacion.aspx", "Informe Solicitudes Afiliación", "Affiliation Solicitudes Report", 0, "AFIL", true));
            listPaginas.Add(new Pagina("AgregarSolicitudDesafiliacion.aspx", "Agregar Solicitud de Desafiliación/Graduación", "Add Disaffiliation/Graduation Solicitude", 3, "AFIL", true));
            listPaginas.Add(new Pagina("InformeSolicitudesDesafiliacion.aspx", "Informe Solicitudes Desafiliaciones/Graduaciones", "Dissafiliations/Graduations Solicitudes Report", 0, "AFIL", true));
            listPaginas.Add(new Pagina("InformeNADFAS.aspx", "Informe de NADFAS", "NADFAS Report", 0, "AFIL", true));

            //APOYO A JÓVENES
            listPaginas.Add(new Pagina("Referencias.aspx", "Referencias", "References", (int)Selec.Ninguno, "APJO", true));
            listPaginas.Add(new Pagina("Asistencias.aspx", "Registro de Asistencias", "Asistances Registration", (int)Selec.Ninguno, "APJO", true));
            listPaginas.Add(new Pagina("AsistenciasGrupo.aspx", "Registro de Asistencias en Grupo", "Assistances Registration by Group", (int)Selec.Ninguno, "APJO", true));
            listPaginas.Add(new Pagina("AsistenciasGrupoArchivo.aspx", "Registro de Asistencias por Archivo", "Assistances Registration from File", (int)Selec.Ninguno, "APJO", true));
            listPaginas.Add(new Pagina("Seguimiento.aspx", "Seguimiento", "Tracing", (int)Selec.Ninguno, "APJO", true));

            //ADMINISTRACIÓN DE BECAS
            listPaginas.Add(new Pagina("ResumenBecas.aspx", "Resumen Becas", "Becas Information Sumary", (int)Selec.MiemFamAfilGradDesa, "EDUC", true));
            listPaginas.Add(new Pagina("HistorialEducati.aspx", "Historial Educativo", "Educational History", (int)Selec.AfilApadGrad, "EDUC", true));
            listPaginas.Add(new Pagina("RegistroActividadesInd.aspx", "Registro de Actividades", "Activity Log", (int)Selec.Ninguno, "EDUC", true));
            listPaginas.Add(new Pagina("HistorialActividades.aspx", "Historial de Actividades", "Activity History", (int)Selec.Apad, "EDUC", true));
            listPaginas.Add(new Pagina("CargaActividadesExcel.aspx", "Carga de Actividades Excel", "-", (int)Selec.Apad, "EDUC", true));
            listPaginas.Add(new Pagina("ActividadesDuplicadas.aspx", "Actividades Duplicadas", "-", (int)Selec.Apad, "EDUC", true));
            
        }
        public int retornaSeguridadSeleccion(String page)
        {
            int sSel = 0;
            foreach (Pagina f in listPaginas)
            {
                if (page == f.page)
                {
                    sSel = f.sel;
                }
            }
            return sSel;
        }
        public DataTable retornaAreasMenu(String S, String L, String U)
        {
            DataTable dtFunciones = bdU.obtenerFuncionesDeUsuario(U, S);
            DataTable dtAreas = bdU.obtenerAreas(S, L);
            DataTable dtAreasMenu = new DataTable();
            dtAreasMenu.Columns.Add("Des");
            dtAreasMenu.Columns.Add("Code");
            Boolean areaEstaEnApp;
            foreach (DataRow rowArea in dtAreas.Rows)
            {
                areaEstaEnApp = false;
                foreach (Funcion funcion in listFunciones)
                {
                    if (funcion.area.Equals(rowArea["Code"].ToString()))
                    {
                        foreach (DataRow rowFuncion in dtFunciones.Rows)
                        {
                            if (rowFuncion["Code"].ToString().Equals(funcion.code))
                            {
                                foreach (Pagina pagina in listPaginas)
                                {
                                    if (pagina.page.Equals(funcion.page) && (pagina.enMenu))
                                    {
                                        areaEstaEnApp = true;
                                    }
                                }
                            }
                        }
                    }
                }
                if (areaEstaEnApp)
                {
                    dtAreasMenu.Rows.Add(rowArea["Des"].ToString(), rowArea["Code"].ToString());
                }
            }
            return dtAreasMenu;
        }
        public String retornaTitulo(String page, String L)
        {
            String desc = "";
            foreach (Pagina p in listPaginas)
            {
                if (page == p.page)
                {
                    if (L == "es")
                    {
                        desc = p.desS;
                    }
                    else
                    {
                        desc = p.desE;
                    }
                }
            }
            return desc;
        }

        public DataTable retornaNombresPaginasMenu(String U, String S, String L, String area)
        {
            DataTable tblFunciones = bdU.obtenerFuncionesDeUsuarioTodas(U, S, L, area);
            DataTable tblPaginas = new DataTable();
            tblPaginas.Columns.Add("Des");
            tblPaginas.Columns.Add("Link");
            Boolean incluirPagina;
            foreach (Pagina p in listPaginas)
            {
                incluirPagina = false;
                if (p.area.Equals(area))
                {
                    foreach (Funcion funcion in listFunciones)
                    {
                        if (funcion.page.Equals(p.page))
                        {
                            foreach (DataRow rowFuncion in tblFunciones.Rows)
                            {
                                if (rowFuncion["Code"].ToString().Equals(funcion.code))
                                {
                                    incluirPagina = true;
                                }
                            }
                        }
                    }
                }
                if (incluirPagina)
                {
                    if (L.Equals("es"))
                    {
                        tblPaginas.Rows.Add(p.desS, p.page);
                    }
                    else
                    {
                        tblPaginas.Rows.Add(p.desE, p.page);
                    }
                }
            }
            return tblPaginas;
        }
        public Boolean paginaEsPermitida(String U, String S, String pagina)
        {
            DataTable tblFunciones = bdU.obtenerFuncionesDeUsuario(U, S);
            Boolean paginaEsPermitida = false;
            foreach (Funcion f in listFunciones)
            {
                if (f.page.Equals(pagina))
                {
                    foreach (DataRow funcion in tblFunciones.Rows)
                    {
                        if (f.code.Equals(funcion["Code"].ToString()))
                        {
                            paginaEsPermitida = true;
                        }
                    }
                }
            }
            return paginaEsPermitida;
        }
    }
}