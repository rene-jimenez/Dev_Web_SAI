Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports Contexto.Persistencia.Relacional.Sql
Imports CES.nspPagina
Imports Contexto.Entidades.Persistencia.Relacional.Daos

Namespace nspPagina
    Public Class daoPagina : Inherits DaoSql(Of pagina)
        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            If (Not predicado.ContieneParametro("tipoConsulta")) Then
                Throw New NotSupportedException("Imposible continuar: no contiene parámetro correcto.")
            End If
            Dim tipoConsulta As tipoConsultaPagina = CType(predicado.Parametros("tipoConsulta").Valor, tipoConsultaPagina)
            Select Case tipoConsulta
                Case tipoConsultaPagina.id
                    comando.Parameters.AddWithValue("@Accion", 1)
                    comando.Parameters.AddWithValue("@id", predicado.Parametros("id").Valor)
                Case tipoConsultaPagina.estadoOrdenadoYAgrupado
                    comando.Parameters.AddWithValue("@Accion", 2)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                Case tipoConsultaPagina.nombre
                    comando.Parameters.AddWithValue("@Accion", 3)
                    comando.Parameters.AddWithValue("@nombre", predicado.Parametros("nombre").Valor)
                Case tipoConsultaPagina.jerarquia
                    comando.Parameters.AddWithValue("@Accion", 4)
                    comando.Parameters.AddWithValue("@jerarquia", predicado.Parametros("jerarquia").Valor)
                Case tipoConsultaPagina.idPaginaPadre
                    comando.Parameters.AddWithValue("@Accion", 5)
                    comando.Parameters.AddWithValue("@idPaginaPadre", predicado.Parametros("idPaginaPadre").Valor)
                Case tipoConsultaPagina.esActivo
                    comando.Parameters.AddWithValue("@Accion", 6)
                    comando.Parameters.AddWithValue("@esActivo", predicado.Parametros("esActivo").Valor)
                Case tipoConsultaPagina.todos
                    comando.Parameters.AddWithValue("@Accion", 99)
            End Select

            comando.CommandText = "proAlm_ObtenerPagina"
            comando.CommandType = CommandType.StoredProcedure
        End Sub

        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As pagina
            Dim pagina As New pagina
            pagina.id = Guid.Parse(lectorRenglonActual("id").ToString)
            pagina.esActivo = CBool(lectorRenglonActual("esActivo").ToString)
            pagina.nombre = lectorRenglonActual("nombre").ToString
            pagina.url = lectorRenglonActual("url").ToString
            pagina.jerarquia = lectorRenglonActual("jerarquia").ToString
            If lectorRenglonActual("idPaginaPadre").ToString <> "" Then
                pagina.idPaginaPadre = Guid.Parse(lectorRenglonActual("idPaginaPadre").ToString)
            End If
            pagina.ipUsuario = lectorRenglonActual("ipUsuario").ToString
            pagina.idUsuarioMovimiento = Guid.Parse(lectorRenglonActual("idUsuarioMovimiento").ToString)
            Return pagina
        End Function

        Protected Overrides Sub OnConfigurarComandoInsertar(comando As SqlCommand, entidad As pagina)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_AgregarPagina"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                comando.Parameters.AddWithValue("@url", entidad.url)
                comando.Parameters.AddWithValue("@jerarquia", entidad.jerarquia)
                If Not entidad.idPaginaPadre Is Nothing Then
                    comando.Parameters.AddWithValue("@idPaginaPadre", entidad.idPaginaPadre)
                End If
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoActualizar(comando As SqlCommand, entidad As pagina)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EditarPagina"
                comando.Parameters.AddWithValue("@id", entidad.id)
                comando.Parameters.AddWithValue("@esActivo", entidad.esActivo)
                comando.Parameters.AddWithValue("@nombre", entidad.nombre)
                comando.Parameters.AddWithValue("@url", entidad.url)
                comando.Parameters.AddWithValue("@jerarquia", entidad.jerarquia)
                comando.Parameters.AddWithValue("@idPaginaPadre", entidad.idPaginaPadre)
                comando.Parameters.AddWithValue("@ipUsuario", entidad.ipUsuario)
                comando.Parameters.AddWithValue("@idUsuarioMovimiento", entidad.idUsuarioMovimiento)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

        Protected Overrides Sub OnConfigurarComandoEliminar(comando As SqlCommand, entidad As pagina)
            If (Not IsNothing(entidad)) Then
                comando.CommandType = CommandType.StoredProcedure
                comando.CommandText = "proAlm_EliminarPagina"
                comando.Parameters.AddWithValue("@id", entidad.id)
            Else
                Throw New NotSupportedException("Imposible continuar: Entidad no contiene datos.")
            End If
        End Sub

    End Class
End Namespace

