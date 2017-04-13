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
var organization_service_1 = require("./organization.service");
var OrganizationListComponent = (function () {
    function OrganizationListComponent(_organizationService) {
        this._organizationService = _organizationService;
    }
    OrganizationListComponent.prototype.ngOnInit = function () {
        var _this = this;
        this._organizationService.getOrganizations()
            .subscribe(function (organizations) { return _this.organizations = organizations; }, function (error) { return _this.errorMessage = error; });
    };
    return OrganizationListComponent;
}());
OrganizationListComponent = __decorate([
    core_1.Component({
        templateUrl: 'app/organizations/organization-list.component.html',
        styleUrls: ['app/organizations/organization-list.component.css']
    }),
    __metadata("design:paramtypes", [organization_service_1.OrganizationService])
], OrganizationListComponent);
exports.OrganizationListComponent = OrganizationListComponent;
//# sourceMappingURL=organization-list.component.js.map