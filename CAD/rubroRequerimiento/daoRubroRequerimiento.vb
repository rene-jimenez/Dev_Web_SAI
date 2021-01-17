Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspRubroRequerimiento
Imports Contexto.Entidades.Persistencia.Relacional.Daos


Namespace nspRubroRequerimiento
    Public Class daoRubroRequerimiento : Inherits DaoSql(Of rubroRequerimiento)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotImplementedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaRubroRequerimiento = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaRubroRequerimiento)
            Select Case tipoConsulta
                Case tipoConsultaRubroRequerimiento.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaRubroRequerimiento.esCotizable
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@esCotizable", predicado.Parametros("esCotizable").Valor)
                Case tipoConsultaRubroRequerimiento.esActivo
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                Case tipoConsultaRubroRequerimiento.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select
            comando.CommandText = "proAlm_ObtenerRubroRequerimiento"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As rubroRequerimiento
            Dim rubroRequerimiento As New rubroRequerimiento
            rubroRequerimiento.id = Guid.Parse(lectorRenglonActual("id").ToString)
            rubroRequerimiento.nombre = lectorRenglonActual("nombre").ToString
            rubroRequerimiento.esActivo = CBool(lectorRenglonActual("esActivo").ToString)
            rubroRequerimiento.esCotizable = CBool(lectorRenglonActual("esCotizable").ToString)
            rubroRequerimiento.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            rubroRequerimiento.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            Return rubroRequerimiento
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As rubroRequerimiento)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarRubroRequerimiento"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                comando.Parameters.AddWithValue("@esCotizable", entidad.esCotizable)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidadad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As rubroRequerimiento)
            If (Not IsNothing(entidad)) Then
                Select Case entidad._tipoEdicion
                    Case tipoEdicionRubroRequerimiento.cambiarEstado
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_CambiarEstadoRequerimiento"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                    Case tipoEdicionRubroRequerimiento.editar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_EditarRequerimiento"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                        comando.Parameters.AddWithValue("@esCotizable", entidad.esCotizable)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                End Select
            Else
                Throw New NotSupportedException("Imposble continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As rubroRequerimiento)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarRubroRequerimiento"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace

