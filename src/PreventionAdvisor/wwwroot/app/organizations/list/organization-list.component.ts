import { Component, OnInit }  from '@angular/core';

import { IOrganization } from '../organization';
import { OrganizationService } from '../organization.service';

@Component({
    templateUrl: 'app/organizations/list/organization-list.component.html',
    styleUrls: ['app/organizations/list/organization-list.component.css']
})
export class OrganizationListComponent implements OnInit {
    listFilter: string;
    errorMessage: string;

    organizations: IOrganization[];

    constructor(private _organizationService: OrganizationService) {

    }

    ngOnInit(): void {
        this._organizationService.getOrganizations(this.setOrganizations.bind(this));
    }

    private setOrganizations(organizations: IOrganization[] ){
        this.organizations = organizations;
    }

}
