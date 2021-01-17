Imports cadenero.entidades
Namespace nspProveedor
    <Serializable>
    Public Class proveedor
        Inherits nspBase.base
        Public Property ciudad As String
        Public Property codigoPostal As String
        Public Property colonia As String
        Public Property contacto As String
        Public Property domicilioFiscal As String
        Public Property domicilio As String
        Public Property estado As String
        Public Property giro As String
        Public Property rfc As String
        Public Property _tipoEdicion As tipoEdicionProveedor
        Public Sub New()

        End Sub
    End Class
End Namespace

