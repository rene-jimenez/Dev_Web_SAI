Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspArea
Namespace nspArea
    Public Class Proceso_ObtenerAreas : Inherits Accion(Of List(Of area))
        Public Property tipoConsulta As tipoConsultaArea
        Public Property id As Guid
        Public Property idAreaPadre As Guid
        Public Property tipo As String
        Public Property jerarquia As Integer
        Public Property codigo As String
        Public Property esActivo As Boolean?


        Public Sub New()
            MyBase.New("Proceso_ObtenerAreas", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of area)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaArea.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaArea.idAreaPadre
                    parametros.Add(New ParametroPredicado("idAreaPadre", idAreaPadre))
                Case tipoConsultaArea.tipo
                    parametros.Add(New ParametroPredicado("tipo", tipo))
                Case tipoConsultaArea.jerarquia
                    parametros.Add(New ParametroPredicado("jerarquia", jerarquia))
                Case tipoConsultaArea.codigo
                    parametros.Add(New ParametroPredicado("codigo", codigo))
                Case tipoConsultaArea.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaArea.jerarquiaEsActivo
                    parametros.Add(New ParametroPredicado("jerarquia", jerarquia))
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaArea.idAreaPadreEsActivo
                    'parametros.Add(New ParametroPredicado("idAreaPadre", idAreaPadre))
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaArea.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspArea.daoArea)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace

