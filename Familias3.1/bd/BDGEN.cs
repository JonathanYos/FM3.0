using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Familias3._1.bd
{
    public class BDGEN
    {
        static String conexionString;
        public BDGEN()
        {
            conexionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
        }
        public DataTable obtenerAlfabetismo(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END AS Des
                                    FROM CdLiteracy
                                    ORDER BY CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public String obtenerEdad(String fechaNacimiento, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT CASE WHEN @idioma = 'es' THEN dbo.fn_GEN_CalcularEdad(@fechaNacimiento) ELSE dbo.fn_GEN_CalculateAge(@fechaNacimiento) END AS Edad";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@fechaNacimiento", fechaNacimiento);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            String edad = tablaDatos.Rows[0]["Edad"].ToString();
            return edad;
        }
        public DataTable obtenerAreas(String sitio, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT CASE WHEN @idioma = 'es'
                THEN DescSpanish 
                ELSE DescEnglish 
                END Des, Code 
                FROM CdGeographicArea
                WHERE Active = 1 AND Project = @sitio
                ORDER BY CASE WHEN @idioma = 'es'
                THEN DescSpanish 
                WHEN @idioma = 'en' 
                THEN DescEnglish
                ELSE Code
                END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable obtenerCarrerasEduc(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' THEN DescSpanish ELSE DescEnglish END AS Des 
                                    FROM CdEducationCareer
                                    ORDER BY CASE WHEN @idioma = 'es' THEN DescSpanish ELSE DescEnglish END ";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerCentrosEduc(String sitio)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT * FROM School S WHERE S.RecordStatus = ' ' AND Project = @sitio AND Active = 1
                                        ORDER BY NAME";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerCentrosEduc(String sitio, String grado)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT S.Code AS Code, S.Name AS Name FROM School S
                                    INNER JOIN CdGrade CG ON  S.EducationLevelsOffered LIKE '%' + CG.EducationLevel +'%' AND CG.Code = @grado AND Active = 1
                                    WHERE S.RecordStatus = ' ' AND S.Project = 'F' 
                                    ORDER BY NAME";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@grado", grado);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable obtenerDiagnostico(String sitio, String idioma, Boolean post)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT CLD.Code, CASE WHEN @idioma = 'es' THEN CASE WHEN CLD.Applies = 1 THEN 'SÍ' ELSE 'NO' END + ' - ' + CLD.DescSpanish ELSE CASE WHEN CLD.Applies = 1 THEN 'YES' ELSE 'NO' END + ' - ' + CLD.DescEnglish END AS Des 
                                    FROM CdFamilyLivingDiagnosis CLD
                                    WHERE (CLD.Project = @sitio OR '*' = CLD.Project) AND CLD.PostAnalysis = @post
                                    ORDER BY CASE WHEN @idioma = 'es' THEN CLD.DescSpanish ELSE CLD.DescEnglish END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@post", post);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable obtenerComentarioDeDiagnostico(String sitio, String diagnostico, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT CLD.Comments AS Comentario, '<b>' + CASE WHEN CLD.Applies = 1 THEN CASE WHEN @idioma = 'es' THEN 'SÍ' ELSE 'YES' END ELSE 'NO' END + '</b>' AS Aplica
                                    FROM CdFamilyLivingDiagnosis CLD
                                    WHERE (CLD.Project = @sitio OR '*' = CLD.Project) AND CLD.Code = @diagnostico";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@diagnostico", diagnostico);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable obtenerEtnias(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END AS Des
                                    FROM CdEthnicity
                                    ORDER BY CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerEstadosAfil(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END AS Des
                                    FROM CdAffiliationStatus
                                    ORDER BY CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerEstadosEducativos(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish ELSE DescEnglish
                                    END AS Des FROM CdEducationStatus
                                    WHERE Code IN ('ESTU', 'GANO', 'PERD', 'REPI')
                                    ORDER BY CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish ELSE DescEnglish
                                    END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerEstadosEducativosBusquedas(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                                    THEN DescSpanish 
                                                    ELSE DescEnglish END AS Des
                                    FROM CdEducationStatus
                                    ORDER BY CASE WHEN @idioma = 'es' THEN DescSpanish ELSE DescEnglish END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerExcepcionesEstadoEduc(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                                    THEN DescSpanish 
                                                    ELSE DescEnglish END AS Des
                                    FROM CdEducationReasonNottoContinue
                                    ORDER BY CASE WHEN @idioma = 'es' THEN DescSpanish ELSE DescEnglish END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerFamiliasBusqueda(String sitio, String idioma, String TS, String estadoTS, String estadoAfil, String area, String region, String direccion)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = "";
            comandoString = @"SELECT F.FamilyId, F.Project + CONVERT(VARCHAR(10),F.FamilyId) AS Familia, FER.EmployeeId AS TS, M.FirstNames + ' ' + M.LastNames AS JefeCasa, F.Address AS Direccion, CASE WHEN @idioma = 'es' THEN CGA.DescSpanish ELSE CGA.DescEnglish END AS Area, F.Classification AS Clasificacion, CASE WHEN @idioma = 'es' THEN CAS.DescSpanish ELSE CAS.DescEnglish END AS EstadoAfiliacion FROM Family F 
                LEFT JOIN FamilyEmployeeRelation FER ON F.RecordStatus = FER.RecordStatus AND F.Project = FER.Project AND F.FamilyId = FER.FamilyId AND ((FER.EndDate IS NULL AND @estadoTS = 'Activo') OR (FER.EndDate IS NOT NULL AND @estadoTS = 'Inactivo') OR (@estadoTS = ''))
                INNER JOIN FamilyMemberRelation FMR ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND F.FamilyId = FMR.FamilyId AND FMR.InactiveReason IS NULL AND FMR.Type IN ('JEFE','JEFM')
                INNER JOIN Member M ON M.RecordStatus = FMR.RecordStatus AND M.Project = FMR.Project AND M.MemberId = FMR.MemberId
                LEFT JOIN CdGeographicArea CGA ON F.Area = CGA.Code
                LEFT JOIN CdGeographicPueblo CGP ON F.Pueblo = CGP.Pueblo
                INNER JOIN CdAffiliationStatus CAS ON F.AffiliationStatus = CAS.Code
                WHERE F.RecordStatus = ' ' AND F.Project = @sitio AND ((F.AffiliationStatus = @estadoAfil) OR ('' = @estadoAfil)) AND ((FER.EmployeeId = @TS) OR ('' = @TS)) AND ((CGA.Code = @area) OR ('' = @area)) AND ((CGP.Region = @region) OR ('' = @region)) AND ((F.Address LIKE @direccion) OR ('' = @direccion))
                ORDER BY EmployeeId, FER.StartDate";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@TS", TS);
            comando.Parameters.AddWithValue("@estadoTS", estadoTS);
            comando.Parameters.AddWithValue("@estadoAfil", estadoAfil);
            comando.Parameters.AddWithValue("@area", area);
            comando.Parameters.AddWithValue("@region", region);
            comando.Parameters.AddWithValue("@direccion", direccion);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerGeneros(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END AS Des
                                    FROM CdGender 
                                    WHERE Code != 'D'
                                    ORDER BY CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerGrados(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END AS Des
                                    FROM CdGrade
                                    ORDER BY Orden";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerGradosApad(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END AS Des
                                    FROM CdGrade
                                    ORDER BY Orden";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerMaestros(String sitio)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT * FROM FwEmployee FE 
                                    INNER JOIN FwEmployeeRole FER ON FE.EmployeeId = FER.EmployeeId AND FER.Status = 'ACTV' AND FER.Organization = 'F' AND FER.Role = 'MSTR'";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerMiembrosOtraInfo(String sitio, String idioma, String filNombre, String filApellido, String filDiaNacimiento, String filMesNacimiento, String filAñoNacimiento, String filNombreU, String filArea, String filTS, String filTipoAfil, Boolean infoEduc, String año, Boolean filApad, Boolean filAfil, Boolean filOtros, Boolean filDesaf, Boolean filDesafMiembro, Boolean filGrad, Boolean filGradMiembro)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = "";
            if (!infoEduc)
            {
                comandoString = @"SELECT M.MemberId, F.FamilyId, M.FirstNames AS Nombres, M.LastNames AS Apellidos, M.PreferredName AS NombreUsual, dbo.fn_GEN_FormatDate(M.BirthDate, 'es') AS FechaNacimiento, M.Gender AS Genero, 
                                    CASE WHEN @idioma = 'es' THEN cdAT.DescSpanish ELSE cdAT.DescEnglish END AS AfilTipo,                    							
                                        CASE WHEN @idioma = 'es' THEN 
                    						CASE WHEN (M.AffiliationStatus = 'AFIL')
                    							THEN 'Apadrinado'
                    						WHEN (M.AffiliationStatus = 'DESA')
                    							THEN 'Desafiliado'
                    						WHEN (M.AffiliationStatus = 'GRAD')
                    							THEN 'Graduados'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Afiliado'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Miembro Familia Desafiliada'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
                    							THEN 'Miembro Familia Graduada'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Afiliado (Inactivo)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Miembro Familia Desafiliada (Inactivo)'
                    						WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
                    							THEN 'Miembro Familia Graduada (Inactivo)'
                    						END
                    					ELSE 
                    				CASE WHEN (M.AffiliationStatus = 'AFIL')
                    					THEN 'Sponsored'
                    				WHEN (M.AffiliationStatus = 'DESA')
                    					THEN 'Disaffiliate'
                    				WHEN (M.AffiliationStatus = 'GRAD')
                    					THEN 'Graduate'
                    				WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
                    					THEN 'Relatives of Affiliate Family'
                    				WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
                    					THEN 'Relatives of Disaffiliate Family'
                    				WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
                    					THEN 'Relatives of Graduate Family'
                    				WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
                    					THEN 'Relatives of Affiliate Family (Inactive)'
                    				WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
                    					THEN 'Relatives of Disaffiliate Family (Inactive)'
                    				WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
                    					THEN 'Relatives of Graduate Family (Inactive)'
                    				END 
                    			END AS TipoMiembro,
                    			dbo.fn_GEN_Semaforo(M.Project, M.MemberId) AS Semaforo, F.Classification AS Clasificacion, dbo.fn_GEN_TS(F.Project, F.FamilyId) AS TS, F.Address Direccion, CASE WHEN @idioma = 'es' THEN CGA.DescSpanish ELSE CGA.DescEnglish END AS Area, F.Pueblo, dbo.fn_GEN_regionFamilia(F.Project, F.FamilyId) as Region, YEAR(M.AffiliationStatusDate) AñoEstadoAfil
                    		FROM Member M 
                            INNER JOIN Family F ON M.RecordStatus = F.RecordStatus AND M.Project = F.Project AND M.LastFamilyId = F.FamilyId
                            INNER JOIN FamilyMemberRelation FMR ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND M.MemberId = FMR.MemberId AND F.FamilyId = FMR.FamilyId AND
                    			((M.AffiliationStatus = 'AFIL' AND @filApad = '1') 
                    		OR (M.AffiliationStatus = 'DESA' AND @filDesaf = '1') 
                    		OR (M.AffiliationStatus = 'GRAD' AND @filGrad = '1')
							OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL')) AND FMR.InactiveReason IS NULL AND @filAfil = '1')
                    		OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA')) AND FMR.InactiveReason IS NULL AND @filDesafMiembro = '1')
                    		OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD')) AND FMR.InactiveReason IS NULL AND @filGradMiembro = '1')
                    		OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL')) AND FMR.InactiveReason IS NOT NULL AND '0' = '1')
                    		OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA')) AND FMR.InactiveReason IS NOT NULL AND '0' = '1')
                    		OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD')) AND FMR.InactiveReason IS NOT NULL AND '0' = '1')
                    		OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus IS NULL)) AND FMR.InactiveReason IS NULL AND '0' = '1'))
                        LEFT JOIN FamilyEmployeeRelation FER ON F.Project = FER.Project AND F.FamilyId = FER.FamilyId AND F.RecordStatus = FER.RecordStatus AND FER.EndDate IS NULL                     
                        LEFT JOIN CdAffiliationStatus CAS ON M.AffiliationStatus = CAS.Code
                        LEFT JOIN CdAffiliationType cdAT ON M.AffiliationType = cdAT.Code
                    	LEFT JOIN CdGeographicArea CGA ON CGA.Code = F.Area
                    	WHERE M.RecordStatus = ' ' AND M.Project = @sitio 
                        AND ((M.FirstNames LIKE @filNombre) OR ('' = @filNombre))
                        AND ((M.LastNames LIKE @filApellido) OR ('' = @filApellido))
                        AND ((M.PreferredName LIKE @filNombreU) OR ('' = @filNombreU))
                        AND ((DAY(M.BirthDate) = @filDiaNacimiento) OR ('' = @filDiaNacimiento))
                        AND ((MONTH(M.BirthDate) = @filMesNacimiento) OR ('' = @filMesNacimiento))
                        AND ((YEAR(M.BirthDate) = @filAñoNacimiento) OR ('' = @filAñoNacimiento))
                        AND ((FER.EmployeeId LIKE @filTS) OR ('' = @filTS))
                        AND ((M.AffiliationType = @filTipoAfil) OR ('' = @filTipoAfil))
                        AND ((F.Area = @filArea) OR ('' = @filArea))";
            }
            else
            {
                comandoString = @"SELECT M.MemberId, M.LastFamilyId AS FamilyId, M.FirstNames AS Nombres, M.LastNames AS Apellidos, M.PreferredName AS NombreUsual, dbo.fn_GEN_FormatDate(M.BirthDate, 'es') AS FechaNacimiento, M.Gender AS Genero, 
                        CASE WHEN @idioma = 'es' THEN cdAT.DescSpanish ELSE cdAT.DescEnglish END AS AfilTipo,							
                        CASE WHEN @idioma = 'es' THEN 
								CASE WHEN (M.AffiliationStatus = 'AFIL')
									THEN 'Apadrinado'
								WHEN (M.AffiliationStatus = 'DESA')
									THEN 'Desafiliado'
								WHEN (M.AffiliationStatus = 'GRAD')
									THEN 'Graduado'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
									THEN 'Afiliado'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
									THEN 'Miembro Familia Desafiliada'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
									THEN 'Miembro Familia Graduada'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Afiliado (Inactivo)'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Miembro Familia Desafiliada (Inactivo)'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Miembro Familia Graduada (Inactivo)'
								END
							ELSE 
								CASE WHEN (M.AffiliationStatus = 'AFIL')
									THEN 'Sponsored'
								WHEN (M.AffiliationStatus = 'DESA')
									THEN 'Disaffiliate'
								WHEN (M.AffiliationStatus = 'GRAD')
									THEN 'Graduate'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
									THEN 'Relatives of Affiliate Family'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
									THEN 'Relatives of Disaffiliate Family'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
									THEN 'Relatives of Graduate Family'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Relatives of Affiliate Family (Inactive)'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Relatives of Disaffiliate Family (Inactive)'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Relatives of Graduate Family (Inactive)'
								END 
							END AS TipoMiembro,
							dbo.fn_GEN_Semaforo(M.Project, M.MemberId) AS Semaforo, F.Classification AS Clasificacion, dbo.fn_GEN_TS(F.Project, F.FamilyId) AS TS, F.Address Direccion, CASE WHEN @idioma = 'es' THEN CGA.DescSpanish ELSE CGA.DescEnglish END AS Area, F.Pueblo, dbo.fn_GEN_regionFamilia(F.Project, F.FamilyId) as Region, YEAR(M.AffiliationStatusDate) AñoEstadoAfil,
                            MEY.SchoolYear AS Año, 
							CASE WHEN @idioma = 'es' THEN cdG.DescSpanish ELSE cdG.DescEnglish END AS Grado,
							CASE WHEN @idioma = 'es' THEN cdES.DescSpanish ELSE cdES.DescEnglish END AS EstadoEducativo,
							CASE WHEN @idioma = 'es' THEN cdL.DescSpanish ELSE cdL.DescEnglish END AS NivelEducativo,
							S.Name AS CentroEducativo,
							CASE WHEN @idioma = 'es' THEN cdEC.DescSpanish ELSE cdEC.DescEnglish END AS CarreraEducativa,
                            CASE WHEN @idioma = 'es' THEN cdRNC.DescSpanish ELSE cdRNC.DescEnglish END AS ExcepcionEstadoEduc
							FROM Member M 
                            INNER JOIN Family F ON M.RecordStatus = F.RecordStatus AND M.Project = F.Project AND M.LastFamilyId = F.FamilyId
                            INNER JOIN FamilyMemberRelation FMR ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND M.MemberId = FMR.MemberId AND F.FamilyId = FMR.FamilyId AND
								((M.AffiliationStatus = 'AFIL' AND @filApad = '1') 
								OR (M.AffiliationStatus = 'DESA' AND @filDesaf = '1') 
								OR (M.AffiliationStatus = 'GRAD' AND @filGrad = '1') 
								OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL')) AND FMR.InactiveReason IS NULL AND @filAfil = '1')
								OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA')) AND FMR.InactiveReason IS NULL AND @filDesafMiembro = '1')
								OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD')) AND FMR.InactiveReason IS NULL AND @filGradMiembro = '1')
								OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL')) AND FMR.InactiveReason IS NOT NULL AND '0' = '1')
								OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA')) AND FMR.InactiveReason IS NOT NULL AND '0' = '1')
								OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD')) AND FMR.InactiveReason IS NOT NULL AND '0' = '1')
								OR (((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus IS NULL)) AND FMR.InactiveReason IS NULL AND '0' = '1'))
                            LEFT JOIN FamilyEmployeeRelation FER ON F.Project = FER.Project AND F.FamilyId = FER.FamilyId AND F.RecordStatus = FER.RecordStatus AND FER.EndDate IS NULL  
                            LEFT JOIN CdAffiliationStatus CAS ON M.AffiliationStatus = CAS.Code
                            LEFT JOIN CdAffiliationType cdAT ON M.AffiliationType = cdAT.Code
							LEFT JOIN CdGeographicArea CGA ON CGA.Code = F.Area
                            LEFT OUTER JOIN dbo.MemberEducationYear MEY ON M.Project = MEY.Project AND M.MemberId = MEY.MemberId AND MEY.RecordStatus = ' ' 
                            AND MEY.SchoolYear = @año AND MEY.Grade = dbo.fn_GEN_getActualGrade(MEY.Project, MEY.MemberId, MEY.SchoolYear) 
                            LEFT OUTER JOIN dbo.CdGrade cdG ON MEY.Grade = cdG.Code 
                            LEFT OUTER JOIN dbo.CdEducationStatus cdES ON cdES.Code = MEY.Status 
                            LEFT OUTER JOIN dbo.CdEducationCareer cdEC ON cdEC.Code = MEY.Career 
                            LEFT OUTER JOIN dbo.School S ON S.Project = MEY.Project AND S.RecordStatus = MEY.RecordStatus AND S.Code = MEY.SchoolCode                            
                            LEFT OUTER JOIN dbo.CdEducationLevel cdL ON cdL.Code = cdG.EducationLevel 
                            LEFT OUTER JOIN dbo.CdEducationReasonNottoContinue cdRNC ON cdRNC.Code = MEY.ReasonNotToContinue
							WHERE M.RecordStatus = ' ' AND M.Project = @sitio 
                            AND ((M.FirstNames LIKE @filNombre) OR ('' = @filNombre))
                            AND ((M.LastNames LIKE @filApellido) OR ('' = @filApellido))
                            AND ((M.PreferredName LIKE @filNombreU) OR ('' = @filNombreU))
                            AND ((DAY(M.BirthDate) = @filDiaNacimiento) OR ('' = @filDiaNacimiento))
                            AND ((MONTH(M.BirthDate) = @filMesNacimiento) OR ('' = @filMesNacimiento))
                            AND ((YEAR(M.BirthDate) = @filAñoNacimiento) OR ('' = @filAñoNacimiento))
                            AND ((FER.EmployeeId LIKE @filTS) OR ('' = @filTS))
                            AND ((M.AffiliationType = @filTipoAfil) OR ('' = @filTipoAfil))
                            AND ((F.Area LIKE @filArea) OR ('' = @filArea))";
            }
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@filNombre", filNombre);
            comando.Parameters.AddWithValue("@filApellido", filApellido);
            comando.Parameters.AddWithValue("@filDiaNacimiento", filDiaNacimiento);
            comando.Parameters.AddWithValue("@filMesNacimiento", filMesNacimiento);
            comando.Parameters.AddWithValue("@filAñoNacimiento", filAñoNacimiento);
            comando.Parameters.AddWithValue("@filNombreU", filNombreU);
            comando.Parameters.AddWithValue("@filTipoAfil", filTipoAfil);
            comando.Parameters.AddWithValue("@filTS", filTS);
            comando.Parameters.AddWithValue("@filArea", filArea);
            comando.Parameters.AddWithValue("@filApad", filApad);
            comando.Parameters.AddWithValue("@filAfil", filAfil);
            comando.Parameters.AddWithValue("@filOtros", filOtros);
            comando.Parameters.AddWithValue("@filGrad", filGrad);
            comando.Parameters.AddWithValue("@filGradMiembro", filGradMiembro);
            comando.Parameters.AddWithValue("@filDesaf", filDesaf);
            comando.Parameters.AddWithValue("@filDesafMiembro", filDesafMiembro);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable obtenerMiembrosOtraInfoEmp(String sitio, String idioma, String filNombre, String filApellido, String filDiaNacimiento, String filMesNacimiento, String filAñoNacimiento, String filNombreU, Boolean filEmpleados, Boolean filFamEmpleados)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = "";
            if ((sitio == "E" || sitio == "A"))
            {
                comandoString = @"SELECT M.MemberId, F.FamilyId, M.FirstNames AS Nombres, M.LastNames AS Apellidos, M.PreferredName AS NombreUsual, dbo.fn_GEN_FormatDate(M.BirthDate, 'es') AS FechaNacimiento, M.Gender AS Genero, F.Address Direccion
                    		FROM Member M 
                            INNER JOIN Family F ON M.RecordStatus = F.RecordStatus AND M.Project = F.Project AND M.LastFamilyId = F.FamilyId
                            INNER JOIN FamilyMemberRelation FMR ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND M.MemberId = FMR.MemberId AND F.FamilyId = FMR.FamilyId AND FMR.InactiveReason IS NULL AND ((FMR.Type IN ('1JEF', '1JEM') AND @filEmpleados = '1' ) OR (FMR.Type NOT IN ('1JEF', '1JEM') AND @filFamEmpleados = '1'))
                            WHERE M.RecordStatus = ' ' AND M.Project = @sitio
                            AND ((M.FirstNames LIKE @filNombre) OR ('' = @filNombre))
                            AND ((M.LastNames LIKE @filApellido) OR ('' = @filApellido))
                            AND ((M.PreferredName LIKE @filNombreU) OR ('' = @filNombreU))
                            AND ((DAY(M.BirthDate) = @filDiaNacimiento) OR ('' = @filDiaNacimiento))
                            AND ((MONTH(M.BirthDate) = @filMesNacimiento) OR ('' = @filMesNacimiento))
                            AND ((YEAR(M.BirthDate) = @filAñoNacimiento) OR ('' = @filAñoNacimiento))
                            ORDER BY M.MemberId, F.FamilyId";
            }
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@filNombre", filNombre);
            comando.Parameters.AddWithValue("@filApellido", filApellido);
            comando.Parameters.AddWithValue("@filDiaNacimiento", filDiaNacimiento);
            comando.Parameters.AddWithValue("@filMesNacimiento", filMesNacimiento);
            comando.Parameters.AddWithValue("@filAñoNacimiento", filAñoNacimiento);
            comando.Parameters.AddWithValue("@filNombreU", filNombreU);
            comando.Parameters.AddWithValue("@filEmpleados", filEmpleados);
            comando.Parameters.AddWithValue("@filFamEmpleados", filFamEmpleados);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable obtenerMiembrosInfoEduc(String sitio, String idioma, String año, String filCarrera, String filCentroEduc, String filEstadoEduc, String filExcEstadoEduc, String filGrado, String filMaestro, String filNivelEduc, String filPueblo, String filTipoAfil, String filTipoEscuela, Boolean apad, Boolean grad, Boolean desaf)
        {
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = "";
            if (!(sitio == "E" || sitio == "A"))
            {
                comandoString = @"SELECT M.MemberId, M.LastFamilyId AS FamilyId, M.FirstNames AS  Nombres, M.LastNames AS Apellidos, M.PreferredName AS NombreUsual, dbo.fn_GEN_FormatDate(M.BirthDate, 'es') AS FechaNacimiento, M.Gender AS Genero, 
							CASE WHEN @idioma = 'es' THEN 
								CASE WHEN (M.AffiliationStatus = 'AFIL')
									THEN 'Apadrinado'
								WHEN (M.AffiliationStatus = 'DESA')
									THEN 'Desafiliado'
								WHEN (M.AffiliationStatus = 'GRAD')
									THEN 'Graduado'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
									THEN 'Afiliado'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
									THEN 'Miembro Familia Desafiliada'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
									THEN 'Miembro Familia Graduada'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Afiliado (Inactivo)'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Miembro Familia Desafiliada (Inactivo)'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Miembro Familia Graduada (Inactivo)'
								END
							ELSE 
								CASE WHEN (M.AffiliationStatus = 'AFIL')
									THEN 'Sponsored'
								WHEN (M.AffiliationStatus = 'DESA')
									THEN 'Disaffiliate'
								WHEN (M.AffiliationStatus = 'GRAD')
									THEN 'Graduate'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NULL))
									THEN 'Relatives of Affiliate Family'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NULL))
									THEN 'Relatives of Disaffiliate Family'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NULL))
									THEN 'Relatives of Graduate Family'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'AFIL') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Relatives of Affiliate Family (Inactive)'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'DESA') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Relatives of Disaffiliate Family (Inactive)'
								WHEN ((M.AffiliationStatus IS NULL) AND (F.AffiliationStatus = 'GRAD') AND (FMR.InactiveReason IS NOT NULL))
									THEN 'Relatives of Graduate Family (Inactive)'
								END 
							END AS TipoMiembro,
							dbo.fn_GEN_Semaforo(M.Project, M.MemberId) AS Semaforo, F.Classification AS Clasificacion, dbo.fn_GEN_TS(F.Project, F.FamilyId) AS TS, F.Address Direccion, CASE WHEN @idioma = 'es' THEN CGA.DescSpanish ELSE CGA.DescEnglish END AS Area, F.Pueblo, dbo.fn_GEN_regionFamilia(F.Project, F.FamilyId) as Region, YEAR(M.AffiliationStatusDate) AñoEstadoAfil,
                            MEY.SchoolYear AS Año, 
							CASE WHEN @idioma = 'es' THEN cdG.DescSpanish ELSE cdG.DescEnglish END AS Grado,
							CASE WHEN @idioma = 'es' THEN cdES.DescSpanish ELSE cdES.DescEnglish END AS EstadoEducativo,
							CASE WHEN @idioma = 'es' THEN cdL.DescSpanish ELSE cdL.DescEnglish END AS NivelEducativo,
							S.Name AS CentroEducativo,
							CASE WHEN @idioma = 'es' THEN cdEC.DescSpanish ELSE cdEC.DescEnglish END AS CarreraEducativa,
							CASE WHEN @idioma = 'es' THEN cdRNC.DescSpanish ELSE cdRNC.DescEnglish END AS ExcepcionEstadoEduc
							FROM Member M 
                            INNER JOIN Family F ON M.RecordStatus = F.RecordStatus AND M.Project = F.Project AND M.LastFamilyId = F.FamilyId
                            INNER JOIN FamilyMemberRelation FMR ON F.RecordStatus = FMR.RecordStatus AND F.Project = FMR.Project AND M.MemberId = FMR.MemberId AND F.FamilyId = FMR.FamilyId AND
								((M.AffiliationStatus = 'AFIL' AND @filApad = '1') 
								OR (M.AffiliationStatus = 'DESA' AND @filDesaf = '1') 
								OR (M.AffiliationStatus = 'GRAD' AND @filGrad = '1'))
                            LEFT JOIN CdAffiliationStatus CAS ON F.AffiliationStatus = CAS.Code
							LEFT JOIN CdGeographicArea CGA ON CGA.Code = F.Area
                            LEFT OUTER JOIN dbo.MemberEducationYear MEY ON M.Project = MEY.Project AND M.MemberId = MEY.MemberId AND MEY.RecordStatus = ' ' 
                            AND MEY.SchoolYear = @año AND MEY.Grade = dbo.fn_GEN_getActualGrade(MEY.Project, MEY.MemberId, MEY.SchoolYear) 
                            LEFT OUTER JOIN dbo.CdGrade cdG ON MEY.Grade = cdG.Code 
                            LEFT OUTER JOIN dbo.CdEducationStatus cdES ON cdES.Code = MEY.Status 
                            LEFT OUTER JOIN dbo.CdEducationCareer cdEC ON cdEC.Code = MEY.Career 
                            LEFT OUTER JOIN dbo.School S ON S.Project = MEY.Project AND S.RecordStatus = MEY.RecordStatus AND S.Code = MEY.SchoolCode 
                            LEFT OUTER JOIN dbo.CdSchoolType cdST ON cdST.Code = S.SchoolType                              
                            LEFT OUTER JOIN dbo.CdEducationLevel cdL ON cdL.Code = cdG.EducationLevel 
							LEFT OUTER JOIN dbo.CdEducationReasonNottoContinue cdRNC ON cdRNC.Code = MEY.ReasonNotToContinue
							WHERE M.RecordStatus = ' ' AND M.Project = @sitio AND (MEY.Career LIKE @filCarrera OR '' = @filCarrera)  AND (MEY.SchoolCode LIKE @filCentroEduc OR '' = @filCentroEduc) AND (MEY.Status LIKE @filEstadoEduc OR '' = @filEstadoEduc) AND (S.SchoolType LIKE @filTipoEscuela OR '' = @filTipoEscuela) AND (MEY.ReasonNotToContinue LIKE @filExcEstadoEduc OR '' = @filExcEstadoEduc) AND (MEY.Grade LIKE @filGrado OR '' = @filGrado) AND (cdL.Code LIKE @filNivelEduc OR '' = @filNivelEduc)";
            }
            else
            {
                return new DataTable();
            }
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@año", año);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@filCarrera", filCarrera);
            comando.Parameters.AddWithValue("@filCentroEduc", filCentroEduc);
            comando.Parameters.AddWithValue("@filEstadoEduc", filEstadoEduc);
            comando.Parameters.AddWithValue("@filExcEstadoEduc", filExcEstadoEduc);
            comando.Parameters.AddWithValue("@filNivelEduc", filNivelEduc);
            comando.Parameters.AddWithValue("@filTipoEscuela", filTipoEscuela);
            comando.Parameters.AddWithValue("@filGrado", filGrado);
            comando.Parameters.AddWithValue("@filMaestro", filMaestro);
            comando.Parameters.AddWithValue("@filApad", apad);
            comando.Parameters.AddWithValue("@filGrad", grad);
            comando.Parameters.AddWithValue("@filDesaf", desaf);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerMunicipios(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish ELSE DescEnglish
                                    END AS Des FROM CdMunicipality
                                    ORDER BY CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish ELSE DescEnglish
                                    END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerNivelEduc(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' THEN DescSpanish ELSE DescEnglish END AS Des 
                                    FROM CdEducationLevel
                                    ORDER BY CASE WHEN @idioma = 'es' THEN DescSpanish ELSE DescEnglish END ";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable obtenerOtrasAfiliaciones(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END AS Des
                                    FROM CdOtherAffiliation
                                    ORDER BY CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish 
                                    ELSE DescEnglish END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable obtenerObjetivosVisita(String sitio, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT CASE WHEN @idioma = 'es'
                THEN DescSpanish 
                ELSE DescEnglish 
                END Des, Code 
                FROM CdFamilyVisitObjective
                WHERE Inactive = 0 AND Project = @sitio
                ORDER BY CASE WHEN @idioma = 'es'
                THEN DescSpanish 
                WHEN @idioma = 'en' 
                THEN DescEnglish
                ELSE Code
                END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable obtenerRazonesInactivo(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish ELSE DescEnglish
                                    END AS Des FROM CdFamMemRelInactiveReason
                                    ORDER BY CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish ELSE DescEnglish
                                    END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerRelaciones(String idioma, String genero)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish ELSE DescEnglish
                                    END AS Des FROM CdFamilyMemberRelationType
                                    WHERE (Gender = @genero OR '' = @genero)
                                    ORDER BY CASE WHEN @idioma = 'es' 
                                    THEN DescSpanish ELSE DescEnglish
                                    END";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            comando.Parameters.AddWithValue("@genero", genero);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }

        public DataTable obtenerItemsActividades(String sitio, String idioma, String area)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT Code, CASE WHEN '" + idioma + "' = 'es' THEN DescSpanish ELSE DescEnglish END AS Des"
                        + " FROM CdFamilyActivityType WHERE (Project = '" + sitio + "' OR Project = '*') AND Active = 1 AND FunctionalArea = '" + area + "'";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }

        public DataTable obtenerItemsAvisos(String sitio, String idioma, String area)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            String sql = @"SELECT CFWT.Code, CASE WHEN '" + idioma + "' = 'es' THEN CFWT.DescSpanish ELSE CFWT.DescEnglish END AS Des FROM CdFamilyWarningType CFWT"
                        + " WHERE CFWT.Project = '" + sitio + "' AND CFWT.FunctionalArea = '" + area + "' AND CFWT.Active = 1"
                        + " ORDER BY CASE WHEN '" + idioma + "' = 'es' THEN CFWT.DescSpanish ELSE CFWT.DescEnglish END ";
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable obtenerItemsCatalogos(String sql)
        {
            SqlConnection con = new SqlConnection(conexionString);
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;
            SqlCommand showresult = new SqlCommand(sql, con);
            con.Open();
            dr = showresult.ExecuteReader();
            DataTable dt = new DataTable();
            dt.Load(dr);
            dr.Close();
            return dt;
        }
        public DataTable obtenerTiposAfil(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN
                                        @idioma = 'es' THEN 
                                        DescSpanish ELSE DescEnglish END AS Des 
                                        FROM CdAffiliationType 
                                        WHERE AffiliationLevel = 'F' AND Code != 'EXTR'";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerTiposEscuela(String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Code, CASE WHEN @idioma = 'es' THEN DescSpanish ELSE DescEnglish END AS Des 
                                    FROM CdSchoolType
                                    ORDER BY CASE WHEN @idioma = 'es' THEN DescSpanish ELSE DescEnglish END ";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerPueblos(String sitio, String idioma)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT Pueblo AS Des, Pueblo AS Code
                FROM CdGeographicPueblo
                WHERE Active = 1 AND Project = @sitio
                ORDER BY Des";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            comando.Parameters.AddWithValue("@idioma", idioma);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerRegiones()
        {
            DataTable tablaDatos = new DataTable();
            tablaDatos.Columns.Add("Code");
            tablaDatos.Columns.Add("Des");
            tablaDatos.Rows.Add("I", "I");
            tablaDatos.Rows.Add("II", "II");
            return tablaDatos;
        }
        public DataTable obtenerTS(String sitio)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT EmployeeId FROM FwEmployee E1 WHERE 0 < 
                (SELECT COUNT(*) FROM FwEmployee E
                INNER JOIN FamilyEmployeeRelation FER ON E.EmployeeId = FER.EmployeeId
                INNER JOIN Family F ON F.FamilyId = FER.FamilyId AND F.Project = FER.Project AND F.RecordStatus = FER.RecordStatus
                WHERE FER.RecordStatus = ' ' AND FER.EndDate IS NULL AND F.AffiliationStatus = 'AFIL' AND F.Project = @sitio AND FER.EmployeeId = E1.EmployeeId)
                ORDER BY EmployeeId";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
        public DataTable obtenerTS2(String sitio)
        {
            DateTime now = DateTime.Now;
            SqlConnection conexion = new SqlConnection(conexionString);
            conexion.Open();
            String comandoString = @"SELECT E1.EmployeeId FROM FwEmployee E1 
                                    INNER JOIN FwEmployeeRole FER1 
                                    ON Role = 'TS'
                                    AND Organization = @sitio
                                    AND Status = 'ACTV'
									WHERE 0 < 
                                             (SELECT COUNT(*) FROM FwEmployee E
                                            INNER JOIN FamilyEmployeeRelation FER ON E.EmployeeId = FER.EmployeeId
                                            INNER JOIN Family F ON F.FamilyId = FER.FamilyId AND F.Project = FER.Project AND F.RecordStatus = FER.RecordStatus
                                            WHERE FER.RecordStatus = ' ' AND FER.EndDate IS NULL AND F.AffiliationStatus = 'AFIL' AND F.Project = @sitio AND FER.EmployeeId = E1.EmployeeId)
                                    GROUP BY E1.EmployeeId
				                    ORDER BY E1.EmployeeId";
            SqlCommand comando = new SqlCommand(comandoString, conexion);
            comando.Parameters.AddWithValue("@sitio", sitio);
            SqlDataAdapter adaptador = new SqlDataAdapter();
            adaptador.SelectCommand = comando;
            DataTable tablaDatos = new DataTable();
            adaptador.Fill(tablaDatos);
            conexion.Close();
            return tablaDatos;
        }
    }
}