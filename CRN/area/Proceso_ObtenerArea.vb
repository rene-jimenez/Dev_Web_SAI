Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspArea
Namespace nspArea
    Public Class Proceso_ObtenerArea : Inherits Accion(Of area)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnaArea", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As area
            Return New Proceso_ObtenerAreas() With {.tipoConsulta = tipoConsultaArea.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace

