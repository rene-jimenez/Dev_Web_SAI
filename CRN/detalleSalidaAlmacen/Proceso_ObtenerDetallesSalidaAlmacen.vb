Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspDetalleSalidaAlmacen
Namespace nspDetalleSalidaAlmacen
    Public Class Proceso_ObtenerDetallesSalidaAlmacen : Inherits Accion(Of List(Of detalleSalidaAlmacen))
        Public Property tipoConsulta As tipoConsultaDetalleSalidaAlmacen
        Public Property id As Guid
        Public Property idArticulo As Guid?
        Public Property fechaInicial As Date?
        Public Property fechaFinal As Date?
        Public Property idSistema As Guid?
        Public Property idSalida As Guid

        Public Sub New()
            MyBase.New("Proceso_ObtenerDetallesSalidaAlmacen", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of detalleSalidaAlmacen)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaDetalleSalidaAlmacen.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaDetalleSalidaAlmacen.idSalida
                    parametros.Add(New ParametroPredicado("idSalida", idSalida))
                Case tipoConsultaDetalleSalidaAlmacen.rangofechas
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idArticulo", idArticulo))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaDetalleSalidaAlmacen.soloRangoFechas
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaDetalleSalidaAlmacen.todos
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspDetalleSalidaAlmacen.daoDetalleSalidaAlmacen)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class

End Namespace

