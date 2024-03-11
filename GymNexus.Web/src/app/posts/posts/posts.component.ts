import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { PostsService } from '../posts.service';
import { PostViewModel } from '../post-view-model';
import { Subject, takeUntil } from 'rxjs';
import { format } from 'date-fns';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { PostFormComponent } from '../post-form/post-form.component';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss'],
})
export class PostsComponent implements OnInit, OnDestroy {

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  posts: PostViewModel[] = [];
  currentPosts: PostViewModel[] = [];
  newComment!: string;
  error: any;

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _postsService: PostsService,
    private _dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadPosts();
  }

  toggleLike(post: PostViewModel): void {
    // Increment likes for the post; replace with real API call if needed
    // if (!post.likes) {
    //   post.likes = 0;
    // }
    // post.likes += 1;
  }

  addComment(post: PostViewModel): void {
    if (!post.comments) {
      post.comments = [];
    }

    const date = new Date();
    const formattedDate = format(date, 'dd/MM/yyyy HH:mm');

    post.comments.push({
      createdBy: 'root@abv.bg',
      createdOn: formattedDate,
      content: this.newComment,
    });
    this.newComment = '';
  }

  openAddPost() {
    const dialogRef = this._dialog.open(PostFormComponent, {
      width: '60%',
      height: '80%',
    });

    dialogRef.componentInstance.postAdded
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(() => {
        this.loadPosts();
      });
  }

  private loadPosts(): void {
    this._postsService
      .getAllPosts()
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: (posts) => {
          this.posts = posts;

          if (this.paginator) {
            this.paginator.pageIndex = 0;
          }

          this.setPage(0, 2);
        },
        error: (e) => {
          this.error = e;
        },
      });
  }

  changePage(event: PageEvent): void {
    this.setPage(event.pageIndex, event.pageSize);
  }

  private setPage(pageIndex: number, pageSize: number): void {
    const start = pageIndex * pageSize;
    this.currentPosts = this.posts.slice(start, start + pageSize);
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
