let tarefas = {
    variaveis:
    {
        url: "/Tarefas/Index_SelecionarFiltro",
        urlPrincipal: "/Tarefas/Index",
        urlEditar: "/Tarefas/Editar/",
        urlDelete: "/Tarefas/Delete/",
        botaoEditarHtml: null,
        botaoExcluirHtml: null
    },
    campos:
    {
        filtroNome: $("#filtroNome"),
        filtroCompleta: $("#filtroCompleta"),
        tarefa: {
        campoId: $("#campoId"),
        campoNome: $("#campoNome"),
        campoDescricao: $("#campoDescricao"),
        campoCompleta: $("#campoCompleta"),
        }

    },
    funcoes:
    {

        filtrar: () => {
            $('#table').DataTable({
                "paging": false,
                "lengthChange": false,
                "searching": false,
                "ordering": false,
                "info": false,
                "autoWidth": false,
                "destroy": true,
                "responsive": false,
                "ajax": {
                    "url": tarefas.variaveis.url,
                    "data": {
                        filtroNome: tarefas.campos.filtroNome.val(),
                        filtroCompleta: tarefas.campos.filtroCompleta.prop("checked")
                    },
                    "type": 'POST',
                    "dataSrc": "value"
                },
                "columns": [
                    { "data": "id" },
                    { "data": "nome" },
                    { "data": "descricao" },
                    {
                        "data": "completa",
                        "render": (data) => data ? "Sim" : "Não"
                    },
                    {
                        "data": "id",
                        "render": (data) => {
                            tarefas.variaveis.botaoEditarHtml = `<a href="${tarefas.variaveis.urlEditar}${data}">Editar<a/> `
                            tarefas.variaveis.botaoExcluirHtml = ` <a href="${tarefas.variaveis.urlDelete}${data}">Delete<a/>`

                            return tarefas.variaveis.botaoEditarHtml + tarefas.variaveis.botaoExcluirHtml
                        }
                    }
                ]
            });
        },
        Editar: () => {

            const campoIdValue = tarefas.campos.tarefa.campoId.val();
            const campoNomeValue = tarefas.campos.tarefa.campoNome.val();
            const campoDescricaoValue = tarefas.campos.tarefa.campoDescricao.val();
            const campoCompletaValue = tarefas.campos.tarefa.campoCompleta.val();

            const dadosTarefa = {
                Id: campoIdValue,
                Nome: campoNomeValue,
                Descricao: campoDescricaoValue,
                Completa: campoCompletaValue
            };

            $.ajax({
                method: "POST",
                url: tarefas.variaveis.urlEditar,
                data: {
                    tarefa: dadosTarefa
                },
                success: (data) => {
                    console.log(data)
                    window.location.href = tarefas.variaveis.urlPrincipal
                }
            });
        },
    }
}
$(document).ready(() => tarefas.funcoes.filtrar())