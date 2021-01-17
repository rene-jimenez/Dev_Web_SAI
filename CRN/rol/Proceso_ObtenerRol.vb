Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspRol
Namespace nspRol
    Public Class Proceso_ObtenerUnRol : Inherits Accion(Of rol)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnRol", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As rol
            Return New Proceso_ObtenerListaRol() With {.tipoConsulta = tipoConsultaRol.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class

End Namespace

