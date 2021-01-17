Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports CES.nspEntrada
Namespace nspEntrada
    Public Class Proceso_ObtenerEntradas : Inherits Accion(Of List(Of entrada))
        Public Property tipoConsulta As tipoConsultaEntrada
        Public Property id As Guid
        Public Property idPedido As Guid?
        Public Property idSistema As Guid?
        Public Property numEntrada As String
        Public Property fechaInicial As Date?
        Public Property fechaFinal As Date?
        Public Property tipo As Boolean?
        Public Sub New()
            MyBase.New("Proceso_ObtenerEntradas", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of entrada)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaEntrada.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaEntrada.idPedido
                    parametros.Add(New ParametroPredicado("idPedido", idPedido))
                   ' parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaEntrada.numEntrada
                    parametros.Add(New ParametroPredicado("numEntrada", numEntrada))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaEntrada.rangoFecha
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaEntrada.tipo
                    parametros.Add(New ParametroPredicado("tipo", tipo))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaEntrada.todos
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspEntrada.daoEntrada)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace

