Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspEntrada
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspEntrada
    Public Class daoEntrada : Inherits DaoSql(Of entrada)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parametro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaEntrada = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaEntrada)
            Select Case tipoConsulta
                Case tipoConsultaEntrada.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaEntrada.idPedido
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idPedido", predicado.Parametros("idPedido").Valor)
                    'comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaEntrada.numEntrada
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@numEntrada", predicado.Parametros("numEntrada").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaEntrada.rangoFecha
                    comando.Parameters.AddWithValue("@Accion", 4)
                    comando.Parameters.AddWithValue("@fechaInicial", predicado.Parametros("fechaInicial").Valor)
                    comando.Parameters.AddWithValue("@fechaFinal", predicado.Parametros("fechaFinal").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaEntrada.tipo
                    comando.Parameters.AddWithValue("@Accion", 5)
                    comando.Parameters.AddWithValue("@tipo", predicado.Parametros("tipo").Valor)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
                Case tipoConsultaEntrada.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            End Select
            comando.CommandText = "proAlm_ObtenerEntradaAlmacen"
            comando.CommandType = CommandType.StoredProcedure

        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As entrada
            Dim entrada As New entrada
            entrada.id = Guid.Parse(lectorRenglonActual("id").ToString)
            If lectorRenglonActual("idPedido").ToString <> "" Then
                entrada.idPedido = Guid.Parse(lectorRenglonActual("idPedido").ToString)
            End If
            If lectorRenglonActual("numEntrada").ToString <> "" Then
                entrada.numEntrada = lectorRenglonActual("numEntrada").ToString
            End If
            If lectorRenglonActual("tipo").ToString <> "" Then
                entrada.tipo = CBool(lectorRenglonActual("tipo").ToString)
            End If
            If lectorRenglonActual("fechaEntrada").ToString <> "" Then
                entrada.fechaEntrada = lectorRenglonActual("fechaEntrada").ToString
            End If
            If lectorRenglonActual("esNota").ToString <> "" Then
                entrada.esNota = CBool(lectorRenglonActual("esNota").ToString)
            End If
            If lectorRenglonActual("numRemision").ToString <> "" Then
                entrada.numRemision = lectorRenglonActual("numRemision").ToString
            End If
            If lectorRenglonActual("comentario").ToString <> "" Then
                entrada.comentario = lectorRenglonActual("comentario").ToString
            End If
            If lectorRenglonActual("numeroPedido").ToString <> "" Then
                entrada._numeroPedido = lectorRenglonActual("numeroPedido").ToString
            End If
            If lectorRenglonActual("nombre").ToString <> "" Then
                entrada._nombreProveedor = lectorRenglonActual("nombre").ToString
            End If
            If lectorRenglonActual("fechaRecibido").ToString <> "" Then
                entrada._fechaPedidoRecibido = lectorRenglonActual("fechaRecibido").ToString
            End If
            If lectorRenglonActual("turnoDRM").ToString <> "" Then
                entrada._turnoDRM = lectorRenglonActual("turnoDRM").ToString
            End If
            If lectorRenglonActual("tipoPago").ToString <> "" Then
                entrada._tipoPago = lectorRenglonActual("tipoPago").ToString
            End If

            entrada.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
            entrada.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            entrada.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            Return entrada
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As entrada)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarEntradaAlmacen"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idPedido", entidad.idPedido)
                comando.Parameters.AddWithValue("@numEntrada", entidad.numEntrada)
                comando.Parameters.AddWithValue("@tipo", entidad.tipo)
                comando.Parameters.AddWithValue("@fechaEntrada", entidad.fechaEntrada)
                comando.Parameters.AddWithValue("@esNota", entidad.esNota)
                comando.Parameters.AddWithValue("@numRemision", entidad.numRemision)
                comando.Parameters.AddWithValue("@comentario", entidad.comentario)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As entrada)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarEntradaAlmacen"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@tipo", entidad.tipo)
                comando.Parameters.AddWithValue("@esNota", entidad.esNota)
                comando.Parameters.AddWithValue("@numRemision", entidad.numRemision)
                comando.Parameters.AddWithValue("@comentario", entidad.comentario)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As entrada)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarEntradaAlmacen"
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

