import { Workplace } from '../workplaces/workplace';

export class Dashboard {
    incompleteWorkplaces: Workplace[] = [];
    reportsCount: number = 0;
    totalItems: number = 0;
    totalItemsOk: number = 0;
    totalItemsFail: number = 0;
    totalItemsNvt: number = 0;
    totalItemsNull: number = 0;
}
