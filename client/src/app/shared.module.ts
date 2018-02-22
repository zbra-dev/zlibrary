import { NgModule } from '@angular/core';
import { RepositoryModule } from './repository/repository.module';
import { ServiceModule } from './service/service.module';
import { MediatorsModule } from './ui/mediators/mediators.module';

@NgModule({
    imports: [RepositoryModule, ServiceModule, MediatorsModule]
})
export class SharedModule {
}
