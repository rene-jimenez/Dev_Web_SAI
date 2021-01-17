Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspEntrada
Imports System.Transactions
Namespace nspEntrada
    Public Class Proceso_ActualizarEntrada : Inherits Accion(Of respuestaDelProceso)
        Public Property entidad As entrada
        Dim controladorMensajes As New notificacionesDeUsuario

        Public Sub New()
            MyBase.New("Proceso_ActualizarEntrada", "Actualiza un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Using scope As New TransactionScope()

                    Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspEntrada.daoEntrada)()
                    dao.Actualizar(entidad)
                    respuesta.comentario = "ok"
                    respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    scope.Complete()
                End Using
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    respuesta.comentario = "El registro estaría duplicado"
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

