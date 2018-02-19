import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScrollIntoViewDirective } from './scroll-into-view.directive';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    ScrollIntoViewDirective
  ],
  declarations: [
    ScrollIntoViewDirective
  ]
})
export class DirectivesModule { }
