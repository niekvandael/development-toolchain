import { Component, OnInit }  from '@angular/core';

import { Workplace } from '../workplace';
import { WorkplaceService } from '../workplace.service';

@Component({
    templateUrl: 'app/workplaces/list/workplace-list.component.html',
    styleUrls: ['app/workplaces/list/workplace-list.component.css']
})
export class WorkplaceListComponent implements OnInit {
    listFilter: string;
    errorMessage: string;

    workplaces: Workplace[];

    constructor(private _workplaceService: WorkplaceService) {

    }

    ngOnInit(): void {
        this._workplaceService.getWorkplaces(this.setWorkplaces.bind(this));
    }

    private setWorkplaces(workplaces: Workplace[] ){
        this.workplaces = workplaces;
    }

}
