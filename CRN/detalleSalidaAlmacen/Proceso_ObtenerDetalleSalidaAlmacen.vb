Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspDetalleSalidaAlmacen
Namespace nspDetalleSalidaAlmacen
    Public Class Proceso_ObtenerDetalleSalidaAlmacen : Inherits Accion(Of detalleSalidaAlmacen)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerDetalleSalidaAlmacen", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As detalleSalidaAlmacen
            Return New Proceso_ObtenerDetallesSalidaAlmacen() With {.tipoConsulta = tipoConsultaDetalleSalidaAlmacen.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class

End Namespace


