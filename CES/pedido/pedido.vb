Imports cadenero.entidades.nspBase
Namespace nspPedido
    <Serializable>
    Public Class pedido : Inherits base
        Public Property idOficio As Guid?
        Public Property idAutoriza As Guid?
        Public Property idElaboro As Guid?
        Public Property idReviso As Guid?
        Public Property idProveedor As Guid?
        Public Property fechaElaboracion As Date?
        Public Property estatusPedido As Boolean?
        Public Property numeroPedido As Integer
        Public Property iva As Boolean?
        Public Property idTipoPago As Guid?
        Public Property verAlmacen As Boolean?
        Public Property idPartida As Guid?
        Public Property notasPedido As String
        Public Property fechaRequerida As Date?
        Public Property fechaAcordada As Date?
        Public Property fechaRecibido As Date?
        Public Property observaciones As String
        Public Property descuento As Double
        Public Property _nombreAutoriza As String
        Public Property _nombreElaboro As String
        Public Property _nombreReviso As String
        Public Property _nombreProveedor As String
        Public Property _nombrePartida As String
        Public Property _nombreTipoPago As String
        Public Sub New()

        End Sub
    End Class
End Namespace

