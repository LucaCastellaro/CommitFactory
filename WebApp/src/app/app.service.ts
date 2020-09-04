import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Constants } from './shared/shared.constants';

@Injectable()
export class AppService {
    constructor(
        private http: HttpClient
    ) { }
    
    public DeleteOldTasks(): Observable<any> {
        return this.http.delete<any>(Constants.mongoApiUrl + 'DeleteOldTasks');
    }
}