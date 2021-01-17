Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports CES.nspDetalleEntrada

Namespace nspDetalleEntrada
    Public Class Proceso_ObtenerDetallesEntrada : Inherits Accion(Of List(Of detalleEntrada))
        Public Property tipoConsulta As tipoConsultaDetalleEntrada
        Public Property id As Guid
        Public Property idSistema As Guid?
        Public Property idEntrada As Guid?
        Public Property fechaInicial As Date?
        Public Property fechaFinal As Date?
        Public Property idArticulo As Guid?
        Public Property esParcial As Boolean

        Public Sub New()
            MyBase.New("Proceso_ObtenerDetalleEntrada", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of detalleEntrada)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaDetalleEntrada.idEntrada
                    parametros.Add(New ParametroPredicado("idEntrada", idEntrada))
                Case tipoConsultaDetalleEntrada.idEntradaEsParcial
                    parametros.Add(New ParametroPredicado("idEntrada", idEntrada))
                Case tipoConsultaDetalleEntrada.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaDetalleEntrada.rangoFechas
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idArticulo", idArticulo))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaDetalleEntrada.rangoFechasProvedor
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaDetalleEntrada.todos
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspDetalleEntrada.daoDetalleEntrada)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace

