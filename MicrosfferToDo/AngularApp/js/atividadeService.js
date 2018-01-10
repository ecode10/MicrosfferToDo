(function (app) {

    var atividadeService = function ($http) {

        var getAtividades = function () {
            return $http.get("/api/atividadestodo");
        };

        var atualizar = function (id, nome) {
            var _dados = { "NomeTodo": nome, "CompletouTodo": "0", "IdTodo": id };
            return $http.put("/api/atividadestodo/" + id, _dados);
        };

        var inserir = function (ativid) {
            var _dados = { "NomeTodo": ativid.NomeTodo, "CompletouTodo": "0" };
            return $http.post("/api/atividadestodo", _dados);
        };

        var deleta = function (ativid) {
            return $http.delete("/api/atividadestodo/" + ativid.IdTodo);
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