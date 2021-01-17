Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspDetallePedido
Namespace nspDetallePedido
    Public Class Proceso_ObtenerDetallePedido : Inherits Accion(Of detallePedido)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerDetallePedido", "Obtiene un registro")
        End Sub

        Protected Overrides Function OnEjecutar() As detallePedido
            Return New Proceso_ObtenerDetallePedidos() With {.tipoConsulta = tipoConsultaDetallePedido.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace

