import { Component, OnDestroy, OnInit } from '@angular/core';
import { PostsService } from '../posts.service';
import { PostViewModel } from '../post-view-model';
import { Subject, takeUntil } from 'rxjs';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit, OnDestroy {

  posts: PostViewModel[] = [];

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(private _postsService: PostsService) {
  }

  ngOnInit(): void {
    this._postsService.getAllPosts().pipe(takeUntil(this._unsubscribeAll)).subscribe(posts => {
      this.posts = posts;
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
