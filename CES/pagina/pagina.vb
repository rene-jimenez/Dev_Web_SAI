Imports cadenero.entidades.nspBase
Namespace nspPagina
    <Serializable>
    Public Class pagina
        Inherits base
        Public Property url As String
        Public Property jerarquia As Integer
        Public Property idPaginaPadre As Guid?
        Public Sub New()

        End Sub
    End Class
End Namespace

