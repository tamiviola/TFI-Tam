﻿@ModelType EE.Novedad

@section breadcrumb
    <ul class="breadcrumb">
        <li>
            <a href="@Url.Action("Index", "Novedad")">Novedades</a>
        </li>
        <li>
            <a class="active">Detalles</a>
        </li>
    </ul>
end section

<div class="panel panel-transparent ">
    <!-- Nav tabs -->
    <ul class="nav nav-tabs nav-tabs-fillup">
        <li class="active">
            <a data-toggle="tab" href="#tab-fillup1"><span>Información</span></a>
        </li>
    </ul>
    <!-- Tab panes -->
    <div class="tab-content">
        <div class="tab-pane active" id="tab-fillup1">
            @Html.DisplayNameFor(Function(model) model.FechaCreacion): @Html.DisplayFor(Function(model) model.FechaCreacion)<br />
            @Html.DisplayNameFor(Function(model) model.Titulo): @Html.DisplayFor(Function(model) model.Titulo)<br />
            @Html.DisplayNameFor(Function(model) model.Tipo): @Html.DisplayFor(Function(model) model.Tipo)<br />
            @Html.DisplayNameFor(Function(model) model.Categoria): @Html.DisplayFor(Function(model) model.Categoria.Nombre)<br />
        </div>
    </div>
</div>
<p>
    @Code
        If User.IsInRole("EditarNovedad") Then
        @Html.ActionLink("Editar", "Editar", New With {.id = Model.Id}, New With {.class = "btn btn-primary btn-cons"})
        End If
        If User.IsInRole("EliminarNovedad") Then
        @<button class="btn btn-primary btn-cons" data-target="#modalStickUpSmall" data-toggle="modal">Eliminar</button>
        End If
        If User.IsInRole("VerNovedades") Then
        @Html.ActionLink("Volver", "Index", Nothing, New With {.class = "btn btn-default btn-cons"})
        End If
    End Code
</p>

<div class="modal fade stick-up" id="modalStickUpSmall" tabindex="-1" role="dialog" aria-hidden="true">
    <div class="modal-dialog modal-sm">
        <div class="modal-content-wrapper">
            <div class="modal-content">
                <div class="modal-header clearfix text-left">
                    <button type="button" class="close" data-dismiss="modal" aria-hidden="true">
                        <i class="pg-close fs-14"></i>
                    </button>
                    <h5>Está seguro que desea eliminar el registro?</h5>
                </div>
                <div class="modal-body">
                    <p class="no-margin">Esto eliminará permanentemente el registro.</p>
                </div>
                <div class="modal-footer">
                    @Using Html.BeginForm("Eliminar", "Novedad", New With {.id = Model.Id}, FormMethod.Get)
                        @Html.AntiForgeryToken()
                        @<button type="submit" class="btn btn-primary btn-cons pull-left inline">Aceptar</button>
                    End Using
                    <button type="button" class="btn btn-default btn-cons no-margin pull-left inline" data-dismiss="modal">Cancelar</button>
                </div>
            </div>
        </div>
        <!-- /.modal-content -->
    </div>
    <!-- /.modal-dialog -->
</div>

