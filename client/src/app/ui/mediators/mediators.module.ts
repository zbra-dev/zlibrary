import {NgModule} from '@angular/core';
import {LoaderMediator} from './loader.mediator';
import {ToastMediator} from './toast.mediator';

@NgModule({
    providers: [
        LoaderMediator,
        ToastMediator
    ]
})
export class MediatorsModule {
}
