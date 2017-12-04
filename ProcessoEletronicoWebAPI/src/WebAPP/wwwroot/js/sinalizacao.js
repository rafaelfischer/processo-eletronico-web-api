$('body').on('click', '.btn-excluir-sinalizacao', function () {

    var id = $(this).attr("data-id");
    var descricao = $(this).attr("data-descricao");

    carregaModalDefault(
        "Excluir Sinalização",
        "Deseja realmente excluir a sinalização \"" + descricao + "\"?",
        "Excluir",
        "Cancelar",
        "btnConfirmarExclusaoSinalizacao",
        "btnCancelarExclusao",
        id);
});

$('body').on('click', 'button[data-btn="btnConfirmarExclusaoSinalizacao"]', function () {

    var id = $(this).attr("data-acaoconfirmar");
    var url = "/Sinalizacao/Delete";

    var formData = new FormData();
    formData.append("id", id);

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
                type: "DELETE",
                success: function (data) {
                    $("#lista-sinalizacoes").html(data)
                }
            }
        );
    }

});

$('body').on('click', '.btn-editar-sinalizacao', function () {
    var url = $(this).attr("href");
    var tr = $(this).parents("tr");
    
    if (isNullOrEmpty(url)) {
        return false;
    }
    else {

        $.ajax(
            {
                url: url,
                processData: false,
                contentType: false,
                type: "GET",
                success: function (data) {
                    tr.children('td').hide();
                    tr.append('<td colspan="3">' + data + '</td>')
                },
                error: function () {
                    toastr['success']('Sinalização não encontrada, favor recarregar a página para obter a lista atualizada.');
                }
                
            }
        );
    }

    return false;

});

$('#form-nova-sinalizacao').on('click', '.btn-cancelar-sinalizacao', function () {
    ResetFormSinalizacao();
    $('#div-form-nova-sinalizacao').removeClass('in');
});

$('#lista-sinalizacoes').on('click', '.btn-cancelar-sinalizacao', function () {
    $(this).parents('tr').children('td').toggle();
    $(this).parents('td').remove();
});


function ResetFormSinalizacao() {
    $('.box-body #form-nova-sinalizacao')[0].reset();
    $.each($('#form-nova-sinalizacao input'), function () {
        $(this).val('');
    });
}

$('body').on('click', '#btn-form-sinalizacao', function () {
    ResetFormSinalizacao();
});

function ExibirSinalizacaoAtualizada(data) {
    $(this).parents('tr').html(data.responseText);
}


