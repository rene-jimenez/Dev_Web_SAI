Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspProveedor
Namespace nspProveedor
    Public Class Proceso_ObtenerProveedor : Inherits Accion(Of proveedor)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerProveedor", "Obtiene un registro")
        End Sub

        Protected Overrides Function OnEjecutar() As proveedor
            Return New Proceso_ObtenerProveedores() With {.tipoConsulta = tipoConsultaProveedor.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace

