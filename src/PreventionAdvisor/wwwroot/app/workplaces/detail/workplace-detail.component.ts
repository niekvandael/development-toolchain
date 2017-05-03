import { ChecklistItem } from './../checklistItem';
import { Category } from './../category';
import { Component, OnInit, OnDestroy } from '@angular/core';
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
export class WorkplaceDetailComponent implements OnInit {
    workplace: Workplace = new Workplace();
    listFilter: string;
    errorMessage: string;
    mode: string;
    completedItems: number = 0;
    filteredCategories: Category[] = [];
    categories: Category[] = [];

    selectedItem: ChecklistItem = new ChecklistItem();
    selecteditemsModalItem: number = addItemsModalNavOptions.ChecklistItem;
    newCategory: Category = new Category();
    newChecklistItem: ChecklistItem = new ChecklistItem(this.workplace.id);

    private selectedItemCopy: ChecklistItem;

    private workplaceSub: Subscription;

    constructor(private _notifier: NotifierService, private _workplaceService: WorkplaceService, private _categoryService: CategoryService, private _checklistItemService: ChecklistItemService, private _route: ActivatedRoute, private _router: Router, private _location: Location) {

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
        this.getCategories();
    }

    setWorkplace(workplace: Workplace): void {
        this.workplace = workplace;
        this.calculateCompletion();
        this.findFilteredCategories();
    }

    calculateCompletion(): void {
        this.completedItems = 0;
        for (let checklistItem of this.workplace.checklistItems) {
            if (checklistItem.status === 1 || checklistItem.status === 2) {
                this.completedItems++;
            }
        }
    }

    findFilteredCategories(): void {
        for (let checklistItem of this.workplace.checklistItems) {
            if (!this.filteredCategories.filter(x => x.id === checklistItem.categoryId).length) {
                this.filteredCategories.push(checklistItem.category);
            }
        }
    }

    editChecklistItem(checkListItem: ChecklistItem): void {
        this.selectedItem = checkListItem;
        this.selectedItemCopy = Object.assign({}, this.selectedItem);
    }

    resetChecklistItem(checkListItem: ChecklistItem) {
        for (var prop in this.selectedItemCopy) {
            this.selectedItem[prop] = this.selectedItemCopy[prop];
        }
    }

    saveItem(): void {
        this._checklistItemService.updateChecklistItem(this.selectedItem, this.saveItemCallback.bind(this));
    }

    saveItemCallback(): void {
        this.calculateCompletion();
    }

    onSubmitAddItemsModal(): void {
        if (this.selecteditemsModalItem === addItemsModalNavOptions.ChecklistItem) {
            this._checklistItemService.addChecklistItem(this.newChecklistItem, this.addChecklistItemCallback.bind(this));
        } else if (this.selecteditemsModalItem === addItemsModalNavOptions.Category) {
            this._categoryService.addCategory(this.newCategory, this.addCategoryCallback.bind(this));
        }
    }

    addChecklistItemCallback(): void {
        this.resetAddItemsModal(addItemsModalNavOptions.ChecklistItem);
        this.getWorkplace(this.workplace.id);
        this._notifier.notify('success', 'Werkpunt toegevoegd');
    }

    addCategoryCallback(): void {
        this.newCategory = new Category();
        this.getCategories();
        this.resetAddItemsModal(addItemsModalNavOptions.Category);
        this._notifier.notify('success', 'Categorie toegevoegd');

    }

    getCategories(): void {
        this._categoryService.getCategories(this.getCategoriesCallback.bind(this));
    }

    getCategoriesCallback(categories: Category[]): void {
        this.categories = categories;
    }

    resetAddItemsModal(defaultModalNavSelection: addItemsModalNavOptions = addItemsModalNavOptions.ChecklistItem): void {
        this.newCategory = new Category();
        this.newChecklistItem = new ChecklistItem(this.workplace.id);
        this.selecteditemsModalItem = defaultModalNavSelection;
    }
}

enum addItemsModalNavOptions {
    ChecklistItem = 1,
    Category = 2
}