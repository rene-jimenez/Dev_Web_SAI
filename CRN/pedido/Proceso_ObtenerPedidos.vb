Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPedido
Namespace nspPedido
    Public Class Proceso_ObtenerPedidos : Inherits Accion(Of List(Of pedido))
        Public Property tipoConsulta As tipoConsultaPedido
        Public Property id As Guid
        Public Property idOficio As Guid?
        Public Property numeroPedido As String
        Public Property idSistema As Guid?

        Public Sub New()
            MyBase.New("Proceso_ObtenerPedidos", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of pedido)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaPedido.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaPedido.idOficio
                    parametros.Add(New ParametroPredicado("idOficio", idOficio))
                Case tipoConsultaPedido.x_numeroPedido_idSistema
                    parametros.Add(New ParametroPredicado("numeroPedido", numeroPedido))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaPedido.Sin_afectacion
                    parametros.Add(New ParametroPredicado("idOficio", idOficio))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaPedido.con_Afectacion
                    parametros.Add(New ParametroPredicado("idOficio", idOficio))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaPedido.con_EntradaAlmacen_Completo
                    parametros.Add(New ParametroPredicado("idOficio", idOficio))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaPedido.con_EntradaAlmacen_Parcial
                    parametros.Add(New ParametroPredicado("idOficio", idOficio))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaPedido.todos
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspPedido.daoPedido)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace

