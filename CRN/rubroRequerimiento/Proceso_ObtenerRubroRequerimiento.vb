Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspRubroRequerimiento
Namespace nspRubroRequerimiento
    Public Class Proceso_ObtenerRubroRequerimiento : Inherits Accion(Of rubroRequerimiento)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerRubroRequerimiento", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As rubroRequerimiento
            Return New Proceso_ObtenerRubroRequerimientos() With {.tipoConsulta = tipoConsultaRubroRequerimiento.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace

