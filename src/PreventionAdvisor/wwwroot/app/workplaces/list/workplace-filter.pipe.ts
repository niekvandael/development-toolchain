import {  PipeTransform, Pipe } from '@angular/core';
import { Workplace } from '../workplace';

@Pipe({
    name: 'workplaceFilter'
})
export class WorkplaceFilterPipe implements PipeTransform {

    transform(value: Workplace[], filterBy: string): Workplace[] {
        filterBy = filterBy ? filterBy.toLocaleLowerCase() : null;
        return filterBy ? value.filter((workplace: Workplace) =>
            workplace.title.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
            workplace.address.street.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
            workplace.address.city.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
            workplace.address.zipcode.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
            workplace.address.number.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
            workplace.organization.name.toLocaleLowerCase().indexOf(filterBy) !== -1
        ) : value;
    }
}
