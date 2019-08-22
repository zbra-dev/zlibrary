import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, Validators, FormControl } from '@angular/forms';
import { Author } from '../../../model/author';
import { AuthorValidator } from '../../validators/author-validator';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { AuthorService } from '../../../service/author.service';
import { ToastMediator } from '../../mediators/toast.mediator';

@Component({
    selector: 'zli-author-popup',
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

    constructor(private authorService: AuthorService, private loaderMediator: LoaderMediator, private toastMediator: ToastMediator) {
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

    get nameControl() {
        return this.authorForm.get('nameControl');
    }

    public initNewAuthor() {
        this.isNew = true;
    }

    public saveAuthor() {
        this.loaderMediator.execute(
            this.authorService.save(this.author).subscribe(
                author => {
                    this.initNewAuthor();
                    this.authorForm.reset();
                    this.toastMediator.show("Autor adicionado com sucesso.");
                    this.onCancel();
                }, error => {
                    this.toastMediator.show("Autor já existe.");
                }
            )
        );
    }

    public onCancel(): void {
        this.cancelEvent.emit(null);
    }

}
