import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { Publisher } from '../../../model/publisher';
import { PublisherService } from '../../../service/publisher.service';
import { PublisherValidator } from '../../validators/publisher-validator';

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

    constructor(private publisherService: PublisherService, private loaderMediator: LoaderMediator, private toastMediator: ToastMediator) {
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

    get nameControl() {
        return this.publisherForm.get('nameControl');
    }

    public initNewPublisher() {
        this.isNew = true;
    }

    public savePublisher() {
        this.loaderMediator.execute(
            this.publisherService.save(this.publisher).subscribe(
                publisher => {
                    this.initNewPublisher();
                    this.publisherForm.reset();
                    this.toastMediator.show("Editora adicionada com sucesso.");
                    this.onCancel();
                }, error => {
                    this.toastMediator.show("Editora já existe.");
                }
            )
        );
    }

    public onCancel(): void {
        this.cancelEvent.emit(null);
    }

}