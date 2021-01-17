Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspRubroRequerimiento
Namespace nspRubroRequerimiento
    Public Class Proceso_ObtenerRubroRequerimientos : Inherits Accion(Of List(Of rubroRequerimiento))
        Public Property tipoConsulta As tipoConsultaRubroRequerimiento
        Public Property id As Guid
        Public Property esActivo As Boolean?
        Public Property esCotizable As Boolean?
        Public Sub New()
            MyBase.New("Proceso_ObtenerRubroRequerimientos", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of rubroRequerimiento)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaRubroRequerimiento.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaRubroRequerimiento.esCotizable
                    parametros.Add(New ParametroPredicado("esCotizable", esCotizable))
                Case tipoConsultaRubroRequerimiento.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaRubroRequerimiento.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspRubroRequerimiento.daoRubroRequerimiento)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace

