/************************************************INTERESSADOS************************************************/
//CARREGAR ORGANIZACOES
$("#interessadoTipo").on("change", function () {

    var formData = new FormData();
    var tipoInteressado = $(this).val();
    var idRascunho = $('#formTipoInteressados').find('#Id').val();

    var url = "";
    formData.append("tipoInteressado", tipoInteressado);
    formData.append("idRascunho", idRascunho);

    if (tipoInteressado > 0) {
        url = "/RascunhoInteressado/FormInteressado";
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
                $("#formInteressado").html(data);
                $('#formInteressado select').select2({ width: '100%' });
            }
        }
    );
});

//CARREGAR UNIDADES POR ORGANIZACAO
$("#formInteressado").on("select2:select", "#interessadoOrgao", function () {

    var formData = new FormData();
    var orgao = $(this).val();
    var url = "/RascunhoInteressado/GetUnidadesPorOrganizacao";
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
$("body").on("click", ".btnCarregarOrganizacao", function () {

    if (isNullOrEmpty($("#interessadoOrgao").val())) {
        return false;
    }

    var idRascunho = $("#formTipoInteressados").find("#Id").val();
    var tipoInteressado = $("#interessadoTipo").val();
    var formData = new FormData();
    var url = "/RascunhoInteressado/IncluirInteressadoPJOrganograma";
    var guidOrganizacao = $("#interessadoOrgao").val();
    var guidUnidade = $("#interessadoOrgaoUnidade").val();

    formData.append("idRascunho", idRascunho);

    switch (tipoInteressado) {
        case "1":
            if (isNullOrEmpty(guidUnidade)) {
                formData.append("guidOrganizacao", guidOrganizacao);
            }
            else {
                formData.append("guidOrganizacao", guidOrganizacao);
                formData.append("guidUnidade", guidUnidade);
            }
            break;
        case "2":
            break;
        case "3":
            break;
        default:
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

    return false;
});

//EXCLUIR INTERESSADO PESSOA JURÍDICA
/*Evento click para o botão excluir da lista de interessados*/
$("#listainteressados").on("click", ".btn-excluir-interessado-pj", function () {

    //$(this).parent('div').find('.btn').toggleClass('disabled');
    var formData = new FormData();
    formData.append("idRascunho", $("#formanexo").find("#Id").val());
    formData.append("idAnexo", $(this).attr("data-id"));

    var id = $(this).attr("data-id");

    carregaModalDefault(
        "Excluir Anexo",
        "Deseja realmente excluir o interessado Pessoa Jurídica ID " + id + "?",
        "Excluir",
        "Cancelar",
        "btnConfirmarExclusaoInteressadoPJ",
        "btnCancelarExclusao",
        id);
});

/*confirmar Exclusão de anexo*/
$('body').on('click', '.btnConfirmarExclusaoInteressadoPJ', function () {

    var id = $(this).attr('data-acaoconfirmar');
    var url = "/RascunhoInteressado/ExcluirInteressadoPJ";
    var idRascunho = $("#formTipoInteressados").find("#Id").val();

    var formData = new FormData();
    formData.append("idRascunho", idRascunho);
    formData.append("idInteressadoPJ", id);

    if (isNullOrEmpty(id)) {
        return false;
    }
    else {

        $.ajax(
            {
                url: url,
                data: formData,
                processData: false,
                contentType: false,
                type: "POST",
                success: function (data) {
                    $("#listainteressados").html(data)
                }
            }
        );
    }
});

//EXCLUIR INTERESSADO PESSOA FISICA
/*Evento click para o botão excluir da lista de interessados*/
$("#listainteressados").on("click", ".btn-excluir-interessado-pf", function () {

    //$(this).parent('div').find('.btn').toggleClass('disabled');
    var formData = new FormData();
    formData.append("idRascunho", $("#formanexo").find("#Id").val());
    formData.append("idAnexo", $(this).attr("data-id"));

    var id = $(this).attr("data-id");

    carregaModalDefault(
        "Excluir Anexo",
        "Deseja realmente excluir o interessado Pessoa Física ID " + id + "?",
        "Excluir",
        "Cancelar",
        "btnConfirmarExclusaoInteressadoPF",
        "btnCancelarExclusao",
        id);
});

/*confirmar Exclusão de anexo*/
$('body').on('click', '.btnConfirmarExclusaoInteressadoPF', function () {

    var id = $(this).attr('data-acaoconfirmar');
    var url = "/RascunhoInteressado/ExcluirInteressadoPF";
    var idRascunho = $("#formTipoInteressados").find("#Id").val();

    var formData = new FormData();
    formData.append("idRascunho", idRascunho);
    formData.append("idInteressadoPF", id);

    if (isNullOrEmpty(id)) {
        return false;
    }
    else {

        $.ajax(
            {
                url: url,
                data: formData,
                processData: false,
                contentType: false,
                type: "POST",
                success: function (data) {
                    $("#listainteressados").html(data)
                }
            }
        );
    }
});



//$("#listainteressados").on("click", ".btn-excluir-interessado-pf", function () {

//    var idRascunho = $("#formTipoInteressados").find("#Id").val();
//    var formData = new FormData();
//    var url = "/RascunhoInteressado/ExcluirInteressadoPF";

//    formData.append("idRascunho", idRascunho);
//    formData.append("idInteressadoPF", $(this).attr('data-id'));

//    $.ajax(
//        {
//            url: url,
//            data: formData,
//            processData: false,
//            contentType: false,
//            type: "POST",
//            success: function (data) {
//                $("#listainteressados").html(data)
//                //$('#interessadoUnidade select').select2({ width: '100%' })
//            }
//        }
//    );

//    return false;
//});

//CARREGAR INTERESSADO PESSOA JURÍDICA PARA FORMULARIO DE EDICAO
$("#listainteressados").on("click", ".btn-editar-interessado-pj", function () {

    var idRascunho = $("#formTipoInteressados").find("#Id").val();
    var formData = new FormData();
    var url = "/RascunhoInteressado/FormInteressadoPJPreenchido";

    formData.append("idRascunho", idRascunho);
    formData.append("idInteressadoPJ", $(this).attr('data-id'));

    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#formInteressado").html(data)
                $('#forminteressados select').select2({ width: '100%' })
            }
        }
    );

    return false;
});

//CARREGAR INTERESSADO PESSOA FISICA PARA FORMULARIO DE EDICAO
$("#listainteressados").on("click", ".btn-editar-interessado-pf", function () {

    var idRascunho = $("#formTipoInteressados").find("#Id").val();
    var formData = new FormData();
    var url = "/RascunhoInteressado/FormInteressadoPFPreenchido";

    formData.append("idRascunho", idRascunho);
    formData.append("idInteressadoPF", $(this).attr('data-id'));

    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#formInteressado").html(data)
                $('#forminteressados select').select2({ width: '100%' })
            }
        }
    );

    return false;
});

//CARREGAR MUNICIPOS POR UF
$("#formInteressado").on("select2:select", "#uf", function () {
    $.get(
        "/Suporte/GetMunicipiosPorUF?uf=" + $(this).val(), function (data) {
            $('#GuidMunicipio').empty();
            if (data.length > 0) {                
                data.unshift({ id: '-1', text: 'Selecione um Município' });
                $('#GuidMunicipio').select2({ width: '100%', data: data });
            }
        });
});

//INCLUIR CAMPOS DE CONTATO DE INTERESSADO
$("#formInteressado").on("click", "#btnAddContato", function () {    
    var formContato = $('#contatos div.formContato:first').clone();
    formContato.appendTo('#contatos');
    AtualizaNomeCamposContato();
});

function AtualizaNomeCamposContato() {
    //$.each($('#contatos div.formContato input[type="text"]'), function (i) {
    //    $(this).attr('name', 'Contato[' + index + '].Telefone');
    //    $(this).parent().find('input[type="radio"]').attr('name', 'Contato[' + index + '].TipoContato');
    //}); 

    $.each($('#contatos div.formContato'), function (i) {
        $(this).find('input[type="text"]').attr('name', 'Contatos[' + i + '].Telefone');
        $(this).find('input[type="radio"]').attr('name', 'Contatos[' + i + '].TipoContato.id');
    }); 
}

$("#formInteressado").on("click", ".btnDelContato", function () {
    $(this).parents('.formContato').remove();
    AtualizaNomeCamposContato();
});

//INCLUIR CAMPO DE EMAIL DE INTERESSADO
$("#formInteressado").on("click", "#btnAddEmail", function () {    
    var formEmail = $('#emails div.formEmail:first').clone();    
    formEmail.appendTo('#emails');
    AtualizaNomeCamposEmail();
});

function AtualizaNomeCamposEmail() {
    $.each($('#emails div.formEmail input'), function (i) {
        $(this).attr('name', 'Emails[' + i + '].Endereco');
    });    
}

$("#formInteressado").on("click", ".btnDelEmail", function () {
    $(this).parents('.formEmail').remove();
    AtualizaNomeCamposEmail();
});


//Confirmação de Exclusao
function Confirm() {    
    alert("Foi!");
    $("#formPF input, #formPJ input").unmask();
}