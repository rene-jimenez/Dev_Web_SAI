Imports System.Data.SqlClient
Imports CAD.nspControladorDaos
Imports CES.nspUsuarios
Imports Contexto.Persistencia.Relacional.Sql
Imports Contexto.Entidades.Persistencia.Relacional.Daos
Namespace nspUsuarios
    Public Class daoUsuarios : Inherits DaoSql(Of usuarios)

        Public Sub New(controladorDaos As ControladorDaosBase)
            MyBase.New(controladorDaos)
        End Sub

        Protected Overrides Sub OnConfigurarComandoSeleccion(comando As SqlCommand, Optional predicado As Predicado = Nothing)
            'comando.Parameters.AddWithValue("@idSistema", predicado.Parametros("idSistema").Valor)
            comando.CommandText = "proAlm_ObtenerUsuariosTodos"
            comando.CommandType = CommandType.StoredProcedure

        End Sub
        Protected Overrides Function OnConfigurarEntidadSeleccionada(lectorRenglonActual As SqlDataReader) As usuarios
            Dim usuarios As New usuarios
            usuarios.id = Guid.Parse(lectorRenglonActual("id").ToString)
            'usuarios.email = lectorRenglonActual("email").ToString
            usuarios.esActivo = lectorRenglonActual("esActivo").ToString
            usuarios.fechaAlta = CDate(lectorRenglonActual("fechaAlta").ToString)
            If lectorRenglonActual("fechaBaja").ToString <> "" Then
                usuarios.fechaBaja = CDate(lectorRenglonActual("fechaBaja").ToString)
            End If
            usuarios.nombre = UCase(lectorRenglonActual("nombre").ToString)
            usuarios.contrasena = lectorRenglonActual("contrasena").ToString
            usuarios.esResetcontrasena = lectorRenglonActual("esResetcontrasena").ToString
            'usuarios.telefono = lectorRenglonActual("telefono").ToString
            usuarios.usuario = lectorRenglonActual("usuario").ToString
            usuarios.ultimaSesion = lectorRenglonActual("ultimaSesion").ToString
            usuarios.idSistema = Guid.Parse(lectorRenglonActual("idSistema").ToString)
            Return usuarios
        End Function

    End Class


End Namespace
