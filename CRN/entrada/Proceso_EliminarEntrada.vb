Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports System.Transactions
Imports CES.nspEntrada
Imports CRN.nspArticulo
Namespace nspEntrada
    Public Class Proceso_EliminarEntrada : Inherits Accion(Of respuestaDelProceso)

        Public Property id As Guid
        Public Property idUsuarioMovimiento As Guid
        Public Property ipUsuario As String
        Public Property idSistema As Guid
        'Public Property idArticulo As Guid
        Public Property listaDetalleEntrada As List(Of CES.nspDetalleEntrada.detalleEntrada)
        Public Sub New()
            MyBase.New("Proceso_EliminarEntrada", "Elimina un registro en la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Dim entidad = New Proceso_ObtenerEntrada() With {.id = id}.Ejecutar()
            Try
                Using scope As New TransactionScope()
                    entidad.idUsuarioMovimiento = idUsuarioMovimiento
                    entidad.ipUsuario = ipUsuario
                    entidad.idSistema = idSistema
                    For i = 0 To listaDetalleEntrada.Count - 1
                        Dim ActualizarArt = New Proceso_ObtenerArticulo() With {.id = listaDetalleEntrada(i).idArticulo}.Ejecutar()
                        ActualizarArt.existencia = ActualizarArt.existencia - listaDetalleEntrada(i).cantidad
                        ActualizarArt.idUsuarioMovimiento = idUsuarioMovimiento
                        ActualizarArt.ipUsuario = ipUsuario
                        ActualizarArt.idSistema = idSistema
                        Dim respuestaArt = New Proceso_ActualizarArticulo() With {.entidad = ActualizarArt}.Ejecutar()
                        If respuestaArt.respuesta <> tipoRespuestaDelProceso.Completado Then
                            Throw New Exception(respuestaArt.comentario)
                        End If
                        Dim respuestaDetallesEntrada = New CRN.nspDetalleEntrada.Proceso_EliminarDetalleEntrada() With {.id = listaDetalleEntrada(i).id, .idUsuarioMovimiento = listaDetalleEntrada(i).idUsuarioMovimiento, .idSistema = listaDetalleEntrada(i).idSistema, .ipUsuario = listaDetalleEntrada(i).ipUsuario}.Ejecutar
                        If respuestaDetallesEntrada.respuesta <> tipoRespuestaDelProceso.Completado Then
                            Throw New Exception(respuestaDetallesEntrada.comentario)
                        End If
                    Next
                    Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspEntrada.daoEntrada)()
                    dao.Eliminar(entidad)
                    respuesta.comentario = "ok"
                    respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    scope.Complete()
                End Using
            Catch exSQL As SqlException
                If exSQL.Number = 547 Then ' Cuando un registro ya fue ocupado, las siguientes lineas desactivan la entidad, solo cuando aplique.
                    respuesta.comentario = "Imposible eliminar el regitro ya fue ocupado."
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
