Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspArea
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspArea
    Public Class daoArea : Inherits DaoSql(Of area)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaArea = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaArea)
            Select Case tipoConsulta
                Case tipoConsultaArea.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)

                Case tipoConsultaArea.idAreaPadre
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idAreaPadre", predicado.Parametros("idAreaPadre").Valor)

                Case tipoConsultaArea.tipo
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@tipo", predicado.Parametros("tipo").Valor)

                Case tipoConsultaArea.jerarquia
                    comando.Parameters.AddWithValue("@Accion", 4)
                    comando.Parameters.AddWithValue("@jerarquia", predicado.Parametros("jerarquia").Valor)

                Case tipoConsultaArea.codigo
                    comando.Parameters.AddWithValue("@Accion", 5)
                    comando.Parameters.AddWithValue("@codigo", predicado.Parametros("codigo").Valor)

                Case tipoConsultaArea.esActivo
                    comando.Parameters.AddWithValue("@Accion", 6)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)

                Case tipoConsultaArea.jerarquiaEsActivo
                    comando.Parameters.AddWithValue("@Accion", 7)
                    comando.Parameters.AddWithValue("@jerarquia", predicado.Parametros("jerarquia").Valor)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                Case tipoConsultaArea.idAreaPadreEsActivo
                    comando.Parameters.AddWithValue("@Accion", 8)
                    'comando.Parameters.AddWithValue("@idAreaPadre", predicado.Parametros("idAreaPadre").Valor)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                Case tipoConsultaArea.esAreaPadre
                    comando.Parameters.AddWithValue("@Accion", 9)
                Case tipoConsultaArea.esAreaHija
                    comando.Parameters.AddWithValue("@Accion", 10)
                Case tipoConsultaArea.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select

            comando.CommandText = "proAlm_ObtenerArea"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As area
            Dim area As New area
            area.id = Guid.Parse(lectorRenglonActual("id").ToString)
            If lectorRenglonActual("nombre").ToString <> "" Then
                area.nombre = lectorRenglonActual("nombre").ToString
            End If
            If lectorRenglonActual("esActivo").ToString <> "" Then
                area.esActivo = CBool(lectorRenglonActual("esActivo").ToString)
            End If
            If lectorRenglonActual("codigo").ToString <> "" Then
                area.codigo = lectorRenglonActual("codigo").ToString
            End If
            If lectorRenglonActual("idAreaPadre").ToString <> "" Then
                area.idAreaPadre = Guid.Parse(lectorRenglonActual("idAreaPadre").ToString)
            End If
            If lectorRenglonActual("tipo").ToString <> "" Then
                area.tipo = lectorRenglonActual("tipo").ToString
            End If
            If lectorRenglonActual("jerarquia").ToString <> "" Then
                area.jerarquia = lectorRenglonActual("jerarquia").ToString
            End If
            area.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            area.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            If lectorRenglonActual("idSistema").ToString <> "" Then
                area.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
            End If
            Return area
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As area)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarArea"
                comando.Parameters.AddWithValue("@id", entidad.id)
                If Not entidad.idAreaPadre Is Nothing Then
                    comando.Parameters.AddWithValue("@idAreaPadre", entidad.idAreaPadre)
                End If
                comando.Parameters.AddWithValue("@codigo", entidad.codigo)
                If Not entidad.jerarquia Is Nothing Then
                    comando.Parameters.AddWithValue("@jerarquia", entidad.jerarquia)
                End If
                comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                comando.Parameters.AddWithValue("@tipo", entidad.tipo)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)

            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As area)
            If (Not IsNothing(entidad)) Then
                Select Case entidad._tipoEdicion
                    Case tipoEdicionArea.cambiarEstado
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_CambiarEstadoArea"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                    Case tipoEdicionArea.editar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_EditarArea"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        If entidad.idAreaPadre.ToString <> "00000000-0000-0000-0000-000000000000" Then
                            comando.Parameters.AddWithValue("@idAreaPadre", entidad.idAreaPadre)
                        Else
                            comando.Parameters.AddWithValue("@idAreaPadre", "00000000-0000-0000-0000-000000000000")
                        End If
                        comando.Parameters.AddWithValue("@codigo", entidad.codigo)
                        comando.Parameters.AddWithValue("@jerarquia", entidad.jerarquia)
                        comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                        comando.Parameters.AddWithValue("@tipo", entidad.tipo)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                        comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                End Select

            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As area)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarArea"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace

