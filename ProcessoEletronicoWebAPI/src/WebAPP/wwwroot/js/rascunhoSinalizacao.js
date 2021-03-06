﻿/************************************************SINALIZACOES************************************************/
/**
 * Inicializa os campos de sinalização com o plugin iCheck
 */

ResetSinalizacoesLista();


function AtualizaSinalizacoesLista(data) {    
    $('#formsinalizacoes input[type="checkbox"]').iCheck('uncheck');
    $(this).attr('checked', true);
    $(this).removeAttr('name');

    $mensagens = data.mensagens;

    $.each(data.entidade, function (i, v) {        
        $('#formsinalizacoes input[value="' + v.id + '"]').attr('name', 'Sinalizacoes['+ i +'].Id' );
        $('#formsinalizacoes input[value="' + v.id + '"]').iCheck('check');        
    });
    
    ResetSinalizacoesLista();
    ExibirMensagemRetornoJson();
}

function ResetSinalizacoesLista() {
    $('#formsinalizacoes input[type="checkbox"]').iCheck({
        checkboxClass: 'icheckbox_square-red',
        radioClass: 'iradio_square-red',
        increaseArea: '5%' // optional
    });
}

function RedefineListaSinalizacoes() {
    $.each($('#formsinalizacoes input[type="checkbox"][checked]'), function (i) {
        $(this).attr('name', 'Sinalizacoes[' + i + '].Id');
    });
}

function LimparSinalizacoes() {
    $('#formsinalizacoes input[type="checkbox"]').iCheck('uncheck');
    $('#formsinalizacoes input[type="checkbox"]').removeAttr('checked');    
}

$('#formsinalizacoes').on('ifUnchecked', 'input[type="checkbox"]', function (event) {
    $(this).removeAttr('name');
    $(this).removeAttr('checked');
    RedefineListaSinalizacoes();
});

$('#formsinalizacoes').on('ifChecked', 'input[type="checkbox"]', function (event) {        
    $(this).attr('checked', true);
    RedefineListaSinalizacoes();
});