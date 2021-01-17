Imports cadenero.entidades.nspBase
Namespace nspTelefonoProveedor
    <Serializable>
    Public Class telefonoProveedor
        Inherits base
        Public Property idProveedor As Guid?
        Public Property codigoLargaDistancia As String
        Public Property numero As String
        Public Property extension As String
        Public Property tipo As String
        Public Property _nombre As String
        Public Property _ciudad As String
        Public Property _colonia As String
        Public Property _contacto As String
        Public Property _domicilio As String
        Public Property _estado As String
        Public Property _giro As String
        Public Property _rfc As String

        Public Sub New()
        End Sub
    End Class
End Namespace

