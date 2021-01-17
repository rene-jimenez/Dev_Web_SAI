Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspPermiso
Imports System.Transactions
Namespace nspPermiso
    Public Class Proceso_AgregarPermiso : Inherits Accion(Of respuestaDelProceso)
        Public Property listaEntidad As List(Of CES.nspPermiso.permiso)
        Public Sub New()
            MyBase.New("Proceso_AgregarPermiso", "Agrega un registro a la base de datos")
        End Sub
        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Try
                Using scope As New TransactionScope

                    Dim daoEliminar = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspPermiso.daoPermiso)()
                    daoEliminar.Eliminar(listaEntidad(0))

                    Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspPermiso.daoPermiso)()
                    For I = 0 To listaEntidad.Count - 1
                        dao.Agregar(listaEntidad(I))
                    Next
                    respuesta.comentario = "ok"
                    respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    scope.Complete()
                End Using
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    respuesta.comentario = "No puede duplicar la información en la base de datos."
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                Else
                    respuesta.comentario = ex.Message.ToString
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            Catch ex As Exception
                respuesta.comentario = "Ocurrió un error al agregar un registro en la base de datos"
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function

    End Class
End Namespace

