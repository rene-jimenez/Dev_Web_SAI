Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspDetallePedido
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspDetallePedido
    Public Class daoDetallePedido : Inherits DaoSql(Of detallePedido)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotImplementedException()
            End If
            Dim tipoConsulta As tipoConsultaDetallePedido = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaDetallePedido)
            Select Case tipoConsulta
                Case tipoConsultaDetallePedido.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaDetallePedido.idPedido
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@idPedido", predicado.Parametros("idPedido").Valor)
                Case tipoConsultaDetallePedido.xPedidoArticulo
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@idPedido", predicado.Parametros("idPedido").Valor)
                    comando.Parameters.AddWithValue("@idArticulo", predicado.Parametros("idArticulo").Valor)
                Case tipoConsultaDetallePedido.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
                    comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            End Select
            comando.CommandText = "proAlm_ObtenerDetallePedido"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As detallePedido
            Dim detallePedido As New detallePedido
            detallePedido.id = Guid.Parse(lectorRenglonActual("id").ToString)
            detallePedido.idArticulo = Guid.Parse(lectorRenglonActual("idArticulo").ToString)
            detallePedido.idPedido = Guid.Parse(lectorRenglonActual("idPedido").ToString)
            detallePedido.precioUnitario = lectorRenglonActual("precioUnitario")
            detallePedido.cantidad = lectorRenglonActual("cantidad")
            detallePedido._articulo = lectorRenglonActual("_articulo")
            detallePedido._numeroPedido = Decimal.Parse(lectorRenglonActual("_numeroPedido").ToString)
            detallePedido._total = Decimal.Parse(lectorRenglonActual("_total").ToString)
            detallePedido._unidadMedidaArticulo = lectorRenglonActual("_unidadMedidaArticulo")
            Return detallePedido
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As detallePedido)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarDetallePedido"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idArticulo", entidad.idArticulo)
                comando.Parameters.AddWithValue("@idPedido", entidad.idPedido)
                comando.Parameters.AddWithValue("@precioUnitario", entidad.precioUnitario)
                comando.Parameters.AddWithValue("@cantidad", entidad.cantidad)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As detallePedido)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarDetallePedido"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idArticulo", entidad.idArticulo)
                comando.Parameters.AddWithValue("@idPedido", entidad.idPedido)
                comando.Parameters.AddWithValue("@precioUnitario", entidad.precioUnitario)
                comando.Parameters.AddWithValue("@cantidad", entidad.cantidad)
                comando.Parameters.AddWithValue("@idSistema", entidad.idSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As detallePedido)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarDetallePedido"
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

