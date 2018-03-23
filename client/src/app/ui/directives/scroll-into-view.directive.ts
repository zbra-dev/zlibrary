import { Directive, ElementRef, SimpleChanges, Input, OnChanges } from '@angular/core';

@Directive({
  selector: '[zliScrollIntoView]'
})
export class ScrollIntoViewDirective implements OnChanges {

  @Input()
  public zliScrollIntoView: boolean;

  constructor(private el: ElementRef) { }

  public ngOnChanges(changes: SimpleChanges): void {
    if (!!changes.zliScrollIntoView.currentValue) {

      const parent = this.el.nativeElement.parentNode,
        overTop = this.el.nativeElement.offsetTop + this.el.nativeElement.clientHeight < parent.scrollTop,
        overBottom = (this.el.nativeElement.offsetTop + this.el.nativeElement.clientHeight) > (parent.scrollTop + parent.offsetHeight);

      if (overBottom) {
        parent.scrollTop = this.el.nativeElement.offsetTop - parent.clientHeight + this.el.nativeElement.clientHeight;
      } else if (overTop) {
        parent.scrollTop = this.el.nativeElement.offsetTop - this.el.nativeElement.clientHeight + parent.offsetTop;
      }
    }
  }
}
