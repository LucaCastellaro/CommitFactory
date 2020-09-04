import { NgModule } from '@angular/core';

import {
    ChevronUpComponent,
    ChevronDownComponent,
    PlusComponent,
    TrashComponent,
    ReloadComponent
} from './icons/icons.export';

import { ArrayExtension } from './extensions/array.extension';

const icons = [
    ChevronUpComponent,
    ChevronDownComponent,
    PlusComponent,
    TrashComponent,
    ReloadComponent
]

const components = [
    ...icons
];

@NgModule({
    imports: [],
    exports: components,
    declarations: components,
    providers: [],
})
export class SharedModule { }
