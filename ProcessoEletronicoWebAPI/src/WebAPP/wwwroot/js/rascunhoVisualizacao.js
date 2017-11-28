//CARREGAR RASCUNHO DE PROCESSO PARA VISUALIZAÇÃO
$("body").on("click", ".btnVisualizarRascunho", function () {

    var formData = new FormData();
    var idRascunho = $(this).attr('data-id');
    var url = "/Rascunho/Visualizar";
    formData.append("id", idRascunho);

    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#visualizarRascunho").html(data);
                $('#tab-visualizar').trigger('click')
            }
        }
    );
});

//ENVIA IDRASCUNHO PARA AUTUAR PROCESSO
$("body").on("click", ".btnAutuarRascunho", function () {

    var formData = new FormData();
    var idRascunho = $(this).attr('data-id');
    var url = "/Rascunho/AutuarProcessoPorIdRascunho";
    formData.append("id", idRascunho);

    $.ajax(
        {
            url: url,
            data: formData,
            processData: false,
            contentType: false,
            type: "POST",
            success: function (data) {
                $("#visualizarRascunho").html(data);
                $('#tab-visualizar').trigger('click')
            }
        }
    );
});