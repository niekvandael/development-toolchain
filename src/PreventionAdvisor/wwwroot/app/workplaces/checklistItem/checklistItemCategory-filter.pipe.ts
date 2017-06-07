import { Category } from './../category';
import {  PipeTransform, Pipe } from '@angular/core';
import { ChecklistItem } from '../checklistItem';

@Pipe({
    name: 'checkListItemCategoryFilter'
})
export class ChecklistItemCategoryFilterPipe implements PipeTransform {

    transform(value: ChecklistItem[], filterBy: Category): ChecklistItem[] {
        filterBy = filterBy ? filterBy : null;
        return filterBy ? value.filter((checklistItem: ChecklistItem) =>
            checklistItem.category.id === filterBy.id
        ) : value;
    }
}
