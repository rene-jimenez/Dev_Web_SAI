Imports Contexto.Accion.Accion
Imports CES.nspEntrada
Namespace nspEntrada
    Public Class Proceso_ObtenerEntrada : Inherits Accion(Of entrada)
        Public Property id As Guid
        Public Sub New()
            MyBase.New("Proceso_ObtenerEntrada", "Obtiene un registro")
        End Sub
        Protected Overrides Function OnEjecutar() As entrada
            Return New Proceso_ObtenerEntradas() With {.tipoConsulta = tipoConsultaEntrada.id, .id = id}.Ejecutar().FirstOrDefault()
        End Function
    End Class
End Namespace

