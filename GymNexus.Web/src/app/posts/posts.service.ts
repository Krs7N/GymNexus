import { Injectable, Injector } from '@angular/core';
import { CrudService } from '../core/services/crud.service';
import { PostModel } from './post-model';
import { Observable } from 'rxjs';
import { PostViewModel } from './post-view-model';
import { environment } from 'src/environments/environment.development';
import { CommentViewModel } from './comment-view-model';

@Injectable({
  providedIn: 'root'
})
export class PostsService extends CrudService<PostModel> {

  constructor(injector: Injector) { 
    super(injector);
  }

  getAllPosts(): Observable<PostViewModel[]> {
    return this.httpClient.get<PostViewModel[]>(`${environment.apiBaseUrl}/${this.APIUrl}`);
  }

  getPost(id: number): Observable<PostViewModel> {
    return this.httpClient.get<PostViewModel>(`${environment.apiBaseUrl}/${this.APIUrl}/${id}`);
  }

  togglePostLike(id: number) : Observable<boolean> {
    return this.httpClient.put<boolean>(`${environment.apiBaseUrl}/${this.APIUrl}/${id}/like`, null);
  }

  createOrEditPostComment(id: number, comment: CommentViewModel): Observable<void> {
    debugger
    return this.httpClient.put<void>(`${environment.apiBaseUrl}/${this.APIUrl}/${id}/comment`, comment);
  }

  override getResourceUrl(): string {
    return 'posts';
  }
}
