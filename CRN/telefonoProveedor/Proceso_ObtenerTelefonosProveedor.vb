Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspTelefonoProveedor
Namespace nspTelefonoProveedor
    Public Class Proceso_ObtenerTelefonosProveedor : Inherits Accion(Of List(Of telefonoProveedor))
        Public Property tipoConsulta As tipoConsultaTelefonoProveedor
        Public Property id As Guid
        Public Property idProveedor As Guid
        Public Property esActivo As Boolean?
        Public Sub New()
            MyBase.New("Proceso_ObtenerTelefonosProveedor", "Obtiene lista ordenada")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of telefonoProveedor)
            Dim parametros As New List(Of ParametroPredicado)
            parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
            Select Case tipoConsulta
                Case tipoConsultaTelefonoProveedor.id
                    parametros.Add(New ParametroPredicado("id", id))
                Case tipoConsultaTelefonoProveedor.idProveedor
                    parametros.Add(New ParametroPredicado("idProveedor", idProveedor))
                'Case tipoConsultaTelefonoProveedor.esActivo
                '    parametros.Add(New ParametroPredicado("esActivo", esActivo))
                Case tipoConsultaTelefonoProveedor.todos
            End Select
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspTelefonoProveedor.daoTelefonoProveedor)().ObtenerConjunto(New Predicado("", parametros.ToArray()))

        End Function
    End Class
End Namespace

