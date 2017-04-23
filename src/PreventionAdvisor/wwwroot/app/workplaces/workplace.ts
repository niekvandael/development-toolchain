import { IOrganization } from './../organizations/organization';
import { IAddress } from './../address/address';

export class Workplace {
    id: string = '00000000-0000-0000-0000-000000000000';
    organization: IOrganization = new IOrganization();
    projectnumber: string = '';
    address: IAddress = new IAddress();
    title: string = '';
    projectlead: string = '';
    projectcontroller: string = '';
    description: string = '';
}
