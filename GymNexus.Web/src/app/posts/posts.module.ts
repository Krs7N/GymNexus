import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { postsRoutes } from './posts.routes';
import { MatCardModule } from '@angular/material/card';
import { MatButtonModule } from '@angular/material/button';
import { PostsComponent } from './posts/posts.component';

@NgModule({
  declarations: [
    PostsComponent
  ],
  imports: [
    RouterModule.forChild(postsRoutes),
    CommonModule,
    MatCardModule,
    MatButtonModule
  ]
})
export class PostsModule { }
