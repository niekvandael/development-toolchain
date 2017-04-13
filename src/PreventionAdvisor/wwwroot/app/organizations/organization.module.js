"use strict";
var __decorate = (this && this.__decorate) || function (decorators, target, key, desc) {
    var c = arguments.length, r = c < 3 ? target : desc === null ? desc = Object.getOwnPropertyDescriptor(target, key) : desc, d;
    if (typeof Reflect === "object" && typeof Reflect.decorate === "function") r = Reflect.decorate(decorators, target, key, desc);
    else for (var i = decorators.length - 1; i >= 0; i--) if (d = decorators[i]) r = (c < 3 ? d(r) : c > 3 ? d(target, key, r) : d(target, key)) || r;
    return c > 3 && r && Object.defineProperty(target, key, r), r;
};
Object.defineProperty(exports, "__esModule", { value: true });
var core_1 = require("@angular/core");
var router_1 = require("@angular/router");
var organization_list_component_1 = require("./organization-list.component");
var organization_service_1 = require("./organization.service");
var shared_module_1 = require("../shared/shared.module");
var OrganizationModule = (function () {
    function OrganizationModule() {
    }
    return OrganizationModule;
}());
OrganizationModule = __decorate([
    core_1.NgModule({
        imports: [
            shared_module_1.SharedModule,
            router_1.RouterModule.forChild([
                { path: 'Organizations', component: organization_list_component_1.OrganizationListComponent },
            ])
        ],
        declarations: [
            organization_list_component_1.OrganizationListComponent,
        ],
        providers: [
            organization_service_1.OrganizationService,
        ]
    })
], OrganizationModule);
exports.OrganizationModule = OrganizationModule;
//# sourceMappingURL=organization.module.js.map