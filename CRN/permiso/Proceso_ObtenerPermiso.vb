Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPermiso
Namespace nspPermiso
    Public Class Proceso_ObtenerPermiso : Inherits Accion(Of permiso)
        Public Property tipoConsulta As tipoConsultapermiso
        Public Property id As Guid
        Public Property idRol As Guid

        Public Sub New()
            MyBase.New("Proceso_ObtenerPermiso", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As permiso
            Return New Proceso_ObtenerPermisos() With {.tipoConsulta = tipoConsultapermiso.id, .id = id}.Ejecutar().FirstOrDefault()

            '    Dim parametros As New List(Of ParametroPredicado)
            '    parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            '    Select Case tipoConsulta
            '        Case tipoConsultapermiso.id
            '            parametros.Add(New ParametroPredicado("id", id))
            '        Case tipoConsultapermiso.idRol
            '            parametros.Add(New ParametroPredicado("idRol", idRol))
            '    End Select
            '    Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspPermiso.daoPermiso)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace

