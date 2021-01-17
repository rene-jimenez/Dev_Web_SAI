Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspSalidaAlmacen
Namespace nspSalidaAlmacen
    Public Class Proceso_ObtenerSalidasAlmacen : Inherits Accion(Of List(Of salidaAlmacen))
        Public Property tipoConsulta As tipoConsultaSalidaAlmacen
        Public Property id As Guid
        Public Property fechaInicial As Date?
        Public Property fechaFinal As Date?
        Public Property idSistema As Guid?
        Public Property idArea As Guid?

        Public Sub New()
            MyBase.New("Proceso_ObtenerSalidasAlmacen", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of salidaAlmacen)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaSalidaAlmacen.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaSalidaAlmacen.rangoFechas
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaSalidaAlmacen.rangoFechasObtenerAreas
                    parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                    parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaSalidaAlmacen.valesSalidaPoridArea
                    parametros.Add(New ParametroPredicado("idArea", idArea))
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
                Case tipoConsultaSalidaAlmacen.todos
                    parametros.Add(New ParametroPredicado("idSistema", idSistema))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspSalidaAlmacen.daoSalidaAlmacen)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class

End Namespace

