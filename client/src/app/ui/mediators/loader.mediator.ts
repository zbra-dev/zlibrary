import {EventEmitter, Injectable} from '@angular/core';
import {Router} from '@angular/router';
import {Observable} from 'rxjs/Observable';
import {Subscription} from 'rxjs/Subscription';

@Injectable()
export class LoaderMediator {
    private loadingCounter: number;
    private loadEmitter: EventEmitter<boolean>;

    constructor(private router: Router) {
        this.loadingCounter = 0;
        this.loadEmitter = new EventEmitter(true);
        this.router.events.subscribe(() => this.reset());
    }

    public get onLoadChanged(): Observable<boolean> {
        return this.loadEmitter.asObservable();
    }

    public execute(subscription: Subscription) {
        this.loadingCounter++;
        this.beginLoad();
        subscription.add(() => {
            this.finishAction();
        });
    }

    public get isLoading(): boolean {
        return this.loadingCounter > 0;
    }

    private finishAction() {
        this.loadingCounter--;
        if (!this.loadingCounter) {
            this.endLoad();
        }
    }

    private beginLoad() {
        this.loadEmitter.emit(true);
    }

    private endLoad() {
        this.loadEmitter.emit(false);
    }

    private reset() {
        this.loadingCounter = 0;
        this.endLoad();
    }
}
