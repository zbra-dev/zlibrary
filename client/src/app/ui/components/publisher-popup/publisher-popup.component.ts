import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, Validators, FormControl, AbstractControl } from '@angular/forms';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { Publisher } from '../../../model/publisher';
import { PublisherService } from '../../../service/publisher.service';
import { PublisherValidator } from '../../validators/publisher-validator';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'zli-publisher-popup',
    templateUrl: './publisher-popup.component.html',
    styleUrls: ['./publisher-popup.component.scss']
})

export class PublisherPopupComponent implements OnInit {

    public publisher = new Publisher(null, null);
    public publisherForm: FormGroup;
    isNew = false;
    isBusy = false;

    @Output()
    cancelEvent = new EventEmitter();

    constructor(private publisherService: PublisherService, private loaderMediator: LoaderMediator, private toastMediator: ToastMediator, private translate: TranslateService) {
        this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
        this.publisherForm = new FormGroup({
            nameControl: new FormControl(this.publisher.name, Validators.compose([
                Validators.required,
                PublisherValidator.validateEmptyString(),
                PublisherValidator.validateTypeahead()
            ]))
        });
    }

    ngOnInit(): void {
    }

    public get nameControl(): AbstractControl {
        return this.publisherForm.get('nameControl');
    }

    public get isNewPublisher(): boolean {
        return this.isNew;
    }

    public get isEmpty(): boolean {
        return this.isInvalid
            && !!this.nameControl.errors;
    }

    public get isInvalid(): boolean {
        return this.nameControl.invalid
            && (this.nameControl.dirty || this.nameControl.touched);
    }


    public initNewPublisher() {
        this.isNew = true;
        this.publisherForm.reset();
    }

    public savePublisher() {
        this.loaderMediator.execute(
            this.publisherService.save(this.publisher).subscribe(
                publisher => {
                    this.initNewPublisher();
                    this.publisherForm.reset();
                    this.toastMediator.show(this.translate.instant('PUBLISHERS.ADD'));
                    this.onCancel();
                }, error => {
                    this.toastMediator.show(error);
                }
            )
        );
    }

    public onCancel(): void {
        this.cancelEvent.emit(null);
    }

}