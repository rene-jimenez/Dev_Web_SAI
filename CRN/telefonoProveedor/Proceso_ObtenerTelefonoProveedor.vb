Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspTelefonoProveedor
Namespace nspTelefonoProveedor
    Public Class Proceso_ObtenerTelefonoProveedor : Inherits Accion(Of telefonoProveedor)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerUnTelefonoProveedor", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As telefonoProveedor
            Return New Proceso_ObtenerTelefonosProveedor() With {.tipoConsulta = tipoConsultaTelefonoProveedor.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace

