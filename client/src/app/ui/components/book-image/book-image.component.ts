import { Component, OnInit, ViewEncapsulation, Input, Output, EventEmitter } from '@angular/core';
import { CoverImageService } from '../../../service/cover-image.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { Book } from '../../../model/book';

const BASE64_BASE_URL = 'data:image/jpg;base64,';

@Component({
    selector: 'zli-book-image',
    templateUrl: './book-image.component.html',
    styleUrls: ['./book-image.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class BookImageComponent implements OnInit {
    public coverImageURL: string;
    public isLoading: boolean;

    constructor(private coverImageService: CoverImageService,
        private loaderMediator: LoaderMediator,
        private toastMediator: ToastMediator) {
            this.isLoading = true;
    }

    @Input()
    set book(book: Book) {
        if (!!book && !!book.id) {
            this.loadImage(book.coverImageKey);
        }
    }

    ngOnInit() {
    }

    public loadImage(coverImageKey: string): void {
        if (!!coverImageKey) {
            this.loaderMediator.execute(
                this.coverImageService.loadImage(coverImageKey).subscribe(
                    image => {
                        this.coverImageURL = `${BASE64_BASE_URL}${image}`;
                    }, error => {
                        this.coverImageURL = null;
                    }, () => {
                        this.isLoading = false;
                    }
                )
            );
        }
    }
}
