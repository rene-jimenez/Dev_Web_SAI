Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPermiso
Namespace nspPermiso
    Public Class Proceso_ObtenerPermisos : Inherits Accion(Of List(Of permiso))
        Public Property tipoConsulta As tipoConsultapermiso
        Public Property id As Guid
        Public Property idRol As Guid
        Public Property esActivo As Boolean?

        Public Sub New()
            MyBase.New("Proceso_ObtenerPermisos", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of permiso)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultapermiso.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultapermiso.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))

                Case tipoConsultapermiso.idRol
                    parametros.Add(New ParametroPredicado("idRol", idRol))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspPermiso.daoPermiso)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class

End Namespace

