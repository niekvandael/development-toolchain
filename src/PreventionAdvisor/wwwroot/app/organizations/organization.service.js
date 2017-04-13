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
var router_1 = require("@angular/router");
var Observable_1 = require("rxjs/Observable");
require("rxjs/add/operator/do");
require("rxjs/add/operator/catch");
require("rxjs/add/operator/map");
require("rxjs/add/observable/throw");
var common_component_1 = require("../shared/common.component");
var OrganizationService = (function () {
    function OrganizationService(_http, _router) {
        this._http = _http;
        this._router = _router;
        this._organizationsUrl = '';
        this._organizationUrl = '';
        this._commonComponent = new common_component_1.CommonComponent();
        this._apiLocation = this._commonComponent.getAPILocation();
        this._organizationsUrl = this._apiLocation + 'api/Organization';
        this._organizationUrl = this._apiLocation + 'api/Organizations/';
        this._options = new http_1.RequestOptions({
            headers: new http_1.Headers({
                'Content-Type': 'application/json'
            }),
            withCredentials: true
        });
    }
    OrganizationService.prototype.getOrganizations = function () {
        var _this = this;
        return this._http.get(this._organizationsUrl, this._options)
            .map(function (response) { return response.json(); })
            .catch(function (error) {
            return _this.handleError(error);
        });
    };
    OrganizationService.prototype.getOrganization = function (id) {
        return this._http.get(this._organizationUrl + id, this._options)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    OrganizationService.prototype.handleError = function (err) {
        if (err.status === 401) {
            return this.unauthorised();
        }
        else if (err.status === 403) {
            return this.forbidden();
        }
        else {
            return Observable_1.Observable.throw(err);
        }
    };
    OrganizationService.prototype.unauthorised = function () {
        this._router.navigate(['login']);
        return null;
    };
    OrganizationService.prototype.forbidden = function () {
        this._router.navigate(['/']);
        return null;
    };
    return OrganizationService;
}());
OrganizationService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [typeof (_a = typeof http_1.Http !== "undefined" && http_1.Http) === "function" && _a || Object, typeof (_b = typeof router_1.Router !== "undefined" && router_1.Router) === "function" && _b || Object])
], OrganizationService);
exports.OrganizationService = OrganizationService;
var _a, _b;
//# sourceMappingURL=organization.service.js.map