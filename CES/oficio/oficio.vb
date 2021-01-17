Imports cadenero.entidades.nspBase
Namespace nspOficio
    <Serializable>
    Public Class oficio : Inherits base
        Public Property idArea As Guid?
        Public Property idCargoPresupuestal As Guid?
        Public Property idEstatusOficio As Guid?
        Public Property idResponsable As Guid?
        Public Property idRubro As Guid?
        Public Property asunto As String
        Public Property comentarios As String
        Public Property fechaAtendido As Date?
        Public Property fechaCaptura As Date?
        Public Property esAtendido As Boolean?
        Public Property esDocInterno As Boolean?
        Public Property indicaciones As String
        Public Property turnoDRM As String
        Public Property turnoSAF As String
        Public Property _area As String
        Public Property _cargoPresupuestal As String
        Public Property _estatusOficio As String
        Public Property _rubro As String
        Public Property _responsable As String
        Public Property _sistema As String
        Public Property _tipoEdicion As tipoEdicionOficio
        Public Property _tipoGuardar As tipoGuardarOficio
        Public Sub New()
        End Sub
    End Class

End Namespace


