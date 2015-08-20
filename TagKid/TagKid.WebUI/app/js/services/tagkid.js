app.service("tagkid", ["$http", "err", "clientData", "dialogService", function ($http, err, clientData, dialogService) {
        var send = function (method, controller, action, data, success, error, complete) {
            var token = clientData.token();

            var opts = {
                method: method,
                url: "/api/" + controller + "/" + action,
                headers: {
                    'tagkid-auth-token': token || ""
                }
            };

            if (method === "GET" || method === "DELETE") {
                opts.params = data;
            } else {
                opts.data = data;
            }

            return $http(opts)
                .success(function (resp, status, headers, config) {
                    if (resp.ResponseCode === 0 && success) {
                        success(resp);
                    } else if (resp.ResponseCode !== 0) {
                        // No login / Token expired
                        if (resp.ResponseCode === err.Auth_LoginRequired || resp.ResponseCode === err.Auth_LoginTokenExpired) {
                            dialogService.openAuthDialog(function () {
                                send(method, controller, action, data, success, error, complete);
                            });

                            return;
                        }

                        if (error) {
                            error(resp);
                        } else {
                            alert(resp.ResponseCode + ": " + resp.ResponseMessage);
                        }
                    }
                })
                .error(function (resp, status, headers, config) {
                    if (error) {
                        error({
                            ResponseCode: err.UnknownError,
                            ResponseMessage: "Ooops! Something terribly went wrong :("
                        });
                    }
                })
                .finally(function () {
                    if (complete) {
                        complete();
                    }
                });
        };

        this.post = function (controller, action, data, success, error, complete) {
            return send("POST", controller, action, data, success, error, complete);
        };

        this.put = function (controller, action, data, success, error, complete) {
            return send("PUT", controller, action, data, success, error, complete);
        };

        this.get = function (controller, action, data, success, error, complete) {
            return send("GET", controller, action, data, success, error, complete);
        };

        this.delete = function (controller, action, data, success, error, complete) {
            return send("DELETE", controller, action, data, success, error, complete);
        };
    }
]);