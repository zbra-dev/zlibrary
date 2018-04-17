import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IsbnPipe } from './isbn.pipe';

@NgModule({
  imports: [
    CommonModule
  ],
  exports: [
    IsbnPipe
  ],
  declarations: [
    IsbnPipe
  ]
})
export class PipesModule { }
