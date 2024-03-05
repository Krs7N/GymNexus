import { Component, OnDestroy, OnInit } from '@angular/core';
import { PostsService } from '../posts.service';
import { PostViewModel } from '../post-view-model';
import { Subject, takeUntil } from 'rxjs';
import { format } from 'date-fns';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit, OnDestroy {

  posts: PostViewModel[] = [];
  newComment!: string;

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(private _postsService: PostsService) {
  }

  ngOnInit(): void {
    this._postsService.getAllPosts().pipe(takeUntil(this._unsubscribeAll)).subscribe(posts => {
      this.posts = posts;
    });
  }

  toggleLike(post: PostViewModel): void {
    // Increment likes for the post; replace with real API call if needed
    if (!post.likes) {
      post.likes = 0;
    }
    post.likes += 1;
  }

  addComment(post: PostViewModel): void {
    if (!post.comments) {
      post.comments = [];
    }

    const date = new Date();
    const formattedDate = format(date, 'dd/MM/yyyy HH:mm');

    post.comments.push({ createdBy: 'root@abv.bg', createdOn: formattedDate, content: this.newComment });
    this.newComment = '';
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
