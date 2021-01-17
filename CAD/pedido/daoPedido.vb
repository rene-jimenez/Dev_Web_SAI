Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspPedido
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspPedido
    Public Class daoPedido : Inherits DaoSql(Of pedido)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotImplementedException()
            End If
            Dim tipoConsulta As tipoConsultaPedido = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaPedido)
            Select Case tipoConsulta
                Case tipoConsultaPedido.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaPedido.idOficio
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idOficio", predicado.Parametros("idOficio").Valor)
                Case tipoConsultaPedido.x_numeroPedido_idSistema
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@numeroPedido", predicado.Parametros("numeroPedido").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaPedido.Sin_afectacion
                    comando.Parameters.AddWithValue("@Accion", 4)
                    comando.Parameters.AddWithValue("@idOficio", predicado.Parametros("idOficio").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaPedido.con_Afectacion
                    comando.Parameters.AddWithValue("@Accion", 5)
                    comando.Parameters.AddWithValue("@idOficio", predicado.Parametros("idOficio").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaPedido.con_EntradaAlmacen_Completo
                    comando.Parameters.AddWithValue("@Accion", 6)
                    comando.Parameters.AddWithValue("idOficio", predicado.Parametros("idOficio").Valor)
                    comando.Parameters.AddWithValue("idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaPedido.con_EntradaAlmacen_Parcial
                    comando.Parameters.AddWithValue("@Accion", 7)
                    comando.Parameters.AddWithValue("idOficio", predicado.Parametros("idOficio").Valor)
                    comando.Parameters.AddWithValue("idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaPedido.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            End Select
            comando.CommandText = "proAlm_ObtenerPedido"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As pedido
            Dim pedido As New pedido
            pedido.id = Guid.Parse(lectorRenglonActual("id").ToString)
            pedido.idOficio = Guid.Parse(lectorRenglonActual("idOficio").ToString)
            pedido.idAutoriza = Guid.Parse(lectorRenglonActual("idAutoriza").ToString)
            pedido.idElaboro = Guid.Parse(lectorRenglonActual("idElaboro").ToString)
            pedido.idReviso = Guid.Parse(lectorRenglonActual("idReviso").ToString)
            pedido.idProveedor = Guid.Parse(lectorRenglonActual("idProveedor").ToString)
            pedido.fechaElaboracion = CDate(lectorRenglonActual("fechaElaboracion").ToString)
            pedido.estatusPedido = CBool(lectorRenglonActual("estatusPedido").ToString)
            pedido.numeroPedido = lectorRenglonActual("numeroPedido").ToString
            If lectorRenglonActual("iva").ToString <> "" Then
                pedido.iva = lectorRenglonActual("iva").ToString
            End If
            pedido.idTipoPago = Guid.Parse(lectorRenglonActual("idTipoPago").ToString)
            If lectorRenglonActual("verAlmacen").ToString <> "" Then
                pedido.verAlmacen = CBool(lectorRenglonActual("verAlmacen").ToString)
            End If
            pedido.idPartida = Guid.Parse(lectorRenglonActual("idPartida").ToString)
            If lectorRenglonActual("notasPedido").ToString <> "" Then
                pedido.notasPedido = lectorRenglonActual("notasPedido").ToString
            End If
            pedido.fechaRequerida = CDate(lectorRenglonActual("fechaRequerida").ToString)
            pedido.fechaAcordada = CDate(lectorRenglonActual("fechaAcordada").ToString)
            pedido.fechaRecibido = CDate(lectorRenglonActual("fechaRecibido").ToString)
            pedido.observaciones = lectorRenglonActual("observaciones").ToString
            pedido.descuento = lectorRenglonActual("descuento").ToString
            pedido._nombreAutoriza = lectorRenglonActual("_nombreAutoriza").ToString
            pedido._nombreElaboro = lectorRenglonActual("_nombreElaboro").ToString
            pedido._nombreReviso = lectorRenglonActual("_nombreReviso").ToString
            pedido._nombreProveedor = lectorRenglonActual("_nombreProveedor").ToString
            pedido._nombrePartida = lectorRenglonActual("_nombrePartida").ToString
            pedido._nombreTipoPago = lectorRenglonActual("_nombreTipoPago").ToString
            Return pedido
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As pedido)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarPedido"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idOficio", entidad.idOficio)
                comando.Parameters.AddWithValue("@idAutoriza", entidad.idAutoriza)
                comando.Parameters.AddWithValue("@idElaboro", entidad.idElaboro)
                comando.Parameters.AddWithValue("@idReviso", entidad.idReviso)
                comando.Parameters.AddWithValue("@idProveedor", entidad.idProveedor)
                comando.Parameters.AddWithValue("@estatusPedido", entidad.estatusPedido)
                comando.Parameters.AddWithValue("@numeroPedido", entidad.numeroPedido)
                If Not entidad.iva Is Nothing Then
                    comando.Parameters.AddWithValue("@iva", entidad.iva)
                End If
                comando.Parameters.AddWithValue("@idTipoPago", entidad.idTipoPago)
                If Not entidad.verAlmacen Is Nothing Then
                    comando.Parameters.AddWithValue("@verAlmacen", entidad.verAlmacen)
                End If
                comando.Parameters.AddWithValue("@idPartida", entidad.idPartida)
                comando.Parameters.AddWithValue("@fechaRequerida", entidad.fechaRequerida)
                comando.Parameters.AddWithValue("@fechaAcordada", entidad.fechaAcordada)
                comando.Parameters.AddWithValue("@fechaRecibido", entidad.fechaRecibido)
                If Not entidad.observaciones Is Nothing Then
                    comando.Parameters.AddWithValue("@observaciones", entidad.observaciones)
                End If
                comando.Parameters.AddWithValue("@descuento", entidad.descuento)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)

            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As pedido)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarPedido"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idAutoriza", entidad.idAutoriza)
                comando.Parameters.AddWithValue("@idElaboro", entidad.idElaboro)
                comando.Parameters.AddWithValue("@idReviso", entidad.idReviso)
                comando.Parameters.AddWithValue("@idProveedor", entidad.idProveedor)
                comando.Parameters.AddWithValue("@estatusPedido", entidad.estatusPedido)
                If Not entidad.iva Is Nothing Then
                    comando.Parameters.AddWithValue("@iva", entidad.iva)
                End If
                comando.Parameters.AddWithValue("@idTipoPago", entidad.idTipoPago)
                If Not entidad.verAlmacen Is Nothing Then
                    comando.Parameters.AddWithValue("@verAlmacen", entidad.verAlmacen)
                End If
                comando.Parameters.AddWithValue("@idPartida", entidad.idPartida)
                If Not entidad.notasPedido Is Nothing Then
                    comando.Parameters.AddWithValue("@notasPedido", entidad.notasPedido)
                End If

                comando.Parameters.AddWithValue("@fechaRequerida", entidad.fechaRequerida)
                comando.Parameters.AddWithValue("@fechaAcordada", entidad.fechaAcordada)
                comando.Parameters.AddWithValue("@fechaRecibido", entidad.fechaRecibido)
                If Not entidad.observaciones Is Nothing Then
                    comando.Parameters.AddWithValue("@observaciones", entidad.observaciones)
                End If
                comando.Parameters.AddWithValue("@descuento", entidad.descuento)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace

