Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspPermiso
Namespace nspPermiso
    Public Class Proceso_EliminarPermiso : Inherits Accion(Of respuestaDelProceso)
        Public Property id As Guid
        Public Property idUsuarioMovimiento As Guid
        Public Property ipPcUsuario As String

        Public Sub New()
            MyBase.New("Proceso_EliminarPermiso", "Elimina un registro en la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Dim entidad = New Proceso_ObtenerPermiso() With {.id = id}.Ejecutar()
            Try
                entidad.idUsuarioMovimiento = idUsuarioMovimiento
                entidad.ipPcUsuario = ipPcUsuario
                Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspPermiso.daoPermiso)()
                dao.Eliminar(entidad)
                respuesta.comentario = "La acción se realizó con éxito.."
                respuesta.respuesta = tipoRespuestaDelProceso.Completado
            Catch exSQL As SqlException
                If exSQL.Number = 547 Then ' Cuando un registro ya fue ocupado, las siguientes líneas desactivan la entidad, solo cuando aplique.
                    Try
                        Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspPermiso.daoPermiso)()
                        entidad.esActivo = False
                        dao.Actualizar(entidad)
                    Catch ex2 As Exception
                        respuesta.comentario = "Ocurrió un error al eliminar el registro seleccionado."
                        respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                    End Try
                Else
                End If
            Catch ex As Exception
                respuesta.comentario = "Ocurrió un error al eliminar el registro seleccionado."
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function
    End Class

End Namespace


