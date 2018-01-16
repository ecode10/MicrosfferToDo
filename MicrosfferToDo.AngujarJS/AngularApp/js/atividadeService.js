(function (app) {

    /**
     * @description Essa classe chama o endereço do web api criado em C# passando os parâmetros necessários
     * @author Mauricio Junior
     * @param {any} $http
     */

    var atividadeService = function ($http) {

        //web api que busca as atividades pelo status
        //é necessário passar o token e password
        var getAtividades = function (id) {
            return $http.get("/api/atividadestodo/status/" + id, {
                headers: { "token": "561d1cc4-c7b5-431e-94a7-e0c2ed9a8d2c", "pwd": "micr@$ffer.T@D@" }
            });
        };

        //web api que atualiza os dados
        //é necessário passar o token e password
        var atualizar = function (id, nome, status) {
            var dados = { "NomeTodo": nome, "CompletoTodo": status, "IdTodo": id };
            return $http.put("/api/atividadestodo/" + id, dados, {
                headers: { "token": "561d1cc4-c7b5-431e-94a7-e0c2ed9a8d2c", "pwd": "micr@$ffer.T@D@" }
            });
        };

        //web api que insere os dados
        //é necessário passar o token e password
        var inserir = function (ativid) {
            var dados = { "NomeTodo": ativid.NomeTodo, "CompletoTodo": "0" };
            return $http.post("/api/atividadestodo", dados, {
                headers: { "token": "561d1cc4-c7b5-431e-94a7-e0c2ed9a8d2c", "pwd": "micr@$ffer.T@D@" }
            });
        };

        //web api que deleta os dados
        //é necessário passar o token e password
        var deleta = function (ativid) {
            return $http.delete("/api/atividadestodo/" + ativid.IdTodo, {
                headers: { "token": "561d1cc4-c7b5-431e-94a7-e0c2ed9a8d2c", "pwd": "micr@$ffer.T@D@" }
            });
        };

        return {
            getAtividades: getAtividades,
            atualizar: atualizar,
            inserir: inserir,
            deleta: deleta
        };
    };

    app.factory("atividadeService", atividadeService);

}(angular.module("atividadesModulo")))