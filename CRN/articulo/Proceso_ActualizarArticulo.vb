Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspArticulo
Namespace nspArticulo
    Public Class Proceso_ActualizarArticulo : Inherits Accion(Of respuestaDelProceso)
        Public Property entidad As articulo
        Dim controladorMensajes As New notificacionesDeUsuario
        Public Sub New()
            MyBase.New("Proceso_ActualizarArticulo", "Actualiza un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try

                Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspArticulo.daoArticulo)()
                entidad._tipoEdicion = tipoEdicionArticulo.editar
                dao.Actualizar(entidad)
                respuesta.comentario = "ok"
                respuesta.respuesta = tipoRespuestaDelProceso.Completado
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_registro_estaria_duplicado, "articulo")
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
