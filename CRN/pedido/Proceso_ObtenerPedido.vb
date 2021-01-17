Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPedido
Namespace nspPedido
    Public Class Proceso_ObtenerPedido : Inherits Accion(Of pedido)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerPedido", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As pedido
            Return New Proceso_ObtenerPedidos() With {.tipoConsulta = tipoConsultaPedido.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace

