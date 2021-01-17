Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspRol
Namespace nspRol
    Public Class Proceso_EliminarRol : Inherits Accion(Of respuestaDelProceso)
        Public Property id As Guid
        Public Property idUsuarioMovimiento As Guid
        Public Property ipPcUsuario As String
        Public Property esActivo As Boolean
        Public Sub New()
            MyBase.New("Proceso_EliminarRol", "Elimina un registro en la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Dim entidad = New Proceso_ObtenerUnRol() With {.id = id}.Ejecutar()
            entidad.idUsuarioMovimiento = idUsuarioMovimiento
            entidad.ipPcUsuario = ipPcUsuario
            Try
                Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspRol.daoRol)()
                dao.Eliminar(entidad)
                respuesta.comentario = "La acción se realizó con éxito.."
                respuesta.respuesta = tipoRespuestaDelProceso.Completado
            Catch exSQL As SqlException
                If exSQL.Number = 547 Then ' Cuando un registro ya fue ocupado, las siguientes lineas desactivan la entidad, solo cuando aplique.
                    Try
                        Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspRol.daoRol)()
                        entidad.esActivo = esActivo
                        dao.Actualizar(entidad)
                        respuesta.comentario = "La acción se realizó con éxito.."
                        respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    Catch ex2 As Exception
                        respuesta.comentario = ex2.Message.ToString
                        respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                    End Try
                Else
                End If
            Catch ex As Exception
                respuesta.comentario = ex.Message.ToString
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function
    End Class

End Namespace

