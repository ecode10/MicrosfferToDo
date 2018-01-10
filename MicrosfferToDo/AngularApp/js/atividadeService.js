(function (app) {

    var atividadeService = function ($http) {

        var getAtividades = function (id) {
            return $http.get("/api/atividadestodo/status/" + id, {
                headers: { "token": "561d1cc4-c7b5-431e-94a7-e0c2ed9a8d2c", "pwd": "micr@$ffer.T@D@" }
            });
        };

        var atualizar = function (id, nome, status) {
            var _dados = { "NomeTodo": nome, "CompletoTodo": status, "IdTodo": id };
            return $http.put("/api/atividadestodo/" + id, _dados, {
                headers: { "token": "561d1cc4-c7b5-431e-94a7-e0c2ed9a8d2c", "pwd": "micr@$ffer.T@D@" }
            });
        };

        var inserir = function (ativid) {
            var _dados = { "NomeTodo": ativid.NomeTodo, "CompletoTodo": "0" };
            return $http.post("/api/atividadestodo", _dados, {
                headers: { "token": "561d1cc4-c7b5-431e-94a7-e0c2ed9a8d2c", "pwd": "micr@$ffer.T@D@" }
            });
        };

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