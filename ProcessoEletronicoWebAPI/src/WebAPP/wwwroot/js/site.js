/*Variáveis Globais*/
var $modal = $('#modaldefault');
var $btnConfirma = $modal.find('#btnModalConfirma');
var $btnCancela = $modal.find('#btnModalCancela');
var $titulo = $modal.find('#modaldefaultTitulo');
var $conteudo = $modal.find('.modal-body');
var $classBtnConfirma = "";
var $classBtnCancela = "";

function isNullOrEmpty(s) {
    return (s == null || s === "");
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
        $btnConfirma.addClass($classBtnConfirma);
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

    $modal.modal('show');
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

$(document).ajaxError(function (jqXHR, textStatus, errorThrown) {    
    console.log(textStatus);    
});