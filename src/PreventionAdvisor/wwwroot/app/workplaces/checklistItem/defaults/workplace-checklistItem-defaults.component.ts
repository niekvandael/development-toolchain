import { CheckListItemListComponent } from './../list/workplace-checklistItem-list.component';
import { ChecklistItem } from './../../checklistItem';
import { Category } from './../../category';
import { Component, AfterViewInit, OnDestroy, Input, Output, EventEmitter} from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { NotifierService } from 'angular-notifier';

import { Workplace } from '../../workplace';

import { WorkplaceService } from '../../workplace.service';
import { ChecklistItemService } from '../../checklistItem.service';

import { Subscription } from 'rxjs/Subscription';

@Component({
    templateUrl: 'app/workplaces/checklistItem/defaults/workplace-checklistItem-defaults.component.html',
    styleUrls: ['app/workplaces/checklistItem/defaults/workplace-checklistItem-defaults.component.css']
})

export class CheckListItemDefaultsComponent extends CheckListItemListComponent {
    
    constructor(private _workplaceService: WorkplaceService, _notifier: NotifierService, _checklistItemService: ChecklistItemService, _route: ActivatedRoute, _router: Router, _location: Location) {
        super(_notifier, _checklistItemService, _route, _router, _location);

        _workplaceService.getDefaultWorkplace(this.getDefaultWorkplaceCallback.bind(this));
    };

    getDefaultWorkplaceCallback(workplace: Workplace): void {
        this.setWorkplace(workplace);
    }

    getWorkplace(id: string) {
        this._workplaceService.getWorkplace(id, this.setWorkplace.bind(this));
    }

    setWorkplace(workplace: Workplace){
        this.workplace = workplace;
    }
}