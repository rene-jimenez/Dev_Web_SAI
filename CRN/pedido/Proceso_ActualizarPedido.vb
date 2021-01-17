Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports Contexto.Notificaciones
Imports System.Data.SqlClient
Imports CES.nspPedido
Imports System.Transactions
Namespace nspPedido
    Public Class Proceso_ActualizarPedido : Inherits Accion(Of respuestaDelProceso)
        Public Property entidad As pedido
        Public Property idOficio As Guid?
        Public Property idEstatusOficio As Guid?
        Public Property idCargoPresupuestal As Guid?
        Public Property idSistema As Guid?
        Public Property idUsuarioMovimiento As Guid?
        Public Property ipUsuario As String
        Dim controladorMensajes As New notificacionesDeUsuario
        Public Sub New()
            MyBase.New("Proceso_ActualizarPedido", "Actualiza un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Using scope As New TransactionScope()

                    Dim resultadoValidación = validar()
                    If resultadoValidación.respuesta = tipoRespuestaDelProceso.Completado Then

                        Dim oficioAnterior = New CRN.nspOficio.Proceso_ObtenerUnOficio() With {.id = idOficio}.Ejecutar
                        Dim respuestaOficio = New CRN.nspOficio.Proceso_EditarOficio_EspecialPedidoSolicitud() With {.idOficio = idOficio, .idArea = oficioAnterior.idArea, .idCargoPresupuestal = idCargoPresupuestal, .idEstatusOficio = idEstatusOficio,
                                                                                                .idSistema = idSistema, .idUsuarioMovimiento = idUsuarioMovimiento, .ipUsuario = ipUsuario}.Ejecutar
                        If respuestaOficio.respuesta <> tipoRespuestaDelProceso.Completado Then
                            Throw New Exception(resultadoValidación.comentario)
                        End If
                        Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspPedido.daoPedido)()
                        dao.Actualizar(entidad)
                        respuesta.comentario = "ok"
                        respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    Else
                        Throw New Exception(resultadoValidación.comentario)
                    End If
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

        Private Function validar() As respuestaDelProceso
            Dim respuesta As New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try

                'Campos obligatorios para el proceso
                If idUsuarioMovimiento Is Nothing Then
                    Throw New Exception("El idUsuario movimiento es campo obligatorio")
                End If
                'If idSistema Is Nothing Then
                '    Throw New Exception("El idSistema es campo obligatorio")
                'End If
                If ipUsuario.Length = 0 Then
                    Throw New Exception("La ipUsuario es campo obligatorio")
                End If

                'Campos obligatorios para pedido

                If entidad.idAutoriza Is Nothing Then
                    Throw New Exception("idAutoriza es campo obligatorio")
                End If
                If entidad.idElaboro Is Nothing Then
                    Throw New Exception("idElaboro es campo obligatorio")
                End If
                If entidad.idReviso Is Nothing Then
                    Throw New Exception("idReviso es campo obligatorio")
                End If
                If entidad.idProveedor Is Nothing Then
                    Throw New Exception("idProveedor es campo obligatorio")
                End If
                If entidad.idPartida Is Nothing Then
                    Throw New Exception("idPartida es campo obligatorio")
                End If
                If entidad.iva Is Nothing Then
                    Throw New Exception("Iva es campo obligatorio")
                End If
                If entidad.idTipoPago Is Nothing Then
                    Throw New Exception("idTipoPago es campo obligatorio")
                End If
                If entidad.verAlmacen Is Nothing Then
                    Throw New Exception("verAlmacen es campo obligatorio")
                End If
                If entidad.fechaRequerida Is Nothing Then
                    Throw New Exception("fechaRequerida es campo obligatorio")
                End If
                If entidad.fechaAcordada Is Nothing Then
                    Throw New Exception("fechaAcordada es campo obligatorio")
                End If
                If entidad.fechaRecibido Is Nothing Then
                    Throw New Exception("fechaRecibido es campo obligatorio")
                End If


                If entidad.verAlmacen = True And entidad.estatusPedido = False Then
                    Throw New Exception("Es necesario validar en la CPD que para marcar la casilla ver almacen, es necesario marcar tambien la casilla de pedido")
                End If


                If entidad.estatusPedido = True And idEstatusOficio <> Guid.Parse("35747111-2222-3333-4444-111111111114") Then
                    Throw New Exception("IdEstatusOficio incorrecto cuando se marca la casilla pedido.")
                End If




                'Campos obligatorios para oficio

                If idOficio Is Nothing Then
                    Throw New Exception("idOficio es campo obligatorio")
                End If
                If idCargoPresupuestal Is Nothing Then
                    Throw New Exception("idCargoPresupuestal es campo obligatorio")
                End If
                If idEstatusOficio Is Nothing Then
                    Throw New Exception("idEstatusOficio es campo obligatorio")
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

