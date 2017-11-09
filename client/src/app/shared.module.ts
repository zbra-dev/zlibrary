import {NgModule} from '@angular/core';
import {RepositoryModule} from './repository/repository.module';
import {ServiceModule} from './service/service.module';

@NgModule({
    imports: [RepositoryModule, ServiceModule]
})
export class SharedModule {
}
