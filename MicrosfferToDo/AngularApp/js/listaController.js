(function (app) {

    var listaController = function ($scope, atividadeService)
    {
        //pega os dados do web api e atualiza as atividades não realizadas
        atividadeService.getAtividades(0).success(function (data) {
            $scope.atividadestodo = data;
        });

        //pega os dados do web api e atualiza as atividades realizadas
        atividadeService.getAtividades(1).success(function (data) {
            $scope.atividadestodoRealizado = data;
        });
        
        //método que deleta o dado usando web api - atividades não realizadas
        //acionado no click do botao delete
        $scope.deleta = function (ativ) {
            atividadeService.deleta(ativ).success(function () {
                removerAtividadePorId(ativ.IdTodo);
            });
        };

        //atualiza o status das atividades realizadas e não realizadas
        //acionada no click da imagem
        $scope.atualizaStatus = function (ativ, status) {
            atividadeService.atualizar(ativ.IdTodo, ativ.NomeTodo, status).success(function () {
                carregaDados();
                carregaDadosRealizados();
            });
        };

        //salva ou edita os dados
        //acionado no click do botão salvar
        $scope.salvar = function (ativ) {

            if (document.getElementById("btnSalvar").innerText == "Editar") {

                //pega os valores dos campos
                var _nome = document.getElementById("nomeTodo").value;
                var _id = document.getElementById("IdTodo").value;

                //atualiza com a web api
                atividadeService.atualizar(_id, _nome, 0).success(function (data) {

                    //carrega os dados da atividade não realizada
                    carregaDados();

                    //muda os dados dos botoes e campos
                    document.getElementById("btnSalvar").innerText = "Salvar";
                    document.getElementById("nomeTodo").value = "";
                    document.getElementById("IdTodo").value = "";
                });

            } else { //insere usando web api
                var _nome = document.getElementById("nomeTodo").value;

                //verifica se os dados foram digitados
                if (_nome.trim().length > 0) {

                    //insere na web api
                    atividadeService.inserir(ativ).success(function (data) {
                        carregaDados();
                    });
                } else {
                    //exibe mensagem ao usuário
                    alert("Por favor, digite a atividade.");
                }
            }
        };

        //coloca os dados nos campos para edição
        //acionado no click do edit dentro da tabela (table)
        $scope.editarDados = function (ativ) {
            document.getElementById("nomeTodo").value = ativ.NomeTodo;
            document.getElementById("IdTodo").value = ativ.IdTodo;

            document.getElementById("btnSalvar").innerText = "Editar";
        };

        //método privado utilizado para apagar ao invés de atualizar a página com o webapi
        var removerAtividadePorId = function (id) {
            for (var i = 0; i < $scope.atividadestodo.length; i++) {
                if ($scope.atividadestodo[i].IdTodo == id) {
                    $scope.atividadestodo.splice(i, 1);
                    break;
                }
            }
        };

        //funcao privada que carrega os dados da atividade não relizada
        var carregaDados = function () {
            atividadeService.getAtividades(0).success(function (data) {
                $scope.atividadestodo = data;
            });
        };

        //funcao privada que carrega os dados da atividade realizada
        var carregaDadosRealizados = function () {
            atividadeService.getAtividades(1).success(function (data) {
                $scope.atividadestodoRealizado = data;
            });
        };
        
    };

    app.controller('listaController', listaController);

}(angular.module("atividadesModulo")))