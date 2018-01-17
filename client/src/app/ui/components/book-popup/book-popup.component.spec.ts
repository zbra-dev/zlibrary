import { async, ComponentFixture, TestBed } from '@angular/core/testing';

import { BookPopupComponent } from './book-popup.component';

describe('BookPopupComponent', () => {
  let component: BookPopupComponent;
  let fixture: ComponentFixture<BookPopupComponent>;

  beforeEach(async(() => {
    TestBed.configureTestingModule({
      declarations: [ BookPopupComponent ]
    })
    .compileComponents();
  }));

  beforeEach(() => {
    fixture = TestBed.createComponent(BookPopupComponent);
    component = fixture.componentInstance;
    fixture.detectChanges();
  });

  it('should create', () => {
    expect(component).toBeTruthy();
  });
});
