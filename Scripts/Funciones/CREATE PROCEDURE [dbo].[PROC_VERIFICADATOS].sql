USE [sys_sodexo]
GO
/****** Object:  StoredProcedure [dbo].[PROC_VERIFICADATOS]    Script Date: 06/24/2015 14:42:19 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO



--CREATE PROCEDURE [dbo].[PROC_VERIFICADATOS](
CREATE PROCEDURE [dbo].[PROC_VERIFICADATOS](
	@salMensajeProc			VARCHAR(150) OUTPUT,
	@salEstatusProc			BIT			 OUTPUT
) AS BEGIN

	SET NOCOUNT ON
	SET @salEstatusProc = 0
	SET @salMensajeProc = ''
	
	DECLARE @dato DECIMAL(20,2)
	
	--//--	CUENTAS CONTABLES--//--
	IF EXISTS(
		 
		 SELECT TOP 5 ' | ' + S.CAT_CODIGO 
			FROM Temp_asidetalle S 
			WHERE CAT_CODIGO NOT IN (SELECT CAT_CODIGO FROM con_catalogo)  
	) 	
	
	BEGIN
		SET @salMensajeProc='Favor verificar cuentas contables!!'
		RETURN
	END
	
		--//--	CENTROS DE COSTOS--//--
	IF EXISTS(

		SELECT TOP 5 ' | ' + S.CCOS_CODIGO 
			FROM Temp_asidetalle S 
			WHERE CCOS_CODIGO NOT IN (SELECT CCOS_CODIGO FROM con_ccostos)  AND CCOS_CODIGO <>''

	) 	
	
	BEGIN
		SET @salMensajeProc='Favor verificar centros de costos!!'
		RETURN
	END
	
		--//--	CLIENTES--//--
	IF EXISTS(
		
		SELECT TOP 5 ' | ' + S.DET_CODTERCERO 
			FROM Temp_asidetalle S 
			WHERE DET_CODTERCERO NOT IN (SELECT CLI_CODIGO FROM CLI_CLIENTES union all SELECT PRO_CODIGO  FROM PRO_PROVEEDORES)  AND DET_CODTERCERO <>''

	) 	
	
	BEGIN
		SET @salMensajeProc='Favor verificar tercero!!'
		RETURN
	END
	
			--//--	PROVEEDORES--//--
	IF EXISTS(
		
		SELECT TOP 5 ' | ' + S.DET_CODTERCERO 
			FROM Temp_asidetalle S 
			WHERE DET_CODTERCERO NOT IN (SELECT CLI_CODIGO FROM CLI_CLIENTES union all SELECT PRO_CODIGO  FROM PRO_PROVEEDORES)  AND DET_CODTERCERO <>''

	) 	
	
	BEGIN
		SET @salMensajeProc='Favor verificar tercero!!'
		RETURN
	END


	--//--	CONSECUTIVOS--//--
	IF EXISTS(		
			SELECT DBO.CON_ASIDETALLE.ASI_NUMERO, DBO.CON_ASIDETALLE.TDOC_CODIGO,DBO.CON_ASIDETALLE.DET_NUMDOC 
			FROM DBO.CON_ASIDETALLE, DBO.Temp_asidetalle
			WHERE DBO.CON_ASIDETALLE.TDOC_CODIGO = DBO.Temp_asidetalle.TDOC_CODIGO
			AND  DBO.CON_ASIDETALLE.DET_NUMDOC = DBO.Temp_asidetalle.DET_NUMDOC	
			and DBO.CON_ASIDETALLE.TDOC_CODIGO in(select CONF_DATO from emp_configuracion where CONF_DESCRIPCION in ('CliTDocVent','PrvTDocComp','PrvTDocImp'))
AND  DBO.CON_ASIDETALLE.DET_NUMDOC <>''
			) 	
	
	BEGIN
		SET @salMensajeProc='Consecutivos ingresados ya existen para este tipo de documento!!'
		RETURN
	END

	--//--	FECHA DE PERIODO--//--
	IF NOT EXISTS(		
			select 1 from (
			select top 1 ASI_FECHA from [dbo].[Temp_asientos] ) as d
			where d.ASI_FECHA between convert(datetime,(select CONF_DATO from emp_configuracion where CONF_DESCRIPCION = 'FecEjerINI'),103)
			and convert(datetime,(select CONF_DATO+' 23:59:59.000' from emp_configuracion where CONF_DESCRIPCION = 'FecEjerFIN'),103)		
			) 	
	BEGIN
		SET @salMensajeProc='Las fechas de documento se encuentran fuera del rango del periodo contable!!'
		RETURN
	END
	
		--//--	VERIFICA CUADRE DE ASIENTO--//--
		
	SELECT @dato= SUM(det_montodl)-SUM(det_montohl) from dbo.Temp_asidetalle 
	
	IF @dato <> 0
	
	BEGIN
		SET @salMensajeProc='Debe y Haber no suman igual!!'
		RETURN
	END



SET @salEstatusProc = 1
		
END




