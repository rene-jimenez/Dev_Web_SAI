Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspOficio
Namespace nspOficio
    Public Class Proceso_ComplementarOficio : Inherits Accion(Of respuestaDelProceso)
        Public Property idOficio As Guid?
        Public Property idResponsable As Guid?
        Public Property idRubro As Guid?
        Public Property idUsuarioMovimiento As Guid?
        Public Property indicaciones As String
        Public Property ipUsuario As String

        Public Sub New()
            MyBase.New("Proceso_ComplementarOficio", "Actualiza un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try

                Dim resultadoValidación = validar()
                If resultadoValidación.respuesta = tipoRespuestaDelProceso.Completado Then
                    Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspOficio.daoOficio)()
                    Dim oficioAEditar = New Proceso_ObtenerUnOficio() With {.id = idOficio}.Ejecutar
                    oficioAEditar.idResponsable = idResponsable
                    oficioAEditar.idRubro = idRubro
                    oficioAEditar.idUsuarioMovimiento = idUsuarioMovimiento
                    oficioAEditar.indicaciones = indicaciones
                    oficioAEditar.ipUsuario = ipUsuario
                    oficioAEditar._tipoEdicion = tipoEdicionOficio.complementar
                    dao.Actualizar(oficioAEditar)
                    respuesta.comentario = "ok"
                    respuesta.respuesta = tipoRespuestaDelProceso.Completado
                Else
                    Throw New Exception(resultadoValidación.comentario)
                End If
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
                If idResponsable Is Nothing Then
                    Throw New Exception("El idResponsable es campo obligatorio")
                End If
                If idUsuarioMovimiento Is Nothing Then
                    Throw New Exception("El idUsuario movimiento es campo obligatorio")
                End If
                If idRubro Is Nothing Then
                    Throw New Exception("El idRubro es campo obligatorio")
                End If
                If indicaciones.Length = 0 Then
                    Throw New Exception("Indicación es campo obligatorio")
                End If
                If ipUsuario.Length = 0 Then
                    Throw New Exception("Ip usuario es campo obligatorio")
                End If
                respuesta.respuesta = tipoRespuestaDelProceso.Completado
            Catch ex As Exception
                respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                respuesta.comentario = ex.Message.ToString
            End Try
            Return respuesta
        End Function

    End Class
End Namespace

