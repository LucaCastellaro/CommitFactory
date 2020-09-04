import { Component, Input, EventEmitter } from '@angular/core';
import { Task, GitAction } from '../shared/models/models.export';
import { NewCommitFormService } from './new-commit-form.service';
import { HttpResponse } from '@angular/common/http';
import { Constants } from '../shared/shared.constants';

@Component({
    selector: 'app-new-commit-form',
    templateUrl: 'new-commit-form.component.html'
})

export class NewCommitFormComponent {
    @Input() newTaskAdded: EventEmitter<string>;
    public isPanelExpanded: boolean = true;
    public isBranch: boolean = false;
    public category: string = '';
    public description: string = '';
    public gitAction: GitAction;

    public gitActionDatasource: GitAction[] = [];

    constructor(
        private newCommitFormService: NewCommitFormService
    ) {
        this.gitActionDatasource = Object.assign([], this.newCommitFormService.GitActions);
        this.gitAction = Object.assign({}, this.gitActionDatasource[0]);
    }

    public TogglePanelExpansion(): void {
        this.isPanelExpanded = !this.isPanelExpanded;
    }

    public SelectGitAction(event: any): void {
        const selectedGitAction: string = event.target.value;
        this.gitAction = this.gitActionDatasource
            .find(action => action.Description === selectedGitAction || action.Icon === selectedGitAction);
    }

    public AddTask(): void {
        if (!this.category)
            alert('Inserire una categoria.');
        else if (!this.description)
            alert('Inserire una descrizione.');
        else if (!this.gitAction)
            alert('Scegliere una git action.');
        else {
            const taskToAdd: Task = {
                Category: this.category,
                Description: this.description,
                GitAction: this.gitAction,
                IsBranch: this.isBranch,
                Timestamp: new Date()
            };

            this.newCommitFormService.AddTask(taskToAdd)
                .subscribe((data: boolean) => {
                    if (!data)
                        alert('Impossibile aggiungere il task.');
                    else {
                        this.newTaskAdded.emit();
                    }
                });
        }
    }

    public Reset(): void {
        this.category = '';
        this.description = '';
        this.isBranch = false;
    }
}