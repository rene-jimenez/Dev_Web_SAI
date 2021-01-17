


Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports CES.nspUsuarios
Namespace nspUsuarios
    Public Class Proceso_ObtenerUsuarios : Inherits Accion(Of List(Of usuarios))
        Public Sub New()
            MyBase.New("Proceso_ObtenerUsuarios", "Obtiene el listado de registros")
        End Sub
        Protected Overrides Function OnEjecutar() As List(Of usuarios)
            Dim parametros As New List(Of ParametroPredicado)
            Return New CAD.nspControladorDaos.ControladorDaosBase().ObtenerDao(Of CAD.nspUsuarios.daoUsuarios)().ObtenerConjunto(New Predicado("", parametros.ToArray()))
        End Function
    End Class
End Namespace