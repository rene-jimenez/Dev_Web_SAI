Namespace nspDetalleSalidaAlmacen
    <Serializable>
    Public Class detalleSalidaAlmacen
        Public Property id As Guid
        Public Property idSalida As Guid?
        Public Property idArticulo As Guid?
        Public Property cantidad As Integer
        Public Property fecha As Date?
        Public Property idSistema As Guid?
        Public Property ipUsuario As String
        Public Property idUsuarioMovimiento As Guid?
        Public Property _nombreArticulo As String
        Public Property _codigoBarras As String
        Public Property _ultimoPrecio As Double
        Public Property _importe As Double
        Public Property _nombreArea As String
    End Class
End Namespace

