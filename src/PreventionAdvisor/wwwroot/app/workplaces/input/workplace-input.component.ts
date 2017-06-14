import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Workplace } from '../workplace';
import { WorkplaceService } from '../workplace.service';
import { OrganizationService } from '../../organizations/organization.service';
import { IOrganization } from '../../organizations/organization';

import { Subscription }       from 'rxjs/Subscription';

@Component({
    templateUrl: 'app/workplaces/input/workplace-input.component.html',
    styleUrls: ['app/workplaces/input/workplace-input.component.css']
})
export class WorkplaceInputComponent implements OnInit {
    workplace: Workplace = new Workplace();
    organizations: IOrganization[] = [];
    errorMessage: string;
    mode: string;

    private workplaceSub: Subscription;
    private organizationsSub: Subscription;

    constructor(private _workplaceService: WorkplaceService,private _organizationService: OrganizationService, private _route: ActivatedRoute, private _router: Router, private _location: Location) {

    };

    onSubmit(){
        if(this.mode === 'add')
        {
            this._workplaceService.addWorkplace(this.workplace, this.navigateToWorkplace.bind(this));
        }
        else
        {
            this._workplaceService.updateWorkplace(this.workplace, this.navigateToWorkplace.bind(this));
        }
    }

    navigateToWorkplace(workplace: Workplace){
         this._router.navigate(['/workplace/detail', workplace.id]);
    }

   getWorkplace(id: string) {
       if (id !== '00000000-0000-0000-0000-000000000000')
       {
            this.mode = 'update';
            this._workplaceService.getWorkplace(id, this.setWorkplace.bind(this));
       } 
       else
       {
           this.mode = 'add';
            this.workplace = new Workplace();
       }
    }

    getOrganizations() {
        this._organizationService.getOrganizations(this.setOrganizations.bind(this));
    }

    setOrganizations(organizations: IOrganization[]) {
        this.organizations = organizations;
    }

    ngOnInit(): void {
        this.workplaceSub = this._route.params.subscribe(
            params => {
                let id = params['id'];
                this.getWorkplace(id);
        });
        this.organizationsSub = this._route.params.subscribe(
            params => {
                let id = params['id'];
                this.getOrganizations();
        });
    }

    setWorkplace(workplace: Workplace){
        this.workplace = workplace;
    }

}