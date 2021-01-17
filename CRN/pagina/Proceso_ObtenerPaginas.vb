Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspPagina
Namespace nspPagina
    Public Class Proceso_ObtenerPaginas : Inherits Accion(Of List(Of pagina))
        Public Property tipoConsulta As tipoConsultaPagina
        Public Property id As Guid
        Public Property esActivo As Boolean
        Public Property jerarquia As Integer
        Public Property idPaginaPadre As Guid
        Public Sub New()
            MyBase.New("proceso_ObtenerPaginas", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of pagina)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaPagina.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaPagina.estadoOrdenadoYAgrupado
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaPagina.nombre
                    parametros.Add(New ParametroPredicado("nombre", Nombre))
                Case tipoConsultaPagina.jerarquia
                    parametros.Add(New ParametroPredicado("jerarquia", jerarquia))
                Case tipoConsultaPagina.idPaginaPadre
                    parametros.Add(New ParametroPredicado("idPaginaPadre", idPaginaPadre))
                Case tipoConsultaPagina.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspPagina.daoPagina)().ObtenerConjunto(New Predicado("", parametros.ToArray))
        End Function
    End Class
End Namespace

