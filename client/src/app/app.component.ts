import { Component, ViewContainerRef } from '@angular/core';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'root',
    templateUrl: './app.component.html'
})
export class AppComponent {
    // ViewContainerRef is necessary for dynamic component creation (e.g.: ToastComponent)
    constructor(public viewContainerRef: ViewContainerRef, private translate: TranslateService) {
        translate.addLangs(["pt-br"]);

        translate.setDefaultLang("pt-br");
    }
}
