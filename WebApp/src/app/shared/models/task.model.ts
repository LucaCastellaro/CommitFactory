import { GitAction } from './git-action.model';

export interface Task {
    GitAction: GitAction;
    Category: string;
    Description : string;
    Timestamp: Date;
    IsBranch: boolean;
}