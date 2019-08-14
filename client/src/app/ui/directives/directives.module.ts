import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { ScrollIntoViewDirective } from './scroll-into-view.directive';
import { NumericDirective } from './numeric.directive';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    ScrollIntoViewDirective,
    NumericDirective
  ],
  declarations: [
    ScrollIntoViewDirective,
    NumericDirective
  ]
})
export class DirectivesModule { }
