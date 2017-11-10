import {ApplicationRef, ComponentFactoryResolver, ComponentRef, Injectable} from '@angular/core';
import {ToastComponent} from './toast.component';

@Injectable()
export class Toast {
    private currentNotification: ComponentRef<ToastComponent>;

    constructor(private applicationRef: ApplicationRef,
                private componentFactoryResolver: ComponentFactoryResolver) {
    }

    public show(message: string): void {
        if (!!this.currentNotification) {
            this.currentNotification.destroy();
            this.currentNotification = null;
        }

        const componentFactory = this.componentFactoryResolver.resolveComponentFactory(ToastComponent);
        const viewContainerRef = this.applicationRef.components[0].instance.viewContainerRef;

        viewContainerRef.clear();

        this.currentNotification = viewContainerRef.createComponent(componentFactory) as ComponentRef<ToastComponent>;
        this.currentNotification.instance.message = message;
        this.currentNotification.instance.onDismiss.subscribe(() => {
            this.currentNotification.destroy();
        });
    }
}
