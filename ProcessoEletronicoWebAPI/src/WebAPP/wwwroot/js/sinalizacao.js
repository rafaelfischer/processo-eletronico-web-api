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
    
});

function ResetFormSinalizacao() {
    $('#form-sinalizacao')[0].reset();
}
