Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPedido
Imports CRN.nspEntrada
Imports CES.nspEntrada

Namespace nspPedido
    Public Class Proceso_ObtenerPedidos_Con_Entradas : Inherits Accion(Of List(Of pedido))
        Public Property idSistema As Guid?
        Public Property ConEntrada As Boolean
        Public Sub New()
            MyBase.New("Proceso_ObtenerPedidos_Con_Entradas", "Obtiene el listado de pedidos con Entradas")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of pedido)
            Dim resultado = New Proceso_ObtenerPedidos() With {.tipoConsulta = tipoConsultaPedido.todos, .idSistema = idSistema}.Ejecutar().Where(Function(x) x.verAlmacen = True).OrderBy(Function(n) n.numeroPedido).ToList
            Dim lista As New pedido
            Dim listaRemplazar As New List(Of pedido)

            For Each e In resultado
                Dim oficio = New CRN.nspOficio.Proceso_ObtenerUnOficio() With {.id = e.idOficio}.Ejecutar()
                e.observaciones = IIf(IsNothing(oficio), "", oficio.turnoDRM)
            Next
            For Each e In resultado
                Dim ent = New Proceso_ObtenerEntradas() With {.tipoConsulta = tipoConsultaEntrada.idPedido, .idPedido = e.id}.Ejecutar()
                If ConEntrada Then
                    If ent.Count > 0 Then
                        listaRemplazar.Add(e)
                    End If
                Else
                    If ent.Count = 0 Then
                        listaRemplazar.Add(e)
                    End If
                End If

            Next
            Return listaRemplazar
        End Function
    End Class
End Namespace