Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspPedido
Imports System.Transactions
Namespace nspPedido
    Public Class Proceso_AgregarPedidoSinDRM : Inherits Accion(Of respuestaDelProceso)
        Public Property entidad As pedido
        Public Property listaDetallesPedido As List(Of CES.nspDetallePedido.detallePedido)
        Public Property oficio As CES.nspOficio.oficio

        Public Property idSistema As Guid?
        Public Property idUsuarioMovimiento As Guid?
        Public Property ipUsuario As String


        Dim controladorMensajes As New notificacionesDeUsuario
        Public Sub New()
            MyBase.New("Proceso_AgregarPedidoSinDRM", "Agrega un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Using scope As New TransactionScope()
                    entidad.id = Guid.NewGuid
                    Dim idOficio As Guid = Guid.NewGuid
                    Dim resultadoValidación = validar()
                    If resultadoValidación.respuesta = tipoRespuestaDelProceso.Completado Then

                        'Guardar oficio
                        oficio.id = idOficio
                        oficio.idSistema = idSistema
                        oficio.ipUsuario = ipUsuario
                        oficio.idUsuarioMovimiento = idUsuarioMovimiento
                        Dim respuestaOficio = New CRN.nspOficio.Proceso_AgregarOficioEspecial() With {.idOficio = idOficio, .idArea = oficio.idArea, .idCargoPresupuestal = oficio.idCargoPresupuestal, .idEstatusOficio = oficio.idEstatusOficio,
                                                                                                .turnoDRM = oficio.turnoDRM, .idSistema = idSistema, .idUsuarioMovimiento = idUsuarioMovimiento, .ipUsuario = ipUsuario}.Ejecutar
                        If respuestaOficio.respuesta <> tipoRespuestaDelProceso.Completado Then
                            Throw New Exception(resultadoValidación.comentario)
                        End If

                        'Guardar pedido
                        Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspPedido.daoPedido)()
                        entidad.idOficio = idOficio
                        entidad.idSistema = idSistema
                        entidad.ipUsuario = ipUsuario
                        entidad.idUsuarioMovimiento = idUsuarioMovimiento
                        entidad.numeroPedido = CInt(New CRN.nspGenerico.Proceso_ObtenerDatoGenerico() With {.idSistema = idSistema, .tipoConsulta = CES.nspGenerico.tipoConsultaGenerico.ultimoNumeroPedido}.Ejecutar.valor.ToString)
                        dao.Agregar(entidad)


                        'Guardar detallesPedido

                        For i = 0 To listaDetallesPedido.Count - 1
                            Dim respuestaDetallesPedido = New CRN.nspDetallePedido.Proceso_AgregarDetallePedido() With {.entidad = listaDetallesPedido(i)}.Ejecutar
                            If respuestaDetallesPedido.respuesta <> tipoRespuestaDelProceso.Completado Then
                                Throw New Exception(respuestaDetallesPedido.comentario)
                            End If
                        Next
                        respuesta.comentario = "ok"
                        respuesta.respuesta = tipoRespuestaDelProceso.Completado
                        scope.Complete()
                    Else
                        Throw New Exception(resultadoValidación.comentario)
                    End If
                End Using
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_registro_estaria_duplicado, "pedido")
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
                If idSistema Is Nothing Then
                    Throw New Exception("El idSistema es campo obligatorio")
                End If
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

                'Campos obligatorios para oficio

                If oficio.idArea Is Nothing Then
                    Throw New Exception("idArea es campo obligatorio")
                End If
                If oficio.idCargoPresupuestal Is Nothing Then
                    Throw New Exception("idCargoPresupuestal es campo obligatorio")
                End If
                If oficio.idEstatusOficio Is Nothing Then
                    Throw New Exception("idEstatusOficio es campo obligatorio")
                End If

                If oficio.turnoDRM = "" Then
                    Throw New Exception("El turnoDRM es campo obligatorio")
                End If


                If entidad.verAlmacen = True And entidad.estatusPedido = False Then
                    Throw New Exception("Es necesario validar en la CPD que para marcar la casilla ver almacen, es necesario marcar tambien la casilla de pedido")
                End If


                If entidad.estatusPedido = True And oficio.idEstatusOficio <> Guid.Parse("35747111-2222-3333-4444-111111111114") Then
                    Throw New Exception("IdEstatusOficio incorrecto cuando se marca la casilla pedido.")
                End If


                'Campos obligatorios para lista detalles pedido

                If listaDetallesPedido.Count = 0 Then
                    Throw New Exception("La lista de detalles pedido no puede estar vacía")
                Else
                    For i = 0 To listaDetallesPedido.Count - 1

                        If listaDetallesPedido(i).cantidad = 0 Then
                            Throw New Exception("El valor del campo cantidad debe ser mayor a cero, verifique la lista antes de continuar.")
                        End If
                        If listaDetallesPedido(i).precioUnitario = 0 Then
                            Throw New Exception("El valor del campo precio unitario debe ser mayor a cero, verifique la lista antes de continuar.")
                        End If
                        If listaDetallesPedido(i).idArticulo Is Nothing Then
                            Throw New Exception("El campo artículo es obligatorio, verifique la lista antes de continuar.")
                        End If

                        'Las siguientes lineas complementar los detalles de pedido
                        listaDetallesPedido(i).id = Guid.NewGuid
                        listaDetallesPedido(i).idPedido = entidad.id
                        listaDetallesPedido(i).idSistema = idSistema
                        listaDetallesPedido(i).idUsuarioMovimiento = idUsuarioMovimiento
                        listaDetallesPedido(i).ipUsuario = ipUsuario
                    Next
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

