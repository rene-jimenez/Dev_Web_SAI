Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspPermiso
Namespace nspPermiso
    Public Class daoPermiso : Inherits DaoSql(Of permiso)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Contexto.Entidades.Persistencia.Relacional.Daos.Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultapermiso = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultapermiso)
            Select Case tipoConsulta
                Case tipoConsultapermiso.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultapermiso.esActivo
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                Case tipoConsultapermiso.idRol
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@idRol", predicado.Parametros("idRol").Valor)
                Case tipoConsultapermiso.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select
            comando.CommandText = "proAlm_ObtenerPermiso"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As permiso
            Return New permiso() With {.id = Guid.Parse(lectorRenglonActual("id").ToString),
                .idPagina = Guid.Parse(lectorRenglonActual("idPagina").ToString),
                .idRol = Guid.Parse(lectorRenglonActual("idRol").ToString),
            .esActivo = Boolean.Parse(lectorRenglonActual("esActivo").ToString),
            .nombrePagina = lectorRenglonActual("nombrePagina")}
        End Function

        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As permiso)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarPermiso"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                comando.Parameters.AddWithValue("@idPagina", entidad.idPagina)
                comando.Parameters.AddWithValue("@idRol", entidad.idRol)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@ipPcUsuario", entidad.ipPcUsuario)
                ' Si hay mas campos escribelos aquí
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As permiso)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarPermiso"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                comando.Parameters.AddWithValue("@idPagina", entidad.idPagina)
                comando.Parameters.AddWithValue("@idRol", entidad.idRol)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@ipPcUsuario", entidad.ipPcUsuario)
                ' Si hay mas campos escribelos aquí
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As permiso)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarPermisoRol"
                comando.Parameters.AddWithValue("@idRol", entidad.idRol)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@ipPcUsuario", entidad.ipPcUsuario)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace
