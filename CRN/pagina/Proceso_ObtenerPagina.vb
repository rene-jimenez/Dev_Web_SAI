Imports Contexto.Entidades.Persistencia.Relacional.Daos
Imports Contexto.Accion.Accion
Imports CES.nspPagina
Namespace nspPagina
    Public Class Proceso_ObtenerPagina : Inherits Accion(Of pagina)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerPagina", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As pagina
            Return New Proceso_ObtenerPaginas() With {.TipoConsulta = tipoConsultaPagina.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace

