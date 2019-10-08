import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Author } from '../../../model/author';
import { AuthorValidator } from '../../validators/author-validator';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { AuthorService } from '../../../service/author.service';
import { ToastMediator } from '../../mediators/toast.mediator';
import { AbstractControl } from '@angular/forms';
import { TranslateService } from '@ngx-translate/core';

@Component({
    selector: 'author-popup',
    templateUrl: './author-popup.component.html',
    styleUrls: ['./author-popup.component.scss']
})

export class AuthorPopupComponent implements OnInit {

    public author = new Author(null, null);
    public authorForm: FormGroup;
    isNew = false;
    isBusy = false;

    @Output()
    cancelEvent = new EventEmitter();

    constructor(private authorService: AuthorService, private loaderMediator: LoaderMediator, private toastMediator: ToastMediator, private translate: TranslateService) {
        this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
        this.authorForm = new FormGroup({
            nameControl: new FormControl(this.author.name, Validators.compose([
                Validators.required,
                AuthorValidator.validateEmptyString(),
                AuthorValidator.validateTypeahead()
            ]))
        });
    }

    ngOnInit(): void {
    }

    public get nameControl(): AbstractControl {
        return this.authorForm.get('nameControl');
    }

    public get isNewAuthor(): boolean {
        return this.isNew;
    }

    public get isEmpty(): boolean {
        return this.isInvalid && !!this.nameControl.errors;
    }

    public get isInvalid(): boolean {
        return this.nameControl.invalid && (this.nameControl.dirty || this.nameControl.touched);
    }

    public initNewAuthor() {
        this.isNew = true;
        this.authorForm.reset();
    }

    public saveAuthor() {
        this.loaderMediator.execute(
            this.authorService.save(this.author).subscribe(
                author => {
                    this.initNewAuthor();
                    this.authorForm.reset();
                    this.toastMediator.show(this.translate.instant('AUTHORS.ADD'));
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
