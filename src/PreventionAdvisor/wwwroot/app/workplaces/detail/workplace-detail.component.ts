import { ChecklistItem } from './../checklistItem';
import { Category } from './../category';
import { Component, AfterViewInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { NotifierService } from 'angular-notifier';

import { Workplace } from '../workplace';

import { WorkplaceService } from '../workplace.service';
import { ChecklistItemService } from '../checklistItem.service';
import { CategoryService } from '../category.service';

import { Subscription } from 'rxjs/Subscription';

@Component({
    templateUrl: 'app/workplaces/detail/workplace-detail.component.html',
    styleUrls: ['app/workplaces/detail/workplace-detail.component.css']
})
export class WorkplaceDetailComponent implements AfterViewInit {
    workplace: Workplace = new Workplace();
    listFilter: string;
    errorMessage: string;
    mode: string;
    completedItems: number = 0;

    private workplaceSub: Subscription;

    constructor(private _notifier: NotifierService, private _workplaceService: WorkplaceService, private _route: ActivatedRoute, private _router: Router, private _location: Location) {

    }; 

    getWorkplace(id: string) {
        this._workplaceService.getWorkplace(id, this.setWorkplace.bind(this));
    }

    ngAfterViewInit(): void {
        this.workplaceSub = this._route.params.subscribe(
            params => {
                let id = params['id'];
                this.getWorkplace(id);
            });
    }

    setWorkplace(workplace: Workplace): void {
        this.workplace = workplace;
        this.calculateCompletion();
    }

    calculateCompletion(): void {
        this.completedItems = 0;
        for (let checklistItem of this.workplace.checklistItems) {
            if (checklistItem.status === 1 || checklistItem.status === 2) {
                this.completedItems++;
            }
        }
    }
}