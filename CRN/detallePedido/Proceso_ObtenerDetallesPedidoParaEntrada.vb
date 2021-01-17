Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports CES.nspDetallePedidoParaEntrada
Namespace nspDetallePedidoParaEntrada
    Public Class Proceso_ObtenerDetallesPedidoParaEntrada : Inherits Accion(Of List(Of detallePedidoParaEntrada))
        Public Property tipoConsulta As tipoConsultaDetallePedidoParaEntrada
        Public Property idPedido As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerDetallePedidosParaEntrada", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of detallePedidoParaEntrada)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaDetallePedidoParaEntrada.idPedido
                    parametros.Add(New ParametroPredicado("idPedido", idPedido))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspDetallePedidoParaEntrada.DaoDetallePedidoParaEntrada)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class

End Namespace
