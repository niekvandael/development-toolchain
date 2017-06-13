import { ChecklistItem } from './checklistItem';
import { IOrganization } from './../organizations/organization';
import { IAddress } from './../address/address';
import { Category } from "./category";

export class Workplace {
    id: string = '00000000-0000-0000-0000-000000000000';
    organization: IOrganization = new IOrganization();
    projectNumber: string = '';
    address: IAddress = new IAddress();
    title: string = '';
    projectLead: string = '';
    projectController: string = '';
    description: string = '';
    categories: Category[] = [];
}
