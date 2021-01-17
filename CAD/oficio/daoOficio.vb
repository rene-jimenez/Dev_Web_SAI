Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspOficio
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspOficio
    Public Class daoOficio : Inherits DaoSql(Of Oficio)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotImplementedException("Imposible continuar: No contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaOficio = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaOficio)
            Select Case tipoConsulta
                Case tipoConsultaOficio.id
                    comando.Parameters.AddWithValue("Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.idEstatusOficio
                    comando.Parameters.AddWithValue("Accion", 2)
                    comando.Parameters.AddWithValue("@idEstatusOficio", predicado.Parametros("idEstatusOficio").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.pedidoAgregar
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.pedidoModificar
                    comando.Parameters.AddWithValue("@Accion", 4)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.pedidoConsultar
                    comando.Parameters.AddWithValue("@Accion", 5)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.solicitudAgregar
                    comando.Parameters.AddWithValue("@Accion", 6)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficioGeneral"
                Case tipoConsultaOficio.editarSolicitud
                    comando.Parameters.AddWithValue("@Accion", 7)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.actualizarSolicitud
                    comando.Parameters.AddWithValue("@Accion", 8)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.consultarSolicitud
                    comando.Parameters.AddWithValue("@Accion", 9)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.afectacionAgregar
                    comando.Parameters.AddWithValue("@Accion", 10)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.afectacionEditar
                    comando.Parameters.AddWithValue("@Accion", 11)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.afectacionConsultar
                    comando.Parameters.AddWithValue("@Accion", 12)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.afectacionSustitucion
                    comando.Parameters.AddWithValue("@Accion", 13)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.porDRM
                    comando.Parameters.AddWithValue("@Accion", 14)
                    comando.Parameters.AddWithValue("@turnoDRM", predicado.Parametros("turnoDRM").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.alcanceAgregar
                    comando.Parameters.AddWithValue("@Accion", 15)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.alcanceEditar
                    comando.Parameters.AddWithValue("@Accion", 16)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.alcanceActualizar
                    comando.Parameters.AddWithValue("@Accion", 17)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.alcanceCancelar
                    comando.Parameters.AddWithValue("@Accion", 17) 'El QUERY ES EL MISMO QUE EN ACTUALZIAR
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.alcanceConsultar
                    comando.Parameters.AddWithValue("@Accion", 27)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.comprobacionAgregar
                    comando.Parameters.AddWithValue("@Accion", 20)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.comprobacionConsultar
                    comando.Parameters.AddWithValue("@Accion", 21)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.devolucionAgregar
                    comando.Parameters.AddWithValue("@Accion", 22)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.devolucionConsultar
                    comando.Parameters.AddWithValue("@Accion", 23)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.esAtendido
                    comando.Parameters.AddWithValue("@Accion", 24)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@esAtendido", predicado.Parametros("esAtendido").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.complementarOficio
                    comando.Parameters.AddWithValue("@Accion", 25)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.cancelarSolicitud
                    comando.Parameters.AddWithValue("@Accion", 26)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
                Case tipoConsultaOficio.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                    comando.CommandText = "proAlm_ObtenerOficio"
            End Select
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As oficio
            Dim oficio As New oficio
            oficio.id = Guid.Parse(lectorRenglonActual("id").ToString)
            If lectorRenglonActual("idArea").ToString <> "" Then
                oficio.idArea = Guid.Parse(lectorRenglonActual("idArea").ToString)
            End If
            If lectorRenglonActual("idCargoPresupuestal").ToString <> "" Then
                oficio.idCargoPresupuestal = Guid.Parse(lectorRenglonActual("idCargoPresupuestal").ToString)
            End If
            If lectorRenglonActual("idEstatusOficio").ToString <> "" Then
                oficio.idEstatusOficio = Guid.Parse(lectorRenglonActual("idEstatusOficio").ToString)
            End If
            If lectorRenglonActual("idResponsable").ToString <> "" Then
                oficio.idResponsable = Guid.Parse(lectorRenglonActual("idResponsable").ToString)
            End If
            If lectorRenglonActual("idRubro").ToString <> "" Then
                oficio.idRubro = Guid.Parse(lectorRenglonActual("idRubro").ToString)
            End If
            If lectorRenglonActual("idSistema").ToString <> "" Then
                oficio.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
            End If
            If lectorRenglonActual("idUsuarioMovimiento").ToString <> "" Then
                oficio.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            End If
            If lectorRenglonActual("asunto").ToString <> "" Then
                oficio.asunto = lectorRenglonActual("asunto").ToString
            End If
            If lectorRenglonActual("comentarios").ToString <> "" Then
                oficio.comentarios = lectorRenglonActual("comentarios").ToString
            End If
            If lectorRenglonActual("fechaAtendido").ToString <> "" Then
                oficio.fechaAtendido = CDate(lectorRenglonActual("fechaAtendido").ToString)
            End If
            If lectorRenglonActual("fechaCaptura").ToString <> "" Then
                oficio.fechaCaptura = CDate(lectorRenglonActual("fechaCaptura").ToString)
            End If
            If lectorRenglonActual("esAtendido").ToString <> "" Then
                oficio.esAtendido = CBool(lectorRenglonActual("esAtendido").ToString)
            End If
            If lectorRenglonActual("esDocInterno").ToString <> "" Then
                oficio.esDocInterno = CBool(lectorRenglonActual("esDocInterno").ToString)
            End If
            If lectorRenglonActual("indicaciones").ToString <> "" Then
                oficio.indicaciones = lectorRenglonActual("indicaciones").ToString
            End If
            If lectorRenglonActual("turnoDRM").ToString <> "" Then
                oficio.turnoDRM = lectorRenglonActual("turnoDRM").ToString
            End If
            If lectorRenglonActual("turnoSAF").ToString <> "" Then
                oficio.turnoSAF = lectorRenglonActual("turnoSAF").ToString
            End If
            If lectorRenglonActual("_Area").ToString <> "" Then
                oficio._area = lectorRenglonActual("_Area").ToString
            End If
            If lectorRenglonActual("_CargoPresupuestal").ToString <> "" Then
                oficio._cargoPresupuestal = lectorRenglonActual("_CargoPresupuestal").ToString
            End If
            If lectorRenglonActual("_EstatusOficio").ToString <> "" Then
                oficio._estatusOficio = lectorRenglonActual("_EstatusOficio").ToString
            End If
            If lectorRenglonActual("_Rubro").ToString <> "" Then
                oficio._rubro = lectorRenglonActual("_Rubro").ToString
            End If
            If lectorRenglonActual("_Responsable").ToString <> "" Then
                oficio._responsable = lectorRenglonActual("_Responsable").ToString
            End If
            If lectorRenglonActual("_Sistema").ToString <> "" Then
                oficio._sistema = lectorRenglonActual("_Sistema").ToString
            End If
            Return oficio
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As oficio)
            If (Not IsNothing(entidad)) Then
                Select Case entidad._tipoGuardar
                    Case tipoGuardarOficio.Normal
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_AgregarOficio"
                        comando.Parameters.AddWithValue("@idArea", entidad.idArea)
                        comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                        comando.Parameters.AddWithValue("@asunto", entidad.asunto)
                        comando.Parameters.AddWithValue("@turnoDRM", entidad.turnoDRM)                'comando.Parameters.AddWithValue("@idEstatusOficio", entidad.idEstatusOficio)
                        comando.Parameters.AddWithValue("@turnoSAF", entidad.turnoSAF)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                    Case tipoGuardarOficio.Especial
                        comando.CommandType = CommandType.StoredProcedure
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.CommandText = "proAlm_AgregarOficioEspecial"
                        comando.Parameters.AddWithValue("@idArea", entidad.idArea)
                        comando.Parameters.AddWithValue("@idCargoPresupuestal", entidad.idCargoPresupuestal)
                        comando.Parameters.AddWithValue("@idEstatusOficio", entidad.idEstatusOficio)
                        comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                        comando.Parameters.AddWithValue("@turnoDRM", entidad.turnoDRM)                'comando.Parameters.AddWithValue("@idEstatusOficio", entidad.idEstatusOficio)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                End Select
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As oficio)
            If (Not IsNothing(entidad)) Then
                Select Case entidad._tipoEdicion
                    Case tipoEdicionOficio.complementar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_ComplementarOficio"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@idResponsable", entidad.idResponsable)
                        comando.Parameters.AddWithValue("@idRubro", entidad.idRubro)
                        comando.Parameters.AddWithValue("@indicaciones", entidad.indicaciones)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                    Case tipoEdicionOficio.especial_Pedido_Solicitud
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_EditarOficioEspecial_P_S"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@idArea", entidad.idArea)
                        comando.Parameters.AddWithValue("@idCargoPresupuestal", entidad.idCargoPresupuestal)
                        comando.Parameters.AddWithValue("@idEstatusOficio", entidad.idEstatusOficio)
                        comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                    Case tipoEdicionOficio.editar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_EditarOficio"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@idArea", entidad.idArea)
                        comando.Parameters.AddWithValue("@idCargoPresupuestal", entidad.idCargoPresupuestal)
                        comando.Parameters.AddWithValue("@idResponsable", entidad.idResponsable)
                        comando.Parameters.AddWithValue("@idEstatusOficio", entidad.idEstatusOficio)
                        comando.Parameters.AddWithValue("@idRubro", entidad.idRubro)
                        comando.Parameters.AddWithValue("@asunto", entidad.asunto)
                        comando.Parameters.AddWithValue("@comentarios", entidad.comentarios)
                        comando.Parameters.AddWithValue("@fechaAtendido", entidad.fechaAtendido)
                        comando.Parameters.AddWithValue("@indicaciones", entidad.indicaciones)
                        comando.Parameters.AddWithValue("@esAtendido", entidad.esAtendido)
                        comando.Parameters.AddWithValue("@turnoDRM", entidad.turnoDRM)
                        comando.Parameters.AddWithValue("@turnoSAF", entidad.turnoSAF)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                End Select
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace

