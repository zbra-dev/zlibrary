import { NgModule } from '@angular/core';
import { GroupedOrdersConverter } from './grouped-orders.converter';

@NgModule({
    providers: [
        GroupedOrdersConverter
    ]
})

export class ConverterModule {
}
