import {  PipeTransform, Pipe } from '@angular/core';
import { ChecklistItem } from '../checklistItem';

@Pipe({
    name: 'checkListItemFilter'
})
export class ChecklistItemFilterPipe implements PipeTransform {

    transform(value: ChecklistItem[], filterBy: string): ChecklistItem[] {
        filterBy = filterBy ? filterBy.toLocaleLowerCase() : null;
        return filterBy ? value.filter((checklistItem: ChecklistItem) =>
            checklistItem.title.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
            ('' + checklistItem.status).indexOf(filterBy) !== -1
        ) : value;
    }
}
