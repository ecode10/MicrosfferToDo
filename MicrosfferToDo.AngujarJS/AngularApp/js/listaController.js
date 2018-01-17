(function (app) {

    var listaController = function ($scope, atividadeService)
    {
        var limparBotao;
        var removerAtividadePorId;
        var carregaDados;
        var carregaDadosRealizados;

        //pega os dados do web api e atualiza as atividades não realizadas
        atividadeService.getAtividades(0).then(function (response) {
            $scope.atividadestodo = response.data;

            limparBotao();
            
        });

        //pega os dados do web api e atualiza as atividades realizadas
        atividadeService.getAtividades(1).then(function (response) {
            $scope.atividadestodoRealizado = response.data;
        });
        
        //método que deleta o dado usando web api - atividades não realizadas
        //acionado no click do botao delete
        $scope.deleta = function (ativ) {
            atividadeService.deleta(ativ).then(function () {
                removerAtividadePorId(ativ.IdTodo);

                //muda os dados dos botoes e campos
                limparBotao();
            });
        };

        //atualiza o status das atividades realizadas e não realizadas
        //acionada no click da imagem
        $scope.atualizaStatus = function (ativ, status) {
            atividadeService.atualizar(ativ.IdTodo, ativ.NomeTodo, status).then(function () {
                carregaDados();
                carregaDadosRealizados();
            });
        };

        //salva ou edita os dados
        //acionado no click do botão salvar
        $scope.salvar = function (ativ) {
            //pega os valores dos campos
            var nome = $scope.atividadestodo.NomeTodo;
            var id = $scope.atividadestodo.IdTodo;

            //alert($scope.atividadestodo.BtnSalvar);
            if ($scope.atividadestodo.BtnSalvar === "Salvar alterações") {

                //atualiza com a web api
                atividadeService.atualizar(id, nome, 0).then(function () {

                    //carrega os dados da atividade não realizada
                    carregaDados();

                    alert("Atividade alterada com sucesso.");
                });

            } else { //insere usando web api

                //verifica se os dados foram digitados
                if (nome.trim().length > 0) {

                    //insere na web api
                    atividadeService.inserir(ativ).then(function () {
                       carregaDados();
                    });
                } else {
                    //exibe mensagem ao usuário
                    alert("Por favor, digite a atividade.");
                }
            }
        };

        //salva ou edita os dados
        //acionado no click do botão salvar
        $scope.limpar = function () {

            limparBotao();
        };

        //coloca os dados nos campos para edição
        //acionado no click do edit dentro da tabela (table)
        $scope.editarDados = function (ativ) {

            $scope.atividadestodo.NomeTodo = ativ.NomeTodo;
            $scope.atividadestodo.IdTodo = ativ.IdTodo;
            $scope.atividadestodo.BtnSalvar = "Salvar alterações";

            document.getElementById("nomeTodo").focus();
        };

        limparBotao = function () {

            $scope.atividadestodo.NomeTodo = "";
            $scope.atividadestodo.IdTodo = "";
            $scope.atividadestodo.BtnSalvar = "Salvar";

            //document.getElementById("nomeTodo").focus();
        };

        //método privado utilizado para apagar ao invés de atualizar a página com o webapi
        removerAtividadePorId = function (id) {
            for (var i = 0; i < $scope.atividadestodo.length; i++) {
                if ($scope.atividadestodo[i].IdTodo === id) {
                    $scope.atividadestodo.splice(i, 1);
                    break;
                }
            }
        };

        //funcao privada que carrega os dados da atividade não relizada
        carregaDados = function () {
            atividadeService.getAtividades(0).then(function (response) {
                $scope.atividadestodo = response.data;

                limparBotao();
            });
        };

        //funcao privada que carrega os dados da atividade realizada
        carregaDadosRealizados = function () {
            atividadeService.getAtividades(1).then(function (response) {
                $scope.atividadestodoRealizado = response.data;

                limparBotao();
            });
        };
    };

    app.controller("listaController", listaController);

}(angular.module("atividadesModulo")))