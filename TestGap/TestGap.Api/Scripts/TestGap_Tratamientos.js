var pantallaBloqueada = false;
var iconBloqueo = "#imgCargando";
var mensajeExito = '#MjsExito';
var mensajeErro = '#MjsError';

var linkRealizarAccion = "#linkRealizarAccion";
var linkCancelarAccion = "#linkCancelarAccion";
var linkEditarTratamiento = ".lnkEditarTratamiento";
var linkDetalleTratamiento = ".lnkDetalleTratamiento";
var linkEliminarTratamiento = ".lnkEliminarTratamiento";

var btnGuardarTratamiento = "#btnGuardarTratamiento";
var btnGuardarEdicionTratamiento = "#btnGuardarEdicionTratamiento";
var btnEliminarTratamiento = "#btnEliminarTratamiento";




var ContenedorLinkNegativo = '#ContenedorLinkNegativo';
var ContenedorLinkPositivo = '#ContenedorLinkPositivo';

var ContenedorTablaTratamientos = '#ContenedorTablaTratamientos';

var mostrarCancelar = false;
var CargaIdPaciente = 0;
var CargaIdTratamiento = 1;
var CargaJson = 2;

$(document).ready(function() {


    /***Aciones principales*****/
    $(linkRealizarAccion).on("click", function() {
        mostrarCancelar = true;
        EjecutarAjaxGenerico(window.urlActionCrearTratamiento, RegistroTratamiento, CargarDatos(CargaJson));
    });
    $(linkCancelarAccion).on("click", function() {
        mostrarCancelar = false;
        EjecutarAjaxGenerico(window.urlActionConsultarTratamiento, RegistroTratamiento, CargarDatos(CargaIdPaciente));
    });
    
    /***Aciones Links****/
    $('body').delegate(linkEditarTratamiento, 'click', function(event) {
        mostrarCancelar = true;
        EjecutarAjaxGenerico(window.urlActionEditarTratamiento, RegistroTratamiento, CargarDatos(CargaIdTratamiento, $(this).attr('id').split('_')[1]));
    });
    $('body').delegate(linkDetalleTratamiento, 'click', function(event) {
        mostrarCancelar = true;
        EjecutarAjaxGenerico(window.urlActionDetalleTratamiento, RegistroTratamiento, CargarDatos(CargaIdTratamiento, $(this).attr('id').split('_')[1]));
    });
    $('body').delegate(linkEliminarTratamiento, 'click', function(event) {
        mostrarCancelar = true;
        EjecutarAjaxGenerico(window.urlActionEliminarTratamiento, RegistroTratamiento, CargarDatos(CargaIdTratamiento, $(this).attr('id').split('_')[1]));
    });

    /***Aciones Botones****/
    $('body').delegate(btnGuardarTratamiento, 'click', function(event) {
        mostrarCancelar = false;
        EjecutarAjaxGenerico(window.urlActionProcesarCrearTratamiento, RegistroTratamiento, CargarDatos(CargaJson));
    });

    $('body').delegate(btnGuardarEdicionTratamiento, 'click', function (event) {
        mostrarCancelar = false;
        EjecutarAjaxGenerico(window.urlActionProcesarEditarTratamiento, RegistroTratamiento, CargarDatos(CargaJson));
    });

    $('body').delegate(btnEliminarTratamiento, 'click', function (event) {
        mostrarCancelar = false;
        EjecutarAjaxGenerico(window.urlActionPprocesarEliminarTratamiento, RegistroTratamiento, CargarDatos(CargaIdTratamiento));
    });
});

function RegistroTratamiento(respuesta) {

    if (respuesta.CodigoRepuesta == 0 || respuesta.CodigoRepuesta == "0") {

        $(!mostrarCancelar ? ContenedorLinkNegativo : ContenedorLinkPositivo).hide("slow", function() {

            $(ContenedorTablaTratamientos).hide("slow", function() {
                if (respuesta.ObjetoRetornado != null && respuesta.ObjetoRetornado.html != null) {
                    $(ContenedorTablaTratamientos).html(respuesta.ObjetoRetornado.html);
                }
                $(ContenedorTablaTratamientos).show("slow", function() {
                    $(mostrarCancelar ? ContenedorLinkNegativo : ContenedorLinkPositivo).show("slow");
                });
            });

        });
    } else {
        $(ContenedorTablaTratamientos).hide("slow", function() {
            if (respuesta.ObjetoRetornado != null && respuesta.ObjetoRetornado.html != null) {
                $(ContenedorTablaTratamientos).html(respuesta.ObjetoRetornado.html);
            }
            $(ContenedorTablaTratamientos).show("slow");
        });
    }

}

function CargarDatos(TipoCarga,id) {
    
    var datos= {
        __RequestVerificationToken: $('ContenedorRegistroTratamiento input[name="__RequestVerificationToken"]').val()
    };
    switch (TipoCarga) {
        case CargaIdPaciente:
            datos.id = $("#Id_Paciente").val();
            break;
        case CargaIdTratamiento:
            datos.id = id!=null?id:$("#Id_Tratamiento").val();
            break;
        case CargaJson:
            datos.tratamiento = {
                Id_Paciente: $("#Id_Paciente").val(),
                Id_Tratamiento: $("#Id_Tratamiento").val(),
                Fecha_Inicio: $("#Fecha_Inicio").val(),
                Fecha_Fin: $("#Fecha_Fin").val(),
                Costo: $("#Costo").val(),
                Detalle: $("#Detalle").val()
            }
            break;
    }
    return datos;
}

function  EjecutarAjaxGenerico (url, nombreMetodoEjecutar, objeto) {
    var contentType = "application/json; charset=utf-8";
   imgCargando();
    $.ajax({
        url: url,
        cache: false,
        type: "POST",
        data: JSON.stringify(objeto),
        dataType: "json",
        contentType: contentType,
        processData: true,
        success: function (msg) {
            imgCargando();
            if (!(msg.CodigoRepuesta == 0 || msg.CodigoRepuesta == "0")) {
                
                mostrarMensajeExito(msg.MensajeRespuesta);
            } else {
                mostrarMensajeError(msg.MensajeRespuesta);
            }
            nombreMetodoEjecutar(msg);
        },
        error: function (jqXhr, textStatus, errorThrown) {
            imgCargando();
            mostrarMensajeError(errorThrown);

        }
    });
}

function imgCargando() {
    if (pantallaBloqueada) {
        $(iconBloqueo).show("slow");
        
    } else {
        $(iconBloqueo).hide("slow");
    }
}

function mostrarMensajeError(mensaje) {
    $(mensajeErro).text(mensaje);
    $(mensajeErro).fadeIn("slow", function() {
        window.setTimeout(function() {
            $(mensajeErro).fadeOut("slow");
        }, 10000);

    });
}

function mostrarMensajeExito(mensaje) {
    $(mensajeExito).text(mensaje);
    $(mensajeExito).fadeIn("slow", function() {
        window.setTimeout(function() {
            $(mensajeExito).fadeOut("slow");
        }, 10000);
    });
}