using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
namespace Familias3._1.bd
{
    public class BDMISC
    {
        static String conexionString;
        public BDMISC()
        {
            conexionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public String ingMingresarMiembro(String sitio, String usuario, String familia, String nombres, String apellidos, String fechaNacimiento, String genero, String nombreUsual, String CUI, Boolean tenemosCUI, String ultimoGrado, String puedeLeer, String telefono, String otraAfiliacion, String madreBiologica, String padreBiologico)
        {
            //String idMiembro="";
            //try
            //{
            //DateTime fechaCreacion = DateTime.Now;
            //SqlConnection conexion = new SqlConnection(conexionString);
            //SqlCommand comando = new SqlCommand();
            //comando.Connection = conexion;
            //comando.CommandType = CommandType.StoredProcedure;
            //comando.CommandText = "dbo.sp_GEN_IngresoMiembro";
            //comando.Parameters.AddWithValue("@sitio", sitio);
            //comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"));
            //comando.Parameters.AddWithValue("@usuario", usuario);
            //comando.Parameters.AddWithValue("@familia", familia);
            //comando.Parameters.AddWithValue("@nombres", nombres);
            //comando.Parameters.AddWithValue("@apellidos", apellidos);
            //if (!String.IsNullOrEmpty(fechaNacimiento))
            //{
            //    comando.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
            //}
            //else
            //{
            //    comando.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
            //}
            //comando.Parameters.AddWithValue("@genero", genero);
            //comando.Parameters.AddWithValue("@nombreUsual", nombreUsual);
            //comando.Parameters.AddWithValue("@CUI", CUI);
            //comando.Parameters.AddWithValue("@tenemosCUI", tenemosCUI);
            //if (!String.IsNullOrEmpty(ultimoGrado))
            //{
            //    comando.Parameters.AddWithValue("@ultimoGrado", ultimoGrado);
            //}
            //else
            //{
            //    comando.Parameters.AddWithValue("@ultimoGrado", DBNull.Value);
            //}
            //comando.Parameters.AddWithValue("@telefono", telefono);
            //if (!String.IsNullOrEmpty(puedeLeer))
            //{
            //    comando.Parameters.AddWithValue("@puedeLeer", puedeLeer);
            //}
            //else
            //{
            //    comando.Parameters.AddWithValue("@puedeLeer", DBNull.Value);
            //}
            //if (!String.IsNullOrEmpty(otraAfiliacion))
            //{
            //    comando.Parameters.AddWithValue("@otraAfiliacion", otraAfiliacion);
            //}
            //else
            //{
            //    comando.Parameters.AddWithValue("@otraAfiliacion", DBNull.Value);
            //}

            //comando.Parameters.Add("@idMiembro", SqlDbType.Int);
            //comando.Parameters["@idMiembro"].Direction = ParameterDirection.Output;

            //    conexion.Open();
            //    comando.ExecuteNonQuery();
            //    idMiembro = Convert.ToString(comando.Parameters["@idMiembro"].Value);
            //}
            //catch (Exception ex)
            //{
            //    idMiembro = ex.ToString();
            //}
            DateTime fechaCreacion = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO Member (Project, MemberId, CreationDateTime, RecordStatus, UserId, LastFamilyId, FirstNames, LastNames, BirthDate, Gender, PreferredName, LiveDead, OfficialId, HasFaithOfAgeOrOfficialId, LastGradePassed, Literacy, CellularPhoneNumber,  OtherAffiliation, BiologicalMotherMemberId, BiologicalFatherMemberId)
                                                SELECT @sitio, @miembro, @fechaCreacion, ' ', @usuario, @familia, @nombres, @apellidos, @fechaNacimiento, @genero, @nombreUsual, 'V', @CUI, @tenemosCUI, @ultimoGrado, @puedeLeer, @telefono, @otraAfiliacion, @madreBiologica, @padreBiologico";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"));
            comando.Parameters.AddWithValue("@usuario", usuario);
            String miembro = ingMobtenerMiembroDisponible(sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@nombres", nombres);
            comando.Parameters.AddWithValue("@apellidos", apellidos);
            if (!String.IsNullOrEmpty(fechaNacimiento))
            {
                comando.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
            }
            else
            {
                comando.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@genero", genero);
            comando.Parameters.AddWithValue("@nombreUsual", nombreUsual);
            comando.Parameters.AddWithValue("@CUI", CUI);
            comando.Parameters.AddWithValue("@tenemosCUI", tenemosCUI);
            if (!String.IsNullOrEmpty(ultimoGrado))
            {
                comando.Parameters.AddWithValue("@ultimoGrado", ultimoGrado);
            }
            else
            {
                comando.Parameters.AddWithValue("@ultimoGrado", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@telefono", telefono);
            if (!String.IsNullOrEmpty(puedeLeer))
            {
                comando.Parameters.AddWithValue("@puedeLeer", puedeLeer);
            }
            else
            {
                comando.Parameters.AddWithValue("@puedeLeer", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(otraAfiliacion))
            {
                comando.Parameters.AddWithValue("@otraAfiliacion", otraAfiliacion);
            }
            else
            {
                comando.Parameters.AddWithValue("@otraAfiliacion", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(madreBiologica))
            {
                comando.Parameters.AddWithValue("@madreBiologica", madreBiologica);
            }
            else
            {
                comando.Parameters.AddWithValue("@madreBiologica", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(padreBiologico))
            {
                comando.Parameters.AddWithValue("@padreBiologico", padreBiologico);
            }
            else
            {
                comando.Parameters.AddWithValue("@padreBiologico", DBNull.Value);
            }
            comando.ExecuteNonQuery();
            conexion.Close();
            return miembro;
        }
        public String ingMobtenerMiembroDisponible(String sitio)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT dbo.fn_GEN_idMiembroDisponible(@sitio) AS miembro";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            String miembro = tablaDatos.Rows[0]["miembro"].ToString();
            return miembro;
        }
        public void mdfFactualizarFamilia(String sitio, String familia, String fechaCreacion, String usuario, String area, String direccion, String telefono, String etnia, String fechaUltimaAct, String municipio, String tiempoEnLugar)
        {
            DateTime fechaCreacionNEW = DateTime.Now;
            DateTime fechaExpiracion = fechaCreacionNEW;
            mdfFinsertarFamilia(sitio, familia, fechaCreacionNEW.ToString("yyyy-MM-dd HH:mm:ss"), usuario, area, direccion, telefono, etnia, fechaUltimaAct, municipio, tiempoEnLugar);
            //mdfEliminarFamilia(sitio, familia, fechaCreacion, fechaExpiracion.ToString("yyyy-MM-dd HH:mm:ss.fff"));
        }
        protected void mdfFeliminarFamilia(String sitio, String familia, String fechaCreacion, String fechaExpiracion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"UPDATE SET RecordStatus = 'H', ExpirationDateTime = @fechaExpiracion WHERE Project = @sitio AND Family = @familia AND CreationDateTime = @fechaCreacion";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected void mdfFinsertarFamilia(String sitio, String familia, String fechaCreacion, String usuario, String area,  String direccion, String telefono, String etnia, String fechaUltimaAct, String municipio, String tiempoEnLugar)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO Family (Project, FamilyId, CreationDateTime, RecordStatus, UserId, AffiliationLevel, AffiliationStatus, AffiliationStatusDate, Area, Pueblo, Address, TelephoneNumber, Ethnicity, LastUpdateDate, Classification, ClassificationDate, Municipality, TimeOnPlace, NextClassification, RFaroNumber)
                                    SELECT Project, FamilyId, @fechaCreacion, RecordStatus, @usuario, AffiliationLevel, AffiliationStatus, AffiliationStatusDate, @area, (SELECT Pueblo FROM CdGeographicArea cdGA WHERE cdGA.Code = @area), @direccion, @telefono, @etnia, @fechaUltimaAct, Classification, ClassificationDate, @municipio, @tiempoEnLugar, NextClassification, RFaroNumber
                                    FROM Family
                                    WHERE FamilyId = @familia AND Project = @sitio AND RecordStatus = ' '";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            if (!String.IsNullOrEmpty(area))
            {
                comando.Parameters.AddWithValue("@area", area);
            }
            else
            {
                comando.Parameters.AddWithValue("@area", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@direccion", direccion);
            comando.Parameters.AddWithValue("@telefono", telefono);
            if (!String.IsNullOrEmpty(etnia))
            {
                comando.Parameters.AddWithValue("@etnia", etnia);
            }
            else
            {
                comando.Parameters.AddWithValue("@etnia", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(fechaUltimaAct))
            {
                comando.Parameters.AddWithValue("@fechaUltimaAct", fechaUltimaAct);
            }
            else
            {
                comando.Parameters.AddWithValue("@fechaUltimaAct", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(municipio))
            {
                comando.Parameters.AddWithValue("@municipio", municipio);
            }
            else
            {
                comando.Parameters.AddWithValue("@municipio", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@tiempoEnLugar", tiempoEnLugar);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        public void mdfFActualizarRelacion(String sitio, String miembro, String familia, String tipo, String usuario, String razonInactivo)
        {
            DateTime fechaCreacion = DateTime.Now;
            DateTime fechaActivo = fechaCreacion;
            DateTime fechaExpiracionSLCT = fechaCreacion;
            String strFechaInactivo = "";
            if (!String.IsNullOrEmpty(razonInactivo))
            {
                strFechaInactivo = fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss");
            }
            mdfFinsertarRelacion(sitio, miembro, familia, tipo, fechaActivo.ToString("yyyy-MM-dd HH:mm:ss"), fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"), usuario, razonInactivo, strFechaInactivo);
            //mdfFEliminarRelacion(sitio, miembro, familia, tipoSLCT, fechaActivoSLCT, fechaCreacionSLCT, fechaExpiracionSLCT.ToString("yyyy-MM-dd HH:mm:ss"));
        }


        public void mdfFCambiarFamiliaMiembro(String sitio, String miembro, String familia, String usuario)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"INSERT INTO Member (Project, MemberId, CreationDateTime, RecordStatus, UserId, LastFamilyId, FirstNames, LastNames, PreferredName, BirthDate, Gender, AffiliationType, AffiliationStatus, AffiliationStatusDate, LiveDead, DeathDate, BiologicalMotherMemberId, BiologicalFatherMemberId, OtherAffiliation, OfficialId, HasFaithOfAgeOrOfficialId, CellularPhoneNumber, Literacy, LastGradePassed, LastGradePassedYear, LastGradePassedStatus)
                                    SELECT Project, MemberId, @fechaCreacion, RecordStatus, @usuario, @familia, FirstNames, LastNames, PreferredName, BirthDate, Gender, AffiliationType, AffiliationStatus, AffiliationStatusDate, LiveDead, DeathDate, BiologicalMotherMemberId, BiologicalFatherMemberId, OtherAffiliation, OfficialId, HasFaithOfAgeOrOfficialId, CellularPhoneNumber, Literacy, LastGradePassed, LastGradePassedYear, LastGradePassedStatus
                                    FROM Member
                                    WHERE MemberId = @miembro AND Project = @sitio AND RecordStatus = ' '";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@fechaCreacion", now.ToString("yyyy-MM-dd HH:mm:ss"));
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.ExecuteNonQuery();
            conexion.Close();
        }

        protected void mdfFEliminarRelacion(String sitio, String miembro, String familia, String tipo, String fechaActivo, String fechaCreacion, String fechaExpiracion)
        {
            //            SqlConnection conexion = new SqlConnection(conexionString);
            //            conexion.Open();
            //            DateTime now = DateTime.Now;
            //            String comandoString = @"UPDATE  FamilyMemberRelation SET RecordStatus = ' ', ExpirationDateTime =  @fechaExpiracion
            //                                   WHERE Project = @sitio AND MemberId = @miembro AND FamilyId = @familia AND Type = @tipo AND ActiveDate = @fechaActivo AND CreationDateTime = @fechaCreacion";
            //            SqlCommand comando = new SqlCommand(comandoString, conexion);
            //            comando.Parameters.AddWithValue("@sitio", sitio);
            //            comando.Parameters.AddWithValue("@miembro", miembro);
            //            comando.Parameters.AddWithValue("@familia", familia);
            //            comando.Parameters.AddWithValue("@tipo", tipo);
            //            comando.Parameters.AddWithValue("@fechaActivo", fechaActivo);
            //            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            //            comando.Parameters.AddWithValue("@fechaExpiracion", fechaExpiracion);
            //            comando.ExecuteNonQuery();
            //            conexion.Close();
        }
        public void mdfFinsertarRelacionInactivar(String sitio, String familia, String miembro, String tipo, String fechaActivo, String fechaCreacion, String usuario, String razonInactivoSLCT, String fechaInactivoSLCT)
        {
            DataTable dtRelacion = mdfFobtenerRelacionActual(sitio, miembro);
            if (dtRelacion.Rows.Count > 0)
            {
                mdfFinactivarRelacionActual(sitio, miembro, fechaCreacion, usuario, razonInactivoSLCT, fechaInactivoSLCT);
            }
            mdfFinsertarRelacion(sitio, miembro, familia, tipo, fechaActivo, fechaCreacion, usuario, "", "");
        }

        protected DataTable mdfFobtenerRelacionActual(String sitio, String miembro)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT * FROM FamilyMemberRelation FMR
                                   WHERE FMR.RecordStatus = ' ' AND FMR.Project = @sitio AND FMR.MemberId = @miembro AND InactiveDate IS NULL";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable mdfFobtenerRelacionesActivasOtrasFamilias(String sitio, String familia, String idioma, String miembrosIncluir)
        {
            miembrosIncluir = miembrosIncluir.TrimEnd(',');
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT M.MemberId AS Miembro, FMR.FamilyId AS Familia, M.FirstNames + ' ' + M.LastNames AS Nombre, CASE WHEN @idioma = 'es' THEN cdFMRT.DescSpanish ELSE cdFMRT.DescEnglish END AS RelacionDesc, FMR.Type AS Relacion   FROM FamilyMemberRelation FMR 
                                    INNER JOIN Member M ON FMR.RecordStatus = M.RecordStatus AND FMR.Project = M.Project AND FMR.MemberId = M.MemberId 
                                    INNER JOIN CdFamilyMemberRelationType cdFMRT ON cdFMRT.Code = FMR.Type
                                    WHERE FMR.RecordStatus = ' ' AND FMR.FamilyId != @familia AND FMR.Project = @sitio AND FMR.InactiveDate IS NULL AND FMR.MemberId IN (" +
                                    miembrosIncluir +
                                    @") AND 0 <  (SELECT COUNT(*) 
                                    FROM FamilyMemberRelation FMR2 
                                    WHERE FMR.RecordStatus = FMR2.RecordStatus AND FMR.MemberId = FMR2.MemberId AND FMR.Project = FMR2.Project AND FMR2.InactiveReason IS NOT NULL AND FMR2.FamilyId = @familia)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable mdfFobtenerCantidadRelacionesActivasOtrasFamiliasPorMiembro(String sitio, String miembro, String familia, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT FMR.FamilyId AS Familia, FMR.MemberId AS Miembro, M.FirstNames + ' ' + M.LastNames AS Nombre, CASE WHEN @idioma = 'es' THEN CdFMRT.DescSpanish ELSE CdFMRT.DescEnglish END AS RelacionDes, M.Gender AS Genero, CdFMRT.Code AS Relacion, CdFMIR.Code AS RazonInactivo FROM FamilyMemberRelation FMR
                                    INNER JOIN Member M ON M.RecordStatus = FMR.RecordStatus AND M.Project = FMR.Project AND M.MemberId = FMR.MemberId
                                    INNER JOIN CdFamilyMemberRelationType CdFMRT ON FMR.Type = CdFMRT.Code AND ( FMR.FamilyId != @familia AND  0 = (SELECT COUNT(*) FROM FamilyMemberRelation FMR2 WHERE FMR.RecordStatus = FMR2.RecordStatus AND FMR2.Project = FMR.Project AND FMR2.MemberId = @miembro AND FMR2.FamilyId = @familia))
                                    LEFT JOIN CdFamMemRelInactiveReason CdFMIR ON CdFMIR.Code = FMR.InactiveReason
                                    WHERE FMR.RecordStatus = ' ' AND FMR.MemberId = @miembro  AND FMR.Project = @sitio
                                    ORDER BY InactiveDate";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public int mdfFobtenerCantidadRelacionesActivasOtrasFamiliasPorMiembro2(String sitio, String miembro, String familia)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT COUNT(*) AS Num FROM FamilyMemberRelation FMR
                                    WHERE FMR.RecordStatus = ' ' AND FMR.MemberId = @miembro AND FMR.FamilyId != @familia AND FMR.Project = @sitio AND FMR.InactiveDate IS NULL";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@familia", familia);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            //return tablaDatos;
            int num = Int32.Parse(tablaDatos.Rows[0]["Num"].ToString());
            return num;
        }

        public int mdfFobtenerCantidadRelacionesJefeActivasOtrasFamiliasPorMiembro(String sitio, String miembro, String familia)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT COUNT(*) FROM FamilyMemberRelation FMR
                                    WHERE FMR.RecordStatus = ' ' AND FMR.MemberId = @miembro AND FMR.FamilyId != @familia AND FMR.Project = @sitio AND (FMR.Type = 'JEFE' OR FMR.Type = 'JEFM') AND FMR.InactiveDate IS NULL";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@familia", familia);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            int num = Int32.Parse(tablaDatos.Rows[0]["Num"].ToString());
            return num;
        }

        public void mdfFinactivarRelacionActual(String sitio, String miembro, String fechaCreacion, String usuario, String razonInactivo, String fechaInactivo)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"INSERT INTO FamilyMemberRelation (Project, MemberId, FamilyId, Type, ActiveDate, CreationDateTime, RecordStatus, UserId, InactiveReason, InactiveDate)
                                   SELECT Project, MemberId, FamilyId, Type, ActiveDate, @fechaCreacion, ' ', @usuario, @razonInactivo, @fechaInactivo FROM FamilyMemberRelation FMR
                                   WHERE FMR.RecordStatus = ' ' AND FMR.Project = @sitio AND FMR.MemberId = @miembro AND FMR.InactiveDate IS NULL;";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@razonInactivo", razonInactivo);
            comando.Parameters.AddWithValue("@fechaInactivo", fechaInactivo);
            comando.ExecuteNonQuery();
            conexion.Close();
        }


        protected void mdfFinsertarRelacion(String sitio, String miembro, String familia, String tipo, String fechaActivo, String fechaCreacion, String usuario, String razonInactivo, String fechaInactivo)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO FamilyMemberRelation (Project, MemberId, FamilyId, Type, ActiveDate, CreationDateTime, RecordStatus, UserId, InactiveReason, InactiveDate)
                                   VALUES(@sitio, @miembro, @familia, @tipo, @fechaActivo, @fechaCreacion, ' ', @usuario, @razonInactivo, @fechaInactivo)";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@tipo", tipo);
            comando.Parameters.AddWithValue("@fechaActivo", fechaActivo);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion);
            comando.Parameters.AddWithValue("@usuario", usuario);
            if (!String.IsNullOrEmpty(razonInactivo))
            {
                comando.Parameters.AddWithValue("@razonInactivo", razonInactivo);
            }
            else
            {
                comando.Parameters.AddWithValue("@razonInactivo", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(fechaInactivo))
            {
                comando.Parameters.AddWithValue("@fechaInactivo", fechaInactivo);
            }
            else
            {
                comando.Parameters.AddWithValue("@fechaInactivo", DBNull.Value);
            }
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public DataTable mdfFobtenerInfoBasica(String sitio, String familia)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT F.Address, F.Area, F.Ethnicity, F.Municipality, F.Pueblo, F.TelephoneNumber, DAY(F.LastUpdateDate) AS Day, MONTH(F.LastUpdateDate) AS Month, YEAR(F.LastUpdateDate) AS Year, F.TimeOnPlace, *
                                    FROM Family F 
                                    WHERE F.RecordStatus = ' ' AND F.Project = @sitio AND F.FamilyId = @familia";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable mdfFobtenerMiembros(String sitio, String familia, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT  CONVERT(varchar(10), M.MemberId) AS MemberId, M.FirstNames + ' ' + M.LastNames AS Nombre, CASE WHEN @idioma = 'es' THEN dbo.fn_GEN_CalcularEdad(BirthDate) ELSE dbo.fn_GEN_CalculateAge(BirthDate) END AS Edad, FMR.Type AS Relacion, FMR.InactiveReason AS RazonInactivo, M.Gender AS Genero, FMR.CreationDateTime AS fechaCreacion, FMR.ActiveDate AS fechaActivo, 'S' AS yaTieneRelacion, CASE WHEN @idioma = 'es' THEN CMR.DescSpanish ELSE CMR.DescEnglish END AS RelacionDesc,  CASE WHEN @idioma = 'es' THEN CFMRI.DescSpanish ELSE CFMRI.DescEnglish END AS RazonInactivoDesc FROM Member M 
                                    LEFT JOIN FamilyMemberRelation FMR ON M.MemberId = FMR.MemberId AND M.Project = FMR.Project AND M.RecordStatus = FMR.RecordStatus
                                    LEFT JOIN CdFamilyMemberRelationType CMR ON FMR.Type = CMR.Code
                                    LEFT JOIN CdFamMemRelInactiveReason CFMRI ON FMR.InactiveReason = CFMRI.Code
                                    WHERE M.RecordStatus = ' ' AND FMR.Project = @sitio AND FMR.FamilyId = @familia
                                    ORDER BY CMR.DisplayOrder, M.MemberId";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@now", now);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable mdfFobtenerInfoParaMiembrosNuevos(String sitio, String familia, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT M.MemberId, M.FirstNames + ' ' + M.LastNames AS Nombre, CASE WHEN @idioma = 'es' THEN dbo.fn_GEN_CalcularEdad(BirthDate) ELSE dbo.fn_GEN_CalculateAge(BirthDate) END AS Edad, FMR.Type AS Relacion, FMR.InactiveReason AS RazonInactivo, M.Gender AS Genero, FMR.CreationDateTime AS fechaCreacion, FMR.ActiveDate AS fechaActivo, 'S' AS yaTieneRelacion FROM Member M 
                                    LEFT JOIN FamilyMemberRelation FMR ON M.MemberId = FMR.MemberId AND M.Project = FMR.Project AND M.RecordStatus = FMR.RecordStatus
                                    INNER JOIN CdFamilyMemberRelationType CMR ON FMR.Type = CMR.Code
                                    WHERE M.RecordStatus = ' ' AND FMR.Project = @sitio AND FMR.FamilyId = @familia
                                    ORDER BY CMR.DisplayOrder, M.MemberId";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@now", now);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public String mdfFobtenerPuebloDeArea(String area)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT CGP.Pueblo FROM CdGeographicArea CGA INNER JOIN CdGeographicPueblo CGP ON CGA.Pueblo = CGP.Pueblo WHERE @area = CGA.Code";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@area", area);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            String pueblo = "";
            if (tablaDatos.Rows.Count > 0)
            {
                pueblo = tablaDatos.Rows[0]["Pueblo"].ToString();
            }
            return pueblo;
        }



        public void mdfMactualizarMiembro(String sitio, String miembro, String usuario, String nombres, String apellidos, String nombreUsual, String fechaNacimiento, String fechaFallecimiento, String madreBiologica, String padreBiologico, String otraAfiliacion, String CUI, Boolean tenemosCUI, String telefono, String puedeLeer, String ultimoGrado, String estadoUltimoGrado, String añoUltimoGrado)
        {
            DateTime fechaCreacion = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            DateTime now = DateTime.Now;
            String comandoString = @"INSERT INTO Member (Project, MemberId, CreationDateTime, RecordStatus, UserId, LastFamilyId, FirstNames, LastNames, PreferredName, BirthDate, Gender, AffiliationType, AffiliationStatus, AffiliationStatusDate, LiveDead, DeathDate, BiologicalMotherMemberId, BiologicalFatherMemberId, OtherAffiliation, OfficialId, HasFaithOfAgeOrOfficialId, CellularPhoneNumber, Literacy, LastGradePassed, LastGradePassedYear, LastGradePassedStatus)
                                    SELECT Project, MemberId, @fechaCreacion, RecordStatus, @usuario, LastFamilyId, @nombres, @apellidos, @nombreUsual, @fechaNacimiento, Gender, AffiliationType, AffiliationStatus, AffiliationStatusDate, CASE WHEN (@fechaFallecimiento = '') OR (@fechaFallecimiento IS NULL) THEN 'V' ELSE 'M' END, @fechaFallecimiento, @madreBiologica, @padreBiologico, @otraAfiliacion, @CUI, @tenemosCUI, @telefono, @puedeLeer, @ultimoGrado, @añoUltimoGrado, @estadoUltimoGrado
                                    FROM Member
                                    WHERE MemberId = @miembro AND Project = @sitio AND RecordStatus = ' '";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@fechaCreacion", fechaCreacion.ToString("yyyy-MM-dd HH:mm:ss"));
            comando.Parameters.AddWithValue("@usuario", usuario);
            comando.Parameters.AddWithValue("@nombres", nombres);
            comando.Parameters.AddWithValue("@apellidos", apellidos);
            comando.Parameters.AddWithValue("@nombreUsual", nombreUsual);
            if (!String.IsNullOrEmpty(fechaNacimiento))
            {
                comando.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
            }
            else
            {
                comando.Parameters.AddWithValue("@fechaNacimiento", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(fechaFallecimiento))
            {
                comando.Parameters.AddWithValue("@fechaFallecimiento", fechaFallecimiento);
            }
            else
            {
                comando.Parameters.AddWithValue("@fechaFallecimiento", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(madreBiologica))
            {
                comando.Parameters.AddWithValue("@madreBiologica", madreBiologica);
            }
            else
            {
                comando.Parameters.AddWithValue("@madreBiologica", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(padreBiologico))
            {
                comando.Parameters.AddWithValue("@padreBiologico", padreBiologico);
            }
            else
            {
                comando.Parameters.AddWithValue("@padreBiologico", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(otraAfiliacion))
            {
                comando.Parameters.AddWithValue("@otraAfiliacion", otraAfiliacion);
            }
            else
            {
                comando.Parameters.AddWithValue("@otraAfiliacion", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@CUI", CUI);
            comando.Parameters.AddWithValue("@tenemosCUI", tenemosCUI);
            comando.Parameters.AddWithValue("@telefono", telefono);
            if (!String.IsNullOrEmpty(puedeLeer))
            {
                comando.Parameters.AddWithValue("@puedeLeer", puedeLeer);
            }
            else
            {
                comando.Parameters.AddWithValue("@puedeLeer", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(ultimoGrado))
            {
                comando.Parameters.AddWithValue("@ultimoGrado", ultimoGrado);
            }
            else
            {
                comando.Parameters.AddWithValue("@ultimoGrado", DBNull.Value);
            }
            if (!String.IsNullOrEmpty(estadoUltimoGrado))
            {
                comando.Parameters.AddWithValue("@estadoUltimoGrado", estadoUltimoGrado);
            }
            else
            {
                comando.Parameters.AddWithValue("@estadoUltimoGrado", DBNull.Value);
            }
            comando.Parameters.AddWithValue("@añoUltimoGrado", añoUltimoGrado);
            comando.ExecuteNonQuery();
            conexion.Close();
        }
        public DataTable mdfMobtenerMiembroEsp(String sitio, String miembro, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT M.FirstNames AS nombres, M.LastNames AS apellidos, M.PreferredName AS nombreUsual, DAY(M.BirthDate) AS diaNacimiento, MONTH(M.BirthDate) AS mesNacimiento, YEAR(M.BirthDate) AS añoNacimiento, CASE WHEN @idioma = 'es' THEN dbo.fn_GEN_CalcularEdad(BirthDate) ELSE dbo.fn_GEN_CalculateAge(BirthDate) END AS Edad, CASE WHEN @idioma = 'es' THEN CLD.DescSpanish ELSE CLD.DescEnglish END AS vivoMuerto, DAY(M.DeathDate) AS diaFallecimiento, MONTH(M.DeathDate) AS mesFallecimiento, YEAR(M.DeathDate) AS añoFallecimiento, M.OfficialId AS CUI, M.HasFaithOfAgeOrOfficialId AS tenemosCUI, M.LastGradePassed AS ultimoGrado, M.LastGradePassedYear añoUltimoGrado, M.LastGradePassedStatus AS estadoUltimoGrado, M.Literacy AS puedeLeer, M.CellularPhoneNumber AS telefono, M.OtherAffiliation AS otraAfiliacion, M.BiologicalMotherMemberId AS madreBiologica, M.BiologicalFatherMemberId AS padreBiologico
                                    FROM Member M
									INNER JOIN CdLiveDead CLD ON M.LiveDead = CLD.Code 
                                    WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND M.MemberId = @miembro";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@miembro", miembro);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@now", now);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable mdfMobtenerMiembros(String sitio, String familia, String idioma)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT M.MemberId, M.FirstNames + ' ' + M.LastNames AS Nombre, CASE WHEN @idioma = 'es' THEN dbo.fn_GEN_CalcularEdad(BirthDate) ELSE dbo.fn_GEN_CalculateAge(BirthDate) END AS Edad, CASE WHEN @idioma = 'es' THEN CMR.DescSpanish ELSE CMR.DescEnglish END AS Relacion, CASE WHEN @idioma= 'es' THEN CFMRIR.DescSpanish ELSE CFMRIR.DescEnglish END AS RazonInactivo FROM Member M 
                                    LEFT JOIN FamilyMemberRelation FMR ON M.MemberId = FMR.MemberId AND M.Project = FMR.Project AND M.RecordStatus = FMR.RecordStatus
                                    INNER JOIN CdFamilyMemberRelationType CMR ON FMR.Type = CMR.Code
									LEFT JOIN CdFamMemRelInactiveReason CFMRIR ON FMR.InactiveReason = CFMRIR.Code
                                    WHERE M.RecordStatus = ' ' AND FMR.Project = @sitio AND FMR.FamilyId = @familia
                                    ORDER BY CMR.DisplayOrder, M.MemberId";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@familia", familia);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
    }
}