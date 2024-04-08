import { Component, OnDestroy, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { ActivatedRoute, Router } from '@angular/router';
import { Subject, takeUntil } from 'rxjs';
import { PostOverviewModel } from 'src/app/posts/post-overview-model';
import { PostsService } from 'src/app/posts/posts.service';
import { ConfirmDialogComponent } from 'src/app/shared/confirm-dialog/confirm-dialog.component';
import { SnackbarService } from 'src/app/shared/services/snackbar.service';

@Component({
  selector: 'app-admin-manage-posts',
  templateUrl: './admin-manage-posts.component.html',
  styleUrls: ['./admin-manage-posts.component.scss']
})
export class AdminManagePostsComponent implements OnInit, OnDestroy {

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  mostLikedPost!: PostOverviewModel;
  mostCommentedPost!: PostOverviewModel;

  error: any;

  constructor(
    private _route: ActivatedRoute,
    private _router: Router,
    private _snackbarService: SnackbarService,
    private _postsService: PostsService,
    public dialog: MatDialog
  ) { }

  ngOnInit(): void {
    this.mostLikedPost = this._route.snapshot.data['mostLikedPost'];
    this.mostCommentedPost = this._route.snapshot.data['mostCommentedPost'];
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
            this._router.navigate(['/admin', 'dashboard']);
          },
          error: (e) => {
            this.error = e;
          },
        });
      }
    });
  }

  ngOnDestroy(): void {
    this._unsubscribeAll.next(null);
    this._unsubscribeAll.complete();
  }
}
