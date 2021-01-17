Namespace nspUsuarios
    <Serializable>
    Public Class usuarios
        Public Property id As Guid?
        Public Property email As String
        Public Property esActivo As Boolean
        Public Property fechaAlta As Date
        Public Property fechaBaja As Date?
        Public Property nombre As String
        Public Property contrasena As String
        Public Property esResetcontrasena As Boolean
        Public Property telefono As String
        Public Property usuario As String
        Public Property ultimaSesion As String
        Public Property idSistema As Guid?

        Public Sub New()

        End Sub
    End Class
End Namespace
