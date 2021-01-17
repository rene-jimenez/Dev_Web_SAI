Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspArticulo
Namespace nspArticulo
    Public Class Proceso_ObtenerArticulo : Inherits Accion(Of articulo)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerArticulo", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As articulo
            Return New Proceso_ObtenerArticulos() With {.tipoConsulta = tipoConsultaArticulo.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace

