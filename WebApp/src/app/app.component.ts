import { Component, EventEmitter } from '@angular/core';
import { AppService } from './app.service';

@Component({
  selector: 'app-root',
  templateUrl: './app.component.html',
  styleUrls: ['./app.component.css']
})
export class AppComponent {
  public newTaskAdded: EventEmitter<string> = new EventEmitter();

  constructor(
    // private appService: AppService
  ){
    // this.appService.DeleteOldTasks();
    // setTimeout(() => {
    //   return;
    // }, 1000);
  }
}
