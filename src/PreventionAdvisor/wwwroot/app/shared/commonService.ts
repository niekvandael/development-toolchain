import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';

import { CommonComponent } from '../shared/common.component';

@Injectable()
export class CommonService {
    private _commonComponent: CommonComponent;
    private _apiLocation: string;
    private _options: RequestOptions;


    constructor(private _http: Http) {
        this._commonComponent = new CommonComponent();
        this._apiLocation = this._commonComponent.getAPILocation();

        this._options = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            withCredentials: true
        });
    }

    public doGet(url: String, callback: any, options?: RequestOptions) {
        this._http.get(this._apiLocation + url, options == null ? this._options : options )
            .map((response: Response) => {
                callback(response.text() === '' ? '' : response.json());
            })
            .catch(this.handleError).subscribe();
    }

    public doPost(url: String, body: any, callback: any, options?: RequestOptions) {
        this._http.post(this._apiLocation + url, body, options == null ? this._options : options )
            .map((response: Response) => {
                callback(response.text() === '' ? '' : response.json());
            })
            .catch(this.handleError).subscribe();
    }

    public doPut(url: String, body: any, callback: any, options?: RequestOptions) {
        this._http.put(this._apiLocation + url, body, options == null ? this._options : options )
            .map((response: Response) => {
                callback(response.text() === '' ? '' : response.json());
            })
            .catch(this.handleError).subscribe();
    }

    public doDelete(url: String, callback: any, options?: RequestOptions) {
        this._http.delete(this._apiLocation + url, options == null ? this._options : options )
            .map((response: Response) => {
                callback(response.text() === '' ? '' : response.json());
            })
            .catch(this.handleError).subscribe();
    }

    private handleError(err: Response) {
        console.log('Error while calling: ' + err);
        return Observable.throw(err);
    }

}
