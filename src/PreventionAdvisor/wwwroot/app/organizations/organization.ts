import { IAddress } from './../address/address';

export class IOrganization {
    id: string = '00000000-0000-0000-0000-000000000000';
    name: string = '';
    vat: string = '';
    website: string = '';
    phone: string = '';
    address: IAddress = new IAddress();
}