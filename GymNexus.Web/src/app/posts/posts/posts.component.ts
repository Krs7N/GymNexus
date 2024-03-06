declare var cloudinary: any;

import { Component, OnDestroy, OnInit } from '@angular/core';
import { PostsService } from '../posts.service';
import { PostViewModel } from '../post-view-model';
import { Subject, takeUntil } from 'rxjs';
import { format } from 'date-fns';
import { PageEvent } from '@angular/material/paginator';
import { MatDialog } from '@angular/material/dialog';
import { PostFormComponent } from '../post-form/post-form.component';

@Component({
  selector: 'app-posts',
  templateUrl: './posts.component.html',
  styleUrls: ['./posts.component.scss']
})
export class PostsComponent implements OnInit, OnDestroy {

  posts: PostViewModel[] = [];
  currentPosts: PostViewModel[] = [];
  newComment!: string;

  private _unsubscribeAll: Subject<any> = new Subject<any>();

  constructor(
    private _postsService: PostsService,
    private _dialog: MatDialog) {
  }

  ngOnInit(): void {
    this._postsService.getAllPosts().pipe(takeUntil(this._unsubscribeAll)).subscribe(posts => {
      this.posts = posts;
      this.setPage(0, 2);
    });
  }

  toggleLike(post: PostViewModel): void {
    // Increment likes for the post; replace with real API call if needed
    // if (!post.likes) {
    //   post.likes = 0;
    // }
    // post.likes += 1;
    // cloudinary.openUploadWidget({
    //   cloudName: 'dekvgy42s',
    //   uploadPreset: 'gymnexus',
    //   sources: ['local', 'url', 'camera', 'image_search'],
    //   showAdvancedOptions: false,
    //   cropping: true,
    //   multiple: false,
    //   defaultSource: 'local',
    //   styles: {
    //     palette: {
    //       window: "#FFFFFF",
    //       windowBorder: "#90A0B3",
    //       tabIcon: "#0078FF",
    //       menuIcons: "#5A616A",
    //       textDark: "#000000",
    //       textLight: "#FFFFFF",
    //       link: "#0078FF",
    //       action: "#FF620C",
    //         inactiveTabIcon: "#0E2F5A",
    //         error: "#F44235",
    //         inProgress: "#0078FF",
    //         complete: "#20B832",
    //         sourceBg: "#E4EBF1"
    //       },
    //       fonts: {
    //         default: null,
    //         "'Fira Sans', sans-serif": {
    //           url: "https://fonts.googleapis.com/css?family=Fira+Sans",
    //           active: true
    //         }
    //       }
    //     }
    //   }, (error: Error, result: any) => {
    //     if (!error && result && result.event === "success") {
    //       console.log('Upload Widget event - ', result);
    //       // Handle the uploaded image's URL
    //       // e.g., updating a form control or state variable with the image URL
    //     }
    //   });
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

  openAddPost() {
    const dialogRef = this._dialog.open(PostFormComponent, {
      width: '400px',
      data: {
      }
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
