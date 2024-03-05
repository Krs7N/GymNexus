import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { postsRoutes } from './posts.routes';



@NgModule({
  declarations: [],
  imports: [
    RouterModule.forChild(postsRoutes),
    CommonModule
  ]
})
export class PostsModule { }
