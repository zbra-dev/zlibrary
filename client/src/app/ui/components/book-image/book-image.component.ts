import { Component, OnInit, ViewEncapsulation, Input, Output, EventEmitter } from '@angular/core';
import { CoverImageService } from '../../../service/cover-image.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { Book } from '../../../model/book';

const BASE64_BASE_URL = 'data:image/jpg;base64,';

@Component({
    selector: 'book-image',
    templateUrl: './book-image.component.html',
    styleUrls: ['./book-image.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class BookImageComponent implements OnInit {
    public coverImageURL: string;
    public isLoading: boolean;
    public bookTitle: string;

    constructor(private coverImageService: CoverImageService,
        private loaderMediator: LoaderMediator,
        private toastMediator: ToastMediator) {
            this.isLoading = true;
    }

    @Input()
    set book(book: Book) {
        if (!!book && !!book.id) {
            this.loadImage(book.coverImageKey);
            this.bookTitle = book.title;
        }
    }

    ngOnInit() {
    }

    public loadImage(coverImageKey: string): void {
        if (!!coverImageKey) {
            this.loaderMediator.execute(
                this.coverImageService.loadImage(coverImageKey).subscribe(
                    image => {
                        if(!!image){
                            this.coverImageURL = `${BASE64_BASE_URL}${image}`;
                        } else {
                            this.coverImageURL = null;
                        }
                    }, error => {
                        this.coverImageURL = null;
                        this.toastMediator.show(`Erro ao carregar a imagem: ${error}`);
                    }, () => {
                        this.isLoading = false;
                    }
                )
            );
            
        }
    }
}
