﻿Imports BLL
Imports EE
Imports System.IO

Public Class ProductoController
    Inherits BaseController

    Private vBLL As ProductoBLL
    Private vPapelBLL As PapelBLL
    Private vCartuchoBLL As CartuchoBLL
    Private vTemaBLL As TemaBLL
    Private vTipoProductoBLL As TipoProductoBLL
    Sub New()
        Me.vBLL = New ProductoBLL
        Me.vPapelBLL = New PapelBLL
        Me.vCartuchoBLL = New CartuchoBLL
        Me.vTemaBLL = New TemaBLL
        Me.vTipoProductoBLL = New TipoProductoBLL
    End Sub

    '
    ' GET: /Producto
    Function Index() As ActionResult
        Dim vLista As List(Of Producto) = Me.vBLL.Listar()
        Return View(vLista)
    End Function

    '
    ' GET: /Producto/Details/5
    <Autorizar(Roles:="ConsultarProducto")>
    Function Detalles(ByVal id As Integer) As ActionResult
        Dim vProducto As Producto = Me.vBLL.ConsutarPorId(id)
        Return View(vProducto)
    End Function

    '
    ' GET: /Producto/Create
    <Autorizar(Roles:="CrearProducto")>
    Function Crear() As ActionResult
        ViewBag.Papeles = Me.vPapelBLL.Listar()
        ViewBag.Cartuchos = Me.vCartuchoBLL.Listar()
        ViewBag.Temas = Me.vTemaBLL.Listar()
        ViewBag.TiposProductos = Me.vTipoProductoBLL.Listar()
        Return View()
    End Function

    '
    ' POST: /Producto/Create
    <Autorizar(Roles:="CrearProducto")>
    <HttpPost()> _
    Function Crear(ByVal entidad As Producto, ByVal Archivo As HttpPostedFileBase) As ActionResult
        Dim listaTiposImagenes As New List(Of String)
        listaTiposImagenes.AddRange({"image/gif", "image/jpeg", "image/png"})
        If Archivo Is Nothing Then
            ModelState.AddModelError("Archivo", "Campo requerido")
        ElseIf Archivo.ContentLength = 0 Then
            ModelState.AddModelError("Archivo", "Campo requerido")
        ElseIf listaTiposImagenes.Contains(Archivo.ContentType) = False Then
            ModelState.AddModelError("Archivo", "Tipo de archivo no permitido")
        End If
        ModelState("Cartucho.Modelo").Errors.Clear()
        ModelState("Cartucho.Nombre").Errors.Clear()
        ModelState("Cartucho.Marca").Errors.Clear()
        ModelState("Cartucho.Tipo").Errors.Clear()
        ModelState("Tema.Tema").Errors.Clear()
        ModelState("TipoProducto.Tipo").Errors.Clear()
        ModelState("Papel.Color").Errors.Clear()
        ModelState("Papel.Nombre").Errors.Clear()
        ModelState("Papel.Tipo").Errors.Clear()
        If ModelState.IsValid Then
            Dim directorioSubidas As String = "~/Content/img"
            Dim urlSubidas As String = "/Content/img"
            Dim idImagen As String = Guid.NewGuid().ToString() + Path.GetExtension(Archivo.FileName)
            Dim rutaImagen As String = Path.Combine(Server.MapPath(directorioSubidas), idImagen)
            Dim urlImagen As String = urlSubidas + "/" + idImagen

            Dim imagen As System.Drawing.Image = System.Drawing.Image.FromStream(Archivo.InputStream)

            Archivo.SaveAs(rutaImagen)
            entidad.Fondo = urlImagen
            entidad.Ancho = imagen.Width
            entidad.Alto = imagen.Height

            Me.vBLL.Crear(entidad)
            Return RedirectToAction("Index")
        End If
        ViewBag.Papeles = Me.vPapelBLL.Listar()
        ViewBag.Cartuchos = Me.vCartuchoBLL.Listar()
        ViewBag.Temas = Me.vTemaBLL.Listar()
        ViewBag.TiposProductos = Me.vTipoProductoBLL.Listar()
        Return View(entidad)
    End Function

    '
    ' GET: /Producto/Edit/5
    <Autorizar(Roles:="EditarProducto")>
    Function Editar(ByVal id As Integer) As ActionResult
        Dim vProducto As Producto = Me.vBLL.ConsutarPorId(id)
        ViewBag.Papeles = Me.vPapelBLL.Listar()
        ViewBag.Cartuchos = Me.vCartuchoBLL.Listar()
        ViewBag.Temas = Me.vTemaBLL.Listar()
        ViewBag.TiposProductos = Me.vTipoProductoBLL.Listar()
        Return View(vProducto)
    End Function

    '
    ' POST: /Producto/Edit/5
    <Autorizar(Roles:="EditarProducto")>
    <HttpPost()> _
    Function Editar(ByVal id As Integer, ByVal entidad As Producto, ByVal Archivo As HttpPostedFileBase) As ActionResult
        Dim listaTiposImagenes As New List(Of String)
        listaTiposImagenes.AddRange({"image/gif", "image/jpeg", "image/png"})
        If Archivo IsNot Nothing Then
            If Archivo.ContentLength = 0 Then
                ModelState.AddModelError("Archivo", "Campo requerido")
            End If
            If listaTiposImagenes.Contains(Archivo.ContentType) = False Then
                ModelState.AddModelError("Archivo", "Tipo de archivo no permitido")
            End If
        End If
        ModelState("Cartucho.Modelo").Errors.Clear()
        ModelState("Cartucho.Nombre").Errors.Clear()
        ModelState("Cartucho.Marca").Errors.Clear()
        ModelState("Cartucho.Tipo").Errors.Clear()
        ModelState("Tema.Tema").Errors.Clear()
        ModelState("TipoProducto.Tipo").Errors.Clear()
        ModelState("Papel.Color").Errors.Clear()
        ModelState("Papel.Nombre").Errors.Clear()
        ModelState("Papel.Tipo").Errors.Clear()
        If ModelState.IsValid Then
            If Archivo IsNot Nothing Then
                Dim vProducto As Producto = Me.vBLL.ConsutarPorId(id)
                Dim rutaImagenActual As String = Server.MapPath("~" + vProducto.Fondo)
                If IO.File.Exists(rutaImagenActual) Then
                    IO.File.Delete(rutaImagenActual)
                End If
                Dim directorioSubidas As String = "~/Content/img"
                Dim urlSubidas As String = "/Content/img"
                Dim idImagen As String = Guid.NewGuid().ToString() + Path.GetExtension(Archivo.FileName)
                Dim rutaImagen As String = Path.Combine(Server.MapPath(directorioSubidas), idImagen)
                Dim urlImagen As String = urlSubidas + "/" + idImagen

                Dim imagen As System.Drawing.Image = System.Drawing.Image.FromStream(Archivo.InputStream)

                Archivo.SaveAs(rutaImagen)
                entidad.Fondo = urlImagen
                entidad.Ancho = imagen.Width
                entidad.Alto = imagen.Height
            End If
            Me.vBLL.Editar(entidad)
            Return RedirectToAction("Index")
        End If
        ViewBag.Papeles = Me.vPapelBLL.Listar()
        ViewBag.Cartuchos = Me.vCartuchoBLL.Listar()
        ViewBag.Temas = Me.vTemaBLL.Listar()
        ViewBag.TiposProductos = Me.vTipoProductoBLL.Listar()
        Return View(entidad)
    End Function

    '
    ' GET: /Producto/Delete/5
    <Autorizar(Roles:="EliminarProducto")>
    Function Eliminar(ByVal id As Integer) As ActionResult
        If ModelState.IsValid Then
            Me.vBLL.Eliminar(id)
        End If
        Return RedirectToAction("Index")
    End Function

    Function Agregar(ByVal id As Integer) As ActionResult
        Dim vProducto As Producto = Me.vBLL.ConsutarPorId(id)
        Return View(vProducto)
    End Function

    <Autorizar()>
    Function Comentar(ByVal productoId As Integer) As ActionResult
        Dim c As New Comentario
        c.Producto.Id = productoId
        Return View(c)
    End Function

    <Autorizar()>
    <HttpPost()>
    Function Comentar(ByVal c As Comentario) As ActionResult
        ModelState("Producto.Nombre").Errors.Clear()
        If ModelState.IsValid Then
            Me.vBLL.Comentar(c)
        End If
        Return RedirectToAction("Agregar", New With {c.Producto.Id})
    End Function

    <HttpPost()>
    Function Comparar(ByVal form As FormCollection) As ActionResult
        Dim ids = form.Item("productosId")
        Dim listaProductos As List(Of Producto) = Me.vBLL.Comparar(ids)
        Return View(listaProductos)
    End Function

End Class