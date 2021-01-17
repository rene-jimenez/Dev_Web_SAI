Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspEntrada
Imports System.Transactions
Namespace nspEntrada
    Public Class Proceso_AgregarEntrada : Inherits Accion(Of respuestaDelProceso)
        Public Property entidad As entrada
        Public Property listaDetalleEntrada As List(Of CES.nspDetalleEntrada.detalleEntrada)
        Public Property idSistema As Guid?
        Public Property idUsuarioMovimiento As Guid?
        Public Property ipUsuario As String
        Dim controladorMensajes As New notificacionesDeUsuario
        Public Sub New()
            MyBase.New("Proceso_AgregarEntrada", "Agrega un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Using scope As New TransactionScope()
                    Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspEntrada.daoEntrada)()
                    dao.Agregar(entidad)
                    For i = 0 To listaDetalleEntrada.Count - 1
                        Dim respuestaDetallesEntrada = New CRN.nspDetalleEntrada.Proceso_AgregarDetalleEntrada() With {.entidad = listaDetalleEntrada(i)}.Ejecutar
                        If respuestaDetallesEntrada.respuesta <> tipoRespuestaDelProceso.Completado Then
                            Throw New Exception(respuestaDetallesEntrada.comentario)
                        End If
                    Next
                    respuesta.comentario = "ok"
                    respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    scope.Complete()
                End Using
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_registro_estaria_duplicado, "entrada")
                    respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                Else
                    respuesta.comentario = ex.Message.ToString
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            Catch ex As Exception
                respuesta.comentario = ex.Message.ToString
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function
    End Class
End Namespace

