using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;

namespace Familias3._1
{
    public class Diccionario
    {
        private String L;
        private String S;

        public Diccionario(String idioma, String sitio)
        {
            this.L = idioma;
            this.S = sitio;
        }

        public String obtenerMesAbr(int numeroMes){
            try
            {
                String monthName = new DateTime(1900, numeroMes, 1).ToString("MMM", CultureInfo.CreateSpecificCulture(L));
                return monthName;
            }
            catch
            {
                return "Desconocido";
            } 
        }

        public String dicImageURL
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "../Images/FamiliasdeEsperanza_Logo_RGBHeader.png";
                }
                else
                {
                    return "../Images/FamiliasdeEsperanza_Logo_RGBHeader.png";
                    //return "../Images/CommonHope_Logo_RGBHeader.png";
                }
            }
        }
        public String msjCambiarIdioma
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cambiar idioma a Inglés";
                }
                else
                {
                    return "Changue language to Spanish";
                }
            }
        }
        public String msjCampoNecesario
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Este campo es necesario.";
                }
                else
                {
                    return "This field is required.";
                }
            }
        }
        public String msjDebeingresarUno
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Debe ingresar uno de los parámetros.";
                }
                else
                {
                    return "You must enter one of the parameters.";
                }
            }
        }
        public String msjEliminarRegistro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "¿Está seguro de eliminar el registro?";
                }
                else
                {
                    return "Are you sure to delete the record?";
                }
            }
        }
        public String msjEsaNoEsPsw
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Esa no es su contraseña actual.";
                }
                else
                {
                    return "That is not your current password.";
                }
            }
        }
        public String msjFormatoFecha
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "(dd, mm, aaaa)";
                }
                else
                {
                    return "(dd, mm, yyyy)";
                }
            }
        }
        public String msjFechaIncorrecta
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "La fecha ingresada no es correcta.";
                }
                else
                {
                    return "The date entered is not correct.";
                }
            }
        }

        public String msjNoEncontroFamilia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No se encontró a ninguna familia.";
                }
                else
                {
                    return "No family found.";
                }
            }
        }
        public String msjNoEncontroMiembro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No se encontró a ningún miembro.";
                }
                else
                {
                    return "No member found.";
                }
            }
        }
        public String msjNoEncontroResultados
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No se encontraron resultados.";
                }
                else
                {
                    return "No results found.";
                }
            }
        }
        public String msjNoSePermiteMismaPsw
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No se puede actualizar, ya que ingresó la misma contraseña.";
                }
                else
                {
                    return "Cannot be updated, since you entered the same password.";
                }
            }
        }
        public String msjNoSeRealizo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No se realizó la acción.";
                }
                else
                {
                    return "The action could not be performed.";
                }
            }
        }
        public String msjNoSeRealizoExcp
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No se realizó la acción. Por favor, envíe el siguiente mensaje a Sistemas: ";
                }
                else
                {
                    return "The action could not be performed. Please, show the following message to IT department: ";
                }
            }
        }
        public String msjNoSonIdenticosPsw
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Esos valores, no son idénticos.";
                }
                else
                {
                    return "Those values ​​are not identical.";
                }
            }
        }
        public String msjParametroNecesario
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Este parámetro es necesario.";
                }
                else
                {
                    return "This parameter is required.";
                }
            }
        }
        public String msjSeHaActualizado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Se han registrado los cambios, exitosamente.";
                }
                else
                {
                    return "The changes have been registered, successfully.";
                }
            }
        }
        public String msjSeHaIngresado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Se ha guardado el registro, exitosamente.";
                }
                else
                {
                    return "The record has been saved, successfully.";
                }
            }
        }
        public String msjSeHaEliminado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Se ha eliminado el registro, exitosamente.";
                }
                else
                {
                    return "The record has been eliminated, successfully.";
                }
            }
        }

        public String msjSeHanIngresado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Se han guardado los registros, exitosamente.";
                }
                else
                {
                    return "The records has been saved, successfully.";
                }
            }
        }
        public String Si
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Sí";
                }
                else
                {
                    return "Yes";
                }
            }
        }
        public String No
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No";
                }
                else
                {
                    return "No";
                }
            }
        }
        public String accion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Acción";
                }
                else
                {
                    return "Action";
                }
            }
        }
        public String acciones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Acciones";
                }
                else
                {
                    return "Actions";
                }
            }
        }
        public String aceptar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Aceptar";
                }
                else
                {
                    return "Accept";
                }
            }
        }
        public String actividad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actividad";
                }
                else
                {
                    return "Activity";
                }
            }
        }
        public String actividades
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actividades";
                }
                else
                {
                    return "Activities";
                }
            }
        }
        public String actividadesFamiliares
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actividades Familiares";
                }
                else
                {
                    return "Family Activities";
                }
            }
        }
        public String activo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Activo";
                }
                else
                {
                    return "Active";
                }
            }
        }
        public String actualPsw
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actual Contraseña";
                }
                else
                {
                    return "Current Password";
                }
            }
        }
        public String actualizacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actualización";
                }
                else
                {
                    return "Update";
                }
            }
        }
        public String actualizar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actualizar";
                }
                else
                {
                    return "Update";
                }
            }
        }
        public String actualizarFamilia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actualizar Familia";
                }
                else
                {
                    return "Update Family";
                }
            }
        }
        public String actualizarMiembro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actualizar Miembro";
                }
                else
                {
                    return "Update Member";
                }
            }
        }
        public String advertencia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Advertencia";
                }
                else
                {
                    return "Warning";
                }
            }
        }
        public String afiliacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Afiliación";
                }
                else
                {
                    return "Affiliation";
                }
            }
        }
        public String afilEstado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Estado de Afiliación";
                }
                else
                {
                    return "Affiliation Status";
                }
            }
        }
        public String afiliado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Afiliado";
                }
                else
                {
                    return "Affiliate";
                }
            }
        }
        public String afiliados
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Afiliados";
                }
                else
                {
                    return "Affiliates";
                }
            }
        }
        public String afilEstadoFecha
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha Estado de Afiliación";
                }
                else
                {
                    return "Affiliation Status Date";
                }
            }
        }
        public String afilNivel
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nivel de Afiliación";
                }
                else
                {
                    return "Affiliation Level";
                }
            }
        }
        public String afilTIpo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tipo de Afiliación";
                }
                else
                {
                    return "Affiliation Type";
                }
            }
        }
        public String año
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Año";
                }
                else
                {
                    return "Year";
                }
            }
        }
        public String añoEducativo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Año Educativo";
                }
                else
                {
                    return "School Year";
                }
            }
        }
        public String añoProximaClasificacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Año Próxima Clasificación";
                }
                else
                {
                    return "Next Classification Year";
                }
            }
        }
        public String añoUltimoGrado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Año Último Grado";
                }
                else
                {
                    return "Last Grade Year";
                }
            }
        }
        public String apadrinado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Apadrinado";
                }
                else
                {
                    return "Sponsored";
                }
            }
        }
        public String apadrinados
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Apadrinados";
                }
                else
                {
                    return "Sponsored";
                }
            }
        }
        public String apadrinamiento
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Apadrinamiento";
                }
                else
                {
                    return "Sponsorship";
                }
            }
        }
        public String apellidos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Apellidos";
                }
                else
                {
                    return "Last Names";
                }
            }
        }
        public String aplica
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Aplica";
                }
                else
                {
                    return "Apply";
                }
            }
        }
        public String apoyoEducativo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Apoyo Educativo";
                }
                else
                {
                    return "Educational Support";
                }
            }
        }
        public String apoyoJovenes
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Apoyo a Jóvenes";
                }
                else
                {
                    return "Youth Support";
                }
            }
        }
        public String aproboTodas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Aprobó Todas";
                }
                else
                {
                    return "Approve All";
                }
            }
        }
        public String asignar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Asignar";
                }
                else
                {
                    return "Assign";
                }
            }
        }
        public String area
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Área";
                }
                else
                {
                    return "Area";
                }
            }
        }
        public String autorizadoPor
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Autorizado Por";
                }
                else
                {
                    return "Authorized By";
                }
            }
        }
        public String aviso
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Aviso";
                }
                else
                {
                    return "Warning";
                }
            }
        }
        public String avisos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Avisos";
                }
                else
                {
                    return "Warnings";
                }
            }
        }
        public String avisosFamiliares
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Avisos Familiares";
                }
                else
                {
                    return "Family Warnings";
                }
            }
        }
        public String becas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Becas";
                }
                else
                {
                    return "Education";
                }
            }
        }
        public String beneficios
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Derechos";
                }
                else
                {
                    return "Rights";
                }
            }
        }
        public String buscar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Buscar";
                }
                else
                {
                    return "Search";
                }
            }
        }
        public String celular
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Celular";
                }
                else
                {
                    return "Mobile";
                }
            }
        }
        public String cerrarSesion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cerrar Sesión";
                }
                else
                {
                    return "Log Out";
                }
            }
        }
        public String caja
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Caja";
                }
                else
                {
                    return "Accounting";
                }
            }
        }
        public String calificaciones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Calificaciones";
                }
                else
                {
                    return "Grades";
                }
            }
        }
        public String cambiarPref
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cambiar Preferencias";
                }
                else
                {
                    return "Changue Preferences";
                }
            }
        }
        public String cambiarPsw
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cambiar Contraseña";
                }
                else
                {
                    return "Changue Password";
                }
            }
        }
        public String cancelar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cancelar";
                }
                else
                {
                    return "Cancel";
                }
            }
        }
        public String candidaturasSegundasAfil
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Candidaturas a Segundas Afiliaciones";
                }
                else
                {
                    return "Second Affilitions Candidatures";
                }
            }
        }
        public String cantidad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cantidad";
                }
                else
                {
                    return "Quantity";
                }
            }
        }
        public String cantidadPerdidas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cantidad Perdidas";
                }
                else
                {
                    return "Failed Amount";
                }
            }
        }
        public String carrera
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Carrera Educativa";
                }
                else
                {
                    return "Education Career";
                }
            }
        }
        public String categoria
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Categoría";
                }
                else
                {
                    return "Category";
                }
            }
        }
        public String centroEducativo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Centro Educativo";
                }
                else
                {
                    return "School";
                }
            }
        }

        public String comentario
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Comentario";
                }
                else
                {
                    return "Commentary";
                }
            }
        }

        public String condicion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Condición";
                }
                else
                {
                    return "Condition";
                }
            }
        }
        public String condiciones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Condiciones";
                }
                else
                {
                    return "Conditions";
                }
            }
        }
        public String copiar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Copiar";
                }
                else
                {
                    return "Copy";
                }
            }
        }
        public String corregirDatos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Corregir los Datos";
                }
                else
                {
                    return "Correct the Data";
                }
            }
        }
        public String clasificacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Clasificación";
                }
                else
                {
                    return "Classification";
                }
            }
        }
        public String clasificacionActual
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Clasificación Actual";
                }
                else
                {
                    return "Current Classification";
                }
            }
        }
        public String clasificacionFecha
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha Clasificación";
                }
                else
                {
                    return "Classification Date";
                }
            }
        }
        public String clinica
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Clínica";
                }
                else
                {
                    return "Clinic";
                }
            }
        }
        public String condicionesVivienda
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Condiciones de Vivienda";
                }
                else
                {
                    return "Living Conditions";
                }
            }
        }
        public String confirmarPsw
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Confirmar Contraseña";
                }
                else
                {
                    return "Confirm Password";
                }
            }
        }
        public String cuentaActiva
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cuenta Activa";
                }
                else
                {
                    return "Active Account";
                }
            }
        }
        public String desafiliado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Desafiliado";
                }
                else
                {
                    return "Disaffiliated";
                }
            }
        }
        public String desafiliados
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Desafiliados";
                }
                else
                {
                    return "Disaffiliated";
                }
            }
        }
        public String diagnostico
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Diagnóstico";
                }
                else
                {
                    return "Diagnosis";
                }
            }
        }
        public String direccion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Dirección";
                }
                else
                {
                    return "Address";
                }
            }
        }
        public String DPI
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "DPI";
                }
                else
                {
                    return "DPI";
                }
            }
        }
        public String edad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Edad";
                }
                else
                {
                    return "Age";
                }
            }
        }
        public String educacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Educación";
                }
                else
                {
                    return "Education";
                }
            }
        }
        public String eliminar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Eliminar";
                }
                else
                {
                    return "Delete";
                }
            }
        }
        public String empleados
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Empleados";
                }
                else
                {
                    return "Employees";
                }
            }
        }
        public String entregas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Entregas";
                }
                else
                {
                    return "Deliveries";
                }
            }
        }
        public String español
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Español";
                }
                else
                {
                    return "Spanish";
                }
            }
        }
        public String estado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Estado";
                }
                else
                {
                    return "Status";
                }
            }
        }
        public String estadoEducativo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Estado Educativo";
                }
                else
                {
                    return "Education Status";
                }
            }
        }
        public String estadoUltimoGrado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Estado Último Grado";
                }
                else
                {
                    return "Last Grade Status";
                }
            }
        }
        public String etnia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Étnia";
                }
                else
                {
                    return "Ethnic Group";
                }
            }
        }
        public String excepcionEstadoEducativo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Excepción de Estado Educativo";
                }
                else
                {
                    return "Education Status Exception";
                }
            }
        }
        public String exoneracion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Exoneración";
                }
                else
                {
                    return "Exoneration";
                }
            }
        }
        public String familia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Familia";
                }
                else
                {
                    return "Family";
                }
            }
        }
        public String familiaresEmpleados
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Familiares de Empleados";
                }
                else
                {
                    return "Employees Relatives";
                }
            }
        }
        public String familias
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Familias";
                }
                else
                {
                    return "Families";
                }
            }
        }
        public String familiaSeleccionada
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Familia Seleccionada";
                }
                else
                {
                    return "Selected Family";
                }
            }
        }
        public String fase
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fase";
                }
                else
                {
                    return "Phase";
                }
            }
        }
        public String fecha
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha";
                }
                else
                {
                    return "Date";
                }
            }
        }
        public String fechaActividad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Actividad";
                }
                else
                {
                    return "Activity Date";
                }
            }
        }
        public String fechaAutorizacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Autorización";
                }
                else
                {
                    return "Authorization Date";
                }
            }
        }
        public String fechaEntrega
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Entrega";
                }
                else
                {
                    return "Delivery Date";
                }
            }
        }
        public String fechaFallecimiento
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha Fallecimiento";
                }
                else
                {
                    return "Death Date";
                }
            }
        }
        public String fechaClasifActual
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha Clasificación Actual";
                }
                else
                {
                    return "Current Classification Date";
                }
            }
        }
        public String fechaInicio
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Inicio";
                }
                else
                {
                    return "Start Date";
                }
            }
        }
        public String fechaFin
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Fin";
                }
                else
                {
                    return "End Date";
                }
            }
        }
        public String fechaNacimiento
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Nacimiento";
                }
                else
                {
                    return "Birth Date";
                }
            }
        }

        public String fechaRegistro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Registro";
                }
                else
                {
                    return "Registered Date";
                }
            }
        }

        public String fechaUltimaVisita
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha Última Visita";
                }
                else
                {
                    return "Last Visit Date";
                }
            }
        }
        public String frecuencia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Frecuencia";
                }
                else
                {
                    return "Frecuency";
                }
            }
        }
        public String fuente
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fuente";
                }
                else
                {
                    return "Source";
                }
            }
        }
        public String gastos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Gastos";
                }
                else
                {
                    return "Expenses";
                }
            }
        }
        public String generar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Generar";
                }
                else
                {
                    return "Generate";
                }
            }
        }
        public String genero
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Género";
                }
                else
                {
                    return "Gender";
                }
            }
        }
        public String grado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Grado";
                }
                else
                {
                    return "Grade";
                }
            }
        }
        public String graduado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Graduado";
                }
                else
                {
                    return "Graduate";
                }
            }
        }
        public String graduados
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Graduados";
                }
                else
                {
                    return "Graduates";
                }
            }
        }
        public String guardar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Guardar";
                }
                else
                {
                    return "Save";
                }
            }
        }
        public String guardarVisita
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Guardar Visita";
                }
                else
                {
                    return "Save Visita";
                }
            }
        }
        public String horasRequeridas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Horas Requeridas";
                }
                else
                {
                    return "Required Hours";
                }
            }
        }
        public String horasTrabajadas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Horas Trabajadas";
                }
                else
                {
                    return "Hours Worked";
                }
            }
        }
        public String id
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Número";
                }
                else
                {
                    return "Id";
                }
            }
        }
        public String idFamilia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Número de Familia";
                }
                else
                {
                    return "Family Id";
                }
            }
        }
        public String idFaro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Número de Faro";
                }
                else
                {
                    return "Faro Id";
                }
            }
        }
        public String idioma
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Idioma";
                }
                else
                {
                    return "Language";
                }
            }
        }
        public String inactivo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Inactivo";
                }
                else
                {
                    return "Inactive";
                }
            }
        }
        public String inactivos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Inactivos";
                }
                else
                {
                    return "Inactive";
                }
            }
        }
        public String inactivoFecha
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha Inactivo";
                }
                else
                {
                    return "Inactive Date";
                }
            }
        }
        public String inactivoRazon
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Razón Inactivo";
                }
                else
                {
                    return "Inactive Reason";
                }
            }
        }
        public String incluirInfoEduc
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Incluir Info. Educativa";
                }
                else
                {
                    return "Include Educative Info.";
                }
            }
        }
        public String infoGeneral
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Información General";
                }
                else
                {
                    return "General Information";
                }
            }
        }
        public String ingles
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Inglés";
                }
                else
                {
                    return "English";
                }
            }
        }
        public String ingresar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresar";
                }
                else
                {
                    return "Enter";
                }
            }
        }
        public String ingresarMiembro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresar Miembro";
                }
                else
                {
                    return "Add Member";
                }
            }
        }

        public String ingresosExtra
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresos Extra";
                }
                else
                {
                    return "Additional Incomes";
                }
            }
        }
        public String informacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Información";
                }
                else
                {
                    return "Information";
                }
            }
        }
        public String jefeCasa
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Jefe de Casa";
                }
                else
                {
                    return "Head of House";
                }
            }
        }
        public String lee
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Lee";
                }
                else
                {
                    return "Literacy";
                }
            }
        }
        public String maestro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Maestro";
                }
                else
                {
                    return "Teacher";
                }
            }
        }
        public String medico
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Médico";
                }
                else
                {
                    return "Medic";
                }
            }
        }
        public String mensaje
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Mensaje";
                }
                else
                {
                    return "Message";
                }
            }
        }
        public String menu
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Menú";
                }
                else
                {
                    return "Menu";
                }
            }
        }
        public String mesesAtras
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Meses Atrás";
                }
                else
                {
                    return "Months Ago";
                }
            }
        }

        public String miembro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Miembro";
                }
                else
                {
                    return "Member";
                }
            }
        }
        public String miembros
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Miembros";
                }
                else
                {
                    return "Members";
                }
            }
        }
        public String MiembrosPorOtraInfo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Miembros por Otra Información";
                }
                else
                {
                    return "Members by Other Information";
                }
            }
        }
        public String MiembrosPorEducInfo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Miembros por Información de Educación";
                }
                else
                {
                    return "Members by Educational Information ";
                }
            }
        }
        public String miembroSeleccionado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Miembro Seleccionado";
                }
                else
                {
                    return "Selected Member";
                }
            }
        }
        public String miembrosFamDes
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Miembros de Familia Desafiliada";
                }
                else
                {
                    return "Members of a Disaffiliated Family";
                }
            }
        }
        public String miembrosFamGrad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Miembros de Familia Graduada";
                }
                else
                {
                    return "Members of a Graduate Family";
                }
            }
        }
        public String mostrarInactivos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Mostrar Inactivos";
                }
                else
                {
                    return "Show Inactive";
                }
            }
        }
        public String mostrarSoloActivos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Mostrar Solo Activos";
                }
                else
                {
                    return "Show Only Active";
                }
            }
        }
        public String municipio
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Municipio";
                }
                else
                {
                    return "Municipality";
                }
            }
        }
        public String idMiembro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Número de Miembro";
                }
                else
                {
                    return "Member Id";
                }
            }
        }
        public String nacimiento
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nacimiento";
                }
                else
                {
                    return "Birthdate";
                }
            }
        }
        public String nivelEduc{
            get
            {
                if (L.Equals("es"))
                {
                    return "Nivel Educativo";
                }
                else
                {
                    return "Education Level";
                }
            }
        }
        public String nombre
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nombre";
                }
                else
                {
                    return "Name";
                }
            }
        }
        public String nombres
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nombres";
                }
                else
                {
                    return "First Names";
                }
            }
        }
        public String nombreMiembro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nombre de Miembro";
                }
                else
                {
                    return "Member's Name";
                }
            }
        }
        public String nombreMadre
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nombre de la Madre";
                }
                else
                {
                    return "Mother's Name";
                }
            }
        }
        public String nombrePadre
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nombre del Padre";
                }
                else
                {
                    return "Father's Name";
                }
            }
        }
        public String nombrePadrino
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nombre de Padrino";
                }
                else
                {
                    return "Sponsor Name";
                }
            }
        }
        public String nombreUsual
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nombre Usual";
                }
                else
                {
                    return "Usual Name";
                }
            }
        }
        public String nota
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nota";
                }
                else
                {
                    return "Note";
                }
            }
        }
        public String notaLibre
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nota Libre";
                }
                else
                {
                    return "Open Note";
                }
            }
        }
        public String notas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Notas";
                }
                else
                {
                    return "Notes";
                }
            }
        }
        public String noTiene
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No Tiene";
                }
                else
                {
                    return "Does not Have";
                }
            }
        }
        public String noTieneDerechos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No Tiene Derechos";
                }
                else
                {
                    return "Does not Have Rights";
                }
            }
        }
        public String nuevaBusqueda
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nueva Búsqueda";
                }
                else
                {
                    return "New Search";
                }
            }
        }
        public String nuevaSeleccion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nueva Selección";
                }
                else
                {
                    return "New Selection";
                }
            }
        }

        public String nuevaPsw
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nueva Contraseña";
                }
                else
                {
                    return "New Password";
                }
            }
        }

        public String nuevoMiembro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nuevo Miembro";
                }
                else
                {
                    return "New Member";
                }
            }
        }

        public String nuevoTS
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nuevo Trabajador Social";
                }
                else
                {
                    return "New Social Worker";
                }
            }
        }

        public String numeroMadre
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Número de Madre";
                }
                else
                {
                    return "Mother's Id";
                }
            }
        }
        public String numeroPadre
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Número de Padre";
                }
                else
                {
                    return "Father's Id";
                }
            }
        }
        public String observaciones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Observaciones";
                }
                else
                {
                    return "Observations";
                }
            }
        }
        public String ocupaciones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ocupaciones";
                }
                else
                {
                    return "Occupations";
                }
            }
        }
        public String ordenarPor
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ordenar Por";
                }
                else
                {
                    return "Order By";
                }
            }
        }
        public String otraAfil
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Otra Afiliación";
                }
                else
                {
                    return "Other Affiliation";
                }
            }
        }
        public String otros
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Otros";
                }
                else
                {
                    return "Others";
                }
            }
        }
        public String padres
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Padres";
                }
                else
                {
                    return "Parents";
                }
            }
        }
        public String padresBiologicos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Padres Biológicos";
                }
                else
                {
                    return "Biologycal Parents";
                }
            }
        }
        public String padrinos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Padrinos";
                }
                else
                {
                    return "Sponsorships";
                }
            }
        }
        public String predeterminados
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Predeterminados";
                }
                else
                {
                    return "Default";
                }
            }
        }
        public String principales
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Principales";
                }
                else
                {
                    return "Main";
                }
            }
        }
        public String programa
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Programa";
                }
                else
                {
                    return "Program";
                }
            }
        }
        public String programasEducativos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Programas Educativos";
                }
                else
                {
                    return "Educational Programs";
                }
            }
        }
        public String problemasLegales
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Problemas Legales";
                }
                else
                {
                    return "Legal Issues";
                }
            }
        }
        public String proximoGrado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Próximo Grado";
                }
                else
                {
                    return "Next Grade";
                }
            }
        }
        public String PorId
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Por Número";
                }
                else
                {
                    return "By Id";
                }
            }
        }
        public String posesiones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Posesiones";
                }
                else
                {
                    return "Possessions";
                }
            }
        }
        public String proximaClasificacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Próxima Clasificación";
                }
                else
                {
                    return "Next Classification";
                }
            }
        }
        public String pueblo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Pueblo";
                }
                else
                {
                    return "Pueblo";
                }
            }
        }
        public String razon
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Razón";
                }
                else
                {
                    return "Reason";
                }
            }
        }
        public String region
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Región";
                }
                else
                {
                    return "Region";
                }
            }
        }
        public String regresar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Regresar";
                }
                else
                {
                    return "Return";
                }
            }
        }
        public String relacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Relación";
                }
                else
                {
                    return "Relation";
                }
            }
        }
        public String relaciones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Relaciones";
                }
                else
                {
                    return "Relations";
                }
            }
        }
        public String relacionesFamiliares
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Relaciones Familiares";
                }
                else
                {
                    return "Family Relations";
                }
            }
        }
        public String repetir
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Repetir";
                }
                else
                {
                    return "Repeat";
                }
            }
        }
        public String salud
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Salud";
                }
                else
                {
                    return "Health";
                }
            }
        }

        public String seccion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Sección";
                }
                else
                {
                    return "Section";
                }
            }
        }

        public String seleccionar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Seleccionar";
                }
                else
                {
                    return "Select";
                }
            }
        }
        public String semaforo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Semáforo";
                }
                else
                {
                    return "Semaphore";
                }
            }
        }
        public String sitio
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Sitio";
                }
                else
                {
                    return "Site";
                }
            }
        }
        public String solicitud
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Solicitud";
                }
                else
                {
                    return "Request";
                }
            }
        }
        public String tiempoDeVivir
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tiempo de Vivir en el Lugar";
                }
                else
                {
                    return "Time Living in this Place";
                }
            }
        }
        public String tieneCUI
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tiene Fe de Edad / No. Cedula";
                }
                else
                {
                    return "Have Birth Certificate / Official Id";
                }
            }
        }
        public String tipo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tipo";
                }
                else
                {
                    return "Type";
                }
            }
        }
        public String tipoEscuela
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tipo Centro Educativo";
                }
                else
                {
                    return "School Type";
                }
            }
        }
        public String tipoMiembro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tipo de Miembro";
                }
                else
                {
                    return "Member Type";
                }
            }
        }
        public String total
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Total";
                }
                else
                {
                    return "Total";
                }
            }
        }
        public String trabajadorS
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Trabajador Social";
                }
                else
                {
                    return "Social Worker";
                }
            }
        }
        public String trabajoSocial
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Trabajo Social";
                }
                else
                {
                    return "Social Work";
                }
            }
        }
        public String telefono
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Número de Teléfono";
                }
                else
                {
                    return "Phone Number";
                }
            }
        }
        public String telefonos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Números de Teléfono";
                }
                else
                {
                    return "Phone Numbers";
                }
            }
        }
        public String ultimaActualizacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Última Actualización";
                }
                else
                {
                    return "Last Update";
                }
            }
        }
        public String ultimoGrado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Último Grado";
                }
                else
                {
                    return "Last Grade";
                }
            }
        }
        public String unidad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Unidad";
                }
                else
                {
                    return "Unity";
                }
            }
        }
        public String usuario
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Usuario";
                }
                else
                {
                    return "User";
                }
            }
        }
        public String visita
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Visita";
                }
                else
                {
                    return "Visit";
                }
            }
        }
        public String visitas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Visitas";
                }
                else
                {
                    return "Visits";
                }
            }
        }
        public String vivoOmuerto
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Vivo/Muerto";
                }
                else
                {
                    return "Live/Dead";
                }
            }
        }
        public String vivienda
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Vivienda";
                }
                else
                {
                    return "Living Place";
                }
            }
        }
        public String viviendas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Viviendas";
                }
                else
                {
                    return "Housing";
                }
            }
        }
        public String TSadicciones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Adicciones";
                }
                else
                {
                    return "Adictions";
                }
            }
        }
        public String TSactividadesApad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actividades de Apadrinamiento";
                }
                else
                {
                    return "Sponsorship Activities";
                }
            }
        }
        public String TSactividadesApoyoEduc
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actividades de Apoyo Educativo";
                }
                else
                {
                    return "Educational Support Activities";
                }
            }
        }
        public String TSactividadesApoyoJov
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actividades de Apoyo a Jóvenes";
                }
                else
                {
                    return "Youth Support Activities";
                }
            }
        }
        public String TSactividadesClinica
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actividades de Clínica";
                }
                else
                {
                    return "Clinic Activities";
                }
            }
        }
        public String TSactividadesBecas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actividades de Educación";
                }
                else
                {
                    return "Education Activities";
                }
            }
        }
        public String TSactividadesTS
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actividades de Trabajo Social";
                }
                else
                {
                    return "Social Work Activities";
                }
            }
        }

        public String TSactualizarClasif
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actualizar Clasificación Actual";
                }
                else
                {
                    return "Update Current Classification";
                }
            }
        }
        public String TSactualizarOcupacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actualizar Ocupación";
                }
                else
                {
                    return "Update Occupation";
                }
            }
        }
        public String TSactualizarVisita
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Actualizar Visita";
                }
                else
                {
                    return "Update Visit";
                }
            }
        }
        public String TSagua
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Agua";
                }
                else
                {
                    return "Water";
                }
            }
        }
        public String TSagregarMiembroOtraFam
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Agregar Miembro de Otra Familia";
                }
                else
                {
                    return "Add Another Family Member";
                }
            }
        }
        public String TSalcoholismo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Alcoholismo";
                }
                else
                {
                    return "Alcoholism";
                }
            }
        }
        public String TSantecedentesFamiliares
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Antecedentes Familiares";
                }
                else
                {
                    return "Family Background";
                }
            }
        }
        public String TSantecedentesPyP
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Antecedentes Penales y Policia";
                }
                else
                {
                    return "Criminal and Police Records";
                }
            }
        }
        public String TSañoSeleccionado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Año Seleccionado";
                }
                else
                {
                    return "Selected Year";
                }
            }
        }
        public String TSasignarRelacionMiembroOtraFamilia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Asignar Relacion a Miembro de Otra Familia";
                }
                else
                {
                    return "Assign Relation from Another Family Member";
                }
            }
        }
        public String TSaporteMensual
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Aporte Mensual";
                }
                else
                {
                    return "Monthly Contribution";
                }
            }
        }
        public String fechaAviso
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Aviso";
                }
                else
                {
                    return "Warning Date";
                }
            }
        }
        public String TSbaño
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Baño";
                }
                else
                {
                    return "Bathroom";
                }
            }
        }
        public String TScantidad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cantidad";
                }
                else
                {
                    return "Quantity";
                }
            }
        }

        public String TScalidad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Calidad";
                }
                else
                {
                    return "Quality";
                }
            }
        }
        public String TScasa
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Casa";
                }
                else
                {
                    return "House";
                }
            }
        }

        public String TSclasificacionAñoSelec
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Clasificación de Año Seleccionado";
                }
                else
                {
                    return "Selected Year Classification";
                }
            }
        }

        public String TScocina
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cocina";
                }
                else
                {
                    return "Kitchen";
                }
            }
        }
        public String TScronicas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Crónicas";
                }
                else
                {
                    return "Chronic Diseases";
                }
            }
        }
        public String TSdesercion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Deserción";
                }
                else
                {
                    return "Desertion";
                }
            }
        }
        public String TSdivorcio
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Divorcio";
                }
                else
                {
                    return "Divorce";
                }
            }
        }
        public String TSDPI
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "DPI";
                }
                else
                {
                    return "Identification Document";
                }
            }
        }
        public String TSdrenaje
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Drenaje";
                }
                else
                {
                    return "Drainage";
                }
            }
        }
        public String TSdrogadiccion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Drogadicción";
                }
                else
                {
                    return "Addiction";
                }
            }
        }
        public String TSeconomia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Economía";
                }
                else
                {
                    return "Economy";
                }
            }
        }
        public String TSelectricidad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Electricidad";
                }
                else
                {
                    return "Electricity";
                }
            }
        }
        public String TSemocional
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Emocional";
                }
                else
                {
                    return "Emotional Health";
                }
            }
        }
        public String TSenfermedadesPrimarias
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Enfermedades Primarias";
                }
                else
                {
                    return "Primary Disease";
                }
            }
        }
        public String TSenfoquesDeVisita
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Enfoques de Visita";
                }
                else
                {
                    return "Visit Approaches";
                }
            }
        }
        public String TSenfoque
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Enfoque";
                }
                else
                {
                    return "Approach";
                }
            }
        }
        public String TSescritura
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Escritura";
                }
                else
                {
                    return "Deed";
                }
            }
        }
        public String TSestadisticasVisitas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Estadísticas de Visitas";
                }
                else
                {
                    return "Visits Statistics";
                }
            }
        }
        public String TSfamiliasconTS
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Familias con Trabajador Social";
                }
                else
                {
                    return "Families with Social Worker";
                }
            }
        }
        public String TSfamiliassinTS
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Familias sin Trabajador Social";
                }
                else
                {
                    return "Families without Social Worker";
                }
            }
        }
        public String TSfechaVisita
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Visita";
                }
                else
                {
                    return "Visit Date";
                }
            }
        }

        public String TSfocos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Focos de Atención";
                }
                else
                {
                    return "Attention Focus";
                }
            }
        }

        public String TSgasto
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Gasto";
                }
                else
                {
                    return "Expense";
                }
            }
        }

        public String TSgastos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Gastos";
                }
                else
                {
                    return "Expenses";
                }
            }
        }
        public String TShigiene
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Higiene";
                }
                else
                {
                    return "Hygiene";
                }
            }
        }
        public String TShistorialActividades
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Historial de Actividades Familiares";
                }
                else
                {
                    return "Family Activities History";
                }
            }
        }
        public String TShistorialAñoEscolar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Historial de Años Educativos";
                }
                else
                {
                    return "School Years History";
                }
            }
        }
        public String TShistorialAsignacionesTS
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Historial de Asignaciones";
                }
                else
                {
                    return "Assignments History";
                }
            }
        }

        public String TShistorialClasificacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Historial de Clasificación Familiar";
                }
                else
                {
                    return "Family Classification History";
                }
            }
        }

        public String TShistorialIngresosExtra
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Historial de Ingresos Extra";
                }
                else
                {
                    return "Additional Incomes History";
                }
            }
        }

        public String TShistorialOcupaciones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Historial de Ocupaciones";
                }
                else
                {
                    return "Occupations History";
                }
            }
        }
        public String TShistorialNADFAS
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Historial de NADFAS";
                }
                else
                {
                    return "NADFAS History";
                }
            }
        }

        public String TShistorialVisitas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Historial de Visitas";
                }
                else
                {
                    return "Visits History";
                }
            }
        }
        public String TShistorialViveres
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Historial de Víveres";
                }
                else
                {
                    return "Family Helps History";
                }
            }
        }
        public String TShoraVisita
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Hora de Visita";
                }
                else
                {
                    return "Visit Time";
                }
            }
        }
        public String TShorasSemanales
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Horas Semanales";
                }
                else
                {
                    return "Weekly Hours";
                }
            }
        }
        public String TSinactivarRelacionOtraFamilia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Inactivar Relacion en Otra Familia";
                }
                else
                {
                    return "Inactivate Relation in Another Family";
                }
            }
        }
        public String TSinformacionFamiliar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Información Familiar";
                }
                else
                {
                    return "Family Information";
                }
            }
        }
        public String TSinformacionIndividual
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Información Individual";
                }
                else
                {
                    return "Individual Information";
                }
            }
        }
        public String TSinformeFamilias
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Informe de Familias por Área y por Ts";
                }
                else
                {
                    return "Families by Village and Social Worker Report";
                }
            }
        }
        public String TSinformeVisitas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Informe de Visitas";
                }
                else
                {
                    return "Visits Report";
                }
            }
        }
        public String TSingresarActividad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresar Actividad";
                }
                else
                {
                    return "Enter Activity";
                }
            }
        }
        public String TSingresarAñoEscolar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresar Año Educativo";
                }
                else
                {
                    return "Enter School Year";
                }
            }
        }
        public String TSingresarAviso
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresar Aviso";
                }
                else
                {
                    return "Enter Warning";
                }
            }
        }
        public String TSingresarIngresoExtra
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresar Ingreso Extra";
                }
                else
                {
                    return "Enter Additional Income";
                }
            }
        }
        public String TSingresarNADFAS
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresar NADFAS";
                }
                else
                {
                    return "Enter NADFAS";
                }
            }
        }
        public String TSingresarOcupacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresar Ocupación";
                }
                else
                {
                    return "Enter Occupation";
                }
            }
        }
        public String TSingresarVisita
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresar Visita";
                }
                else
                {
                    return "Enter Visit";
                }
            }
        }

        public String TSingresarMiembro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresar Miembro";
                }
                else
                {
                    return "Enter Member";
                }
            }
        }
        public String TSingresarVivere
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingresar Vívere";
                }
                else
                {
                    return "Enter Family Help";
                }
            }
        }
        public String TSingresoMensual
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingreso Mensual";
                }
                else
                {
                    return "Monthly Income";
                }
            }
        }
        public String TSingresoSemanal
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingreso Semanal";
                }
                else
                {
                    return "Weekly Income";
                }
            }
        }
        public String TSjornada
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Jornada";
                }
                else
                {
                    return "Work Day";
                }
            }
        }

        public String TSlugarTrabajo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Lugar de Trabajo";
                }
                else
                {
                    return "Workplace";
                }
            }
        }
        public String TSmaterial
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Material";
                }
                else
                {
                    return "Material";
                }
            }
        }
        public String TSmedioAmbiente
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Medio Ambiente";
                }
                else
                {
                    return "Environment";
                }
            }
        }
        public String TSmonto
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Monto";
                }
                else
                {
                    return "Amount";
                }
            }
        }
        public String TSmostrarTodas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Mostrar Todas";
                }
                else
                {
                    return "Show All";
                }
            }
        }
        public String TSmostrarUltimas
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Mostrar Solo Últimas";
                }
                else
                {
                    return "Show Only Latest";
                }
            }
        }
        public String TSNoApadrinadosFaseII
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "№ Apads. Fase II";
                }
                else
                {
                    return "№ Sponsoreds in Phase II ";
                }
            }
        }
        public String TSnuevaActividad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nueva Actividad";
                }
                else
                {
                    return "New Activity";
                }
            }
        }
        public String TSnuevaAsignacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nueva Asignación";
                }
                else
                {
                    return "New Assignment";
                }
            }
        }

        public String TSnuevoIngresoExtra
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nuevo Ingreso Extra";
                }
                else
                {
                    return "New Additional Income";
                }
            }
        }

        public String TSnuevaOcupacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nueva Ocupación";
                }
                else
                {
                    return "New Occupation";
                }
            }
        }
        public String TSnuevaRelacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nueva Relación";
                }
                else
                {
                    return "New Relation";
                }
            }
        }
        public String TSnuevaVisita
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nueva Visita";
                }
                else
                {
                    return "New Visit";
                }
            }
        }
        public String TSnuevoAviso
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nuevo Aviso";
                }
                else
                {
                    return "New Warning";
                }
            }
        }
        public String TSnuevoAñoEscolar
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nuevo Año Escolar";
                }
                else
                {
                    return "New School Year";
                }
            }
        }
        public String TSnuevoNADFAS
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nuevo NADFAS";
                }
                else
                {
                    return "New NADFAS";
                }
            }
        }
        public String TSnuevoVivere
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Nuevo Vívere";
                }
                else
                {
                    return "New Family Help";
                }
            }
        }
        public String TSnumeroCuartos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Número de Cuartos";
                }
                else
                {
                    return "Number of Rooms";
                }
            }
        }
        public String TSpensionAlimenticia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Pensión Alimenticia";
                }
                else
                {
                    return "Alimony";
                }
            }
        }
        public String TSposesion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Posesión";
                }
                else
                {
                    return "Possession";
                }
            }
        }
        public String TSposesiones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Posesiones";
                }
                else
                {
                    return "Possessions";
                }
            }
        }
        public String TSocupacion
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ocupación";
                }
                else
                {
                    return "Occupation";
                }
            }
        }
        public String TSocupaciones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ocupaciones";
                }
                else
                {
                    return "Occupations";
                }
            }
        }
        public String TSocupacionesMiembro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ocupaciones de Miembro";
                }
                else
                {
                    return "Member Occupations";
                }
            }
        }

        public String TSIngresoExtraSeleccionado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ingreso Extra Seleccionado";
                }
                else
                {
                    return "Selected Additonal Income";
                }
            }
        }

        public String TSocupacionSeleccionada
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ocupacion Seleccionada";
                }
                else
                {
                    return "Selected Occupation";
                }
            }
        }
        public String TSotros
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Otros";
                }
                else
                {
                    return "Others";
                }
            }
        }
        public String TSpared
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Pared";
                }
                else
                {
                    return "Wall";
                }
            }
        }
        public String TSpiso
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Piso";
                }
                else
                {
                    return "Floor";
                }
            }
        }
        public String TSproblemasAprendizaje
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Problemas de Aprendizaje";
                }
                else
                {
                    return "Learning Disabilities";
                }
            }
        }
        public String TSpropiedad
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Propiedad";
                }
                else
                {
                    return "Property";
                }
            }
        }
        public String TSrazonFin
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Razón Finalización";
                }
                else
                {
                    return "Termination Reason";
                }
            }
        }
        public String TSreasignarRelaciones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Reasignar Relaciones";
                }
                else
                {
                    return "Reassign Relations";
                }
            }
        }
        public String TSrectificacionyPartida
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Rectificación y Partida de Nacimiento";
                }
                else
                {
                    return "Rectification and Birth Certification";
                }
            }
        }
        public String TSreestablecerRelaciones
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Reestablecer Relaciones";
                }
                else
                {
                    return "Reestablish Relations";
                }
            }
        }
        public String TSrepitencia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Repitencia";
                }
                else
                {
                    return "Repetition";
                }
            }
        }
        public String TSrendimientoAcademico
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Rendimiento Académico";
                }
                else
                {
                    return "Academic Performance";
                }
            }
        }
        public String TSservicios
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Servicios";
                }
                else
                {
                    return "Utilities";
                }
            }
        }

        public String TSsituacionSocial
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Situación Social";
                }
                else
                {
                    return "Social Situation";
                }
            }
        }

        public String TSsocialesLegales
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Problemas Sociales y Legales";
                }
                else
                {
                    return "Social and Legal Problems";
                }
            }
        }

        public String TStamañoTerreno
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tamaño del Terreno (m)";
                }
                else
                {
                    return "Property Size (m)";
                }
            }
        }
        public String TStamañoAreaCultivo
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tamaño del Terreno de Cultivo (m)";
                }
                else
                {
                    return "Cultivated Land Size (m)";
                }
            }
        }
        public String TStecho
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Techo";
                }
                else
                {
                    return "Ceiling";
                }
            }
        }
        public String TStenencia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tenencia";
                }
                else
                {
                    return "Ownership";
                }
            }
        }
        public String TStieneIGGS
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tiene Afiliación con IGGS";
                }
                else
                {
                    return "Has IGGS Affiliation";
                }
            }
        }
        public String TStieneEscritura
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tiene Escritura";
                }
                else
                {
                    return "Has House Deed";
                }
            }
        }
        public String TStieneSegundoPiso
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tiene Segundo Piso";
                }
                else
                {
                    return "Has Second Floor";
                }
            }
        }
        public String TStipoVisita
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Tipo de Visita";
                }
                else
                {
                    return "Visit Type";
                }
            }
        }
        public String TSubicacionTerreno
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ubicación/Terreno";
                }
                else
                {
                    return "Ubication/Terrain";
                }
            }
        }
        public String TSviolencia
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Violencia";
                }
                else
                {
                    return "Violence";
                }
            }
        }
        public String TSvivienda
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Vivienda";
                }
                else
                {
                    return "Living Place";
                }
            }
        }
        public String TSVIF
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "VIF";
                }
                else
                {
                    return "VIF";
                }
            }
        }


        public String infoGeneralAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Información general";
                }
                else
                {
                    return "General information";
                }
            }
        }
        public String msjNotieneunacarreraAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No tiene una carrera";
                }
                else
                {
                    return "Do not have a career";
                }
            }
        }
        public String restriccionAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Restricción";
                }
                else
                {
                    return "Restriction";
                }
            }
        }
        public String telefonoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Teléfono";
                }
                else
                {
                    return "Phone";
                }
            }
        }
        public String titIngCartAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Ingresar Carta";
                }
                else
                {
                    return " Enter letter";
                }
            }
        }
        public String notasAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Notas:";
                }
                else
                {
                    return " Notes:";
                }
            }
        }
        public String categoriaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Categoria:";
                }
                else
                {
                    return " Category:";
                }
            }
        }
        public String AceptarAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Aceptar";
                }
                else
                {
                    return " To Accept";
                }
            }
        }
        public String CancelarAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Cancelar";
                }
                else
                {
                    return " Cancel";
                }
            }
        }
        public String UltimasFechasAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Ultimas Fechas";
                }
                else
                {
                    return " Last Dates";
                }
            }
        }
        public String TipoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Tipo:";
                }
                else
                {
                    return " Type:";
                }
            }
        }
        public String IngresarRegaloAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Ingresar Regalo";
                }
                else
                {
                    return " Enter a Gift";
                }
            }
        }
        public String EscuelaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Escuela";
                }
                else
                {
                    return " School";
                }
            }
        }

        public String FechaFotoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Fecha Foto: ";
                }
                else
                {
                    return " Photo Date: ";
                }
            }
        }
        public String FechaSeleccionadarelAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Fecha de Selección: ";
                }
                else
                {
                    return " Selection Date: ";
                }
            }
        }
        public String FechaSeleccionadacarAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Fecha de Envio: ";
                }
                else
                {
                    return " Shipping Date: ";
                }
            }
        }
        public String RetomarFotoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Retomar Foto";
                }
                else
                {
                    return " Take the photo";
                }
            }
        }
        public String CartasAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Cartas";
                }
                else
                {
                    return " Letters";
                }
            }
        }
        public String RegaloAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Regalos";
                }
                else
                {
                    return " Gifts";
                }
            }
        }
        public String ModificarAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Modificar";
                }
                else
                {
                    return " Modify";
                }
            }
        }
        public String EliminarAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Eliminar";
                }
                else
                {
                    return " Delete";
                }
            }
        }
        public String ImprimirAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Imprimir";
                }
                else
                {
                    return " Print";
                }
            }
        }
        public String FechaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return " Fecha";
                }
                else
                {
                    return " Date";
                }
            }
        }
        public String RegistroIngresadoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Registro Ingresado Exitosamente";
                }
                else
                {
                    return "Registration Successfully Entered";
                }
            }
        }
        public String RegistroModificadoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Registro Modificado Exitosamente";
                }
                else
                {
                    return "Successfully Modified Record";
                }
            }
        }
        public String RegistroEliminadoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Registro Eliminado Exitosamente";
                }
                else
                {
                    return "Record Successfully Deleted";
                }
            }
        }
        public String CambiodeImagenAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cambio de imagen exitoso";
                }
                else
                {
                    return "Successful makeover";
                }
            }
        }
        public String NohaSeleccionadoarchivoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No ha seleccionado ningun archivo";
                }
                else
                {
                    return "I do not select any files";
                }
            }
        }
        public String ExtensionvalidaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Extension no valida";
                }
                else
                {
                    return "Invalid Extension";
                }
            }
        }
        public String PaisAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Pais";
                }
                else
                {
                    return "Country";
                }
            }
        }
        public String EstadoOProvinciaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Estado/Provincia";
                }
                else
                {
                    return "State/Prov";
                }
            }
        }
        public String HablaEspanolAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Habla Español";
                }
                else
                {
                    return "Speaks Spanish";
                }
            }
        }
        public String NumeroPadrino
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Número de Padrino";
                }
                else
                {
                    return "No.";
                }
            }
        }
        public String OrganizacionAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Organización";
                }
                else
                {
                    return "Organisation";
                }
            }
        }
        public String SiAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Sí";
                }
                else
                {
                    return "Yes";
                }
            }
        }
        public String NoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No";
                }
                else
                {
                    return "Not";
                }
            }
        }
        public String FechaInicioAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha Inicio";
                }
                else
                {
                    return "Start Date";
                }
            }
        }
        public String FechaFinAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha Fin";
                }
                else
                {
                    return "End Date";
                }
            }
        }
        public String DeFechaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "De Fecha";
                }
                else
                {
                    return "From Date";
                }
            }
        }
        public String AFechaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "A Fecha";
                }
                else
                {
                    return "To Date";
                }
            }
        }
        public String OtraBusquedaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Otra Busquedad";
                }
                else
                {
                    return "Another Search";
                }
            }
        }
        public String FechaMayorAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "La fecha inicial es mayor a la final.";
                }
                else
                {
                    return "The initial date is greater than the final date.";
                }
            }
        }
        public String EscritaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Carta Escrita";
                }
                else
                {
                    return "Written Letter";
                }
            }
        }
        public String fechaSeleccionAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Seleccion";
                }
                else
                {
                    return "Selection Date";
                }
            }
        }
        public String msjFechaVaciaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "La fecha esta vacia";
                }
                else
                {
                    return "The date is empty";
                }
            }
        }
        public String FechaEntregaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Entrega";
                }
                else
                {
                    return "Delivery Date";
                }
            }
        }
        public String CampoVacioAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Campo Vacio";
                }
                else
                {
                    return "Empty Field";
                }
            }
        }
        public String FechaVisitaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Fecha de Visita";
                }
                else
                {
                    return "Visit Date";
                }
            }
        }
        public String CantidadAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cantidad";
                }
                else
                {
                    return "Quantity";
                }
            }
        }
        public String RazonAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Razon";
                }
                else
                {
                    return "Reason";
                }
            }
        }
        public String MsjCantidadMayor0
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "La cantidad debe ser mayor a 0";
                }
                else
                {
                    return "The quantity must be greater than 0";
                }
            }
        }
        public String ImagenActualAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Imagen Actual";
                }
                else
                {
                    return "Current image";
                }
            }
        }
        public String ImagenNuevaAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Imagen Nueva";
                }
                else
                {
                    return "New image";
                }
            }
        }
        public String FotoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Foto";
                }
                else
                {
                    return "Photo";
                }
            }
        }
        public String MsjNoContieneFotoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No contiene Foto";
                }
                else
                {
                    return "Does not contain photo";
                }
            }
        }
        public String MsjCambiodeImagenExitoso
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Cambio de Imagen Exitoso";
                }
                else
                {
                    return "Successful makeover";
                }
            }
        }
        public String MsjLimitacion8dias
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No puede registrar la visita despues de 8 dias";
                }
                else
                {
                    return "You cannot register the visit after 8 days";
                }
            }
        }
        public String MsjNohaSeleccionadomiembro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No ha seleccionado ningun miembro";
                }
                else
                {
                    return "You have not selected any members";
                }
            }
        }
        public String MsjFamilianoAfiliada
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "La familia no esta afiliadda";
                }
                else
                {
                    return "The family is not affiliated";
                }
            }
        }
        public String MsjYaingresoRegistro
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ya ha sido ingresado un registro con los mismos datos";
                }
                else
                {
                    return "A record with the same data has already been entered";
                }
            }
        }
        public String MsjAmbosCamposestanllenos
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Ambos campos estan llenos";
                }
                else
                {
                    return "Both fields are full";
                }
            }
        }
        public String MsjmiembronoApadrinado
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "El miembro no esta apadrinado";
                }
                else
                {
                    return "The member is not sponsored";
                }
            }
        }
        public String MsjNohaSeleccionadopadrino
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "No ha seleccionado ningun padrino";
                }
                else
                {
                    return "You have not selected any sponsors";
                }
            }
        }
        public String ContieneFotoAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Contiene Foto";
                }
                else
                {
                    return "Contain photo";
                }
            }
        }
        public String RenovacionAPAD
        {
            get
            {
                if (L.Equals("es"))
                {
                    return "Renovación";
                }
                else
                {
                    return "Renovation";
                }
            }
        }
    }
}