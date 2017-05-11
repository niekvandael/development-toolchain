import { Injectable } from '@angular/core';
import { Http, Response, RequestOptions, Headers } from '@angular/http';
import { Router } from '@angular/router';

import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/map';
import 'rxjs/add/observable/throw';
import { NotifierService } from 'angular-notifier';

import { CommonComponent } from '../shared/common.component';

@Injectable()
export class CommonService {
    private _commonComponent: CommonComponent;
    private _apiLocation: string;
    private _options: RequestOptions;

    constructor(private _notifier: NotifierService, private _http: Http, private _router: Router) {
        this._commonComponent = new CommonComponent();
        this._apiLocation = this._commonComponent.getAPILocation();

        this._options = new RequestOptions({
            headers: new Headers({
                'Content-Type': 'application/json'
            }),
            withCredentials: true
        });
    }

    public doGet(url: string, callback: any, options?: RequestOptions) {
        var request = new XMLHttpRequest();
        request.onreadystatechange = function () {
            if ((request.readyState == 4) && (request.status < 400)) {
                callback(request.responseXML);
            }
        };

        request.open("GET", url, true);
        request.send();
/*
        this._http.get(this._apiLocation + url, options == null ? this._options : options )
            .map((response: Response) => {
                callback(response.text() === '' ? '' : response.json());
            })
            .catch(this.handleError.bind(this)).subscribe();
*/
    }

    public doPost(url: String, body: any, callback: any, options?: RequestOptions) {
        this._http.post(this._apiLocation + url, body, options == null ? this._options : options )
            .map((response: Response) => {
                callback(response.text() === '' ? '' : response.json());
            })
            .catch(this.handleError.bind(this)).subscribe();
    }

    public doPut(url: String, body: any, callback: any, options?: RequestOptions) {
        this._http.put(this._apiLocation + url, body, options == null ? this._options : options )
            .map((response: Response) => {
                callback(response.text() === '' ? '' : response.json());
            })
            .catch(this.handleError.bind(this)).subscribe();
    }

    public doDelete(url: String, callback: any, options?: RequestOptions) {
        this._http.delete(this._apiLocation + url, options == null ? this._options : options )
            .map((response: Response) => {
                callback(response.text() === '' ? '' : response.json());
            })
            .catch(this.handleError.bind(this)).subscribe();
    }

    private handleError(err: Response) {
        if (err.status === 401) {
            this.unauthorised();
        } else if (err.status === 403) {
            this.forbidden();
        } else if (err.status === 400) {

            // Bad request (Session timeout)
            this.unauthorised();
        } else {
            this._notifier.notify('error', 'Er ging iets mis, probeer het aub opnieuw');

            console.log(err)
            this.unauthorised();
        }

        return Observable.throw(err);
    }

    private unauthorised(): Observable<any> {
        this._notifier.notify('error', 'Opnieuw inloggen verplicht');

        this._router.navigate(['login']);
        return null;
    }

    private forbidden(): Observable<any> {
        this._notifier.notify('error', 'Opnieuw inloggen verplicht');

        this._router.navigate(['/']);
        return null;
    }
}
