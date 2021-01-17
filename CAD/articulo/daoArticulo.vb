Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspArticulo
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspArticulo
    Public Class daoArticulo : Inherits DaoSql(Of articulo)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub
        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotImplementedException()
            End If
            Dim tipoConsulta As tipoConsultaArticulo = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaArticulo)
            Select Case tipoConsulta
                Case tipoConsultaArticulo.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaArticulo.esActivo
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                    comando.Parameters.AddWithValue("@tipoSistema", predicado.Parametros("tipoSistema").Valor)
                Case tipoConsultaArticulo.nombre_x_categoria
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@nombre", predicado.Parametros("nombre").Valor)
                    comando.Parameters.AddWithValue("@idCategoria", predicado.Parametros("idCategoria").Valor)
                    comando.Parameters.AddWithValue("@tipoSistema", predicado.Parametros("tipoSistema").Valor)
                Case tipoConsultaArticulo.entraAlmacen
                    comando.Parameters.AddWithValue("@Accion", 4)
                    comando.Parameters.AddWithValue("@entraAlmacen", predicado.Parametros("entraAlmacen").Valor)
                    comando.Parameters.AddWithValue("@tipoSistema", predicado.Parametros("tipoSistema").Valor)
                Case tipoConsultaArticulo.xExistencia
                    comando.Parameters.AddWithValue("@Accion", 5)
                    comando.Parameters.AddWithValue("@existencia", predicado.Parametros("existencia").Valor)
                    comando.Parameters.AddWithValue("@tipoSistema", predicado.Parametros("tipoSistema").Valor)
                Case tipoConsultaArticulo.codigoBarras
                    comando.Parameters.AddWithValue("@Accion", 6)
                    comando.Parameters.AddWithValue("@codigoBarras", predicado.Parametros("codigoBarras").Valor)
                    comando.Parameters.AddWithValue("@tipoSistema", predicado.Parametros("tipoSistema").Valor)
                Case tipoConsultaArticulo.reporteXCategoria
                    comando.Parameters.AddWithValue("@Accion", 7)
                    comando.Parameters.AddWithValue("@idCategoria", predicado.Parametros("idCategoria").Valor)
                    comando.Parameters.AddWithValue("@tipoSistema", predicado.Parametros("tipoSistema").Valor)
                Case tipoConsultaArticulo.nombre
                    comando.Parameters.AddWithValue("@Accion", 8)
                    comando.Parameters.AddWithValue("@nombre", predicado.Parametros("nombre").Valor)
                    comando.Parameters.AddWithValue("@tipoSistema", predicado.Parametros("tipoSistema").Valor)
                Case tipoConsultaArticulo.activosParaCodigoBarras
                    comando.Parameters.AddWithValue("@Accion", 9)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                    comando.Parameters.AddWithValue("@tipoSistema", predicado.Parametros("tipoSistema").Valor)
                Case tipoConsultaArticulo.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
                    comando.Parameters.AddWithValue("@tipoSistema", predicado.Parametros("tipoSistema").Valor)
            End Select
            comando.CommandText = "proAlm_ObtenerArticulo"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As articulo
            Dim articulo As New articulo
            articulo.id = Guid.Parse(lectorRenglonActual("id").ToString)
            articulo.idUnidadMedida = Guid.Parse(lectorRenglonActual("idUnidadMedida").ToString)
            articulo.nombre = lectorRenglonActual("nombre").ToString
            articulo.codigoBarras = lectorRenglonActual("codigoBarras").ToString
            articulo.cantidadInicial = CInt(lectorRenglonActual("cantidadInicial").ToString)
            articulo.existencia = CInt(lectorRenglonActual("existencia").ToString)
            articulo.stockMinimo = CInt(lectorRenglonActual("stockMinimo").ToString)
            articulo.stockMaximo = CInt(lectorRenglonActual("stockMaximo").ToString)
            articulo.url = lectorRenglonActual("url").ToString
            articulo.idCategoria = Guid.Parse(lectorRenglonActual("idCategoria").ToString)
            articulo.ultimoPrecio = Decimal.Parse(lectorRenglonActual("ultimoPrecio").ToString)
            articulo.entraAlmacen = CBool(lectorRenglonActual("entraAlmacen").ToString)
            articulo.esActivo = CBool(lectorRenglonActual("esActivo").ToString)
            articulo.tipoSistema = lectorRenglonActual("tipoSistema").ToString
            articulo.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            articulo.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            articulo.nombreUnidadMedida = lectorRenglonActual("nombreUnidadMedida").ToString
            articulo.nombreCategoria = lectorRenglonActual("nombreCategoria").ToString
            Return articulo
        End Function
        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As articulo)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarArticulo"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@idUnidadMedida", entidad.idUnidadMedida)
                comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                comando.Parameters.AddWithValue("@codigoBarras", entidad.codigoBarras)
                comando.Parameters.AddWithValue("@cantidadInicial", entidad.cantidadInicial)
                comando.Parameters.AddWithValue("@existencia", entidad.existencia)
                comando.Parameters.AddWithValue("@stockMinimo", entidad.stockMinimo)
                comando.Parameters.AddWithValue("@stockMaximo", entidad.stockMaximo)
                comando.Parameters.AddWithValue("@url", entidad.url)
                comando.Parameters.AddWithValue("@idCategoria", entidad.idCategoria)
                comando.Parameters.AddWithValue("@ultimoPrecio", entidad.ultimoPrecio)
                comando.Parameters.AddWithValue("@entraAlmacen", entidad.entraAlmacen)
                comando.Parameters.AddWithValue("@tipoSistema", entidad.tipoSistema)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)

            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As articulo)
            If (Not IsNothing(entidad)) Then
                Select Case entidad._tipoEdicion
                    Case tipoEdicionArticulo.cambiarEstado
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_CambiarEstadoArticulo"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                    Case tipoEdicionArticulo.editar
                        comando.CommandType = CommandType.StoredProcedure
                        comando.CommandText = "proAlm_EditarArticulo"
                        comando.Parameters.AddWithValue("@id", entidad.id)
                        If Not entidad.idUnidadMedida Is Nothing Then
                            comando.Parameters.AddWithValue("@idUnidadMedida", entidad.idUnidadMedida)
                        End If
                        If Not entidad.nombre Is Nothing Then
                            comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                        End If
                        If Not entidad.codigoBarras Is Nothing Then
                            comando.Parameters.AddWithValue("@codigoBarras", entidad.codigoBarras)
                        End If

                        comando.Parameters.AddWithValue("@cantidadInicial", entidad.cantidadInicial)
                        comando.Parameters.AddWithValue("@existencia", entidad.existencia)
                        comando.Parameters.AddWithValue("@stockMinimo", entidad.stockMinimo)
                        comando.Parameters.AddWithValue("@stockMaximo", entidad.stockMaximo)
                        If Not entidad.url Is Nothing Then
                            comando.Parameters.AddWithValue("@url", entidad.url)
                        End If
                        If Not entidad.idCategoria Is Nothing Then
                            comando.Parameters.AddWithValue("@idCategoria", entidad.idCategoria)
                        End If
                        comando.Parameters.AddWithValue("@ultimoPrecio", entidad.ultimoPrecio)

                        comando.Parameters.AddWithValue("@entraAlmacen", entidad.entraAlmacen)

                        comando.Parameters.AddWithValue("@tipoSistema", entidad.tipoSistema)
                        comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                        comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
                End Select
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub
        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As articulo)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarArticulo"
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
