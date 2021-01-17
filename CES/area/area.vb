Imports cadenero.entidades
Namespace nspArea
    <Serializable>
    Public Class area
        Inherits nspBase.base
        Public Property codigo As String
        Public Property idAreaPadre As Guid?
        Public Property tipo As String
        Public Property jerarquia As String
        Public Property _tipoEdicion As tipoEdicionArea
        Public Sub New()
        End Sub
    End Class
End Namespace

