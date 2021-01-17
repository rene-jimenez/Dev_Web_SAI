Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports Contexto.Notificaciones.controladorMensajes
Imports System.Data.SqlClient
Imports System.Transactions
Imports CES.nspProveedor
Namespace nspProveedor
    Public Class Proceso_AgregarProveedor : Inherits Accion(Of respuestaDelProceso)
        Public Property listaTelefono As New List(Of CES.nspTelefonoProveedor.telefonoProveedor)
        Public Property entidad As proveedor
        Dim controladorMensajes As New notificacionesDeUsuario
        Public Sub New()
            MyBase.New("Proceso_AgregarProveedor", "Agrega un registro a la base de datos")
        End Sub

        Protected Overrides Function OnEjecutar() As respuestaDelProceso
            Dim respuesta As respuestaDelProceso = New respuestaDelProceso(tipoRespuestaDelProceso.Completado)
            Dim entidadRepetida As String = ""
            Try
                Using scope As New TransactionScope()
                    Dim dao = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspProveedor.daoProveedor)()
                    entidadRepetida = "proveedor"
                    dao.Agregar(entidad)
                    entidadRepetida = "teléfono proveedor"
                    For i = 0 To listaTelefono.Count - 1
                        Dim daoTelefono = New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspTelefonoProveedor.daoTelefonoProveedor)() ' Aqui va telefono proveedor
                        listaTelefono(i).idProveedor = entidad.id
                        daoTelefono.Agregar(listaTelefono(i))
                    Next
                    scope.Complete()
                    respuesta.comentario = "ok"
                    respuesta.respuesta = tipoRespuestaDelProceso.Completado
                End Using
            Catch ex As SqlException
                If ex.Number = 2601 Then
                    respuesta.comentario = "El " & entidadRepetida & " está duplicado."
                    respuesta.comentario = controladorMensajes.obtenerMensaje(tipoNotificacionesDeUsuario._El_registro_estaria_duplicado, "proveedor")
                    respuesta.respuesta = tipoRespuestaDelProceso.Advertencia
                Else
                    respuesta.comentario = ex.Message.ToString
                    respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
                End If
            Catch ex As Exception
                respuesta.comentario = ex.Message.ToString
                respuesta.respuesta = tipoRespuestaDelProceso.NoCompletado
            End Try
            Return respuesta
        End Function
    End Class
End Namespace

