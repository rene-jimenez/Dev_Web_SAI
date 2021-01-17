Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspSalidaAlmacen
Namespace nspSalidaAlmacen
    Public Class Proceso_ObtenerSalidaAlmacen : Inherits Accion(Of salidaAlmacen)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerSalidaAlmacen", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As salidaAlmacen
            Return New Proceso_ObtenerSalidasAlmacen() With {.tipoConsulta = tipoConsultaSalidaAlmacen.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class

End Namespace


