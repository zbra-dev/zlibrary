import { NgModule } from '@angular/core';
import { RepositoryModule } from './repository/repository.module';
import { ServiceModule } from './service/service.module';
import { MediatorsModule } from './ui/mediators/mediators.module';
import { TranslateModule } from '@ngx-translate/core';

@NgModule({
    imports: [
        RepositoryModule, 
        ServiceModule, 
        MediatorsModule
    ],
    exports: [
        TranslateModule
    ]
})
export class SharedModule {
}
