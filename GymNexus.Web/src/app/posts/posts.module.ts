import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { postsRoutes } from './posts.routes';
import { PostsComponent } from './posts/posts.component';
import { MaterialModule } from '../shared/material.module';
import { SharedModule } from '../shared/shared.module';
import { PostFormComponent } from './post-form/post-form.component';
import { ErrorPageComponent } from '../shared/error-page/error-page.component';

@NgModule({
  declarations: [
    PostsComponent,
    PostFormComponent
  ],
  imports: [
    RouterModule.forChild(postsRoutes),
    MaterialModule,
    SharedModule,
    ErrorPageComponent
  ]
})
export class PostsModule { }
