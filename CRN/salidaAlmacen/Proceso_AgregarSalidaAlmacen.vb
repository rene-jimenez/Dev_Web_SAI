Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspSalidaAlmacen, CES.nspDetalleSalidaAlmacen
Imports System.Transactions
Namespace nspSalidaAlmacen
    Public Class Proceso_AgregarSalidaAlmacen : Inherits Accion(Of respuestaDelProceso)
        Public Property entidad As salidaAlmacen
        Public Property listaDetalleSalida As List(Of detalleSalidaAlmacen)
        Public Sub New()
            MyBase.New("Proceso_AgregarSalidaAlmacen", "Agrega un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Using scope As New TransactionScope
                    Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspSalidaAlmacen.daoSalidaAlmacen)()
                    dao.Agregar(entidad)
                    For i = 0 To listaDetalleSalida.Count - 1

                        listaDetalleSalida(i).idSalida = entidad.id
                        Dim daoDetalles = New CRN.nspDetalleSalidaAlmacen.Proceso_AgregarDetalleSalidaAlmacen() With {.entidad = listaDetalleSalida(i)}.Ejecutar()
                        'If daoDetalles.respuesta <> tipoRespuestaDelProceso.Completado Then
                        '    Throw New Exception(respuesta.comentario)
                        'End If

                    Next

                    respuesta.comentario = "ok"
                    respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    scope.Complete()
                End Using

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

    End Class
End Namespace

