Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspProveedor
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspProveedor
    Public Class daoProveedor : Inherits DaoSql(Of proveedor)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotImplementedException("Imposible continuar: No contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaProveedor = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaProveedor)
            Select Case tipoConsulta
                Case tipoConsultaProveedor.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaProveedor.esActivo
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                Case tipoConsultaProveedor.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select
            comando.CommandText = "proAlm_ObtenerProveedor"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As proveedor
            Dim proveedor As New proveedor
            proveedor.id = Guid.Parse(lectorRenglonActual("id").ToString)
            If lectorRenglonActual("nombre").ToString <> "" Then
                proveedor.nombre = lectorRenglonActual("nombre").ToString
            End If
            If lectorRenglonActual("esActivo").ToString <> "" Then
                proveedor.esActivo = CBool(lectorRenglonActual("esActivo").ToString)
            End If
            If lectorRenglonActual("ciudad").ToString <> "" Then
                proveedor.ciudad = lectorRenglonActual("ciudad").ToString
            End If
            If lectorRenglonActual("codigoPostal").ToString <> "" Then
                proveedor.codigoPostal = lectorRenglonActual("codigoPostal").ToString
            End If
            If lectorRenglonActual("colonia").ToString <> "" Then
                proveedor.colonia = lectorRenglonActual("colonia").ToString
            End If
            If lectorRenglonActual("contacto").ToString <> "" Then
                proveedor.contacto = lectorRenglonActual("contacto").ToString
            End If
            If lectorRenglonActual("domicilioFiscal").ToString <> "" Then
                proveedor.domicilioFiscal = lectorRenglonActual("domicilioFiscal").ToString
            End If
            If lectorRenglonActual("domicilio").ToString <> "" Then
                proveedor.domicilio = lectorRenglonActual("domicilio").ToString
            End If
            If lectorRenglonActual("estado").ToString <> "" Then
                proveedor.estado = lectorRenglonActual("estado").ToString
            End If
            If lectorRenglonActual("giro").ToString <> "" Then
                proveedor.giro = lectorRenglonActual("giro").ToString
            End If
            If lectorRenglonActual("rfc").ToString <> "" Then
                proveedor.rfc = lectorRenglonActual("rfc").ToString
            End If
            proveedor.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            proveedor.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            Return proveedor
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As proveedor)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarProveedor"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                If Not entidad.ciudad Is Nothing Then
                    comando.Parameters.AddWithValue("@ciudad", entidad.ciudad)
                End If
                If Not entidad.codigoPostal Is Nothing Then
                    comando.Parameters.AddWithValue("@codigoPostal", entidad.codigoPostal)
                End If
                If Not entidad.colonia Is Nothing Then
                    comando.Parameters.AddWithValue("@colonia", entidad.colonia)
                End If
                If Not entidad.contacto Is Nothing Then
                    comando.Parameters.AddWithValue("@contacto", entidad.contacto)
                End If
                If Not entidad.domicilioFiscal Is Nothing Then
                    comando.Parameters.AddWithValue("@domicilioFiscal", entidad.domicilioFiscal)
                End If
                If Not entidad.domicilio Is Nothing Then
                    comando.Parameters.AddWithValue("@domicilio", entidad.domicilio)
                End If
                If Not entidad.estado Is Nothing Then
                    comando.Parameters.AddWithValue("@estado", entidad.estado)
                End If
                If Not entidad.giro Is Nothing Then
                    comando.Parameters.AddWithValue("@giro", entidad.giro)
                End If
                If Not entidad.rfc Is Nothing Then
                    comando.Parameters.AddWithValue("@rfc", entidad.rfc)
                End If
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As proveedor)
            If (Not IsNothing(entidad)) Then
                Select Case entidad._tipoEdicion
                    Case tipoEdicionProveedor.cambiarEstado
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_CambiarEstadoProveedor"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                    Case tipoEdicionProveedor.editar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_EditarProveedor"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                        If Not entidad.ciudad Is Nothing Then
                            comando.Parameters.AddWithValue("@ciudad", entidad.ciudad)
                        End If
                        If Not entidad.codigoPostal Is Nothing Then
                            comando.Parameters.AddWithValue("@codigoPostal", entidad.codigoPostal)
                        End If
                        If Not entidad.colonia Is Nothing Then
                            comando.Parameters.AddWithValue("@colonia", entidad.colonia)
                        End If
                        If Not entidad.contacto Is Nothing Then
                            comando.Parameters.AddWithValue("@contacto", entidad.contacto)
                        End If
                        If Not entidad.domicilioFiscal Is Nothing Then
                            comando.Parameters.AddWithValue("@domicilioFiscal", entidad.domicilioFiscal)
                        End If
                        If Not entidad.domicilio Is Nothing Then
                            comando.Parameters.AddWithValue("@domicilio", entidad.domicilio)
                        End If
                        If Not entidad.estado Is Nothing Then
                            comando.Parameters.AddWithValue("@estado", entidad.estado)
                        End If
                        If Not entidad.giro Is Nothing Then
                            comando.Parameters.AddWithValue("@giro", entidad.giro)
                        End If
                        If Not entidad.rfc Is Nothing Then
                            comando.Parameters.AddWithValue("@rfc", entidad.rfc)
                        End If

                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                End Select
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As proveedor)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarProveedor"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace

