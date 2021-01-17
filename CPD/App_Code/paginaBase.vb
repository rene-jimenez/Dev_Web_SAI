Imports CES.nspPopup
Imports CES
Imports cadenero.entidades.nspUsuarioAutenticado
Imports System.Management
Imports System.Net
Imports System.Net.NetworkInformation
Namespace nspPaginaBase
    Public Class PaginaBase : Inherits System.Web.UI.Page
        Public ReadOnly Property IdUsuario As Guid?
            Get
                Return OnObtenerIdUsuario()
            End Get
        End Property
        Public ReadOnly Property sistemaActivo As nspSistema.sistema
            Get
                Return OnObtenerSistema()
            End Get
        End Property
        Public ReadOnly Property direccionIP As String
            Get
                Return OnObtenerIPusuarioAutenticado()
            End Get
        End Property
        Public ReadOnly Property idElaboro As Guid?
            Get
                Return onObtenerIdElaboro()
            End Get
        End Property

        Public ReadOnly Property IdRol As Guid?
            Get
                Return OnObtenerIdRol()
            End Get
        End Property
        Public ReadOnly Property NombreUsuario As String
            Get
                Return OnObtenerNombreUsuario()
            End Get
        End Property
        Public ReadOnly Property myUsuario As String
            Get
                Return OnObtenerUsuario()
            End Get
        End Property
        Public ReadOnly Property RolUsuario As String
            Get
                Return OnObtenerRolUsuario()
            End Get
        End Property
        Public ReadOnly Property ImagenUsuario As String
            Get
                Return OnObtenerImagen()
            End Get
        End Property
        Public ReadOnly Property UsuarioEsAutenticado As Boolean
            Get
                Return OnObtenerusuarioAutenticado()
            End Get
        End Property
        Public ReadOnly Property Resetear As Boolean
            Get
                Return OnVerificarReteoContrasena()
            End Get
        End Property
        Private Function OnObtenerIPusuarioAutenticado() As String
            Dim strHostName As String = Dns.GetHostName()
            Dim ipEntry As IPHostEntry = Dns.GetHostEntry(strHostName)

            Dim lblIPAddress = Convert.ToString(ipEntry.AddressList(ipEntry.AddressList.Length - 1))
            Dim lblHostName = Convert.ToString(ipEntry.HostName)

            Dim IPAdd As String = String.Empty
            IPAdd = Request.ServerVariables("HTTP_X_FORWARDED_FOR")
            If String.IsNullOrEmpty(IPAdd) Then
                IPAdd = Request.ServerVariables("REMOTE_ADDR")
            End If
            Return IPAdd
        End Function
        Private Function OnObtenerusuarioAutenticado() As String
            Dim continuar As Boolean = False
            If (Not HttpContext.Current.User Is Nothing) Then
                If (HttpContext.Current.User.Identity.IsAuthenticated) Then
                    Dim usuario = OnObtenerEntidad()
                    If (Not usuario Is Nothing) Then
                        continuar = True
                    Else
                        continuar = False
                    End If
                End If
            End If
            Return continuar
        End Function
        Private Function OnObtenerIdUsuario() As Guid?
            Dim id As Guid?
            If (Not HttpContext.Current.User Is Nothing) Then
                If (HttpContext.Current.User.Identity.IsAuthenticated) Then
                    Dim usuario = OnObtenerEntidad()
                    If (Not usuario Is Nothing) Then
                        id = usuario.idUsuario
                    Else
                        id = Nothing
                    End If
                End If
            End If
            Return id
        End Function
        Private Function OnObtenerIdRol() As Guid?
            Dim id As Guid?
            If (Not HttpContext.Current.User Is Nothing) Then
                If (HttpContext.Current.User.Identity.IsAuthenticated) Then
                    Dim rol = OnObtenerEntidad()
                    If (Not rol Is Nothing) Then
                        id = rol.idRol
                    Else
                        id = Nothing
                    End If
                End If
            End If
            Return id
        End Function
        Private Function OnObtenerNombreUsuario() As String
            Dim nombre As String = String.Empty
            If (Not HttpContext.Current.User Is Nothing) Then
                If (HttpContext.Current.User.Identity.IsAuthenticated) Then
                    Dim usuario = OnObtenerEntidad()
                    If (Not usuario Is Nothing) Then
                        nombre = usuario.nombreUsuario
                    End If
                End If
            End If
            Return nombre
        End Function
        Private Function OnObtenerSistema() As nspSistema.sistema
            Dim sistema As New nspSistema.sistema
            If (Not HttpContext.Current.User Is Nothing) Then
                Dim sistemaSesion = OnObtenerEntidadSistema()
                If (Not sistemaSesion Is Nothing) Then
                    sistema.id = sistemaSesion.id
                    sistema.idSistema = sistemaSesion.id
                    sistema.nombre = sistemaSesion.nombre
                    sistema.logo = sistemaSesion.logo
                    sistema.tipo = sistemaSesion.tipo
                    sistema.año = sistemaSesion.año
                    sistema.fondo = sistemaSesion.fondo
                End If

            End If
            Return sistema
        End Function
        Private Function onObtenerIdElaboro() As Guid?
            Dim idElaboro = New CRN.nspFirma.Proceso_ObtenerFirmas() With {.tipoConsulta = nspFirma.tipoConsultaFirma.todos}.Ejecutar.Where(Function(a) a.idUsuario = OnObtenerIdUsuario.Value And a.nombre = "Elaboró")
            If idElaboro.Count > 0 Then
                Return idElaboro.FirstOrDefault.id
            End If
        End Function

        Private Function OnObtenerUsuario() As String
            Dim usuarioText As String = String.Empty
            If (Not HttpContext.Current.User Is Nothing) Then
                If (HttpContext.Current.User.Identity.IsAuthenticated) Then
                    Dim usuario = OnObtenerEntidad()
                    If (Not usuario Is Nothing) Then
                        usuarioText = usuario.nombreUsuario
                    End If
                End If
            End If
            Return usuarioText
        End Function
        Private Function OnObtenerRolUsuario() As String
            Dim rol As String = String.Empty
            If (Not HttpContext.Current.User Is Nothing) Then
                If (HttpContext.Current.User.Identity.IsAuthenticated) Then
                    Dim usuario = OnObtenerEntidad()
                    If (Not usuario Is Nothing) Then
                        rol = usuario.rol
                    End If
                End If
            End If
            Return rol
        End Function
        Private Function OnObtenerImagen() As String
            Dim nombre As String = String.Empty
            If (Not HttpContext.Current.User Is Nothing) Then
                Dim usuario = OnObtenerEntidad()
                If (Not usuario Is Nothing) Then
                    nombre = usuario.foto
                End If
            End If
            Return nombre
        End Function
        Private Function OnVerificarReteoContrasena() As Boolean
            Dim reset As Boolean
            If (Not HttpContext.Current.User Is Nothing) Then
                If (HttpContext.Current.User.Identity.IsAuthenticated) Then
                    Dim rol = OnObtenerEntidad()
                    If (Not rol Is Nothing) Then
                        reset = rol.resetear
                    Else
                        CerrarSesion()
                    End If
                End If
            End If
            Return reset
        End Function
        Private Function OnObtenerEntidad() As usuarioAutenticado
            Dim user As New usuarioAutenticado
            If Not Session("user") Is Nothing Then
                user = CType(Session("user"), usuarioAutenticado)
            Else
                CerrarSesion()
            End If
            Return user
        End Function

        Private Function OnObtenerEntidadSistema() As nspSistema.sistema
            Dim sistema As New nspSistema.sistema
            If Not Session("system_Sispro") Is Nothing Then
                sistema = CType(Session("system_Sispro"), nspSistema.sistema)
            Else
                CerrarSesion()
            End If
            Return sistema
        End Function


        Public Sub OnMostrarMensajeAccion(titulo As String, cuerpo As String, tipo As tipoPopup, redireccionar As Boolean, url As String)
            Dim claseTitulo As String
            Dim claseBoton As String
            Select Case tipo
                Case tipoPopup.Verde
                    claseTitulo = "text-success"
                    claseBoton = "btn btn-success"
                Case tipoPopup.Naranja
                    claseTitulo = "text-warning"
                    claseBoton = "btn btn-warning"
                Case tipoPopup.Rojo
                    claseTitulo = "text-danger"
                    claseBoton = "btn btn-danger"
                Case Else
                    claseTitulo = "text-default"
                    claseBoton = "btn btn-default"
            End Select
            Dim urlPage As String = Page.ResolveClientUrl("~/" + url)
            Dim sb As StringBuilder = New StringBuilder
            Dim modal As New StringBuilder
            modal.Append("<div class='modal-content'>")
            modal.Append("<div class='modal-header'>")
            modal.Append("<button type='button' class='close' data-dismiss='modal' aria-hidden='true'>×</button>")
            modal.Append(String.Format("<h4><p class='{0}'>{1}</p></h4>", claseTitulo, titulo))
            modal.Append("</div>")
            modal.Append("<div class='modal-body'>")
            modal.Append(String.Format("<p class='text-center'>{0}</p>", cuerpo))
            modal.Append("</div>")
            modal.Append("<div class='modal-footer'>")

            If (redireccionar) Then
                modal.Append(String.Format("<a href='{1}' id='btnRedirect' class='{0}' >Cerrar</a>", claseBoton, urlPage))
            Else
                modal.Append(String.Format("<a href='#' id='btnCerrar' class='{0}' data-dismiss='modal' aria-hidden='true' >Aceptar</a>", claseBoton))
            End If
            modal.Append("</div>")
            modal.Append("</div>")
            sb.Append(String.Format("muestra(""{0}"");", modal))
            ScriptManager.RegisterClientScriptBlock(Me, Me.GetType, "scriptmodal", sb.ToString, True)

        End Sub
        Public Sub CerrarSesion()
            HttpContext.Current.User = Nothing
            HttpContext.Current.Session.Clear()
            HttpContext.Current.Session.Abandon()
            HttpContext.Current.Response.Cookies.Clear()
            HttpContext.Current.Response.Clear()
            HttpContext.Current.Request.Cookies.Clear()
            FormsAuthentication.SignOut()
            HttpContext.Current.Response.Redirect("~/login.aspx")
        End Sub
        Public Sub mandaDefault()
            HttpContext.Current.Response.Redirect("~/management/default.aspx")
        End Sub
    End Class
End Namespace