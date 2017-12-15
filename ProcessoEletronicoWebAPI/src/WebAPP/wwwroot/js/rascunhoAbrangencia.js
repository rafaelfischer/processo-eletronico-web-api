/************************************************ABRANGENCIA************************************************/

/*Inicialização do selectbox de municipio e uf com o plugin select2*/
$eSUf.select2({ width: '100%' });
$eSMunicipio.select2(
    {
        width: '100%',
        placeholder: 'Selecione um Município',
        language: {
            noResults: function () {
                return "Nenhum município encontrado.";
            }
        }
    }
);

/*Evento Change para o campo UF que dispara a consulta de municípios por UF*/
$eSUf.on('change', function (e) {
    $.get(
        "/Suporte/GetMunicipiosPorUF?uf=" + $eSUf.val(), function (data) {
            $eSMunicipio.empty();
            if (data.length > 0) {
                data.unshift({ id: 'all', text: 'Todos' });
                data.unshift({ id: '-1', text: 'Selecione um Município' });
                $eSMunicipio.select2({ width: '100%', data: data });
            }
        });
});

/*Evento select para o campo municipio que cria checkbox a partir da seleção*/
$eSMunicipio.on('select2:select', function (e) {
    var idSelecionado = $('#municipiosajax').val();
    var exist = false;

    if (idSelecionado == -1)
        return false;

    //Seleciona todos as opções ou apenas uma opção
    if (idSelecionado == 'all') {
        $.each($("#municipiosajax option"), function () {
            if ($(this).val() != 'all' && $(this).val() != -1 && !JaExiste($(this).val())) {
                IncluirSelecionado($(this).text(), $(this).val());
            }
        });
    } else {

        if (JaExiste(idSelecionado))
            return false;

        /*Cria novo checkbox*/
        IncluirSelecionado($("#municipiosajax option:selected").text(), $('#municipiosajax').val());
    }

    $('input').iCheck({
        checkboxClass: 'icheckbox_square-red',
        radioClass: 'iradio_square-red',
        increaseArea: '20%' // optional
    });

    $eSMunicipio.val(-1).trigger("change");
});

/*Evento de uncheck para os checkbox que utilizam o plugin iCheck*/
$('form').on('ifUnchecked', 'input[type="checkbox"]', function (event) {
    $('#' + this.value).remove();

    $.each($('#grupomunicipios input[type="checkbox"]'), function (i) {
        $(this).attr('name', 'MunicipiosRascunhoProcesso[' + i + '].GuidMunicipio');
    })
    
});

/**
 * Inclui municipio selecionado passando como parâmetros text e val
 * @param {any} text
 * @param {any} val
 */
function IncluirSelecionado(text, val) {
    index = $('#grupomunicipios input[type="checkbox"]').length;
    $('#grupomunicipios').append('<div class="checkbox municipio col-lg-3 col-md-4 col-xs-6" id="' + val + '"><label><input type="checkbox" checked name="MunicipiosRascunhoProcesso[' + index + '].GuidMunicipio" value="' + val + '" /> ' + text + '</label></div>');    
}

/**
 * Verifica se ID seletor existe no DOM
 * @param {any} idSelecionado
 */
function JaExiste(idSelecionado) {
    var exist = false;
    if ($('#' + idSelecionado).length > 0)
        exist = true;
    return exist;
}

/**
 * Reseta o formulário de abrangência
 */
function LimparAbrangencia() {
    $("#grupomunicipios div.municipio").remove();
    $('form#formabrangencia')[0].reset();
    $eSMunicipio.val(-1).trigger("change");
    $eSUf.val(0).trigger("change");
    index = 0;
}
/**
 * Reseta os campos de municipio com o plugin iCheck
 */
function ResetMunicipioLista() {
    $('#formabrangencia input').iCheck({
        checkboxClass: 'icheckbox_square-red',
        radioClass: 'iradio_square-red',
        increaseArea: '5%' // optional
    });
}
