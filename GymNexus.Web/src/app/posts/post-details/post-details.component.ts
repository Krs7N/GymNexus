import { Component, OnDestroy, OnInit } from '@angular/core';
import { PostViewModel } from '../post-view-model';
import { ActivatedRoute, Router } from '@angular/router';
import { PostsService } from '../posts.service';
import { AuthService } from 'src/app/auth/services/auth.service';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';
import { Subject, takeUntil } from 'rxjs';
import { UserModel } from 'src/app/auth/models/user-model';
import { CommentViewModel } from '../comment-view-model';
import { MatDialog } from '@angular/material/dialog';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { PostFormComponent } from '../post-form/post-form.component';

@Component({
  selector: 'app-post-details',
  templateUrl: './post-details.component.html',
  styleUrls: ['./post-details.component.scss']
})
export class PostDetailsComponent implements OnInit, OnDestroy {

  commentMap: Map<number, string> = new Map<number, string>();
  editCommentId: number | null = null;
  postId: number = 0;
  user?: UserModel;
  post: PostViewModel = {} as PostViewModel;
  error: any;

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _authService: AuthService,
    private _postsService: PostsService,
    private _snackbarService: SnackbarService,
    public dialog: MatDialog
    ) {}

  ngOnInit(): void {
    this.postId = this._route.snapshot.params['id'];
    this.user = this._authService.getUser();

    this.loadPost();
  }

  editPost(post: PostViewModel) {
    const dialogRef = this.dialog.open(PostFormComponent, {
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
        this.loadPost();
      });
  }

  deletePost(id: number) {
    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Delete Post',
        message: 'Are you sure you want to delete this post?'
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this._postsService.delete(id).pipe(takeUntil(this._unsubscribeAll)).subscribe({
          next: () => {
            this._snackbarService.openSuccess('Post deleted successfully');
            this._router.navigate(['/posts']);
          },
          error: (e) => {
            this.error = e;
          },
        });
      }
    });
  }

  toggleLike(): void {
    this._postsService.togglePostLike(this.post.id).subscribe({
      next: (hasUserLikedPost) => {

        if (hasUserLikedPost) {
          this._snackbarService.openSuccess('You liked this post');
        } else {
          this._snackbarService.openSuccess('You no longer like this post');
        }

        this.loadPost();
      },
      error: (e) => {
        console.error(e);
      },
    });
  }

  addComment(): void {
    const comment = this.commentMap.get(this.post.id);

    if (comment === undefined || comment.trim() === '') {
      return;
    }

    let date = new Date().toISOString();

    this._postsService.createOrEditPostComment(this.post.id, { content: comment, createdBy: this.user?.email, createdOn: date } as CommentViewModel)
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: () => {
          this._snackbarService.openSuccess(`Successfully added comment to post - ${this.post.title}`);
          this.loadPost();
        },
        error: (e) => {
          console.error(e);
        },
      });

    this.commentMap.set(this.post.id, '');
  }

  editComment(id: number) {
    this.editCommentId = id;
  }

  submitEditComment(comment: CommentViewModel, postId: number): void {
    this.editCommentId = null;

    let date = new Date().toISOString();

    this._postsService.createOrEditPostComment(postId, { id: comment.id, content: comment.content, createdBy: this.user?.email, createdOn: date, isEdited: true } as CommentViewModel)
      .pipe(takeUntil(this._unsubscribeAll))
      .subscribe({
        next: () => {
          this._snackbarService.openSuccess(`Successfully edited comment`);
          comment.isEdited = true;
          this.loadPost();
        },
        error: (e) => {
          console.error(e);
        },
      });
  }

  cancelEditComment(): void {
    this.editCommentId = null;
  }

  deleteComment(commentId: number, postId: number) {

    const dialogRef = this.dialog.open(ConfirmDialogComponent, {
      width: '400px',
      data: {
        title: 'Deletion',
        message: 'Are you sure you want to delete this comment?'
      }
    });

    dialogRef.afterClosed().pipe(takeUntil(this._unsubscribeAll)).subscribe(result => {
      if (result) {
        this._postsService.deletePostComment(postId, commentId)
          .pipe(takeUntil(this._unsubscribeAll))
          .subscribe({
            next: () => {
              this._snackbarService.openSuccess(`Successfully deleted comment`);
              this.loadPost();
            },
            error: (e) => {
              console.error(e);
            },
          });
      }
    });
  }

  private loadPost(): void {
    this._postsService.getPost(this.postId).pipe(takeUntil(this._unsubscribeAll)).subscribe({
      next: (post: PostViewModel) => {
        this.post = post;
      },
      error: (e) => {
        console.error(e);
      }
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
