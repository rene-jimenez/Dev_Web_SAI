Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspOficio
Namespace nspOficio
    Public Class Proceso_AgregarOficio : Inherits Accion(Of respuestaDelProceso)
        Public Property idArea As Guid?
        Public Property asunto As String
        Public Property turnoDRM As String
        Public Property turnoSAF As String
        Public Property idSistema As Guid?
        Public Property idUsuarioMovimiento As Guid?
        Public Property ipUsuario As String
        Public Sub New()
            MyBase.New("Proceso_AgregarOficio", "Agrega un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Dim resultadoValidación = validar()
                If resultadoValidación.respuesta = tipoRespuestaDelProceso.Completado Then
                    Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspOficio.daoOficio)()
                    Dim nuevoOficio As New oficio
                    nuevoOficio.id = Guid.NewGuid
                    nuevoOficio.idArea = idArea
                    nuevoOficio.asunto = asunto
                    nuevoOficio.turnoDRM = turnoDRM
                    nuevoOficio.turnoSAF = turnoSAF
                    nuevoOficio.idSistema = idSistema
                    nuevoOficio.idUsuarioMovimiento = idUsuarioMovimiento
                    nuevoOficio.ipUsuario = ipUsuario
                    nuevoOficio._tipoGuardar = tipoGuardarOficio.Normal
                    dao.Agregar(nuevoOficio)
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
                If idArea Is Nothing Then
                    Throw New Exception("El área es campo obligatorio")
                End If
                If idSistema Is Nothing Then
                    Throw New Exception("El idSistema es campo obligatorio")
                End If
                If idUsuarioMovimiento Is Nothing Then
                    Throw New Exception("El idUsuario movimiento es campo obligatorio")
                End If
                If asunto.Length = 0 Then
                    Throw New Exception("El asunto es campo obligatorio")
                End If
                If turnoDRM.Length = 0 Then
                    Throw New Exception("El turno DRM es campo obligatorio")
                End If
                If ipUsuario.Length = 0 Then
                    Throw New Exception("La ipUsuario es campo obligatorio")
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
