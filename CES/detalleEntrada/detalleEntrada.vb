Imports cadenero.entidades.nspBase
Namespace nspDetalleEntrada
    <Serializable>
    Public Class detalleEntrada : Inherits base
        Public Property idEntrada As Guid?
        Public Property idArticulo As Guid?
        Public Property cantidad As Integer
        Public Property esParcial As Boolean
        Public Property fechaEntrada As Date?
        Public Property _esNota As Boolean?
        Public Property _numEntrada As String
        Public Property _numRemision As String
        Public Property _codigoBarras As String
        Public Property _nombreProveedor As String
        Public Property _nombreArticulo As String
        Public Property _fechaElaboracionPedido As Date?
        Public Property _cantidadPedido As Integer
        Public Property _iva As Boolean?
        Public Property _unidadMedidaArticulo As String
        Public Property _precioUnitario As Double
        Public Property _cantidadFaltante As Integer
        Public Property _valorIva As Double
        Public Property _subTotal As Double
        Public Property _total As Double

        Public Sub New()

        End Sub
    End Class
End Namespace

