Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspTelefonoProveedor
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspTelefonoProveedor
    Public Class daoTelefonoProveedor : Inherits DaoSql(Of telefonoProveedor)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaTelefonoProveedor = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaTelefonoProveedor)
            Select Case tipoConsulta
                Case tipoConsultaTelefonoProveedor.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaTelefonoProveedor.idProveedor
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idProveedor", predicado.Parametros("idProveedor").Valor)
                'Case tipoConsultaTelefonoProveedor.esActivo
                '    comando.Parameters.AddWithValue("@Accion", 2)
                '    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                Case tipoConsultaTelefonoProveedor.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select

            comando.CommandText = "proAlm_ObtenerTelefonoProveedors"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As telefonoProveedor
            Dim telefonoProveedor As New telefonoProveedor
            telefonoProveedor.id = Guid.Parse(lectorRenglonActual("id").ToString)
            telefonoProveedor.idProveedor = Guid.Parse(lectorRenglonActual("idProveedor").ToString)
            telefonoProveedor.codigoLargaDistancia = lectorRenglonActual("codigoLargaDistancia").ToString
            telefonoProveedor.numero = lectorRenglonActual("numero").ToString
            telefonoProveedor.extension = lectorRenglonActual("extension").ToString
            telefonoProveedor.tipo = lectorRenglonActual("tipo").ToString
            telefonoProveedor.esActivo = CBool(lectorRenglonActual("esActivo").ToString)
            'telefonoProveedor.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            'telefonoProveedor.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            telefonoProveedor._nombre = lectorRenglonActual("nombre").ToString
            telefonoProveedor._ciudad = lectorRenglonActual("ciudad").ToString
            telefonoProveedor._colonia = lectorRenglonActual("colonia").ToString
            telefonoProveedor._contacto = lectorRenglonActual("contacto").ToString
            telefonoProveedor._domicilio = lectorRenglonActual("domicilio").ToString
            telefonoProveedor._estado = lectorRenglonActual("estado").ToString
            telefonoProveedor._giro = lectorRenglonActual("giro").ToString
            telefonoProveedor._rfc = lectorRenglonActual("rfc").ToString
            Return telefonoProveedor
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As telefonoProveedor)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarTelefonoProveedor"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idProveedor", entidad.idProveedor)
                comando.Parameters.AddWithValue("@codigoLargaDistancia", entidad.codigoLargaDistancia)
                comando.Parameters.AddWithValue("@numero", entidad.numero)
                comando.Parameters.AddWithValue("@extension", entidad.extension)
                comando.Parameters.AddWithValue("@tipo", entidad.tipo)
                comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As telefonoProveedor)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarTelefonoProveedor"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idProveedor", entidad.idProveedor)
                comando.Parameters.AddWithValue("@codigoLargaDistancia", entidad.codigoLargaDistancia)
                comando.Parameters.AddWithValue("@numero", entidad.numero)
                comando.Parameters.AddWithValue("@extension", entidad.extension)
                comando.Parameters.AddWithValue("@tipo", entidad.tipo)
                comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As telefonoProveedor)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarTelefonoProveedor"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
    End Class
End Namespace

