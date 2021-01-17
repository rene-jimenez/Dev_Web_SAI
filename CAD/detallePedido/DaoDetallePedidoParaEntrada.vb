Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspDetallePedidoParaEntrada
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspDetallePedidoParaEntrada
    Public Class DaoDetallePedidoParaEntrada : Inherits DaoSql(Of detallePedidoParaEntrada)

        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("idPedido")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro idPedido para hacer consulta.")
            End If
            comando.Parameters.AddWithValue("@idPedido", predicado.Parametros("idPedido").Valor)
            comando.CommandText = "proAlm_ObtenerDetallePedidoParaEntrada"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As detallePedidoParaEntrada
            Dim detallePedidoParaEntrada As New detallePedidoParaEntrada
            detallePedidoParaEntrada.id = Guid.Parse(lectorRenglonActual("id").ToString)
            detallePedidoParaEntrada.idArticulo = Guid.Parse(lectorRenglonActual("idArticulo").ToString)
            detallePedidoParaEntrada.idPedido = Guid.Parse(lectorRenglonActual("idPedido").ToString)
            detallePedidoParaEntrada.precioUnitario = lectorRenglonActual("precioUnitario")
            detallePedidoParaEntrada.cantidadPedida = lectorRenglonActual("cantidadPedida")
            detallePedidoParaEntrada._articulo = lectorRenglonActual("_articulo")
            detallePedidoParaEntrada._codigoBarras = lectorRenglonActual("_codigoBarras")
            detallePedidoParaEntrada._existencia = lectorRenglonActual("_existencia")
            detallePedidoParaEntrada._numeroPedido = Decimal.Parse(lectorRenglonActual("_numeroPedido").ToString)
            detallePedidoParaEntrada._subTotal = Decimal.Parse(lectorRenglonActual("_subTotal").ToString)
            detallePedidoParaEntrada._ivaSubtotal = Decimal.Parse(lectorRenglonActual("_ivaSubtotal").ToString)
            detallePedidoParaEntrada._total = Decimal.Parse(lectorRenglonActual("_total").ToString)
            detallePedidoParaEntrada._unidadMedidaArticulo = lectorRenglonActual("_unidadMedidaArticulo")
            Return detallePedidoParaEntrada
        End Function
    End Class

End Namespace



