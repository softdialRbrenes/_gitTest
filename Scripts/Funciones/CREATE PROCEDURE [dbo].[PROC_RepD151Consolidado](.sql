USE [sys_sodexo]
GO
/****** Object:  StoredProcedure [dbo].[PROC_RepD151Consolidado]    Script Date: 06/24/2015 14:42:09 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[PROC_RepD151Consolidado](
	-- VARIABLES DE ENTRADA	
	 @entFechaIni DATETIME,
	 @entFechaFin DATETIME

)AS
BEGIN
	-- VARIABLES INTERNAS
	
	DECLARE @varFechaFinPerAnt  VARCHAR(50)
	DECLARE @varFechaIniPerAct  VARCHAR(50)
	DECLARE @varFechaIni		VARCHAR(50)
	DECLARE @varFechaFin		VARCHAR(50)
	DECLARE @varBDPeriodoAnt	VARCHAR(50)
	DECLARE @varBDPeriodoAct	VARCHAR(50)
	
	DECLARE @varSqlAnt		VARCHAR(MAX)
	DECLARE @varSqlAct		VARCHAR(MAX)
	
	DECLARE @varSqlTempBD	VARCHAR(MAX)
	
		-- DECLARO TABLA
	DECLARE @varTabla TABLE
	(	
			DOCUMENTO		VARCHAR(100),
			CODIGO		    VARCHAR(100),
			FECHA			DATETIME,
			TOTAL			DECIMAL(25,2),
			D151			INT		
	)

	DECLARE @varTBLFechaPer TABLE(dFecha varchar(50))

	IF NOT EXISTS(SELECT 1 FROM sys.objects WHERE name = 'temp_RepD151Consolidado')
	BEGIN
		
		CREATE TABLE [dbo].[temp_RepD151Consolidado](
			[CODIGO] [varchar](100) NULL,
			[NOMBRE] [varchar](250) NULL,
			[CEDULA] [varchar](50) NULL,
			[DOCUMENTO] [varchar](100) NULL,
			[FECHA] [datetime] NULL,
			[TOTAL] [decimal](25, 2) NULL
						
		) 

	END
	ELSE
	BEGIN
		DELETE FROM dbo.temp_RepD151Consolidado
	END
	 		
	-- ASIGNACIONES

	--Asignar valorles de fechas para pruebas del stoted procedure
	--SELECT @entFechaIni = '2013-10-01'
	--SELECT @entFechaFin = '2014-09-30'
	
	SELECT @varBDPeriodoAnt = EMP_PERIODOANT FROM emp_empresa
	SELECT @varBDPeriodoAct = 'sys_'+ EMP_CODIGO FROM emp_empresa
	
	--//Llena la fecha de fin de periodo del periodo anterior
		SET @varSqlTempBD= 
		'SELECT  SUBSTRING(CONF_DATO,7,4)+''-''+ SUBSTRING(CONF_DATO,4,2)+''-''+SUBSTRING(CONF_DATO,1,2)+'' 23:59:59''
		FROM '+ @varBDPeriodoAnt + '.dbo.EMP_CONFIGURACION 
		WHERE CONF_DESCRIPCION =''FecEjerFIN'''
		
		INSERT INTO @varTBLFechaPer	EXEC (@varSqlTempBD)
		
		SELECT @varFechaFinPerAnt = dFecha FROM @varTBLFechaPer
	
	--//Llena la fecha de fin de periodo del periodo anterior
		SET @varSqlTempBD= 
		'SELECT SUBSTRING(CONF_DATO,7,4)+''-''+ SUBSTRING(CONF_DATO,4,2)+''-''+SUBSTRING(CONF_DATO,1,2)+'' 00:00:00''
		FROM sys_sodexo.dbo.EMP_CONFIGURACION 
		WHERE CONF_DESCRIPCION =''FecEjerINI'''
		
		INSERT INTO @varTBLFechaPer	EXEC (@varSqlTempBD)
		
		SELECT @varFechaIniPerAct = dFecha FROM @varTBLFechaPer
	
	
	--//Formateo de variables de entrada, fecha de inicio y fin
	SELECT @varFechaIni = SUBSTRING(CONVERT(VARCHAR(50),@entFechaIni,120),1,10) + ' 00:00:00'
	SELECT @varFechaFin = SUBSTRING(CONVERT(VARCHAR(50),@entFechaFin,120),1,10) + ' 23:59:59'
	
	
	-- SELECCIONA LOS DATOS DEL PERIODO ANTERIOR

	SET  @varSqlAnt =''+
	'SELECT (AD.TDOC_CODIGO +''-''+ AD.DET_NUMDOC) AS DOC, AD.DET_CODTERCERO, A.ASI_FECHA, (AD.DET_MontoHL - AD.DET_MontoDL) AS TOTAL, A.ASI_D151
	FROM '+ @varBDPeriodoAnt +'.dbo.CON_ASIDETALLE AD
	INNER JOIN '+ @varBDPeriodoAnt +'.dbo.con_asientos A
	ON AD.ASI_NUMERO = A.ASI_NUMERO	
		INNER JOIN
			(
				SELECT DISTINCT AD.DET_NUMEROREF, LEFT(AD.DET_NUMEROREF,charindex(''+'',AD.DET_NUMEROREF)-1) AS ASIENTO, 
				ROUND(SUM(AD.DET_MontoHL) - SUM(AD.DET_MontoDL),2) AS SALDOL, 
				ROUND(SUM(AD.DET_MontoHE) - SUM(AD.DET_MontoDE),2) AS SALDOE 
				FROM '+ @varBDPeriodoAnt +'.dbo.CON_ASIDETALLE AD 
				WHERE AD.DET_TIPTERCERO=2 AND AD.DET_CODTERCERO like ''%'' 
				AND AD.DET_NUMEROREF<>'''' AND DET_CONSECREF='''' 
				GROUP BY AD.DET_NUMEROREF 
			) AS C
				ON convert(varchar(50),AD.DET_NUMEROREF) = convert(varchar(50),C.DET_NUMEROREF)
				AND AD.ASI_NUMERO = C.ASIENTO
				AND AD.DET_FECHA BETWEEN '''+ @varFechaIni +''' AND '''+ @varFechaFinPerAnt +'''
	ORDER BY AD.DET_CODTERCERO, A.ASI_FECHA'

	INSERT INTO @varTabla (	DOCUMENTO, CODIGO, FECHA, TOTAL, D151)
	EXEC (@varSqlAnt)

	-- SELECCIONA LOS DATOS DEL PERIODO ACTUAL
	SET  @varSqlAct =''+
	'SELECT (AD.TDOC_CODIGO +''-''+ AD.DET_NUMDOC) AS DOC, AD.DET_CODTERCERO, A.ASI_FECHA, (AD.DET_MontoHL - AD.DET_MontoDL) AS TOTAL, A.ASI_D151
	FROM '+ @varBDPeriodoAct +'.dbo.CON_ASIDETALLE AD
	INNER JOIN '+ @varBDPeriodoAct +'.dbo.con_asientos A
	ON AD.ASI_NUMERO = A.ASI_NUMERO	
		INNER JOIN
			(
				SELECT DISTINCT AD.DET_NUMEROREF, LEFT(AD.DET_NUMEROREF,charindex(''+'',AD.DET_NUMEROREF)-1) AS ASIENTO, 
				ROUND(SUM(AD.DET_MontoHL) - SUM(AD.DET_MontoDL),2) AS SALDOL, 
				ROUND(SUM(AD.DET_MontoHE) - SUM(AD.DET_MontoDE),2) AS SALDOE 
				FROM '+ @varBDPeriodoAct +'.dbo.CON_ASIDETALLE AD 
				WHERE AD.DET_TIPTERCERO=2 AND AD.DET_CODTERCERO like ''%'' 
				AND AD.DET_NUMEROREF<>'''' AND DET_CONSECREF='''' 
				GROUP BY AD.DET_NUMEROREF 
			) AS C
				ON convert(varchar(50),AD.DET_NUMEROREF) = convert(varchar(50),C.DET_NUMEROREF)
				AND AD.ASI_NUMERO = C.ASIENTO
				AND AD.DET_FECHA BETWEEN ''' + @varFechaIniPerAct + ''' AND ''' + @varFechaFin + '''
				
	ORDER BY AD.DET_CODTERCERO, A.ASI_FECHA'


	INSERT INTO @varTabla (	DOCUMENTO, CODIGO, FECHA, TOTAL, D151)
	EXEC (@varSqlAct)


	INSERT INTO temp_RepD151Consolidado
	SELECT T.CODIGO,P.PRO_NOMBRE,P.PRO_CEDJUR,T.DOCUMENTO,  CONVERT(VARCHAR,T.FECHA,110) AS FECHA, 
	case when T.D151=1 then 	((T.TOTAL) / ((i.IMP_PORCENTAJE/100)+1)) 	else 	(T.TOTAL) end as TOTAL
	FROM @varTabla T
	INNER JOIN pro_proveedores P ON T.CODIGO=P.PRO_CODIGO
	LEFT JOIN INV_IMPUESTOS I ON P.IMP_CODIGO = I.IMP_CODIGO
	ORDER BY T.CODIGO, T.FECHA
			
END 










