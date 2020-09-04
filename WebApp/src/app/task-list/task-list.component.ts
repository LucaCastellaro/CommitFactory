import { Component, OnInit, Input, EventEmitter } from '@angular/core';
import { TaskListService } from './task-list.service';
import { Task } from '../shared/models/task.model';
import { GitAction } from '../shared/models/models.export';
import { ArrayExtension } from '../shared/extensions/array.extension';

@Component({
    selector: 'app-task-list',
    templateUrl: 'task-list.component.html'
})

export class TaskListComponent implements OnInit {

    @Input() newTaskAdded: EventEmitter<string>;

    public allTasks: ArrayExtension<Task> = new ArrayExtension<Task>();
    public thereAreMoreTasks: boolean = true;
    public isLoading: boolean = false;

    constructor(
        private taskListService: TaskListService
    ) { }

    ngOnInit() {
        this.GetMoreTasks();
        this.newTaskAdded.subscribe(() => this.Refresh());
    }



    public GetMoreTasks(): void {
        this.isLoading = true;
        this.taskListService.GetLimitedNumberOfTasks(this.allTasks.length)
            .subscribe((data: ArrayExtension<Task>) => {
                this.ThereAreMoreTasks(data);
                if (!data.isEmpty) {
                    const newTasks = this.ApiResultToTask(data);
                    
                    this.allTasks = <ArrayExtension<Task>>([...this.allTasks, ...newTasks]);
                    this.isLoading = false;
                }
            });
    }

    private ThereAreMoreTasks(data: ArrayExtension<Task>): void {
        this.thereAreMoreTasks = !data.isEmpty && data.length > 9;
    }

    private ApiResultToTask(data: ArrayExtension<Task>): Array<Task> {
        return data.map((tmpTask: any) => <Task>{
            Category: (tmpTask.isBranch ? `[${tmpTask.category}]` : `${tmpTask.category}:`),
            Description: tmpTask.description,
            GitAction: <GitAction>{
                Description: tmpTask.gitAction.description,
                Icon: tmpTask.gitAction.icon
            },
            IsBranch: tmpTask.isBranch,
            Timestamp: tmpTask.timestamp
        });
    }



    public Refresh(): void {
        this.isLoading = true;
        this.allTasks.length = 0;
        this.taskListService.GetLimitedNumberOfTasks()
            .subscribe((data: ArrayExtension<Task>) => {
                this.ThereAreMoreTasks(data);
                if (!data.isEmpty) {
                    const newTasks = this.ApiResultToTask(data);

                    this.allTasks = <ArrayExtension<Task>>newTasks;
                    this.isLoading = false;
                }
            });
    }



    public DeleteTask(task: Task): void {
        if (confirm('Sei sicuro di voler eliminare questo commit?') == false)
            return;

        this.taskListService.DeleteTask(task)
            .subscribe((data: Boolean) => {
                if (data){
                    // this.allTasks = Object.assign([], this.allTasks.filter(i => i.Timestamp !== task.Timestamp));
                    this.Refresh();
                }
                else
                    alert('Impossibile eliminare questo commit.');
            })

    }
    
}