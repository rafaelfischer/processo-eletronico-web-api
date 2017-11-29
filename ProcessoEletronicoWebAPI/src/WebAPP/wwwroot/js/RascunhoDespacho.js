/*Incialização da página*/
$(document).ready(function () {
    $('#form-dados-basicos select').select2({ width: '100%' });
});

//CARREGAR UNIDADES POR ORGANIZACAO
$("#form-dados-basicos").on("select2:select", "#GuidOrganizacaoDestino", function () {

    var formData = new FormData();
    var orgao = $(this).val();
    var url = "/Suporte/GetUnidadesPorOrganizacao";

    //$.ajax(
    //    {
    //        url: url,
    //        data: { guidOrganizacao: orgao },
    //        processData: false,
    //        contentType: false,
    //        type: "GET",
    //        success: function (data) {
    //            $('#GuidUnidadeDestino').select2({ width: '100%', data: data })
    //        }
    //    }
    //);

    $.get(url, { guidOrganizacao: orgao })
        .done(function (data) {
            if (data.length > 0) {                
                data.unshift({ id: '0', text: 'Selecione uma unidade' });
                $('#GuidUnidadeDestino').select2({ width: '100%', data: data })
            }            
        });
});