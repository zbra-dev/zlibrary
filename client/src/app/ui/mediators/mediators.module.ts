import {NgModule} from '@angular/core';
import {LoaderMediator} from './loader.mediator';
import {ToastMediator} from './toast.mediator';
import { ConfirmMediator } from './confirm.mediator';

@NgModule({
    providers: [
        LoaderMediator,
        ToastMediator,
        ConfirmMediator
    ]
})
export class MediatorsModule {
}
