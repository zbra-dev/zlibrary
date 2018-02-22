import { Component, ViewContainerRef } from '@angular/core';

@Component({
    selector: 'zli-root',
    templateUrl: './app.component.html'
})
export class AppComponent {
    // ViewContainerRef is necessary for dynamic component creation (e.g.: ToastComponent)
    constructor(public viewContainerRef: ViewContainerRef) {
    }
}
