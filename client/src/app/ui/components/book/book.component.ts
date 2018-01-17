import { Component, Input, OnInit, ViewEncapsulation } from '@angular/core';
import { Book } from '../../../model/book';
import { CoverImageService } from '../../../service/coverImage.service';
import { LoaderMediator } from '../../mediators/loader.mediator';
import { ToastMediator } from '../../mediators/toast.mediator';
import { BookService } from '../../../service/book.service';

@Component({
    selector: 'zli-book',
    templateUrl: './book.component.html',
    styleUrls: ['./book.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class BookComponent implements OnInit {
    @Input()
    public book: Book;
    public image: string;
    public isBusy = false;
    public isAvailable: boolean;
    public isAdmin = true;
 
    constructor(private coverImageService: CoverImageService,
        private loaderMediator: LoaderMediator,
        private bookService: BookService,
        private toastMediator: ToastMediator) {
            this.loaderMediator.onLoadChanged.subscribe(loading => this.isBusy = loading);
    }

    public ngOnInit(): void {
        this.getImage();
        this.IsAvailable();
    }

    public getImage(): void {
        if (this.book != null) {
            this.loaderMediator.execute(
                this.coverImageService.LoadImage(this.book).subscribe(
                    image => {
                        this.image = image;
                    }, error => {
                        this.image = null;
                        this.toastMediator.show(`Error loading books: ${error}`);
                    }
                )
            );
        }
    }

    public IsAvailable(): void{
        if (this.book != null) {
          this.loaderMediator.execute(
            this.bookService.IsBookAvailable(this.book).subscribe(
              bool => {
                console.log("reservations: " + bool);
                  this.isAvailable = bool;
              }, error => {
                this.isAvailable = null;
                this.toastMediator.show(`Error loading books: ${error}`);
              }
            )
          );
        }
      }

      public delete()
      {
        if (this.book != null) {
            this.loaderMediator.execute(
              this.bookService.delete(this.book).subscribe(
                  error => {
                  this.isAvailable = null;
                  this.toastMediator.show(`Error loading books: ${error}`);
                }
              )
            );
          }
          this.bookService.findAll();    
      }
}
