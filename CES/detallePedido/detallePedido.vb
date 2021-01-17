Imports cadenero.entidades.nspBase
Namespace nspDetallePedido
    <Serializable>
    Public Class detallePedido : Inherits base
        Public Property idArticulo As Guid?
        Public Property idPedido As Guid?
        Public Property precioUnitario As Double
        Public Property cantidad As Integer
        Public Property _articulo As String
        Public Property _numeroPedido As Integer
        Public Property _total As Double
        Public Property _unidadMedidaArticulo As String
        Public Sub New()
        End Sub
    End Class
End Namespace

