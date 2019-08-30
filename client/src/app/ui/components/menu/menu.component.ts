import { Component, OnInit, ViewEncapsulation, ViewChild } from '@angular/core';
import { ReservationHistoryComponent, ReservationHistoryType } from '../reservation-history/reservation-history.component';
import { RequestedBooksComponent} from '../requested-books/requested-books.component';
import { BsModalService } from 'ngx-bootstrap';
import { AuthService } from '../../../service/auth.service';
import { User } from '../../../model/user';
import { ApprovedBooksComponent } from '../approved-books/approved-books.component';
import { AuthorPopupComponent } from '../author-popup/author-popup.component';
import { PublisherPopupComponent } from '../publisher-popup/publisher-popup.component';
import { BookPopupComponent } from '../book-popup/book-popup.component';

@Component({
    selector: 'menu',
    templateUrl: './menu.component.html',
    styleUrls: ['./menu.component.scss'],
    encapsulation: ViewEncapsulation.None
})
export class MenuComponent implements OnInit {
    @ViewChild(BookPopupComponent)
    bookPopupComponent: BookPopupComponent;

    @ViewChild(AuthorPopupComponent)
    authorPopupComponent: AuthorPopupComponent;

    @ViewChild(PublisherPopupComponent)
    publisherPopupComponent: PublisherPopupComponent;

    public user: User;
    public showUserMenu: boolean;
    public isBusy = false;

    constructor(private modalService: BsModalService,
        private service: AuthService) {
    }

    ngOnInit() {
        this.user = this.service.getLoggedUser();
    }

    public viewReservationHistory(): void {
        this.modalService.show(ReservationHistoryComponent);
    }

    public onLogout() {
        this.service.logout();
        window.location.reload();
    }

    public get isAdmin() { return this.user.isAdministrator; }

    public toggleMenu() {
        this.showUserMenu = !this.showUserMenu;
    }

    public showRentedBooks() {
        const reservationHistoryModalControl = this.modalService.show(ReservationHistoryComponent);
        const reservationHistoryComponent = reservationHistoryModalControl.content as ReservationHistoryComponent;
        reservationHistoryComponent.reservationHistoryType = ReservationHistoryType.Loaned;
        reservationHistoryComponent.modalControl = reservationHistoryModalControl;
        this.toggleMenu();
    }

    public showWaitingList() {
        const reservationHistoryModalControl = this.modalService.show(ReservationHistoryComponent);
        const reservationHistoryComponent = reservationHistoryModalControl.content as ReservationHistoryComponent;
        reservationHistoryComponent.reservationHistoryType = ReservationHistoryType.Waiting;
        reservationHistoryComponent.modalControl = reservationHistoryModalControl;
        this.toggleMenu();
    }

    public showRequestedBooks() {
        const requestedBooksModalControl = this.modalService.show(RequestedBooksComponent);
        const requestedBooksComponet = requestedBooksModalControl.content as RequestedBooksComponent;
        requestedBooksComponet.modalControl = requestedBooksModalControl;
        this.toggleMenu();
    }

    public addNewBook() {
        this.bookPopupComponent.initNewBook();
        this.toggleBookSidebar();
        this.toggleMenu();
    }

    public addNewAuthor() {
        this.authorPopupComponent.initNewAuthor();
        this.toggleAuthorSidebar();
        this.toggleMenu();
    }

    public addNewPublisher() {
        this.publisherPopupComponent.initNewPublisher();
        this.togglePublisherSidebar();
        this.toggleMenu();
    }

    public showApprovedBooks() {
        const approvedBooksModalControl = this.modalService.show(ApprovedBooksComponent);
        const approvedBooksComponent = approvedBooksModalControl.content as ApprovedBooksComponent;
        approvedBooksComponent.modalControl = approvedBooksModalControl;
        this.toggleMenu();
    }

    public toggleBookSidebar(): void {
        document.getElementById('background-book').classList.toggle('active');
        if (document.getElementById('sidebar-book').classList.toggle('active') === false) {
            this.isBusy = false;
        }
    }

    public toggleAuthorSidebar(): void {
        document.getElementById('background-author').classList.toggle('active');
        if (document.getElementById('sidebar-author').classList.toggle('active') === false) {
            this.isBusy = false;
        }
    }

    public togglePublisherSidebar(): void {
        document.getElementById('background-publisher').classList.toggle('active');
        if (document.getElementById('sidebar-publisher').classList.toggle('active') === false) {
            this.isBusy = false;
        }
    }

}
