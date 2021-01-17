Imports cadenero.entidades.nspBase
Namespace nspEntrada
    Public Class entrada : Inherits base
        Public Property idPedido As Guid?
        Public Property numEntrada As String
        Public Property tipo As Boolean
        Public Property fechaEntrada As Date?
        Public Property esNota As Boolean
        Public Property numRemision As String
        Public Property comentario As String
        Public Property _numeroPedido As String
        Public Property _nombreProveedor As String
        Public Property _fechaPedidoRecibido As String
        Public Property _turnoDRM As String
        Public Property _tipoPago As String

        Public Sub New()

        End Sub

    End Class
End Namespace

