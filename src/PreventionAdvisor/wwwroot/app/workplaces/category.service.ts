import { Injectable } from '@angular/core';

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { CommonService } from '../shared/commonService';

import { Category } from './category';

@Injectable()
export class CategoryService  extends CommonService {

    private _url = 'api/category';

    getCategories(callback: any){
        this.doGet(this._url, callback);
    }

    getCategory(id: string, callback:any) {
        this.doGet(this._url + '/' + id, callback);
    }

    addCategory(category: Category, callback: any) {
        this.doPost(this._url, category, callback);
    }

    updateCategory(category: Category, callback: any) {
        this.doPut(this._url, category, callback);
    }

}
