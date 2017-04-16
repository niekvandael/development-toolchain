import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { IOrganization } from '../organization';
import { OrganizationService } from '../organization.service';

import { Subscription }       from 'rxjs/Subscription';

@Component({
    templateUrl: 'app/organizations/input/organization-input.component.html',
    styleUrls: ['app/organizations/input/organization-input.component.css']
})
export class OrganizationInputComponent implements OnInit {
    organization: IOrganization = new IOrganization();
    errorMessage: string;
    mode: string;

    private sub: Subscription;

    constructor(private _organizationService: OrganizationService, private _route: ActivatedRoute, private _router: Router, private _location: Location) {

    };

    onSubmit(){
        if(this.mode === 'add')
        {
            this._organizationService.addOrganization(this.organization, this.navigateToList.bind(this));
        }
        else
        {
            this._organizationService.updateOrganization(this.organization, this.navigateToList.bind(this));
        }
    }

    navigateToList(){
         this._location.back();
//         this._router.navigate(['/organizations']);
    }
   getOrganization(id: string) {
       if (id !== '00000000-0000-0000-0000-000000000000')
       {
           this.mode = 'update';
            this._organizationService.getOrganization(id)
                .subscribe(organization => this.organization = organization, error => this.errorMessage = <any>error);
       } 
       else
       {
           this.mode = 'add';
            this.organization = new IOrganization();
       }
    }

    ngOnInit(): void {
        this.sub = this._route.params.subscribe(
            params => {
                let id = params['id'];
                this.getOrganization(id);
        });
    }
}