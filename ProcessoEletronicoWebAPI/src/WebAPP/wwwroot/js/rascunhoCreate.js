$(document).ready(function () {
    $('input').iCheck({
        checkboxClass: 'icheckbox_square-red',
        radioClass: 'iradio_square-red',
        increaseArea: '20%' // optional
    });
    $("form#basico select").select2({ width: '100%' });
});

var $eSUf = $("#ufajax");
var $eSMunicipio = $("#municipiosajax");
var $eSMunicipiosSelecionados = $("#municipiosselecionados");

$eSUf.select2({ width: '100%' });
$eSMunicipio.select2({ width: '100%', placeholder: 'Selecione um Município' });
$eSMunicipiosSelecionados.select2({ width: '100%' });

var abrangencia = [];
var objetoSelecionado = {};
var selecionados = [];

$eSUf.on('change', function (e) {
    $.get(
        "/MunicipioUf/GetMunicipiosPorUF?uf=" + $eSUf.val(), function (data) {
            $eSMunicipio.empty();
            data.unshift({ id: 'all', text: 'Todos' });
            data.unshift({ id: '-1', text: 'Selecione um Município' });            
            $eSMunicipio.select2({ width: '100%', data: data });
        });
});

//$eSMunicipio.on('select2:select', function (e) {
//    var idSelecionado = $('#municipiosajax').val();
//    var exist = false;

//    if (idSelecionado == -1)
//        return false;    

//    //Seleciona todos as opções ou apenas uma opção
//    if (idSelecionado == 'all') {
//        $.each($("#municipiosajax option"), function () {
//            if ($(this).val() != 'all' && $(this).val() != -1 && !JaExiste($(this).val())) {
//                IncluirSelecionado($(this).text(), $(this).val());
//            }            
//        });
//    } else {        

//        if (JaExiste(idSelecionado))
//            return false;

//        var newOption = new Option($("#municipiosajax option:selected").text(), $('#municipiosajax').val(), false, false);

//        selecionados.push({ id: newOption.value, text: newOption.text });

//        $eSMunicipiosSelecionados.append(newOption).trigger('change');

//        abrangencia = [];

//        $.each($('#municipiosselecionados option'), function () {
//            abrangencia.push($(this).val());
//        });

//        $eSMunicipiosSelecionados.val(abrangencia).trigger("change");
//    }

    
//    $eSMunicipio.val(-1).trigger("change");
//});

$eSMunicipiosSelecionados.on('select2:unselect', function (e) {
    var unselected_value = e.params.data.id;
    var posicao = abrangencia.indexOf(unselected_value);

    var posicaoObj;
    $.each(selecionados, function (i) {
        if (this.id == unselected_value) {
            posicaoObj = i;
        }
    });

    if (~posicao) {
        abrangencia.splice(posicao, 1);
        selecionados.splice(posicaoObj, 1);
        $eSMunicipiosSelecionados.empty();
        $eSMunicipiosSelecionados.select2({ width: '100%', data: selecionados });
        if (abrangencia.length > 0)
            $eSMunicipiosSelecionados.val(abrangencia).trigger("change");
    }
});

$('#btnLimparAbrangencia').on('click', function () {
    LimparAbrangencia();
});

//function IncluirSelecionado(text, val) {
//    $.each(selecionados, function (i) {
//        if (this.id == val) {
//            return false;
//        }
//    });

//    var newOption = new Option(text, val, false, false);

//    selecionados.push({ id: newOption.value, text: newOption.text });

//    $eSMunicipiosSelecionados.append(newOption).trigger('change');

//    abrangencia = [];

//    $.each($('#municipiosselecionados option'), function () {
//        abrangencia.push($(this).val());
//    });

//    $eSMunicipiosSelecionados.val(abrangencia).trigger("change");
//}

function IncluirSelecionado(text, val) {
    $.each(selecionados, function (i) {
        if (this.id == val) {
            return false;
        }
    });

    $('#grupomunicipios').append('<div class="checkbox municipio" id="' + val + '"><label><input type="checkbox" chec               ked name="municipiosselecionados" value="' + val + '" /> ' + text + '</label></div>');    
}

function JaExiste(idSelecionado) {
    var exist = false;

    //Finalizar amanha....

    $("#grupomunicipios").find($("#municipiosajax option[value=]"));
    $.each(selecionados, function (i) {
        if (this.id == idSelecionado) {
            exist = true;
        }
    });

    return exist;
}

function LimparAbrangencia() {
    abrangencia = [];
    selecionados = [];
    $eSMunicipiosSelecionados.empty();
}


/*Cria checkbox a partir da seleção de municípos*/
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

$('form').on('ifUnchecked', 'input[name="municipiosselecionados"]', function (event) {
    $('#' + this.value).remove();
});