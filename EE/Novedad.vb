﻿Imports System.ComponentModel.DataAnnotations
Public Class Novedad

    Private vId As Integer
    Public Property Id() As Integer
        Get
            Return vId
        End Get
        Set(ByVal value As Integer)
            vId = value
        End Set
    End Property

    Private vFechaCreacion As Date
    Public Property FechaCreacion() As Date
        Get
            Return vFechaCreacion
        End Get
        Set(ByVal value As Date)
            vFechaCreacion = value
        End Set
    End Property

    Private vTitulo As String
    Public Property Titulo() As String
        Get
            Return vTitulo
        End Get
        Set(ByVal value As String)
            vTitulo = value
        End Set
    End Property

    Private vContenido As String
    Public Property Contenido() As String
        Get
            Return vContenido
        End Get
        Set(ByVal value As String)
            vContenido = value
        End Set
    End Property

    Private vTipo As String
    Public Property Tipo() As String
        Get
            Return vTipo
        End Get
        Set(ByVal value As String)
            vTipo = value
        End Set
    End Property

    Private vCategoria As Categoria
    Public Property Categoria() As Categoria
        Get
            Return vCategoria
        End Get
        Set(ByVal value As Categoria)
            vCategoria = value
        End Set
    End Property

End Class