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
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var http_1 = require("@angular/http");
var Observable_1 = require("rxjs/Observable");
require("rxjs/add/operator/do");
require("rxjs/add/operator/catch");
require("rxjs/add/operator/map");
require("rxjs/add/observable/throw");
var common_component_1 = require("../shared/common.component");
var OrganizationService = (function () {
    function OrganizationService(_http) {
        this._http = _http;
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
        return this._http.get(this._organizationsUrl, this._options)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    OrganizationService.prototype.getOrganization = function (id) {
        return this._http.get(this._organizationUrl + id, this._options)
            .map(function (response) { return response.json(); })
            .catch(this.handleError);
    };
    OrganizationService.prototype.handleError = function (error) {
        // in a real world app, we may send the server to some remote logging infrastructure
        // instead of just logging it to the console
        console.error(error);
        return Observable_1.Observable.throw(error.json().error || 'Server error');
    };
    return OrganizationService;
}());
OrganizationService = __decorate([
    core_1.Injectable(),
    __metadata("design:paramtypes", [http_1.Http])
], OrganizationService);
exports.OrganizationService = OrganizationService;
//# sourceMappingURL=organization.service.js.map