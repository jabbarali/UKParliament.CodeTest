import { BrowserModule } from '@angular/platform-browser';
import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { provideHttpClient, withInterceptorsFromDi } from '@angular/common/http';
import { RouterModule } from '@angular/router';

import { AppComponent } from './app.component';
import { PersonManagerComponent } from './components/person-manager/person-manager.component';
import { PersonListComponent } from './components/person-list/person-list.component';
import { PersonEditorComponent } from './components/person-editor/person-editor.component';

@NgModule({
  declarations: [
    AppComponent,
    PersonManagerComponent,
    PersonListComponent,
    PersonEditorComponent
  ],
  imports: [
    BrowserModule.withServerTransition({ appId: 'ng-cli-universal' }),
    FormsModule,
    RouterModule.forRoot([
      { path: '', component: PersonManagerComponent, pathMatch: 'full' }
    ])
  ],
  providers: [provideHttpClient(withInterceptorsFromDi())],
  bootstrap: [AppComponent]
})
export class AppModule { }
