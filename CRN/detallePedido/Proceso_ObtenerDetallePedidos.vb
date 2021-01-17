Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspDetallePedido
Namespace nspDetallePedido
    Public Class Proceso_ObtenerDetallePedidos : Inherits Accion(Of List(Of detallePedido))
        Public Property tipoConsulta As tipoConsultaDetallePedido
        Public Property id As Guid
        Public Property idPedido As Guid?
        Public Property idArticulo As Guid?
        Public Property idSistema As Guid?
        Public Sub New()
            MyBase.New("Proceso_ObtenerDetallePedidos", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of detallePedido)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaDetallePedido.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaDetallePedido.idPedido
                    parametros.Add(New ParametroPredicado("idPedido", idPedido))
                Case tipoConsultaDetallePedido.xPedidoArticulo
                    parametros.Add(New ParametroPredicado("idPedido", idPedido))
                    parametros.Add(New ParametroPredicado("idArticulo", idArticulo))
                Case tipoConsultaDetallePedido.todos
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspDetallePedido.daoDetallePedido)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace

