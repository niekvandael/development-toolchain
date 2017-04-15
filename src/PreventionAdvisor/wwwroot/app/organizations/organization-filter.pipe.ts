import {  PipeTransform, Pipe } from '@angular/core';
import { IOrganization } from './organization';

@Pipe({
    name: 'organizationFilter'
})
export class OrganizationFilterPipe implements PipeTransform {

    transform(value: IOrganization[], filterBy: string): IOrganization[] {
        filterBy = filterBy ? filterBy.toLocaleLowerCase() : null;
        return filterBy ? value.filter((org: IOrganization) =>
            org.name.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
            org.vat.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
            org.website.toLocaleLowerCase().indexOf(filterBy) !== -1 ||
            ('' + org.phone).toLocaleLowerCase().indexOf(filterBy) !== -1            ) : value;
    }
}
