Imports Contexto.Accion.Accion
Imports CES.nspDetalleEntrada
Namespace nspDetalleEntrada
    Public Class Proceso_ObtenerDetalleEntrada : Inherits Accion(Of detalleEntrada)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerDetalleEntrada", "Obtiene un registro")
        End Sub

        Protected Overrides Function OnEjecutar() As detalleEntrada
            Return New Proceso_ObtenerDetallesEntrada() With {.tipoConsulta = tipoConsultaDetalleEntrada.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace

