Imports cadenero.entidades.nspBase
Namespace nspArticulo
    <Serializable>
    Public Class articulo
        Inherits base
        Public Property idUnidadMedida As Guid?
        Public Property codigoBarras As String
        Public Property cantidadInicial As Integer
        Public Property existencia As Integer
        Public Property stockMinimo As Integer
        Public Property stockMaximo As Integer
        Public Property url As String
        Public Property idCategoria As Guid?
        Public Property ultimoPrecio As Decimal
        Public Property entraAlmacen As Boolean
        Public Property tipoSistema As String
        Public Property _tipoEdicion As tipoEdicionArticulo
        Public Property nombreUnidadMedida As String
        Public Property nombreCategoria As String

        Public Sub New()

        End Sub
    End Class
End Namespace

