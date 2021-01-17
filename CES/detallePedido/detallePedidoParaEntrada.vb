Imports cadenero.entidades.nspBase
Namespace nspDetallePedidoParaEntrada
    <Serializable>
    Public Class detallePedidoParaEntrada : Inherits base
        Public Property idArticulo As Guid?
        Public Property idPedido As Guid?
        Public Property precioUnitario As Double
        Public Property cantidadPedida As Integer
        Public Property _articulo As String
        Public Property _codigoBarras As String
        Public Property _existencia As Integer
        Public Property _numeroPedido As Integer
        Public Property _subTotal As Double
        Public Property _ivaSubtotal As Double
        Public Property _total As Double
        Public Property _unidadMedidaArticulo As String
        Public Sub New()
        End Sub
    End Class
End Namespace

