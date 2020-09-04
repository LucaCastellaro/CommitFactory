import { Injectable } from '@angular/core';
import { HttpClient, HttpResponse } from '@angular/common/http';
import { Observable } from 'rxjs';

import { Constants } from '../shared/shared.constants';
import { Task, GitAction } from '../shared/models/models.export';

@Injectable()
export class NewCommitFormService {
    // private headers = new HttpHeaders().set('Content-Type', 'application/json');
    public readonly GitActions: GitAction[] = [
        { Description: 'Improve code structure / format', Icon: '🎨' }
        , { Description: 'Improve performance', Icon: '⚡️' }
        , { Description: 'Remove code or files', Icon: '🔥' }
        , { Description: 'Bugfix', Icon: '🐛' }
        , { Description: 'Hotfix', Icon: '🚑' }
        , { Description: 'New feature', Icon: '✨' }
        , { Description: 'Documentation', Icon: '📝' }
        , { Description: 'Deploy', Icon: '🚀' }
        , { Description: 'Edit UI / styles', Icon: '💄' }
        , { Description: 'Initial commit', Icon: '🎉' }
        , { Description: 'Add / update tests', Icon: '✅' }
        , { Description: 'Fix security issues', Icon: '🔒' }
        , { Description: 'Release / new version', Icon: '🔖' }
        , { Description: 'Remove warning', Icon: '🚨' }
        , { Description: 'WIP', Icon: '🚧' }
        , { Description: 'Fix CI build', Icon: '💚' }
        , { Description: 'Downgrade dependencies', Icon: '⬇️' }
        , { Description: 'Upgrade dependencies', Icon: '⬆️' }
        , { Description: 'Pin dependencies version', Icon: '📌' }
        , { Description: 'CI build version', Icon: '👷' }
        , { Description: 'Analytics or track code', Icon: '📈' }
        , { Description: 'Refactoring', Icon: '♻️' }
        , { Description: 'Add a dependecy', Icon: '➕' }
        , { Description: 'Remove a dependecy', Icon: '➖' }
        , { Description: 'Configuration file', Icon: '🔧' }
        , { Description: 'Build script', Icon: '🔨' }
        , { Description: 'Localization', Icon: '🌐' }
        , { Description: 'Fix typos', Icon: '✏️' }
        , { Description: 'Bad code to be improved', Icon: '💩' }
        , { Description: 'Revert', Icon: '⏪' }
        , { Description: 'Merge', Icon: '🔀' }
        , { Description: 'Package / library', Icon: '📦' }
        , { Description: 'External API change', Icon: '👽' }
        , { Description: 'Move / rename resources', Icon: '🚚' }
        , { Description: 'License', Icon: '📄' }
        , { Description: 'Breaking change', Icon: '💥' }
        , { Description: 'Asset', Icon: '🍱' }
        , { Description: 'Improve accessibility', Icon: '♿️' }
        , { Description: 'Comment', Icon: '💡' }
        , { Description: 'Write drunk code', Icon: '🍻' }
        , { Description: 'Text & literals', Icon: '💬' }
        , { Description: 'DB changes', Icon: '🗃' }
        , { Description: 'Add / update log', Icon: '🔊' }
        , { Description: 'Remove log', Icon: '🔇' }
        , { Description: 'Contributors', Icon: '👥' }
        , { Description: 'UX & usability', Icon: '🚸' }
        , { Description: 'Architectural changes', Icon: '🏗' }
        , { Description: 'Responsive design', Icon: '📱' }
        , { Description: 'Mock', Icon: '🤡' }
        , { Description: 'Easter egg', Icon: '🥚' }
        , { Description: '.gitignore', Icon: '🙈' }
        , { Description: 'Screenshot', Icon: '📸' }
        , { Description: 'Experiment new things', Icon: '⚗' }
        , { Description: 'Improve SEO', Icon: '🔍' }
        , { Description: 'Types (Flow, TypeScript)', Icon: '🏷️' }
        , { Description: 'Seed file', Icon: '🌱' }
        , { Description: 'Feature flag', Icon: '🚩' }
        , { Description: 'Catch exception', Icon: '🥅' }
        , { Description: 'Animations & transitions', Icon: '💫' }
        , { Description: 'Deprecated code to be cleaned', Icon: '🗑' }
    ].sort((a, b) => a.Description.localeCompare(b.Description));

    constructor(
        private http: HttpClient
    ) { }

    public AddTask(task: Task): Observable<boolean> {
        return this.http.post<boolean>(Constants.mongoApiUrl + 'AddTask', task);
    }
}