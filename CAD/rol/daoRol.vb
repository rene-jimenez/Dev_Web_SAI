Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspRol
Namespace nspRol
    Public Class daoRol : Inherits DaoSql(Of rol)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Contexto.Entidades.Persistencia.Relacional.Daos.Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaRol = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaRol)
            Select Case tipoConsulta
                Case tipoConsultaRol.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaRol.rol
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@rol", predicado.Parametros("rol").Valor)
                Case tipoConsultaRol.estado
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                Case tipoConsultaRol.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select

            comando.CommandText = "proAlm_ObtenerRol"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As rol
            Return New rol() With {.id = Guid.Parse(lectorRenglonActual("id").ToString),
                                    .rol = lectorRenglonActual("nombre"),
                                     .esActivo = Boolean.Parse(lectorRenglonActual("esActivo").ToString)}
        End Function

        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As rol)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarRol"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@nombreRol", entidad.rol)
                comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@ipPcUsuario", entidad.ipPcUsuario)

            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As rol)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarRol"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@nombreRol", entidad.rol)
                comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@ipPcUsuario", entidad.ipPcUsuario)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As rol)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarRol"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@ipPcUsuario", entidad.ipPcUsuario)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace