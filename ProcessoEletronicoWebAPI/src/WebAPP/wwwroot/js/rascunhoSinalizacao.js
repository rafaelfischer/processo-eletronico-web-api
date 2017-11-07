/************************************************SINALIZACOES************************************************/
/**
 * Inicializa os campos de sinalização com o plugin iCheck
 */

function AtualizaSinalizacoesLista(data) {    
    $('#formsinalizacoes input[type="checkbox"]').iCheck('uncheck');

    $.each(data.responseJSON, function (i, v) {        
        $('#formsinalizacoes input[value="' + v.id + '"]').iCheck('check');
        if ($('#formsinalizacoes input[value="' + v.id + '"]').length > 0)
            alert('Achou!');
    });

    ResetSinalizacoesLista();
}

function ResetSinalizacoesLista() {
    $('#formsinalizacoes input[type="checkbox"]').iCheck({
        checkboxClass: 'icheckbox_square-red',
        radioClass: 'iradio_square-red',
        increaseArea: '5%' // optional
    });
}