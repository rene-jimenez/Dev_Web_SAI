Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspOficio
Namespace nspOficio
    Public Class Proceso_ObtenerUnOficio : Inherits Accion(Of oficio)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnOficio", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As oficio
            Return New Proceso_ObtenerListaOficio() With {.tipoConsulta = tipoConsultaOficio.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class

End Namespace
