$(document).ready(function () {
    ResetSinalizacoesLista();
    ResetMunicipioLista();
});

var $eSUf = $("#ufajax");
var $eSMunicipio = $("#municipiosajax");

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

var abrangencia = [];
var objetoSelecionado = {};
var selecionados = [];
var index = 0;

$eSUf.on('change', function (e) {
    $.get(
        "/Suporte/GetMunicipiosPorUF?uf=" + $eSUf.val(), function (data) {
            $eSMunicipio.empty();
            if(data.length > 0){
                data.unshift({ id: 'all', text: 'Todos' });
                data.unshift({ id: '-1', text: 'Selecione um Município' });            
                $eSMunicipio.select2({ width: '100%', data: data });
            }
        });
});

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

$('form').on('ifUnchecked', 'input[type="checkbox"]', function (event) {    
    $('#' + this.value).remove();
});

function IncluirSelecionado(text, val) {    
    $('#grupomunicipios').append('<div class="checkbox municipio col-lg-3 col-md-4 col-xs-6" id="' + val + '"><label><input type="checkbox" checked name="MunicipiosRascunhoProcesso[' + index + '].GuidMunicipio" value="' + val + '" /> ' + text + '</label></div>');
    index++;
}

function JaExiste(idSelecionado) {
    var exist = false;
    if ($('#' + idSelecionado).length > 0)
        exist = true;
    return exist;
}

function LimparAbrangencia() {
    $("#grupomunicipios div.municipio").remove();
    $('form#formabrangencia')[0].reset();
    $eSMunicipio.val(-1).trigger("change");
    $eSUf.val(0).trigger("change");
    index = 0;
}

function LimparFormBasico() {    
    $('#Atividade_Id').val($('#Atividade_Id option:first').val()).trigger('change')
    
    $('#GuidUnidade').index[0].trigger("change");
    $('form#formbasico')[0].reset();    
}

function ResetMunicipioLista() {
    $('#formabrangencia input').iCheck({
        checkboxClass: 'icheckbox_square-red',
        radioClass: 'iradio_square-red',
        increaseArea: '5%' // optional
    });
}

function ResetSinalizacoesLista() {
    $('#formbasico input').iCheck({
        checkboxClass: 'icheckbox_square-red',
        radioClass: 'iradio_square-red',
        increaseArea: '5%' // optional
    });
}

function uploadFiles(inputId) {
    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);
        formData.append("GuidOrganizacao", $("#formanexo").find("#GuidOrganizacao").val());
        formData.append("Id", $("#formanexo").find("#Id").val());
    }

    //startUpdatingProgressIndicator();
    $.ajax(
        {
            url: "/rascunho/UploadAnexo",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#grupoanexos").html(data)
            }
        }
    );
}

function uploadFiles(inputId) {
    var input = document.getElementById(inputId);
    var files = input.files;
    var formData = new FormData();

    if (files.length == 0)
        return false;

    for (var i = 0; i != files.length; i++) {
        formData.append("files", files[i]);        
        formData.append("idRascunho", $("#formanexo").find("#Id").val());
    }

    $('#textLoad').toggleClass('disabled');
    $('#textLoad').text('Carregando...');
    $('#progressAnexo').toggleClass('hide');

    $.ajax(
        {
            url: "/rascunho/UploadAnexo",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {                     
                $("#textLoad").text('Selecionar arquivos');
                $('#textLoad').toggleClass('disabled');
                $("#progressAnexo").toggleClass('hide');
                $("#grupoanexos").html(data)
            },
            error: function (erro) {
                console.log(erro);
                $("#progressAnexo").toggleClass('hide');
            }
        }
    );
}

$("#grupoanexos").on("click", ".btnExcluirAnexo", function () {

    
    $(this).parent('div').find('.btn').toggleClass('disabled')

    var formData = new FormData();
    
    formData.append("idRascunho", $("#formanexo").find("#Id").val());
    formData.append("idAnexo", $(this).attr("data-id"));
    
    $.ajax(
        {
            url: "/rascunho/ExcluirAnexo",
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#grupoanexos").html(data)
            }
        }
    );
});

//CARREGAR ORGANIZACOES
$("#interessadoTipo").on("change", function () {

    var formData = new FormData();
    var tipoInteressado = $(this).val();
    var url = "";
    formData.append("tipoInteressado", tipoInteressado);
    
    if (tipoInteressado > 0) {
        url = "/rascunho/FormInteressado";
    } else {
        return false;
    }
    
    $.ajax(
        {
            url: url, 
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#formInteressado").html(data)
                $('#formInteressado select').select2({ width: '100%' })
            }
        }
    );
});

//CARREGAR UNIDADES POR ORGANIZACAO
$("#forminteressados").on("select2:select", "#interessadoOrgao", function () {

    var formData = new FormData();
    var orgao = $(this).val();
    var url = "/rascunho/GetUnidadesPorOrganizacao";
    formData.append("guidOrganizacao", orgao);    

    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#interessadoUnidade").html(data)
                $('#interessadoUnidade select').select2({ width: '100%' })
            }
        }
    );
});

//SALVAR INTERESSADO ORGANIZACAO/UNIDADE
$("#forminteressados").on("submit", function () {

    var tipoInteressado = $(this).val();
    var formData = new FormData();
    var url = "/rascunho/GetUnidadesPorOrganizacao";

    switch (tipoInteressado) {
        case 1:
            formData.append("guidOrganizacao", orgao);
            break;
        case 2:
            break;
        case 3:
            break;
        default:
            return false;
    }

    if ($("#interessadoOrgao").val() == "") {
        return false
    }

    

    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#interessadoUnidade").html(data)
                $('#interessadoUnidade select').select2({ width: '100%' })
            }
        }
    );
});

//btnEditarAnexo