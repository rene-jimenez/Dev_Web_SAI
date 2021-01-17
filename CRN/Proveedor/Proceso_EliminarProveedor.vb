﻿Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports CES.nspProveedor
Imports System.Transactions
Namespace nspProveedor
    Public Class Proceso_EliminarProveedor : Inherits Accion(Of respuestaDelProceso)
        Public Property id As Guid
        Public Property idUsuarioMovimiento As Guid
        Public Property ipUsuario As String
        Public Property idSistema As Guid
        Public Sub New()
            MyBase.New("Proceso_EliminarProveedor", "Elimina un registro en la base de datos")
        End Sub

        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Dim entidad = New Proceso_ObtenerProveedor() With {.id = id}.Ejecutar()
            Try
                Using scope As New TransactionScope()
                    entidad.ipUsuario = ipUsuario
                    entidad.idUsuarioMovimiento = idUsuarioMovimiento
                    entidad.idSistema = idSistema
                    Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspProveedor.daoProveedor)()
                    dao.Eliminar(entidad)
                    respuesta.comentario = "La acción se realizó con éxito.."
                    respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    scope.Complete()
                End Using
            Catch exSQL As SqlException
                If exSQL.Number = 547 Then ' Cuando un registro ya fue ocupado, las siguientes lineas desactivan la entidad, solo cuando aplique.
                    Try
                        Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspProveedor.daoProveedor)()
                        entidad._tipoEdicion = tipoEdicionProveedor.cambiarEstado
                        entidad.esActivo = False
                        dao.Actualizar(entidad)
                        respuesta.comentario = "La acción se realizó con éxito.."
                        respuesta.respuesta = tipoRespuestaDelProceso.Completado
                    Catch ex2 As Exception
                        respuesta.comentario = "Ocurrió un error al eliminar el registro seleccionado."
                        respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                    End Try
                Else
                    respuesta.comentario = exSQL.Message.ToString
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            Catch ex As Exception
                respuesta.comentario = "Ocurrió un error al eliminar el registro seleccionado."
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function
    End Class
End Namespace

