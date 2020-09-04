import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { HttpClientModule } from '@angular/common/http'

import { SharedModule } from './shared/shared.module';

import { AppComponent } from './app.component';
import { NewCommitFormComponent } from './new-commit-form/new-commit-form.component';
import { TaskListComponent } from './task-list/task-list.component';

import { NewCommitFormService } from './new-commit-form/new-commit-form.service';
import { TaskListService } from './task-list/task-list.service';
import { AppService } from './app.service';

@NgModule({
  declarations: [
    AppComponent,
    NewCommitFormComponent,
    TaskListComponent
  ],
  imports: [
    BrowserModule,
    FormsModule,
    HttpClientModule,
    SharedModule
  ],
  providers: [
    NewCommitFormService,
    TaskListService,
    AppService
  ],
  bootstrap: [
    AppComponent
  ]
})
export class AppModule { }
