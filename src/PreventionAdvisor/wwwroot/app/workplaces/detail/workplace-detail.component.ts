import { Category } from './../category';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Workplace } from '../workplace';
import { Category } from '../category';

import { WorkplaceService } from '../workplace.service';

import { Subscription }       from 'rxjs/Subscription';

@Component({
    templateUrl: 'app/workplaces/detail/workplace-detail.component.html',
    styleUrls: ['app/workplaces/detail/workplace-detail.component.css']
})
export class WorkplaceDetailComponent implements OnInit {
    workplace: Workplace = new Workplace();
    listFilter: string;
    errorMessage: string;
    mode: string;
    completedItems: number = 0;
    categories: Category[] = [];

    private workplaceSub: Subscription;

    constructor(private _workplaceService: WorkplaceService, private _route: ActivatedRoute, private _router: Router, private _location: Location) {

    };

   getWorkplace(id: string) {
        this._workplaceService.getWorkplace(id, this.setWorkplace.bind(this));
    }

    ngOnInit(): void {
        this.workplaceSub = this._route.params.subscribe(
            params => {
                let id = params['id'];
                this.getWorkplace(id);
        });

    }

    setWorkplace(workplace: Workplace){
        this.workplace = workplace;
        this.calculateCompletion();
        this.findCategories();
    }

    calculateCompletion(){
        for (let checklistItem of this.workplace.checklistItems) {
            if(checklistItem.status === 1)
            {
                this.completedItems++;
            }
        }
    }

    findCategories(){
        for (let checklistItem of this.workplace.checklistItems) {
           if(!this.categories.filter(x => x.id === checklistItem.categoryId).length)
           {
               this.categories.push(checklistItem.category);
           }
        }
    }

}