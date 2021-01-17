Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspProveedor
Namespace nspProveedor
    Public Class Proceso_ObtenerProveedores : Inherits Accion(Of List(Of proveedor))

        Public Property tipoConsulta As tipoConsultaProveedor
        Public Property id As Guid
        Public Property esActivo As Boolean?
        Public Sub New()
            MyBase.New("Proceso_ObtenerProveedor", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of proveedor)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaProveedor.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaProveedor.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaProveedor.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspProveedor.daoProveedor)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace

