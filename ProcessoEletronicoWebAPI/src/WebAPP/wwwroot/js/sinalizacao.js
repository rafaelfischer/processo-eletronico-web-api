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

$('body').on('click', '.btnConfirmarExclusaoSinalizacao', function () {

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

function ResetFormSinalizacao() {
    $('#form-sinalizacao')[0].reset();
}
