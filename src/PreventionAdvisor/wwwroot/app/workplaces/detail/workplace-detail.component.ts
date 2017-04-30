import { ChecklistItem } from './../checklistItem';
import { Category } from './../category';
import { Component, OnInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';

import { Workplace } from '../workplace';

import { WorkplaceService } from '../workplace.service';

import { Subscription } from 'rxjs/Subscription';

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
    selectedItem: ChecklistItem = new ChecklistItem();
    private selectedItemCopy: ChecklistItem;

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

    setWorkplace(workplace: Workplace) {
        this.workplace = workplace;
        this.calculateCompletion();
        this.findCategories();
    }

    calculateCompletion() {
        for (let checklistItem of this.workplace.checklistItems) {
            if (checklistItem.status === 1) {
                this.completedItems++;
            }
        }
    }

    findCategories() {
        for (let checklistItem of this.workplace.checklistItems) {
            if (!this.categories.filter(x => x.id === checklistItem.categoryId).length) {
                this.categories.push(checklistItem.category);
            }
        }
    }

    editChecklistItem(checkListItem: ChecklistItem) {
        this.selectedItem = checkListItem;
        this.selectedItemCopy = Object.assign({}, this.selectedItem);
    }

    resetChecklistItem(checkListItem: ChecklistItem) {
        for (var prop in this.selectedItemCopy) {
            this.selectedItem[prop] = this.selectedItemCopy[prop];
        }
    }

    saveItem() {
        this._workplaceService.updateWorkplace(this.workplace, this.saveItemCallback.bind(this));
    }

    saveItemCallback(){
        // nothing to do here...
    }
}