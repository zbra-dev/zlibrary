import { Component, Input, ViewEncapsulation } from '@angular/core';

@Component({
    selector: 'zli-loading-overlay',
    templateUrl: './loading-overlay.component.html',
    styleUrls: ['./loading-overlay.component.scss'],
    encapsulation: ViewEncapsulation.Emulated
})
export class LoadingOverlayComponent {
    @Input()
    public isBusy: boolean;
}
