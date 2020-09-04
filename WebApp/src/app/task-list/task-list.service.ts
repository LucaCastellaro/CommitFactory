import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Task } from '../shared/models/task.model';
import { HttpClient, HttpParams, HttpHeaders } from '@angular/common/http';
import { Constants } from '../shared/shared.constants';
import { TaskListConstants } from './task-list.constants';

@Injectable()
export class TaskListService {
    
    private readonly commonHeaders: HttpHeaders = new HttpHeaders().set('Content-Type', 'application/json');

    constructor(
        private http: HttpClient
    ) { }

    public GetLimitedNumberOfTasks(tasksToSkip: number = 0): Observable<Task[]> {
        const params = new HttpParams()
            .set('tasksToGet', TaskListConstants.TasksToGet.toString())
            .set('tasksToSkip', tasksToSkip.toString());

        return this.http.get<Task[]>(Constants.mongoApiUrl + 'GetLimitedNumberOfTasks', {
            params: params,
            headers: new HttpHeaders().set('Content-Type', 'application/json')
        });
    }

    public DeleteTask(task: Task): Observable<Boolean> {
        return this.http.delete<Boolean>(Constants.mongoApiUrl + 'DeleteTask', {
            params: new HttpParams().set('jsonTask', JSON.stringify(task)),
            headers: new HttpHeaders().set('Content-Type', 'application/json')
        });
    }

}