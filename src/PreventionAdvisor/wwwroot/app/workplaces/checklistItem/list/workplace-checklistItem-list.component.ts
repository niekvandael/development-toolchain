import { ChecklistItem } from './../../checklistItem';
import { Category } from './../../category';
import { Component, AfterViewInit, OnDestroy, Input, Output, EventEmitter} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { NotifierService } from 'angular-notifier';

import { Workplace } from '../../workplace';

import { WorkplaceService } from '../../workplace.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'checkListItemList',
    templateUrl: 'app/workplaces/checklistItem/list/workplace-checklistItem-list.component.html',
    styleUrls: ['app/workplaces/checklistItem/list/workplace-checklistItem-list.component.css']
})
export class CheckListItemListComponent implements AfterViewInit {
    private listFilter: string;
    private errorMessage: string;
    private mode: string;
    private completedItems: number = 0;
    private selectedCategoryId: string;
    private selectedItem: ChecklistItem = new ChecklistItem();
    private selecteditemsModalItem: number = addItemsModalNavOptions.ChecklistItem;
    private newCategory: Category = new Category();
    private newChecklistItem: ChecklistItem = new ChecklistItem();
    private selectedItemCopy: ChecklistItem;
    public workplace: Workplace;

    constructor(private _notifier: NotifierService, private _route: ActivatedRoute, private _router: Router, private _location: Location, protected _workplaceService: WorkplaceService) {
    };

    @Input('workplace')
    set _setWorkplace(workplace: Workplace) {
        if(workplace != undefined){
            this.setWorkplace(workplace);
        }
        
    }

    public setWorkplace(workplace:Workplace){
        if(workplace === undefined){
            return;
        }
        
        this.workplace = workplace;
    }

    @Output('updateWorkplace') 
    updateWorkplace = new EventEmitter<Workplace>();

    @Output('requestDataRefresh') 
    requestDataRefresh = new EventEmitter<string>();

    ngAfterViewInit(): void {
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
        this._workplaceService.updateWorkplace(this.workplace, this.saveItemCallback.bind(this));
    }

    saveItemCallback(): void {
        this.updateWorkplace.emit(this.workplace);
    }

    onSubmitAddItemsModal(): void {
        if (this.selecteditemsModalItem === addItemsModalNavOptions.ChecklistItem) {
            let category = this.findCategoryById(this.selectedCategoryId);
            category.checklistItems.push(this.newChecklistItem);
            this._workplaceService.updateWorkplace(this.workplace, this.addChecklistItemCallback.bind(this));

        } else if (this.selecteditemsModalItem === addItemsModalNavOptions.Category) {
            this.workplace.categories.push(this.newCategory);
            this._workplaceService.updateWorkplace(this.workplace, this.addCategoryCallback.bind(this));
            this.newCategory = new Category();
        }
    }

    addChecklistItemCallback(workplace: Workplace): void {
        this.setWorkplace(workplace);

        this.resetAddItemsModal(addItemsModalNavOptions.ChecklistItem);
        this._notifier.notify('success', 'Werkpunt toegevoegd');
        
        this.requestDataRefresh.emit(this.workplace.id);
    }

    addCategoryCallback(workplace: Workplace): void {
        this.setWorkplace(workplace);

        this.newCategory = new Category();
        this.resetAddItemsModal(addItemsModalNavOptions.Category);
        this._notifier.notify('success', 'Categorie toegevoegd');
    }

    resetAddItemsModal(defaultModalNavSelection: addItemsModalNavOptions = addItemsModalNavOptions.ChecklistItem): void {
        this.newCategory = new Category();
        this.selecteditemsModalItem = defaultModalNavSelection;
    }

    updateCategory(category: Category, newValue: string){
        category.title = newValue;
        this._workplaceService.updateWorkplace(this.workplace, this.updateCategoryCallback.bind(this));
    }

    updateCategoryCallback(category: Category){
        this._notifier.notify('success', 'Categorie aangepast');
    }

    findCategoryById(id: string){
        for (let category of this.workplace.categories) {
           if(category.id === id){
               return category;
           }
        }
    }
}

enum addItemsModalNavOptions {
    ChecklistItem = 1,
    Category = 2
}