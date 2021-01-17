Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspArticulo
Namespace nspArticulo
    Public Class Proceso_EliminarArticulo : Inherits Accion(Of respuestaDelProceso)
        Public Property id As Guid
        Public Property idUsuarioMovimiento As Guid
        Public Property ipUsuario As String
        Public Property esActivo As Boolean?
        Public Property idSistema As Guid
        Public Sub New()
            MyBase.New("Proceso_EliminarArticulo", "Elimina un registro en la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Dim entidad = New Proceso_ObtenerArticulo() With {.id = id}.Ejecutar()
            Try
                entidad.idUsuarioMovimiento = idUsuarioMovimiento
                entidad.ipUsuario = ipUsuario
                entidad.idSistema = idSistema
                Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspArticulo.daoArticulo)()
                dao.Eliminar(entidad)
                respuesta.comentario = "La acción se realizó con éxito.."
                respuesta.respuesta = tipoRespuestaDelProceso.Completado
            Catch exSQL As SqlException
                If exSQL.Number = 547 Then ' Cuando un registro ya fue ocupado, las siguientes lineas desactivan la entidad, solo cuando aplique.
                    Try
                        Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspArticulo.daoArticulo)()
                        entidad._tipoEdicion = tipoEdicionArticulo.cambiarEstado
                        entidad.esActivo = esActivo
                        entidad.idUsuarioMovimiento = idUsuarioMovimiento
                        entidad.ipUsuario = ipUsuario
                        entidad.idSistema = idSistema
                        dao.Actualizar(entidad)
                        respuesta.comentario = "ok"
                        respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    Catch ex2 As Exception
                        respuesta.comentario = "Ocurrió un error al eliminar el registro seleccionado."
                        respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                    End Try
                Else
                    respuesta.comentario = exSQL.Message.ToString
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            Catch ex As Exception
                respuesta.comentario = "Ocurrió un error al eliminar el registro seleccionado."
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function
    End Class
End Namespace

