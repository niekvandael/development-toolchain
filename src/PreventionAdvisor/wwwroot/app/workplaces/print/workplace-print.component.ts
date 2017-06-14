import { ChecklistItem } from './../checklistItem';
import { Category } from './../category';
import { Component, AfterViewInit, OnDestroy } from '@angular/core';
import { Router, ActivatedRoute } from '@angular/router';
import { Location } from '@angular/common';
import { NotifierService } from 'angular-notifier';
import { Workplace } from '../workplace';
import { WorkplaceService } from '../workplace.service';
import { Subscription } from 'rxjs/Subscription';

@Component({
    templateUrl: 'app/workplaces/print/workplace-print.component.html',
    styleUrls: ['app/workplaces/print/workplace-print.component.css']
})
export class WorkplacePrintComponent implements AfterViewInit {
    workplace: Workplace = new Workplace();
    today : Date = new Date();
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
    }

    printPage(){
        window.print();
    }
}

enum addItemsModalNavOptions {
    ChecklistItem = 1,
    Category = 2
}