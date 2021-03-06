﻿Public Class HomeController
    Inherits BaseController

    Sub New()
        Me.vProductoBLL = New BLL.ProductoBLL()
    End Sub
    '
    ' GET: /Home
    Private vProductoBLL As BLL.ProductoBLL
    Function Index() As ActionResult
        Dim vTemaBLL As New BLL.TemaBLL
        ViewBag.Temas = vTemaBLL.Listar()
        Dim vTipoProducto As New BLL.TipoProductoBLL
        ViewBag.TipoProductos = vTipoProducto.Listar()
        Return View(Me.vProductoBLL.Listar())
    End Function

    Function QuienesSomos() As ActionResult
        Return View()
    End Function

    Function Contacto() As ActionResult
        Return View()
    End Function

    Function PoliticaPrivacidad() As ActionResult
        Return View()
    End Function

    Function FAQ() As ActionResult
        Return View()
    End Function

    Function PaginaNoEncontrada() As ActionResult
        Return View()
    End Function

    Function Excepcion() As ActionResult
        Return View()
    End Function

End Class