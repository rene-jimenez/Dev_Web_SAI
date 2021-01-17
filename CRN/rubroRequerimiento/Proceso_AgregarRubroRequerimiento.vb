Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspRubroRequerimiento
Namespace nspRubroRequerimiento
    Public Class Proceso_AgregarRubroRequerimiento : Inherits Accion(Of respuestaDelProceso)

        Public Property entidad As rubroRequerimiento
        Dim controladorMensaje As New notificacionesDeUsuario
        Public Sub New()
            MyBase.New("Proceso_AgregarRubroRequerimiento", "Agregar un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respueta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspRubroRequerimiento.daoRubroRequerimiento)()
                dao.Agregar(entidad)
                respueta.comentario = "ok"
                respueta.respuesta = tipoRespuestaDelProceso.Completado
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    respueta.comentario = controladorMensaje.obtenerMensaje(tipoNotificacionesDeUsuario._El_registro_estaria_duplicado, "rubro")
                    respueta.respuesta = tipoRespuestaDelProceso.Advertencia
                Else
                    respueta.comentario = ex.Message.ToString
                    respueta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            Catch ex As Exception
                respueta.comentario = ex.Message.ToString
                respueta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respueta
        End Function
    End Class
End Namespace
