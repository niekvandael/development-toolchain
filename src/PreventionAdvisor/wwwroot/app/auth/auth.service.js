"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
var __metadata = (this && this.__metadata) || function (k, v) {
    if (typeof Reflect === "object" && typeof Reflect.metadata === "function") return Reflect.metadata(k, v);
};
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var Observable_1 = require("rxjs/Observable");
require("rxjs/add/operator/do");
require("rxjs/add/operator/catch");
require("rxjs/add/operator/map");
require("rxjs/add/observable/throw");
var common_component_1 = require("../shared/common.component");
var AuthService = (function () {
    function AuthService(_http) {
        this._http = _http;
        this._commonComponent = new common_component_1.CommonComponent();
        this._apiLocation = this._commonComponent.getAPILocation();
        this._loginUrl = this._apiLocation + '/api/Login';
        this._logoutUrl = this._apiLocation + '/api/Logout';
        this._reportUrl = this._apiLocation + '/api/Report'; // TODO DELETE
        this._options = new http_1.RequestOptions({
            headers: new http_1.Headers({
                'Content-Type': 'application/json'
            }),
            withCredentials: true
        });
    }
    AuthService.prototype.login = function (user) {
        var options = new http_1.RequestOptions({
            headers: new http_1.Headers({
                'Content-Type': 'application/x-www-form-urlencoded'
            }),
            withCredentials: true,
        });
        return this._http.post(this._loginUrl, "Username=" + user.username + "&Password=" + user.password, options)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    AuthService.prototype.logout = function () {
        return this._http.post(this._logoutUrl, null, this._options)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    AuthService.prototype.downloadReport = function () {
        return this._http.post(this._reportUrl, null, this._options)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    AuthService.prototype.handleError = function (error) {
        console.error(error);
        return Observable_1.Observable.throw(error.json().error || 'Server error');
    };
    return AuthService;
}());
AuthService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], AuthService);
exports.AuthService = AuthService;
//# sourceMappingURL=auth.service.js.map