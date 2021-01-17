Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspSalidaAlmacen
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspSalidaAlmacen
    Public Class daoSalidaAlmacen : Inherits DaoSql(Of salidaAlmacen)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaSalidaAlmacen = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaSalidaAlmacen)
            Select Case tipoConsulta
                Case tipoConsultaSalidaAlmacen.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaSalidaAlmacen.rangoFechas
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaSalidaAlmacen.rangoFechasObtenerAreas
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaSalidaAlmacen.valesSalidaPoridArea
                    comando.Parameters.AddWithValue("@Accion", 4)
                    comando.Parameters.AddWithValue("@idArea", predicado.Parametros("idArea").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaSalidaAlmacen.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)

            End Select

            comando.CommandText = "proAlm_ObtenerSalidaAlmacen"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As salidaAlmacen
            Dim accion As Integer = lectorRenglonActual("accion").ToString
            Dim salidaAlmacen As New salidaAlmacen
            Select Case accion
                Case 3
                    salidaAlmacen.idArea = Guid.Parse(lectorRenglonActual("idArea").ToString)
                    salidaAlmacen._codigoConNombreArea = lectorRenglonActual("codigoConNombreArea").ToString
                Case Else
                    salidaAlmacen.id = Guid.Parse(lectorRenglonActual("id").ToString)
                    salidaAlmacen.idArea = Guid.Parse(lectorRenglonActual("idArea").ToString)
                    salidaAlmacen.esVale = CBool(lectorRenglonActual("esVale").ToString)
                    salidaAlmacen.numVale = lectorRenglonActual("numVale").ToString
                    If lectorRenglonActual("numOficio").ToString <> "" Then
                        salidaAlmacen.numOficio = lectorRenglonActual("numOficio").ToString
                    End If
                    salidaAlmacen.fechaSalida = CDate(lectorRenglonActual("fechaSalida").ToString)
                    If lectorRenglonActual("comentario").ToString <> "" Then
                        salidaAlmacen.comentario = lectorRenglonActual("comentario").ToString
                    End If
                    salidaAlmacen._nombreArea = lectorRenglonActual("nombreArea").ToString
                    salidaAlmacen._codigoArea = lectorRenglonActual("codigoArea").ToString
                    salidaAlmacen._codigoConNombreArea = lectorRenglonActual("codigoConNombreArea").ToString
                    salidaAlmacen.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
                    salidaAlmacen.ipUsuario = lectorRenglonActual("ipUsuario").ToString
                    salidaAlmacen.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)

            End Select

            Return salidaAlmacen
        End Function

        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As salidaAlmacen)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarSalidaAlmacen"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idArea", entidad.idArea)
                comando.Parameters.AddWithValue("@esVale", entidad.esVale)
                comando.Parameters.AddWithValue("@numVale", entidad.numVale)
                If Not entidad.numOficio Is Nothing Then
                    comando.Parameters.AddWithValue("@numOficio", entidad.numOficio)
                End If
                comando.Parameters.AddWithValue("@fechaSalida", entidad.fechaSalida)
                If Not entidad.comentario Is Nothing Then
                    comando.Parameters.AddWithValue("@comentario", entidad.comentario)
                End If
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As salidaAlmacen)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarSalidaAlmacen"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idArea", entidad.idArea)
                comando.Parameters.AddWithValue("@esVale", entidad.esVale)
                comando.Parameters.AddWithValue("@numVale", entidad.numVale)
                If Not entidad.numOficio Is Nothing Then
                    comando.Parameters.AddWithValue("@numOficio", entidad.numOficio)
                End If
                comando.Parameters.AddWithValue("@fechaSalida", entidad.fechaSalida)
                If Not entidad.comentario Is Nothing Then
                    comando.Parameters.AddWithValue("@comentario", entidad.comentario)
                End If
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As salidaAlmacen)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarSalidaAlmacen"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace


