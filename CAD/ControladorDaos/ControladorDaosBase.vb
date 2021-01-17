Imports Contexto.Persistencia.Relacional.Sql
Namespace nspControladorDaos
    Public Class ControladorDaosBase : Inherits DaoControladorSql
        Public Sub New()
            MyBase.New(Contexto.Configuracion.Configuracion.ContextoConfiguracion.Crear(
                       New Contexto.Configuracion.Persistencia.XML.XML.PersistenciaEstrategiaXml(
                           "CPD.Entidades.Configuracion.xml")).GetProperty("ConnectionStrinCPD"))
        End Sub
    End Class
End Namespace

