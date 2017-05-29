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

    constructor(protected _notifier: NotifierService, private _http: Http, private _router: Router) {
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
        var vm = this;
        request.onreadystatechange = function () {
            if ((request.readyState == 4)) {
                if (request.status < 400) {
                    callback(JSON.parse(request.response));
                } else {
                    vm.handleError(request);
                }
                
            }
        };

        request.open("GET", this._apiLocation + url, true);
        request.send();
/*
        this._http.get(this._apiLocation + url, options == null ? this._options : options )
            .map((response: Response) => {
                callback(response.text() === '' ? '' : response.json());
            })
            .catch(this.handleError.bind(this)).subscribe();
*/
    }

    public doPost(url: String, body: any, callback: any, options?: RequestOptions, errorHandler?: any) {
        this._http.post(this._apiLocation + url, body, options == null ? this._options : options )
            .map((response: Response) => {
                callback(response.text() === '' ? '' : response.json());
            })
            .catch(errorHandler != null ? errorHandler() : this.handleError.bind(this)).subscribe();
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

    private handleError(err: any) {
        if (err.status === 401) {
            this.unauthorised();
        } else if (err.status === 403) {
            this.forbidden();
        } else if (err.status === 400) {

            // Bad request (Session timeout)
            this.unauthorised();
        } else {
            this.notifyError('Opnieuw inloggen verplicht');

            console.log(err)
            this.unauthorised();
        }

        return Observable.throw(err);
    }

    private unauthorised(): Observable<any> {
        this.notifyError('Opnieuw inloggen verplicht');

        this._router.navigate(['login']);
        return null;
    }

    private forbidden(): Observable<any> {
        this.notifyError('Opnieuw inloggen verplicht');
        this._router.navigate(['/']);
        return null;
    }

    private notifyError(message: string) {
        var now = new Date().getTime();
        var previousDate = eval('window._previousErrorTimestamp');

        // Only show notification if time in between is less than 200ms
        if (previousDate == undefined || now - previousDate < 200) {
            this._notifier.notify('error', message);
        }
        eval("window._previousErrorTimestamp = '" + now + "';");
    }
}
