Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspRol
Namespace nspRol
    Public Class Proceso_ObtenerListaRol : Inherits Accion(Of List(Of rol))
        Public Property tipoConsulta As tipoConsultaRol
        Public Property id As Guid
        Public Property esActivo As Boolean?

        Public Sub New()
            MyBase.New("Proceso_ObtenerListaRol", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of rol)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaRol.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaRol.esActivo
                    parametros.Add(New ParametroPredicado("esActivo", esActivo))
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspRol.daoRol)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class

End Namespace
