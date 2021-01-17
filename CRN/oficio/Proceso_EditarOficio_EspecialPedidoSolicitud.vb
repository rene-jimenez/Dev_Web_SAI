Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspOficio
Namespace nspOficio
    Public Class Proceso_EditarOficio_EspecialPedidoSolicitud : Inherits Accion(Of respuestaDelProceso)
        Public Property idOficio As Guid?
        Public Property idArea As Guid?
        Public Property idUsuarioMovimiento As Guid?
        Public Property idSistema As Guid?
        Public Property idCargoPresupuestal As Guid?
        Public Property idEstatusOficio As Guid?
        Public Property ipUsuario As String

        Public Sub New()
            MyBase.New("Proceso_EditarOficio_EspecialPedidoSolicitud", "Actualiza un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspOficio.daoOficio)()
                Dim oficioAEditar = New Proceso_ObtenerUnOficio() With {.id = idOficio}.Ejecutar
                oficioAEditar.idArea = idArea
                oficioAEditar.idCargoPresupuestal = idCargoPresupuestal
                oficioAEditar.idEstatusOficio = idEstatusOficio
                oficioAEditar.idUsuarioMovimiento = idUsuarioMovimiento
                oficioAEditar.ipUsuario = ipUsuario
                oficioAEditar.idSistema = idSistema
                oficioAEditar._tipoEdicion = tipoEdicionOficio.especial_Pedido_Solicitud
                dao.Actualizar(oficioAEditar)
                respuesta.comentario = "ok"
                respuesta.respuesta = tipoRespuestaDelProceso.Completado
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    respuesta.comentario = "No puede duplicar la información en la base de datos."
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                Else
                    respuesta.comentario = ex.Message.ToString
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            Catch ex As Exception
                respuesta.comentario = "Ocurrió un error al agregar un registro en la base de datos"
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function
        Private Function validar() As respuestaDelProceso
            Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                If idOficio Is Nothing Then
                    Throw New Exception("El idOficio es campo obligatorio")
                End If
                If idArea Is Nothing Then
                    Throw New Exception("El área es campo obligatorio")
                End If
                If idCargoPresupuestal Is Nothing Then
                    Throw New Exception("El idCargoPresupuestal es campo obligatorio")
                End If
                If idUsuarioMovimiento Is Nothing Then
                    Throw New Exception("El idUsuario movimiento es campo obligatorio")
                End If
                If idEstatusOficio Is Nothing Then
                    Throw New Exception("El idEstatusOficio es campo obligatorio")
                End If

                If ipUsuario.Length = 0 Then
                    Throw New Exception("La ipUsuario es campo obligatorio")
                End If
                If idSistema Is Nothing Then
                    Throw New Exception("El idSistema es campo obligatorio")
                End If
            Catch ex As Exception
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = ex.Message.ToString
            End Try
            Return respuesta
        End Function
    End Class
End Namespace
