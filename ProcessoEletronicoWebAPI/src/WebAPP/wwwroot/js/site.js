/*Variáveis Globais*/
var $modalDefault = $('#modaldefault');
var $modalDetalhe = $('#modal-detalhe');
var $btnConfirma = $modalDefault.find('#btnModalConfirma');
var $btnCancela = $modalDefault.find('#btnModalCancela');
var $titulo = $modalDefault.find('#modaldefaultTitulo');
var $conteudo = $modalDefault.find('.modal-body');
var $tituloModalDetalhe = $modalDetalhe.find('#modal-detalhe-titulo');
var $conteudoModalDetalhe = $modalDetalhe.find('.modal-body');
var $classBtnConfirma = "";
var $classBtnCancela = "";

$(document).ready(function () {
    ativaTooltipBootstrap();
});

/*Toastr*/
toastr.options = {
    "closeButton": true,
    "debug": false,
    "newestOnTop": false,
    "progressBar": true,
    "positionClass": "toast-top-center",
    "preventDuplicates": false,
    "onclick": null,
    "showDuration": "300",
    "hideDuration": "1000",
    "timeOut": "5000",
    "extendedTimeOut": "1000",
    "showEasing": "swing",
    "hideEasing": "linear",
    "showMethod": "fadeIn",
    "hideMethod": "fadeOut"
}

$(document).ready(function () {
    ExibirMensagem();
});

function ExibirMensagem() {
    $.each($mensagens, function () {
        toastr[this.TipoToastr](this.Texto)
    });

    $mensagens = [];
}

function ExibirMensagemRetornoJson() {
    $.each($mensagens, function () {
        toastr[this.tipoToastr](this.texto)
    });

    $mensagens = [];
}

/*Correcao padding-right body ao fechar modal*/
$(document.body).on('hide.bs.modal,hidden.bs.modal, shown.bs.modal, show.bs.modal', function () {
    $('body').css('padding-right', '0');
});

function isNullOrEmpty(s) {
    return (s === null || s === "");
}

function carregaModalDefault(
    titulo,
    conteudo,
    btnConfirma,
    btnCancela,
    classBtnConfirma,
    classBtnCancela,
    acaoConfirma,
    acaoCancela
) {
    $titulo.text(titulo);
    $conteudo.html(conteudo);

    if (!isNullOrEmpty(btnConfirma)) {
        $btnConfirma.text(btnConfirma);
    }

    if (!isNullOrEmpty(btnCancela)) {
        $btnCancela.text(btnCancela);
    }

    if (!isNullOrEmpty(classBtnConfirma)) {
        $classBtnConfirma = classBtnConfirma;
        $btnConfirma.attr('data-btn', $classBtnConfirma);
    }

    if (!isNullOrEmpty(classBtnCancela)) {
        $classBtnCancela = classBtnCancela;
        $btnCancela.addClass($classBtnCancela);
    }

    if (!isNullOrEmpty(acaoConfirma)) {
        $btnConfirma.attr('data-acaoconfirmar', acaoConfirma);
    }

    if (!isNullOrEmpty(acaoCancela)) {
        $btnCancela.attr('data-acaocancelas', acaoCancela);
    }

    $modalDefault.modal('show');
}

function ResetModalDefault() {
    $btnConfirma.text(btnConfirma);
    $btnCancela.text(btnCancela);
    $titulo.text(titulo);
    $conteudo.html(conteudo);
    $btnConfirma.removeClass($classBtnConfirma);
    $btnCancela.removeClass($classBtnCancela);
    $btnConfirma.removeAttr('data-acao');
    $btnCancela.removeAttr('data-acao');
}

function CarregaModelDetalhe(titulo, conteudo) {
    $tituloModalDetalhe.text(titulo);
    $conteudoModalDetalhe.html(conteudo);

    $modalDetalhe.modal('show')
}

$(document).ajaxError(function (jqXHR, textStatus, errorThrown) {
    //console.log(textStatus);    
});

function mascaraCpf(valor) {
    return valor.replace(/(\d{3})(\d{3})(\d{3})(\d{2})/g, "\$1.\$2.\$3\-\$4");
}

function mascaraCnpj(valor) {
    return valor.replace(/(\d{2})(\d{3})(\d{3})(\d{4})(\d{2})/g, "\$1.\$2.\$3\/\$4\-\$5");
}

function ativaTooltipBootstrap() {
    $('[data-toggle="tooltip"]').tooltip({
        placement: 'top'
    });
}

/*CONTROLE LOAD AJAX */
$(document).ajaxStart(function () {
    $('#modalLoad').modal();
});

$(document).ajaxComplete(function (data) {
    console.log(data);
});

$(document).ajaxStop(function () {
    $('#modalLoad').modal('hide');
    ExibirMensagem();
});

