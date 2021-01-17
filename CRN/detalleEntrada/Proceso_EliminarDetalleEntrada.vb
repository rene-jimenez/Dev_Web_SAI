Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports System.Transactions
Imports CES.nspDetalleEntrada, CRN.nspArticulo

Namespace nspDetalleEntrada
    Public Class Proceso_EliminarDetalleEntrada : Inherits Accion(Of respuestaDelProceso)
        Public Property id As Guid
        Public Property idUsuarioMovimiento As Guid
        Public Property ipUsuario As String
        Public Property idSistema As Guid
        Public Sub New()
            MyBase.New("Proceso_EliminarDetalleEntrada", "Elimina un registro en la base de datos")
        End Sub

        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Dim entidad = New Proceso_ObtenerDetalleEntrada() With {.id = id}.Ejecutar()
            Try
                Using scope As New TransactionScope()
                    entidad.idUsuarioMovimiento = idUsuarioMovimiento
                    entidad.ipUsuario = ipUsuario
                    entidad.idSistema = idSistema
                    Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspDetalleEntrada.daoDetalleEntrada)()
                    dao.Eliminar(entidad)
                    respuesta.comentario = "ok"
                    respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    scope.Complete()
                End Using
            Catch ex As Exception
                respuesta.comentario = "Ocurrió un error al eliminar el registro seleccionado."
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function
    End Class
End Namespace

