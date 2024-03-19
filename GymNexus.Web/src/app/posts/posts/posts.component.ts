import { Component, OnDestroy, OnInit, ViewChild } from '@angular/core';
import { PostsService } from '../posts.service';
import { PostViewModel } from '../post-view-model';
import { Subject, takeUntil } from 'rxjs';
import { format } from 'date-fns';
import { MatPaginator, PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { PostFormComponent } from '../post-form/post-form.component';
import { AuthService } from 'src/app/auth/services/auth.service';
import { UserModel } from 'src/app/auth/models/user-model';
import { SnackbarService } from 'src/app/shared/snackbar.service';
import { CommentViewModel } from '../comment-view-model';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss'],
})
export class PostsComponent implements OnInit, OnDestroy {

  @ViewChild(MatPaginator) paginator!: MatPaginator;

  commentMap: Map<number, string> = new Map<number, string>();
  posts: PostViewModel[] = [];
  user: UserModel | undefined;
  currentPosts: PostViewModel[] = [];
  error: any;

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _postsService: PostsService,
    private _authService: AuthService,
    private _snackbarService: SnackbarService,
    private _dialog: MatDialog
  ) {}

  ngOnInit(): void {
    this.loadPosts();

    this.user = this._authService.getUser();
  }

  toggleLike(post: PostViewModel): void {
    this._postsService.togglePostLike(post.id).subscribe({
      next: (hasUserLikedPost) => {

        if (hasUserLikedPost) {
          this._snackbarService.openSuccess('You liked this post');
        } else {
          this._snackbarService.openSuccess('You no longer like this post');
        }

        this.loadPosts(true);
      },
      error: (e) => {
        this.error = e;
      },
    });
  }

  addComment(post: PostViewModel): void {
    const comment = this.commentMap.get(post.id);

    if (comment === undefined || comment.trim() === '') {
      return;
    }

    const user = this._authService.getUser();
    let date = new Date().toISOString();

    this._postsService.createOrEditPostComment(post.id, { content: comment, createdBy: user?.email, createdOn: date } as CommentViewModel)
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: () => {
          this._snackbarService.openSuccess(`Successfully added comment to ${post.title}`);
          this.loadPosts(true);
        },
        error: (e) => {
          this.error = e;
        },
      });

    this.commentMap.set(post.id, '');
  }

  openAddPost() {
    const dialogRef = this._dialog.open(PostFormComponent, {
      width: '60%',
      height: '80%',
      data: {
        title: 'Add New Post'
      },
    });

    dialogRef.componentInstance.postAdded
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(() => {
        this.loadPosts();
      });
  }

  editPost(post: PostViewModel) {
    const dialogRef = this._dialog.open(PostFormComponent, {
      width: '60%',
      height: '80%',
      data: {
        post: post,
        title: 'Edit Post'
      },
    });

    dialogRef.componentInstance.postAdded
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe(() => {
        this.loadPosts(true);
      });
  }

  deletePost(post: PostViewModel) {
    const dialogRef = this._dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Delete Post',
        message: 'Are you sure you want to delete this post?'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._postsService.delete(post.id).pipe(takeUntil(this._unsubscribeAll)).subscribe({
          next: () => {
            this._snackbarService.openSuccess('Post deleted successfully');
            this.loadPosts(true);
          },
          error: (e) => {
            this.error = e;
          },
        });
      }
    });

  }

  private loadPosts(innerLoad: boolean = false): void {
    this._postsService
      .getAllPosts()
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: (posts) => {
          this.posts = posts;

          if (!innerLoad) {
            this.paginator.pageIndex = 0;
            this.setPage(0, 2);
          } else {
            this.setPage(this.paginator.pageIndex, this.paginator.pageSize);
          }
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
