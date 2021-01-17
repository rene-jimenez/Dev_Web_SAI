Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports CES.nspOficio
Namespace nspOficio
    Public Class Proceso_ObtenerListaOficio : Inherits Accion(Of List(Of oficio))
        Public Property tipoConsulta As tipoConsultaOficio
        Public Property id As Guid?
        Public Property fechaInicial As Date
        Public Property fechaFinal As Date
        Public Property turnoDRM As String
        Public Property esAtendido As Boolean
        Public Property idEstatusOficio As Guid?
        Public Property idSistema As Guid?

        Public Sub New()
            MyBase.New("Proceso_ObtenerListaEntidad", "Obtiene el listado de registros")
        End Sub

        Protected Overrides Function OnEjecutar() As List(Of oficio)
            Try
                Dim parametros As New List(Of ParametroPredicado)
                parametros.Add(New ParametroPredicado("tipoConsulta", tipoConsulta))
                Select Case tipoConsulta
                    Case tipoConsultaOficio.id
                        If id Is Nothing Then
                            Throw New Exception("El id es campo obligatorio para este tipo de consulta")
                        Else
                            parametros.Add(New ParametroPredicado("id", id))
                        End If
                    Case tipoConsultaOficio.idEstatusOficio
                        If idEstatusOficio Is Nothing Then
                            Throw New Exception("El idEstatusOficio es campo obligatorio para este tipo de consulta")
                        Else
                            parametros.Add(New ParametroPredicado("idEstatusOficio", idEstatusOficio))
                            parametros.Add(New ParametroPredicado("idSistema", idSistema))
                        End If
                    Case tipoConsultaOficio.pedidoAgregar
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.pedidoModificar
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.pedidoConsultar
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.solicitudAgregar
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.editarSolicitud
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.actualizarSolicitud
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.consultarSolicitud
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.afectacionAgregar
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.afectacionEditar
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.afectacionConsultar
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.afectacionSustitucion
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.porDRM
                        parametros.Add(New ParametroPredicado("turnoDRM", turnoDRM))
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.esAtendido
                        parametros.Add(New ParametroPredicado("fechaInicial", fechaInicial))
                        parametros.Add(New ParametroPredicado("fechaFinal", fechaFinal))
                        parametros.Add(New ParametroPredicado("esAtendido", esAtendido))
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case tipoConsultaOficio.cancelarSolicitud
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                    Case Else
                        parametros.Add(New ParametroPredicado("idSistema", idSistema))
                End Select
                Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspOficio.daoOficio)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
            Catch ex As Exception
                Throw New Exception(ex.Message.ToString)
            End Try
        End Function
    End Class




End Namespace

