Imports cadenero.entidades.nspBase
Namespace nspSalidaAlmacen
    <Serializable>
    Public Class salidaAlmacen : Inherits base

        Public Property idArea As Guid?
        Public Property esVale As Boolean?
        Public Property numVale As String
        Public Property numOficio As String
        Public Property fechaSalida As Date?
        Public Property comentario As String
        Public Property _nombreArea As String
        Public Property _codigoArea As String
        Public Property _codigoConNombreArea As String
        Public Sub New()


        End Sub

    End Class
End Namespace
